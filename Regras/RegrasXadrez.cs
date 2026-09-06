using Xadrez.Classes;

namespace Xadrez.Regras
{
    public class RegrasXadrez
    {
        public bool MovimentoValido(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino,
            string cor)
        {
            return ObterMovimentoValido(
                tabuleiro,
                origem,
                destino,
                cor) != null;
        }

        public Movimento? ObterMovimentoValido(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino,
            string cor)
        {
            if (!origem.Valida || !destino.Valida || origem == destino)
                return null;

            Peca? peca = tabuleiro[origem];

            if (peca == null || peca.Cor != cor)
                return null;

            Peca? destinoOcupado = tabuleiro[destino];

            if (destinoOcupado != null &&
                destinoOcupado.Cor == cor)
            {
                return null;
            }

            if (destinoOcupado?.Tipo == "Rei")
                return null;

            Movimento movimento = new(origem, destino);

            if (!MovimentoGeometricoValido(tabuleiro, movimento))
                return null;

            Tabuleiro simulacao = tabuleiro.Clonar();
            AplicarMovimento(simulacao, movimento);

            if (EstaEmXeque(simulacao, cor))
                return null;

            return movimento;
        }

        public void AplicarMovimento(
            Tabuleiro tabuleiro,
            Movimento movimento)
        {
            Peca? peca = tabuleiro[movimento.Origem];

            if (peca == null)
                throw new InvalidOperationException("Não existe peça na origem.");

            bool enPassant =
                peca.Tipo == "Peao" &&
                movimento.Origem.Coluna != movimento.Destino.Coluna &&
                tabuleiro[movimento.Destino] == null;

            movimento.EnPassant = enPassant;

            if (enPassant)
            {
                int direcao =
                    peca.Cor == "Branco" ? 1 : -1;

                int linhaCapturada =
                    movimento.Destino.Linha - direcao;

                if (linhaCapturada >= 0 &&
                    linhaCapturada < 8)
                {
                    tabuleiro[
                        linhaCapturada,
                        movimento.Destino.Coluna] = null;
                }
            }

            bool roque =
                peca.Tipo == "Rei" &&
                Math.Abs(
                    movimento.Destino.Coluna -
                    movimento.Origem.Coluna) == 2;

            movimento.Roque = roque;

            tabuleiro[movimento.Origem] = null;

            peca.JaMoveu = true;

            tabuleiro[movimento.Destino] = peca;

            if (roque)
            {
                int origemTorre =
                    movimento.Destino.Coluna == 2 ? 0 : 7;

                int destinoTorre =
                    movimento.Destino.Coluna == 2 ? 3 : 5;

                Peca? torre =
                    tabuleiro[
                        movimento.Origem.Linha,
                        origemTorre];

                if (torre != null &&
                    torre.Tipo == "Torre")
                {
                    tabuleiro[
                        movimento.Origem.Linha,
                        origemTorre] = null;

                    torre.JaMoveu = true;

                    tabuleiro[
                        movimento.Origem.Linha,
                        destinoTorre] = torre;
                }
            }

            tabuleiro.UltimoMovimento = new Movimento(
                movimento.Origem,
                movimento.Destino)
            {
                Roque = movimento.Roque,
                EnPassant = movimento.EnPassant
            };
        }

        public bool EstaEmXeque(
            Tabuleiro tabuleiro,
            string cor)
        {
            Posicao? rei = LocalizarRei(tabuleiro, cor);

            if (rei == null)
                return true;

            return CasaAtacada(
                tabuleiro,
                rei.Value,
                cor);
        }

        public bool XequeMate(
            Tabuleiro tabuleiro,
            string cor)
        {
            return EstaEmXeque(tabuleiro, cor) &&
                   !ExisteMovimentoLegal(tabuleiro, cor);
        }

        public bool Afogamento(
            Tabuleiro tabuleiro,
            string cor)
        {
            return !EstaEmXeque(tabuleiro, cor) &&
                   !ExisteMovimentoLegal(tabuleiro, cor);
        }

        public bool ExisteMovimentoLegal(
            Tabuleiro tabuleiro,
            string cor)
        {
            foreach (var item in tabuleiro.ObterPecas())
            {
                if (item.Peca.Cor != cor)
                    continue;

                for (int linha = 0; linha < 8; linha++)
                {
                    for (int coluna = 0; coluna < 8; coluna++)
                    {
                        Posicao destino = new(linha, coluna);

                        if (ObterMovimentoValido(
                                tabuleiro,
                                item.Posicao,
                                destino,
                                cor) != null)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool MovimentoGeometricoValido(
            Tabuleiro tabuleiro,
            Movimento movimento)
        {
            Peca? peca = tabuleiro[movimento.Origem];

            if (peca == null)
                return false;

            int deltaLinha =
                movimento.Destino.Linha -
                movimento.Origem.Linha;

            int deltaColuna =
                movimento.Destino.Coluna -
                movimento.Origem.Coluna;

            int absLinha = Math.Abs(deltaLinha);
            int absColuna = Math.Abs(deltaColuna);

            switch (peca.Tipo)
            {
                case "Peao":
                    return MovimentoPeaoValido(
                        tabuleiro,
                        movimento.Origem,
                        movimento.Destino);

                case "Torre":
                    return MovimentoTorreValido(
                        tabuleiro,
                        movimento.Origem,
                        movimento.Destino);

                case "Cavalo":
                    return
                        (absLinha == 2 && absColuna == 1) ||
                        (absLinha == 1 && absColuna == 2);

                case "Bispo":
                    return absLinha == absColuna &&
                           CaminhoLivre(
                               tabuleiro,
                               movimento.Origem,
                               movimento.Destino);

                case "Rainha":
                    if (absLinha == absColuna ||
                        movimento.Origem.Linha == movimento.Destino.Linha ||
                        movimento.Origem.Coluna == movimento.Destino.Coluna)
                    {
                        return CaminhoLivre(
                            tabuleiro,
                            movimento.Origem,
                            movimento.Destino);
                    }

                    return false;

                case "Rei":
                    if (absLinha <= 1 &&
                        absColuna <= 1)
                    {
                        return absLinha != 0 || absColuna != 0;
                    }

                    return MovimentoRoqueValido(
                        tabuleiro,
                        movimento.Origem,
                        movimento.Destino);

                default:
                    return false;
            }
        }

        private bool MovimentoPeaoValido(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino)
        {
            Peca? peao = tabuleiro[origem];

            if (peao == null || peao.Tipo != "Peao")
                return false;

            int direcao =
                peao.Cor == "Branco" ? 1 : -1;

            int diferencaLinha =
                destino.Linha - origem.Linha;

            int diferencaColuna =
                destino.Coluna - origem.Coluna;

            Peca? destinoOcupado = tabuleiro[destino];

            if (diferencaColuna == 0 &&
                diferencaLinha == direcao &&
                destinoOcupado == null)
            {
                return true;
            }

            int linhaInicial =
                peao.Cor == "Branco" ? 1 : 6;

            if (diferencaColuna == 0 &&
                diferencaLinha == direcao * 2 &&
                origem.Linha == linhaInicial &&
                !peao.JaMoveu &&
                destinoOcupado == null)
            {
                Posicao intermediaria =
                    new(
                        origem.Linha + direcao,
                        origem.Coluna);

                return tabuleiro[intermediaria] == null;
            }

            if (Math.Abs(diferencaColuna) == 1 &&
                diferencaLinha == direcao)
            {
                if (destinoOcupado != null &&
                    destinoOcupado.Cor != peao.Cor)
                {
                    return true;
                }

                return PodeFazerEnPassant(
                    tabuleiro,
                    origem,
                    destino);
            }

            return false;
        }

        private bool PodeFazerEnPassant(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino)
        {
            Movimento? ultimo =
                tabuleiro.UltimoMovimento;

            if (ultimo == null)
                return false;

            if (tabuleiro[destino] != null)
                return false;

            Peca? peaoAtual =
                tabuleiro[origem];

            Peca? peaoAdjacente =
                tabuleiro[
                    origem.Linha,
                    destino.Coluna];

            if (peaoAtual == null ||
                peaoAdjacente == null)
            {
                return false;
            }

            if (peaoAdjacente.Tipo != "Peao" ||
                peaoAdjacente.Cor == peaoAtual.Cor)
            {
                return false;
            }

            if (ultimo.Destino.Linha != origem.Linha ||
                ultimo.Destino.Coluna != destino.Coluna)
            {
                return false;
            }

            if (Math.Abs(
                    ultimo.Destino.Linha -
                    ultimo.Origem.Linha) != 2)
            {
                return false;
            }

            return true;
        }

        private bool MovimentoTorreValido(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino)
        {
            if (origem.Linha != destino.Linha &&
                origem.Coluna != destino.Coluna)
            {
                return false;
            }

            return CaminhoLivre(
                tabuleiro,
                origem,
                destino);
        }

        private bool CaminhoLivre(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino)
        {
            int direcaoLinha =
                Math.Sign(
                    destino.Linha -
                    origem.Linha);

            int direcaoColuna =
                Math.Sign(
                    destino.Coluna -
                    origem.Coluna);

            int linha =
                origem.Linha + direcaoLinha;

            int coluna =
                origem.Coluna + direcaoColuna;

            while (linha != destino.Linha ||
                   coluna != destino.Coluna)
            {
                if (tabuleiro[linha, coluna] != null)
                    return false;

                linha += direcaoLinha;
                coluna += direcaoColuna;
            }

            return true;
        }

        private bool MovimentoRoqueValido(
            Tabuleiro tabuleiro,
            Posicao origem,
            Posicao destino)
        {
            Peca? rei = tabuleiro[origem];

            if (rei == null ||
                rei.Tipo != "Rei" ||
                rei.JaMoveu)
            {
                return false;
            }

            int linhaInicial =
                rei.Cor == "Branco" ? 0 : 7;

            if (origem.Linha != linhaInicial ||
                destino.Linha != linhaInicial ||
                origem.Coluna != 4)
            {
                return false;
            }

            if (destino.Coluna != 2 &&
                destino.Coluna != 6)
            {
                return false;
            }

            if (EstaEmXeque(tabuleiro, rei.Cor))
                return false;

            bool grande =
                destino.Coluna == 2;

            int colunaTorre =
                grande ? 0 : 7;

            Peca? torre =
                tabuleiro[
                    linhaInicial,
                    colunaTorre];

            if (torre == null ||
                torre.Tipo != "Torre" ||
                torre.Cor != rei.Cor ||
                torre.JaMoveu)
            {
                return false;
            }

            int inicio =
                Math.Min(origem.Coluna, colunaTorre) + 1;

            int fim =
                Math.Max(origem.Coluna, colunaTorre);

            for (int coluna = inicio;
                 coluna < fim;
                 coluna++)
            {
                if (tabuleiro[linhaInicial, coluna] != null)
                    return false;
            }

            int direcao = grande ? -1 : 1;

            Posicao passagem =
                new(
                    linhaInicial,
                    origem.Coluna + direcao);

            if (CasaAtacada(
                    tabuleiro,
                    passagem,
                    rei.Cor))
            {
                return false;
            }

            if (CasaAtacada(
                    tabuleiro,
                    destino,
                    rei.Cor))
            {
                return false;
            }

            return true;
        }

        private bool CasaAtacada(
            Tabuleiro tabuleiro,
            Posicao alvo,
            string corDefensora)
        {
            string corAtacante =
                corDefensora == "Branco"
                    ? "Preto"
                    : "Branco";

            foreach (var item in tabuleiro.ObterPecas())
            {
                Peca peca = item.Peca;

                if (peca.Cor != corAtacante)
                    continue;

                Posicao origem = item.Posicao;

                int deltaLinha =
                    alvo.Linha - origem.Linha;

                int deltaColuna =
                    alvo.Coluna - origem.Coluna;

                int absLinha = Math.Abs(deltaLinha);
                int absColuna = Math.Abs(deltaColuna);

                switch (peca.Tipo)
                {
                    case "Peao":
                        int direcao =
                            peca.Cor == "Branco" ? 1 : -1;

                        if (deltaLinha == direcao &&
                            absColuna == 1)
                        {
                            return true;
                        }

                        break;

                    case "Cavalo":
                        if ((absLinha == 2 && absColuna == 1) ||
                            (absLinha == 1 && absColuna == 2))
                        {
                            return true;
                        }

                        break;

                    case "Rei":
                        if (absLinha <= 1 &&
                            absColuna <= 1 &&
                            (absLinha != 0 ||
                             absColuna != 0))
                        {
                            return true;
                        }

                        break;

                    case "Torre":
                        if ((origem.Linha == alvo.Linha ||
                             origem.Coluna == alvo.Coluna) &&
                            CaminhoLivre(tabuleiro, origem, alvo))
                        {
                            return true;
                        }

                        break;

                    case "Bispo":
                        if (absLinha == absColuna &&
                            CaminhoLivre(tabuleiro, origem, alvo))
                        {
                            return true;
                        }

                        break;

                    case "Rainha":
                        if ((origem.Linha == alvo.Linha ||
                             origem.Coluna == alvo.Coluna ||
                             absLinha == absColuna) &&
                            CaminhoLivre(tabuleiro, origem, alvo))
                        {
                            return true;
                        }

                        break;
                }
            }

            return false;
        }

        private Posicao? LocalizarRei(
            Tabuleiro tabuleiro,
            string cor)
        {
            foreach (var item in tabuleiro.ObterPecas())
            {
                if (item.Peca.Tipo == "Rei" &&
                    item.Peca.Cor == cor)
                {
                    return item.Posicao;
                }
            }

            return null;
        }
    }
}

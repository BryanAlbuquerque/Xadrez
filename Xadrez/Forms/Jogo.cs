using Xadrez.Classes;

namespace Xadrez
{
    public partial class Jogo : Form
    {
        private PictureBox pecaSelecionada;

        // CONTROLE DE TURNO
        private string turnoAtual = "Branco";

        public Jogo()
        {
            InitializeComponent();

            RegistrarEventosPecas();
            RegistrarEventosCasas();

            ConfigurarDadosDasPecas();

            lblTurno.Text = "Turno: Branco";
        }

        // CONFIGURAR DADOS DAS PEÇAS
        private void ConfigurarDadosDasPecas()
        {
            foreach (Control controle in this.Controls)
            {
                if (controle is Panel panel)
                {
                    foreach (Control interno in panel.Controls)
                    {
                        if (interno is PictureBox peca)
                        {
                            string nome = peca.Name;

                            string cor = nome.Contains("Branco")
                                ? "Branco"
                                : "Preto";

                            string tipo = "";

                            if (nome.Contains("Peao"))
                                tipo = "Peao";

                            else if (nome.Contains("Torre"))
                                tipo = "Torre";

                            else if (nome.Contains("Cavalo"))
                                tipo = "Cavalo";

                            else if (nome.Contains("Bispo"))
                                tipo = "Bispo";

                            else if (nome.Contains("Rainha"))
                                tipo = "Rainha";

                            else if (nome.Contains("Rei"))
                                tipo = "Rei";

                            peca.Tag = new DadosPeca()
                            {
                                Tipo = tipo,
                                Cor = cor,
                                JaMoveu = false
                            };
                        }
                    }
                }
            }
        }

        // REGISTRAR EVENTOS DAS PEÇAS
        private void RegistrarEventosPecas()
        {
            foreach (Control controle in this.Controls)
            {
                if (controle is Panel panel)
                {
                    foreach (Control interno in panel.Controls)
                    {
                        if (interno is PictureBox peca)
                        {
                            peca.Click += SelecionarPeca;
                        }
                    }
                }
            }
        }

        // REGISTRAR EVENTOS DAS CASAS
        private void RegistrarEventosCasas()
        {
            foreach (Control controle in this.Controls)
            {
                if (controle is Panel panel)
                {
                    panel.Click += MoverPeca;
                }
            }
        }

        // SELECIONAR PEÇA
        private void SelecionarPeca(object sender, EventArgs e)
        {
            PictureBox peca = (PictureBox)sender;

            DadosPeca dados = (DadosPeca)peca.Tag;

            if (dados.Cor != turnoAtual)
            {
                MessageBox.Show("Não é o turno dessa peça.");
                return;
            }

            pecaSelecionada = peca;
        }

        // MOVER PEÇA
        private void MoverPeca(object sender, EventArgs e)
        {
            if (pecaSelecionada == null)
                return;

            Panel destino = (Panel)sender;

            Panel origem = (Panel)pecaSelecionada.Parent;

            if (origem == destino)
                return;

            DadosPeca dados = (DadosPeca)pecaSelecionada.Tag;

            bool movimentoValido = false;

            switch (dados.Tipo)
            {
                case "Peao":
                    movimentoValido =
                        MovimentoPeaoValido(pecaSelecionada, destino);
                    break;

                case "Torre":
                    movimentoValido =
                        MovimentoTorreValido(origem, destino);
                    break;

                case "Bispo":
                    movimentoValido =
                        MovimentoBispoValido(origem, destino);
                    break;

                case "Cavalo":
                    movimentoValido =
                        MovimentoCavaloValido(origem, destino);
                    break;

                case "Rainha":
                    movimentoValido =
                        MovimentoRainhaValido(origem, destino);
                    break;

                case "Rei":
                    movimentoValido =
                        MovimentoReiValido(origem, destino);
                    break;
            }

            if (!movimentoValido)
            {
                MessageBox.Show("Movimento inválido.");
                return;
            }

            PictureBox pecaDestino =
                ObterPecaNaCasa(destino);

            // NÃO PODE CAPTURAR A PRÓPRIA PEÇA
            if (pecaDestino != null)
            {
                DadosPeca dadosDestino =
                    (DadosPeca)pecaDestino.Tag;

                if (dadosDestino.Cor == dados.Cor)
                {
                    MessageBox.Show(
                        "Você não pode capturar sua própria peça.");
                    return;
                }

                destino.Controls.Remove(pecaDestino);

                pecaDestino.Dispose();
            }

            ColocarPeca(pecaSelecionada, destino);

            dados.JaMoveu = true;

            AlternarTurno();

            pecaSelecionada = null;
        }

        // PEÃO
        private bool MovimentoPeaoValido(
            PictureBox peca,
            Panel destino)
        {
            DadosPeca dados = (DadosPeca)peca.Tag;

            Panel origem = (Panel)peca.Parent;

            int linhaOrigem = ObterLinha(origem);
            int linhaDestino = ObterLinha(destino);

            int diferencaLinha =
                linhaDestino - linhaOrigem;

            char colunaOrigem = ObterColuna(origem);
            char colunaDestino = ObterColuna(destino);

            int diferencaColuna =
                Math.Abs(colunaDestino - colunaOrigem);

            PictureBox pecaDestino =
                ObterPecaNaCasa(destino);

            // BRANCO
            if (dados.Cor == "Branco")
            {
                // FRENTE
                if (colunaOrigem == colunaDestino)
                {
                    // 1 CASA
                    if (diferencaLinha == 1 &&
                        pecaDestino == null)
                    {
                        return true;
                    }

                    // 2 CASAS
                    if (!dados.JaMoveu &&
                        diferencaLinha == 2 &&
                        pecaDestino == null)
                    {
                        return true;
                    }
                }

                // CAPTURA
                if (diferencaColuna == 1 &&
                    diferencaLinha == 1 &&
                    pecaDestino != null)
                {
                    return true;
                }
            }

            // PRETO
            else
            {
                if (colunaOrigem == colunaDestino)
                {
                    if (diferencaLinha == -1 &&
                        pecaDestino == null)
                    {
                        return true;
                    }

                    if (!dados.JaMoveu &&
                        diferencaLinha == -2 &&
                        pecaDestino == null)
                    {
                        return true;
                    }
                }

                if (diferencaColuna == 1 &&
                    diferencaLinha == -1 &&
                    pecaDestino != null)
                {
                    return true;
                }
            }

            return false;
        }

        // TORRE
        private bool MovimentoTorreValido(
            Panel origem,
            Panel destino)
        {
            bool linha =
                ObterLinha(origem) == ObterLinha(destino);

            bool coluna =
                ObterColuna(origem) == ObterColuna(destino);

            if (!linha && !coluna)
                return false;

            return CaminhoLivre(origem, destino);
        }

        // BISPO
        private bool MovimentoBispoValido(
            Panel origem,
            Panel destino)
        {
            int linha =
                Math.Abs(
                    ObterLinha(origem) -
                    ObterLinha(destino));

            int coluna =
                Math.Abs(
                    ObterColuna(origem) -
                    ObterColuna(destino));

            if (linha != coluna)
                return false;

            return CaminhoLivre(origem, destino);
        }

        // CAVALO
        private bool MovimentoCavaloValido(
            Panel origem,
            Panel destino)
        {
            int linha =
                Math.Abs(
                    ObterLinha(origem) -
                    ObterLinha(destino));

            int coluna =
                Math.Abs(
                    ObterColuna(origem) -
                    ObterColuna(destino));

            return (linha == 2 && coluna == 1)
                || (linha == 1 && coluna == 2);
        }

        // RAINHA
        private bool MovimentoRainhaValido(
            Panel origem,
            Panel destino)
        {
            return MovimentoTorreValido(origem, destino)
                || MovimentoBispoValido(origem, destino);
        }

        // REI
        private bool MovimentoReiValido(
            Panel origem,
            Panel destino)
        {
            int linha =
                Math.Abs(
                    ObterLinha(origem) -
                    ObterLinha(destino));

            int coluna =
                Math.Abs(
                    ObterColuna(origem) -
                    ObterColuna(destino));

            return linha <= 1 && coluna <= 1;
        }

        // CAMINHO LIVRE
        private bool CaminhoLivre(
            Panel origem,
            Panel destino)
        {
            int linhaOrigem = ObterLinha(origem);
            int colunaOrigem = ObterIndiceColuna(origem);

            int linhaDestino = ObterLinha(destino);
            int colunaDestino = ObterIndiceColuna(destino);

            int direcaoLinha =
                Math.Sign(linhaDestino - linhaOrigem);

            int direcaoColuna =
                Math.Sign(colunaDestino - colunaOrigem);

            int linhaAtual =
                linhaOrigem + direcaoLinha;

            int colunaAtual =
                colunaOrigem + direcaoColuna;

            while (
                linhaAtual != linhaDestino ||
                colunaAtual != colunaDestino)
            {
                Panel casa =
                    ObterCasa(linhaAtual, colunaAtual);

                if (ObterPecaNaCasa(casa) != null)
                {
                    return false;
                }

                linhaAtual += direcaoLinha;
                colunaAtual += direcaoColuna;
            }

            return true;
        }

        // OBTER CASA
        private Panel ObterCasa(int linha, int coluna)
        {
            string nome =
                $"{(char)('A' + coluna)}{linha}";

            foreach (Control controle in this.Controls)
            {
                if (controle is Panel panel &&
                    panel.Name == nome)
                {
                    return panel;
                }
            }

            return null;
        }

        // PEÇA NA CASA
        private PictureBox ObterPecaNaCasa(Panel casa)
        {
            foreach (Control controle in casa.Controls)
            {
                if (controle is PictureBox peca)
                {
                    return peca;
                }
            }

            return null;
        }

        // LINHA/COLUNA
        private int ObterLinha(Panel casa)
        {
            return int.Parse(
                casa.Name[1].ToString());
        }

        private char ObterColuna(Panel casa)
        {
            return casa.Name[0];
        }

        private int ObterIndiceColuna(Panel casa)
        {
            return casa.Name[0] - 'A';
        }

        // ALTERNAR TURNO
        private void AlternarTurno()
        {
            turnoAtual =
                turnoAtual == "Branco"
                ? "Preto"
                : "Branco";

            lblTurno.Text =
                $"Turno: {turnoAtual}";
        }

        // COLOCAR PEÇA
        private void ColocarPeca(
            PictureBox peca,
            Panel casa)
        {
            peca.Parent = casa;

            peca.Location = new Point(0, 0);

            peca.Dock = DockStyle.Fill;

            peca.SizeMode =
                PictureBoxSizeMode.StretchImage;

            peca.BringToFront();

            peca.Visible = true;
        }

        // POSICIONAR PEÇAS
        private void PosicionarBrancasEmCima()
        {
            // BRANCAS EM CIMA
            ColocarPeca(TorreBranca01, A8);
            ColocarPeca(CavaloBranco01, B8);
            ColocarPeca(BispoBranco01, C8);
            ColocarPeca(RainhaBranca, D8);
            ColocarPeca(ReiBranco, E8);
            ColocarPeca(BispoBranco02, F8);
            ColocarPeca(CavaloBranco02, G8);
            ColocarPeca(TorreBranca02, H8);

            ColocarPeca(PeaoBranco01, A7);
            ColocarPeca(PeaoBranco02, B7);
            ColocarPeca(PeaoBranco03, C7);
            ColocarPeca(PeaoBranco04, D7);
            ColocarPeca(PeaoBranco05, E7);
            ColocarPeca(PeaoBranco06, F7);
            ColocarPeca(PeaoBranco07, G7);
            ColocarPeca(PeaoBranco08, H7);

            // PRETAS EMBAIXO
            ColocarPeca(TorrePreta01, A1);
            ColocarPeca(CavaloPreto01, B1);
            ColocarPeca(BispoPreto01, C1);
            ColocarPeca(RainhaPreta, D1);
            ColocarPeca(ReiPreto, E1);
            ColocarPeca(BispoPreto02, F1);
            ColocarPeca(CavaloPreto02, G1);
            ColocarPeca(TorrePreta02, H1);

            ColocarPeca(PeaoPreto01, A2);
            ColocarPeca(PeaoPreto02, B2);
            ColocarPeca(PeaoPreto03, C2);
            ColocarPeca(PeaoPreto04, D2);
            ColocarPeca(PeaoPreto05, E2);
            ColocarPeca(PeaoPreto06, F2);
            ColocarPeca(PeaoPreto07, G2);
            ColocarPeca(PeaoPreto08, H2);
        }

        // =========================================================
        // POSICIONAR PRETAS EM CIMA
        // =========================================================

        private void PosicionarPretasEmCima()
        {
            // PRETAS EM CIMA
            ColocarPeca(TorrePreta01, A8);
            ColocarPeca(CavaloPreto01, B8);
            ColocarPeca(BispoPreto01, C8);
            ColocarPeca(RainhaPreta, D8);
            ColocarPeca(ReiPreto, E8);
            ColocarPeca(BispoPreto02, F8);
            ColocarPeca(CavaloPreto02, G8);
            ColocarPeca(TorrePreta02, H8);

            ColocarPeca(PeaoPreto01, A7);
            ColocarPeca(PeaoPreto02, B7);
            ColocarPeca(PeaoPreto03, C7);
            ColocarPeca(PeaoPreto04, D7);
            ColocarPeca(PeaoPreto05, E7);
            ColocarPeca(PeaoPreto06, F7);
            ColocarPeca(PeaoPreto07, G7);
            ColocarPeca(PeaoPreto08, H7);

            // BRANCAS EMBAIXO
            ColocarPeca(TorreBranca01, A1);
            ColocarPeca(CavaloBranco01, B1);
            ColocarPeca(BispoBranco01, C1);
            ColocarPeca(RainhaBranca, D1);
            ColocarPeca(ReiBranco, E1);
            ColocarPeca(BispoBranco02, F1);
            ColocarPeca(CavaloBranco02, G1);
            ColocarPeca(TorreBranca02, H1);

            ColocarPeca(PeaoBranco01, A2);
            ColocarPeca(PeaoBranco02, B2);
            ColocarPeca(PeaoBranco03, C2);
            ColocarPeca(PeaoBranco04, D2);
            ColocarPeca(PeaoBranco05, E2);
            ColocarPeca(PeaoBranco06, F2);
            ColocarPeca(PeaoBranco07, G2);
            ColocarPeca(PeaoBranco08, H2);
        }

        // INICIAR JOGO
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (checkBrancasP1.Checked)
            {
                PosicionarBrancasEmCima();
            }
            else if (checkPretasP1.Checked)
            {
                PosicionarPretasEmCima();
            }
        }
    }
}
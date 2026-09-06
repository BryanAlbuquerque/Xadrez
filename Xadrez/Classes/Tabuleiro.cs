namespace Xadrez.Classes
{
    public class Tabuleiro
    {
        private readonly Peca?[,] casas = new Peca?[8, 8];

        public Movimento? UltimoMovimento { get; set; }

        public Peca? this[int linha, int coluna]
        {
            get => casas[linha, coluna];
            set => casas[linha, coluna] = value;
        }

        public Peca? this[Posicao posicao]
        {
            get => casas[posicao.Linha, posicao.Coluna];
            set => casas[posicao.Linha, posicao.Coluna] = value;
        }

        public void Limpar()
        {
            Array.Clear(casas, 0, casas.Length);
            UltimoMovimento = null;
        }

        public Tabuleiro Clonar()
        {
            Tabuleiro copia = new();

            for (int linha = 0; linha < 8; linha++)
            {
                for (int coluna = 0; coluna < 8; coluna++)
                {
                    copia[linha, coluna] =
                        casas[linha, coluna]?.Clonar();
                }
            }

            if (UltimoMovimento != null)
            {
                copia.UltimoMovimento = new Movimento(
                    UltimoMovimento.Origem,
                    UltimoMovimento.Destino)
                {
                    Roque = UltimoMovimento.Roque,
                    EnPassant = UltimoMovimento.EnPassant
                };
            }

            return copia;
        }

        public IEnumerable<(Posicao Posicao, Peca Peca)> ObterPecas()
        {
            for (int linha = 0; linha < 8; linha++)
            {
                for (int coluna = 0; coluna < 8; coluna++)
                {
                    Peca? peca = casas[linha, coluna];

                    if (peca != null)
                    {
                        yield return (
                            new Posicao(linha, coluna),
                            peca);
                    }
                }
            }
        }
    }
}

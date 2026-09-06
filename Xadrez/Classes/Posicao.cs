namespace Xadrez.Classes
{
    public readonly struct Posicao
    {
        public int Linha { get; }
        public int Coluna { get; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public bool Valida =>
            Linha >= 0 &&
            Linha < 8 &&
            Coluna >= 0 &&
            Coluna < 8;

        public override string ToString()
        {
            if (!Valida)
                return "??";

            return $"{(char)('A' + Coluna)}{Linha + 1}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Posicao outra &&
                   outra.Linha == Linha &&
                   outra.Coluna == Coluna;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Linha, Coluna);
        }

        public static bool operator ==(Posicao a, Posicao b)
        {
            return a.Linha == b.Linha &&
                   a.Coluna == b.Coluna;
        }

        public static bool operator !=(Posicao a, Posicao b)
        {
            return !(a == b);
        }
    }
}

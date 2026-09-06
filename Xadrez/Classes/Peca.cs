namespace Xadrez.Classes
{
    public class Peca
    {
        public string Tipo { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public bool JaMoveu { get; set; }

        public Peca Clonar()
        {
            return new Peca
            {
                Tipo = Tipo,
                Cor = Cor,
                JaMoveu = JaMoveu
            };
        }

        public override string ToString()
        {
            return $"{Cor} {Tipo}";
        }
    }
}

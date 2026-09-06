namespace Xadrez.Classes
{
    public class Movimento
    {
        public Posicao Origem { get; }
        public Posicao Destino { get; }

        public bool Roque { get; set; }
        public bool EnPassant { get; set; }

        public Movimento(Posicao origem, Posicao destino)
        {
            Origem = origem;
            Destino = destino;
        }
    }
}

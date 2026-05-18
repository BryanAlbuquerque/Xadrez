using Xadrez.Classes;

namespace Xadrez
{
    public partial class Jogo : Form
    {
        private PictureBox pecaSelecionada;
        public Jogo()
        {
            InitializeComponent();

            checkBrancasP1.CheckedChanged += AtualizarChecks;
            checkPretasP1.CheckedChanged += AtualizarChecks;
            checkBrancasP2.CheckedChanged += AtualizarChecks;
            checkPretasP2.CheckedChanged += AtualizarChecks;

            RegistrarEventosPecas();
            RegistrarEventosCasas();
        }

        private void SelecionarPeca(object sender, EventArgs e)
        {
            pecaSelecionada = (PictureBox)sender;
        }
        private void RegistrarEventosPecas()
        {
            //Brancas
            ReiBranco.Click += SelecionarPeca;
            RainhaBranca.Click += SelecionarPeca;
            TorreBranca01.Click += SelecionarPeca;
            TorreBranca02.Click += SelecionarPeca;

            CavaloBranco01.Click += SelecionarPeca;
            CavaloBranco02.Click += SelecionarPeca;

            BispoBranco01.Click += SelecionarPeca;
            BispoBranco02.Click += SelecionarPeca;

            PeaoBranco01.Click += SelecionarPeca;
            PeaoBranco02.Click += SelecionarPeca;
            PeaoBranco03.Click += SelecionarPeca;
            PeaoBranco04.Click += SelecionarPeca;
            PeaoBranco05.Click += SelecionarPeca;
            PeaoBranco06.Click += SelecionarPeca;
            PeaoBranco07.Click += SelecionarPeca;
            PeaoBranco08.Click += SelecionarPeca;

            //Pretas
            ReiPreto.Click += SelecionarPeca;
            RainhaPreta.Click += SelecionarPeca;
            TorrePreta01.Click += SelecionarPeca;
            TorrePreta02.Click += SelecionarPeca;

            CavaloPreto01.Click += SelecionarPeca;
            CavaloPreto02.Click += SelecionarPeca;

            BispoPreto01.Click += SelecionarPeca;
            BispoPreto02.Click += SelecionarPeca;

            PeaoPreto01.Click += SelecionarPeca;
            PeaoPreto02.Click += SelecionarPeca;
            PeaoPreto03.Click += SelecionarPeca;
            PeaoPreto04.Click += SelecionarPeca;
            PeaoPreto05.Click += SelecionarPeca;
            PeaoPreto06.Click += SelecionarPeca;
            PeaoPreto07.Click += SelecionarPeca;
            PeaoPreto08.Click += SelecionarPeca;
        }

        private void MoverPeca(object sender, EventArgs e)
        {
            if (pecaSelecionada == null)
                return;

            Panel casaDestino = (Panel)sender;

            ColocarPeca(pecaSelecionada, casaDestino);

            pecaSelecionada = null;
        }
        private void RegistrarEventosCasas()
        {
            A1.Click += MoverPeca;
            A2.Click += MoverPeca;
            A3.Click += MoverPeca;
            A4.Click += MoverPeca;
            A5.Click += MoverPeca;
            A6.Click += MoverPeca;
            A7.Click += MoverPeca;
            A8.Click += MoverPeca;

            B1.Click += MoverPeca;
            B2.Click += MoverPeca;
            B3.Click += MoverPeca;
            B4.Click += MoverPeca;
            B5.Click += MoverPeca;
            B6.Click += MoverPeca;
            B7.Click += MoverPeca;
            B8.Click += MoverPeca;

            C1.Click += MoverPeca;
            C2.Click += MoverPeca;
            C3.Click += MoverPeca;
            C4.Click += MoverPeca;
            C5.Click += MoverPeca;
            C6.Click += MoverPeca;
            C7.Click += MoverPeca;
            C8.Click += MoverPeca;

            D1.Click += MoverPeca;
            D2.Click += MoverPeca;
            D3.Click += MoverPeca;
            D4.Click += MoverPeca;
            D5.Click += MoverPeca;
            D6.Click += MoverPeca;
            D7.Click += MoverPeca;
            D8.Click += MoverPeca;

            E1.Click += MoverPeca;
            E2.Click += MoverPeca;
            E3.Click += MoverPeca;
            E4.Click += MoverPeca;
            E5.Click += MoverPeca;
            E6.Click += MoverPeca;
            E7.Click += MoverPeca;
            E8.Click += MoverPeca;

            F1.Click += MoverPeca;
            F2.Click += MoverPeca;
            F3.Click += MoverPeca;
            F4.Click += MoverPeca;
            F5.Click += MoverPeca;
            F6.Click += MoverPeca;
            F7.Click += MoverPeca;
            F8.Click += MoverPeca;

            G1.Click += MoverPeca;
            G2.Click += MoverPeca;
            G3.Click += MoverPeca;
            G4.Click += MoverPeca;
            G5.Click += MoverPeca;
            G6.Click += MoverPeca;
            G7.Click += MoverPeca;
            G8.Click += MoverPeca;

            H1.Click += MoverPeca;
            H2.Click += MoverPeca;
            H3.Click += MoverPeca;
            H4.Click += MoverPeca;
            H5.Click += MoverPeca;
            H6.Click += MoverPeca;
            H7.Click += MoverPeca;
            H8.Click += MoverPeca;
        }

        private void AtualizarChecks(object sender, EventArgs e)
        {
            // PLAYER 1
            checkBrancasP2.Visible = !checkBrancasP1.Checked;
            checkPretasP2.Visible = !checkPretasP1.Checked;

            // PLAYER 2
            checkBrancasP1.Visible = !checkBrancasP2.Checked;
            checkPretasP1.Visible = !checkPretasP2.Checked;
        }

        // MÉTODO PARA COLOCAR PEÇA EM UMA CASA
        private void ColocarPeca(PictureBox peca, Panel casa)
        {
            peca.Parent = casa;

            peca.Location = new Point(0, 0);

            peca.SizeMode = PictureBoxSizeMode.StretchImage;

            peca.Dock = DockStyle.Fill;

            peca.BringToFront();

            peca.Visible = true;
        }

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

        private void Peca_Click(object sender, EventArgs e)
        {
            PictureBox peca = sender as PictureBox;
            if (peca != null)
            {
                ColocarPeca(peca, peca.Parent as Panel);

            }
        }

        private void btnIniciar_Click_1(object sender, EventArgs e)
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
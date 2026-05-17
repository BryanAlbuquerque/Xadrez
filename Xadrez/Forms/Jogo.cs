using Xadrez.Classes;

namespace Xadrez
{
    public partial class Jogo : Form
    {
        public Jogo()
        {
            InitializeComponent();

            checkBrancasP1.CheckedChanged += AtualizarChecks;
            checkPretasP1.CheckedChanged += AtualizarChecks;
            checkBrancasP2.CheckedChanged += AtualizarChecks;
            checkPretasP2.CheckedChanged += AtualizarChecks;
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

        private void Jogo_Load(object sender, EventArgs e)
        {

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
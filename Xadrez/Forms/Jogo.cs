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
            // Brancas
            checkBrancasP2.Visible = !checkBrancasP1.Checked;
            checkBrancasP1.Visible = !checkBrancasP2.Checked;

            // Pretas
            checkPretasP2.Visible = !checkPretasP1.Checked;
            checkPretasP1.Visible = !checkPretasP2.Checked;
        }

        private void Jogo_Load(object sender, EventArgs e)
        {

        }

        private void PecasBrancas()
        {
            ReiBranco.Visible = true;
            RainhaBranca.Visible = true;
            TorreBranca01.Visible = true;
            TorreBranca02.Visible = true;
            CavaloBranco01.Visible = true;
            CavaloBranco02.Visible = true;
            BispoBranco01.Visible = true;
            BispoBranco02.Visible = true;
            PeaoBranco01.Visible = true;
            PeaoBranco02.Visible = true;
            PeaoBranco03.Visible = true;
            PeaoBranco04.Visible = true;
            PeaoBranco05.Visible = true;
            PeaoBranco06.Visible = true;
            PeaoBranco07.Visible = true;
            PeaoBranco08.Visible = true;
        }

        private void PecasPretas()
        {
            ReiPreto.Visible = true;
            RainhaPreta.Visible = true;
            TorrePreta01.Visible = true;
            TorrePreta02.Visible = true;
            CavaloPreto01.Visible = true;
            CavaloPreto02.Visible = true;
            BispoPreto01.Visible = true;
            BispoPreto02.Visible = true;
            PeaoPreto01.Visible = true;
            PeaoPreto02.Visible = true;
            PeaoPreto03.Visible = true;
            PeaoPreto04.Visible = true;
            PeaoPreto05.Visible = true;
            PeaoPreto06.Visible = true;
            PeaoPreto07.Visible = true;
            PeaoPreto08.Visible = true;
        }

        private void LogicaEscolha()
        {
            if (checkBrancasP1.Checked)
            {
                PecasBrancas();
            }
            else if (checkPretasP1.Checked)
            {
                PecasPretas();
            }
            if (checkBrancasP2.Checked)
            {
                PecasBrancas();
            }
            else if (checkPretasP2.Checked)
            {
                PecasPretas();
            }
        }

        private void PosicaoComecoJogo() 
        { 
            if(checkBrancasP1.Checked)
            {
                ReiBranco.Location = new Point(0, 7 * 60);
                RainhaBranca.Location = new Point(1 * 60, 7 * 60);
                TorreBranca01.Location = new Point(0, 7 * 60);
                TorreBranca02.Location = new Point(7 * 60, 7 * 60);
                CavaloBranco01.Location = new Point(1 * 60, 7 * 60);
                CavaloBranco02.Location = new Point(6 * 60, 7 * 60);
                BispoBranco01.Location = new Point(2 * 60, 7 * 60);
                BispoBranco02.Location = new Point(5 * 60, 7 * 60);
                PeaoBranco01.Location = new Point(0, 6 * 60);
                PeaoBranco02.Location = new Point(1 * 60, 6 * 60);
                PeaoBranco03.Location = new Point(2 * 60, 6 * 60);
                PeaoBranco04.Location = new Point(3 * 60, 6 * 60);
                PeaoBranco05.Location = new Point(4 * 60, 6 * 60);
                PeaoBranco06.Location = new Point(5 * 60, 6 * 60);
                PeaoBranco07.Location = new Point(6 * 60, 6 * 60);
                PeaoBranco08.Location = new Point(7 * 60, 6 * 60);

                PecasBrancas();
            }
            else if (checkBrancasP2.Checked)
            {
                ReiBranco.Location = new Point(0, 0);
                RainhaBranca.Location = new Point(1 * 60, 0);
                TorreBranca01.Location = new Point(0, 0);
                TorreBranca02.Location = new Point(7 * 60, 0);
                CavaloBranco01.Location = new Point(1 * 60, 0);
                CavaloBranco02.Location = new Point(6 * 60, 0);
                BispoBranco01.Location = new Point(2 * 60, 0);
                BispoBranco02.Location = new Point(5 * 60, 0);
                PeaoBranco01.Location = new Point(0, 1 * 60);
                PeaoBranco02.Location = new Point(1 * 60, 1 * 60);
                PeaoBranco03.Location = new Point(2 * 60, 1 * 60);
                PeaoBranco04.Location = new Point(3 * 60, 1 * 60);
                PeaoBranco05.Location = new Point(4 * 60, 1 * 60);
                PeaoBranco06.Location = new Point(5 * 60, 1 * 60);
                PeaoBranco07.Location = new Point(6 * 60, 1 * 60);
                PeaoBranco08.Location = new Point(7 * 60, 1 * 60);

                PecasBrancas();
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            LogicaEscolha();
            PosicaoComecoJogo();

        }
    }
}

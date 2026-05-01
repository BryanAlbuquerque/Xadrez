using Xadrez.Classes;

namespace Xadrez
{
    public partial class Jogo : Form
    {
        public Jogo()
        {
            InitializeComponent();
        }

        private void Jogo_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (checkBrancasP1.Checked && checkBrancasP2.Checked)
            {
                checkBrancasP2.Checked = false;
                MessageBox.Show("Jogador 1 escolheu as peças brancas, Jogador 2 jogará com as peças pretas.");
            }
            else if (checkPretasP1.Checked && checkPretasP2.Checked)
            {
                checkPretasP2.Checked = false;
                MessageBox.Show("Jogador 1 escolheu as peças pretas, Jogador 2 jogará com as peças brancas.");
            }

           
        }
    }
}

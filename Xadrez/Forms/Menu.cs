using System;

namespace Xadrez.Forms
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form Jogo = new Jogo();
            Jogo.Show();
            this.Hide();
        }

        private void btnOpcao_Click(object sender, EventArgs e)
        {

        }
    }
}

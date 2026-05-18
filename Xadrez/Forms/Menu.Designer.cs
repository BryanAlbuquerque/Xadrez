namespace Xadrez.Forms
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            btnFechar = new Button();
            button1 = new Button();
            label1 = new Label();
            btnOpcao = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnFechar
            // 
            btnFechar.BackColor = Color.White;
            btnFechar.Cursor = Cursors.Hand;
            btnFechar.FlatStyle = FlatStyle.Flat;
            btnFechar.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnFechar.Location = new Point(570, 2);
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(129, 46);
            btnFechar.TabIndex = 1;
            btnFechar.Text = "FECHAR";
            btnFechar.UseVisualStyleBackColor = false;
            btnFechar.Click += btnFechar_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Honeydew;
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.Location = new Point(279, 307);
            button1.Name = "button1";
            button1.Size = new Size(129, 46);
            button1.TabIndex = 2;
            button1.Text = "Jogar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Black;
            label1.Font = new Font("Showcard Gothic", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(235, 40);
            label1.Name = "label1";
            label1.Size = new Size(220, 60);
            label1.TabIndex = 3;
            label1.Text = "Xadrez";
            // 
            // btnOpcao
            // 
            btnOpcao.BackColor = Color.Black;
            btnOpcao.Cursor = Cursors.Hand;
            btnOpcao.FlatStyle = FlatStyle.Flat;
            btnOpcao.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnOpcao.ForeColor = SystemColors.Control;
            btnOpcao.Location = new Point(570, 54);
            btnOpcao.Name = "btnOpcao";
            btnOpcao.Size = new Size(129, 46);
            btnOpcao.TabIndex = 4;
            btnOpcao.Text = "Opções";
            btnOpcao.UseVisualStyleBackColor = false;
            btnOpcao.Click += btnOpcao_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(587, 238);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(112, 160);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(1, 238);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(112, 160);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(698, 396);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 399);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(btnOpcao);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(btnFechar);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnFechar;
        private Button button1;
        private Label label1;
        private Button btnOpcao;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
    }
}
using System.Drawing;
using System.Windows.Forms;
using Xadrez.Classes;
using Xadrez.Regras;

namespace Xadrez
{
    public partial class Jogo : Form
    {
        private readonly Tabuleiro tabuleiro;
        private readonly RegrasXadrez regras;

        private readonly Panel[,] casas = new Panel[8, 8];

        private PictureBox? pecaSelecionada;
        private Posicao? posicaoSelecionada;

        private string corAtual = "Branco";

        private int pecasBrancasCapturadas = 0;
        private int pecasPretasCapturadas = 0;

        private bool jogoIniciado;

        private string jogadorBranco = "P1";
        private string jogadorPreto = "P2";

        public Jogo()
        {
            InitializeComponent();

            tabuleiro = new Tabuleiro();
            regras = new RegrasXadrez();

            InicializarCasas();
            ConfigurarEventosCasas();
            ConfigurarPecasVisuais();
            ConfigurarSelecaoDeCores();

            lblTurno.Text = "Aguardando escolha das cores";

            lblPecasBrancasRemovidas.Text =
                "Brancas capturadas: 0";

            lblPecasPretasRemovidas.Text =
                "Pretas capturadas: 0";
        }

        private void InicializarCasas()
        {
            for (int linha = 0; linha < 8; linha++)
            {
                for (int coluna = 0; coluna < 8; coluna++)
                {
                    char letra = (char)('A' + coluna);
                    int numero = linha + 1;

                    string nome = $"{letra}{numero}";

                    Control[] encontrados =
                        Controls.Find(nome, true);

                    if (encontrados.Length == 0 ||
                        encontrados[0] is not Panel painel)
                    {
                        throw new InvalidOperationException(
                            $"A casa '{nome}' não foi encontrada no Designer.");
                    }

                    casas[linha, coluna] = painel;
                }
            }
        }

        private void ConfigurarEventosCasas()
        {
            for (int linha = 0; linha < 8; linha++)
            {
                for (int coluna = 0; coluna < 8; coluna++)
                {
                    Panel casa = casas[linha, coluna];

                    casa.Click -= Casa_Click;
                    casa.Click += Casa_Click;

                    foreach (Control controle in casa.Controls)
                    {
                        if (controle is PictureBox pictureBox)
                        {
                            pictureBox.Click -= Peca_Click;
                            pictureBox.Click += Peca_Click;
                        }
                    }
                }
            }
        }

        private void ConfigurarPecasVisuais()
        {
            foreach (PictureBox pictureBox in ObterTodasAsPecas())
            {
                Peca? peca =
                    CriarPecaAPartirDoNome(
                        pictureBox.Name);

                pictureBox.Tag = peca;

                pictureBox.SizeMode =
                    PictureBoxSizeMode.Zoom;

                pictureBox.Cursor =
                    Cursors.Hand;

                pictureBox.Visible = false;
            }

            PosicionarPecasIniciais();
            ReconstruirTabuleiroLogico();
        }

        private IEnumerable<PictureBox> ObterTodasAsPecas()
        {
            for (int linha = 0; linha < 8; linha++)
            {
                for (int coluna = 0; coluna < 8; coluna++)
                {
                    Panel casa =
                        casas[linha, coluna];

                    foreach (Control controle in casa.Controls)
                    {
                        if (controle is PictureBox pictureBox)
                        {
                            yield return pictureBox;
                        }
                    }
                }
            }
        }

        private Peca? CriarPecaAPartirDoNome(string nome)
        {
            string tipo;
            string cor;

            if (nome.Contains("Peao"))
                tipo = "Peao";
            else if (nome.Contains("Torre"))
                tipo = "Torre";
            else if (nome.Contains("Cavalo"))
                tipo = "Cavalo";
            else if (nome.Contains("Bispo"))
                tipo = "Bispo";
            else if (nome.Contains("Rainha"))
                tipo = "Rainha";
            else if (nome.Contains("Rei"))
                tipo = "Rei";
            else
                return null;

            if (nome.Contains("Branca") ||
                nome.Contains("Branco"))
            {
                cor = "Branco";
            }
            else if (nome.Contains("Preta") ||
                     nome.Contains("Preto"))
            {
                cor = "Preto";
            }
            else
            {
                return null;
            }

            return new Peca
            {
                Tipo = tipo,
                Cor = cor,
                JaMoveu = false
            };
        }

        private void PosicionarPecasIniciais()
        {
            EsconderTodasAsPecas();

            // PRETAS
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

            // BRANCAS
            ColocarPeca(PeaoBranco01, A7);
            ColocarPeca(PeaoBranco02, B7);
            ColocarPeca(PeaoBranco03, C7);
            ColocarPeca(PeaoBranco04, D7);
            ColocarPeca(PeaoBranco05, E7);
            ColocarPeca(PeaoBranco06, F7);
            ColocarPeca(PeaoBranco07, G7);
            ColocarPeca(PeaoBranco08, H7);

            ColocarPeca(TorreBranca01, A8);
            ColocarPeca(CavaloBranco01, B8);
            ColocarPeca(BispoBranco01, C8);
            ColocarPeca(RainhaBranca, D8);
            ColocarPeca(ReiBranco, E8);
            ColocarPeca(BispoBranco02, F8);
            ColocarPeca(CavaloBranco02, G8);
            ColocarPeca(TorreBranca02, H8);
        }

        private void EsconderTodasAsPecas()
        {
            foreach (PictureBox pictureBox in ObterTodasAsPecas())
            {
                pictureBox.Visible = false;
            }
        }

        private void ColocarPeca(PictureBox peca, Panel casa)
        {
            peca.Parent = casa;
            peca.Dock = DockStyle.Fill;
            peca.BringToFront();
            peca.Visible = true;
        }

        private void ReconstruirTabuleiroLogico()
        {
            tabuleiro.Limpar();

            for (int linhaVisual = 0;
                 linhaVisual < 8;
                 linhaVisual++)
            {
                for (int colunaVisual = 0;
                     colunaVisual < 8;
                     colunaVisual++)
                {
                    Panel casa =
                        casas[linhaVisual, colunaVisual];

                    PictureBox? visual =
                        ObterPecaNaCasa(casa);

                    if (visual?.Tag is Peca peca)
                    {
                        Posicao posicaoLogica =
                            ObterPosicao(casa);

                        tabuleiro[posicaoLogica] =
                            peca;
                    }
                }
            }
        }

        private PictureBox? ObterPecaNaCasa(Panel casa)
        {
            foreach (Control controle in casa.Controls)
            {
                if (controle is PictureBox pictureBox &&
                    pictureBox.Visible)
                {
                    return pictureBox;
                }
            }

            return null;
        }

        private void Casa_Click(object? sender, EventArgs e)
        {
            if (!jogoIniciado)
                return;

            if (sender is not Panel casa)
                return;

            MoverPeca(casa);
        }

        private void Peca_Click(object? sender, EventArgs e)
        {
            if (!jogoIniciado)
                return;

            if (sender is not PictureBox pictureBox)
                return;

            SelecionarPeca(pictureBox);
        }

        private void SelecionarPeca( PictureBox pictureBox)
        {
            if (pictureBox.Tag is not Peca peca)
                return;

            if (!pictureBox.Visible)
                return;

            Panel? casa =
                pictureBox.Parent as Panel;

            if (casa == null)
                return;

            // Nenhuma peça selecionada:
            // somente a cor do jogador da vez pode ser selecionada.
            if (pecaSelecionada == null)
            {
                if (peca.Cor != corAtual)
                {
                    MessageBox.Show(
                        $"Não é possível selecionar esta peça.\n\n" +
                        $"É o turno do {ObterJogadorDaCor(corAtual)} " +
                        $"({corAtual}).",
                        "Turno inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                pecaSelecionada =
                    pictureBox;

                posicaoSelecionada =
                    ObterPosicao(casa);

                DestacarCasa(casa);

                return;
            }

            // Clicou novamente na peça selecionada.
            if (pecaSelecionada == pictureBox)
            {
                LimparSelecao();
                return;
            }

            // Clicou em uma peça da própria cor.
            if (peca.Cor == corAtual)
            {
                LimparSelecao();

                pecaSelecionada =
                    pictureBox;

                posicaoSelecionada =
                    ObterPosicao(casa);

                DestacarCasa(casa);

                return;
            }

            // Clicou em peça adversária:
            // tenta realizar captura.
            MoverPeca(casa);
        }

        private void MoverPeca(Panel casaDestino)
        {
            if (pecaSelecionada == null ||
                posicaoSelecionada == null)
            {
                MessageBox.Show(
                    "Selecione uma peça antes de escolher o destino.",
                    "Xadrez",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            Posicao origem =
                posicaoSelecionada.Value;

            Posicao destino =
                ObterPosicao(casaDestino);

            TentarMover(
                origem,
                destino);
        }

        private void TentarMover(Posicao origem, Posicao destino)
        {
            if (!jogoIniciado)
                return;

            Movimento? movimento =
                regras.ObterMovimentoValido(
                    tabuleiro,
                    origem,
                    destino,
                    corAtual);

            if (movimento == null)
            {
                MessageBox.Show(
                    "Movimento inválido.\n\n" +
                    "Verifique se a peça pode realizar esse movimento " +
                    "e se ele não deixa seu próprio Rei em xeque.",
                    "Movimento inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ExecutarMovimento(movimento);
        }

        private void ExecutarMovimento( Movimento movimento)
        {
            Peca? peca =
                tabuleiro[movimento.Origem];

            if (peca == null)
                return;

            Panel casaOrigem =
                ObterCasaVisual(
                    movimento.Origem);

            Panel casaDestino =
                ObterCasaVisual(
                    movimento.Destino);

            PictureBox? visualPeca =
                ObterPecaNaCasa(
                    casaOrigem);

            if (visualPeca == null)
                return;

            // Captura normal
            Peca? pecaDestino =
                tabuleiro[movimento.Destino];

            if (pecaDestino != null &&
                pecaDestino.Cor != peca.Cor)
            {
                PictureBox? visualCapturada =
                    ObterPecaNaCasa(
                        casaDestino);

                if (visualCapturada != null)
                {
                    RegistrarCaptura(
                        visualCapturada);

                    visualCapturada.Visible =
                        false;

                    casaDestino.Controls.Remove(
                        visualCapturada);
                }
            }

            // En passant
            if (movimento.EnPassant)
            {
                int direcao =
                    peca.Cor == "Branco"
                        ? 1
                        : -1;

                Posicao posicaoCapturada =
                    new(
                        movimento.Destino.Linha - direcao,
                        movimento.Destino.Coluna);

                Panel casaCapturada =
                    ObterCasaVisual(
                        posicaoCapturada);

                PictureBox? visualCapturada =
                    ObterPecaNaCasa(
                        casaCapturada);

                if (visualCapturada != null)
                {
                    RegistrarCaptura(
                        visualCapturada);

                    visualCapturada.Visible =
                        false;

                    casaCapturada.Controls.Remove(
                        visualCapturada);
                }
            }

            // Atualiza o modelo lógico.
            regras.AplicarMovimento(
                tabuleiro,
                movimento);

            // Move a peça visualmente.
            visualPeca.Parent =
                casaDestino;

            visualPeca.Dock =
                DockStyle.Fill;

            visualPeca.BringToFront();
            visualPeca.Visible = true;

            // Roque
            if (movimento.Roque)
            {
                int origemTorreColuna =
                    movimento.Destino.Coluna == 2
                        ? 0
                        : 7;

                int destinoTorreColuna =
                    movimento.Destino.Coluna == 2
                        ? 3
                        : 5;

                Posicao origemTorre =
                    new(
                        movimento.Origem.Linha,
                        origemTorreColuna);

                Posicao destinoTorre =
                    new(
                        movimento.Origem.Linha,
                        destinoTorreColuna);

                Panel casaOrigemTorre =
                    ObterCasaVisual(
                        origemTorre);

                Panel casaDestinoTorre =
                    ObterCasaVisual(
                        destinoTorre);

                PictureBox? visualTorre =
                    ObterPecaNaCasa(
                        casaOrigemTorre);

                if (visualTorre != null)
                {
                    visualTorre.Parent =
                        casaDestinoTorre;

                    visualTorre.Dock =
                        DockStyle.Fill;

                    visualTorre.BringToFront();
                    visualTorre.Visible = true;
                }
            }

            LimparSelecao();

            VerificarPromocao(
                movimento.Destino);

            // Troca o turno.
            AlternarTurno();

            // Verifica xeque, xeque-mate ou afogamento.
            VerificarEstadoDaPartida();
        }

        private void RegistrarCaptura( PictureBox peca)
        {
            if (peca.Tag is not Peca dados)
                return;

            if (dados.Cor == "Branco")
            {
                pecasBrancasCapturadas++;

                lblPecasBrancasRemovidas.Text =
                    $"Brancas capturadas: " +
                    $"{pecasBrancasCapturadas}";
            }
            else if (dados.Cor == "Preto")
            {
                pecasPretasCapturadas++;

                lblPecasPretasRemovidas.Text =
                    $"Pretas capturadas: " +
                    $"{pecasPretasCapturadas}";
            }
        }

        private void VerificarPromocao(Posicao posicao)
        {
            Peca? peca =
                tabuleiro[posicao];

            if (peca == null ||
                peca.Tipo != "Peao")
            {
                return;
            }

            bool chegouAoFinal =
                (peca.Cor == "Branco" &&
                 posicao.Linha == 7) ||
                (peca.Cor == "Preto" &&
                 posicao.Linha == 0);

            if (!chegouAoFinal)
                return;

            Panel casa =
                ObterCasaVisual(posicao);

            PictureBox? visual =
                ObterPecaNaCasa(casa);

            if (visual == null)
                return;

            string escolha =
                MostrarEscolhaPromocao();

            if (string.IsNullOrWhiteSpace(escolha))
                escolha = "Rainha";

            peca.Tipo =
                escolha;

            visual.Tag =
                peca;

            PictureBox? novaImagem =
                ObterImagemPromocao(
                    peca.Cor,
                    escolha);

            if (novaImagem != null)
            {
                novaImagem.Parent =
                    casa;

                novaImagem.Dock =
                    DockStyle.Fill;

                novaImagem.BringToFront();

                novaImagem.Tag =
                    peca;

                novaImagem.Visible =
                    true;

                visual.Visible =
                    false;
            }
        }

        private string MostrarEscolhaPromocao()
        {
            using Form formulario =
                new Form();

            formulario.Text =
                "Promoção do Peão";

            formulario.StartPosition =
                FormStartPosition.CenterParent;

            formulario.FormBorderStyle =
                FormBorderStyle.FixedDialog;

            formulario.MinimizeBox =
                false;

            formulario.MaximizeBox =
                false;

            formulario.Width =
                300;

            formulario.Height =
                180;

            Label label =
                new Label
                {
                    Text =
                        "Escolha a peça para promoção:",

                    Dock =
                        DockStyle.Top,

                    Height =
                        50,

                    TextAlign =
                        ContentAlignment.MiddleCenter
                };

            ComboBox comboBox =
                new ComboBox
                {
                    Dock =
                        DockStyle.Top,

                    DropDownStyle =
                        ComboBoxStyle.DropDownList
                };

            comboBox.Items.Add(
                "Rainha");

            comboBox.Items.Add(
                "Torre");

            comboBox.Items.Add(
                "Bispo");

            comboBox.Items.Add(
                "Cavalo");

            comboBox.SelectedIndex =
                0;

            Button confirmar =
                new Button
                {
                    Text =
                        "Confirmar",

                    Dock =
                        DockStyle.Bottom,

                    Height =
                        40,

                    DialogResult =
                        DialogResult.OK
                };

            formulario.Controls.Add(
                comboBox);

            formulario.Controls.Add(
                label);

            formulario.Controls.Add(
                confirmar);

            formulario.AcceptButton =
                confirmar;

            if (formulario.ShowDialog(this) ==
                DialogResult.OK)
            {
                return comboBox.SelectedItem?
                           .ToString()
                       ?? "Rainha";
            }

            return "Rainha";
        }

        private PictureBox? ObterImagemPromocao(string cor, string tipo)
        {
            string prefixo =
                cor == "Branco"
                    ? "Branca"
                    : "Preta";

            foreach (PictureBox pictureBox
                     in ObterTodasAsPecas())
            {
                if (!pictureBox.Name.Contains(tipo))
                    continue;

                if (!pictureBox.Name.Contains(prefixo))
                    continue;

                return pictureBox;
            }

            return null;
        }

        private void AlternarTurno()
        {
            corAtual =
                corAtual == "Branco"
                    ? "Preto"
                    : "Branco";

            string jogador =
                ObterJogadorDaCor(
                    corAtual);

            lblTurno.Text =
                $"Turno: {jogador} - {corAtual}";
        }

        private void VerificarEstadoDaPartida()
        {
            string corJogadorAtual = corAtual;
            string corJogadorOponente =
                corAtual == "Branco" ? "Preto" : "Branco";

            string jogadorAtual = ObterJogadorDaCor(corJogadorAtual);
            string jogadorVencedor = ObterJogadorDaCor(corJogadorOponente);

            // Verificação de segurança:
            // em uma partida normal o Rei nunca é realmente capturado,
            // mas se por algum motivo ele desaparecer do tabuleiro,
            // encerramos a partida.
            bool reiAtualExiste = tabuleiro.ObterPecas()
                .Any(x => x.Peca.Tipo == "Rei" &&
                          x.Peca.Cor == corJogadorAtual);

            bool reiOponenteExiste = tabuleiro.ObterPecas()
                .Any(x => x.Peca.Tipo == "Rei" &&
                          x.Peca.Cor == corJogadorOponente);

            if (!reiAtualExiste)
            {
                jogoIniciado = false;
                pecaSelecionada = null;

                MessageBox.Show(
                    $"CHECKMATE!\n\n" +
                    $"O Jogador {jogadorVencedor} venceu!",
                    "Fim de jogo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (!reiOponenteExiste)
            {
                jogoIniciado = false;
                pecaSelecionada = null;

                MessageBox.Show(
                    $"CHECKMATE!\n\n" +
                    $"O Jogador {jogadorVencedor} venceu!",
                    "Fim de jogo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // O jogador atual está em xeque.
            bool estaEmXeque = regras.EstaEmXeque(
                tabuleiro,
                corJogadorAtual);

            if (estaEmXeque)
            {
                // Se está em xeque e não possui nenhum movimento legal,
                // então é XEQUE-MATE.
                bool xequeMate = regras.XequeMate(
                    tabuleiro,
                    corJogadorAtual);

                if (xequeMate)
                {
                    jogoIniciado = false;
                    pecaSelecionada = null;

                    MessageBox.Show(
                        $"CHECKMATE!\n\n" +
                        $"O Rei do Jogador {jogadorAtual} foi colocado em xeque-mate.\n\n" +
                        $"O Jogador {jogadorVencedor} venceu!",
                        "Fim de jogo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // Está em xeque, mas ainda pode escapar.
                MessageBox.Show(
                    $"CHECK!\n\n" +
                    $"O Rei do Jogador {jogadorAtual} está em xeque.\n\n" +
                    $"O Jogador {jogadorAtual} precisa realizar uma jogada que tire o Rei do xeque.",
                    "Xeque",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Se não está em xeque, verificamos afogamento.
            if (regras.Afogamento(tabuleiro, corJogadorAtual))
            {
                jogoIniciado = false;
                pecaSelecionada = null;

                MessageBox.Show(
                    "EMPATE!\n\n" +
                    $"O Jogador {jogadorAtual} não possui movimentos legais, " +
                    "mas o Rei não está em xeque.",
                    "Afogamento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }
        }

        private string ObterJogadorDaCor(string cor)
        {
            return cor == "Branco"
                ? jogadorBranco
                : jogadorPreto;
        }

        private void LimparSelecao()
        {
            if (pecaSelecionada?.Parent is Panel casa)
            {
                RemoverDestaque(casa);
            }

            pecaSelecionada =
                null;

            posicaoSelecionada =
                null;
        }

        private void DestacarCasa(Panel casa)
        {
            casa.BorderStyle =
                BorderStyle.Fixed3D;
        }

        private void RemoverDestaque(Panel casa)
        {
            casa.BorderStyle =
                BorderStyle.None;
        }

        private Posicao ObterPosicao(Panel casa)
        {
            string nome =
                casa.Name.ToUpperInvariant();

            if (nome.Length < 2)
                throw new InvalidOperationException(
                    $"Nome de casa inválido: {casa.Name}");

            char letra =
                nome[0];

            if (letra < 'A' ||
                letra > 'H')
            {
                throw new InvalidOperationException(
                    $"Coluna inválida: {casa.Name}");
            }

            if (!int.TryParse(
                    nome.Substring(1),
                    out int numero))
            {
                throw new InvalidOperationException(
                    $"Linha inválida: {casa.Name}");
            }

            if (numero < 1 ||
                numero > 8)
            {
                throw new InvalidOperationException(
                    $"Linha fora do tabuleiro: {casa.Name}");
            }

            return new Posicao(
                8 - numero,
                letra - 'A');
        }

        private Panel ObterCasaVisual(Posicao posicao)
        {
            if (!posicao.Valida)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(posicao));
            }

            int linhaVisual =
                7 - posicao.Linha;

            return casas[
                linhaVisual,
                posicao.Coluna];
        }

        private void btnIniciar_Click_1(object? sender, EventArgs e)
        {
            IniciarPartida();
        }

        private void IniciarPartida()
        {
            if (!ValidarSelecaoDeCores())
                return;

            jogoIniciado =
                false;

            LimparSelecao();

            pecasBrancasCapturadas =
                0;

            pecasPretasCapturadas =
                0;

            lblPecasBrancasRemovidas.Text =
                "Brancas capturadas: 0";

            lblPecasPretasRemovidas.Text =
                "Pretas capturadas: 0";

            PosicionarPecasIniciais();

            ReconstruirTabuleiroLogico();

            corAtual =
                "Branco";

            jogoIniciado =
                true;

            lblTurno.Text =
                $"Turno: {jogadorBranco} - Branco";

            MessageBox.Show(
                $"Partida iniciada!\n\n" +
                $"{jogadorBranco} joga com as peças Brancas.\n" +
                $"{jogadorPreto} joga com as peças Pretas.\n\n" +
                $"As Brancas começam.",
                "Xadrez",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool ValidarSelecaoDeCores()
        {
            bool brancoP1 =
                checkBrancasP1.Checked;

            bool pretoP1 =
                checkPretasP1.Checked;

            bool brancoP2 =
                checkBrancasP2.Checked;

            bool pretoP2 =
                checkPretasP2.Checked;

            int totalBrancas =
                (brancoP1 ? 1 : 0) +
                (brancoP2 ? 1 : 0);

            int totalPretas =
                (pretoP1 ? 1 : 0) +
                (pretoP2 ? 1 : 0);

            if (totalBrancas != 1 ||
                totalPretas != 1)
            {
                MessageBox.Show(
                    "Escolha exatamente um jogador para as peças Brancas " +
                    "e um jogador para as peças Pretas.",
                    "Escolha das cores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            jogadorBranco =
                brancoP1
                    ? "P1"
                    : "P2";

            jogadorPreto =
                pretoP1
                    ? "P1"
                    : "P2";

            if (jogadorBranco ==
                jogadorPreto)
            {
                MessageBox.Show(
                    "Um jogador não pode ficar com as duas cores.",
                    "Escolha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void ConfigurarSelecaoDeCores()
        {
            checkBrancasP1.CheckedChanged -=
                checkBrancasP1_CheckedChanged;

            checkPretasP1.CheckedChanged -=
                checkPretasP1_CheckedChanged;

            checkBrancasP2.CheckedChanged -=
                checkBrancasP2_CheckedChanged;

            checkPretasP2.CheckedChanged -=
                checkPretasP2_CheckedChanged;

            checkBrancasP1.CheckedChanged +=
                checkBrancasP1_CheckedChanged;

            checkPretasP1.CheckedChanged +=
                checkPretasP1_CheckedChanged;

            checkBrancasP2.CheckedChanged +=
                checkBrancasP2_CheckedChanged;

            checkPretasP2.CheckedChanged +=
                checkPretasP2_CheckedChanged;

            AtualizarSelecaoDeCores();
        }

        private void AtualizarSelecaoDeCores()
        {
            // P1 escolheu Brancas.
            if (checkBrancasP1.Checked)
            {
                checkPretasP1.Checked =
                    false;

                checkBrancasP2.Checked =
                    false;

                checkPretasP1.Enabled =
                    false;

                checkBrancasP2.Enabled =
                    false;

                checkPretasP2.Enabled =
                    true;
            }
            // P1 escolheu Pretas.
            else if (checkPretasP1.Checked)
            {
                checkBrancasP1.Checked =
                    false;

                checkPretasP2.Checked =
                    false;

                checkBrancasP1.Enabled =
                    false;

                checkPretasP2.Enabled =
                    false;

                checkBrancasP2.Enabled =
                    true;
            }
            // P2 escolheu Brancas.
            else if (checkBrancasP2.Checked)
            {
                checkPretasP2.Checked =
                    false;

                checkBrancasP1.Checked =
                    false;

                checkBrancasP2.Enabled =
                    true;

                checkPretasP1.Enabled =
                    true;

                checkPretasP2.Enabled =
                    false;
            }
            // P2 escolheu Pretas.
            else if (checkPretasP2.Checked)
            {
                checkBrancasP2.Checked =
                    false;

                checkPretasP1.Checked =
                    false;

                checkPretasP2.Enabled =
                    true;

                checkBrancasP1.Enabled =
                    true;

                checkBrancasP2.Enabled =
                    false;
            }
            else
            {
                checkBrancasP1.Enabled =
                    true;

                checkPretasP1.Enabled =
                    true;

                checkBrancasP2.Enabled =
                    true;

                checkPretasP2.Enabled =
                    true;
            }
        }

        private void checkBrancasP1_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBrancasP1.Checked)
            {
                checkPretasP1.Checked =
                    false;

                checkBrancasP2.Checked =
                    false;
            }

            AtualizarSelecaoDeCores();
        }

        private void checkPretasP1_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkPretasP1.Checked)
            {
                checkBrancasP1.Checked =
                    false;

                checkPretasP2.Checked =
                    false;
            }

            AtualizarSelecaoDeCores();
        }

        private void checkBrancasP2_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBrancasP2.Checked)
            {
                checkPretasP2.Checked =
                    false;

                checkBrancasP1.Checked =
                    false;
            }

            AtualizarSelecaoDeCores();
        }

        private void checkPretasP2_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkPretasP2.Checked)
            {
                checkBrancasP2.Checked =
                    false;

                checkPretasP1.Checked =
                    false;
            }

            AtualizarSelecaoDeCores();
        }
    }
}

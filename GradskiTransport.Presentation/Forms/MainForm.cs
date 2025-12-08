using GradskiTransport.Presentation.Interfaces;
using GradskiTransport.Presentation.Presenters;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly IUserPresenter _userPresenter;
        private readonly ILinijaPresenter _linijaPresenter;
        private Korisnik? _currentUser;

        public MainForm()
        {
            InitializeComponent();
            _userPresenter = new UserPresenter();
            _linijaPresenter = new LinijaPresenter();
            
            // Početno stanje - prikaz login forme
            ShowLoginForm();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // MainForm
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Name = "MainForm";
            this.Text = "Gradski Transport - Sistem upravljanja";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            
            this.ResumeLayout(false);
        }

        private void ShowLoginForm()
        {
            this.Controls.Clear();
            
            var loginForm = new LoginForm();
            loginForm.Dock = DockStyle.Fill;
            loginForm.LoginSuccessful += OnLoginSuccessful;
            
            this.Controls.Add(loginForm);
        }

        private void OnLoginSuccessful(Korisnik user)
        {
            _currentUser = user;
            ShowMainInterface();
        }

        private void ShowMainInterface()
        {
            this.Controls.Clear();
            
            // Kreiranje glavnog menija
            var menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("Fajl");
            var linijeMenu = new ToolStripMenuItem("Linije");
            var karteMenu = new ToolStripMenuItem("Karte");
            var statistikaMenu = new ToolStripMenuItem("Statistika");
            var helpMenu = new ToolStripMenuItem("Pomoć");

            // Dodavanje stavki u File meni
            fileMenu.DropDownItems.Add("Odjava", null, (s, e) => Logout());
            fileMenu.DropDownItems.Add("-");
            fileMenu.DropDownItems.Add("Izlaz", null, (s, e) => Close());

            // Dodavanje stavki u Linije meni
            linijeMenu.DropDownItems.Add("Pregled linija", null, (s, e) => ShowLinijeForm());
            linijeMenu.DropDownItems.Add("Raspored polazaka", null, (s, e) => ShowRasporedForm());

            // Dodavanje stavki u Karte meni
            karteMenu.DropDownItems.Add("Upravljanje kartama", null, (s, e) => ShowKarteForm());

            // Dodavanje stavki u Statistika meni
            statistikaMenu.DropDownItems.Add("Pregled statistike", null, (s, e) => ShowStatistikaForm());

            // Dodavanje stavki u Help meni
            helpMenu.DropDownItems.Add("O aplikaciji", null, (s, e) => ShowAboutForm());

            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, linijeMenu, karteMenu, statistikaMenu, helpMenu });
            
            // Status bar
            var statusStrip = new StatusStrip();
            var userLabel = new ToolStripStatusLabel($"Korisnik: {_currentUser?.ImePrezime} ({_currentUser?.Uloga})");
            var timeLabel = new ToolStripStatusLabel();
            timeLabel.Spring = true;
            timeLabel.TextAlign = ContentAlignment.MiddleRight;
            
            statusStrip.Items.AddRange(new ToolStripItem[] { userLabel, timeLabel });
            
            // Timer za ažuriranje vremena
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) => timeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            timer.Start();

            // Glavni panel
            var mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.WhiteSmoke;

            // Dobrodošlica label
            var welcomeLabel = new Label();
            welcomeLabel.Text = $"Dobrodošli u Gradski Transport sistem!\n\nKorisnik: {_currentUser?.ImePrezime}\nUloga: {_currentUser?.Uloga}\n\nIzaberite opciju iz menija za rad sa aplikacijom.";
            welcomeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            welcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            welcomeLabel.Dock = DockStyle.Fill;
            welcomeLabel.AutoSize = false;

            mainPanel.Controls.Add(welcomeLabel);

            // Dodavanje kontrola u formu
            this.Controls.Add(mainPanel);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            
            this.MainMenuStrip = menuStrip;
        }

        private void ShowLinijeForm()
        {
            var linijeForm = new LinijaForm();
            linijeForm.ShowDialog();
        }

        private void ShowRasporedForm()
        {
            var rasporedForm = new RasporedForm();
            rasporedForm.ShowDialog();
        }

        private void ShowKarteForm()
        {
            var karteForm = new KartaForm();
            karteForm.ShowDialog();
        }

        private void ShowStatistikaForm()
        {
            var statistikaForm = new StatistikaForm();
            statistikaForm.ShowDialog();
        }

        private void ShowAboutForm()
        {
            MessageBox.Show(
                "Gradski Transport v1.0\n\n" +
                "Sistem za upravljanje gradskim transportom\n" +
                "© 2025 - Sva prava zadržana",
                "O aplikaciji",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void Logout()
        {
            _currentUser = null;
            ShowLoginForm();
        }
    }
}
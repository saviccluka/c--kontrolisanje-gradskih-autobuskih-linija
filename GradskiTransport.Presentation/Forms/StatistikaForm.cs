using GradskiTransport.Presentation.Presenters;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Forms
{
    public partial class StatistikaForm : Form
    {
        private readonly KartaPresenter _kartaPresenter;
        private DataGridView statistikaPoTipuDataGridView;
        private DataGridView statistikaPoStatusuDataGridView;
        private Label ukupnaZaradaLabel;
        private Label brojKarataLabel;
        private Label brojPutnikaLabel;
        private Button refreshButton;

        public StatistikaForm()
        {
            _kartaPresenter = new KartaPresenter();
            InitializeComponent();
            LoadStatistikeAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // StatistikaForm
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Name = "StatistikaForm";
            this.Text = "Statistika sistema - Gradski Transport";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Header panel
            var headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(25, 118, 210);
            headerPanel.Padding = new Padding(20, 10, 20, 10);

            var titleLabel = new Label();
            titleLabel.Text = "Statistika sistema";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);

            refreshButton = new Button();
            refreshButton.Text = "Osveži";
            refreshButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            refreshButton.BackColor = Color.White;
            refreshButton.ForeColor = Color.FromArgb(25, 118, 210);
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Location = new Point(1000, 10);
            refreshButton.Size = new Size(80, 35);
            refreshButton.Click += RefreshButton_Click;

            headerPanel.Controls.AddRange(new Control[] { titleLabel, refreshButton });
            this.Controls.Add(headerPanel);

            // Main content panel
            var mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // Overview panel
            var overviewPanel = new Panel();
            overviewPanel.Dock = DockStyle.Top;
            overviewPanel.Height = 100;
            overviewPanel.BackColor = Color.White;
            overviewPanel.Padding = new Padding(20);
            overviewPanel.BorderStyle = BorderStyle.FixedSingle;

            var overviewTitle = new Label();
            overviewTitle.Text = "Pregled sistema";
            overviewTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            overviewTitle.ForeColor = Color.FromArgb(25, 118, 210);
            overviewTitle.Location = new Point(20, 10);
            overviewTitle.AutoSize = true;

            brojKarataLabel = new Label();
            brojKarataLabel.Font = new Font("Segoe UI", 10F);
            brojKarataLabel.ForeColor = Color.FromArgb(64, 64, 64);
            brojKarataLabel.Location = new Point(20, 35);
            brojKarataLabel.AutoSize = true;

            brojPutnikaLabel = new Label();
            brojPutnikaLabel.Font = new Font("Segoe UI", 10F);
            brojPutnikaLabel.ForeColor = Color.FromArgb(64, 64, 64);
            brojPutnikaLabel.Location = new Point(20, 55);
            brojPutnikaLabel.AutoSize = true;

            ukupnaZaradaLabel = new Label();
            ukupnaZaradaLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ukupnaZaradaLabel.ForeColor = Color.FromArgb(40, 167, 69);
            ukupnaZaradaLabel.Location = new Point(20, 75);
            ukupnaZaradaLabel.AutoSize = true;

            overviewPanel.Controls.AddRange(new Control[] { 
                overviewTitle, brojKarataLabel, brojPutnikaLabel, ukupnaZaradaLabel 
            });

            // Statistics panels
            var statisticsPanel = new Panel();
            statisticsPanel.Dock = DockStyle.Fill;
            statisticsPanel.Padding = new Padding(0, 10, 0, 0);

            // Statistika po tipu panel
            var tipPanel = new Panel();
            tipPanel.Dock = DockStyle.Left;
            tipPanel.Width = 570;
            tipPanel.Padding = new Padding(0, 0, 10, 0);

            var tipTitle = new Label();
            tipTitle.Text = "Statistika po tipu karte";
            tipTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            tipTitle.ForeColor = Color.FromArgb(25, 118, 210);
            tipTitle.Dock = DockStyle.Top;
            tipTitle.Height = 30;
            tipTitle.TextAlign = ContentAlignment.MiddleLeft;

            statistikaPoTipuDataGridView = new DataGridView();
            statistikaPoTipuDataGridView.Dock = DockStyle.Fill;
            statistikaPoTipuDataGridView.BackgroundColor = Color.White;
            statistikaPoTipuDataGridView.BorderStyle = BorderStyle.FixedSingle;
            statistikaPoTipuDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            statistikaPoTipuDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            statistikaPoTipuDataGridView.MultiSelect = false;
            statistikaPoTipuDataGridView.ReadOnly = true;
            statistikaPoTipuDataGridView.AllowUserToAddRows = false;
            statistikaPoTipuDataGridView.AllowUserToDeleteRows = false;
            statistikaPoTipuDataGridView.RowHeadersVisible = false;
            statistikaPoTipuDataGridView.GridColor = Color.FromArgb(224, 224, 224);
            statistikaPoTipuDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 118, 210);
            statistikaPoTipuDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            statistikaPoTipuDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            statistikaPoTipuDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            statistikaPoTipuDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            statistikaPoTipuDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            statistikaPoTipuDataGridView.ColumnHeadersHeight = 40;

            tipPanel.Controls.AddRange(new Control[] { tipTitle, statistikaPoTipuDataGridView });

            // Statistika po statusu panel
            var statusPanel = new Panel();
            statusPanel.Dock = DockStyle.Right;
            statusPanel.Width = 570;
            statusPanel.Padding = new Padding(10, 0, 0, 0);

            var statusTitle = new Label();
            statusTitle.Text = "Statistika po statusu karte";
            statusTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            statusTitle.ForeColor = Color.FromArgb(25, 118, 210);
            statusTitle.Dock = DockStyle.Top;
            statusTitle.Height = 30;
            statusTitle.TextAlign = ContentAlignment.MiddleLeft;

            statistikaPoStatusuDataGridView = new DataGridView();
            statistikaPoStatusuDataGridView.Dock = DockStyle.Fill;
            statistikaPoStatusuDataGridView.BackgroundColor = Color.White;
            statistikaPoStatusuDataGridView.BorderStyle = BorderStyle.FixedSingle;
            statistikaPoStatusuDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            statistikaPoStatusuDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            statistikaPoStatusuDataGridView.MultiSelect = false;
            statistikaPoStatusuDataGridView.ReadOnly = true;
            statistikaPoStatusuDataGridView.AllowUserToAddRows = false;
            statistikaPoStatusuDataGridView.AllowUserToDeleteRows = false;
            statistikaPoStatusuDataGridView.RowHeadersVisible = false;
            statistikaPoStatusuDataGridView.GridColor = Color.FromArgb(224, 224, 224);
            statistikaPoStatusuDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 118, 210);
            statistikaPoStatusuDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            statistikaPoStatusuDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            statistikaPoStatusuDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            statistikaPoStatusuDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            statistikaPoStatusuDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            statistikaPoStatusuDataGridView.ColumnHeadersHeight = 40;

            statusPanel.Controls.AddRange(new Control[] { statusTitle, statistikaPoStatusuDataGridView });

            statisticsPanel.Controls.AddRange(new Control[] { tipPanel, statusPanel });

            mainPanel.Controls.AddRange(new Control[] { overviewPanel, statisticsPanel });
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        private async void LoadStatistikeAsync()
        {
            try
            {
                // Učitaj osnovne statistike
                var karte = await _kartaPresenter.GetAllKarteAsync();
                var putnici = await _kartaPresenter.GetAllPutniciAsync();
                var ukupnaZarada = await _kartaPresenter.GetUkupnaZaradaAsync();

                brojKarataLabel.Text = $"Ukupno karata: {karte.Count}";
                brojPutnikaLabel.Text = $"Ukupno putnika: {putnici.Count}";
                ukupnaZaradaLabel.Text = $"Ukupna zarada: {ukupnaZarada:C}";

                // Učitaj statistiku po tipu
                var statistikaPoTipu = await _kartaPresenter.GetStatistikaPoTipuAsync();
                statistikaPoTipuDataGridView.DataSource = statistikaPoTipu.Select(kvp => new
                {
                    TipKarte = kvp.Key,
                    BrojKarata = kvp.Value
                }).ToList();

                if (statistikaPoTipuDataGridView.Columns["TipKarte"] != null)
                    statistikaPoTipuDataGridView.Columns["TipKarte"].HeaderText = "Tip karte";
                if (statistikaPoTipuDataGridView.Columns["BrojKarata"] != null)
                    statistikaPoTipuDataGridView.Columns["BrojKarata"].HeaderText = "Broj karata";

                // Učitaj statistiku po statusu
                var statistikaPoStatusu = await _kartaPresenter.GetStatistikaPoStatusuAsync();
                statistikaPoStatusuDataGridView.DataSource = statistikaPoStatusu.Select(kvp => new
                {
                    StatusKarte = kvp.Key,
                    BrojKarata = kvp.Value
                }).ToList();

                if (statistikaPoStatusuDataGridView.Columns["StatusKarte"] != null)
                    statistikaPoStatusuDataGridView.Columns["StatusKarte"].HeaderText = "Status karte";
                if (statistikaPoStatusuDataGridView.Columns["BrojKarata"] != null)
                    statistikaPoStatusuDataGridView.Columns["BrojKarata"].HeaderText = "Broj karata";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju statistika: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshButton_Click(object? sender, EventArgs e)
        {
            LoadStatistikeAsync();
        }
    }
}
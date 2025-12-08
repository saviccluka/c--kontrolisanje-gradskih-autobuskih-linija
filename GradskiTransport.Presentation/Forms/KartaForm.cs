using GradskiTransport.Presentation.Presenters;
using GradskiTransport.Shared.Models;
using GradskiTransport.Shared.Enums;

namespace GradskiTransport.Presentation.Forms
{
    public partial class KartaForm : Form
    {
        private readonly KartaPresenter _kartaPresenter;
        private DataGridView dataGridView;
        private ComboBox putnikComboBox;
        private ComboBox tipKarteComboBox;
        private TextBox brojKarteTextBox;
        private Button dodajKartuButton;
        private Button validirajKartuButton;
        private Button iskoristiKartuButton;
        private Label statusLabel;

        public KartaForm()
        {
            _kartaPresenter = new KartaPresenter();
            InitializeComponent();
            LoadDataAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // KartaForm
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Name = "KartaForm";
            this.Text = "Upravljanje kartama - Gradski Transport";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Header panel
            var headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(25, 118, 210);
            headerPanel.Padding = new Padding(20, 10, 20, 10);

            var titleLabel = new Label();
            titleLabel.Text = "Upravljanje kartama";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);

            headerPanel.Controls.Add(titleLabel);
            this.Controls.Add(headerPanel);

            // Control panel
            var controlPanel = new Panel();
            controlPanel.Dock = DockStyle.Top;
            controlPanel.Height = 120;
            controlPanel.BackColor = Color.White;
            controlPanel.Padding = new Padding(20, 10, 20, 10);

            // Putnik ComboBox
            var putnikLabel = new Label();
            putnikLabel.Text = "Putnik:";
            putnikLabel.Font = new Font("Segoe UI", 10F);
            putnikLabel.Location = new Point(20, 20);
            putnikLabel.Size = new Size(60, 25);

            putnikComboBox = new ComboBox();
            putnikComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            putnikComboBox.Font = new Font("Segoe UI", 10F);
            putnikComboBox.Location = new Point(90, 18);
            putnikComboBox.Width = 200;

            // Tip karte ComboBox
            var tipKarteLabel = new Label();
            tipKarteLabel.Text = "Tip karte:";
            tipKarteLabel.Font = new Font("Segoe UI", 10F);
            tipKarteLabel.Location = new Point(310, 20);
            tipKarteLabel.Size = new Size(80, 25);

            tipKarteComboBox = new ComboBox();
            tipKarteComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipKarteComboBox.Font = new Font("Segoe UI", 10F);
            tipKarteComboBox.Location = new Point(400, 18);
            tipKarteComboBox.Width = 150;

            // Broj karte TextBox
            var brojKarteLabel = new Label();
            brojKarteLabel.Text = "Broj karte:";
            brojKarteLabel.Font = new Font("Segoe UI", 10F);
            brojKarteLabel.Location = new Point(570, 20);
            brojKarteLabel.Size = new Size(80, 25);

            brojKarteTextBox = new TextBox();
            brojKarteTextBox.Font = new Font("Segoe UI", 10F);
            brojKarteTextBox.Location = new Point(660, 18);
            brojKarteTextBox.Width = 150;

            // Buttons
            dodajKartuButton = new Button();
            dodajKartuButton.Text = "Dodaj kartu";
            dodajKartuButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dodajKartuButton.BackColor = Color.FromArgb(25, 118, 210);
            dodajKartuButton.ForeColor = Color.White;
            dodajKartuButton.FlatStyle = FlatStyle.Flat;
            dodajKartuButton.FlatAppearance.BorderSize = 0;
            dodajKartuButton.Location = new Point(20, 60);
            dodajKartuButton.Size = new Size(100, 35);
            dodajKartuButton.Click += DodajKartuButton_Click;

            validirajKartuButton = new Button();
            validirajKartuButton.Text = "Validiraj kartu";
            validirajKartuButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            validirajKartuButton.BackColor = Color.FromArgb(40, 167, 69);
            validirajKartuButton.ForeColor = Color.White;
            validirajKartuButton.FlatStyle = FlatStyle.Flat;
            validirajKartuButton.FlatAppearance.BorderSize = 0;
            validirajKartuButton.Location = new Point(140, 60);
            validirajKartuButton.Size = new Size(120, 35);
            validirajKartuButton.Click += ValidirajKartuButton_Click;

            iskoristiKartuButton = new Button();
            iskoristiKartuButton.Text = "Iskoristi kartu";
            iskoristiKartuButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            iskoristiKartuButton.BackColor = Color.FromArgb(255, 193, 7);
            iskoristiKartuButton.ForeColor = Color.White;
            iskoristiKartuButton.FlatStyle = FlatStyle.Flat;
            iskoristiKartuButton.FlatAppearance.BorderSize = 0;
            iskoristiKartuButton.Location = new Point(280, 60);
            iskoristiKartuButton.Size = new Size(120, 35);
            iskoristiKartuButton.Click += IskoristiKartuButton_Click;

            controlPanel.Controls.AddRange(new Control[] {
                putnikLabel, putnikComboBox, tipKarteLabel, tipKarteComboBox,
                brojKarteLabel, brojKarteTextBox, dodajKartuButton,
                validirajKartuButton, iskoristiKartuButton
            });

            this.Controls.Add(controlPanel);

            // DataGridView
            dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.GridColor = Color.FromArgb(224, 224, 224);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 118, 210);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridView.ColumnHeadersHeight = 40;

            this.Controls.Add(dataGridView);

            // Status Label
            statusLabel = new Label();
            statusLabel.Font = new Font("Segoe UI", 9F);
            statusLabel.ForeColor = Color.FromArgb(64, 64, 64);
            statusLabel.Dock = DockStyle.Bottom;
            statusLabel.Height = 30;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            statusLabel.Padding = new Padding(10, 0, 0, 0);

            this.Controls.Add(statusLabel);

            this.ResumeLayout(false);
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Učitaj karte
                var karte = await _kartaPresenter.GetAllKarteAsync();
                
                dataGridView.DataSource = karte.Select(k => new
                {
                    k.Id,
                    k.BrojKarte,
                    Putnik = $"{k.PutnikIme} {k.PutnikPrezime}",
                    Tip = k.Tip.ToString(),
                    Status = k.Status.ToString(),
                    DatumKupovine = k.DatumKupovine.ToString("dd.MM.yyyy HH:mm"),
                    DatumVazenja = k.DatumVazenja.ToString("dd.MM.yyyy HH:mm"),
                    Cena = k.Cena.ToString("C")
                }).ToList();

                // Sakrij ID kolonu
                if (dataGridView.Columns["Id"] != null)
                    dataGridView.Columns["Id"].Visible = false;

                // Postavi header tekstove
                if (dataGridView.Columns["BrojKarte"] != null)
                    dataGridView.Columns["BrojKarte"].HeaderText = "Broj karte";
                if (dataGridView.Columns["Putnik"] != null)
                    dataGridView.Columns["Putnik"].HeaderText = "Putnik";
                if (dataGridView.Columns["Tip"] != null)
                    dataGridView.Columns["Tip"].HeaderText = "Tip";
                if (dataGridView.Columns["Status"] != null)
                    dataGridView.Columns["Status"].HeaderText = "Status";
                if (dataGridView.Columns["DatumKupovine"] != null)
                    dataGridView.Columns["DatumKupovine"].HeaderText = "Datum kupovine";
                if (dataGridView.Columns["DatumVazenja"] != null)
                    dataGridView.Columns["DatumVazenja"].HeaderText = "Datum važenja";
                if (dataGridView.Columns["Cena"] != null)
                    dataGridView.Columns["Cena"].HeaderText = "Cena";

                // Učitaj putnike
                var putnici = await _kartaPresenter.GetAllPutniciAsync();
                putnikComboBox.DataSource = putnici;
                putnikComboBox.DisplayMember = "ImePrezime";
                putnikComboBox.ValueMember = "Id";

                // Učitaj tipove karata
                var tipovi = _kartaPresenter.GetTipoviKarata();
                tipKarteComboBox.DataSource = tipovi;

                statusLabel.Text = _kartaPresenter.GetStatusMessage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju podataka: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DodajKartuButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (putnikComboBox.SelectedItem == null || tipKarteComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Molimo izaberite putnika i tip karte.", "Greška", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var putnik = (PutnikDto)putnikComboBox.SelectedItem;
                var tipKarte = Enum.Parse<TipKarte>(tipKarteComboBox.SelectedItem.ToString()!);

                var karta = new KartaDto
                {
                    PutnikId = putnik.Id,
                    Tip = tipKarte,
                    BrojKarte = brojKarteTextBox.Text.Trim(),
                    DatumKupovine = DateTime.Now
                };

                bool success = await _kartaPresenter.AddKartaAsync(karta);
                
                if (success)
                {
                    MessageBox.Show("Karta je uspešno dodana!", "Uspeh", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAsync();
                    brojKarteTextBox.Clear();
                }
                else
                {
                    MessageBox.Show(_kartaPresenter.GetStatusMessage(), "Greška", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dodavanju karte: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ValidirajKartuButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(brojKarteTextBox.Text))
                {
                    MessageBox.Show("Molimo unesite broj karte za validaciju.", "Greška", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool isValid = await _kartaPresenter.ValidirajKartuAsync(brojKarteTextBox.Text.Trim());
                
                string message = isValid ? "Karta je validna i može se koristiti." : _kartaPresenter.GetStatusMessage();
                MessageBoxIcon icon = isValid ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
                
                MessageBox.Show(message, "Rezultat validacije", MessageBoxButtons.OK, icon);
                
                if (isValid)
                {
                    LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri validaciji karte: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void IskoristiKartuButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(brojKarteTextBox.Text))
                {
                    MessageBox.Show("Molimo unesite broj karte za iskorišćavanje.", "Greška", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = await _kartaPresenter.IskoristiKartuAsync(brojKarteTextBox.Text.Trim());
                
                string message = success ? "Karta je uspešno iskorišćena." : _kartaPresenter.GetStatusMessage();
                MessageBoxIcon icon = success ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
                
                MessageBox.Show(message, "Rezultat iskorišćavanja", MessageBoxButtons.OK, icon);
                
                if (success)
                {
                    LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri iskorišćavanju karte: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
using GradskiTransport.Presentation.Interfaces;
using GradskiTransport.Presentation.Presenters;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Forms
{
    public partial class RasporedForm : Form
    {
        private readonly ILinijaPresenter _linijaPresenter;
        private ComboBox linijaComboBox;
        private DataGridView polasciDataGridView;
        private Label vremeLabel;

        public RasporedForm()
        {
            _linijaPresenter = new LinijaPresenter();
            InitializeComponent();
            LoadLinijeAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // RasporedForm
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 700);
            this.Name = "RasporedForm";
            this.Text = "Raspored polazaka - Gradski Transport";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Title Label
            var titleLabel = new Label();
            titleLabel.Text = "Raspored polazaka";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(25, 118, 210);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Location = new Point(50, 30);
            titleLabel.Size = new Size(900, 40);

            // Linija Label
            var linijaLabel = new Label();
            linijaLabel.Text = "Izaberite liniju:";
            linijaLabel.Font = new Font("Segoe UI", 10F);
            linijaLabel.Location = new Point(50, 100);
            linijaLabel.Size = new Size(120, 25);

            // Linija ComboBox
            linijaComboBox = new ComboBox();
            linijaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            linijaComboBox.Font = new Font("Segoe UI", 10F);
            linijaComboBox.Location = new Point(180, 98);
            linijaComboBox.Width = 200;
            linijaComboBox.SelectedIndexChanged += LinijaComboBox_SelectedIndexChanged;

            // Vreme Label
            vremeLabel = new Label();
            vremeLabel.Font = new Font("Segoe UI", 10F);
            vremeLabel.ForeColor = Color.FromArgb(64, 64, 64);
            vremeLabel.Location = new Point(400, 100);
            vremeLabel.Size = new Size(300, 25);

            // Polasci DataGridView
            polasciDataGridView = new DataGridView();
            polasciDataGridView.Location = new Point(50, 150);
            polasciDataGridView.Size = new Size(900, 450);
            polasciDataGridView.BackgroundColor = Color.White;
            polasciDataGridView.BorderStyle = BorderStyle.None;
            polasciDataGridView.GridColor = Color.FromArgb(224, 224, 224);
            polasciDataGridView.RowHeadersVisible = false;
            polasciDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            polasciDataGridView.MultiSelect = false;
            polasciDataGridView.ReadOnly = true;
            polasciDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            polasciDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            polasciDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            polasciDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            polasciDataGridView.ColumnHeadersHeight = 40;

            this.Controls.AddRange(new Control[] {
                titleLabel, linijaLabel, linijaComboBox, vremeLabel, polasciDataGridView
            });

            // Timer za ažuriranje vremena
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 sekunda
            timer.Tick += (s, e) => vremeLabel.Text = $"Trenutno vreme: {DateTime.Now:HH:mm:ss}";
            timer.Start();

            this.ResumeLayout(false);
        }

        private async void LoadLinijeAsync()
        {
            try
            {
                var linije = await _linijaPresenter.GetAllLinijeAsync();
                
                linijaComboBox.DataSource = linije;
                linijaComboBox.DisplayMember = "Naziv";
                linijaComboBox.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju linija: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LinijaComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (linijaComboBox.SelectedItem is Linija selectedLinija)
            {
                await LoadPolasciAsync(selectedLinija.Id);
            }
        }

        private async Task LoadPolasciAsync(int linijaId)
        {
            try
            {
                var polasci = await _linijaPresenter.GetPolasciByLinijaAsync(linijaId);
                
                polasciDataGridView.DataSource = polasci.Select(p => new
                {
                    p.Id,
                    VremePolaska = p.VremePolaska.ToString(@"hh\:mm"),
                    p.TipVozila
                }).ToList();

                polasciDataGridView.Columns["Id"].Visible = false;
                polasciDataGridView.Columns["VremePolaska"].HeaderText = "Vreme polaska";
                polasciDataGridView.Columns["TipVozila"].HeaderText = "Tip vozila";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju polazaka: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
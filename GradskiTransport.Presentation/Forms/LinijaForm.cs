using GradskiTransport.Presentation.Interfaces;
using GradskiTransport.Presentation.Presenters;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Forms
{
    public partial class LinijaForm : Form
    {
        private readonly ILinijaPresenter _linijaPresenter;
        private DataGridView dataGridView;

        public LinijaForm()
        {
            _linijaPresenter = new LinijaPresenter();
            InitializeComponent();
            LoadLinijeAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // LinijaForm
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 600);
            this.Name = "LinijaForm";
            this.Text = "Linije - Gradski Transport";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Header panel
            var headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(25, 118, 210);
            headerPanel.Padding = new Padding(20, 10, 20, 10);

            var titleLabel = new Label();
            titleLabel.Text = "Linije gradskog transporta";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);

            headerPanel.Controls.Add(titleLabel);
            this.Controls.Add(headerPanel);

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

            this.ResumeLayout(false);
        }

        private async void LoadLinijeAsync()
        {
            try
            {
                var linije = await _linijaPresenter.GetAllLinijeAsync();
                
                dataGridView.DataSource = linije.Select(l => new
                {
                    l.Id,
                    l.BrojLinije,
                    l.Naziv,
                    l.Opis
                }).ToList();

                dataGridView.Columns["Id"].Visible = false;
                dataGridView.Columns["BrojLinije"].HeaderText = "Broj linije";
                dataGridView.Columns["Naziv"].HeaderText = "Naziv";
                dataGridView.Columns["Opis"].HeaderText = "Opis";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju linija: {ex.Message}", "Greška", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

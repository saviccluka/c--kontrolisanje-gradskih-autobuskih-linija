using GradskiTransport.Presentation.Interfaces;
using GradskiTransport.Presentation.Presenters;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Forms
{
    public partial class LoginForm : UserControl
    {
        private readonly IUserPresenter _userPresenter;
        
        private Label titleLabel;
        private Label usernameLabel;
        private TextBox usernameTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label statusLabel;

        public event Action<Korisnik>? LoginSuccessful;

        public LoginForm()
        {
            _userPresenter = new UserPresenter();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Gradski Transport - Prijava";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(25, 118, 210);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Location = new Point(50, 50);
            titleLabel.Size = new Size(300, 40);

            // Username Label
            usernameLabel = new Label();
            usernameLabel.Text = "Korisničko ime:";
            usernameLabel.Font = new Font("Segoe UI", 10F);
            usernameLabel.Location = new Point(50, 120);
            usernameLabel.Size = new Size(120, 25);

            // Username TextBox
            usernameTextBox = new TextBox();
            usernameTextBox.Font = new Font("Segoe UI", 10F);
            usernameTextBox.Location = new Point(180, 118);
            usernameTextBox.Size = new Size(200, 25);
            usernameTextBox.Text = "admin"; // Default za lakše testiranje

            // Password Label
            passwordLabel = new Label();
            passwordLabel.Text = "Lozinka:";
            passwordLabel.Font = new Font("Segoe UI", 10F);
            passwordLabel.Location = new Point(50, 160);
            passwordLabel.Size = new Size(120, 25);

            // Password TextBox
            passwordTextBox = new TextBox();
            passwordTextBox.Font = new Font("Segoe UI", 10F);
            passwordTextBox.Location = new Point(180, 158);
            passwordTextBox.Size = new Size(200, 25);
            passwordTextBox.UseSystemPasswordChar = true;
            passwordTextBox.Text = "admin123"; // Default za lakše testiranje

            // Login Button
            loginButton = new Button();
            loginButton.Text = "Prijavite se";
            loginButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            loginButton.BackColor = Color.FromArgb(25, 118, 210);
            loginButton.ForeColor = Color.White;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Location = new Point(180, 210);
            loginButton.Size = new Size(120, 35);
            loginButton.Click += LoginButton_Click;

            // Status Label
            statusLabel = new Label();
            statusLabel.Font = new Font("Segoe UI", 9F);
            statusLabel.ForeColor = Color.Red;
            statusLabel.Location = new Point(50, 260);
            statusLabel.Size = new Size(330, 50);
            statusLabel.Text = "Podaci za testiranje:\nadmin / admin123 (Administrator)\nmarko / marko123 (Korisnik)";

            // LoginForm
            this.Controls.AddRange(new Control[] {
                titleLabel, usernameLabel, usernameTextBox, passwordLabel, 
                passwordTextBox, loginButton, statusLabel
            });
            this.Size = new Size(400, 350);
            this.BackColor = Color.White;

            // Event handlers
            usernameTextBox.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) passwordTextBox.Focus(); };
            passwordTextBox.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) LoginButton_Click(s, e); };

            this.ResumeLayout(false);
        }

        private async void LoginButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                statusLabel.Text = "Molimo unesite korisničko ime i lozinku.";
                statusLabel.ForeColor = Color.Red;
                return;
            }

            loginButton.Enabled = false;
            loginButton.Text = "Prijavljivanje...";
            statusLabel.Text = "";

            try
            {
                bool isValid = await _userPresenter.ValidateUserAsync(usernameTextBox.Text, passwordTextBox.Text);
                
                if (isValid)
                {
                    var user = await _userPresenter.GetUserAsync(usernameTextBox.Text);
                    if (user != null)
                    {
                        statusLabel.Text = "Uspešno prijavljivanje!";
                        statusLabel.ForeColor = Color.Green;
                        
                        // Kratka pauza pre nego što pozovemo event
                        await Task.Delay(500);
                        LoginSuccessful?.Invoke(user);
                    }
                }
                else
                {
                    statusLabel.Text = _userPresenter.GetStatusMessage();
                    statusLabel.ForeColor = Color.Red;
                    usernameTextBox.Focus();
                    usernameTextBox.SelectAll();
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Greška pri prijavljivanju: {ex.Message}";
                statusLabel.ForeColor = Color.Red;
            }
            finally
            {
                loginButton.Enabled = true;
                loginButton.Text = "Prijavite se";
            }
        }
    }
}

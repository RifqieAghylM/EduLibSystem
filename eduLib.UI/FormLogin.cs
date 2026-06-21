using eduLib.Application.Auth;
using eduLib.Core.Entities;
using eduLib.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormLogin : Form
    {
        private readonly AuthService _authService;
        private bool _passwordVisible = false;
        private Button btnEye;

        private static readonly List<User> _mockUsers = new List<User>
        {
            new User { Username = "admin", Password = "admin123", UserRole = Role.Admin },
            new User { Username = "user",  Password = "user123",  UserRole = Role.User  }
        };

        public FormLogin()
        {
            InitializeComponent();
            txtpassword.UseSystemPasswordChar = true;
            SetupEyeButton();
            _authService = new AuthService(_mockUsers);
        }

        private void SetupEyeButton()
        {
            btnEye = new Button();

            int btnW = 28;

            btnEye.Size = new Size(btnW, txtpassword.Height);
            btnEye.Location = new Point(
                txtpassword.Right - btnW - 2,
                txtpassword.Top
            );

            btnEye.FlatStyle = FlatStyle.Flat;
            btnEye.FlatAppearance.BorderSize = 0;
            btnEye.FlatAppearance.BorderColor = Color.White;
            btnEye.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 225, 225);
            btnEye.BackColor = Color.White;
            btnEye.Text = "🔒";
            btnEye.Font = new Font("Segoe UI Emoji", 9);
            btnEye.Cursor = Cursors.Hand;
            btnEye.TabStop = false;
            btnEye.Click += BtnEye_Click;

            Control parent = txtpassword.Parent ?? (Control)this;
            parent.Controls.Add(btnEye);
            btnEye.BringToFront();
        }

        private void BtnEye_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtpassword.UseSystemPasswordChar = !_passwordVisible;
            btnEye.Text = _passwordVisible ? "👁" : "🔒";
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = _authService.Login(username, password);
                this.Hide();

                if (user.UserRole == Role.Admin)
                {
                    var adminDashboard = new FormDashboardAdmin();
                    adminDashboard.FormClosed += (s, args) => KembaliKeLogin();
                    adminDashboard.Show();
                }
                else
                {
                    var userDashboard = new FormDashboardUser();
                    userDashboard.FormClosed += (s, args) => KembaliKeLogin();
                    userDashboard.Show();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Username atau Password salah!", "Login Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtpassword.Clear();
                txtpassword.Focus();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Akun Terkunci",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KembaliKeLogin()
        {
            _authService.Logout();
            txtusername.Clear();
            txtpassword.Clear();
            _passwordVisible = false;
            txtpassword.UseSystemPasswordChar = true;
            btnEye.Text = "🔒";
            txtusername.Focus();
            this.Show();
        }
    }
}
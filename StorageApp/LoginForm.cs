// LoginForm.cs

using System;
using System.Windows.Forms;

namespace StorageApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user, pass;
            user = txtUser.Text;
            pass = txtPass.Text;
            if (user == "Admin" && pass == "1234")
            {
                // Jika login berhasil, set DialogResult menjadi OK
                DialogResult = DialogResult.OK;
                MessageBox.Show("Login Berhasil");
            }
            else
            {
                MessageBox.Show("Data yang dimasukan salah");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

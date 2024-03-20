using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace StorageApp
{
    public partial class MainForm : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;

        dbconnection dbconn = new dbconnection();
        public MainForm()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.dbconnect());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecord();
            dataGridView1.RowTemplate.Height = 25;
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `id_barang`, `nama_pemilik`, `rak_nomer`, `tanggal_menyimpan`, `alamat`, `no_telepon` FROM `tb_crud`", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["id_barang"].ToString(), dr["nama_pemilik"].ToString(), dr["rak_nomer"].ToString(), dr["tanggal_menyimpan"].ToString(), dr["alamat"].ToString(), dr["no_telepon"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        public void clear()
        {
            txt_Alamat.Clear();
            txt_IDBarang.Clear();
            txt_NamaPemilik.Clear();
            txt_No.Clear();
            txt_Search.Clear();
            cbo_RakNomer.SelectedIndex = -1;
            dtp_Tanggal.Value = DateTime.Now;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if ((txt_IDBarang.Text == string.Empty) || (txt_NamaPemilik.Text == string.Empty) || (cbo_RakNomer.Text == string.Empty) || (txt_Alamat.Text == string.Empty) || (txt_No.Text == string.Empty) || (cbo_RakNomer.Text == string.Empty))
            {
                MessageBox.Show("Peringatan : Tidak Boleh Ada Yang Kosong!!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string date1 = dtp_Tanggal.Value.ToString("yyyy-MM-dd");
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `tb_crud`(`id_barang`, `nama_pemilik`, `rak_nomer`, `tanggal_menyimpan`, `alamat`, `no_telepon`) VALUES (@id_barang, @nama_pemilik, @rak_nomer, @tanggal_menyimpan, @alamat, @no_telepon)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_barang", txt_IDBarang.Text);
                cmd.Parameters.AddWithValue("@nama_pemilik", txt_NamaPemilik.Text);
                cmd.Parameters.AddWithValue("@rak_nomer", cbo_RakNomer.Text);
                cmd.Parameters.AddWithValue("@tanggal_menyimpan", date1);
                cmd.Parameters.AddWithValue("@alamat", txt_Alamat.Text);
                cmd.Parameters.AddWithValue("@no_telepon", txt_No.Text);

                i = cmd.ExecuteNonQuery();
                if (i>0)
                {
                    MessageBox.Show("Data Sukses di Simpan!!", "CRUD",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data Gagal di Simpan!!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conn.Close();
                LoadRecord();
                clear();
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string date1 = dtp_Tanggal.Value.ToString("yyyy-MM-dd");
            conn.Open();
            cmd = new MySqlCommand("UPDATE `tb_crud`SET`nama_pemilik`=@nama_pemilik,`rak_nomer`=@rak_nomer,`tanggal_menyimpan`=@tanggal_menyimpan,`alamat`=@alamat,`no_telepon`=@no_telepon WHERE `id_barang`=@id_barang", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id_barang", txt_IDBarang.Text);
            cmd.Parameters.AddWithValue("@nama_pemilik", txt_NamaPemilik.Text);
            cmd.Parameters.AddWithValue("@rak_nomer", cbo_RakNomer.Text);
            cmd.Parameters.AddWithValue("@tanggal_menyimpan", date1);
            cmd.Parameters.AddWithValue("@alamat", txt_Alamat.Text);
            cmd.Parameters.AddWithValue("@no_telepon", txt_No.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Data Sukses di Perbarui!!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Gagal di Perbarui!!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            conn.Close();
            LoadRecord();
            clear();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            string date1 = dtp_Tanggal.Value.ToString("yyyy-MM-dd");
            conn.Open();
            cmd = new MySqlCommand("Delete From `tb_crud` WHERE `id_barang`=@id_barang", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id_barang", txt_IDBarang.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Data Sukses di Hapus!!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Gagal di Hapus!!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            conn.Close();
            LoadRecord();
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_IDBarang.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_NamaPemilik.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbo_RakNomer.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dtp_Tanggal.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txt_Alamat.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_No.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `id_barang`, `nama_pemilik`, `rak_nomer`, `tanggal_menyimpan`, `alamat`, `no_telepon` FROM `tb_crud`  WHERE id_barang LIKE '%" + txt_Search.Text + "%' OR nama_pemilik LIKE '%" + txt_Search.Text + "%' OR alamat LIKE '%" + txt_Search.Text + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["id_barang"].ToString(), dr["nama_pemilik"].ToString(), dr["rak_nomer"].ToString(), dr["tanggal_menyimpan"].ToString(), dr["alamat"].ToString(), dr["no_telepon"].ToString());
            }
            dr.Close();
            conn.Close();
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

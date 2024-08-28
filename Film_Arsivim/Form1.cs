using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbFilmArsivi;Integrated Security=True");

        void filimler()
        {
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select AD,LINK from TblFilmler", baglanti);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            filimler();
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand komut = new SqlCommand("insert into TblFilmler(AD,KATEGORI,LINK) values (@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", textBoxFilmAd.Text);
                komut.Parameters.AddWithValue("@p2", textBoxKategori.Text);
                komut.Parameters.AddWithValue("@p3", textBoxLink.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Film listenize eklendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                filimler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                string link = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                webBrowser1.Navigate(link);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonHakkımızda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu proje Semiha Özcan tarafından kodlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonRenkDegistir_Click(object sender, EventArgs e)
        {
            ColorDialog renkSec = new ColorDialog();
            if (renkSec.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = renkSec.Color;
            }
        }

        private void buttonTamEkran_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized; // Tam ekran moduna geç
            }
            else
            {
                this.WindowState = FormWindowState.Normal; // Normal moda dön
            }
        }
    }
}

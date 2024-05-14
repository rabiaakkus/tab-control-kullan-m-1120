using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace tabcontrol_kullanımı_1120
{
    public partial class Form1 : Form
    {
        string baglanti = "Server=localhost;Database=hnk_okul;Uid=root;Pwd=;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DgwDoldur();
        }
        void DgwDoldur()
        {
            using (MySqlConnection baglan = new MySqlConnection(baglanti))
            {
                baglan.Open();
                string sorgu = "SELECT * FROM filmler;";

                MySqlCommand cmd = new MySqlCommand(sorgu, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
             
                dgwFilmler.DataSource = dt;
                dgwFilmler.Columns["film_odul"].Visible = false;
                dgwFilmler.Columns["yonetmen"].Visible = false;
                dgwFilmler.Columns["poster"].Visible = false;
                dgwFilmler.Columns["sure"].Visible = false;
                dgwFilmler.Columns["film_id"].Visible = false;


            }
        }

        private void dgwFilmler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwFilmler.SelectedRows.Count > 0)
            {
                lblFilmAd.Text = dgwFilmler.SelectedRows[0].Cells["film_ad"].Value.ToString();
                lblYonetmen.Text = dgwFilmler.SelectedRows[0].Cells["yonetmen"].Value.ToString();
                lblPuan.Text = dgwFilmler.SelectedRows[0].Cells["imdb_puan"].Value.ToString();
                lblSure.Text = dgwFilmler.SelectedRows[0].Cells["sure"].Value.ToString();
                lblYil.Text = dgwFilmler.SelectedRows[0].Cells["yil"].Value.ToString();
                lblTur.Text = dgwFilmler.SelectedRows[0].Cells["tur"].Value.ToString();


                lblOdul.Text = "ödül yok";
                if (Convert.ToBoolean(dgwFilmler.SelectedRows[0].Cells["odul"].Value))
                {
                    lblOdul.Text = "ödüllü film";
                }


                string resimyol = Path.Combine(Environment.CurrentDirectory, "poster", dgwFilmler.SelectedRows[0].Cells["poster"].Value.ToString());

                if (File.Exists(resimyol))
                {
                    pictureBox1.ImageLocation = resimyol;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }




            }
        }
    }
}

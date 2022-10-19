using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using USB_Barcode_Scanner;

namespace WindowsFormsApp10.Forms
{
    public partial class FormNotifications : Form
    {
        public FormNotifications()
        {
            InitializeComponent();
            BarcodeScanner barcodeScanner = new BarcodeScanner(textBox1);
            barcodeScanner.BarcodeScanned += BarcodeScanner_BarcodeScanned;
        }

        private void BarcodeScanner_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            textBox1.Text = e.Barcode;
        }
    
        //int tutar = 0;
        int musteriBarkod;
       // string islemler;
        string musteriAdi;
        string musteriSoyadi;
        string musteriTelNo;
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-M2FJJ7A2\\SQLEXPRESS;Initial Catalog=MaliKuafor;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        DataTable tablo = new DataTable();
        DataSet ds = new DataSet();
        public void dataGridGuncelle()
        {
            baglanti.Open();
            DataSet dtst = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * From musteriler ", baglanti);
            adtr.Fill(dtst, "musteriler");
            dataGridView1.DataSource = dtst.Tables["musteriler"];
            adtr.Dispose();
            baglanti.Close();

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            //checkBox1.CheckState = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void FormNotifications_Load(object sender, EventArgs e)
        {
            dataGridGuncelle();
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {



                musteriBarkod = Convert.ToInt32(textBox1.Text);
                musteriAdi = textBox2.Text;
                musteriSoyadi = textBox3.Text;
                musteriTelNo = textBox4.Text;
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "UPDATE musteriler SET musteriAdi='" + textBox2.Text + "',musteriSoyadi='" + textBox3.Text + "',musteriTelNo='" + textBox4.Text + "',boyaKodu='" + textBox5.Text + "',tutar='" + textBox6.Text + "' where musteriBarkod=" + textBox1.Text + "";
                int sonuc = komut.ExecuteNonQuery();
                //executenonquery komutu geriye etkilenen kayıt sayısını döndürür
                MessageBox.Show(+sonuc + " " + "Adet Kayıt Güncellendi","Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                komut.Dispose();
                baglanti.Close();
                dataGridGuncelle();
            }
            else
            {
                MessageBox.Show("Hata","Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriBarkod LIKE '%" + textBox10.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriAdi LIKE '%" + textBox7.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriSoyadi LIKE '%" + textBox8.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriTelNo LIKE '%" + textBox9.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                textBox10.Visible = true;
            }
            else if (checkBox7.Checked == false)
            {
                textBox10.Visible = false;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                textBox7.Visible = true;
            }
            else if (checkBox8.Checked == false)
            {
                textBox7.Visible = false;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                textBox8.Visible = true;
            }
            else if (checkBox9.Checked == false)
            {
                textBox8.Visible = false;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox10.Checked == true)
            {
                textBox9.Visible = true;
            }
            else if (checkBox10.Checked == false)
            {
                textBox10.Visible = false;
            }
        }
    }
}

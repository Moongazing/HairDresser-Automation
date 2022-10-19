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
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
            BarcodeScanner barcodeScanner = new BarcodeScanner(textBox1);
            barcodeScanner.BarcodeScanned += BarcodeScanner_BarcodeScanned;
        }

        private void BarcodeScanner_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            textBox1.Text = e.Barcode;
        }

        int tutar = 0;
        string musteriBarkod;
        string islemler;
        string musteriAdi;
        string musteriSoyadi;
        string musteriTelNo;
        DateTime tarih;
        private void FormOrders_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
                label1.ForeColor = ThemeColor.SecondaryColor;
                label2.ForeColor = ThemeColor.PrimaryColor;
            }
        
        }
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
        private void FormProduct_Load(object sender, EventArgs e)
        {
            dataGridGuncelle();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label6.Visible = true;
                textBox5.Visible = true;
                islemler = islemler + "," + checkBox1.Text;
                tutar =tutar+ 50;

            }
            else if (checkBox1.Checked == false)
            {
                label6.Visible = false;
                textBox5.Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {


                tarih = DateTime.Now;
                musteriBarkod = textBox1.Text;
               
                
                musteriAdi = textBox2.Text;
                musteriSoyadi = textBox3.Text;
                musteriTelNo = textBox4.Text;
                baglanti.Open();

                komut.Connection = baglanti;
                komut.CommandText = "Select Count(*) From musteriler Where musteriBarkod='" + textBox1.Text + "'";
                int sonuc = Convert.ToInt32(komut.ExecuteScalar());
                if (sonuc > 0)
                {
                    MessageBox.Show("Kayıt Zaten Var!","Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    komut.Dispose();
                    baglanti.Close();
                }
                else
                {

                    komut.CommandText = "Insert Into musteriler(musteriBarkod,musteriAdi,musteriSoyadi,musteriTelNo,yapılanIslem,tutar,boyaKodu,tarih) Values('" + musteriBarkod + "','" + musteriAdi + "','" + musteriSoyadi + "','" + musteriTelNo + "','" + islemler + "'," + tutar + ",'" + textBox5.Text + "','"+tarih+"')";
                    label8.Text = tutar.ToString();
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                    MessageBox.Show("Kayıt Tamamlandı.","Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;

                    //label7.Text = tutar.ToString();
                    dataGridGuncelle();
                   


                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
              
                islemler = islemler + "," + checkBox2.Text;
                tutar = tutar + 50;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
               
                islemler = islemler + "," + checkBox3.Text;
                tutar = tutar + 50;

            }
        }
    }
}

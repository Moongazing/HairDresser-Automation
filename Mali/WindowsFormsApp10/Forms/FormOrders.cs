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
    public partial class FormOrders : Form
    {
        public FormOrders()
        {
            InitializeComponent();
            BarcodeScanner barcodeScanner = new BarcodeScanner(textBox1);
            barcodeScanner.BarcodeScanned += BarcodeScanner_BarcodeScanned;
        }

        private void BarcodeScanner_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            textBox1.Text = e.Barcode;
        }
    
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-M2FJJ7A2\\SQLEXPRESS;Initial Catalog=MaliKuafor;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        DataTable tablo = new DataTable();
        DataSet ds = new DataSet();

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Visible = true;
            }
            else if (checkBox1.Checked==false)
            {
                textBox1.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox2.Visible = true;
            }
            else if (checkBox2.Checked == false)
            {
                textBox2.Visible = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox3.Visible = true;
            }
            else if (checkBox1.Checked == false)
            {
                textBox3.Visible = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox4.Visible = true;
            }
            else if (checkBox4.Checked == false)
            {
                textBox4.Visible = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                textBox5.Visible = true ;
            }
            else if (checkBox5.Checked == false)
            {
                textBox5.Visible = false;
            }
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            baglanti.Open();
            DataSet dtst = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * From musteriler ", baglanti);
            adtr.Fill(dtst, "musteriler");
            dataGridView1.DataSource = dtst.Tables["musteriler"];
            adtr.Dispose();
            baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriBarkod LIKE '%" + textBox1.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriAdi LIKE '%" + textBox2.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriSoyadi LIKE '%" + textBox3.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE musteriTelNo LIKE '%" + textBox4.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE boyaKodu LIKE '%" + textBox5.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {

                button1.Visible = true;
                dateTimePicker1.Visible = true;
                label2.Visible = true;
                dateTimePicker2.Visible = true;

            }
            else
            {
                button1.Visible = false;
                dateTimePicker1.Visible = false;
                label2.Visible = false;
                dateTimePicker2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tablo.Clear();//Kayıtlar karışmamalı 
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM musteriler WHERE tarih BETWEEN @tarih1 and @tarih2", baglanti);
            adtr.SelectCommand.Parameters.AddWithValue("@tarih1", dateTimePicker1.Value.ToShortDateString());
            adtr.SelectCommand.Parameters.AddWithValue("@tarih2", dateTimePicker2.Value.ToShortDateString());
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}

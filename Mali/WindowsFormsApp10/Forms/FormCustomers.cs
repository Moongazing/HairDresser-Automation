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
    public partial class FormCustomers : Form
    {
        public FormCustomers()
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

        private void button1_Click(object sender, EventArgs e)
        {
            string silinecek = textBox1.Text;
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "DELETE FROM musteriler Where musteriBarkod='" + silinecek + "'";
            int sonuc = komut.ExecuteNonQuery();//sayi döndür kayıt var mı yok mu
            komut.Dispose();
            baglanti.Close();
            if (sonuc == 0)
            {
                MessageBox.Show("Silinecek Kayıt Bulunamadı","Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                textBox1.SelectAll();
            }
            else
                MessageBox.Show(sonuc + " Adet Kayıt Silindi","Silme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                textBox1.Focus();
                textBox1.SelectAll();
        }
    }
}

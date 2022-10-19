using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        
            
        
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        //Constructor
        public Form1()
        {
            
            InitializeComponent();
            random = new Random();
            btnCloseChildForm.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor= ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        private void btnProducts_Click1(object sender, EventArgs e)
        {
          
        }
        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormOrders(), sender);
        }
        private void btnCustomers_Click1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCustomers(), sender);
        }
        private void btnReport_Click1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormReporting(), sender);
        }
        private void btnNotifications_Click1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormNotifications(), sender);
        }
        private void btnSetting_Click1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormSettings(), sender);
        }
        private void btnCloseChildForm_Click1(object sender, EventArgs e)
        {
            
        }
        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "ANASAYFA";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            int cevap; cevap = Convert.ToInt32(MessageBox.Show("Çıkmak İstiyor Musunuz?", "Çıkış", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning));
            if (cevap == 6) { Application.Exit(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnProducts_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void btnOrders_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCustomers(), sender);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormNotifications(), sender);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormOrders(), sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
    }
}

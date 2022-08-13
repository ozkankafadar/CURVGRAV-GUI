using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CURVGRAV
{
    public partial class MainForm : Form
    {
        //Forms
        public static SyntheticApp Synthetic_App;
        public static NewData New_Data;
        public static FieldApp Field_App;
        public static SettingsForm Settings_Form;
        public static AboutForm About_Form;

        public static int numcolmap, numcolscatter, markersize, markerstroke, markertype,fontsize;

        public void cultureset()
        {
            System.Globalization.CultureInfo newculture = new System.Globalization.CultureInfo("tr-TR");
            newculture.NumberFormat.NumberDecimalSeparator = ".";
            newculture.NumberFormat.NumberGroupSeparator = ".";
            Application.CurrentCulture = newculture;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Initial Settings of Maps
            numcolmap = 500;
            numcolscatter = 10;
            markersize = 4;
            markerstroke = 1;
            markertype = 0;
            fontsize = 16;
            cultureset();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (About_Form == null)
            {
                About_Form = new AboutForm();
                //About_Form.MdiParent = this;
                About_Form.ShowDialog();
                
            }            
        }

        private void btn_synthetic_app_Click(object sender, EventArgs e)
        {
            if (Synthetic_App == null)
            {
                Synthetic_App = new SyntheticApp();
                Synthetic_App.MdiParent = this;
                Synthetic_App.Show();
                Synthetic_App.Location = new Point(0, 0);
            }
            else
            {
                Synthetic_App.Activate();
            }
        }

        private void btn_new_file_Click(object sender, EventArgs e)
        {
            if (New_Data == null)
            {
                New_Data = new NewData();
                New_Data.MdiParent = this;
                New_Data.Show();
                New_Data.Location = new Point(0, 0);
            }
            else
            {
                New_Data.Activate();
            }
        }

        private void btn_field_app_Click(object sender, EventArgs e)
        {
            if (Field_App == null)
            {
                Field_App = new FieldApp();
                Field_App.MdiParent = this;
                Field_App.Show();
                Field_App.Location = new Point(0, 0);
            }
            else
            {
                Field_App.Activate();
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            if (Settings_Form == null)
            {
                Settings_Form = new SettingsForm();
                Settings_Form.MdiParent = this;
                Settings_Form.Show();
                Settings_Form.Location = new Point(0, 0);
            }
            else
            {
                Settings_Form.Activate();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Are you sure?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }


    }
}

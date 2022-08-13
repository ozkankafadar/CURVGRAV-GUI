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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            CURVGRAV.MainForm.numcolmap = int.Parse(cbox_numcolour.Text);
            CURVGRAV.MainForm.numcolscatter = int.Parse(cbox_numcolourscatter.Text);
            CURVGRAV.MainForm.markersize = int.Parse(cbox_markersize.Text);
            CURVGRAV.MainForm.markerstroke = int.Parse(cbox_strokethick.Text);
            CURVGRAV.MainForm.markertype = cbox_markertype.SelectedIndex;
            CURVGRAV.MainForm.fontsize = int.Parse(tx_fontsize.Text);
            this.Close();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CURVGRAV.MainForm.Settings_Form = null;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            int numcolmap = CURVGRAV.MainForm.numcolmap;
            int numcolscatter = CURVGRAV.MainForm.numcolscatter;
            int markersize = CURVGRAV.MainForm.markersize;
            int markerstroke = CURVGRAV.MainForm.markerstroke;
            int markertype = CURVGRAV.MainForm.markertype;
            int fontsize = CURVGRAV.MainForm.fontsize;

            tx_fontsize.Text = fontsize.ToString();

            cbox_markertype.SelectedIndex = markertype;          

            for (int i = 0; i < cbox_numcolour.Items.Count; i++)
            {
                if (cbox_numcolour.Items[i].ToString() == numcolmap.ToString()) cbox_numcolour.SelectedIndex = i;
            }

            for (int i = 0; i < cbox_numcolourscatter.Items.Count; i++)
            {
                if (cbox_numcolourscatter.Items[i].ToString() == numcolscatter.ToString()) cbox_numcolourscatter.SelectedIndex = i;
            }

            for (int i = 0; i < cbox_markersize.Items.Count; i++)
            {
                if (cbox_markersize.Items[i].ToString() == markersize.ToString()) cbox_markersize.SelectedIndex = i;
            }

            for (int i = 0; i < cbox_strokethick.Items.Count; i++)
            {
                if (cbox_strokethick.Items[i].ToString() == markerstroke.ToString()) cbox_strokethick.SelectedIndex = i;
            }

            for (int i = 0; i < cbox_numcolourscatter.Items.Count; i++)
            {
                cbox_numcolourscatter.SelectedIndex = markertype;
            }
        }
    }
}

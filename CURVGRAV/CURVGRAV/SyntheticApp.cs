using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace CURVGRAV
{
    public partial class SyntheticApp : Form
    {
        int numberofmass;
        double[,] parameters;
        double[,] anomaly, hgm;
        double beta;
        string unit;
        int nc, nr;
        double dx, dy;
        double[,] Km, Kg, Kmax, Kmin, Kn;
        double maxdepth, mindepth;
        int numcolmap, numcolscatter, markersize, markerstroke,markertype,fontsize;
        Point currentcellvar;
        int faultmodel = 1;
        double[] zcrit, xcrit, ycrit,xecrit,yecrit,zecrit;
        string[] statecrit;
        double[] zzcrit, yycrit, xxcrit;
        double[] zzecrit, yyecrit, xxecrit;
        PlotModel Pm1, Pm2, Pm3, Pm4, Pm5, Pm6, Pm7, Pm8, Pm9;

        public SyntheticApp()
        {
            InitializeComponent();
        }

        private void read_map_settings()
        { 
            numcolmap = CURVGRAV.MainForm.numcolmap;
            numcolscatter = CURVGRAV.MainForm.numcolscatter;
            markersize = CURVGRAV.MainForm.markersize;
            markerstroke = CURVGRAV.MainForm.markerstroke;
            markertype = CURVGRAV.MainForm.markertype;
            fontsize = CURVGRAV.MainForm.fontsize;
        }
        
        private void SyntheticApp_Load(object sender, EventArgs e)
        {
            read_map_settings();
        }       

        private void btn_parameters_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_parameters.Text == "CANCEL")
                {
                    btn_parameters.Text = "PARAMETERS";
                    tx_xnum.Text = null;
                    tx_ynum.Text = null;
                    tx_dx.Text = null;
                    tx_dy.Text = null;
                    nupdown_massnum.Value = 1;

                    btn_calculate.Enabled = false;
                    btn_save.Enabled = false;
                    btn_refresh.Enabled = false;
                    dgparameters.RowCount = 0;
                    panel_horizontal_cylinder.Visible = false;
                    panel_sphere.Visible = false;
                    panel_vertical_cylinder.Visible = false;
                    panel_vertical_fault.Visible = false;

                    tx_Xo_horizontal_cylinder.Text = null;
                    tx_Xo_vertical_cylinder.Text = null;
                    tx_Xo_Sphere.Text = null;
                    tx_Xo_vertical_fault.Text = null;

                    tx_Yo_horizontal_cylinder.Text = null;
                    tx_Yo_vertical_cylinder.Text = null;
                    tx_Yo_Sphere.Text = null;
                    tx_Yo_vertical_fault.Text = null;

                    tx_Zo_horizontal_cylinder.Text = null;
                    tx_Zo_vertical_cylinder.Text = null;
                    tx_Zo_Sphere.Text = null;
                    tx_Zo_vertical_fault.Text = null;

                    tx_density_horizontal_cylinder.Text = null;
                    tx_density_vertical_cylinder.Text = null;
                    tx_density_sphere.Text = null;
                    tx_density_vertical_fault.Text = null;

                    tx_radius_horizontal_cylinder.Text = null;
                    tx_radius_vertical_cylinder.Text = null;
                    tx_Radius_Sphere.Text = null;
                    tx_t_vertical_fault.Text = null;
                    goto son;
                }

                btn_save.Enabled = false;
                btn_refresh.Enabled = false;

                if (tx_xnum.Text == "")
                {
                    MessageBox.Show("Please, enter the number of data in the x direction");
                    goto son;
                }
                else if (tx_ynum.Text == "")
                {
                    MessageBox.Show("Please, enter the number of data in the y direction");
                    goto son;
                }
                else if (tx_dy.Text == "")
                {
                    MessageBox.Show("Please, enter the sampling interval in the y direction");
                    goto son;
                }
                else if (tx_dx.Text == "")
                {
                    MessageBox.Show("Please, enter the sampling interval in the x direction");
                    goto son;
                }
                else if (tx_si.Text == "")
                {
                    MessageBox.Show("Please, enter the beta value");
                    goto son;
                }

                if (int.Parse(tx_xnum.Text) < 3 || int.Parse(tx_ynum.Text) < 3)
                {
                    MessageBox.Show("Numbers of stations must be greater than 3");
                    goto son;
                }


                if (cbox_criteria.Checked)
                {
                    if (tx_maxdepth.Text != null && tx_mindepth.Text != null)
                    {
                        maxdepth = Convert.ToDouble(tx_maxdepth.Text);
                        mindepth = Convert.ToDouble(tx_mindepth.Text);
                    }
                    else
                    {
                        MessageBox.Show("Please, enter the maximum and minimum depth values");
                        goto son;
                    }
                }

                numberofmass = Convert.ToInt32(nupdown_massnum.Value);

                dgparameters.ColumnCount = 1;
                dgparameters.RowCount = 1;

                dgparameters.AllowUserToAddRows = false;
                dgparameters.RowCount = numberofmass;
                dgparameters.RowHeadersVisible = false;
                dgparameters.Columns[0].HeaderText = "Source Masses";

                DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();
                com.Items.AddRange("Sphere", "Horizontal Cylinder", "Vertical Cylinder", "Vertical Fault");
                com.HeaderText = "Source Types";
                dgparameters.Columns.Add(com);

                DataGridViewColumn com2 = new DataGridViewColumn();
                com2.HeaderText = "Location-X (m)";
                com2.CellTemplate = new DataGridViewTextBoxCell();
                com2.ReadOnly = true;
                dgparameters.Columns.Add(com2);

                DataGridViewColumn com3 = new DataGridViewColumn();
                com3.HeaderText = "Location-Y (m)";
                com3.CellTemplate = new DataGridViewTextBoxCell();
                com3.ReadOnly = true;
                dgparameters.Columns.Add(com3);

                DataGridViewColumn com4 = new DataGridViewColumn();
                com4.HeaderText = "Depth (m)";
                com4.CellTemplate = new DataGridViewTextBoxCell();
                com4.ReadOnly = true;
                dgparameters.Columns.Add(com4);

                DataGridViewColumn com5 = new DataGridViewColumn();
                com5.HeaderText = "Radius or Height (m)";
                com5.CellTemplate = new DataGridViewTextBoxCell();
                com5.ReadOnly = true;
                dgparameters.Columns.Add(com5);

                DataGridViewColumn com6 = new DataGridViewColumn();
                com6.HeaderText = "Density Contrast (kg/m\u00b3)";
                com6.CellTemplate = new DataGridViewTextBoxCell();
                com6.ReadOnly = true;
                dgparameters.Columns.Add(com6);

                DataGridViewColumn com7 = new DataGridViewColumn();
                com7.CellTemplate = new DataGridViewTextBoxCell();
                com7.Visible = false;
                dgparameters.Columns.Add(com7);

                dgparameters.Columns[0].Width = 110;
                dgparameters.Columns[1].Width = 120;
                dgparameters.Columns[2].Width = 85;
                dgparameters.Columns[3].Width = 85;
                dgparameters.Columns[4].Width = 65;
                dgparameters.Columns[5].Width = 110;
                dgparameters.Columns[6].Width = 140;

                for (int i = 0; i < numberofmass; i++)
                {
                    dgparameters[0, i].Value = "Mass-" + (i + 1).ToString();
                }

                btn_calculate.Enabled = true;
                btn_parameters.Text = "CANCEL";

            son: ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please, check the entered parameters and try again");
            }
        }

        private void btn_ok_sphere_Click(object sender, EventArgs e)
        {
            if (tx_Xo_Sphere.Text != "" && tx_Yo_Sphere.Text != "" && tx_Zo_Sphere.Text != "" && tx_Radius_Sphere.Text != "" && tx_density_sphere.Text != "")
            {
                dgparameters.Rows[currentcellvar.Y].Cells[2].Value = tx_Xo_Sphere.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[3].Value = tx_Yo_Sphere.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[4].Value = tx_Zo_Sphere.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[5].Value = tx_Radius_Sphere.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[6].Value = tx_density_sphere.Text;
            }
        }

        private void btn_close_sphere_Click(object sender, EventArgs e)
        {
            panel_sphere.Visible = false;
        }

        private void rb_xdirec_horizantal_cylinder_CheckedChanged(object sender, EventArgs e)
        {
            tx_Xo_horizontal_cylinder.Enabled = false;
            tx_Yo_horizontal_cylinder.Enabled = true;
            tx_Xo_horizontal_cylinder.Text = "0";
            tx_Yo_horizontal_cylinder.Text = null;
            pbox_horizontal_cylinder.Image = CURVGRAV.Properties.Resources.horizontal_cylinder_model2;
        }

        private void rb_ydirec_horizantal_cylinder_CheckedChanged(object sender, EventArgs e)
        {
            tx_Xo_horizontal_cylinder.Enabled = true;
            tx_Yo_horizontal_cylinder.Enabled = false;
            tx_Yo_horizontal_cylinder.Text = "0";
            tx_Xo_horizontal_cylinder.Text = null;
            pbox_horizontal_cylinder.Image = CURVGRAV.Properties.Resources.horizontal_cylinder_model1;
        }

        private void pbox_horz_xdirec_Click(object sender, EventArgs e)
        {
            rb_ydirec_horizantal_cylinder.Checked = true;
        }

        private void pbox_horz_ydirec_Click(object sender, EventArgs e)
        {
            rb_xdirec_horizantal_cylinder.Checked = true;
        }

        private void dgparameters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgparameters.Rows[e.RowIndex].Cells[1].Value != null)
            {
                currentcellvar.X = 0;
                currentcellvar.Y = e.RowIndex;

                string sec = dgparameters.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (sec == "Sphere")
                {
                    panel_sphere.Visible = true;
                    panel_horizontal_cylinder.Visible = false;
                    panel_vertical_cylinder.Visible = false;
                    panel_vertical_fault.Visible = false;

                    string x = null;
                    string y = null;
                    string z = null;
                    string radius_thick = null;
                    string density = null;
                    if (dgparameters.Rows[e.RowIndex].Cells[2].Value != null) x = dgparameters.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[3].Value != null) y = dgparameters.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[4].Value != null) z = dgparameters.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[5].Value != null) radius_thick = dgparameters.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[6].Value != null) density = dgparameters.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (x != "") tx_Xo_Sphere.Text = x;
                    if (y != "") tx_Yo_Sphere.Text = y;
                    if (z != "") tx_Zo_Sphere.Text = z;
                    if (radius_thick != "") tx_Radius_Sphere.Text = radius_thick;
                    if (density != "") tx_density_sphere.Text = density;
                }
                else if (sec == "Horizontal Cylinder")
                {
                    panel_horizontal_cylinder.Visible = true;
                    panel_sphere.Visible = false;
                    panel_vertical_cylinder.Visible = false;
                    panel_vertical_fault.Visible = false;

                    string x = null;
                    string y = null;
                    string z = null;
                    string radius_thick = null;
                    string density = null;
                    if (dgparameters.Rows[e.RowIndex].Cells[2].Value != null) x = dgparameters.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[3].Value != null) y = dgparameters.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[4].Value != null) z = dgparameters.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[5].Value != null) radius_thick = dgparameters.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[6].Value != null) density = dgparameters.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (x != "") tx_Xo_horizontal_cylinder.Text = x;
                    if (y != "") tx_Yo_horizontal_cylinder.Text = y;
                    if (z != "") tx_Zo_horizontal_cylinder.Text = z;
                    if (radius_thick != "") tx_radius_horizontal_cylinder.Text = radius_thick;
                    if (density != "") tx_density_horizontal_cylinder.Text = density;

                    if (x == "0") rb_xdirec_horizantal_cylinder.Checked = true;
                    if (y == "0") rb_ydirec_horizantal_cylinder.Checked = true;

                    if (rb_xdirec_horizantal_cylinder.Checked)
                    { tx_Xo_horizontal_cylinder.Text = "0"; tx_Xo_horizontal_cylinder.Enabled = false; tx_Yo_horizontal_cylinder.Enabled = true; }
                    if (rb_ydirec_horizantal_cylinder.Checked)
                    { tx_Yo_horizontal_cylinder.Text = "0"; tx_Yo_horizontal_cylinder.Enabled = false; tx_Xo_horizontal_cylinder.Enabled = true; }
                }
                else if (sec == "Vertical Cylinder")
                {
                    panel_vertical_cylinder.Visible = true;
                    panel_horizontal_cylinder.Visible = false;
                    panel_sphere.Visible = false;
                    panel_vertical_fault.Visible = false;

                    string x = null;
                    string y = null;
                    string z = null;
                    string radius_thick = null;
                    string density = null;
                    if (dgparameters.Rows[e.RowIndex].Cells[2].Value != null) x = dgparameters.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[3].Value != null) y = dgparameters.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[4].Value != null) z = dgparameters.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[5].Value != null) radius_thick = dgparameters.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[6].Value != null) density = dgparameters.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (x != "") tx_Xo_vertical_cylinder.Text = x;
                    if (y != "") tx_Yo_vertical_cylinder.Text = y;
                    if (z != "") tx_Zo_vertical_cylinder.Text = z;
                    if (radius_thick != "") tx_radius_vertical_cylinder.Text = radius_thick;
                    if (density != "") tx_density_vertical_cylinder.Text = density;
                }
                else if (sec == "Vertical Fault")
                {
                    panel_vertical_cylinder.Visible = false;
                    panel_horizontal_cylinder.Visible = false;
                    panel_sphere.Visible = false;
                    panel_vertical_fault.Visible = true;

                    string x = null;
                    string y = null;
                    string z = null;
                    string radius_thick = null;
                    string density = null;
                    string faymod = "0";
                    if (dgparameters.Rows[e.RowIndex].Cells[2].Value != null) x = dgparameters.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[3].Value != null) y = dgparameters.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[4].Value != null) z = dgparameters.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[5].Value != null) radius_thick = dgparameters.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[6].Value != null) density = dgparameters.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (dgparameters.Rows[e.RowIndex].Cells[7].Value != null) faymod = dgparameters.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (x != "") tx_Xo_vertical_fault.Text = x;
                    if (y != "") tx_Yo_vertical_fault.Text = y;
                    if (z != "") tx_Zo_vertical_fault.Text = z;
                    if (radius_thick != "") tx_t_vertical_fault.Text = radius_thick;
                    if (density != "") tx_density_vertical_fault.Text = density;

                    if (x == "0") rb_vertical_fault_model1.Checked = true;
                    if (y == "0") rb_vertical_fault_model3.Checked = true;


                    if (faymod == "1") rb_vertical_fault_model1.Checked = true;
                    if (faymod == "2") rb_vertical_fault_model2.Checked = true;
                    if (faymod == "3") rb_vertical_fault_model3.Checked = true;
                    if (faymod == "4") rb_vertical_fault_model4.Checked = true;


                    if (rb_vertical_fault_model1.Checked || rb_vertical_fault_model2.Checked)
                    {
                        tx_Yo_vertical_fault.Text = "0"; tx_Yo_vertical_fault.Enabled = false; tx_Xo_vertical_fault.Enabled = true;
                        if (dgparameters.Rows[e.RowIndex].Cells[2].Value != null) tx_Xo_vertical_fault.Text = dgparameters.Rows[e.RowIndex].Cells[2].Value.ToString();
                    }
                    if (rb_vertical_fault_model3.Checked || rb_vertical_fault_model4.Checked)
                    {
                        tx_Xo_vertical_fault.Text = "0"; tx_Xo_vertical_fault.Enabled = false; tx_Yo_vertical_fault.Enabled = true;
                        if (dgparameters.Rows[e.RowIndex].Cells[2].Value != null) tx_Yo_vertical_fault.Text = dgparameters.Rows[e.RowIndex].Cells[3].Value.ToString();
                    }
                }
            }
        }

        private void dgparameters_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgparameters.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            currentcellvar = dgparameters.CurrentCellAddress;
            var sendingCB = sender as DataGridViewComboBoxEditingControl;
            string cell = sendingCB.EditingControlFormattedValue.ToString();

            if (cell.ToString() == "Sphere")
            {
                panel_sphere.Visible = true;
                panel_sphere.Location = new Point(3, 142);
                panel_horizontal_cylinder.Visible = false;
                panel_vertical_cylinder.Visible = false;
                panel_vertical_fault.Visible = false;
            }

            if (cell.ToString() == "Horizontal Cylinder")
            {
                panel_horizontal_cylinder.Visible = true;
                panel_horizontal_cylinder.Location = new Point(3, 142);
                panel_sphere.Visible = false;
                panel_vertical_cylinder.Visible = false;
                panel_vertical_fault.Visible = false;
            }

            if (cell.ToString() == "Vertical Cylinder")
            {
                panel_vertical_cylinder.Visible = true;
                panel_vertical_cylinder.Location = new Point(3, 142);
                panel_sphere.Visible = false;
                panel_horizontal_cylinder.Visible = false;
                panel_vertical_fault.Visible = false;
            }

            if (cell.ToString() == "Vertical Fault")
            {
                panel_vertical_fault.Visible = true;
                panel_vertical_fault.Location = new Point(3, 142);
                panel_sphere.Visible = false;
                panel_horizontal_cylinder.Visible = false;
                panel_vertical_cylinder.Visible = false;
            }
        }

        private void btn_close__horizontal_cylinder_Click(object sender, EventArgs e)
        {
            panel_horizontal_cylinder.Visible = false;
        }

        private void btn_ok_horizontal_cylinder_Click(object sender, EventArgs e)
        {
            if (tx_Xo_horizontal_cylinder.Text != "" && tx_Yo_horizontal_cylinder.Text != "" && tx_Zo_horizontal_cylinder.Text != "" && tx_radius_horizontal_cylinder.Text != "" && tx_density_horizontal_cylinder.Text != "")
            {
                if (rb_xdirec_horizantal_cylinder.Checked)
                {
                    dgparameters.Rows[currentcellvar.Y].Cells[2].Value = "0";
                    dgparameters.Rows[currentcellvar.Y].Cells[3].Value = tx_Yo_horizontal_cylinder.Text;
                }
                if (rb_ydirec_horizantal_cylinder.Checked)
                {
                    dgparameters.Rows[currentcellvar.Y].Cells[2].Value = tx_Xo_horizontal_cylinder.Text;
                    dgparameters.Rows[currentcellvar.Y].Cells[3].Value = "0";
                }

                dgparameters.Rows[currentcellvar.Y].Cells[4].Value = tx_Zo_horizontal_cylinder.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[5].Value = tx_radius_horizontal_cylinder.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[6].Value = tx_density_horizontal_cylinder.Text;
            }
        }

        private void btn_close_vertical_cylinder_Click(object sender, EventArgs e)
        {
            panel_vertical_cylinder.Visible = false;
        }

        private void btn_ok_vertical_cylinder_Click(object sender, EventArgs e)
        {
            if (tx_Xo_vertical_cylinder.Text != "" && tx_Yo_vertical_cylinder.Text != "" && tx_Zo_vertical_cylinder.Text != "" && tx_radius_vertical_cylinder.Text != "" && tx_density_vertical_cylinder.Text != "")
            {
                dgparameters.Rows[currentcellvar.Y].Cells[2].Value = tx_Xo_vertical_cylinder.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[3].Value = tx_Yo_vertical_cylinder.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[4].Value = tx_Zo_vertical_cylinder.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[5].Value = tx_radius_vertical_cylinder.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[6].Value = tx_density_vertical_cylinder.Text;
            }
        }

        private void rb_vertical_fault_model1_CheckedChanged(object sender, EventArgs e)
        {
            faultmodel = 1;
            tx_Yo_vertical_fault.Enabled = false;
            tx_Xo_vertical_fault.Enabled = true;
            tx_Yo_vertical_fault.Text = "0";
            tx_Xo_vertical_fault.Text = null;
            pbox_vertical_fault.Image = CURVGRAV.Properties.Resources.fault2;
        }

        private void rb_vertical_fault_model2_CheckedChanged(object sender, EventArgs e)
        {
            faultmodel = 2;
            tx_Yo_vertical_fault.Enabled = false;
            tx_Xo_vertical_fault.Enabled = true;
            tx_Yo_vertical_fault.Text = "0";
            tx_Xo_vertical_fault.Text = null;
            pbox_vertical_fault.Image = CURVGRAV.Properties.Resources.fault1;
        }

        private void rb_vertical_fault_model3_CheckedChanged(object sender, EventArgs e)
        {
            faultmodel = 3;
            tx_Yo_vertical_fault.Enabled = true;
            tx_Xo_vertical_fault.Enabled = false;
            tx_Xo_vertical_fault.Text = "0";
            tx_Yo_vertical_fault.Text = null;
            pbox_vertical_fault.Image = CURVGRAV.Properties.Resources.fault3;
        }

        private void rb_vertical_fault_model4_CheckedChanged(object sender, EventArgs e)
        {
            faultmodel = 4;
            tx_Yo_vertical_fault.Enabled = true;
            tx_Xo_vertical_fault.Enabled = false;
            tx_Xo_vertical_fault.Text = "0";
            tx_Yo_vertical_fault.Text = null;
            pbox_vertical_fault.Image = CURVGRAV.Properties.Resources.fault4;
        }

        private void pbox_vertical_fault_model1_Click(object sender, EventArgs e)
        {
            rb_vertical_fault_model1.Checked=true;
        }

        private void pbox_vertical_fault_model2_Click(object sender, EventArgs e)
        {
            rb_vertical_fault_model2.Checked = true;
        }

        private void pbox_vertical_fault_model3_Click(object sender, EventArgs e)
        {
            rb_vertical_fault_model3.Checked = true;
        }

        private void pbox_vertical_fault_model4_Click(object sender, EventArgs e)
        {
            rb_vertical_fault_model4.Checked = true;
        }

        private void btn_close_vertical_fault_Click(object sender, EventArgs e)
        {
            panel_vertical_fault.Visible = false;
        }

        private void btn_ok_vertical_fault_Click(object sender, EventArgs e)
        {
            if (tx_Xo_vertical_fault.Text != "" && tx_Yo_vertical_fault.Text != "" && tx_Zo_vertical_fault.Text != "" && tx_t_vertical_fault.Text != "" && tx_density_vertical_fault.Text != "")
            {
                if (rb_vertical_fault_model1.Checked)
                {
                    dgparameters.Rows[currentcellvar.Y].Cells[2].Value = tx_Xo_vertical_fault.Text;
                    dgparameters.Rows[currentcellvar.Y].Cells[3].Value = "0";
                    dgparameters.Rows[currentcellvar.Y].Cells[7].Value = "1";
                }
                if (rb_vertical_fault_model3.Checked)
                {
                    dgparameters.Rows[currentcellvar.Y].Cells[2].Value = "0";
                    dgparameters.Rows[currentcellvar.Y].Cells[3].Value = tx_Yo_vertical_fault.Text;
                    dgparameters.Rows[currentcellvar.Y].Cells[7].Value = "3";
                }
                if (rb_vertical_fault_model2.Checked)
                {
                    dgparameters.Rows[currentcellvar.Y].Cells[2].Value = tx_Xo_vertical_fault.Text;
                    dgparameters.Rows[currentcellvar.Y].Cells[3].Value = "0";
                    dgparameters.Rows[currentcellvar.Y].Cells[7].Value = "2";
                }
                if (rb_vertical_fault_model4.Checked)
                {
                    dgparameters.Rows[currentcellvar.Y].Cells[2].Value = "0";
                    dgparameters.Rows[currentcellvar.Y].Cells[3].Value = tx_Yo_vertical_fault.Text;
                    dgparameters.Rows[currentcellvar.Y].Cells[7].Value = "4";
                }

                dgparameters.Rows[currentcellvar.Y].Cells[4].Value = tx_Zo_vertical_fault.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[5].Value = tx_t_vertical_fault.Text;
                dgparameters.Rows[currentcellvar.Y].Cells[6].Value = tx_density_vertical_fault.Text;
            }
        }

        private void tx_Xo_vertical_fault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Yo_vertical_fault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Zo_vertical_fault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_t_vertical_fault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_density_vertical_fault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Xo_horizontal_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Yo_horizontal_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Zo_horizontal_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_radius_horizontal_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_density_horizontal_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Xo_vertical_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Yo_vertical_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Zo_vertical_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_radius_vertical_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_density_vertical_cylinder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Xo_Sphere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Yo_Sphere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Zo_Sphere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_Radius_Sphere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_density_sphere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void SyntheticApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            CURVGRAV.MainForm.Synthetic_App = null;
        }

        private void tx_xnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_dx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_ynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_dy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_si_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_maxdepth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_mindepth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void cbox_criteria_CheckedChanged(object sender, EventArgs e)
        {
            if (tx_maxdepth.Text != "")
            {
                maxdepth = Convert.ToDouble(tx_maxdepth.Text);
                if (tx_mindepth.Text == "")
                { tx_mindepth.Text = "0"; mindepth = 0; }
                criticalpoint_limit();
            }
            else
            {
                //MessageBox.Show("Please, enter the maximum and mininmum depths");
                cbox_criteria.Checked = false;
            }
        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                panel_vertical_cylinder.Visible = false;
                panel_sphere.Visible = false;
                panel_horizontal_cylinder.Visible = false;
                panel_vertical_fault.Visible = false;

                read_map_settings();

                if (tx_si.Text == "")
                {
                    MessageBox.Show("Please, enter the beta value");
                    goto son;
                }

                if (tx_si.Text != "")
                {
                    beta = Convert.ToDouble(tx_si.Text);
                }

                parameters = new double[numberofmass, 8];
                bool ctlr = true;

                for (int i = 0; i < numberofmass; i++)
                {
                    parameters[i, 0] = (i + 1);
                    if (dgparameters[1, i].Value == null) ctlr = false;
                    else
                    {
                        string source = dgparameters[1, i].Value.ToString();
                        if (source == "Sphere") parameters[i, 1] = 1;
                        if (source == "Horizontal Cylinder") parameters[i, 1] = 2;
                        if (source == "Vertical Cylinder") parameters[i, 1] = 3;
                        if (source == "Vertical Fault") parameters[i, 1] = 4;
                    }
                    if (dgparameters[2, i].Value == null) ctlr = false;
                    else parameters[i, 2] = Convert.ToDouble(dgparameters[2, i].Value);//Xo 
                    if (dgparameters[3, i].Value == null) ctlr = false;
                    else parameters[i, 3] = Convert.ToDouble(dgparameters[3, i].Value);//Yo
                    if (dgparameters[4, i].Value == null) ctlr = false;
                    else parameters[i, 4] = Convert.ToDouble(dgparameters[4, i].Value);//Zo
                    if (dgparameters[5, i].Value == null) ctlr = false;
                    else parameters[i, 5] = Convert.ToDouble(dgparameters[5, i].Value);//Radius or height
                    if (dgparameters[6, i].Value == null) ctlr = false;
                    else parameters[i, 6] = Convert.ToDouble(dgparameters[6, i].Value);//density

                    parameters[i, 7] = Convert.ToDouble(dgparameters[7, i].Value);//faultmodel               
                }

                if (!ctlr)
                {
                    MessageBox.Show("Please, enter the all parameters ");
                    goto son;
                }

                nc = Convert.ToInt32(tx_xnum.Text);
                nr = Convert.ToInt32(tx_ynum.Text);
                dx = Convert.ToDouble(tx_dx.Text);
                dy = Convert.ToDouble(tx_dy.Text);

                progressBar1.Minimum = 0;
                progressBar1.Maximum = (nc) * (nr) * (numberofmass) + (nr - 2) * (nc - 2);
                progressBar1.Value = 0;

                anomaly = new double[nc, nr];

                Km = new double[nc, nr];
                Kg = new double[nc, nr];
                Kmax = new double[nc, nr];
                Kmin = new double[nc, nr];
                Kn = new double[nc, nr];

                double xa, ya, za, radius, density;

                for (int k = 0; k < numberofmass; k++)
                {
                    xa = parameters[k, 2];
                    ya = parameters[k, 3];
                    za = parameters[k, 4];
                    radius = parameters[k, 5];
                    density = parameters[k, 6];
                    double source = parameters[k, 1];
                    double direction = parameters[k, 7];

                    if (source == 1) calc_sphere(xa, ya, za, radius, density);
                    if (source == 2) calc_horizontal_cylinder(xa, ya, za, radius, density);
                    if (source == 3) calc_vertical_cylinder(xa, ya, za, radius, density);
                    if (source == 4) calc_vertical_fault(xa, ya, za, radius, density, direction);
                }

                dgcriticalpoint.ColumnCount = 4;
                dgcriticalpoint.RowCount = 1;
                dgcriticalpoint.RowHeadersVisible = false;
                dgcriticalpoint.ColumnHeadersVisible = false;
                dgcriticalpoint.AllowUserToAddRows = false;
                dgcriticalpoint.RowCount++;
                dgcriticalpoint[0, 0].Value = "Xo";
                dgcriticalpoint[1, 0].Value = "Yo";
                dgcriticalpoint[2, 0].Value = "Zo";
                dgcriticalpoint[3, 0].Value = "Dominant Elongation";
                dgcriticalpoint.Columns[3].Width = 150;
                dgcriticalpoint.Rows[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                dgcriticalpoint.Rows[0].DefaultCellStyle.ForeColor = Color.White;

                dgextremum.ColumnCount = 4;
                dgextremum.RowCount = 1;
                dgextremum.RowHeadersVisible = false;
                dgextremum.ColumnHeadersVisible = false;
                dgextremum.AllowUserToAddRows = false;
                dgextremum.RowCount++;
                dgextremum[0, 0].Value = "Xe";
                dgextremum[1, 0].Value = "Ye";
                dgextremum[2, 0].Value = "Zo";
                dgextremum[3, 0].Value = "Extremum Type";
                dgextremum.Columns[3].Width = 150;
                dgextremum.Rows[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                dgextremum.Rows[0].DefaultCellStyle.ForeColor = Color.White;

                if (cbox_usehgm.Checked)
                {//Use HGM
                    calc_criticalpoint_and_extreme(true);
                }
                else
                {//Use Anomaly 
                    calc_criticalpoint_and_extreme(false);
                }

                //Anomaly Maps
                //Anomaly
                plot_anomaly.Model = Drawing("(m)", numcolmap, fontsize, anomaly);
                plot_hgm.Model = Drawing("(m)", numcolmap, fontsize, hgm);

                zcrit = new double[dgcriticalpoint.Rows.Count];
                xcrit = new double[dgcriticalpoint.Rows.Count];
                ycrit = new double[dgcriticalpoint.Rows.Count];
                statecrit = new string[dgcriticalpoint.Rows.Count];

                zecrit = new double[dgextremum.Rows.Count];
                xecrit = new double[dgextremum.Rows.Count];
                yecrit = new double[dgextremum.Rows.Count];

                for (int i = 1; i < dgcriticalpoint.Rows.Count; i++)
                {
                    if (dgcriticalpoint[0, i].Value != null)
                    {
                        xcrit[i - 1] = Convert.ToDouble(dgcriticalpoint[0, i].Value.ToString());
                        ycrit[i - 1] = Convert.ToDouble(dgcriticalpoint[1, i].Value.ToString());
                        zcrit[i - 1] = Convert.ToDouble(dgcriticalpoint[2, i].Value.ToString());
                        statecrit[i - 1] = dgcriticalpoint[3, i].Value.ToString();
                    }
                }

                for (int i = 1; i < dgextremum.Rows.Count; i++)
                {
                    if (dgextremum[0, i].Value != null)
                    {
                        xecrit[i - 1] = Convert.ToDouble(dgextremum[0, i].Value.ToString());
                        yecrit[i - 1] = Convert.ToDouble(dgextremum[1, i].Value.ToString());
                        zecrit[i - 1] = Convert.ToDouble(dgextremum[2, i].Value.ToString());
                    }
                }

                plot_solutions.Model = ScatterDrawing("(m)", numcolmap, fontsize, xcrit, ycrit, zcrit);

                SuperimposeCriticalDrawing("(m)", numcolmap, fontsize, xcrit, ycrit, zcrit, anomaly);

                plot_extreme_points.Model = ScatterDrawing("(m)", numcolmap, fontsize, xecrit, yecrit, zecrit);

                SuperimposeExtremeDrawing("(m)", numcolmap, fontsize, xecrit, yecrit, zecrit, anomaly);

                plot_maximum_curvature.Model = Drawing("(m)", numcolmap, fontsize, Kmax);
                plot_minimum_curvature.Model = Drawing("(m)", numcolmap, fontsize, Kmin);
                plot_curvedness.Model = Drawing("(m)", numcolmap, fontsize, Kn);

                plot_mean_curvature.Model = Drawing("(m)", numcolmap, fontsize, Km);
                plot_gaussian_curvature.Model = Drawing("(m)", numcolmap, fontsize, Kg);

                Pm1 = plot_anomaly.Model;
                Pm2 = plot_hgm.Model;
                Pm3 = plot_solutions.Model;
                Pm4 = plot_anomaly_solutions.Model;
                Pm5 = plot_extreme_points.Model;
                Pm6 = plot_extreme_anomaly.Model;
                Pm7 = plot_maximum_curvature.Model;
                Pm8 = plot_minimum_curvature.Model;
                Pm9 = plot_curvedness.Model;

                //if calculation success
                cbox_ridge.Checked = true;
                cbox_trough.Checked = true;
                btn_save.Enabled = true;
                btn_refresh.Enabled = true;
                grp_box_depth_criterion.Enabled = true;
                grpbox_critical_points.Enabled = true;
            //btn_calculate.Enabled = false;
            son: ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please, check the entered parameters and try again");
            }
        }

        private void calc_criticalpoint_and_extreme(bool usehgm)
        {
            double x0=0;
            double y0 = 0;
            double zz=0;
            double[] b = new double[10];
            double[] d = new double[7];
            hgm = new double[nc, nr];
            int count1 = 1;
            int count2 = 1;

            double xmax = (nc - 1) * dx;
            double ymax = (nr - 1) * dy;


            if (usehgm)
            {
                //HGM calculating
                for (int j = 1; j < nr - 1; j++)
                {
                    double ycc = j * dy;
                    for (int i = 1; i < nc - 1; i++)
                    {
                        Application.DoEvents();
                        double xcc = i * dx;
                        b[0] = anomaly[i - 1, j - 1];
                        b[1] = anomaly[i, j - 1];
                        b[2] = anomaly[i + 1, j - 1];
                        b[3] = anomaly[i - 1, j];
                        b[4] = anomaly[i, j];
                        b[5] = anomaly[i + 1, j];
                        b[6] = anomaly[i - 1, j + 1];
                        b[7] = anomaly[i, j + 1];
                        b[8] = anomaly[i + 1, j + 1];

                        d[1] = (b[2] + b[5] + b[8] - (b[0] + b[3] + b[6])) / (6 * dx);
                        d[2] = (b[6] + b[7] + b[8] - (b[0] + b[1] + b[2])) / (6 * dy);

                        hgm[i, j] = Math.Sqrt(Math.Pow(d[1], 2) + Math.Pow(d[2], 2));

                        if (i == 1) hgm[i - 1, j] = hgm[i, j];
                        if (i == nc - 2) hgm[i + 1, j] = hgm[i, j];
                        if (j == 1) hgm[i, j - 1] = hgm[i, j];
                        if (j == nr - 2) hgm[i, j + 1] = hgm[i, j];
                    }
                }

                hgm[0, 0] = hgm[1, 0];
                hgm[nc - 1, 0] = hgm[nc - 2, 0];

                hgm[0, nr - 1] = hgm[0, 1];
                hgm[nc - 1, nr - 1] = hgm[nc - 1, nr - 2];
            }


            for (int j = 1; j < nr - 1; j++)
            {
                double ycc = j * dy;
                for (int i = 1; i < nc - 1; i++)
                {
                    Application.DoEvents();
                    progressBar1.Value++;
                    lbl_progress.Text = "%"+(progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                    double xcc = i * dx;

                    if (!usehgm)
                    {
                        b[0] = anomaly[i - 1, j - 1];
                        b[1] = anomaly[i, j - 1];
                        b[2] = anomaly[i + 1, j - 1];
                        b[3] = anomaly[i - 1, j];
                        b[4] = anomaly[i, j];
                        b[5] = anomaly[i + 1, j];
                        b[6] = anomaly[i - 1, j + 1];
                        b[7] = anomaly[i, j + 1];
                        b[8] = anomaly[i + 1, j + 1];
                    }
                    else if(usehgm)
                    {
                        b[0] = hgm[i - 1, j - 1];
                        b[1] = hgm[i, j - 1];
                        b[2] = hgm[i + 1, j - 1];
                        b[3] = hgm[i - 1, j];
                        b[4] = hgm[i, j];
                        b[5] = hgm[i + 1, j];
                        b[6] = hgm[i - 1, j + 1];
                        b[7] = hgm[i, j + 1];
                        b[8] = hgm[i + 1, j + 1];                    
                    }

                    d[0] = (5.0 * b[4] + 2.0 * (b[1] + b[3] + b[5] + b[7]) - (b[0] + b[2] + b[6] + b[8])) / 9.0;
                    d[1] = (b[2] + b[5] + b[8] - (b[0] + b[3] + b[6])) / (6 * dx);
                    d[2] = (b[6] + b[7] + b[8] - (b[0] + b[1] + b[2])) / (6 * dy);
                    d[3] = (b[0] + b[2] + b[3] + b[5] + b[6] + b[8] - 2.0 * (b[1] + b[4] + b[7])) / (6 * dx * dx);
                    d[4] = (b[0] - b[2] - b[6] + b[8]) / (4 * dx * dy);
                    d[5] = (b[0] + b[1] + b[2] + b[6] + b[7] + b[8] - 2.0 * (b[3] + b[4] + b[5])) / (6 * dy * dy);

                    if (!usehgm)
                    {
                        hgm[i, j] = Math.Sqrt(Math.Pow(d[1], 2) + Math.Pow(d[2], 2));
                    }

                    //Eigenvalues
                    double aa = d[3];
                    double bb = d[5];
                    double cc = d[4];
                    double dd = d[1];
                    double ee = d[2];


                    Km[i, j] = (aa * (1 + ee * ee) + bb * (1 + dd * dd) - cc * dd * ee) / Math.Pow(1 + dd * dd + ee * ee, 1.5);
                    Kg[i, j] = (4 * aa * bb - cc * cc) / Math.Pow(1 + dd * dd + ee * ee, 2);
                    Kmax[i, j] = Km[i, j] + Math.Sqrt(Km[i, j] * Km[i, j] - Kg[i, j]);
                    Kmin[i, j] = Km[i, j] - Math.Sqrt(Km[i, j] * Km[i, j] - Kg[i, j]);
                    Kn[i, j] = Math.Sqrt((Kmax[i, j] * Kmax[i, j] + Kmin[i, j] * Kmin[i, j]) / 2);

                    double bbb = d[3] + d[5];
                    double term = Math.Pow(d[3] - d[5], 2) + Math.Pow(d[4], 2);
                    double e1 = bbb + Math.Sqrt(term);
                    double e2 = bbb - Math.Sqrt(term);

                    double xx1 = 1;
                    double yy1 = 0;
                    if (d[4] != 0)
                        yy1 = (e1 - 2.0 * d[3]) / d[4];
                    else if (e1 != 2.0 * d[5])
                        yy1 = d[4] / (e1 - 2.0 * d[5]);
                    else
                        yy1 = Math.Pow(10, 38);

                    double xx2 = 1;
                    double yy2 = 0;
                    if (d[4] != 0)
                        yy2 = (e2 - 2.0 * d[3]) / d[4];
                    else if (e2 != 2.0 * d[5])
                        yy2 = d[4] / (e2 - 2.0 * d[5]);
                    else
                        yy2 = Math.Pow(10, 38);

                    if (yy1 == Math.Pow(10, 38))
                    {
                        xx1 = 0; yy1 = 1;
                    }
                    if (yy2 == Math.Pow(10, 38))
                    {
                        xx2 = 0; yy2 = 1;
                    }

                    //Critical Points
                    if (Math.Abs(e1) == Math.Abs(e2))
                    {
                        //no critical point
                        goto a500;
                    }
                    else if (Math.Abs(e1) > Math.Abs(e2))
                    {
                        double den = (d[3] * xx1 * xx1 + d[4] * xx1 * yy1 + d[5] * yy1 * yy1);
                        if (den == 0)
                        {
                            //no critical point
                            goto a500;
                        }
                        else
                        {
                            double t0 = -0.5 * (d[1] * xx1 + d[2] * yy1) / den;
                            x0 = xx1 * t0;
                            y0 = yy1 * t0;
                        }
                    }
                    else
                    {
                        double den = (d[3] * xx2 * xx2 + d[4] * xx2 * yy2 + d[5] * yy2 * yy2);
                        if (den == 0)
                        {
                            //no critical point
                            goto a500;
                        }
                        else
                        {
                            double t0 = -0.5 * (d[1] * xx2 + d[2] * yy2) / den;
                            x0 = xx2 * t0;
                            y0 = yy2 * t0;
                        }
                    }

                    if (x0 ==0 && y0==0) goto a1000;
                    if (x0 < -dx / 2.0 || x0 > dx / 2.0) goto a500;
                    if (y0 < -dy / 2.0 || y0 > dy / 2.0) goto a500;

                    double g0 = d[0] + d[1] * x0 + d[2] * y0 + d[3] * x0 * x0 + d[4] * x0 * y0 + d[5] * y0 * y0;

                    int sta = 0;

                    if (e1 > 0 && e1 > Math.Abs(e2))
                    {
                        //trough, =2
                        sta = 2;
                    }
                    else if (e2 > 0 && e2 > Math.Abs(e1))
                    {
                        //trough, =2
                        sta = 2;
                    }
                    else if (e1 < 0 && Math.Abs(e1) > Math.Abs(e2))
                    {
                        //ridge
                        sta = 1;
                    }
                    else if (e2 < 0 && Math.Abs(e2) > Math.Abs(e1))
                    {
                        //ridge
                        sta = 1;
                    }

                    double rat = 0;
                    //maximum curvature
                    if (Math.Abs(e1) > Math.Abs(e2))
                        rat = yy1 / xx1;
                    else
                        rat = yy2 / xx2;

                    double ak = 2.0 * (d[3] + (d[4] + d[5] * rat) * rat);
                    zz = -2.0 * beta * g0 / e2;

                    if (zz >= 0 && !Double.IsInfinity(zz))
                    {
                        zz = -Math.Sqrt(zz);
                        //zzzcrit[count1] = zz;
                    }
                    else
                        goto a500;

                    dgcriticalpoint.RowCount++;
                    dgcriticalpoint[0, count1].Value = x0 + xcc;
                    dgcriticalpoint[1, count1].Value = y0 + ycc;
                    dgcriticalpoint[2, count1].Value = zz;
                    if (sta == 2)
                        dgcriticalpoint[3, count1].Value = "trough";
                    if (sta == 1)
                        dgcriticalpoint[3, count1].Value = "ridge";

                    count1++;

                    //Find extremum
                a500: ;

                    double xe, ye = 0;

                    double den1 = Math.Pow(d[4], 2) - 4.0 * d[3] * d[5];
                    if (den1 == 0)
                        //no extremum
                        goto a1000;
                    else
                    {
                        xe = (2 * d[5] * d[1] - d[2] * d[4]) / den1;
                        ye = (2 * d[3] * d[2] - d[4] * d[1]) / den1;
                        if (xe < -dx / 2.0 || xe > dx / 2.0) goto a1000;
                        if (ye < -dy / 2.0 || ye > dy / 2.0) goto a1000;

                        double ge = d[0] + d[1] * xe + d[2] * ye + d[3] * xe * xe + d[4] * xe * ye + d[5] * ye * ye;

                        zz = -2.0 * beta * ge / e2;

                        if (zz >= 0 && !Double.IsInfinity(zz))
                        {
                            zz = -Math.Sqrt(zz);
                            //zzzextr[count2] = zz;
                        }
                        else
                            goto a1000;

                        sta = 0;

                        if (e1 < 0 && e2 < 0)
                        {
                            //high, ;3
                            sta = 3;
                        }
                        else if (e1 > 0 && e2 > 0)
                        {
                            //low, ;4
                            sta = 4;
                        }
                        else if (e1 * e2 < 0)
                        {
                            //saddle, ;5
                            sta = 5;
                        }
                    }

                    if (xe + xcc < x0 || xe + xcc > xmax) goto a1000;
                    if (ye + ycc < y0 || ye + ycc > ymax) goto a1000;

                    dgextremum.RowCount++;
                    dgextremum[0, count2].Value = x0 + xcc;
                    dgextremum[1, count2].Value = y0 + ycc;
                    dgextremum[2, count2].Value = zz;
                    if (sta == 3)
                        dgextremum[3, count2].Value = "high";
                    if (sta == 4)
                        dgextremum[3, count2].Value = "low";
                    if (sta == 5)
                        dgextremum[3, count2].Value = "saddle";
                    count2++;

                a1000: ;
                }
            }

        }
     

        private void calc_sphere(double xa, double ya, double za, double radius, double density)
        {
            double xdeg = 0;
            for (int i = 0; i < nc; i++)
            {
                double ydeg = 0;
                for (int j = 0; j < nr; j++)
                {
                    Application.DoEvents();
                    double deger = 100000 * (6.67 * Math.Pow(10, -11) * (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3) * za * density) / (Math.Pow(Math.Pow(xdeg - xa, 2) + Math.Pow(ydeg - ya, 2) + Math.Pow(za, 2), 1.5));
                    anomaly[i, j] += deger;
                    ydeg += dy;
                    progressBar1.Value++;
                    lbl_progress.Text = "%"+(progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                }
                xdeg += dx;
            }
            
        }

        private void calc_horizontal_cylinder(double xa, double ya, double za, double radius, double density)
        {
            double xdeg = 0;
            for (int i = 0; i < nc; i++)
            {
                double ydeg = 0;
                for (int j = 0; j < nr; j++)
                {
                    Application.DoEvents();
                    double deger = 0;
                    if (ya != 0)
                        deger = 100000 * (6.67 * Math.Pow(10, -11) * 2 * Math.PI * Math.Pow(radius, 2) * za * density) / (Math.Pow(ydeg - ya, 2) + Math.Pow(za, 2));
                    else if (xa != 0)
                        deger = 100000 * (6.67 * Math.Pow(10, -11) * 2 * Math.PI * Math.Pow(radius, 2) * za * density) / (Math.Pow(xdeg - xa, 2) + Math.Pow(za, 2));

                    anomaly[i, j] += deger;
                    ydeg += dy;
                    progressBar1.Value++;
                    lbl_progress.Text = "%"+(progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                }
                xdeg += dx;
            }
        }

        private void calc_vertical_cylinder(double xa, double ya, double za, double radius, double density)
        {
            double xdeg = 0;
            for (int i = 0; i < nc; i++)
            {
                double ydeg = 0;
                for (int j = 0; j < nr; j++)
                {
                    Application.DoEvents();
                    double deger = 100000 * (6.67 * Math.Pow(10, -11) * Math.PI * Math.Pow(radius, 2) * density) / (Math.Pow(Math.Pow(xdeg - xa, 2) + Math.Pow(ydeg - ya, 2) + Math.Pow(za, 2), 0.5));
                    anomaly[i, j] += deger;
                    ydeg += dy;
                    progressBar1.Value++;
                    lbl_progress.Text = "%"+(progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                }
                xdeg += dx;
            }
        }

        private void calc_vertical_fault(double xa, double ya, double za, double radius, double density,double direction)
        {
            if (direction==1)
            {
                double xdeg = 0;
                for (int i = 0; i < nc; i++)
                {
                    double ydeg = 0;
                    for (int j = 0; j < nr; j++)
                    {
                        Application.DoEvents();
                        double deger = 0;
                        if (ya != 0)
                        {
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius;
                            deger = carpan * (Math.PI / 2 + Math.Atan((ydeg - ya) / za));
                        }
                        else if (xa != 0)
                        {
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((xdeg - xa) / za));
                        }
                        anomaly[i, j] += deger;
                        ydeg += dy;
                        progressBar1.Value++;
                        lbl_progress.Text = "%"+(progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                    }
                    xdeg += dx;
                }
            }

            if (direction==2)
            {
                
                
                double xdeg = 0;
                for (int i = nc-1; i >= 0; i--)
                {
                    double ydeg = 0;
                    for (int j = nr-1; j >=0 ; j--)
                    {
                        double deger = 0;
                        if (ya != 0)
                        {
                            double yaa = nr * dy - ya;
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((ydeg - yaa) / za));
                        }
                        else if (xa != 0)
                        {
                            double xaa = nc * dx - xa;
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((xdeg - xaa) / za));

                        }
                        anomaly[i, j] += deger;
                        ydeg += dy;
                        progressBar1.Value++;
                        lbl_progress.Text = "%" + (progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                    }
                    xdeg += dx;
                }
            }

            if (direction==3)
            {
                double xdeg = 0;
                for (int i = 0; i < nc; i++)
                {
                    double ydeg = 0;
                    for (int j = 0; j < nr; j++)
                    {
                        double deger = 0;
                        if (ya != 0)
                        {
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((ydeg - ya) / za));
                        }
                        else if (xa != 0)
                        {
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((xdeg - xa) / za));

                        }
                        anomaly[i, j] += deger;
                        ydeg += dy;
                        progressBar1.Value++;
                        lbl_progress.Text = "%" + (progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                    }
                    xdeg += dx;
                }
            }

            if (direction==4)
            {
                double xdeg = 0;
                for (int i = nc - 1; i >= 0; i--)
                {
                    double ydeg = 0;
                    for (int j = nr - 1; j >= 0; j--)
                    {
                        double deger = 0;
                        if (ya != 0)
                        {
                            double yaa = nr * dy - ya;
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((ydeg - yaa) / za));
                        }
                        else if (xa != 0)
                        {
                            double xaa = nc * dx - xa;
                            double carpan = 2 * 6.67 * Math.Pow(10, -11) * density * radius ;
                            deger = carpan * (Math.PI / 2 + Math.Atan((xdeg - xaa) / za));

                        }
                        anomaly[i, j] += deger;
                        ydeg += dy;
                        progressBar1.Value++;
                        lbl_progress.Text = "%" + (progressBar1.Value * 100 / progressBar1.Maximum).ToString();
                    }
                    xdeg += dx;
                }
            }                 
        }

        private PlotModel Drawing(string unit, int numcolmap, int fontsize,double[,] anom)
        {
            var pm = new PlotModel();
            var lca1 = new OxyPlot.Axes.LinearColorAxis { Palette = OxyPalettes.Jet(numcolmap) };
            lca1.Position = OxyPlot.Axes.AxisPosition.Right;
            lca1.FontSize = fontsize;

            var la1 = new OxyPlot.Axes.LinearAxis();
            la1.Position = OxyPlot.Axes.AxisPosition.Bottom;
            la1.FontSize = fontsize;
            la1.Title = "X " + unit;
            la1.Minimum = 0;
            la1.Maximum = (nc - 1) * dx;

            var lb1 = new OxyPlot.Axes.LinearAxis();
            lb1.Position = OxyPlot.Axes.AxisPosition.Left;
            lb1.FontSize = fontsize;
            lb1.Title = "Y " + unit;
            lb1.Minimum = 0;
            lb1.Maximum = (nr - 1) * dy;

            pm.Axes.Add(lca1);
            pm.Axes.Add(la1);
            pm.Axes.Add(lb1);

            OxyPlot.Series.HeatMapSeries hm = new OxyPlot.Series.HeatMapSeries();
            hm.X0 = 0; hm.X1 = (nc - 1) * dx; hm.Y0 = 0; hm.Y1 = (nr - 1) * dy;
            hm.Interpolate = true;
            hm.Data = (Double[,])anom;
            pm.Series.Add(hm);

            return pm;
        }

        private PlotModel ScatterDrawing(string unit, int numcolmap, int fontsize, double[] xpoints,double[] ypoints,double[] zpoints)
        {            
            var pm2 = new PlotModel();
            var lca3 = new OxyPlot.Axes.LinearColorAxis { Palette = OxyPalettes.Jet(numcolscatter) };
            lca3.Position = OxyPlot.Axes.AxisPosition.Right;
            lca3.FontSize = fontsize;

            var la3 = new OxyPlot.Axes.LinearAxis();
            la3.Position = OxyPlot.Axes.AxisPosition.Bottom;
            la3.FontSize = fontsize;
            la3.Title = "X " + unit;
            la3.Minimum = 0;
            la3.Maximum = (nc - 1) * dx;

            var lb3 = new OxyPlot.Axes.LinearAxis();
            lb3.Position = OxyPlot.Axes.AxisPosition.Left;
            lb3.FontSize = fontsize;
            lb3.Title = "Y " + unit;
            lb3.Minimum = 0;
            lb3.Maximum = (nr - 1) * dy;

            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            scatterSeries.BinSize = numcolscatter;
            scatterSeries.MarkerStroke = OxyColors.Black;
            scatterSeries.MarkerStrokeThickness = markerstroke;
            scatterSeries.MarkerType = markertip(markertype);
            pm2.Axes.Add(la3);
            pm2.Axes.Add(lb3);

            int verisay = zpoints.Length;
            for (int i = 0; i < verisay; i++)
            {
                double xp =xpoints[i];
                double yp =ypoints[i];
                double zp = -zpoints[i];
                var sizem = Convert.ToInt32(zp);
                var colorValue = zp;

                if (cbox_criteria.Checked)
                {
                    if (zp <= maxdepth && zp >= mindepth)
                        scatterSeries.Points.Add(new OxyPlot.Series.ScatterPoint(xp, yp, markersize, colorValue));
                }
                else
                {
                    scatterSeries.Points.Add(new OxyPlot.Series.ScatterPoint(xp, yp, markersize, colorValue));
                }
            }

            pm2.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(numcolscatter), FontSize = fontsize });
            pm2.Series.Add(scatterSeries);
            return pm2;
        }
        
        private void SuperimposeCriticalDrawing(string unit, int numcolmap, int fontsize, double[] xpoints, double[] ypoints, double[] zpoints,double[,] anom)
        {
            int cnut = 0;
            zzcrit = new double[zcrit.Length];
            yycrit = new double[zcrit.Length];
            xxcrit = new double[zcrit.Length];
            for (int i = 0; i < zcrit.Length - 1; i++)
            {

                if (cbox_ridge.Checked && cbox_trough.Checked)
                {
                    //ridge and trough
                    zzcrit[cnut] = Math.Abs(zcrit[i]);
                    xxcrit[cnut] = xcrit[i];
                    yycrit[cnut] = ycrit[i];
                    cnut++;
                }
                else if (cbox_ridge.Checked && !cbox_trough.Checked)
                {
                    //ridge
                    if (statecrit[i]!=null)
                    if (statecrit[i].ToString() == "ridge")
                    {
                        zzcrit[cnut] = Math.Abs(zcrit[i]);
                        xxcrit[cnut] = xcrit[i];
                        yycrit[cnut] = ycrit[i];
                        cnut++;
                    }
                }
                else if (!cbox_ridge.Checked && cbox_trough.Checked)
                {
                    //trough
                    if (statecrit[i] != null)
                    if (statecrit[i].ToString() == "trough")
                    {
                        zzcrit[cnut] = Math.Abs(zcrit[i]);
                        xxcrit[cnut] = xcrit[i];
                        yycrit[cnut] = ycrit[i];
                        cnut++;
                    }
                }
            }

            double[,] anomaly2 = new double[nc, nr];
            double kat = 1;
            if (zzcrit.Length > 0)
            {
                double maxval = zzcrit.Max();
                double maxval2 = anomaly.Max2D();
                
                if (maxval > maxval2)
                {
                    kat = maxval / maxval2;
                }
            }
            for (int j = 0; j < nr; j++)
            {
                for (int i = 0; i < nc; i++)
                {
                    anomaly2[i, j] = anomaly[i, j] * kat;
                }
            }

            var pm = new PlotModel();
            var lca1 = new OxyPlot.Axes.LinearColorAxis { Palette = OxyPalettes.Jet(numcolmap) };
            lca1.Position = OxyPlot.Axes.AxisPosition.Right;
            lca1.FontSize = fontsize;

            var la1 = new OxyPlot.Axes.LinearAxis();
            la1.Position = OxyPlot.Axes.AxisPosition.Bottom;
            la1.FontSize = fontsize;
            la1.Title = "X " + unit;
            la1.Minimum = 0;
            la1.Maximum = (nc - 1) * dx;

            var lb1 = new OxyPlot.Axes.LinearAxis();
            lb1.Position = OxyPlot.Axes.AxisPosition.Left;
            lb1.FontSize = fontsize;
            lb1.Title = "Y " + unit;
            lb1.Minimum = 0;
            lb1.Maximum = (nr - 1) * dy;

            pm.Axes.Add(lca1);
            pm.Axes.Add(la1);
            pm.Axes.Add(lb1);

            OxyPlot.Series.HeatMapSeries hm = new OxyPlot.Series.HeatMapSeries();
            hm.X0 = 0; hm.X1 = (nc - 1) * dx; hm.Y0 = 0; hm.Y1 = (nr - 1) * dy;
            hm.Interpolate = true;
            hm.Data = (Double[,])anomaly2;
            pm.Series.Add(hm);

            var la3 = new OxyPlot.Axes.LinearAxis();
            la3.Position = OxyPlot.Axes.AxisPosition.Bottom;
            la3.FontSize = fontsize;
            la3.Title = "X " + unit;
            la3.Minimum = 0;
            la3.Maximum = (nc - 1) * dx;

            var lb3 = new OxyPlot.Axes.LinearAxis();
            lb3.Position = OxyPlot.Axes.AxisPosition.Left;
            lb3.FontSize = fontsize;
            lb3.Title = "Y " + unit;
            lb3.Minimum = 0;
            lb3.Maximum = (nr - 1) * dy;

            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            scatterSeries.BinSize = numcolscatter;
            scatterSeries.MarkerStroke = OxyColors.Black;
            scatterSeries.MarkerStrokeThickness = markerstroke;
            scatterSeries.MarkerType = markertip(markertype);
            pm.Axes.Add(la3);
            pm.Axes.Add(lb3);

            int verisay = zpoints.Length;
            for (int i = 0; i < verisay; i++)
            {
                double xp =xpoints[i];
                double yp =ypoints[i];
                double zp = -zpoints[i];
                var sizem = Convert.ToInt32(zp);
                var colorValue = zp;

                if (cbox_criteria.Checked)
                {
                    if (zp <= maxdepth && zp >= mindepth)
                        scatterSeries.Points.Add(new OxyPlot.Series.ScatterPoint(xp, yp, markersize, colorValue));
                }
                else
                {
                    scatterSeries.Points.Add(new OxyPlot.Series.ScatterPoint(xp, yp, markersize, colorValue));
                }
            }

                pm.Series.Add(scatterSeries);

                plot_anomaly_solutions.Model = pm;


            //return pm;
        }

        private void SuperimposeExtremeDrawing(string unit, int numcolmap, int fontsize, double[] xepoints, double[] yepoints, double[] zepoints, double[,] anom)
        {
            int cnut = 0;
            zzecrit = new double[zecrit.Length];
            yyecrit = new double[zecrit.Length];
            xxecrit = new double[zecrit.Length];
            for (int i = 0; i < zecrit.Length; i++)
            {
                    //ridge and trough
                    zzecrit[cnut] = Math.Abs(zecrit[i]);
                    xxecrit[cnut] = xecrit[i];
                    yyecrit[cnut] = yecrit[i];
                    cnut++;               
            }

            double[,] anomaly2 = new double[nc, nr];
            double kat = 1;
            if (zecrit.Length > 0)
            {
                double maxval = zzecrit.Max();
                double maxval2 = anomaly.Max2D();
                
                if (maxval > maxval2)
                {
                    kat = maxval / maxval2;
                }
            }
           

            for (int j = 0; j < nr; j++)
            {
                for (int i = 0; i < nc; i++)
                {
                    anomaly2[i, j] = anomaly[i, j] * kat;
                }
            }

            var pm = new PlotModel();
            var lca1 = new OxyPlot.Axes.LinearColorAxis { Palette = OxyPalettes.Jet(numcolmap) };
            lca1.Position = OxyPlot.Axes.AxisPosition.Right;
            lca1.FontSize = fontsize;

            var la1 = new OxyPlot.Axes.LinearAxis();
            la1.Position = OxyPlot.Axes.AxisPosition.Bottom;
            la1.FontSize = fontsize;
            la1.Title = "X " + unit;
            la1.Minimum = 0;
            la1.Maximum = (nc - 1) * dx;

            var lb1 = new OxyPlot.Axes.LinearAxis();
            lb1.Position = OxyPlot.Axes.AxisPosition.Left;
            lb1.FontSize = fontsize;
            lb1.Title = "Y " + unit;
            lb1.Minimum = 0;
            lb1.Maximum = (nr - 1) * dy;

            pm.Axes.Add(lca1);
            pm.Axes.Add(la1);
            pm.Axes.Add(lb1);

            OxyPlot.Series.HeatMapSeries hm = new OxyPlot.Series.HeatMapSeries();
            hm.X0 = 0; hm.X1 = (nc - 1) * dx; hm.Y0 = 0; hm.Y1 = (nr - 1) * dy;
            hm.Interpolate = true;
            hm.Data = (Double[,])anomaly2;
            pm.Series.Add(hm);

            var la3 = new OxyPlot.Axes.LinearAxis();
            la3.Position = OxyPlot.Axes.AxisPosition.Bottom;
            la3.FontSize = fontsize;
            la3.Title = "X " + unit;
            la3.Minimum = 0;
            la3.Maximum = (nc - 1) * dx;

            var lb3 = new OxyPlot.Axes.LinearAxis();
            lb3.Position = OxyPlot.Axes.AxisPosition.Left;
            lb3.FontSize = fontsize;
            lb3.Title = "Y " + unit;
            lb3.Minimum = 0;
            lb3.Maximum = (nr - 1) * dy;

            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            scatterSeries.BinSize = numcolscatter;
            scatterSeries.MarkerStroke = OxyColors.Black;
            scatterSeries.MarkerStrokeThickness = markerstroke;
            scatterSeries.MarkerType = markertip(markertype);
            pm.Axes.Add(la3);
            pm.Axes.Add(lb3);

            int verisay = zepoints.Length;
            for (int i = 0; i < verisay; i++)
            {
                double xp = xepoints[i];
                double yp = yepoints[i];
                double zp = -zepoints[i];
                var sizem = Convert.ToInt32(zp);
                var colorValue = zp;

                if (cbox_criteria.Checked)
                {
                    if (zp <= maxdepth && zp >= mindepth)
                        scatterSeries.Points.Add(new OxyPlot.Series.ScatterPoint(xp, yp, markersize, colorValue));
                }
                else
                {
                    scatterSeries.Points.Add(new OxyPlot.Series.ScatterPoint(xp, yp, markersize, colorValue));
                }
            }

            pm.Series.Add(scatterSeries);

            plot_extreme_anomaly.Model = pm;
        }

        MarkerType tip;
        private MarkerType markertip(int co)
        {
            if (co == 0) tip = MarkerType.Circle;
            if (co == 1) tip = MarkerType.Cross;
            if (co == 2) tip = MarkerType.Diamond;
            if (co == 3) tip = MarkerType.Plus;
            if (co == 4) tip = MarkerType.Square;
            if (co == 5) tip = MarkerType.Star;
            if (co == 6) tip = MarkerType.Triangle;
            return tip;
        }

        private void cbox_trough_CheckedChanged(object sender, EventArgs e)
        {
            criticalpoint_limit();
            SuperimposeCriticalDrawing("(m)", numcolmap, fontsize, xxcrit, yycrit, zzcrit, anomaly);
        }

        private void cbox_ridge_CheckedChanged(object sender, EventArgs e)
        {
            criticalpoint_limit();
            SuperimposeCriticalDrawing("(m)", numcolmap, fontsize, xxcrit, yycrit, zzcrit, anomaly);
        }

       


        private void criticalpoint_limit()
        {
            int cnut = 0;
            zzcrit = new double[zcrit.Length];
            yycrit = new double[zcrit.Length];
            xxcrit = new double[zcrit.Length];
            
            for(int i=0;i<zcrit.Length-1;i++)
            {
                
                if (cbox_ridge.Checked && cbox_trough.Checked)
                {
                    //ridge and trough
                    zzcrit[cnut] = zcrit[i];
                    xxcrit[cnut] = xcrit[i];
                    yycrit[cnut] = ycrit[i];
                    cnut++;
                }
                else if (cbox_ridge.Checked && !cbox_trough.Checked)
                {
                    //ridge
                    if (statecrit[i]!=null)
                    if (statecrit[i].ToString() == "ridge")
                    {
                        zzcrit[cnut] = zcrit[i];
                        xxcrit[cnut] = xcrit[i];
                        yycrit[cnut] = ycrit[i];
                        cnut++;
                    }
                }
                else if (!cbox_ridge.Checked && cbox_trough.Checked)
                {
                    //trough
                    if (statecrit[i]!=null)
                    if (statecrit[i].ToString() == "trough")
                    {
                        zzcrit[cnut] = zcrit[i];
                        xxcrit[cnut] = xcrit[i];
                        yycrit[cnut] = ycrit[i];
                        cnut++;
                    }
                }

                plot_solutions.Model = ScatterDrawing("(m)", numcolmap, fontsize, xxcrit, yycrit, zzcrit);
            }
            
        }       

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                cbox_ridge.Checked = true;
                cbox_trough.Checked = true;
                read_map_settings();
                plot_anomaly.Model = Drawing("(m)", numcolmap, fontsize, anomaly);
                plot_hgm.Model = Drawing("(m)", numcolmap, fontsize, hgm);
                plot_solutions.Model = ScatterDrawing("(m)", numcolmap, fontsize, xcrit, ycrit, zcrit);
                SuperimposeCriticalDrawing("(m)", numcolmap, fontsize, xcrit, ycrit, zcrit, anomaly);
                plot_extreme_points.Model = ScatterDrawing("(m)", numcolmap, fontsize, xecrit, yecrit, zecrit);
                SuperimposeExtremeDrawing("(m)", numcolmap, fontsize, xecrit, yecrit, zecrit, anomaly);
                plot_maximum_curvature.Model = Drawing("(m)", numcolmap, fontsize, Kmax);
                plot_minimum_curvature.Model = Drawing("(m)", numcolmap, fontsize, Kmin);
                plot_curvedness.Model = Drawing("(m)", numcolmap, fontsize, Kn);

                plot_mean_curvature.Model = Drawing("(m)", numcolmap, fontsize, Km);
                plot_gaussian_curvature.Model = Drawing("(m)", numcolmap, fontsize, Kg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during the process");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string file = saveFileDialog1.FileName;
                    string filen = System.IO.Path.GetFileNameWithoutExtension(file);
                    Bitmap bitmap;
                    var stream = new System.IO.MemoryStream();
                    var pngExporter = new PngExporter { Width = 1920, Height = 1080, Background = OxyColors.White };
                    pngExporter.Export(Pm1, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_anomaly.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm2, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_hgm.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm3, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_criticalpoints.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm4, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_anom&critpoints.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm5, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_extremum.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm6, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_anom&extremumpoints.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm7, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_maxcurvature.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm8, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_mincurvature.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    stream = new System.IO.MemoryStream();
                    pngExporter.Export(Pm9, stream);
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                    bitmap.Save(file + "_curvedness.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please, check the entered parameters and try again");
            }
        }       

    }
}

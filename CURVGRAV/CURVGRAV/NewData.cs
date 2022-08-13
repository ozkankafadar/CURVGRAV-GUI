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
    public partial class NewData : Form
    {
        int nx, ny;
        double dx, dy;
        string units;
        int nxsay, nysay;
        string pt;



        public NewData()
        {
            InitializeComponent();
        }

        private void NewData_Load(object sender, EventArgs e)
        {
            cbox_unit.SelectedIndex = 0;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                nx = Convert.ToInt32(tx_nc.Text);
                ny = Convert.ToInt32(tx_nr.Text);
                dx = Convert.ToDouble(tx_dx.Text);
                dy = Convert.ToDouble(tx_dy.Text);


                if (nx * ny > 65535)
                {
                    MessageBox.Show("Number of Total cells mustn't be greater than 65535");
                    goto sons2;
                }

                int rc = Convert.ToInt32(ny) + 1;
                int cc = Convert.ToInt32(nx) + 1;
                dg_data.AllowUserToAddRows = false;
                dg_data.RowCount = rc;
                dg_data.ColumnCount = cc;
                dg_data.ColumnHeadersVisible = false;
                dg_data.RowHeadersVisible = false;
                dg_data.Rows[0].ReadOnly = true;
                dg_data.Columns[0].ReadOnly = true;
                dg_data.Rows[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                dg_data.Columns[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                dg_data.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                dg_data.Columns[0].DefaultCellStyle.ForeColor = Color.White;


                int nxsay = Convert.ToInt32(nx);
                int nysay = Convert.ToInt32(ny);

                dg_data.Columns[0].Width = 60;
                for (int j = 0; j < nysay; j++)
                {
                    double x = (j) * dy;
                    dg_data[0, j + 1].Value = x;

                }

                for (int j = 0; j < nxsay; j++)
                {
                    double y = (j) * dx;
                    dg_data[j + 1, 0].Value = y;

                }

                btn_save.Enabled = true;
            sons2: ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please, check the entered parameters and try again");
            }
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                if (tx_nc.Text != "" && tx_nr.Text != "" && tx_dy.Text != "" && tx_dx.Text != "" && tx_comment.Text != "" && cbox_unit.SelectedIndex > 0)
                {

                    nx = Convert.ToInt32(tx_nc.Text);
                    ny = Convert.ToInt32(tx_nr.Text);
                    dx = Convert.ToDouble(tx_dx.Text);
                    dy = Convert.ToDouble(tx_dy.Text);

                    if (nx < 3 || ny < 3)
                    {
                        MessageBox.Show("Numbers of stations must be greater than 3");
                        goto sons2;
                    }

                    if (nx * ny > 65535)
                    {
                        MessageBox.Show("Number of Total cells mustn't be greater than 65535");
                        goto sons2;
                    }

                    dg_data.RowCount = 1;
                    dg_data.ColumnCount = 1;
                    btn_save.Text = "Save";
                    btn_save.Enabled = true;
                    btn_edit.Enabled = true;
                    btn_create.Enabled = false;
                    btn_load.Enabled = false;
                    btn_cancel.Enabled = true;

                    int rc = Convert.ToInt32(ny) + 1;
                    int cc = Convert.ToInt32(nx) + 1;
                    dg_data.AllowUserToAddRows = false;
                    dg_data.RowCount = rc;
                    dg_data.ColumnCount = cc;
                    dg_data.ColumnHeadersVisible = false;
                    dg_data.RowHeadersVisible = false;
                    dg_data.Rows[0].ReadOnly = true;
                    dg_data.Columns[0].ReadOnly = true;
                    dg_data.Rows[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                    dg_data.Columns[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                    dg_data.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                    dg_data.Columns[0].DefaultCellStyle.ForeColor = Color.White;


                    int nxsay = Convert.ToInt32(nx);
                    int nysay = Convert.ToInt32(ny);

                    dg_data.Columns[0].Width = 60;
                    for (int j = 0; j < nysay; j++)
                    {
                        double x = (j) * dy;
                        dg_data[0, j + 1].Value = x;

                    }

                    for (int j = 0; j < nxsay; j++)
                    {
                        double y = (j) * dx;
                        dg_data[j + 1, 0].Value = y;
                    }

                }
                else
                {
                    MessageBox.Show("Please, enter the parameters (column count, row count, sampling intervals, unit and comment)");
                }
            sons2: ;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please, check the entered parameters and try again");
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath + "\\Data";
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.Filter = "Text Files (*.txt)|*.txt";
                int nxsay = 0;
                int nysay = 0;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pt = openFileDialog1.FileName;
                    btn_save.Text = "Update";
                    btn_save.Enabled = true;
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    nx = Convert.ToInt32(sr.ReadLine());
                    tx_nc.Text = nx.ToString();
                    ny = Convert.ToInt32(sr.ReadLine());
                    tx_nr.Text = ny.ToString();
                    dx = Convert.ToDouble(sr.ReadLine());
                    tx_dx.Text = dx.ToString();
                    dy = Convert.ToDouble(sr.ReadLine());
                    tx_dy.Text = dy.ToString();
                    units = sr.ReadLine();

                    if (units == "meter") cbox_unit.SelectedIndex = 1;
                    else if (units == "kilometer") cbox_unit.SelectedIndex = 2;

                    tx_comment.Text = sr.ReadLine();

                    int rc = Convert.ToInt32(ny) + 1;
                    int cc = Convert.ToInt32(nx) + 1;
                    dg_data.AllowUserToAddRows = false;
                    dg_data.RowCount = rc;
                    dg_data.ColumnCount = cc;
                    dg_data.ColumnHeadersVisible = false;
                    dg_data.RowHeadersVisible = false;
                    dg_data.Rows[0].ReadOnly = true;
                    dg_data.Columns[0].ReadOnly = true;
                    dg_data.Rows[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                    dg_data.Columns[0].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                    dg_data.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                    dg_data.Columns[0].DefaultCellStyle.ForeColor = Color.White;

                    nxsay = Convert.ToInt32(nx);
                    nysay = Convert.ToInt32(ny);

                    for (int j = 1; j < nysay + 1; j++)
                    {
                        double y = (j - 1) * dy;
                        for (int i = 1; i < nxsay + 1; i++)
                        {
                            double x = (i - 1) * dx;
                            string[] prt = sr.ReadLine().Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            dg_data[i, j].Value = prt[2];
                            x++;
                        }
                        y++;
                    }
                    sr.Close();

                    for (int j = 0; j < nysay; j++)
                    {
                        double x = (j) * dy;
                        dg_data[0, j + 1].Value = x;
                    }

                    for (int j = 0; j < nxsay; j++)
                    {
                        double y = (j) * dx;
                        dg_data[j + 1, 0].Value = y;
                    }
                }

                btn_save.Enabled = true;
                btn_create.Enabled = false;
                btn_load.Enabled = false;
                btn_edit.Enabled = true;
                btn_cancel.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please, check the format of input file and try again");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_save.Text == "Save")
                {
                    nxsay = Convert.ToInt32(nx);
                    nysay = Convert.ToInt32(ny);

                    for (int j = 1; j < nysay + 1; j++)
                    {
                        double y = (j - 1) * dy;
                        for (int i = 1; i < nxsay + 1; i++)
                        {
                            double x = (i - 1) * dx;
                            if (dg_data[i, j].Value.ToString() == "")
                            {
                                MessageBox.Show("Please, enter data for all columns and rows");
                                goto sonnn;
                            }
                            x++;
                        }
                        y++;
                    }

                    saveFileDialog1.InitialDirectory = Application.StartupPath + "\\Data";
                    saveFileDialog1.DefaultExt = "txt";
                    saveFileDialog1.Filter = "Text Files (*.txt)|*.txt";
                    string files = "";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        files = saveFileDialog1.FileName;

                        System.IO.StreamWriter sw = new System.IO.StreamWriter(files);
                        sw.WriteLine(nx);
                        sw.WriteLine(ny);
                        sw.WriteLine(dx);
                        sw.WriteLine(dy);
                        sw.WriteLine(cbox_unit.Text);
                        sw.WriteLine(tx_comment.Text);
                        //sw.Close();

                        //System.IO.StreamWriter sw1 = new System.IO.StreamWriter(Application.StartupPath + "//Data//" + saveFileDialog1.FileName + "2.txt");

                        nxsay = Convert.ToInt32(nx);
                        nysay = Convert.ToInt32(ny);

                        for (int j = 1; j < nysay + 1; j++)
                        {
                            double y = (j - 1) * dy;
                            for (int i = 1; i < nxsay + 1; i++)
                            {
                                double x = (i - 1) * dx;
                                sw.WriteLine(x + "\t" + y + "\t" + dg_data[i, j].Value);
                                x++;
                            }
                            y++;
                        }
                        sw.Close();
                    }


                    MessageBox.Show("The input file is saved successfully");
                    btn_save.Enabled = false;
                    btn_create.Enabled = true;
                    btn_load.Enabled = true;
                    btn_cancel.Enabled = false;
                }
                if (btn_save.Text == "Update")
                {
                    //saveFileDialog1.InitialDirectory = Application.StartupPath + "//Data";
                    //saveFileDialog1.DefaultExt = "txt";
                    //saveFileDialog1.Filter = "Text Files (*.txt)|*.txt";
                    //string files = "";
                    //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    files = saveFileDialog1.FileName;

                    System.IO.StreamWriter sw = new System.IO.StreamWriter(pt);
                    sw.WriteLine(nx);
                    sw.WriteLine(ny);
                    sw.WriteLine(dx);
                    sw.WriteLine(dy);
                    sw.WriteLine(units);
                    sw.WriteLine(tx_comment.Text);
                    //sw.Close();

                    //System.IO.StreamWriter sw1 = new System.IO.StreamWriter(Application.StartupPath + "//Data//" + saveFileDialog1.FileName + "2.txt");

                    nxsay = Convert.ToInt32(nx);
                    nysay = Convert.ToInt32(ny);

                    for (int j = 1; j < nysay + 1; j++)
                    {
                        double y = (j - 1) * dy;
                        for (int i = 1; i < nxsay + 1; i++)
                        {
                            double x = (i - 1) * dx;
                            sw.WriteLine(x + "\t" + y + "\t" + dg_data[i, j].Value);
                            x++;
                        }
                        y++;
                    }
                    sw.Close();
                    //}
                    btn_save.Enabled = false;
                    btn_create.Enabled = true;
                    btn_load.Enabled = true;
                    btn_cancel.Enabled = false;
                    MessageBox.Show("The input file is updated successfully");
                }


            sonnn: ;
            btn_edit.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Please, check the entered data and try again");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            tx_nc.Text = null;
            tx_nr.Text = null;
            tx_dy.Text = null;
            tx_dx.Text = null;
            tx_comment.Text = null;

            btn_create.Enabled = true;
            btn_load.Enabled = true;
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            btn_edit.Enabled = false;
        }

        private void tx_nc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))){if (e.KeyChar != '\b'){e.Handled = true;}}
        }

        private void tx_dx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }

        private void tx_nr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b') { e.Handled = true; } }
        }

        private void tx_dy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }
        }       

        private void cbox_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_unit.SelectedIndex == 0)
            { label10.Text = "?"; label12.Text = "?"; }
            if (cbox_unit.SelectedIndex == 1)
            { label10.Text = "m"; label12.Text = "m"; }
            if (cbox_unit.SelectedIndex == 2) 
            {    label10.Text = "km";label12.Text = "km";}            
        }

        private void dg_data_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox){TextBox tb = e.Control as TextBox;tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);}}

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (!(char.IsDigit(e.KeyChar))){if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-'){e.Handled = true;}}
        }

        private void NewData_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.New_Data = null;
        }

    }
}

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
    public partial class FieldApp : Form
    {
        double[,] anomaly, hgm;
        double beta;
        string unit;
        int nc, nr;
        double dx, dy;
        double[,] Km, Kg, Kmax, Kmin, Kn;
        double maxdepth, mindepth;
        int numcolmap, numcolscatter, markersize, markerstroke, markertype, fontsize;
        Point currentcellvar;
        double[] zcrit, xcrit, ycrit, xecrit, yecrit, zecrit;
        string[] statecrit;
        double[] zzcrit, yycrit, xxcrit;
        double[] zzecrit, yyecrit, xxecrit;
        PlotModel Pm1, Pm2, Pm3, Pm4, Pm5, Pm6, Pm7, Pm8, Pm9;

        public FieldApp()
        {
            InitializeComponent();
        }

        private void FieldApp_Load(object sender, EventArgs e)
        {

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

        string codee = "";
        private void btn_parameters_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath + "//Data";
                openFileDialog1.DefaultExt = "txt";
                //openFileDialog1.AutoUpgradeEnabled = false;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "Text Files (*.txt)|*.txt";
                string files = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    files = openFileDialog1.FileName;
                    System.IO.StreamReader sr = new System.IO.StreamReader(files);
                    nc = Convert.ToInt32(sr.ReadLine());
                    nr = Convert.ToInt32(sr.ReadLine());
                    dx = Convert.ToDouble(sr.ReadLine());
                    dy = Convert.ToDouble(sr.ReadLine());
                    tx_xnum.Text = nc.ToString();
                    tx_ynum.Text = nr.ToString();
                    tx_dx.Text = dx.ToString();
                    tx_dy.Text = dy.ToString();
                    unit = sr.ReadLine();
                    if (unit == "meter") unit = "(m)";
                    else if (unit == "kilometer") unit = "(km)";
                    else unit = "";
                    label10.Text = unit;
                    label12.Text = unit;

                    sr.ReadLine();

                    anomaly = new double[nc, nr];

                    string[] prt;
                    codee = "";
                    for (int j = 0; j < nr; j++)
                    {
                        for (int i = 0; i < nc; i++)
                        {
                            prt = sr.ReadLine().Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int k = 0; k < 3; k++)
                            {
                                if (prt[k].Contains(","))
                                {
                                    codee = "Please, use dot (.) as decimal seperator";
                                }
                            }                              
                            anomaly[i, j] = Convert.ToDouble(prt[2]);
                        }
                    }

                    sr.Close();

                    numcolmap = CURVGRAV.MainForm.numcolmap;
                    numcolscatter = CURVGRAV.MainForm.numcolscatter;
                    markersize = CURVGRAV.MainForm.markersize;
                    markerstroke = CURVGRAV.MainForm.markerstroke;
                    markertype = CURVGRAV.MainForm.markertype;
                    fontsize = CURVGRAV.MainForm.fontsize;

                    plot_anomaly.Model = Drawing(unit, numcolmap, fontsize, anomaly);
                    plot_anomaly1.Model = Drawing(unit, numcolmap, fontsize, anomaly);

                    btn_calculate.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                if (codee != "")
                    MessageBox.Show(codee);
                else
                MessageBox.Show("Please, check the format of input file and try again");
            }
        }


        private PlotModel Drawing(string unit, int numcolmap, int fontsize, double[,] anom)
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

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            try
            {
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

                nc = Convert.ToInt32(tx_xnum.Text);
                nr = Convert.ToInt32(tx_ynum.Text);
                dx = Convert.ToDouble(tx_dx.Text);
                dy = Convert.ToDouble(tx_dy.Text);

                progressBar1.Minimum = 0;
                progressBar1.Maximum = (nr - 2) * (nc - 2);
                progressBar1.Value = 0;

                Km = new double[nc, nr];
                Kg = new double[nc, nr];
                Kmax = new double[nc, nr];
                Kmin = new double[nc, nr];
                Kn = new double[nc, nr];

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
                //plot_anomaly.Model = Drawing("(m)", numcolmap, fontsize, anomaly);
                plot_hgm.Model = Drawing(unit, numcolmap, fontsize, hgm);

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

                plot_solutions.Model = ScatterDrawing(unit, numcolmap, fontsize, xcrit, ycrit, zcrit);

                SuperimposeCriticalDrawing(unit, numcolmap, fontsize, xcrit, ycrit, zcrit, anomaly);

                plot_extreme_points.Model = ScatterDrawing(unit, numcolmap, fontsize, xecrit, yecrit, zecrit);

                SuperimposeExtremeDrawing(unit, numcolmap, fontsize, xecrit, yecrit, zecrit, anomaly);

                plot_maximum_curvature.Model = Drawing(unit, numcolmap, fontsize, Kmax);
                plot_minimum_curvature.Model = Drawing(unit, numcolmap, fontsize, Kmin);
                plot_curvedness.Model = Drawing(unit, numcolmap, fontsize, Kn);

                plot_mean_curvature.Model = Drawing(unit, numcolmap, fontsize, Km);
                plot_gaussian_curvature.Model = Drawing(unit, numcolmap, fontsize, Kg);

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

            son: ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during the process");
            }
        }


        private void calc_criticalpoint_and_extreme(bool usehgm)
        {
            double x0 = 0;
            double y0 = 0;
            double zz = 0;
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
                    lbl_progress.Text ="%"+ (progressBar1.Value * 100 / progressBar1.Maximum).ToString();
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
                    else if (usehgm)
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

        private PlotModel ScatterDrawing(string unit, int numcolmap, int fontsize, double[] xpoints, double[] ypoints, double[] zpoints)
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

            var scatterSeries = new ScatterSeries {};
            scatterSeries.BinSize = numcolscatter;
            scatterSeries.MarkerStroke = OxyColors.Black;
            scatterSeries.MarkerStrokeThickness = markerstroke;
            scatterSeries.MarkerType = markertip(markertype);

            pm2.Axes.Add(la3);
            pm2.Axes.Add(lb3);

            int verisay = zpoints.Length;
            for (int i = 0; i < verisay; i++)
            {
                double xp = xpoints[i];
                double yp = ypoints[i];
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

        private void SuperimposeCriticalDrawing(string unit, int numcolmap, int fontsize, double[] xpoints, double[] ypoints, double[] zpoints, double[,] anom)
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
                    if (statecrit[i] != null)
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

            var scatterSeries = new ScatterSeries { };
            scatterSeries.BinSize = numcolscatter;
            scatterSeries.MarkerStroke = OxyColors.Black;
            scatterSeries.MarkerStrokeThickness = markerstroke;
            scatterSeries.MarkerType = markertip(markertype);
            pm.Axes.Add(la3);
            pm.Axes.Add(lb3);

            int verisay = zpoints.Length;
            for (int i = 0; i < verisay; i++)
            {
                double xp = xpoints[i];
                double yp = ypoints[i];
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
             if (zzcrit.Length > 0)
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

            var scatterSeries = new ScatterSeries {};
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

        private void cbox_trough_CheckedChanged(object sender, EventArgs e)
        {
            criticalpoint_limit();
            SuperimposeCriticalDrawing(unit, numcolmap, fontsize, xxcrit, yycrit, zzcrit, anomaly);
        }

        private void cbox_ridge_CheckedChanged(object sender, EventArgs e)
        {
            criticalpoint_limit();
            SuperimposeCriticalDrawing(unit, numcolmap, fontsize, xxcrit, yycrit, zzcrit, anomaly);
        }

        private void criticalpoint_limit()
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
                    if (statecrit[i] != null)
                    if (statecrit[i].ToString() == "trough")
                    {
                        zzcrit[cnut] = zcrit[i];
                        xxcrit[cnut] = xcrit[i];
                        yycrit[cnut] = ycrit[i];
                        cnut++;
                    }
                }

                plot_solutions.Model = ScatterDrawing(unit, numcolmap, fontsize, xxcrit, yycrit, zzcrit);
            }

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

        private void tx_maxdepth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }

        }

        private void tx_mindepth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }

        }

        private void tx_si_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))) { if (e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-') { e.Handled = true; } }

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            cbox_ridge.Checked = true;
            cbox_trough.Checked = true;
            read_map_settings();
            plot_anomaly.Model = Drawing(unit, numcolmap, fontsize, anomaly);
            plot_hgm.Model = Drawing(unit, numcolmap, fontsize, hgm);
            plot_solutions.Model = ScatterDrawing(unit, numcolmap, fontsize, xcrit, ycrit, zcrit);
            SuperimposeCriticalDrawing(unit, numcolmap, fontsize, xcrit, ycrit, zcrit, anomaly);
            plot_extreme_points.Model = ScatterDrawing(unit, numcolmap, fontsize, xecrit, yecrit, zecrit);
            SuperimposeExtremeDrawing(unit, numcolmap, fontsize, xecrit, yecrit, zecrit, anomaly);
            plot_maximum_curvature.Model = Drawing(unit, numcolmap, fontsize, Kmax);
            plot_minimum_curvature.Model = Drawing(unit, numcolmap, fontsize, Kmin);
            plot_curvedness.Model = Drawing(unit, numcolmap, fontsize, Kn);

            plot_mean_curvature.Model = Drawing(unit, numcolmap, fontsize, Km);
            plot_gaussian_curvature.Model = Drawing(unit, numcolmap, fontsize, Kg);
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

        private void FieldApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            CURVGRAV.MainForm.Field_App = null;
            
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

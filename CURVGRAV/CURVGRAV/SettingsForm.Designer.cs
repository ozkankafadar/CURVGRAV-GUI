namespace CURVGRAV
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbox_markertype = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbox_strokethick = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbox_markersize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbox_numcolourscatter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbox_numcolour = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tx_fontsize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_OK.Location = new System.Drawing.Point(87, 248);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(107, 30);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbox_markertype);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbox_strokethick);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbox_markersize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbox_numcolourscatter);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 140);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scatter Map";
            // 
            // cbox_markertype
            // 
            this.cbox_markertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_markertype.FormattingEnabled = true;
            this.cbox_markertype.Items.AddRange(new object[] {
            "Circle",
            "Cross",
            "Diamod",
            "Plus",
            "Square",
            "Star",
            "Triangle"});
            this.cbox_markertype.Location = new System.Drawing.Point(135, 23);
            this.cbox_markertype.Name = "cbox_markertype";
            this.cbox_markertype.Size = new System.Drawing.Size(128, 21);
            this.cbox_markertype.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Marker Type";
            // 
            // cbox_strokethick
            // 
            this.cbox_strokethick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_strokethick.FormattingEnabled = true;
            this.cbox_strokethick.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbox_strokethick.Location = new System.Drawing.Point(188, 107);
            this.cbox_strokethick.Name = "cbox_strokethick";
            this.cbox_strokethick.Size = new System.Drawing.Size(75, 21);
            this.cbox_strokethick.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Marker Stroke Thickness";
            // 
            // cbox_markersize
            // 
            this.cbox_markersize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_markersize.FormattingEnabled = true;
            this.cbox_markersize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbox_markersize.Location = new System.Drawing.Point(188, 79);
            this.cbox_markersize.Name = "cbox_markersize";
            this.cbox_markersize.Size = new System.Drawing.Size(75, 21);
            this.cbox_markersize.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Marker Size";
            // 
            // cbox_numcolourscatter
            // 
            this.cbox_numcolourscatter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_numcolourscatter.FormattingEnabled = true;
            this.cbox_numcolourscatter.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "200",
            "300",
            "400",
            "500"});
            this.cbox_numcolourscatter.Location = new System.Drawing.Point(188, 51);
            this.cbox_numcolourscatter.Name = "cbox_numcolourscatter";
            this.cbox_numcolourscatter.Size = new System.Drawing.Size(75, 21);
            this.cbox_numcolourscatter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number of Colors";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbox_numcolour);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 52);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Map";
            // 
            // cbox_numcolour
            // 
            this.cbox_numcolour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_numcolour.FormattingEnabled = true;
            this.cbox_numcolour.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000"});
            this.cbox_numcolour.Location = new System.Drawing.Point(188, 21);
            this.cbox_numcolour.Name = "cbox_numcolour";
            this.cbox_numcolour.Size = new System.Drawing.Size(75, 21);
            this.cbox_numcolour.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Colors";
            // 
            // tx_fontsize
            // 
            this.tx_fontsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tx_fontsize.Location = new System.Drawing.Point(111, 18);
            this.tx_fontsize.Name = "tx_fontsize";
            this.tx_fontsize.Size = new System.Drawing.Size(100, 20);
            this.tx_fontsize.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(45, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Font Size";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(294, 286);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tx_fontsize);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.Text = "MAP SETTINGS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbox_markertype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbox_strokethick;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbox_markersize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbox_numcolourscatter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbox_numcolour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tx_fontsize;
        private System.Windows.Forms.Label label6;
    }
}
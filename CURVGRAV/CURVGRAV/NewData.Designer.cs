namespace CURVGRAV
{
    partial class NewData
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_unit = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_edit = new System.Windows.Forms.Button();
            this.tx_nc = new System.Windows.Forms.TextBox();
            this.tx_nr = new System.Windows.Forms.TextBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.tx_comment = new System.Windows.Forms.TextBox();
            this.tx_dx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tx_dy = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dg_data = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_data)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 541);
            this.panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_cancel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbox_unit);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.tx_nc);
            this.groupBox1.Controls.Add(this.tx_nr);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Controls.Add(this.tx_comment);
            this.groupBox1.Controls.Add(this.tx_dx);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tx_dy);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 433);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Enabled = false;
            this.btn_cancel.Location = new System.Drawing.Point(10, 402);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(173, 23);
            this.btn_cancel.TabIndex = 57;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Unit";
            // 
            // cbox_unit
            // 
            this.cbox_unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_unit.FormattingEnabled = true;
            this.cbox_unit.Items.AddRange(new object[] {
            "select",
            "meter",
            "kilometer"});
            this.cbox_unit.Location = new System.Drawing.Point(43, 98);
            this.cbox_unit.Name = "cbox_unit";
            this.cbox_unit.Size = new System.Drawing.Size(137, 21);
            this.cbox_unit.TabIndex = 55;
            this.cbox_unit.SelectedIndexChanged += new System.EventHandler(this.cbox_unit_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(157, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 54;
            this.label12.Text = "?";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "dy";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(157, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "dx";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(178, 13);
            this.label13.TabIndex = 50;
            this.label13.Text = "Number of stations-Y direction";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Number of stations-X direction";
            // 
            // btn_edit
            // 
            this.btn_edit.Enabled = false;
            this.btn_edit.Location = new System.Drawing.Point(10, 123);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(173, 23);
            this.btn_edit.TabIndex = 9;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // tx_nc
            // 
            this.tx_nc.Location = new System.Drawing.Point(8, 32);
            this.tx_nc.Name = "tx_nc";
            this.tx_nc.Size = new System.Drawing.Size(64, 20);
            this.tx_nc.TabIndex = 0;
            this.tx_nc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tx_nc_KeyPress);
            // 
            // tx_nr
            // 
            this.tx_nr.Location = new System.Drawing.Point(8, 71);
            this.tx_nr.Name = "tx_nr";
            this.tx_nr.Size = new System.Drawing.Size(64, 20);
            this.tx_nr.TabIndex = 1;
            this.tx_nr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tx_nr_KeyPress);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(10, 344);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(173, 23);
            this.btn_load.TabIndex = 7;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // tx_comment
            // 
            this.tx_comment.Location = new System.Drawing.Point(10, 170);
            this.tx_comment.Multiline = true;
            this.tx_comment.Name = "tx_comment";
            this.tx_comment.Size = new System.Drawing.Size(173, 139);
            this.tx_comment.TabIndex = 5;
            // 
            // tx_dx
            // 
            this.tx_dx.Location = new System.Drawing.Point(108, 32);
            this.tx_dx.Name = "tx_dx";
            this.tx_dx.Size = new System.Drawing.Size(44, 20);
            this.tx_dx.TabIndex = 2;
            this.tx_dx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tx_dx_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Comment";
            // 
            // tx_dy
            // 
            this.tx_dy.Location = new System.Drawing.Point(108, 71);
            this.tx_dy.Name = "tx_dy";
            this.tx_dy.Size = new System.Drawing.Size(44, 20);
            this.tx_dy.TabIndex = 3;
            this.tx_dy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tx_dy_KeyPress);
            // 
            // btn_save
            // 
            this.btn_save.Enabled = false;
            this.btn_save.Location = new System.Drawing.Point(10, 373);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(173, 23);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(10, 315);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(173, 23);
            this.btn_create.TabIndex = 6;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dg_data);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(208, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(746, 541);
            this.panel2.TabIndex = 6;
            // 
            // dg_data
            // 
            this.dg_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_data.Location = new System.Drawing.Point(0, 0);
            this.dg_data.Name = "dg_data";
            this.dg_data.Size = new System.Drawing.Size(746, 541);
            this.dg_data.TabIndex = 10;
            this.dg_data.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dg_data_EditingControlShowing);
            // 
            // NewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "NewData";
            this.Text = "CREATE/EDIT INPUT FILE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewData_FormClosing);
            this.Load += new System.EventHandler(this.NewData_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbox_unit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.TextBox tx_nc;
        private System.Windows.Forms.TextBox tx_nr;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.TextBox tx_comment;
        private System.Windows.Forms.TextBox tx_dx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tx_dy;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dg_data;
    }
}
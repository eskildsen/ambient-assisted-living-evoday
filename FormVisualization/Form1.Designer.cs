namespace FormVisualization
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logOutput = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.iconContainer = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.iconToothbrush = new System.Windows.Forms.PictureBox();
            this.iconSoap = new System.Windows.Forms.PictureBox();
            this.iconHandwash = new System.Windows.Forms.PictureBox();
            this.groupSimulations = new System.Windows.Forms.GroupBox();
            this.btnSimulateLeaveRoom = new System.Windows.Forms.Button();
            this.btnSimulateSoap = new System.Windows.Forms.Button();
            this.btnSimulateToothbrush = new System.Windows.Forms.Button();
            this.btnSimulateFlush = new System.Windows.Forms.Button();
            this.btnSimulateHandWashing = new System.Windows.Forms.Button();
            this.btnSimulateToilet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelConnectionStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.iconContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconToothbrush)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSoap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconHandwash)).BeginInit();
            this.groupSimulations.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.iconContainer, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupSimulations, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1407, 445);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.logOutput);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(706, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 3);
            this.groupBox1.Size = new System.Drawing.Size(698, 387);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // logOutput
            // 
            this.logOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutput.Location = new System.Drawing.Point(3, 16);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.Size = new System.Drawing.Size(692, 368);
            this.logOutput.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.tableLayoutPanel1.SetRowSpan(this.listView1, 2);
            this.listView1.Size = new System.Drawing.Size(697, 256);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tid";
            this.columnHeader1.Width = 85;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Patient";
            this.columnHeader2.Width = 61;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Sensor ID";
            this.columnHeader3.Width = 111;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Sensor Type";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Event Type";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Value 1";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Value 2";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Value 3";
            // 
            // iconContainer
            // 
            this.iconContainer.BackColor = System.Drawing.Color.Black;
            this.iconContainer.ColumnCount = 4;
            this.iconContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.iconContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.iconContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.iconContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.iconContainer.Controls.Add(this.pictureBox4, 3, 0);
            this.iconContainer.Controls.Add(this.iconToothbrush, 2, 0);
            this.iconContainer.Controls.Add(this.iconSoap, 1, 0);
            this.iconContainer.Controls.Add(this.iconHandwash, 0, 0);
            this.iconContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconContainer.Location = new System.Drawing.Point(3, 265);
            this.iconContainer.Name = "iconContainer";
            this.iconContainer.RowCount = 1;
            this.tableLayoutPanel1.SetRowSpan(this.iconContainer, 2);
            this.iconContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iconContainer.Size = new System.Drawing.Size(697, 177);
            this.iconContainer.TabIndex = 3;
            this.iconContainer.Click += new System.EventHandler(this.iconContainer_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Image = global::FormVisualization.Properties.Resources.flush;
            this.pictureBox4.Location = new System.Drawing.Point(525, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(169, 171);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            // 
            // iconToothbrush
            // 
            this.iconToothbrush.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconToothbrush.Image = global::FormVisualization.Properties.Resources.toothbrush;
            this.iconToothbrush.Location = new System.Drawing.Point(351, 3);
            this.iconToothbrush.Name = "iconToothbrush";
            this.iconToothbrush.Size = new System.Drawing.Size(168, 171);
            this.iconToothbrush.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconToothbrush.TabIndex = 2;
            this.iconToothbrush.TabStop = false;
            // 
            // iconSoap
            // 
            this.iconSoap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconSoap.Image = global::FormVisualization.Properties.Resources.soap_dispenser;
            this.iconSoap.Location = new System.Drawing.Point(177, 3);
            this.iconSoap.Name = "iconSoap";
            this.iconSoap.Size = new System.Drawing.Size(168, 171);
            this.iconSoap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconSoap.TabIndex = 1;
            this.iconSoap.TabStop = false;
            // 
            // iconHandwash
            // 
            this.iconHandwash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconHandwash.Image = global::FormVisualization.Properties.Resources.wash_hands;
            this.iconHandwash.Location = new System.Drawing.Point(3, 3);
            this.iconHandwash.Name = "iconHandwash";
            this.iconHandwash.Size = new System.Drawing.Size(168, 171);
            this.iconHandwash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconHandwash.TabIndex = 0;
            this.iconHandwash.TabStop = false;
            // 
            // groupSimulations
            // 
            this.groupSimulations.Controls.Add(this.btnSimulateLeaveRoom);
            this.groupSimulations.Controls.Add(this.btnSimulateSoap);
            this.groupSimulations.Controls.Add(this.btnSimulateToothbrush);
            this.groupSimulations.Controls.Add(this.btnSimulateFlush);
            this.groupSimulations.Controls.Add(this.btnSimulateHandWashing);
            this.groupSimulations.Controls.Add(this.btnSimulateToilet);
            this.groupSimulations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSimulations.Location = new System.Drawing.Point(917, 396);
            this.groupSimulations.Name = "groupSimulations";
            this.groupSimulations.Size = new System.Drawing.Size(487, 46);
            this.groupSimulations.TabIndex = 6;
            this.groupSimulations.TabStop = false;
            this.groupSimulations.Text = "Simuleringer";
            // 
            // btnSimulateLeaveRoom
            // 
            this.btnSimulateLeaveRoom.Location = new System.Drawing.Point(411, 16);
            this.btnSimulateLeaveRoom.Name = "btnSimulateLeaveRoom";
            this.btnSimulateLeaveRoom.Size = new System.Drawing.Size(75, 23);
            this.btnSimulateLeaveRoom.TabIndex = 8;
            this.btnSimulateLeaveRoom.Text = "Forlader rum";
            this.btnSimulateLeaveRoom.UseVisualStyleBackColor = true;
            this.btnSimulateLeaveRoom.Click += new System.EventHandler(this.btnSimulateLeaveRoom_Click);
            // 
            // btnSimulateSoap
            // 
            this.btnSimulateSoap.Location = new System.Drawing.Point(249, 16);
            this.btnSimulateSoap.Name = "btnSimulateSoap";
            this.btnSimulateSoap.Size = new System.Drawing.Size(75, 23);
            this.btnSimulateSoap.TabIndex = 7;
            this.btnSimulateSoap.Text = "Sæbe";
            this.btnSimulateSoap.UseVisualStyleBackColor = true;
            this.btnSimulateSoap.Click += new System.EventHandler(this.btnSimulateSoap_Click);
            // 
            // btnSimulateToothbrush
            // 
            this.btnSimulateToothbrush.Location = new System.Drawing.Point(330, 16);
            this.btnSimulateToothbrush.Name = "btnSimulateToothbrush";
            this.btnSimulateToothbrush.Size = new System.Drawing.Size(75, 23);
            this.btnSimulateToothbrush.TabIndex = 5;
            this.btnSimulateToothbrush.Text = "Tandbørste";
            this.btnSimulateToothbrush.UseVisualStyleBackColor = true;
            this.btnSimulateToothbrush.Click += new System.EventHandler(this.btnSimulateToothbrush_Click);
            // 
            // btnSimulateFlush
            // 
            this.btnSimulateFlush.Location = new System.Drawing.Point(87, 16);
            this.btnSimulateFlush.Name = "btnSimulateFlush";
            this.btnSimulateFlush.Size = new System.Drawing.Size(75, 23);
            this.btnSimulateFlush.TabIndex = 6;
            this.btnSimulateFlush.Text = "Reset";
            this.btnSimulateFlush.UseVisualStyleBackColor = true;
            this.btnSimulateFlush.Click += new System.EventHandler(this.btnSimulateFlush_Click);
            // 
            // btnSimulateHandWashing
            // 
            this.btnSimulateHandWashing.Location = new System.Drawing.Point(168, 16);
            this.btnSimulateHandWashing.Name = "btnSimulateHandWashing";
            this.btnSimulateHandWashing.Size = new System.Drawing.Size(75, 23);
            this.btnSimulateHandWashing.TabIndex = 4;
            this.btnSimulateHandWashing.Text = "Håndvask";
            this.btnSimulateHandWashing.UseVisualStyleBackColor = true;
            this.btnSimulateHandWashing.Click += new System.EventHandler(this.btnSimulateHandWashing_Click);
            // 
            // btnSimulateToilet
            // 
            this.btnSimulateToilet.Location = new System.Drawing.Point(6, 16);
            this.btnSimulateToilet.Name = "btnSimulateToilet";
            this.btnSimulateToilet.Size = new System.Drawing.Size(75, 23);
            this.btnSimulateToilet.TabIndex = 3;
            this.btnSimulateToilet.Text = "På Toilet";
            this.btnSimulateToilet.UseVisualStyleBackColor = true;
            this.btnSimulateToilet.Click += new System.EventHandler(this.btnSimulateToilet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelConnectionStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(706, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 46);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Forbindelse";
            // 
            // labelConnectionStatus
            // 
            this.labelConnectionStatus.AutoSize = true;
            this.labelConnectionStatus.Location = new System.Drawing.Point(7, 16);
            this.labelConnectionStatus.Name = "labelConnectionStatus";
            this.labelConnectionStatus.Size = new System.Drawing.Size(42, 13);
            this.labelConnectionStatus.TabIndex = 0;
            this.labelConnectionStatus.Text = "#status";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 445);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "MQTT Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosingAsync);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.iconContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconToothbrush)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSoap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconHandwash)).EndInit();
            this.groupSimulations.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox logOutput;
        private System.Windows.Forms.PictureBox iconHandwash;
        private System.Windows.Forms.PictureBox iconSoap;
        private System.Windows.Forms.PictureBox iconToothbrush;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TableLayoutPanel iconContainer;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.GroupBox groupSimulations;
        private System.Windows.Forms.Button btnSimulateLeaveRoom;
        private System.Windows.Forms.Button btnSimulateSoap;
        private System.Windows.Forms.Button btnSimulateToothbrush;
        private System.Windows.Forms.Button btnSimulateFlush;
        private System.Windows.Forms.Button btnSimulateHandWashing;
        private System.Windows.Forms.Button btnSimulateToilet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelConnectionStatus;
    }
}


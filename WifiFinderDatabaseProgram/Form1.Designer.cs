
namespace WifiFinderDatabaseProgram
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
            this.components = new System.ComponentModel.Container();
            this.comboBoxComPort1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxComPort2 = new System.Windows.Forms.ComboBox();
            this.comboBoxComPort3 = new System.Windows.Forms.ComboBox();
            this.richTextBoxDataPulled = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRefreshComPorts = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.timerSerialPortPull = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonStartPulling = new System.Windows.Forms.Button();
            this.buttonStopPulling = new System.Windows.Forms.Button();
            this.labelPullStatus = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonStartServer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxComPort1
            // 
            this.comboBoxComPort1.FormattingEnabled = true;
            this.comboBoxComPort1.Location = new System.Drawing.Point(6, 30);
            this.comboBoxComPort1.Name = "comboBoxComPort1";
            this.comboBoxComPort1.Size = new System.Drawing.Size(121, 24);
            this.comboBoxComPort1.TabIndex = 0;
            this.comboBoxComPort1.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRefreshComPorts);
            this.groupBox1.Controls.Add(this.comboBoxComPort3);
            this.groupBox1.Controls.Add(this.comboBoxComPort2);
            this.groupBox1.Controls.Add(this.comboBoxComPort1);
            this.groupBox1.Location = new System.Drawing.Point(293, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 152);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Porte";
            // 
            // comboBoxComPort2
            // 
            this.comboBoxComPort2.FormattingEnabled = true;
            this.comboBoxComPort2.Location = new System.Drawing.Point(6, 60);
            this.comboBoxComPort2.Name = "comboBoxComPort2";
            this.comboBoxComPort2.Size = new System.Drawing.Size(121, 24);
            this.comboBoxComPort2.TabIndex = 1;
            this.comboBoxComPort2.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort2_SelectedIndexChanged);
            // 
            // comboBoxComPort3
            // 
            this.comboBoxComPort3.FormattingEnabled = true;
            this.comboBoxComPort3.Location = new System.Drawing.Point(6, 90);
            this.comboBoxComPort3.Name = "comboBoxComPort3";
            this.comboBoxComPort3.Size = new System.Drawing.Size(121, 24);
            this.comboBoxComPort3.TabIndex = 2;
            this.comboBoxComPort3.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort3_SelectedIndexChanged);
            // 
            // richTextBoxDataPulled
            // 
            this.richTextBoxDataPulled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDataPulled.Location = new System.Drawing.Point(3, 18);
            this.richTextBoxDataPulled.Name = "richTextBoxDataPulled";
            this.richTextBoxDataPulled.Size = new System.Drawing.Size(278, 358);
            this.richTextBoxDataPulled.TabIndex = 2;
            this.richTextBoxDataPulled.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBoxDataPulled);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 379);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opsamlet data";
            // 
            // buttonRefreshComPorts
            // 
            this.buttonRefreshComPorts.Location = new System.Drawing.Point(6, 120);
            this.buttonRefreshComPorts.Name = "buttonRefreshComPorts";
            this.buttonRefreshComPorts.Size = new System.Drawing.Size(121, 26);
            this.buttonRefreshComPorts.TabIndex = 3;
            this.buttonRefreshComPorts.Text = "Opdater porte";
            this.buttonRefreshComPorts.UseVisualStyleBackColor = true;
            this.buttonRefreshComPorts.Click += new System.EventHandler(this.buttonRefreshComPorts_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 115200;
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // serialPort3
            // 
            this.serialPort3.BaudRate = 115200;
            this.serialPort3.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort3_DataReceived);
            // 
            // timerSerialPortPull
            // 
            this.timerSerialPortPull.Interval = 1000;
            this.timerSerialPortPull.Tick += new System.EventHandler(this.timerSerialPortPull_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonClear);
            this.groupBox3.Controls.Add(this.labelPullStatus);
            this.groupBox3.Controls.Add(this.buttonStopPulling);
            this.groupBox3.Controls.Add(this.buttonStartPulling);
            this.groupBox3.Location = new System.Drawing.Point(293, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 152);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie status";
            // 
            // buttonStartPulling
            // 
            this.buttonStartPulling.Location = new System.Drawing.Point(6, 22);
            this.buttonStartPulling.Name = "buttonStartPulling";
            this.buttonStartPulling.Size = new System.Drawing.Size(121, 32);
            this.buttonStartPulling.TabIndex = 0;
            this.buttonStartPulling.Text = "Begynd pull";
            this.buttonStartPulling.UseVisualStyleBackColor = true;
            this.buttonStartPulling.Click += new System.EventHandler(this.buttonStartPulling_Click);
            // 
            // buttonStopPulling
            // 
            this.buttonStopPulling.Location = new System.Drawing.Point(6, 60);
            this.buttonStopPulling.Name = "buttonStopPulling";
            this.buttonStopPulling.Size = new System.Drawing.Size(121, 30);
            this.buttonStopPulling.TabIndex = 1;
            this.buttonStopPulling.Text = "Stop";
            this.buttonStopPulling.UseVisualStyleBackColor = true;
            this.buttonStopPulling.Click += new System.EventHandler(this.buttonStopPulling_Click);
            // 
            // labelPullStatus
            // 
            this.labelPullStatus.AutoSize = true;
            this.labelPullStatus.Location = new System.Drawing.Point(3, 132);
            this.labelPullStatus.Name = "labelPullStatus";
            this.labelPullStatus.Size = new System.Drawing.Size(85, 17);
            this.labelPullStatus.TabIndex = 2;
            this.labelPullStatus.Text = "Samler ikke.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(612, 394);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(7, 96);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 30);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Ryd";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonStartServer);
            this.groupBox4.Location = new System.Drawing.Point(437, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(163, 100);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Server";
            // 
            // buttonStartServer
            // 
            this.buttonStartServer.Location = new System.Drawing.Point(6, 30);
            this.buttonStartServer.Name = "buttonStartServer";
            this.buttonStartServer.Size = new System.Drawing.Size(106, 31);
            this.buttonStartServer.TabIndex = 0;
            this.buttonStartServer.Text = "Start server";
            this.buttonStartServer.UseVisualStyleBackColor = true;
            this.buttonStartServer.Click += new System.EventHandler(this.buttonStartServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 394);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxComPort1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxComPort3;
        private System.Windows.Forms.ComboBox comboBoxComPort2;
        private System.Windows.Forms.RichTextBox richTextBoxDataPulled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRefreshComPorts;
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.IO.Ports.SerialPort serialPort3;
        private System.Windows.Forms.Timer timerSerialPortPull;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelPullStatus;
        private System.Windows.Forms.Button buttonStopPulling;
        private System.Windows.Forms.Button buttonStartPulling;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonStartServer;
    }
}


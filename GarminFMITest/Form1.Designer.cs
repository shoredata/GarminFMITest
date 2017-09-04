namespace Midcom_Office
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
      this.listoutput = new System.Windows.Forms.ListBox();
      this.comboport = new System.Windows.Forms.ComboBox();
      this.label_com = new System.Windows.Forms.Label();
      this.buttonstart = new System.Windows.Forms.Button();
      this.buttontestbadmessages = new System.Windows.Forms.Button();
      this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
      this.checkpcm = new System.Windows.Forms.CheckBox();
      this.buttonproductandsupport = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.labelrx = new System.Windows.Forms.Label();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // listoutput
      // 
      this.listoutput.FormattingEnabled = true;
      this.listoutput.Location = new System.Drawing.Point(12, 125);
      this.listoutput.Name = "listoutput";
      this.listoutput.Size = new System.Drawing.Size(484, 290);
      this.listoutput.TabIndex = 0;
      // 
      // comboport
      // 
      this.comboport.FormattingEnabled = true;
      this.comboport.Location = new System.Drawing.Point(430, 11);
      this.comboport.Name = "comboport";
      this.comboport.Size = new System.Drawing.Size(61, 21);
      this.comboport.TabIndex = 1;
      // 
      // label_com
      // 
      this.label_com.AutoSize = true;
      this.label_com.Location = new System.Drawing.Point(393, 14);
      this.label_com.Name = "label_com";
      this.label_com.Size = new System.Drawing.Size(26, 13);
      this.label_com.TabIndex = 2;
      this.label_com.Text = "Port";
      // 
      // buttonstart
      // 
      this.buttonstart.Enabled = false;
      this.buttonstart.Location = new System.Drawing.Point(397, 61);
      this.buttonstart.Name = "buttonstart";
      this.buttonstart.Size = new System.Drawing.Size(94, 22);
      this.buttonstart.TabIndex = 3;
      this.buttonstart.Text = "0. Start";
      this.buttonstart.UseVisualStyleBackColor = true;
      // 
      // buttontestbadmessages
      // 
      this.buttontestbadmessages.Enabled = false;
      this.buttontestbadmessages.Location = new System.Drawing.Point(12, 10);
      this.buttontestbadmessages.Name = "buttontestbadmessages";
      this.buttontestbadmessages.Size = new System.Drawing.Size(142, 22);
      this.buttontestbadmessages.TabIndex = 4;
      this.buttontestbadmessages.Text = "1. Test Bad Messages";
      this.buttontestbadmessages.UseVisualStyleBackColor = true;
      this.buttontestbadmessages.Click += new System.EventHandler(this.buttontestbadmessages_Click);
      // 
      // serialPort1
      // 
      this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
      // 
      // checkpcm
      // 
      this.checkpcm.AutoSize = true;
      this.checkpcm.Location = new System.Drawing.Point(350, 38);
      this.checkpcm.Name = "checkpcm";
      this.checkpcm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.checkpcm.Size = new System.Drawing.Size(141, 17);
      this.checkpcm.TabIndex = 5;
      this.checkpcm.Text = "Connected to PCM AUX";
      this.checkpcm.UseVisualStyleBackColor = true;
      // 
      // buttonproductandsupport
      // 
      this.buttonproductandsupport.Enabled = false;
      this.buttonproductandsupport.Location = new System.Drawing.Point(12, 34);
      this.buttonproductandsupport.Name = "buttonproductandsupport";
      this.buttonproductandsupport.Size = new System.Drawing.Size(41, 22);
      this.buttonproductandsupport.TabIndex = 6;
      this.buttonproductandsupport.Text = "2. Product & Support";
      this.buttonproductandsupport.UseMnemonic = false;
      this.buttonproductandsupport.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(160, 10);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(142, 22);
      this.button1.TabIndex = 7;
      this.button1.Text = "11. Enable FMI b";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // labelrx
      // 
      this.labelrx.AutoSize = true;
      this.labelrx.Location = new System.Drawing.Point(18, 65);
      this.labelrx.Name = "labelrx";
      this.labelrx.Size = new System.Drawing.Size(35, 13);
      this.labelrx.TabIndex = 8;
      this.labelrx.Text = "label1";
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(160, 38);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(142, 22);
      this.button2.TabIndex = 9;
      this.button2.Text = "12. Product ID & Support b";
      this.button2.UseMnemonic = false;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(160, 66);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(142, 22);
      this.button3.TabIndex = 10;
      this.button3.Text = "13. Get PVT b";
      this.button3.UseMnemonic = false;
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(160, 94);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(142, 22);
      this.button4.TabIndex = 11;
      this.button4.Text = "14. Send Stop b";
      this.button4.UseMnemonic = false;
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(508, 427);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.labelrx);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.buttonproductandsupport);
      this.Controls.Add(this.checkpcm);
      this.Controls.Add(this.buttontestbadmessages);
      this.Controls.Add(this.buttonstart);
      this.Controls.Add(this.label_com);
      this.Controls.Add(this.comboport);
      this.Controls.Add(this.listoutput);
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MIDCOM - Garmin FMI Test";
      this.Activated += new System.EventHandler(this.Form1_Activated);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox listoutput;
    private System.Windows.Forms.ComboBox comboport;
    private System.Windows.Forms.Label label_com;
    private System.Windows.Forms.Button buttonstart;
    private System.Windows.Forms.Button buttontestbadmessages;
    private System.IO.Ports.SerialPort serialPort1;
    private System.Windows.Forms.CheckBox checkpcm;
    private System.Windows.Forms.Button buttonproductandsupport;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label labelrx;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
  }
}


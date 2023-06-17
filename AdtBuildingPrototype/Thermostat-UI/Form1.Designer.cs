using System.Timers;

namespace Thermostat_UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components=new System.ComponentModel.Container();
            txtLog=new TextBox();
            splitter1=new Splitter();
            label3=new Label();
            label2=new Label();
            trackBar1=new TrackBar();
            timer1=new System.Windows.Forms.Timer(components);
            chkRandom=new CheckBox();
            label1=new Label();
            lblCurrentTemp=new Label();
            label5=new Label();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.Dock=DockStyle.Bottom;
            txtLog.Location=new Point(0, 97);
            txtLog.Multiline=true;
            txtLog.Name="txtLog";
            txtLog.Size=new Size(359, 52);
            txtLog.TabIndex=24;
            // 
            // splitter1
            // 
            splitter1.Dock=DockStyle.Bottom;
            splitter1.Location=new Point(0, 94);
            splitter1.Name="splitter1";
            splitter1.Size=new Size(359, 3);
            splitter1.TabIndex=25;
            splitter1.TabStop=false;
            // 
            // label3
            // 
            label3.AutoSize=true;
            label3.Location=new Point(302, 50);
            label3.Name="label3";
            label3.Size=new Size(19, 15);
            label3.TabIndex=28;
            label3.Text="90";
            // 
            // label2
            // 
            label2.AutoSize=true;
            label2.Location=new Point(100, 50);
            label2.Name="label2";
            label2.Size=new Size(19, 15);
            label2.TabIndex=27;
            label2.Text="60";
            // 
            // trackBar1
            // 
            trackBar1.Location=new Point(125, 42);
            trackBar1.Maximum=90;
            trackBar1.Minimum=60;
            trackBar1.Name="trackBar1";
            trackBar1.Size=new Size(171, 45);
            trackBar1.TabIndex=26;
            trackBar1.Value=75;
            // 
            // timer1
            // 
            timer1.Interval=5000;
            timer1.Tick+=timer1_Tick;
            // 
            // chkRandom
            // 
            chkRandom.AutoSize=true;
            chkRandom.Location=new Point(221, 12);
            chkRandom.Name="chkRandom";
            chkRandom.Size=new Size(113, 19);
            chkRandom.TabIndex=29;
            chkRandom.Text="Random Temps?";
            chkRandom.UseVisualStyleBackColor=true;
            // 
            // label1
            // 
            label1.AutoSize=true;
            label1.Font=new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location=new Point(12, 9);
            label1.Name="label1";
            label1.Size=new Size(107, 21);
            label1.TabIndex=30;
            label1.Text="Current Temp:";
            label1.Click+=label1_Click;
            // 
            // lblCurrentTemp
            // 
            lblCurrentTemp.AutoSize=true;
            lblCurrentTemp.Font=new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblCurrentTemp.Location=new Point(125, 9);
            lblCurrentTemp.Name="lblCurrentTemp";
            lblCurrentTemp.Size=new Size(40, 21);
            lblCurrentTemp.TabIndex=31;
            lblCurrentTemp.Text="75 F";
            lblCurrentTemp.Click+=label4_Click;
            // 
            // label5
            // 
            label5.AutoSize=true;
            label5.Font=new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location=new Point(12, 42);
            label5.Name="label5";
            label5.Size=new Size(74, 21);
            label5.TabIndex=32;
            label5.Text="Set Point:";
            label5.Click+=label5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(359, 149);
            Controls.Add(label5);
            Controls.Add(lblCurrentTemp);
            Controls.Add(label1);
            Controls.Add(chkRandom);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(trackBar1);
            Controls.Add(splitter1);
            Controls.Add(txtLog);
            Name="Form1";
            Text="Thermostat";
            Load+=Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtLog;
        private Splitter splitter1;
        private Label label3;
        private Label label2;
        private TrackBar trackBar1;
        private System.Windows.Forms.Timer timer1;
        private CheckBox chkRandom;
        private Label label1;
        private Label lblCurrentTemp;
        private Label label5;
    }
}
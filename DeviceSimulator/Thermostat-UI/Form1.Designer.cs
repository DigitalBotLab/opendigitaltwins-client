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
            grpMode=new GroupBox();
            radioButton1=new RadioButton();
            radioButton2=new RadioButton();
            radioButton3=new RadioButton();
            grpFan=new GroupBox();
            radioButton4=new RadioButton();
            radioButton5=new RadioButton();
            radioButton6=new RadioButton();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            grpMode.SuspendLayout();
            grpFan.SuspendLayout();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.Dock=DockStyle.Bottom;
            txtLog.Location=new Point(0, 208);
            txtLog.Multiline=true;
            txtLog.Name="txtLog";
            txtLog.Size=new Size(359, 52);
            txtLog.TabIndex=24;
            // 
            // splitter1
            // 
            splitter1.Dock=DockStyle.Bottom;
            splitter1.Location=new Point(0, 205);
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
            // grpMode
            // 
            grpMode.Controls.Add(radioButton3);
            grpMode.Controls.Add(radioButton2);
            grpMode.Controls.Add(radioButton1);
            grpMode.Location=new Point(12, 83);
            grpMode.Name="grpMode";
            grpMode.Size=new Size(335, 49);
            grpMode.TabIndex=33;
            grpMode.TabStop=false;
            grpMode.Text="Mode";
            grpMode.Enter+=groupBox1_Enter;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize=true;
            radioButton1.Location=new Point(25, 22);
            radioButton1.Name="radioButton1";
            radioButton1.Size=new Size(67, 19);
            radioButton1.TabIndex=0;
            radioButton1.TabStop=true;
            radioButton1.Text="Cooling";
            radioButton1.UseVisualStyleBackColor=true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize=true;
            radioButton2.Location=new Point(130, 22);
            radioButton2.Name="radioButton2";
            radioButton2.Size=new Size(67, 19);
            radioButton2.TabIndex=1;
            radioButton2.TabStop=true;
            radioButton2.Text="Heating";
            radioButton2.UseVisualStyleBackColor=true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize=true;
            radioButton3.Location=new Point(233, 22);
            radioButton3.Name="radioButton3";
            radioButton3.Size=new Size(42, 19);
            radioButton3.TabIndex=2;
            radioButton3.TabStop=true;
            radioButton3.Text="Off";
            radioButton3.UseVisualStyleBackColor=true;
            radioButton3.CheckedChanged+=radioButton3_CheckedChanged;
            // 
            // grpFan
            // 
            grpFan.Controls.Add(radioButton4);
            grpFan.Controls.Add(radioButton5);
            grpFan.Controls.Add(radioButton6);
            grpFan.Location=new Point(12, 138);
            grpFan.Name="grpFan";
            grpFan.Size=new Size(335, 49);
            grpFan.TabIndex=34;
            grpFan.TabStop=false;
            grpFan.Text="Fan";
            grpFan.Enter+=groupBox2_Enter;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize=true;
            radioButton4.Location=new Point(233, 22);
            radioButton4.Name="radioButton4";
            radioButton4.Size=new Size(41, 19);
            radioButton4.TabIndex=2;
            radioButton4.TabStop=true;
            radioButton4.Text="On";
            radioButton4.UseVisualStyleBackColor=true;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize=true;
            radioButton5.Location=new Point(113, 22);
            radioButton5.Name="radioButton5";
            radioButton5.Size=new Size(72, 19);
            radioButton5.TabIndex=1;
            radioButton5.TabStop=true;
            radioButton5.Text="Circulate";
            radioButton5.UseVisualStyleBackColor=true;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize=true;
            radioButton6.Location=new Point(23, 22);
            radioButton6.Name="radioButton6";
            radioButton6.Size=new Size(51, 19);
            radioButton6.TabIndex=0;
            radioButton6.TabStop=true;
            radioButton6.Text="Auto";
            radioButton6.UseVisualStyleBackColor=true;
            // 
            // Form1
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(359, 260);
            Controls.Add(grpFan);
            Controls.Add(grpMode);
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
            grpMode.ResumeLayout(false);
            grpMode.PerformLayout();
            grpFan.ResumeLayout(false);
            grpFan.PerformLayout();
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
        private GroupBox grpMode;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private GroupBox grpFan;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
    }
}
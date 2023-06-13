namespace Thermostat_UI
{
    partial class Thermostat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label3=new Label();
            label2=new Label();
            trackBar1=new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize=true;
            label3.Location=new Point(174, 11);
            label3.Name="label3";
            label3.Size=new Size(19, 15);
            label3.TabIndex=12;
            label3.Text="90";
            // 
            // label2
            // 
            label2.AutoSize=true;
            label2.Location=new Point(3, 11);
            label2.Name="label2";
            label2.Size=new Size(19, 15);
            label2.TabIndex=11;
            label2.Text="60";
            // 
            // trackBar1
            // 
            trackBar1.Location=new Point(25, 3);
            trackBar1.Maximum=90;
            trackBar1.Minimum=60;
            trackBar1.Name="trackBar1";
            trackBar1.Size=new Size(143, 45);
            trackBar1.TabIndex=10;
            trackBar1.Value=75;
            // 
            // Thermostat
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(trackBar1);
            Name="Thermostat";
            Size=new Size(200, 42);
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private Label label2;
        private TrackBar trackBar1;
    }
}

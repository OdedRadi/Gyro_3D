namespace Gyro_3D
{
	partial class GyroForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.hScrollBarDelay = new System.Windows.Forms.HScrollBar();
			this.buttonRun = new System.Windows.Forms.Button();
			this.buttonWider = new System.Windows.Forms.Button();
			this.buttonStep = new System.Windows.Forms.Button();
			this.drawPanel = new System.Windows.Forms.Panel();
			this.timerRun = new System.Windows.Forms.Timer(this.components);
			this.timerRepaint = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(171, 337);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Delay = 00";
			// 
			// hScrollBarDelay
			// 
			this.hScrollBarDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBarDelay.LargeChange = 1;
			this.hScrollBarDelay.Location = new System.Drawing.Point(232, 336);
			this.hScrollBarDelay.Maximum = 200;
			this.hScrollBarDelay.Minimum = 1;
			this.hScrollBarDelay.Name = "hScrollBarDelay";
			this.hScrollBarDelay.Size = new System.Drawing.Size(62, 14);
			this.hScrollBarDelay.TabIndex = 10;
			this.hScrollBarDelay.Value = 10;
			this.hScrollBarDelay.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarDelay_Scroll);
			// 
			// buttonRun
			// 
			this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRun.Location = new System.Drawing.Point(297, 332);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(75, 23);
			this.buttonRun.TabIndex = 9;
			this.buttonRun.Text = "Run";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
			// 
			// buttonWider
			// 
			this.buttonWider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonWider.Location = new System.Drawing.Point(12, 332);
			this.buttonWider.Name = "buttonWider";
			this.buttonWider.Size = new System.Drawing.Size(75, 23);
			this.buttonWider.TabIndex = 8;
			this.buttonWider.Text = "Wider";
			this.buttonWider.UseVisualStyleBackColor = true;
			this.buttonWider.Click += new System.EventHandler(this.buttonWider_Click);
			// 
			// buttonStep
			// 
			this.buttonStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonStep.Location = new System.Drawing.Point(90, 332);
			this.buttonStep.Name = "buttonStep";
			this.buttonStep.Size = new System.Drawing.Size(75, 23);
			this.buttonStep.TabIndex = 7;
			this.buttonStep.Text = "Step";
			this.buttonStep.UseVisualStyleBackColor = true;
			this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
			// 
			// drawPanel
			// 
			this.drawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drawPanel.Location = new System.Drawing.Point(12, 12);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(360, 314);
			this.drawPanel.TabIndex = 12;
			// 
			// timerRun
			// 
			this.timerRun.Interval = 10;
			this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
			// 
			// timerRepaint
			// 
			this.timerRepaint.Interval = 10;
			this.timerRepaint.Tick += new System.EventHandler(this.timerRepaint_Tick);
			// 
			// GyroForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 361);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.hScrollBarDelay);
			this.Controls.Add(this.buttonRun);
			this.Controls.Add(this.buttonWider);
			this.Controls.Add(this.buttonStep);
			this.Controls.Add(this.drawPanel);
			this.Name = "GyroForm";
			this.Text = "Gyro 3D";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.HScrollBar hScrollBarDelay;
		private System.Windows.Forms.Button buttonRun;
		private System.Windows.Forms.Button buttonWider;
		private System.Windows.Forms.Button buttonStep;
		private System.Windows.Forms.Panel drawPanel;
		private System.Windows.Forms.Timer timerRun;
		private System.Windows.Forms.Timer timerRepaint;
	}
}
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.scrollBarPrismRadius = new System.Windows.Forms.HScrollBar();
			this.scrollBarPrismZSize = new System.Windows.Forms.HScrollBar();
			this.scrollBarPyramidZSize = new System.Windows.Forms.HScrollBar();
			this.scrollBarCubeZSize = new System.Windows.Forms.HScrollBar();
			this.scrollBarGyroRadius = new System.Windows.Forms.HScrollBar();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(171, 357);
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
			this.hScrollBarDelay.Location = new System.Drawing.Point(232, 356);
			this.hScrollBarDelay.Maximum = 200;
			this.hScrollBarDelay.Minimum = 1;
			this.hScrollBarDelay.Name = "hScrollBarDelay";
			this.hScrollBarDelay.Size = new System.Drawing.Size(331, 14);
			this.hScrollBarDelay.TabIndex = 10;
			this.hScrollBarDelay.Value = 10;
			this.hScrollBarDelay.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarDelay_Scroll);
			// 
			// buttonRun
			// 
			this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRun.Location = new System.Drawing.Point(566, 352);
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
			this.buttonWider.Location = new System.Drawing.Point(12, 352);
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
			this.buttonStep.Location = new System.Drawing.Point(90, 352);
			this.buttonStep.Name = "buttonStep";
			this.buttonStep.Size = new System.Drawing.Size(75, 23);
			this.buttonStep.TabIndex = 7;
			this.buttonStep.Text = "Step";
			this.buttonStep.UseVisualStyleBackColor = true;
			this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
			// 
			// drawPanel
			// 
			this.drawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drawPanel.Location = new System.Drawing.Point(12, 12);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(418, 334);
			this.drawPanel.TabIndex = 12;
			// 
			// timerRotating
			// 
			this.timerRun.Interval = 10;
			this.timerRun.Tick += new System.EventHandler(this.timerRotating_Tick);
			// 
			// timerRepaint
			// 
			this.timerRepaint.Interval = 10;
			this.timerRepaint.Tick += new System.EventHandler(this.timerRepaint_Tick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.scrollBarPrismRadius);
			this.groupBox1.Controls.Add(this.scrollBarPrismZSize);
			this.groupBox1.Controls.Add(this.scrollBarPyramidZSize);
			this.groupBox1.Controls.Add(this.scrollBarCubeZSize);
			this.groupBox1.Controls.Add(this.scrollBarGyroRadius);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(436, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(205, 143);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Cube Size";
			// 
			// scrollBarPrismRadius
			// 
			this.scrollBarPrismRadius.LargeChange = 1;
			this.scrollBarPrismRadius.Location = new System.Drawing.Point(78, 120);
			this.scrollBarPrismRadius.Maximum = 30;
			this.scrollBarPrismRadius.Minimum = 10;
			this.scrollBarPrismRadius.Name = "scrollBarPrismRadius";
			this.scrollBarPrismRadius.Size = new System.Drawing.Size(118, 13);
			this.scrollBarPrismRadius.TabIndex = 6;
			this.scrollBarPrismRadius.Value = 10;
			this.scrollBarPrismRadius.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarPrismRadius_Scroll);
			// 
			// scrollBarPrismZSize
			// 
			this.scrollBarPrismZSize.LargeChange = 1;
			this.scrollBarPrismZSize.Location = new System.Drawing.Point(78, 103);
			this.scrollBarPrismZSize.Maximum = 30;
			this.scrollBarPrismZSize.Minimum = 10;
			this.scrollBarPrismZSize.Name = "scrollBarPrismZSize";
			this.scrollBarPrismZSize.Size = new System.Drawing.Size(118, 13);
			this.scrollBarPrismZSize.TabIndex = 7;
			this.scrollBarPrismZSize.Value = 10;
			this.scrollBarPrismZSize.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarPrismHeight_Scroll);
			// 
			// scrollBarPyramidZSize
			// 
			this.scrollBarPyramidZSize.LargeChange = 1;
			this.scrollBarPyramidZSize.Location = new System.Drawing.Point(78, 74);
			this.scrollBarPyramidZSize.Maximum = 30;
			this.scrollBarPyramidZSize.Minimum = 10;
			this.scrollBarPyramidZSize.Name = "scrollBarPyramidZSize";
			this.scrollBarPyramidZSize.Size = new System.Drawing.Size(118, 13);
			this.scrollBarPyramidZSize.TabIndex = 5;
			this.scrollBarPyramidZSize.Value = 10;
			this.scrollBarPyramidZSize.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarPyramidHeight_Scroll);
			// 
			// scrollBarCubeZSize
			// 
			this.scrollBarCubeZSize.LargeChange = 1;
			this.scrollBarCubeZSize.Location = new System.Drawing.Point(78, 57);
			this.scrollBarCubeZSize.Maximum = 30;
			this.scrollBarCubeZSize.Minimum = 10;
			this.scrollBarCubeZSize.Name = "scrollBarCubeZSize";
			this.scrollBarCubeZSize.Size = new System.Drawing.Size(118, 13);
			this.scrollBarCubeZSize.TabIndex = 5;
			this.scrollBarCubeZSize.Value = 10;
			this.scrollBarCubeZSize.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarCubeHeight_Scroll);
			// 
			// scrollBarGyroRadius
			// 
			this.scrollBarGyroRadius.LargeChange = 1;
			this.scrollBarGyroRadius.Location = new System.Drawing.Point(78, 26);
			this.scrollBarGyroRadius.Maximum = 30;
			this.scrollBarGyroRadius.Minimum = 10;
			this.scrollBarGyroRadius.Name = "scrollBarGyroRadius";
			this.scrollBarGyroRadius.Size = new System.Drawing.Size(118, 13);
			this.scrollBarGyroRadius.TabIndex = 1;
			this.scrollBarGyroRadius.Value = 10;
			this.scrollBarGyroRadius.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarGyroRadius_Scroll);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(71, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Prism Radius:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 103);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Prism Z:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 75);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Pyramid Z:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Cube Z:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Gyro Radius:";
			// 
			// GyroForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(653, 381);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.hScrollBarDelay);
			this.Controls.Add(this.buttonRun);
			this.Controls.Add(this.buttonWider);
			this.Controls.Add(this.buttonStep);
			this.Controls.Add(this.drawPanel);
			this.MinimumSize = new System.Drawing.Size(420, 420);
			this.Name = "GyroForm";
			this.Text = "Gyro 3D";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.HScrollBar scrollBarPrismRadius;
        private System.Windows.Forms.HScrollBar scrollBarPrismZSize;
        private System.Windows.Forms.HScrollBar scrollBarPyramidZSize;
        private System.Windows.Forms.HScrollBar scrollBarCubeZSize;
        private System.Windows.Forms.HScrollBar scrollBarGyroRadius;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}
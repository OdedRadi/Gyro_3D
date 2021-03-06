﻿using System;
using System.Windows.Forms;
using OpenGL;

namespace Gyro_3D
{
	public partial class GyroForm : Form
	{
		private gyroOGL gyroGL;
        private bool gyroFell;

		public GyroForm()
		{
			InitializeComponent();
			gyroGL = new gyroOGL(drawPanel);
            gyroFell = false;
        }
        private void timerRotating_Tick(object sender, EventArgs e)
		{
			gyroGL.Draw();
		}

        private void timerRepaint_Tick(object sender, EventArgs e)
		{
			gyroGL.Draw();
			timerRepaint.Enabled = false;
		}

		private void buttonStep_Click(object sender, EventArgs e)
		{
			gyroGL.Draw();
		}

		private void buttonWider_Click(object sender, EventArgs e)
		{
			drawPanel.Width += 10;
			Width += 10;
		}

		private void buttonRun_Click(object sender, EventArgs e)
		{
            timerRun.Enabled = !timerRun.Enabled;
           
            if (timerRun.Enabled)
				buttonRun.Text = "Stop";
			else
				buttonRun.Text = "Run";

			label1.Text = "Delay = " + hScrollBarDelay.Value;
		}

		private void hScrollBarDelay_Scroll(object sender, ScrollEventArgs e)
		{
			timerRun.Interval = hScrollBarDelay.Value;
			label1.Text = "Delay = " + hScrollBarDelay.Value;
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
			gyroGL.Draw();
		}

		private void drawPanel_Resize(object sender, EventArgs e)
		{
			gyroGL.OnResize();
		}

		private void GyroForm_Load(object sender, EventArgs e)
		{

		}

        private void scrollBarGyroRadius_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.CubeWidth = e.NewValue / 10f;
            gyroGL.PyramidWidth = e.NewValue / 10f;
            gyroGL.CubeDepth = e.NewValue / 10f;
            gyroGL.PyramidDepth = e.NewValue / 10f;
        }

        private void scrollBarCubeHeight_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.CubeHeight = e.NewValue / 10f;
        }

        private void scrollBarPyramidHeight_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.PyramidHeight = e.NewValue / 12f;
        }

        private void scrollBarCylinderHeight_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.CylinderHeight = e.NewValue / 20f;
        }

        private void scrollBarCylinderRadius_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.CylinderRadius = e.NewValue / 75f;
            gyroGL.CylinderRadius = e.NewValue / 75f;
        }
    }
}

using System;
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
            gyroGL.GyroFellEvent += GyroGL_gyroFellEvent;
            gyroFell = false;
        }

        private void GyroGL_gyroFellEvent()
        {
            timerRotating.Enabled = false;
            timerSwinging.Enabled = true;
            gyroFell = true;
            timerSwinging.Start();
        }

        private void timerRotating_Tick(object sender, EventArgs e)
		{
			gyroGL.Draw();
		}

        private void timerSwinging_Tick(object sender, EventArgs e)
        {
            //gyroGL.DrawSwinging();
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
            if (gyroFell)
            {
                timerSwinging.Enabled = !timerSwinging.Enabled;
            }
            else
            {
                timerRotating.Enabled = !timerRotating.Enabled;
            }

            if (timerRotating.Enabled)
				buttonRun.Text = "Stop";
			else
				buttonRun.Text = "Run";

			label1.Text = "Delay = " + hScrollBarDelay.Value;
		}

		private void hScrollBarDelay_Scroll(object sender, ScrollEventArgs e)
		{
			timerRotating.Interval = hScrollBarDelay.Value;
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

        private void scrollBarPrismHeight_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.PrismHeight = e.NewValue / 20f;
        }

        private void scrollBarPrismRadius_Scroll(object sender, ScrollEventArgs e)
        {
            gyroGL.PrismWidth = e.NewValue / 50f;
            gyroGL.PrismDepth = e.NewValue / 50f;
        }
    }
}

﻿using System.Windows.Forms;

namespace Gyro_3D
{
	class Program
	{
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new GyroForm());
		}
	}
}

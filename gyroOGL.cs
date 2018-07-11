using System;
using System.Windows.Forms;

namespace OpenGL
{
	class gyroOGL
	{
		private Control m_control;

		private float m_angle = 0.0f;
		private int Width { get; set; }
		private int Height { get; set; }

		private float m_cubeLength = 1.0f;
		private float m_prizmeWidth = 0.2f;
		private float m_prizeHeight = 0.7f;

		public gyroOGL(Control control)
		{
			m_control = control;
			Width = m_control.Width;
			Height = m_control.Height;
			InitializeGL();
		}

		~gyroOGL()
		{
			WGL.wglDeleteContext(m_uint_RC);
		}

		uint m_uint_HWND = 0;
		public uint HWND
		{
			get { return m_uint_HWND; }
		}

		uint m_uint_DC = 0;
		public uint DC
		{
			get { return m_uint_DC; }
		}

		uint m_uint_RC = 0;
		public uint RC
		{
			get { return m_uint_RC; }
		}

		protected void DrawAll()
		{
			drawAxes();
			drawCube();
			drawPrizme();
			drawTriangle();
		}

		private void drawAxes()
		{
			//axes
			GL.glBegin(GL.GL_LINES);
			//x  RED
			GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(3.0f, 0.0f, 0.0f);
			//y  GREEN 
			GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 3.0f, 0.0f);
			//z  BLUE
			GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 3.0f);

			GL.glEnd();
		}

		private void drawPrizme()
		{
			float prizmeOrigin = m_cubeLength / 2 - m_prizmeWidth / 2;

			float prizmeEnd = prizmeOrigin + m_prizmeWidth;
			GL.glBegin(GL.GL_QUADS);
			GL.glColor3f(1.0f, 1.0f, 0);

			//1
			GL.glVertex3f(prizmeOrigin, prizmeOrigin, m_cubeLength);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin, prizmeEnd, m_cubeLength);

			//GL.glColor3f(1.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeEnd, prizmeEnd, m_cubeLength);

			//GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(prizmeEnd, prizmeOrigin, m_cubeLength);

			//2
			GL.glVertex3f(prizmeOrigin, prizmeOrigin, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin, prizmeEnd, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(1.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeEnd, prizmeEnd, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(prizmeEnd, prizmeOrigin, m_cubeLength + m_prizeHeight);

			//3

			//GL.glColor3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin, m_cubeLength);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin, m_cubeLength);

			//4

			//GL.glColor3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin + m_prizmeWidth, m_cubeLength);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin + m_prizmeWidth, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin + m_prizmeWidth, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin + m_prizmeWidth, m_cubeLength);

			//5

			//GL.glColor3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin, m_cubeLength);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin + m_prizmeWidth, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin, prizmeOrigin + m_prizmeWidth, m_cubeLength);

			//6

			//GL.glColor3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin, m_cubeLength);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin + m_prizmeWidth, m_cubeLength + m_prizeHeight);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(prizmeOrigin + m_prizmeWidth, prizmeOrigin + m_prizmeWidth, m_cubeLength);

			GL.glEnd();
		}

		private void drawCube()
		{
			GL.glBegin(GL.GL_QUADS);

			//1
			GL.glColor3f(m_cubeLength, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(0.0f, m_cubeLength, 0.0f);

			//GL.glColor3f(1.0f, 1.0f, 0.0f);
			GL.glVertex3f(1.0f, m_cubeLength, 0.0f);

			//GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(m_cubeLength, 0.0f, 0.0f);

			//2

			//GL.glColor3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(0.0f, 0.0f, m_cubeLength);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(0.0f, m_cubeLength, m_cubeLength);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(0.0f, m_cubeLength, 0.0f);


			//3

			//GL.glColor3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);

			//GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(m_cubeLength, 0.0f, 0.0f);

			//GL.glColor3f(1.0f, 0.0f, 1.0f);
			GL.glVertex3f(m_cubeLength, 0.0f, m_cubeLength);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(0.0f, 0.0f, m_cubeLength);


			//4

			//GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(m_cubeLength, 0.0f, 0.0f);

			//GL.glColor3f(1.0f, 0.0f, 1.0f);
			GL.glVertex3f(m_cubeLength, 0.0f, m_cubeLength);

			//GL.glColor3f(1.0f, 1.0f, 1.0f);
			GL.glVertex3f(m_cubeLength, m_cubeLength, m_cubeLength);

			//GL.glColor3f(1.0f, 1.0f, 0.0f);
			GL.glVertex3f(m_cubeLength, m_cubeLength, 0.0f);


			//5

			//GL.glColor3f(1.0f, 1.0f, 1.0f);
			GL.glVertex3f(m_cubeLength, m_cubeLength, m_cubeLength);

			//GL.glColor3f(1.0f, 1.0f, 0.0f);
			GL.glVertex3f(m_cubeLength, m_cubeLength, 0.0f);

			//GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(0.0f, m_cubeLength, 0.0f);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(0.0f, m_cubeLength, m_cubeLength);


			//6

			//GL.glColor3f(1.0f, 1.0f, 1.0f);
			GL.glVertex3f(m_cubeLength, m_cubeLength, m_cubeLength);

			//GL.glColor3f(0.0f, 1.0f, 1.0f);
			GL.glVertex3f(0.0f, m_cubeLength, m_cubeLength);

			//GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(0.0f, 0.0f, m_cubeLength);

			//GL.glColor3f(1.0f, 0.0f, 1.0f);
			GL.glVertex3f(m_cubeLength, 0.0f, m_cubeLength);


			GL.glEnd();
		}

		private void drawTriangular()
		{
			// TODO
		}

		public void Draw()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;

			GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
			GL.glLoadIdentity();


			GL.glTranslatef(0.0f, 0.0f, -3.0f); // Translate 6 Units Into The Screen

			m_angle += 5.0f;
			GL.glRotatef(m_angle, 1.0f, 1.0f, 1.0f);

			DrawAll();

			GL.glFlush();

			WGL.wglSwapBuffers(m_uint_DC);

		}

		protected virtual void InitializeGL()
		{
			m_uint_HWND = (uint)m_control.Handle.ToInt32();
			m_uint_DC = WGL.GetDC(m_uint_HWND);

			// Not doing the following WGL.wglSwapBuffers() on the DC will
			// result in a failure to subsequently create the RC.
			WGL.wglSwapBuffers(m_uint_DC);

			WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
			WGL.ZeroPixelDescriptor(ref pfd);
			pfd.nVersion = 1;
			pfd.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
			pfd.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
			pfd.cColorBits = 32;
			pfd.cDepthBits = 32;
			pfd.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);

			int pixelFormatIndex = 0;
			pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
			if (pixelFormatIndex == 0)
			{
				MessageBox.Show("Unable to retrieve pixel format");
				return;
			}

			if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0)
			{
				MessageBox.Show("Unable to set pixel format");
				return;
			}
			//Create rendering context
			m_uint_RC = WGL.wglCreateContext(m_uint_DC);
			if (m_uint_RC == 0)
			{
				MessageBox.Show("Unable to get rendering context");
				return;
			}
			if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
			{
				MessageBox.Show("Unable to make rendering context current");
				return;
			}


			initRenderingGL();
		}

		public void OnResize()
		{
			Width = m_control.Width;
			Height = m_control.Height;

			//!!!!!!!
			GL.glViewport(0, 0, Width, Height);
			//!!!!!!!

			initRenderingGL();
			Draw();
		}

		protected virtual void initRenderingGL()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;
			if (this.Width == 0 || this.Height == 0)
				return;
			GL.glEnable(GL.GL_DEPTH_TEST);
			GL.glDepthFunc(GL.GL_LEQUAL);

			GL.glViewport(0, 0, Width, Height);
			GL.glClearColor(0, 0, 0, 0);
			GL.glMatrixMode(GL.GL_PROJECTION);
			GL.glLoadIdentity();
			double d = (Width) / Height;

			GLU.gluPerspective(100.0, (Width) / Height, 1.0, 1000.0);
			GL.glMatrixMode(GL.GL_MODELVIEW);
			GL.glLoadIdentity();
		}
	}
}

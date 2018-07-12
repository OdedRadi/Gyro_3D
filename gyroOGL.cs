using System;
using System.Windows.Forms;

namespace OpenGL
{
	class gyroOGL
	{
		private enum eAxis
		{
			X,
			Y,
			Z
		}

		private Control m_control;

		private float m_angle = 0.0f;
		private int Width { get; set; }
		private int Height { get; set; }

		private float m_cubeHeight = 1.0f;
		private float m_cubeWidth = 1.0f;
		private float m_prismHeight = 0.7f;
		private float m_prismWidth = 0.2f;

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
			// drawing the axes, the cube and the pyramid from the current position
			drawAxes();
			drawGyroCube();
			drawGyroPyramid();

			// translate the position to draw the prism on the top of the cube
			float xPrismOrigin = (m_cubeWidth / 2 - m_prismWidth / 2);
			float yPrismOrigin = (m_cubeWidth / 2 - m_prismWidth / 2);
			float zPrismOrigin = m_cubeHeight;

			GL.glTranslatef(xPrismOrigin, yPrismOrigin, zPrismOrigin);
			drawGyroPrism();
		}

		private void drawAxes()
		{
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

		private void drawGyroPrism()
		{
			GL.glBegin(GL.GL_QUADS);

			drawGenericCube(m_prismWidth, m_prismHeight);

			GL.glEnd();
		}

		private void drawGyroCube()
		{
			GL.glBegin(GL.GL_QUADS);

			drawGenericCube(m_cubeHeight, m_cubeWidth);

			GL.glEnd();
		}

		private void drawGyroPyramid()
		{
			// TODO
		}

		private void drawGenericCube(float width, float height)
		{
			GL.glColor3f(1.0f, 0.0f, 0.0f);
			drawParallelSquareSurfaces(width, height, eAxis.X);

			GL.glColor3f(0.0f, 1.0f, 0.0f);
			drawParallelSquareSurfaces(width, height, eAxis.Y);

			GL.glColor3f(0.0f, 0.0f, 1.0f);
			drawParallelSquareSurfaces(width, height, eAxis.Z);
		}

		private void drawParallelSquareSurfaces(float width, float height, eAxis axis)
		{
			float surfacesDistance = axis == eAxis.Z ? height : width;

			for (float i = 0; i < surfacesDistance * 2; i += surfacesDistance)
			{
				switch (axis)
				{
					case eAxis.X:
						GL.glVertex3f(i, 0, 0);
						GL.glVertex3f(i, 0, height);
						GL.glVertex3f(i, width, height);
						GL.glVertex3f(i, width, 0);
						break;
					case eAxis.Y:
						GL.glVertex3f(0, i, 0);
						GL.glVertex3f(0, i, height);
						GL.glVertex3f(width, i, height);
						GL.glVertex3f(width, i, 0);
						break;
					case eAxis.Z:
						GL.glVertex3f(0, 0, i);
						GL.glVertex3f(0, width, i);
						GL.glVertex3f(width, width, i);
						GL.glVertex3f(width, 0, i);
						break;
				}
			}
		}

		public void Draw()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;

			GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
			GL.glLoadIdentity();


			GL.glTranslatef(0.0f, 0.0f, -3.0f); // Translate 6 Units Into The Screen

			m_angle += 5.0f;
			GL.glRotatef(m_angle, 1.0f, 1.0f, 0.0f);

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

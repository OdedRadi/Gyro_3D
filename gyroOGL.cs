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

		// Width - x axis, Height - y axis, Depth - z axis
		private float m_cubeHeight = 1.0f;
		private float m_cubeWidth = 1.0f;
		private float m_cubeDepth = 1.0f;
		private float m_prismHeight = 0.2f;
		private float m_prismWidth = 0.2f;
		private float m_prismDepth = 0.7f;
        private float m_pyramidHeight = 1.0f;
        private float m_pyramidWidth = 1.0f;
        private float m_pyramidDepth = 0.5f;

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
			drawGenericCube(m_prismWidth, m_prismHeight, m_prismDepth);
		}

		private void drawGyroCube()
		{
			drawGenericCube(m_cubeHeight, m_cubeWidth, m_cubeDepth);
		}

		private void drawGyroPyramid()
		{
            GL.glRotatef(180.0f, 1.0f, 1.0f, 0.0f);

            drawSquareSurface(m_pyramidWidth, m_pyramidHeight, 0.0f, eAxis.Z);
            
            GL.glBegin(GL.GL_TRIANGLES);

            // first eAxis.x
            GL.glColor3f(1.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.5f, 0.5f, 1.0f);
            GL.glVertex3f(0.0f, 1.0f, 0.0f);

            // second eAxis.x
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.5f, 0.5f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f, 0.0f);
            // first eAxis.y
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.5f, 0.5f, 1.0f);
            GL.glVertex3f(1.0f, 0.0f, 0.0f);

            // second eAxis.y
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.5f, 0.5f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f, 0.0f);


            GL.glEnd();
            GL.glRotatef(180.0f, 1.0f, 1.0f, 0.0f);
        }

		private void drawGenericCube(float width, float height, float depth)
		{
			GL.glColor3f(1.0f, 0.0f, 0.0f);
			drawSquareSurface(0, height, depth, eAxis.X);
			drawSquareSurface(width, height, depth, eAxis.X);

			GL.glColor3f(0.0f, 1.0f, 0.0f);
			drawSquareSurface(width, 0, depth, eAxis.Y);
			drawSquareSurface(width, height, depth, eAxis.Y);

			GL.glColor3f(0.0f, 0.0f, 1.0f);
			drawSquareSurface(width, height, 0, eAxis.Z);
			drawSquareSurface(width, height, depth, eAxis.Z);			
		}

		private void drawSquareSurface(float width, float height, float depth, eAxis axis)
		{
            GL.glBegin(GL.GL_QUADS);

            switch (axis)
			{
				case eAxis.X:
					GL.glVertex3f(width, 0, 0);
					GL.glVertex3f(width, 0, depth);
					GL.glVertex3f(width, height, depth);
					GL.glVertex3f(width, height, 0);
					break;
				case eAxis.Y:
					GL.glVertex3f(0, height, 0);
					GL.glVertex3f(0, height, depth);
					GL.glVertex3f(width, height, depth);
					GL.glVertex3f(width, height, 0);
					break;
				case eAxis.Z:
					GL.glVertex3f(0, 0, depth);
					GL.glVertex3f(0, height, depth);
					GL.glVertex3f(width, height, depth);
					GL.glVertex3f(width, 0, depth);
					break;
			}

            GL.glEnd();
        }

		public void Draw()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;

			GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
			GL.glLoadIdentity();


			GL.glTranslatef(0.0f, 0.0f, -3.0f); // Translate 6 Units Into The Screen

			m_angle += 1.0f;
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

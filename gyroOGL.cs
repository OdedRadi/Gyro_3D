using System;
using System.Drawing;
using System.Drawing.Imaging;
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

        private enum eGyroState
        {
            Rotating,
            Slowing,
            Stoping
        }

        private Control m_control;
        private eGyroState m_gyroState = eGyroState.Rotating;

        private float m_fallingAngle = 0.0f;
        private float m_rotatingAngle = 0.0f;
        private float m_slowingAngle = 0.0f;
        private float m_stopingAngle = 0.0f;

        private float m_rotatingSpeed = 20.0f;
        private float m_slowingSpeed = 20.0f;
        private float m_stopingSpeed = 10.0f;

        private const float m_fallingFactor = 0.05f;
        private const float m_slowingSlowFactor = 0.05f;
        private const float m_stopingSlowFactor = 0.005f;
        private const float m_rotatingSlowFactor = 0.005f;

        private float m_xAxisFellAngle;
        private float m_yAxisFellAngle;

        private const float m_startStopingSpeed = 1.5f;
        private float m_startedStopingAngle;

        private const float m_swingingAmplitudeDecrease = 5.0f;
        private float m_swingingAmplitude = 30.0f;
        private float m_swingingDirection = 1.0f;

        private int Width { get; set; }
        private int Height { get; set; }

        // Width - x axis, Height - y axis, Depth - z axis
        public float CubeHeight { get; set; }
        public float CubeWidth { get; set; }
        public float CubeDepth { get; set; }
		public float CylinderRadius { get; set; }
		public float CylinderHeight { get; set; }
        public float PyramidHeight { get; set; }
        public float PyramidWidth { get; set; }
        public float PyramidDepth { get; set; }

        private uint[] m_textureList;

        public gyroOGL(Control control)
        {
            m_control = control;
            Width = m_control.Width;
            Height = m_control.Height;
            InitializeGL();

            CubeHeight = 1.0f;
            CubeWidth = 1.0f;
            CubeDepth = 1.0f;
			CylinderRadius = 0.1f;
			CylinderHeight = 0.7f;
            PyramidHeight = 0.7f;
            PyramidWidth = 1.0f;
            PyramidDepth = 1.0f;
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
            // drawing the axes and the cube from the current position
            drawAxes();
            drawGyroCube();

            GL.glColor3f(0.905f, 0.694f, 0.148f);
            GL.glRotatef(180.0f, 1.0f, 0.0f, 1.0f); // rotate the position to draw the pyramid at vertical inversion
            drawGyroPyramid();
            GL.glRotatef(180.0f, -1.0f, 0.0f, -1.0f); // return to previous position

            // translate the position to draw the cylinder on the top of the cube
            float xCylinderOrigin = CubeWidth / 2;
            float yCylinderOrigin = CubeHeight;
            float zCylinderOrigin = CubeDepth / 2;
			
			GL.glTranslatef(xCylinderOrigin, yCylinderOrigin, zCylinderOrigin);
			drawGyroCylinder();
			GL.glTranslatef(-xCylinderOrigin, -yCylinderOrigin, -zCylinderOrigin); // return to previous position
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

        private void drawGyroCylinder()
        {
			int cylinderSegements = 16;

			// draw the cylinder
			GLUquadric GluQuadric;
			GluQuadric = GLU.gluNewQuadric();
											 
			GL.glRotatef(90, -1, 0, 0); // the cylinder height is relative to z axis
			GLU.gluCylinder(GluQuadric, CylinderRadius, CylinderRadius, CylinderHeight, cylinderSegements, cylinderSegements);
			GL.glRotatef(90, 1, 0, 0);

			GLU.gluDeleteQuadric(GluQuadric);

			// draw cylinder cover
			GL.glBegin(GL.GL_POLYGON);
			
			for (int i = 0; i < cylinderSegements; i++)
			{
				float theta = 2.0f * 3.1415926f * i / cylinderSegements; //get the current angle

				float x = CylinderRadius * (float)Math.Cos(theta); //calculate the x component
				float y = CylinderRadius * (float)Math.Sin(theta); //calculate the y component

				GL.glVertex3f(x, CylinderHeight, y);
			}
			
			GL.glEnd();
		}

        private void drawGyroCube()
        {
            GL.glColor3f(1.0f, 1.0f, 1.0f);
            GL.glEnable(GL.GL_TEXTURE_2D);
            //GL.glDisable(GL.GL_LIGHTING);

            drawGenericCube(CubeWidth, CubeHeight, CubeDepth);

            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        private void drawGyroPyramid()
        {
            drawSquareSurface(PyramidWidth, 0.0f, PyramidDepth, eAxis.Y);

            GL.glBegin(GL.GL_TRIANGLES);

            // first x axis
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight, PyramidDepth / 2);
            GL.glVertex3f(0.0f, 0.0f, PyramidDepth);

            // second x axis
            GL.glVertex3f(PyramidWidth, 0.0f, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight, PyramidDepth / 2);
            GL.glVertex3f(PyramidWidth, 0.0f, PyramidDepth);

            // first Z axis
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight, PyramidDepth / 2);
            GL.glVertex3f(PyramidWidth, 0.0f, 0.0f);

            // second Z axis
            GL.glVertex3f(0.0f, 0.0f, PyramidDepth);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight, PyramidDepth / 2);
            GL.glVertex3f(PyramidWidth, 0.0f, PyramidDepth);

            GL.glEnd();
        }

        private void drawGenericCube(float width, float height, float depth)
        {
            // if there is no texture enabled, the glBindTexture do nothing
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[2]);
            drawSquareSurface(0, height, depth, eAxis.X);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[0]);
            drawSquareSurface(width, height, depth, eAxis.X);

            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[3]);
            drawSquareSurface(width, height, 0, eAxis.Z);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[1]);
            drawSquareSurface(width, height, depth, eAxis.Z);

            drawSquareSurface(width, 0, depth, eAxis.Y);
            drawSquareSurface(width, height, depth, eAxis.Y);
        }

        private void drawSquareSurface(float width, float height, float depth, eAxis axis)
        {
            GL.glBegin(GL.GL_QUADS);

            // if there is no texture bind, the glTexCoord2f do nothing
            switch (axis)
            {
                case eAxis.X:
                    GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(width, 0, 0);
                    GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(width, 0, depth);
                    GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(width, height, depth);
                    GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(width, height, 0);
                    break;
                case eAxis.Y:
                    GL.glVertex3f(0, height, 0);
                    GL.glVertex3f(0, height, depth);
                    GL.glVertex3f(width, height, depth);
                    GL.glVertex3f(width, height, 0);
                    break;
                case eAxis.Z:
                    GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(0, 0, depth);
                    GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(0, height, depth);
                    GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(width, height, depth);
                    GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(width, 0, depth);
                    break;
            }

            GL.glEnd();
        }

        public void Draw()
        {
			/*float[] pos = new float[4];
			pos[0] = 10; pos[1] = 10; pos[2] = 10; pos[3] = 1;
			GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);
			GL.glDisable(GL.GL_LIGHTING);*/

			if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();

            GL.glTranslatef(0.0f, 0.0f, -3.0f); // Translate 3 Units Into The Screen

            switch (m_gyroState)
            {
                case eGyroState.Rotating:
                    gyroRotatingTransform();
                    break;
                case eGyroState.Slowing:
                    gyroSlowingTransform();
                    break;
                case eGyroState.Stoping:
                    gyroStopingTransform();
                    break;
            }

			// draw the gyro
			/*GL.glEnable(GL.GL_LIGHTING);
			GL.glEnable(GL.GL_LIGHT0);
			GL.glEnable(GL.GL_COLOR_MATERIAL);*/
			DrawAll();

            GL.glFlush();

            WGL.wglSwapBuffers(m_uint_DC);
        }

        private void gyroRotatingTransform()
        {
            // make the gyro turn around itself
            m_rotatingAngle += m_rotatingSpeed;
            GL.glTranslatef(CubeWidth / 2.0f, 0.0f, CubeDepth / 2.0f);
            GL.glRotatef(m_rotatingAngle, 0.0f, -1.0f, 0.0f);
            GL.glTranslatef(-CubeWidth / 2.0f, 0.0f, -CubeDepth / 2.0f);

            // the gyro loose power
            GL.glRotatef(m_fallingAngle, 1.0f, 0.0f, 0.0f);
            m_rotatingSpeed -= m_rotatingSlowFactor;
            m_fallingAngle += m_fallingFactor;

            if (m_fallingAngle >= 45)
            {
                m_yAxisFellAngle = m_rotatingAngle % 360.0f;
                m_xAxisFellAngle = m_fallingAngle % 360.0f;
                //GyroFellEvent.Invoke(); // this event handled by GyroForm to stop drawRotating, and start drawSwinging
                m_gyroState = eGyroState.Slowing;
            }
        }

        private void gyroSlowingTransform()
        {
            // calc slowing angle
            m_slowingAngle += m_slowingSpeed;

            // set the gyro last position with swinging angle
            GL.glTranslatef(CubeWidth / 2.0f, 0.0f, CubeDepth / 2.0f);
            GL.glRotatef(m_yAxisFellAngle + m_slowingAngle, 0.0f, -1.0f, 0.0f);
            GL.glTranslatef(-CubeWidth / 2.0f, 0.0f, -CubeDepth / 2.0f);
            GL.glRotatef(m_xAxisFellAngle, 1.0f, 0.0f, 0.0f);

            m_slowingSpeed -= m_slowingSlowFactor; // slow down is linear

            if (m_slowingSpeed < m_startStopingSpeed)
            {
                m_startedStopingAngle = m_slowingAngle;
                m_stopingAngle = m_slowingAngle;
                m_stopingSpeed = m_slowingSpeed;
                m_gyroState = eGyroState.Stoping;
            }
        }

        private void gyroStopingTransform()
        {
            // calc slowing angle
            m_stopingAngle += m_stopingSpeed * m_swingingDirection;

            // set the gyro last position with swinging angle
            GL.glTranslatef(CubeWidth / 2.0f, 0.0f, CubeDepth / 2.0f);
            GL.glRotatef(m_yAxisFellAngle + m_stopingAngle, 0.0f, -1.0f, 0.0f);
            GL.glTranslatef(-CubeWidth / 2.0f, 0.0f, -CubeDepth / 2.0f);
            GL.glRotatef(m_xAxisFellAngle, 1.0f, 0.0f, 0.0f);

            m_stopingSpeed -= m_stopingSlowFactor;

            float swingedAngle = (m_stopingAngle - m_startedStopingAngle);

            if (swingedAngle * m_swingingDirection > m_swingingAmplitude)
            {
                m_swingingDirection *= -1; // swing to the other side
                m_swingingAmplitude -= m_swingingAmplitudeDecrease; // swinging amlitude is decrease every swinging direction change
            }

            if (m_stopingSpeed <= 0)
            {
                m_stopingSpeed = 0; // continue drawing, but the slowing angle will not changed
            }
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

            InitTexture();
        }

        void InitTexture()
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            m_textureList = new uint[4];      // storage for texture
            string[] bmpFilesPath = new string[m_textureList.Length];

            bmpFilesPath[0] = "../../Resources/noon.bmp";
            bmpFilesPath[1] = "../../Resources/gimel.bmp";
            bmpFilesPath[2] = "../../Resources/hei.bmp";
            bmpFilesPath[3] = "../../Resources/pei.bmp";

            GL.glGenTextures(m_textureList.Length, m_textureList);
            for (int i = 0; i < m_textureList.Length; i++)
            {
                Bitmap image = new Bitmap(bmpFilesPath[i]);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards

                if (i == 0 || i == 3) // instead of drawing the texture in different way in drawSquareSurface, we flip here the texture
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                BitmapData bitmapData = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[i]);
                //  VN-in order to use System.Drawing.Imaging.BitmapData Scan0 I've added overloaded version to
                //  OpenGL.cs
                //  [DllImport(GL_DLL, EntryPoint = "glTexImage2D")]
                //  public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                    0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapData.Scan0);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);      // Linear Filtering
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);      // Linear Filtering

                image.UnlockBits(bitmapData);
                image.Dispose();
            }
        }
    }
}

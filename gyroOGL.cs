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

        private Control m_control;

        private float m_angle = 0.0f;
        private float m_rotatingSpeed = 20.0f;
        private float m_fallingAngle = 1.0f;
        private float m_fallingFactor = 0.05f;
        private int Width { get; set; }
        private int Height { get; set; }

        // Width - x axis, Height - y axis, Depth - z axis
        public float CubeHeight { get; set; }
        public float CubeWidth { get; set; }
        public float CubeDepth { get; set; }
        public float PrismHeight { get; set; }
        
        public float PrismWidth { get; set; }
        public float PrismDepth { get; set; }
        public float PyramidHeight { get; set; }
        public float PyramidWidth { get; set; }
        public float PyramidDepth { get; set; }

        private uint[] m_textureList;

        public event Action gyroFellEvent;

        public gyroOGL(Control control)
        {
            m_control = control;
            Width = m_control.Width;
            Height = m_control.Height;
            InitializeGL();

            CubeHeight = 1.0f;
            CubeWidth = 1.0f;
            CubeDepth = 1.0f;
            PrismHeight = 0.2f;
            PrismWidth = 0.2f;
            PrismDepth = 0.7f;
            PyramidHeight = 1.0f;
            PyramidWidth = 1.0f;
            PyramidDepth = 0.7f;
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
            GL.glRotatef(180.0f, 1.0f, 1.0f, 0.0f); // rotate the position to draw the pyramid at vertical inversion
            drawGyroPyramid();
            GL.glRotatef(180.0f, 1.0f, 1.0f, 0.0f); // return to previous position

            // translate the position to draw the prism on the top of the cube
            float xPrismOrigin = (CubeWidth / 2 - PrismWidth / 2);
            float yPrismOrigin = (CubeHeight / 2 - PrismHeight / 2);
            float zPrismOrigin = CubeDepth;

            GL.glTranslatef(xPrismOrigin, yPrismOrigin, zPrismOrigin);
            drawGyroPrism();
            GL.glTranslatef(-xPrismOrigin, -yPrismOrigin, -zPrismOrigin); // return to previous position
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
            drawGenericCube(PrismWidth, PrismHeight, PrismDepth);
        }

        private void drawGyroCube()
        {
            GL.glColor3f(1.0f, 1.0f, 1.0f);
            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glDisable(GL.GL_LIGHTING);

            drawGenericCube(CubeHeight, CubeWidth, CubeDepth);

            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        private void drawGyroPyramid()
        {
            drawSquareSurface(PyramidWidth, PyramidHeight, 0.0f, eAxis.Z);

            GL.glBegin(GL.GL_TRIANGLES);

            // first eAxis.x
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight / 2, PyramidDepth);
            GL.glVertex3f(0.0f, PyramidHeight, 0.0f);

            // second eAxis.x
            GL.glVertex3f(PyramidWidth, 0.0f, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight / 2, PyramidDepth);
            GL.glVertex3f(PyramidWidth, PyramidHeight, 0.0f);

            // first eAxis.y
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight / 2, PyramidDepth);
            GL.glVertex3f(PyramidWidth, 0.0f, 0.0f);

            // second eAxis.y
            GL.glVertex3f(0.0f, PyramidHeight, 0.0f);
            GL.glVertex3f(PyramidWidth / 2, PyramidHeight / 2, PyramidDepth);
            GL.glVertex3f(PyramidWidth, PyramidHeight, 0.0f);

            GL.glEnd();
        }

        private void drawGenericCube(float width, float height, float depth)
        {
            // if there is no texture enabled, the glBindTexture do nothing
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[0]);
            drawSquareSurface(0, height, depth, eAxis.X);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[2]);
            drawSquareSurface(width, height, depth, eAxis.X);

            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[3]);
            drawSquareSurface(width, 0, depth, eAxis.Y);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureList[1]);
            drawSquareSurface(width, height, depth, eAxis.Y);

            drawSquareSurface(width, height, 0, eAxis.Z);
            drawSquareSurface(width, height, depth, eAxis.Z);
        }

        private void drawSquareSurface(float width, float height, float depth, eAxis axis)
        {
            GL.glBegin(GL.GL_QUADS);

            // if there is no texture bind, the glTexCoord2f do nothing
            switch (axis)
            {
                case eAxis.X:
                    GL.glTexCoord2f(0.0f, 0.0f);
                    GL.glVertex3f(width, 0, 0);
                    GL.glTexCoord2f(0.0f, 1.0f);
                    GL.glVertex3f(width, 0, depth);
                    GL.glTexCoord2f(1.0f, 1.0f);
                    GL.glVertex3f(width, height, depth);
                    GL.glTexCoord2f(1.0f, 0.0f);
                    GL.glVertex3f(width, height, 0);
                    break;
                case eAxis.Y:
                    GL.glTexCoord2f(0.0f, 0.0f);
                    GL.glVertex3f(0, height, 0);
                    GL.glTexCoord2f(0.0f, 1.0f);
                    GL.glVertex3f(0, height, depth);
                    GL.glTexCoord2f(1.0f, 1.0f);
                    GL.glVertex3f(width, height, depth);
                    GL.glTexCoord2f(1.0f, 0.0f);
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

        public void DrawRotating()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();

            GL.glTranslatef(0.0f, 0.0f, -2.0f); // Translate 6 Units Into The Screen
            GL.glRotatef(270.0f, 1.0f, 0.0f, 0.0f); // make Z axis to be up

            // make the gyro turn around itself
            m_angle += m_rotatingSpeed;
            GL.glTranslatef(CubeWidth / 2.0f, CubeWidth / 2.0f, 0.0f);
            GL.glRotatef(m_angle, 0.0f, 0.0f, 1.0f);
            GL.glTranslatef(-CubeWidth / 2.0f, -CubeWidth / 2.0f, 0.0f);

            // the gyro loose power
            GL.glRotatef(m_fallingAngle, 0.0f, 1.0f, 0.0f);
            m_angle -= m_fallingFactor * m_rotatingSpeed;
            m_fallingAngle += m_fallingFactor;

            if (m_fallingAngle >= 45)
            {
                //m_angle %= 360.0f;
                m_fellAngle = m_angle % 360.0f;
                gyroFellEvent.Invoke(); // this event handled by GyroForm to stop drawRotating, and start drawSwinging
            }

            // draw the gyro
            DrawAll();

            GL.glFlush();

            WGL.wglSwapBuffers(m_uint_DC);
        }

        private float m_swingingDirection = 1.0f;
        private float m_swingingSpeed = 10.0f;
        private float m_fellAngle;
        private float m_fellAmplitude = 0.2f;
        private float m_xRotate = 0.0f;

        public void DrawSwinging()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();

            GL.glTranslatef(0.0f, 0.0f, -2.0f); // Translate 6 Units Into The Screen
            GL.glRotatef(270.0f, 1.0f, 0.0f, 0.0f); // make Z axis to be up


            m_fellAngle += m_swingingSpeed;
            m_swingingSpeed -= 0.01f;
            
            /*if (m_fellAngle + m_swingingSpeed > m_fellAngle + m_fellAmplitude)
            {
                m_swingingDirection *= -1;
            }
            else if (m_fellAngle < m_angle - m_fellAmplitude)
            {
                m_swingingSpeed += 0.1f;
            }*/

            // set the gyro last position
            GL.glTranslatef(CubeWidth / 2.0f, CubeWidth / 2.0f, 0.0f);
            GL.glRotatef(m_fellAngle + m_swingingSpeed, 0.0f, 0.0f, 1.0f);
            GL.glTranslatef(-CubeWidth / 2.0f, -CubeWidth / 2.0f, 0.0f);

            //m_fallingAngle += 0.1f;
            GL.glRotatef(m_fallingAngle, 0.0f, 1.0f, 0.0f);


            if (m_fellAngle % 90.0f < 1 && m_fellAngle % 90.0f > -1)
            {
                m_xRotate += 10.0f;
            }

            GL.glRotatef(m_xRotate, 1.0f, 1.0f, 0.0f);

            //m_angle += 5f;

            //m_swingingAngle += 0.1f;
            //m_swingingDirection *= -1.0f;

            //GL.glRotatef(m_swingingAngle, 0.0f, m_swingingDirection, 0.0f);


            if (m_swingingSpeed > 0)
            {
            // draw the gyro
            DrawAll();
            }


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
            DrawRotating();
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

                if (i < 2)
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

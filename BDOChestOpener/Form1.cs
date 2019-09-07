using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BDOChestOpener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            debugScreenshots();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            activateWindow();
            //SendKeys.Send(" ");
            boundedScreenshot(820, 150, 100, 350, "___CLICKED___.jpg");
            debugScreenshots();
        }

        private void boundedScreenshot(int x, int y, int width, int height, string fileName)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            bmp.Save(fileName, ImageFormat.Jpeg);
        }

        private void debugScreenshots()
        {
            string fileName;
            int seconds = 10;
            int timesPerSecond = 10;
            int sleepTimer = 1000 / timesPerSecond;
            for (int i = 0; i < (seconds * timesPerSecond); i++)
            {
                fileName = "wheel" + i + ".jpg";
                boundedScreenshot(820, 150, 100, 350, fileName);
                Thread.Sleep(sleepTimer);
            }
            System.Media.SystemSounds.Beep.Play();
        }

        private void activateWindow()
        {
            Process process = Process.Start(@"C:\Users\phuzE\Dropbox\Programming\BDOChestOpener\BDOChestOpener\bin\Debug\ActivateSpace.exe");
            /*int id = process.Id;
            Process tempProc = Process.GetProcessById(id);
            tempProc.WaitForExit();*/
            System.Media.SystemSounds.Beep.Play();
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }

        }


        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
    }
}

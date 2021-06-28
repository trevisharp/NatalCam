using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

Application.SetHighDpiMode(HighDpiMode.SystemAware);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);

var form = new Form();
var capture = new VideoCapture(0);
var pb = new PictureBox();
var timer = new Timer();
Graphics g = null;
Bitmap bmp = null;
Mat mat = null;
Mat bg = null;

form.FormBorderStyle = FormBorderStyle.None;
form.WindowState = FormWindowState.Maximized;
form.KeyPreview = true;
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);
timer.Interval = 25;

form.KeyDown += (sender, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
    else if (e.KeyCode == Keys.S)
    {
        capture.Read(bg);
    }
};

timer.Tick += delegate
{
    capture.Read(mat);
    g.DrawImage(mat.ToBitmap(), new Rectangle(0, 0, pb.Width, pb.Height));
    pb.Refresh();
};

form.Load += delegate
{
    bmp = new Bitmap(pb.Width, pb.Height);
    mat = new Mat(new int[] { 
        capture.FrameWidth, capture.FrameHeight
    }, MatType.CV_8U);
    bg = new Mat(new int[] { 
        capture.FrameWidth, capture.FrameHeight
    }, MatType.CV_8U);
    g = Graphics.FromImage(bmp);
    pb.Image = bmp;
    timer.Start();
};

Application.Run(form);
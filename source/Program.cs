using System;
using System.Drawing;
using System.Windows.Forms;
using cs_Fifteen.Classes;
using cs_Fifteen.Forms;

namespace cs_Fifteen
{
    public delegate void DrawImageDelegate(Bitmap pic);
    public delegate void DrawBitmapCallback(Bitmap image);
    public delegate void GetCellsPositionDelegate();
    public delegate MovingDirection GetGestureDelegate(); 

    public enum MovingDirection {Left = 1, Right, Up, Down, Undefined};

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            MainForm mainFOrm = new MainForm();

             = newBlass(mainFOrm)
            Application.Run(mainFOrm);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using cs_Fifteen.Classes;

namespace cs_Fifteen.Forms
{
    public partial class MainForm : Form
    {

        private BaseClass _baseClass;

        public MainForm()
        {
            InitializeComponent();
        }        


        private void FrmMail_Load(object sender, EventArgs e)
        {
            _baseClass = new BaseClass(imgCells);
            _baseClass.DrawBitmap += DrawBitmap;
            _baseClass.Init();
            _baseClass.NewGame();
            _baseClass.StartGame();
        }

        private void ItmNewGame_Click(object sender, EventArgs e)
        {
            _baseClass.StopGame();
            _baseClass.NewGame();
            _baseClass.StartGame();
        }

        private void itmAbout_Click(object sender, EventArgs e)
        {
            //_baseClass.StopGame();
            About about = new About();
            about.ShowDialog();
            //_baseClass.StartGame();
        }

        public void DrawBitmap(Bitmap image)
        {

            if (pbxBoard.InvokeRequired)
            {
                DrawBitmapCallback d = DrawBitmap;
                Invoke(d, new object[] {image});
            }
            else
            {
                lock (CellsStorage.LockObject)
                {
                    pbxBoard.Image = image;
                    pbxBoard.Refresh();
                }
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _baseClass.CloseGame();
            _baseClass.DrawBitmap = null;
            Application.DoEvents(); // Do not delete this; need to prevent Invoke usage when main form already disposed
        }

        private void itmShuffleCells_Click(object sender, EventArgs e)
        {
            _baseClass.ShuffleCells();
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MovingDirection direction = MovingDirection.Undefined;

            switch (e.KeyCode)
            {
                case Keys.Down:
                    direction = MovingDirection.Down;
                    break;

                case Keys.Right:
                    direction = MovingDirection.Right;
                    break;

                case Keys.Up:
                    direction = MovingDirection.Up;
                    break;

                case Keys.Left:
                    direction = MovingDirection.Left;
                    break;                                    
            }

            if (direction != MovingDirection.Undefined)
            {
                if (_baseClass.MoveCell(direction))
                {
                    MessageBox.Show("You solved the game!");                    
                }
            }
        }
    }
    
}

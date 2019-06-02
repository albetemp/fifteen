using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace cs_Fifteen.Classes
{
    class BaseClass
    {

        private readonly ImageList _imgCells;
        private readonly CellsMover _cellsMover;
        private DrawImageDelegate _drawBitmap;
        private CameraManager _cameraManager;
        private Thread _t;

        public DrawImageDelegate DrawBitmap
        {
            get { return _drawBitmap; }
            set
            {
                _drawBitmap = value;
                _cellsMover.DrawBitmap = value;
            }
        }

        public void UpdateGesture(MovingDirection direction)
        {
            _cellsMover.MoveCell(direction);
        }

        public void CloseGame()
        {
            StopGame();
            _t.Abort();
        }

        public void StopGame()
        {
            _cellsMover.Enabled = false;
        }

        public void StartGame()
        {
            _cellsMover.Enabled = true;
        }


        public BaseClass(ImageList imgCells)
        {
            _imgCells = imgCells;
            _cellsMover = new CellsMover();
            _cameraManager = new CameraManager();
        }

        public void Init()
        {
            _cellsMover.DrawBitmap = _drawBitmap;
            _cameraManager.Init(this);
            _t = new Thread(_cameraManager.LoopFrames);
            _t.Start();
        }

        public void ShuffleCells()
        {
            
            _cellsMover.ShuffleCells();    
        }

        public bool MoveCell(MovingDirection direction)
        {
            return _cellsMover.MoveCell(direction);
        }

        public void NewGame()
        {

            _cellsMover.Enabled = false;

            CellsStorage.Cells.Clear();

            List<int> position = new List<int>(16);
            position.Add(0);

            // Create cells; fills images
            // Place cells to the board
            // board 
            // RowColumn

            // 00 01 02 03
            // 10 11 12 13
            // 20 21 22 23
            // 30 31 32 33

            int cellId = 0;

            // Init cells

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    if (cellId < 15)
                    {
                        Cell cell = new Cell(false, _imgCells.Images[cellId], cellId, row, column);
                        CellsStorage.Cells.Add(cell);
                        cellId++;
                    }
                    else
                    {
                        // add 'empty' cell
                        Cell cell = new Cell(true, null, cellId, row, column);
                        CellsStorage.Cells.Add(cell);                        
                    }
                }
            }
        }
    }
}

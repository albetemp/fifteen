using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;

namespace cs_Fifteen.Classes
{
    // Represents cells moving; Recalculates cells position each time when renderer make new frame
    class CellsMover
    {
        private readonly Renderer _renderer;
        private bool _enabled;
        private DrawImageDelegate _drawBitmap;
        private readonly Random _rnd;
        private readonly Timer _vibroCells;
        private bool _vibration;
        
        public DrawImageDelegate DrawBitmap
        {
            get { return _drawBitmap; }
            set
            {
                _drawBitmap = value;
                _renderer.DrawBitmap = value;
                _renderer.GetCellsPosition += _GetCellsPosition;
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                _renderer.Enabled = value;
            }
        }

        public CellsMover()
        {
            _renderer = new Renderer();
            _rnd = new Random();
            _vibroCells = new Timer(3000);
            _vibroCells.Elapsed += OnTimedEvent;
            _vibroCells.Enabled = false;
            _vibration = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            _vibration = true;
            _vibroCells.Enabled = false;

        }

        public void ShuffleCells()
        {

            _renderer.Enabled = false;

            Random rnd = new Random();

            // temporary collection with ID's
            Collection<int> idCollect = new Collection<int>();

            for (int loop = 0; loop < 16; loop++)
            {
                idCollect.Add(loop);
            }

            // now shuffle cells

            for (int loop = 0; loop < 16; loop++)
            {                
                int cellId = Convert.ToInt32(rnd.Next(idCollect.Count));
                MoveCellToRowColumn(loop, Convert.ToInt32(idCollect[cellId] / 4), idCollect[cellId] - Convert.ToInt32(idCollect[cellId] / 4) * 4);
                idCollect.RemoveAt(cellId);
            }

            _renderer.Enabled = true;

        }

        // Place cell directly to teh selected place
        // There are no other checks; Used for the shuffling cells only

        private void MoveCellToRowColumn(int cellId, int newRow, int newColumn)
        {
            Cell cell = CellsStorage.Cells[cellId];

            cell.Row = newRow;
            cell.Column = newColumn;

        }

        private void _GetCellsPosition()
        {
            foreach (Cell loopCells in CellsStorage.Cells)
            {

                loopCells.X = loopCells.Column * 94 + 3;
                loopCells.Y = loopCells.Row*94 + 3;

                if (_vibration)
                {
                    loopCells.X = loopCells.X + 2 - _rnd.Next(4);
                    loopCells.Y = loopCells.Y + 2 - _rnd.Next(4);
                }
            }            
        }

        public bool MoveCell(MovingDirection direction)
        {

            _renderer.Enabled = false;

            _vibration = false;
            _vibroCells.Enabled = false;
            _vibroCells.Enabled = true;

            // bypass prohibited moving
            if (((direction == MovingDirection.Up) && (CellsStorage.EmptyCell.Row == 3)) ||
                ((direction == MovingDirection.Down) && (CellsStorage.EmptyCell.Row == 0)) ||
                ((direction == MovingDirection.Left) && (CellsStorage.EmptyCell.Column == 3)) ||
                ((direction == MovingDirection.Right) && (CellsStorage.EmptyCell.Column == 0)))
            {
                _renderer.Enabled = true;
                return false;
            }

            int emptyCellRow = CellsStorage.EmptyCell.Row;
            int emptyCellColumn = CellsStorage.EmptyCell.Column;

            // I hate copypaste as you but it's 3 days only to make the job ^)

            foreach (Cell cell in CellsStorage.Cells)
            {
                switch (direction)
                {
                    case MovingDirection.Up:
                        if (cell.IsEmptyCell)
                        {
                            cell.Row++;
                        }
                        else
                        {
                            if (((cell.Row - 1) == emptyCellRow) && (cell.Column == emptyCellColumn))
                            {
                                cell.Row--;
                            }
                        }
                        break;
                    case MovingDirection.Down:
                        if (cell.IsEmptyCell)
                        {
                            cell.Row--;
                        }
                        else
                        {
                            if (((cell.Row + 1) == emptyCellRow) && (cell.Column == emptyCellColumn))
                            {
                                cell.Row++;
                            }
                        }
                        break;
                    case MovingDirection.Left:
                        if (cell.IsEmptyCell)
                        {
                            cell.Column++;
                        }
                        else
                        {
                            if (((cell.Column - 1) == emptyCellColumn) && (cell.Row == emptyCellRow))
                            {
                                cell.Column--;
                            }
                        }
                        break;
                    case MovingDirection.Right:
                        if (cell.IsEmptyCell)
                        {
                            cell.Column--;
                        }
                        else
                        {
                            if (((cell.Column + 1) == emptyCellColumn) && (cell.Row == emptyCellRow))
                            {
                                cell.Column++;
                            }
                        }
                        break;
                }
            }

            _renderer.Enabled = true;

            return IsGaveSolved();
        }


        // check if the game was solved
        private bool IsGaveSolved()
        {
            return CellsStorage.Cells.Where(cell => !cell.IsEmptyCell).All(cell => cell.Id == (cell.Column + cell.Row*4));
        }
    }
}

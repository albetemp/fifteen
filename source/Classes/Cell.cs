using System.Drawing;

namespace cs_Fifteen.Classes
{

    class Cell
    {
        public int Id; // from 1 to 16
        public int Column; // from 0 to 3; left to right
        public int Row; // from 0 to 3; up to down
        public Bitmap Pic; // cell picture 
        public int X; // X coordinate on a board
        public int Y; // Y coordinate on a board
        public bool IsEmptyCell; // true for the 'empty' cell (free space)

        public Cell(bool emptyCell, Image image, int id, int row, int column)
        {
            Id = id;
            if ((!emptyCell) && (image != null)) 
            {
                Pic = new Bitmap(image);
            }
            IsEmptyCell = emptyCell;
            X = 0; // Changed by CellsMover
            Y = 0; // Changed by CellsMover
            Column = column; // Changed by CellsMover
            Row = row; // Changed by CellsMover
        }
         
    }
}

using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace cs_Fifteen.Classes
{
    static class CellsStorage
    {

        private static int _emptyCellId; // empty cell collection's Id

        public static ObservableCollection <Cell> Cells;
        public static object LockObject;

        public static Cell EmptyCell
        {
            get { return Cells[_emptyCellId]; }
        }

        static CellsStorage()
        {
            Cells = new ObservableCollection<Cell>();
            Cells.CollectionChanged += CellsOnCollectionChanged; // have to find empty cell and update _emptyCellId
            LockObject = new object(); // Just do not want to add new static class to handle it. BTW, LockObject does not ned now actually.
        }

        private static void CellsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (Cell cell in Cells)
            {
                if (cell.IsEmptyCell)
                {
                    _emptyCellId = Cells.IndexOf(cell);
                }
            }            
        }
    }
}

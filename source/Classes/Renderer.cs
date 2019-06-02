using System.Drawing;
using System.Globalization;
using System.Timers;
using Timer = System.Timers.Timer;

// Renders board image into bitmap, then draw it on the form (pbxBoard)

namespace cs_Fifteen.Classes
{
    class Renderer
    {

        private readonly Timer _invalidateGui;
        private bool _enabled;

        private int _fps; // Not a real fps; represents counter
        private readonly Font _fpsFont; // font for the _fps
        private readonly SolidBrush _fpsBrush; // brush for the _fps
        private readonly Bitmap _backgroundImage;
        private readonly Bitmap _finalImage;

        public GetCellsPositionDelegate GetCellsPosition;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                _invalidateGui.Enabled = value;
            }
        }

        /// <summary>
        /// Delegate to Draw bitmap for the main form
        /// </summary>
        public DrawImageDelegate DrawBitmap;

        public Renderer ()
        {
            _invalidateGui = new Timer(50);
            _invalidateGui.Enabled = false;
            _invalidateGui.Elapsed += OnTimedEvent;
            _fps = 0;
            _fpsFont = new Font("Arial", 16);
            _fpsBrush = new SolidBrush(Color.Black);   
            _backgroundImage = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("cs_Fifteen.Resources.background.jpg"));
            _finalImage = new Bitmap(379, 379);
  
        }

        public void ShowBitmap(Bitmap image)
        {
            if (DrawBitmap != null)
            {
                DrawBitmap(image);
            }
        }

        /// <summary>
        /// Render the final Bitmap with all of the cells
        /// </summary>
        /// <returns></returns>

        private Bitmap _RenderScene()
        {

            _invalidateGui.Enabled = false;

            if (_enabled)
            {

                lock (CellsStorage.LockObject)
                {
                    Graphics g = Graphics.FromImage(_finalImage);

                    // Does not need to clear buffer; Redrawing with background every time

                    g.DrawImage(_backgroundImage, 0, 0, 379, 379);

                    if (GetCellsPosition != null)
                    {
                        // CellsMover calculates current position for the each cells           
                        GetCellsPosition();

                        foreach (Cell loopCell in CellsStorage.Cells)
                        {
                            if (!loopCell.IsEmptyCell)
                            {
                                g.DrawImage(loopCell.Pic, loopCell.X, loopCell.Y);
                            }
                        }
                        g.DrawString(_fps.ToString(CultureInfo.InvariantCulture), _fpsFont, _fpsBrush, 0, 0);
                        _fps++;
                    }
                }
            }
            _invalidateGui.Enabled = true;

            return _finalImage;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (DrawBitmap != null)
            {
                
                DrawBitmap(_RenderScene());
                
            }
        }


    }

}

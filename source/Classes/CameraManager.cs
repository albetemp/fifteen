using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace cs_Fifteen.Classes
{
    class CameraManager
    {

        private MyPipeline _pipeline;
        private BaseClass _sender;
        public MovingDirection GestureDirection;

        public bool Init(object sender)
        {
            _sender = (BaseClass)sender;
            _pipeline = new MyPipeline(this);
            return true;
        }

        public void LoopFrames()
        {
            if (!_pipeline.LoopFrames())
            {
                MessageBox.Show("Failed to initialize or stream data");
            }
            _pipeline.Dispose();            
        }

        public void UpdateGesture(MovingDirection direction)
        {
            _sender.UpdateGesture(direction);
        }

    }

    sealed class MyPipeline : UtilMPipeline
    {
        private int _nframes;
        private bool _deviceLost;
        private readonly CameraManager _sender;
        private MovingDirection _lastDirection;
        private const double TRESHOLD_COEFF = 0.8; // adjust sensitive level (1 is the absolutely sensitive)
        private const int TRESHOLD_NUMBER = 5; // how many times gesture detector should generate detection in the same way
        private int _detectionX; // how many times horizontal move was detected ( > 0 - left to right; < 0 - right to left)
        private int _detectionY; // how many times vertivcal move was detected ( > 0 - down to up; < 0 - up to down)
        private float _cameraDataX;
        private float _cameraDataY;
        private bool _cameraNewDetection; // true if direction was detected and _cameraDataX/Y no more valid
        private readonly Timer _detection;
        private bool _allowDetection;
 

        public MyPipeline(CameraManager sender): base()
        {
            EnableGesture();
            _sender = sender;
            _nframes = 0;
            _deviceLost = false;
            _allowDetection = true;
            _detection = new Timer(500);
            _detection.Enabled = false;
            _detection.Elapsed += OnTimedEvent;
            _NewDetection();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _allowDetection = true;
            _detection.Enabled = false;
        }

        public override void OnGesture(ref PXCMGesture.Gesture data)
        {
             if (data.active)
             {
                            
                 // MessageBox.Show("OnGesture(" + data.label + ")");
             }
        }

        public override bool OnDisconnect()
        {
            if (!_deviceLost) MessageBox.Show("Device disconnected");
            _deviceLost = true;
            return base.OnDisconnect();
        }

        public override void OnReconnect()
        {
            MessageBox.Show("Device reconnected");
            _deviceLost = false;
        }

        public override bool OnNewFrame()
        {
            PXCMGesture gesture = QueryGesture();
            PXCMGesture.GeoNode ndata;
            pxcmStatus sts = gesture.QueryNodeData(0, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, out ndata);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                _lastDirection = _DetectGesture(ndata.positionImage.x, ndata.positionImage.y);
                if (_lastDirection != MovingDirection.Undefined)
                {
                    _sender.UpdateGesture(_lastDirection);
                }

                //Console.WriteLine("node HAND_MIDDLE ({0},{1})", ndata.positionImage.x, ndata.positionImage.y);
            }
                
            return (++_nframes < 50000);
        }

        private void _NewDetection()
        {
            _cameraNewDetection = true;
            _detectionX = 0;
            _detectionY = 0;
            _cameraDataX = 0; // fake number
            _cameraDataY = 0;
            _allowDetection = false;
            _detection.Enabled = true;
        }


        private MovingDirection _DetectGesture(float newX, float newY)
        {
            //_sender.UpdateGesture(_lastDirection);            
            //_lastDirection = MovingDirection.Undefined;

            // Console.WriteLine("x =" + newX.ToString() + " y =" + newY.ToString());

            _lastDirection = MovingDirection.Undefined;

            if (_allowDetection)
            {

                if (_cameraNewDetection)
                {
                    _cameraNewDetection = false;
                    _cameraDataX = newX;
                    _cameraDataY = newY;
                    return MovingDirection.Undefined;
                }

                // Horizontal moving

                if ((newX < _cameraDataX) && ((newX/_cameraDataX) > TRESHOLD_COEFF))
                {
                    // move from left to right
                    _detectionX++;
                    //_detectionY = 0;
                }

                if ((newX > _cameraDataX) && ((_cameraDataX/newX) > TRESHOLD_COEFF))
                {
                    // move from right to left
                    _detectionX--;
                    //_detectionY = 0;
                }

                // Vertical moving

                if ((newY < _cameraDataY) && ((newY/_cameraDataY) > TRESHOLD_COEFF))
                {
                    // move from donw to up
                    _detectionY++;
                    //_detectionX = 0;
                }

                if ((newY > _cameraDataY) && ((_cameraDataY/newY) > TRESHOLD_COEFF))
                {
                    // move from right to left
                    _detectionY--;
                    //_detectionX = 0;
                }

                _cameraDataX = newX;
                _cameraDataY = newY;                

                // So sorry about X if both and X and Y was detected at the same time ^)

                if (_detectionX >= TRESHOLD_NUMBER) _lastDirection = MovingDirection.Right;

                if (-_detectionX >= TRESHOLD_NUMBER) _lastDirection = MovingDirection.Left;

                if (_detectionY >= TRESHOLD_NUMBER) _lastDirection = MovingDirection.Up;

                if (-_detectionY >= TRESHOLD_NUMBER) _lastDirection = MovingDirection.Down;

                if (_lastDirection != MovingDirection.Undefined) _NewDetection();
            }
            return _lastDirection;
        }   
    }



}
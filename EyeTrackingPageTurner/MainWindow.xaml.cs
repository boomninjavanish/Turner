using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tobii.Interaction;
using Tobii.Interaction.Framework;
using WindowsInput;
using WindowsInput.Native;
using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace EyeTrackingPageTurner
{
    /// <summary>
    ///     Turner is an app that allows users to change pages by looking at overlays on the screen.
    /// </summary>
    /// <todo>
    ///     - make overlay height adjustable
    ///     - make overlay Y location adjustable
    ///     - allow user to change dwell time
    ///     - enable/disable eye cursor
    ///     - minimize to tray option
    /// </todo>
    /// 
    
    public partial class MainWindow : Window
    {

        // start the Tobii data stream host
        private readonly Host host = new Host();

        // amount of time in milliseconds for fixation to cause keypress
        private int fixationKeyPressInterval = 250; // TODO: make adjustable in interface

        // variables that store which key to press
        /* public Key rightKey = Key.Right;
        public Key leftKey = Key.Left;
        */
        private VirtualKeyCode rightKey = VirtualKeyCode.RIGHT;
        private VirtualKeyCode leftKey = VirtualKeyCode.LEFT;

        // variables that specifiy the location for fixations to cause page turns
        private double rightFixationBarrier;
        private double leftFixationBarrier;

        // vars for overlay
        private GraphicsWindow window;
        private SolidBrush red;
        private int overlayWidthLeft = 30; // width in pixels of both overlays
        private int overlayWidthRight = 30;
        private int overlayAlpha = 64; // amount of transparency to apply (0-255)

        // grab the current screen resolution (in 1/96 scaled for current dpi)
        private readonly int _screenWidth = (int) System.Windows.SystemParameters.PrimaryScreenWidth;
        private readonly int _screenHeight = (int) System.Windows.SystemParameters.PrimaryScreenHeight;

        // vars for storing the resolution calculated with scaling factor
        private int actualScreenWidth;
        private int actualScreenHeight;

        // vars for binding to interface
        public class FixationSpots 
        {
            private double leftWidthValue;
            private double rightWidthValue;
            public double LeftWidth
            {
                get
                {
                    return leftWidthValue;
                }
                set
                {
                    leftWidthValue = value;
                }
            }

            public double RightWidth
            {
                get
                {
                    return rightWidthValue;
                }

                set
                {
                    rightWidthValue = value;
                }
            }

            
        }
        


        public MainWindow()
        {
            InitializeComponent();

            // get the screen resoultion without scaling
            CalculateActualScreenResolution();

            // setup sliders with values; assign those values to the barriers
            sliderLeftWidth.DataContext = new FixationSpots() { LeftWidth = 5 };
            sliderRightWidth.DataContext = new FixationSpots() { RightWidth = 5 };
            leftFixationBarrier = PercentToPixels(sliderLeftWidth.Value, actualScreenWidth);
            rightFixationBarrier = actualScreenWidth - PercentToPixels(sliderRightWidth.Value, actualScreenWidth);

            
            CreateOverlays();

            // setup a timespan to track the interval in time in which a fixation is held
            TimeSpan fixationKeyPressTime = new TimeSpan(0,0,0,0,fixationKeyPressInterval);

            // init Tobii host data stream
            var fixationDataStream = host.Streams.CreateFixationDataStream();

            // track the beginning of each fixation
            var fixationBeginTime = 0d;
            TimeSpan totalFixationTime = new TimeSpan();

            // track wether page has been turned to prevent multiple page turns
            bool pageHasBeenTurned = false;

            // setup an virtual keyboard using the InputSimulator NuGet pakage
            InputSimulator virtualKeyboard = new InputSimulator();

            fixationDataStream.Next += (o, fixation) =>
            {
                // on next event, the fixation data comes as FixationData objects wrapped in a StreamData<T> object
                var fixationDataPointX = fixation.Data.X;

                // print the current fixation data to the status bar
                this.Dispatcher.Invoke(() =>
                {
                    textBoxStatus.Text = "Gaze point X: [ " + fixationDataPointX + " ]";
                });
                
                switch (fixation.Data.EventType)
                {
                    case FixationDataEventType.Begin:
                        // start a timer and track total fixation time
                        fixationBeginTime = fixation.Data.Timestamp;

                        break;

                    case FixationDataEventType.Data:
                        // calculate total fixation time
                        totalFixationTime = TimeSpan.FromMilliseconds(fixation.Data.Timestamp - fixationBeginTime);
                        UpdateTimeTextBox(totalFixationTime.ToString());

                        // if timer var is greater than fixationKeyPressTime then perform keypress
                        if (pageHasBeenTurned == false & (totalFixationTime > fixationKeyPressTime))
                        {
                            // if fixation is on the right, press right page turn button
                            if (fixationDataPointX > rightFixationBarrier)
                            {
                                //SendKeyPress(rightKey);
                                virtualKeyboard.Keyboard.KeyPress(rightKey);

                                // update the textbox
                                UpdateKeyPressTextBox("Right");
                            }
                            
                            // if fixation is on the left, press left page turn button
                            if (fixationDataPointX < leftFixationBarrier)
                            {
                               // SendKeyPress(leftKey);
                                virtualKeyboard.Keyboard.KeyPress(leftKey);
                                // update the textbox
                                UpdateKeyPressTextBox("Left");
                            }

                            pageHasBeenTurned = true;

                        }
                        break;

                    case FixationDataEventType.End:
                        // reset the timer and page turn flag and wait for next fixation
                        totalFixationTime = TimeSpan.Zero;
                        pageHasBeenTurned = false;

                        break;

                    default:

                        throw new InvalidOperationException("Unknown fixation event type, which doesn't have explicit handling.");
                }
            };
        }

        private int PercentToPixels(double percent, double totalPixels)
        {
            // calculate a width or height in pixels based on percentage of screen
            int pixels;
            pixels = (int)((percent * totalPixels) / 100);
            return pixels;
        }

        private void CalculateActualScreenResolution()
        {
            // calculate the actual screen width and height
            double scaleFactor = GetDisplayScaleFactor();
            actualScreenWidth = (int)(_screenWidth * scaleFactor);
            actualScreenHeight = (int)(_screenHeight * scaleFactor);
        }

        private double GetDisplayScaleFactor()
        {
            // returns the dpi scaling factor; used to calculate the actual pixels on the screen
            _ = new DpiScale();

            DpiScale dpiScale = VisualTreeHelper.GetDpi(this);

            double scaleFactor = dpiScale.PixelsPerInchX / 96;

            return scaleFactor;
        }

        // send a key press
        public void SendKeyPress(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    };

                    InputManager.Current.ProcessInput(e);

                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent

                }
            }
        }

        // update the keypress status textbox
        public void UpdateKeyPressTextBox(string text)
        {
            this.Dispatcher.Invoke(() =>
            {
                textBoxKeyPressStatus.Text = text;
            });
        }

        // update the time status textbox
        public void UpdateTimeTextBox(string text)
        {
            this.Dispatcher.Invoke(() =>
            {
                textBoxTimeStatus.Text = text;
            });
        }

        private void CreateOverlays()
        {
            var graphics = new Graphics
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = true,
                WindowHandle = IntPtr.Zero
            };

            window = new GraphicsWindow(graphics)
            {
                IsTopmost = true,
                IsVisible = true,
                FPS = 60,
                X = 0,
                Y = 0,
                Width = actualScreenWidth,
                Height = actualScreenHeight
            };

            window.SetupGraphics += Window_SetupGraphics;
            window.DestroyGraphics += Window_DestroyGraphics;
            window.DrawGraphics += Window_DrawGraphics;

            window.StartThread();
        }

        private void Window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            gfx.BeginScene();

            // set the background of the scene (the whole screen; transparent)
            gfx.ClearScene();
                        
            // make rectangular overlays on the left and right sides of the screen
            GameOverlay.Drawing.Rectangle rectangleLeft = GameOverlay.Drawing.Rectangle.Create(0, 0, overlayWidthLeft, actualScreenHeight);
            GameOverlay.Drawing.Rectangle rectangleRight = GameOverlay.Drawing.Rectangle.Create(actualScreenWidth - overlayWidthRight, 0, overlayWidthRight, actualScreenHeight);

            gfx.DrawRectangle(red, rectangleLeft, 0);
            gfx.FillRectangle(red, rectangleLeft);

            gfx.DrawRectangle(red, rectangleRight, 0);
            gfx.FillRectangle(red, rectangleRight);

            gfx.EndScene();
        }

        private void Window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            // dispose of any brushes, fonts, or images here
            red.Dispose();
        }

        private void Window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            red = gfx.CreateSolidBrush(255, 0, 0, overlayAlpha);

            
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // close the host upon closing the window
            host.DisableConnection();

            // kill overlays
            window.Dispose();
        }
              

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnApplyButtonPressed(object sender, RoutedEventArgs e)
        {
            // saves all the settings in the window
            // adjust the overlay windows and set the screen barriers to match 
            overlayWidthLeft = PercentToPixels(sliderLeftWidth.Value, actualScreenWidth);
            leftFixationBarrier = overlayWidthLeft;
            overlayWidthRight = PercentToPixels(sliderRightWidth.Value, actualScreenWidth);
            rightFixationBarrier = actualScreenWidth - overlayWidthRight;

            window.Dispose();
            CreateOverlays();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using CsPotrace;
using Point = System.Windows.Point;

namespace PotraceUI
{
    // Try to Rewrite D3dPotrace UI using WPF
    // Chenchen Zhuo, Nankai University
    // crf_moonlight@live.com
    // 2018-7-2

    public partial class MainWindow : Window
    {
        private PotraceParamsVM paramsVM = new PotraceParamsVM();
        private Bitmap sourceImage = null;
        public static List<List<Curve>> ListOfPathes = new List<List<Curve>>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeControls();
            paramsVM.PropertyChanged += new PropertyChangedEventHandler(ParamsObserver);
        }

        private void InitializeControls()
        {
            var _turnPolicyList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "minority"),
                new KeyValuePair<int, string>(1, "majority"),
                new KeyValuePair<int, string>(2, "right"),
                new KeyValuePair<int, string>(3, "black"),
                new KeyValuePair<int, string>(4, "white")
            };

            txtIgnoreArea.Binding(paramsVM, "IgnoreAreaPixels");
            txtTolerance.Binding(paramsVM, "Tolerance");
            txtCornerThreshold.Binding(paramsVM, "CornerThreshold");
            txtThreshold.Binding(paramsVM, "Threshold");
            sdThreshold.Binding(paramsVM, "ThresholdInt");
            cbTurnPolicy.BindingList<KeyValuePair<int, string>>(_turnPolicyList, "Value", "Key");
            cbTurnPolicy.Binding(paramsVM, "TurnPolicy");
            ckbOptimizing.Binding(paramsVM, "Optimizing");

        }

        private void ParamsObserver(object sender, PropertyChangedEventArgs e)
        {
            paramsVM.ExportParams();
            RefreshImages();
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == true)
            {
                var _filename = OpenFileDialog.FileName;
                sourceImage = Bitmap.FromFile(_filename) as Bitmap;
                imgSource.Source = ImageHelper.ColorBitmap(_filename);
                RefreshImages();
            }
        }

        private void ExportSVG(object sender, RoutedEventArgs e)
        {
            if (sourceImage == null) return;
            if (SaveFileDialog.ShowDialog() == true)
            {
                string svg = Potrace.getSVG();
                string StdPath = SaveFileDialog.FileName;
                System.IO.StreamWriter W = new System.IO.StreamWriter(StdPath);
                W.WriteLine(svg);
                W.Flush();
                W.Close();
            }
        }

        private void SetDefaultParams(object sender, RoutedEventArgs e)
        {
            paramsVM.SetDefaultValues();
            paramsVM.ExportParams();
        }

        private void RefreshImages()
        {
            if (sourceImage == null) return;
            Trace();
            RefreshGdi();
            RefreshSvg();
        }

        private void Trace()
        {
            Potrace.Clear();
            ListOfPathes.Clear();
            Potrace.Potrace_Trace(sourceImage, ListOfPathes);
        }

        private void RefreshGdi()
        {
            PathGeometry _geometry;
            var _figures = new List<PathFigure>();

            for (int i = 0; i < ListOfPathes.Count; i++)
            {
                var _figure = new PathFigure();
                List<Curve> _curves = ListOfPathes[i];
                for (int j = 0; j < _curves.Count; j++)
                {
                    var _curve = _curves[j];
                    if (j == 0) _figure.StartPoint = new Point(_curve.A.x, _curve.A.y);

                    if (_curve.Kind == CurveKind.Line)
                    {
                        _figure.Segments.Add(new LineSegment(new Point(_curve.A.x, _curve.A.y), true));
                        _figure.Segments.Add(new LineSegment(new Point(_curve.B.x, _curve.B.y), true));
                    }

                    else
                    {
                        _figure.Segments.Add(new BezierSegment(
                            new Point(_curve.ControlPointA.x, _curve.ControlPointA.y),
                            new Point(_curve.ControlPointB.x, _curve.ControlPointB.y),
                            new Point(_curve.B.x, _curve.B.y),
                            true));
                    }
                }
                _figures.Add(_figure);
            }
            _geometry = new PathGeometry(_figures);
            pathGdi.Data = _geometry;
        }

        private void RefreshSvg()
        {
            string svg = Potrace.getSVG();
            string StdPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create) + "\\Drawing3d.svg";
            System.IO.StreamWriter W = new System.IO.StreamWriter(StdPath);
            W.WriteLine(svg);
            W.Flush();
            W.Close();
            webSvg.Navigate(new Uri(StdPath));
        }
    }
}

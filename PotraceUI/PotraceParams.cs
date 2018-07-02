using System.ComponentModel;

using CsPotrace;

namespace PotraceUI
{
    class PotraceParams
    {
        public int IgnoreAreaPixels;
        public double Tolerance;
        public double CornerThreshold;
        public int Threshold;
        public int TurnPolicy;
        public bool Optimizing;

        public PotraceParams()
        {
            SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            IgnoreAreaPixels = 2;
            Tolerance = 0.2;
            CornerThreshold = 1.0;
            Optimizing = true;
            TurnPolicy = 0;
            Threshold = 50;
        }

        public void ExportParams()
        {
            Potrace.turdsize = IgnoreAreaPixels;
            Potrace.opttolerance = Tolerance;
            Potrace.alphamax = CornerThreshold;
            Potrace.curveoptimizing = Optimizing;
            Potrace.turnpolicy = (CsPotrace.TurnPolicy)TurnPolicy;
            Potrace.Treshold = ((double)Threshold)/100;
        }
    }

    class PotraceParamsVM : INotifyPropertyChanged
    {
        private PotraceParams obj = new PotraceParams();

        public event PropertyChangedEventHandler PropertyChanged;

        public string IgnoreAreaPixels
        {
            get { return obj.IgnoreAreaPixels.ToString(); }
            set {
                ConvertHelper.String2IntCheck(value, ref obj.IgnoreAreaPixels);
                PropertyChanged(this, new PropertyChangedEventArgs("IgnoreAreaPixels"));
            }
        }

        public string Tolerance
        {
            get { return obj.Tolerance.ToString(); }
            set {
                ConvertHelper.String2DoubleCheck(value, ref obj.Tolerance);
                PropertyChanged(this, new PropertyChangedEventArgs("Tolerance"));
            }
        }

        public string CornerThreshold
        {
            get { return obj.CornerThreshold.ToString(); }
            set {
                ConvertHelper.String2DoubleCheck(value, ref obj.CornerThreshold);
                PropertyChanged(this, new PropertyChangedEventArgs("CornerThreshold"));
            }
        }

        public bool Optimizing
        {
            get { return obj.Optimizing; }
            set {
                obj.Optimizing = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Optimizing"));
            }
        }

        public string TurnPolicy
        {
            get { return obj.TurnPolicy.ToString(); }
            set {
                ConvertHelper.String2IntCheck(value, ref obj.TurnPolicy);
                PropertyChanged(this, new PropertyChangedEventArgs("TurnPolicy"));
            }
        }

        public string Threshold
        {
            get { return obj.Threshold.ToString(); }
            set {
                ConvertHelper.String2IntCheck(value, ref obj.Threshold, 0, 100);
                PropertyChanged(this, new PropertyChangedEventArgs("Threshold"));
                PropertyChanged(this, new PropertyChangedEventArgs("ThresholdInt"));
            }
        }

        public int ThresholdInt
        {
            get { return obj.Threshold; }
            set
            {
                obj.Threshold = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Threshold"));
                PropertyChanged(this, new PropertyChangedEventArgs("ThresholdInt"));
            }
        }

        public void SetDefaultValues()
        {
            obj.SetDefaultValues();
            PropertyChanged(this, new PropertyChangedEventArgs("IgnoreAreaPixels"));
            PropertyChanged(this, new PropertyChangedEventArgs("Tolerance"));
            PropertyChanged(this, new PropertyChangedEventArgs("CornerThreshold"));
            PropertyChanged(this, new PropertyChangedEventArgs("Optimizing"));
            PropertyChanged(this, new PropertyChangedEventArgs("TurnPolicy"));
            PropertyChanged(this, new PropertyChangedEventArgs("Threshold"));
            PropertyChanged(this, new PropertyChangedEventArgs("ThresholdInt"));
        }

        public void ExportParams()
        {
            obj.ExportParams();
        }
    }
}

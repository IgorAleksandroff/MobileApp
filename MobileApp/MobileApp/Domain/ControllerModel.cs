using System.ComponentModel;

namespace MobileApp.Domain

{
    /// <summary>
    /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using different methods.
    /// TypeMethod is assigned sfter calculating Controller's parameters by one of the methods.
    /// Defult value - None, Controller's tunning parameters were not calculated.
    /// </summary>
    public enum TypeMethod
    {
        None = -1,   // Controller's tunning parameters were not calculated
        CohenCoonMethod,
        ZieglerNicholsMethod,
        LamdaMethod,
        ISE,
        IAE,
        ITAE
    }

    /// <summary>
    /// The Proportional, Integral and Derivative modes are arranged into different controller algorithms or controller structures:
    /// 0 - Noninteractive Algorithm;
    /// 1 - Interactive Algorithm;
    /// 2 - Parallel Algorithm;
    /// 3 - CentumVP basic type PID control.
    /// </summary>
    public enum TypeAlgorithm
    {
        Noninteractive = 0,
        Interactive,
        Parallel,
        CentumPID
    }

    /// <summary>
    /// Contains a controller's tunning parameters.
    /// </summary>
    public abstract class ControllerModel : INotifyPropertyChanged
    {
        public static string[] describeMethods = new string[] 
        {
            "Manual tuning",
            "CohenCoonMethod. Fast regulator, unstable. Used tau<2*dT (F,L)",
            "ZieglerNicholsMethod. Fast regulator, unstable. Used where tau>2*dT (P,T)",
            "LamdaMethod. Slow and robust regulator",
            "ISE. Minimizing the Integral of the error squared. For optimizing disturbance response",
            "IAE. Minimizing the Absolute error. For optimizing disturbance response",
            "ITAE. Minimizing the Absolute error multiplied by time. For optimizing disturbance response"
        };
        //public static string[] DescribeMethods { get { return describeMethods; } }
        
        private double p, i, d;
        /// <summary>
        /// The Proportional, Integral and Derivative modes are arranged into different controller algorithms or controller structures:
        /// 0 - Noninteractive Algorithm;
        /// 1 - Interactive Algorithm;
        /// 2 - Parallel Algorithm;
        /// 3 - CentumVP basic type PID control.
        /// </summary>
        public TypeAlgorithm TypeAlg { get; set; }
        public TypeMethod TypeM { get; set; }

        /// <summary>
        /// Proportional parameter (Kc, Kc, Kp or PB).
        /// </summary>
        public double P
        {
            get { return p; }
            set
            {
                if (p != value)
                {
                    p = value; //TODO contract programming
                    OnPropertyChanged("P");
                }
            }
        }
        /// <summary>
        /// Integral parameter (Ti, Ti, Ki or Ti).
        /// </summary>
        public double I
        {
            get { return i; }
            set
            {
                if (i != value)
                {
                    i = value;
                    OnPropertyChanged("I");
                }
            }
        }
        /// <summary>
        /// Derivative parameter (Td, Td, Kd or Td).
        /// </summary>
        public double D
        {
            get { return d; }
            set
            {
                if (d != value)
                {
                    d = value;
                    OnPropertyChanged("D");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
    }
}

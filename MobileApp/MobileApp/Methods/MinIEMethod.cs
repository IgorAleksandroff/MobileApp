using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Methods
{
    /// <summary>
    /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Minimum error-integral tuning rules.
    /// The Minimum error-integral tuning rules were designed for controllers with the noninteractive controller algorithm.
    /// The tuning rules were developed for optimizing a control loop’s disturbance response. 
    /// The Minimum error-integral tuning rules were designed only for processes with time-constants equal to or longer than dead times (tau >= td).
    /// </summary>
    class MinIEMethod
    {
        static double P, I, D;
        private static double[,] constIE = new double[6, 6]
                                        {
                                            {1.305, -0.959, 0.492, 0.739, 0, 0},            // ISE PI
                                            {1.495, -0.945, 1.101, 0.771, 0.560, 1.006},    // ISE PID
                                            {0.984, -0.986, 0.608, 0.707, 0, 0},            // IAE PI
                                            {1.435, -0.921, 0.878, 0.749, 0.482, 1.137},    // IAE PID
                                            {0.859, -0.977, 0.674, 0.680, 0, 0},            // ITAE PI
                                            {1.357, -0.947, 0.842, 0.738, 0.381, 0.995}     // ITAE PID
                                        };

        /// <summary>
        /// Calculating settings for PI/PID using different constants for each IE methods.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="i">MinIEMethod type for coefficients selection.</param>
        internal static IControllerModel TuningIE(ref ObjectModel oM, TypeMethod typeMethod, int i)
        {
            // Calculating Controller Gain (Kc)
            P = constIE[i, 0] * Math.Pow(oM.Dt / oM.Tau1, constIE[i, 1]) / oM.Gp;
            // Calculating Integral Time (Ti)
            I = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, constIE[i, 3]) / constIE[i, 2];
            // Calculating Derivative Time (Td)
            D = oM.Tau1 * constIE[i, 4] * Math.Pow(oM.Dt / oM.Tau1, constIE[i, 5]);
            return new ControllerNoninteractive(typeMethod, P, I);
        }
        
    }
}

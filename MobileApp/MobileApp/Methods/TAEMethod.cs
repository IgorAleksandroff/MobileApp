using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Methods
{
    class TAEMethod : IMethodPI
    {
        // Rows of MinIEMethod.constIE
        private int intPI = 4; 
        private int intPID = 5;

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error multiplied by time tuning rules (ITAE).
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 0.859, -0.977, 0.674, 0.680, 0, 0.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPI(ObjectModel oM)
        { 
            return MinIEMethod.TuningIE(ref oM, TypeMethod.ISE, intPI);
        }
        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error multiplied by time tuning rules (ITAE).
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.357, -0.947, 0.842, 0.738, 0.381, 0.995.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPID(ObjectModel oM)
        {
            return MinIEMethod.TuningIE(ref oM, TypeMethod.ISE, intPID);
        }
    }
}

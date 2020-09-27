using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Methods
{
    class AEMethod : IMethodPI
    {
        // Rows of MinIEMethod.constIE
        private int intPI = 2; 
        private int intPID = 3;

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error tuning rules (IAE).
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 0.984, -0.986, 0.608, 0.707, 0, 0.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPI(ObjectModel oM)
        { 
            return MinIEMethod.TuningIE(ref oM, TypeMethod.ISE, intPI);
        }
        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error tuning rules (IAE).
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.435, -0.921, 0.878, 0.749, 0.482, 1.137.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPID(ObjectModel oM)
        {
            return MinIEMethod.TuningIE(ref oM, TypeMethod.ISE, intPID);
        }
    }
}

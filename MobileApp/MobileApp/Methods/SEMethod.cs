using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Methods
{
    class SEMethod : IMethodPI
    {
        // Rows of MinIEMethod.constIE
        private int intPI = 0; 
        private int intPID = 1;

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Integral of the error squared tuning rules (ISE).
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.305, -0.959, 0.492, 0.739, 0, 0.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPI(ObjectModel oM)
        { 
            return MinIEMethod.TuningIE(ref oM, TypeMethod.ISE, intPI);
        }
        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc) and Integral Time (Ti) using the Integral of the error squared tuning rules (ISE).
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.495, -0.945, 1.101, 0.771, 0.560, 1.006.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPID(ObjectModel oM)
        {
            return MinIEMethod.TuningIE(ref oM, TypeMethod.ISE, intPID);
        }
    }
}

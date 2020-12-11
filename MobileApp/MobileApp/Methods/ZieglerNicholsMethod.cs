using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Methods
{
    /// <summary>
    /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Ziegler-Nichols tuning rules.
    /// The Ziegler-Nichols tuning rules were designed for controllers with the interactive controller algorithm.
    /// The Ziegler-Nichols tuning rules work well on processes of which the time constant is at least two times as long as the dead time.
    /// </summary>
    class ZieglerNicholsMethod : IMethodPI
    {
        double P, I, D;
        /// <summary>
        /// Calculating settings for P Controller Gain (Kc= tau / (gp * td)) using the Ziegler-Nichols tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerInteractive's tunning parameters.</returns>
        public ControllerInteractive TuningP(ObjectModel oM)
        {
            // Calculating Controller Gain (Kc)
            P = oM.Tau1 / (oM.Gp * oM.Dt*2); //divide P by two to reduce overshoot

            return new ControllerInteractive(P);
        }

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc = 0.9 * tau / (gp * td)) and Integral Time (Ti = 3.33 * td) using the Ziegler-Nichols tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerInteractive's tunning parameters.</returns>
        public IControllerModel TuningPI(ObjectModel oM)
        {
            // Calculating Controller Gain (Kc)
            P = 0.9 * oM.Tau1 / (oM.Gp * oM.Dt*2); //divide P by two to reduce overshoot
            // Calculating Integral Time (Ti)
            I = 3.33 * oM.Dt;

            return new ControllerInteractive(TypeMethod.ZieglerNicholsMethod, P, I);
        }

        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Ziegler-Nichols tuning rules.
        /// For PID control: Kc = 1.2 * tau / (gp * td); Ti = 2 * td; Td = 0.5 * td.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerInteractive's tunning parameters.</returns>
        public ControllerInteractive TuningPID(ObjectModel oM)
        {
            // Calculating Controller Gain (Kc)
            P = 1.2 * oM.Tau1 / (oM.Gp * oM.Dt*2); //divide P by two to reduce overshoot
            // Calculating Integral Time (Ti)
            I = 2 * oM.Dt;
            // Calculating Derivative Time (Td)
            D = 0.5 * oM.Dt;

            return new ControllerInteractive(P, I, D);
        }
    }
}

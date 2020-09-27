using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Methods
{
    /// <summary>
    /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Lambda tuning rules.
    /// The Lambda tuning rules were designed for controllers with interactive or noninteractive controller algorithm.
    /// The Lambda tuning rules, sometimes also called Internal Model Control (IMC)* tuning, offer a robust alternative to tuning rules aiming for speed, like Ziegler-Nichols, Cohen-Coon, etc.
    /// </summary>
    class LambdaMethod : IMethodPI
    {
        double P, I;
        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc= tau / (gp x (taucl + td))) and Integral Time (Ti = tau) using the Lambda tuning rules.
        /// Closed loop time constant (taucl) = tau of the model.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a ControllerNoninteractive's tunning parameters.</returns>
        public IControllerModel TuningPI(ObjectModel oM)
        {
            // Calculating Controller Gain (Kc)
            P = oM.Tau1 / (oM.Gp * (oM.Tau1 + oM.Dt));
            // Calculating Integral Time (Ti)
            I = oM.Tau1;

            return new ControllerNoninteractive(TypeMethod.LamdaMethod, P, I);
        }
    }
}

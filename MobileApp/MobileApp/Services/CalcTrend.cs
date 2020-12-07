using MobileApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services
{
    public class CalcTrend
    {
        /// <summary>
        /// Calculation of an output trend of the PID controller.
        /// </summary>
        /// <returns>Process variable after PID & Model</returns>
        public static double[,] CalcTrendPID(ObjectModel om, ControllerModel cm, double sv0 = 20, double pv0 = 10, double mv0 = 5)
        {
            int delta = 1; // Time different between x[i] and x[i-1]
            int delay = Convert.ToInt32(Math.Ceiling(om.Dt / delta));
            int len = Convert.ToInt32(om.Tau1) * Convert.ToInt32(om.Gp) * 2 + delay;
            double[,] y = new double[7, len];
            
            // stable state
            y[0, 0] = pv0;                  // PV
            y[1, 0] = pv0;                  // SV
            y[3, 0] = y[0, 0] - y[1, 0];    // E = PV - SV
            y[4, 0] = 0;                    // part P for mv
            y[5, 0] = 0;                    // part I for mv
            y[6, 0] = 0;                    // part D for mv
            y[2, 0] = mv0;                  // MV

            y[0, 1] = y[0, 0];              // PV
            y[1, 1] = sv0;                  // SV
            y[3, 1] = y[0, 1] - y[1, 1];    // E = PV - SV
            //y[2, 1] = y[2, 0] - (y[3, 1] - y[3, 0] + y[3, 1] * delta / cm.I + (y[3, 1] - 2 * y[3, 0]) * cm.D / delta) * 100 / cm.P; // MV
            y[4, 1] = (y[3, 1] - y[3, 0]) * 100 / cm.P;                         // part P for mv
            y[5, 1] = cm.I == 0 ? 0 : (y[3, 1] * delta / cm.I) * 100 / cm.P;    // part I for mv
            y[6, 1] = ((y[3, 1] - 2 * y[3, 0]) * cm.D / delta) * 100 / cm.P;    // part D for mv
            y[2, 1] = y[2, 0] - y[4, 1] - y[5, 1] - y[6, 1];                    // mv

            // next PV, MV calculated via a linear difference equation
            for (int i = 2; i < len; i++)
            {
                if (i <= delay+2)
                {
                    y[0, i] = y[0, i - 1];
                }
                else
                {
                    y[0, i] = y[2, i - 1] * delta * om.Gp / (om.Tau1 + delta) + y[0, i - 1] * om.Tau1 / (om.Tau1 + delta); // PV
                }
                y[1, i] = y[1, i - 1];          // SV
                y[3, i] = y[0, i] - y[1, i];    // E = PV - SV
                //y[2, i] = y[2, i - 1] - (y[3, i] - y[3, i - 1] + y[3, i] * delta / cm.I + (y[3, i] - 2 * y[3, i - 1] + y[3, i - 2]) * cm.D / delta) * 100 / cm.P; //MV
                y[4, i] = (y[3, i] - y[3, i - 1]) * 100 / cm.P;                                     // part P for mv
                y[5, i] = cm.I == 0 ? 0 : (y[3, i] * delta / cm.I) * 100 / cm.P;                    // part I for mv
                y[6, i] = ((y[3, i] - 2 * y[3, i - 1] + y[3, i - 2]) * cm.D / delta) * 100 / cm.P;  // part D for mv
                y[2, i] = y[2, i - 1] - y[4, i] - y[5, i] - y[6, i];                                // mv
            }


            return y;
        }
    }
}

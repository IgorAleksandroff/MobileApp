using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Domain
{
    /// <summary>
    /// Contains model's parameters. Ones describe the control object through the transfer function.
    /// The parameters are calculated experimentally by indetification or by analytical methods.
    /// The parameters of the control object are necessary for calculating the PID controler's tunning parameters of the control loop.
    /// </summary>
    public abstract class ObjectModel
    {
        private double dt;
        private double tau1;
        private double tau2;
        private double beta;

        //if (value >= 0) dt = value;

        /// <summary>
        /// Process gain.
        /// Gp = change in PV [in %] / change in CO [in %].
        /// </summary>
        public double Gp { get; set; }

        /// <summary>
        /// Dead time.
        /// Difference between the step-change in CO and the beginning of change PV response curve.
        /// </summary>
        public double Dt {
            get { return dt; }
            set
            {
                dt = (value >= 0) ? value : 1; //TODO contract programming
            }
        }

        /// <summary>
        /// Time constant.
        /// Difference between intersection at the end of dead time, and the PV reaching 63% of its total change.
        /// </summary>
        public double Tau1 {
            get { return tau1; }
            set
            {
                tau1 = (value >= 0) ? value : 0; //TODO contract programming
            }
        }

        /// <summary>
        /// Time constant of second order.
        /// </summary>
        public double Tau2 {
            get { return tau2; }
            set
            {
                tau2 = (value >= 0) ? value : 0; //TODO contract programming
            }
        }

        /// <summary>
        /// Time constant of second order..
        /// </summary>
        public double Beta {
            get { return beta; }
            set
            {
                beta = (value >= 0) ? value : 0; //TODO contract programming
            }
        }
    }

    class ObjectModels : ObjectModel
    {
        
        /// <summary>
        /// Blank model. Gp=1, Td=0, Tau1=0, Tau2=0, Beta=0.
        /// </summary>
        public ObjectModels()
        {
            Gp = 1.0;
            Dt = 0.0;
            Tau1 = 0.0;
            Tau2 = 0.0;
            Beta = 0.0;
        }

        /// <summary>
        /// 1st order model. Tau2=0, Beta=0.
        /// </summary>
        public ObjectModels(double gp, double td, double tau1)
        {
            Gp = gp;
            Dt = td;
            Tau1 = tau1;
            Tau2 = 0.0;
            Beta = 0.0;
        }

        /// <summary>
        /// Calculation of an output trend of the first order transfer function.
        /// </summary>
        /// <param name="x"> Input control action (Controller output)</param>
        /// <param name="y0"></param>
        /// <returns>Process variable after the element</returns>
        public double[] CalcTrend(double[] x, double y0 = 50)
        {
            int len = x.Length;
            double[] y = new double[len];
            int delta = 2; // Time different between x[i] and x[i-1]

            // first element of first order = 0
            // next element calculated via a linear difference equation
            for (int i = 2; i < len; i++)
            {
                y[i] = x[i] * delta * Gp/(Tau1 + delta) + y[i-1] * Tau1 / (Tau1 + delta);
            }

            return y;
        }
        public double[] CalcTrendD(double[] x, double y0 = 50, double x0 = 1)
        {
            int len = x.Length;
            double[] y = new double[len];
            int delta = 2; // Time different between x[i] and x[i-1]

            // first element of first order = 0
            y[0] = y0;
            x[0] = x0;
            y[1] = y0;
            x[1] = x0;
            // next element calculated via a linear difference equation
            for (int i = 2; i < len; i++)
            {
                y[i] = ((x[i] - x[i-1]) * delta * Gp + y[i-1] * (2 * Tau1 + delta) - y[i-2] * Tau1) / (Tau1 + delta);
            }

            return y;
        }

    }
}

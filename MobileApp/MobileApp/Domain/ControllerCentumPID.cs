namespace MobileApp.Domain
{
    /// <summary>
    /// CentumPID Controller Algorithm. P = PB (Proportional band); I = Ti (Integral Time); D = Td (Derivative Time).   
    /// </summary>
    public class ControllerCentumPID : ControllerModel, ITransferFunction
    {
        /// <summary>
        /// Creating CentumPID Controller.
        /// </summary>
        /// <param name="p">Proportional band</param>
        /// <param name="i">Integral Time</param>
        /// <param name="d">Derivative Time</param>
        public ControllerCentumPID(double p = 1, double i = 1, double d = 0)
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            TypeM = TypeMethod.None;
            P = p;
            I = i;
            D = d;
        }
        // TODO TypeMethod
        public ControllerCentumPID(TypeMethod typeM, double p = 1, double i = 0, double d = 0) : this(p, i, d)
        {
            TypeM = typeM;
        }

        /// Computation out signal after controller.
        public double TransferFunction(double input)
        {
            //TODO ControllerCentumPID TransferFunction
            return input;
        }
        /// <summary>
        /// Calculation of an output trend of the PID controller.
        /// </summary>
        /// <param name="x"> Input Deviation(Ei=PVi-SVi)</param>
        /// <param name="y0"></param>
        /// <param name="x0"></param>
        /// <returns>Process variable after the element</returns>
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
                y[i] = y[i - 1] + (x[i] - x[i - 1] + x[i] * delta / I + (x[i] - 2 * x[i - 1] + x[i - 2]) * D / delta) * 100 / P;
            }

            return y;
        }

            /// <summary>
            /// Converting Interactive to CentumPID Controller Algorithm.
            /// </summary>
            /// <param name="ctr">Interactive Controller Algorithm </param>
            public void Convert(ControllerInteractive ctr)
        {
            P = 100 * ctr.I / (ctr.P * (ctr.I + ctr.D));
            I = ctr.I + ctr.D;
            D = ctr.I * ctr.D / (ctr.I + ctr.D);
        }

        /// <summary>
        /// Converting Parallel to CentumPID Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Parallel Controller Algorithm </param>
        public void Convert(ControllerParallel ctr)
        {
            P = 100 / ctr.P;
            I = ctr.P / ctr.I;
            D = ctr.D / ctr.P;
        }

        /// <summary>
        /// Converting Noninteractive to CentumPID Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Noninteractive Controller Algorithm </param>
        public void Convert(ControllerNoninteractive ctr)
        {
            P = 100 / ctr.P;
            I = ctr.I;
            D = ctr.D;
        }
    }
}

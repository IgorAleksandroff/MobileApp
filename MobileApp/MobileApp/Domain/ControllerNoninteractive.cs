namespace MobileApp.Domain
{
    /// <summary>
    /// Noninteractive Controller Algorithm. P = Kc (Controller Gain); I = Ti (Integral Time); D = Td (Derivative Time).   
    /// </summary>
    public class ControllerNoninteractive : ControllerModel, IControllerModel, ITransferFunction
    {
        /// <summary>
        /// Creating PID Noninteractive Controller.
        /// </summary>
        /// <param name="p">Controller Gain</param>
        /// <param name="i">Integral Time</param>
        /// <param name="d">Derivative Time</param>
        public ControllerNoninteractive(double p = 1, double i = 0, double d = 0)
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            TypeM = TypeMethod.None;
            P = p;
            I = i;
            D = d;
        }
        // TODO TypeMethod
        public ControllerNoninteractive(TypeMethod typeM, double p = 1, double i = 0, double d = 0) : this(p, i, d)
        {
            TypeM = typeM;
        }

        /// Computation out signal after controller.
        public double TransferFunction(double input)
        {
            //TODO ControllerNoninteractive TransferFunction
            return input;
        }

        /// <summary>
        /// Converting Interactive to Noninteractive Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Interactive Controller Algorithm </param>
        public void Convert(ControllerInteractive ctr)
        {
            P = ctr.P * (1 + ctr.D / ctr.I);
            I = ctr.I + ctr.D;
            D = ctr.I * ctr.D / (ctr.I + ctr.D);
        }

        /// <summary>
        /// Converting Parallel to Noninteractive Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Parallel Controller Algorithm </param>
        public void Convert(ControllerParallel ctr)
        {
            P = ctr.P;
            I = ctr.P / ctr.I;
            D = ctr.D / ctr.P;
        }

        /// <summary>
        /// Converting CentumPID to Noninteractive Controller Algorithm.
        /// </summary>
        /// <param name="ctr">CentumPID Controller Algorithm </param>
        public void Convert(ControllerCentumPID ctr)
        {
            P = 100 / ctr.P;
            I = ctr.I;
            D = ctr.D;
        }
        /// <summary>
        /// Converting Noninteractive to CentumPID Controller Algorithm.
        /// </summary>
        /// <returns>CentumPID Controller Algorithm</returns>
        public ControllerCentumPID GetControllerCentumPID()
        {
            ControllerCentumPID ctr = new ControllerCentumPID();
            ctr.P = 100 / P;
            ctr.I = I;
            ctr.D = D;
            return ctr;
        }
    }
}

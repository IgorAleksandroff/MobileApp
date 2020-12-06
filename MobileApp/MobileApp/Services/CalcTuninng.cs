using MobileApp.Domain;
using MobileApp.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MobileApp.Services
{
    /// <summary>
    /// Calculating settings for PID using the all rules.
    /// </summary>
    public class CalcTuninng
    {
        private static List<ControllerModel> contrList;
        private static List<IMethodPI> methodList;
        //private static ControllerCentumPID controller;

        /// <summary>
        /// Calculating settings for CentumPID Controller Algorithm. P = PB (Proportional band); I = Ti (Integral Time); D = Td (Derivative Time) using the all rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <returns>Contains a list of ControllerCentumPID for each rule</returns>
        public static List<ControllerModel> CalcPI(ObjectModel oM)
        {
            contrList = new List<ControllerModel>();
            methodList = new List<IMethodPI>();

            methodList.Add(new CohenCoonMethod());
            methodList.Add(new ZieglerNicholsMethod());
            methodList.Add(new LambdaMethod());
            methodList.Add(new SEMethod());
            methodList.Add(new AEMethod());
            methodList.Add(new TAEMethod());

            //IControllerModel co = methodList[0].TuningPI(oM);
            contrList.Add((methodList[0].TuningPI(oM)).GetControllerCentumPID());
            contrList.Add((methodList[1].TuningPI(oM)).GetControllerCentumPID());

            contrList.Add((methodList[2].TuningPI(oM)).GetControllerCentumPID());

            contrList.Add((methodList[3].TuningPI(oM)).GetControllerCentumPID());
            contrList.Add((methodList[4].TuningPI(oM)).GetControllerCentumPID());
            contrList.Add((methodList[5].TuningPI(oM)).GetControllerCentumPID());


            //co = methodList[1].TuningPI(oM);
            //contrList.Add(co.GetControllerCentumPID());

            /*controller = new ControllerCentumPID();
            controller.Convert(MinIEMethod.ISEtuningPI(oM));
            contrList.Add(controller);

            controller = new ControllerCentumPID();
            controller.Convert(MinIEMethod.IAEtuningPI(oM));
            contrList.Add(controller);

            controller = new ControllerCentumPID();
            controller.Convert(MinIEMethod.ITAEtuningPI(oM));
            contrList.Add(controller);

            controller = new ControllerCentumPID();
            controller.Convert(LambdaMethod.TuningPI(oM));
            contrList.Add(controller);*/

            return contrList;
        }
        
    }
}

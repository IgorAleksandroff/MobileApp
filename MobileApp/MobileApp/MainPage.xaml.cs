using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileApp.Domain;
using MobileApp.Methods;
using MobileApp.Services;
using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public ControllerCentumPID controller = new ControllerCentumPID(100, 20, 0);
        ObjectModels objectConrtol = new ObjectModels(2, 5, 60);
        public List<ControllerModel> ContrList { get; set; }
        public ObservableCollection<ControllerCentumPID> ContrObList { get; set; }
        public ObservableCollection<ChartDataPoint> TrendObject { get; set; }
        public ObservableCollection<ChartDataPoint> TrendModel { get; set; }


        public MainPage()
        {
            InitializeComponent();
            
            ContrObList = new ObservableCollection<ControllerCentumPID> { controller };
            TrendObject = new ObservableCollection<ChartDataPoint>();
            TrendModel = new ObservableCollection<ChartDataPoint>();

            this.BindingContext = this;

            tbGp.Text = objectConrtol.Gp.ToString();
            tbDt.Text = objectConrtol.Dt.ToString();
            tbTau1.Text = objectConrtol.Tau1.ToString();
        }
        private void btCSV_Click(object sender, EventArgs e)
        {
            // for test
            //ContrList.Add(new ControllerCentumPID { P=1, I=2,D=3 });
            ContrObList[0].P += 7;
        }

        private void btTunPI_Click(object sender, EventArgs e)
        {
            readObject();
            calcObjectChart();

            ContrList =CalcTuninng.CalcPI(objectConrtol);

            ContrObList.Clear();
            ContrObList.Add(controller);
            foreach (ControllerCentumPID CPID in ContrList) ContrObList.Add(CPID);
        }

        private void btChart_Click(object sender, EventArgs e)
        {
            readPID();
            calcModelChart();
        }

        private void readObject()
        {
            objectConrtol.Gp = double.Parse(tbGp.Text);
            objectConrtol.Dt = double.Parse(tbDt.Text);
            objectConrtol.Tau1 = double.Parse(tbTau1.Text);
        }
        private void readPID()
        {
            controller.P = double.Parse(txP.Text);
            controller.I = double.Parse(txI.Text);
            controller.D = double.Parse(txD.Text);
        }
        
        private void calcObjectChart()
        {
            TrendObject.Clear();
            TrendModel.Clear();

            double[,] objectTrend = objectConrtol.CalcTrend();

            int lenXY = objectTrend.GetUpperBound(1) + 1;

            for (int i = 0; i < lenXY; i++)
            {
                TrendObject.Add(new ChartDataPoint(i, objectTrend[1, i]));
            }
        }
        private void calcModelChart()
        {
            double[,] modelTrend = CalcTrend.CalcTrendPID(objectConrtol, controller);

            int lenXY = modelTrend.GetUpperBound(1) + 1;

            for (int i = 0; i < lenXY; i++)
            {
                TrendModel.Add(new ChartDataPoint(i, modelTrend[0, i]));
            }
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ControllerCentumPID selectedPID = e.Item as ControllerCentumPID;
            if (selectedPID != null)
                await DisplayAlert("Describe:", $"{ControllerModel.describeMethods[ContrObList.IndexOf(selectedPID)]}", "OK");
        }
    }
}

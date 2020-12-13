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
using SkiaSharp;
using Microcharts;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileApp
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public ControllerCentumPID controller = new ControllerCentumPID(100, 20, 0);
        ObjectModels objectConrtol = new ObjectModels(2, 5, 30);
        public List<ControllerModel> ContrList { get; set; }
        public ObservableCollection<ControllerCentumPID> ContrObList { get; set; }
        public double[,] modelTrend;
        public ObservableCollection<ChartDataPoint> TrendObject { get; set; }
        public ObservableCollection<ChartDataPoint> TrendModel { get; set; }
        public ObservableCollection<ChartDataPoint> TrendP { get; set; }
        public ObservableCollection<ChartDataPoint> TrendI { get; set; }
        public ObservableCollection<ChartDataPoint> TrendD { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ChartEntry> entries { get; set; }
        public Microcharts.Chart Chart1 { get; set; }

        public MainPage()
        {
            InitializeComponent();
            
            ContrObList = new ObservableCollection<ControllerCentumPID> { controller };
            TrendObject = new ObservableCollection<ChartDataPoint>();
            TrendModel = new ObservableCollection<ChartDataPoint>();
            TrendP = new ObservableCollection<ChartDataPoint>();
            TrendI = new ObservableCollection<ChartDataPoint>();
            TrendD = new ObservableCollection<ChartDataPoint>();

            entries = new ObservableCollection<ChartEntry>();
            entries.Add(new ChartEntry(10));
            entries.Add(new ChartEntry(15));
            Chart1 = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.None,
                LineSize = 2,
                BackgroundColor = SKColors.Transparent,
            };

            this.BindingContext = this;

            tbGp.Text = objectConrtol.Gp.ToString();
            tbDt.Text = objectConrtol.Dt.ToString();
            tbTau1.Text = objectConrtol.Tau1.ToString();
        }
        private void btCSV_Click(object sender, EventArgs e)
        {
            FileManager.WriteToCSV(modelTrend);
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
            controller.P = double.Parse(txP.Text ?? "100");
            controller.I = double.Parse(txI.Text ?? "20");
            controller.D = double.Parse(txD.Text ?? "0");
        }
        
        private void calcObjectChart()
        {
            TrendObject.Clear();
            TrendModel.Clear();
            TrendP.Clear();
            TrendI.Clear();
            TrendD.Clear();

            //entries.Clear();

            double[,] objectTrend = objectConrtol.CalcTrend(10/objectConrtol.Gp, 20/objectConrtol.Gp);

            int lenXY = objectTrend.GetUpperBound(1) + 1;

            for (int i = 0; i < lenXY; i++)
            {
                TrendObject.Add(new ChartDataPoint(i, objectTrend[1, i]));
                entries.Add(new ChartEntry(Convert.ToSingle(objectTrend[1, i])));
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Chart1)));
            //Chart01.Chart = new LineChart
            //{
            //    Entries = entries,
            //    LineMode = LineMode.Straight,
            //    PointMode = PointMode.None,
            //    LineSize = 8,
            //    BackgroundColor = SKColors.Transparent,
            //};

        }
        private void calcModelChart()
        {
            modelTrend = CalcTrend.CalcTrendPID(objectConrtol, controller,20,10,10/objectConrtol.Gp);

            int lenXY = modelTrend.GetUpperBound(1) + 1;

            for (int i = 0; i < lenXY; i++)
            {
                TrendModel.Add(new ChartDataPoint(i, modelTrend[0, i]));
                TrendP.Add(new ChartDataPoint(i, modelTrend[4, i]));
                TrendI.Add(new ChartDataPoint(i, modelTrend[5, i]));
                TrendD.Add(new ChartDataPoint(i, modelTrend[6, i]));
            }

            entries.Add(new ChartEntry(5));
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ControllerCentumPID selectedPID = e.Item as ControllerCentumPID;
            if (selectedPID != null)
                await DisplayAlert("Describe:", $"{ControllerModel.describeMethods[ContrObList.IndexOf(selectedPID)]}", "OK");
        }
    }
}

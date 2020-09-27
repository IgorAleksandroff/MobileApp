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

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public ControllerCentumPID controller = new ControllerCentumPID(5, 6, 7);
        ObjectModels objectConrtol = new ObjectModels(0.5, 5, 60);
        public List<ControllerModel> ContrList { get; set; }
        public ObservableCollection<ControllerCentumPID> ContrObList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            //btTunPI.Clicked += btTunPI_Click;
            ContrObList = new ObservableCollection<ControllerCentumPID> { new ControllerCentumPID(4, 6, 7)};

            this.BindingContext = this;

            tbGp.Text = objectConrtol.Gp.ToString();
            tbDt.Text = objectConrtol.Dt.ToString();
            tbTau1.Text = objectConrtol.Tau1.ToString();
        }
        private void readModel()
        {
            objectConrtol.Gp = double.Parse(tbGp.Text);
            objectConrtol.Dt = double.Parse(tbDt.Text);
            objectConrtol.Tau1 = double.Parse(tbTau1.Text);
        }

        private void btTunP_Click(object sender, EventArgs e)
        {
            // for test
            //ContrList.Add(new ControllerCentumPID { P=1, I=2,D=3 });
            ContrObList[0].P += 7;
        }

        private void btTunPI_Click(object sender, EventArgs e)
        {
            readModel();

            ContrList=CalcTuninng.CalcPI(objectConrtol);

            ContrObList.Clear();
            foreach (ControllerCentumPID CPID in ContrList) ContrObList.Add(CPID);
        }

        private void btTunPID_Click(object sender, EventArgs e)
        {
            readModel();
            
        }
        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ControllerCentumPID selectedPID = e.Item as ControllerCentumPID;
            if (selectedPID != null)
                await DisplayAlert("Describe:", $"{ControllerModel.describeMethods[ContrObList.IndexOf(selectedPID)]}", "OK");
        }
    }
}

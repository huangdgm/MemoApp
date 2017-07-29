using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void NewButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;

            await DisplayAlert("Recording",
                "Your voice is being recoreded.",
                "Cancel",
                "Stop");

            App.MemoItems.Add("memo item");
        }

        async void HistoryButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HistoryPage());
        }
    }
}

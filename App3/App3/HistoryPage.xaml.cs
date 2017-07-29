using System;
using App3.Models;
using Xamarin.Forms;

namespace App3
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.MemoManager.GetAllItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemoItemPage
            {
                MemoItem = new MemoItem()
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new MemoItemPage
            {
                MemoItem = e.SelectedItem as MemoItem
            });
        }
    }
}

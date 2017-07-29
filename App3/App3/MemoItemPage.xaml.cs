using System;
using System.Diagnostics;
using System.Linq;
using App3.Models;
using App3.Services;
using Xamarin.Forms;

namespace App3
{
    public partial class MemoItemPage : ContentPage
    {
        App3.Services.IBingSpeechService bingSpeechService;

        bool isRecording = false;

        public static readonly BindableProperty MemoItemProperty =
            BindableProperty.Create("MemoItem", typeof(MemoItem), typeof(MemoItemPage), null);

        public MemoItem MemoItem
        {
            get { return (MemoItem)GetValue(MemoItemProperty); }
            set { SetValue(MemoItemProperty, value); }
        }

        public static readonly BindableProperty IsProcessingProperty =
            BindableProperty.Create("IsProcessing", typeof(bool), typeof(MemoItemPage), false);

        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }

        public MemoItemPage()
        {
            InitializeComponent();

            bingSpeechService = new BingSpeechService(new AuthenticationService(Constants.BingSpeechApiKey), Device.OS.ToString());
        }

        async void OnRecognizeSpeechButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var audioRecordingService = DependencyService.Get<IAudioRecorderService>();

                if (!isRecording)
                {
                    audioRecordingService.StartRecording();

                    ((Button)sender).Image = "recording.png";
                    IsProcessing = true;
                }
                else
                {
                    audioRecordingService.StopRecording();
                }

                isRecording = !isRecording;

                if (!isRecording)
                {
                    var speechResult = await bingSpeechService.RecognizeSpeechAsync(Constants.AudioFilename);
                    Debug.WriteLine("Name: " + speechResult.Name);
                    Debug.WriteLine("Confidence: " + speechResult.Confidence);

                    if (!string.IsNullOrWhiteSpace(speechResult.Name))
                    {
                        MemoItem.Name = char.ToUpper(speechResult.Name[0]) + speechResult.Name.Substring(1);
                        OnPropertyChanged("MemoItem");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (!isRecording)
                {
                    ((Button)sender).Image = "record.png";
                    IsProcessing = false;
                }
            }
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MemoItem.Name))
            {
                await App.MemoManager.SaveItemAsync(MemoItem);
            }
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            await App.MemoManager.DeleteItemAsync(MemoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using Shipfer.ViewModels;
using System.Globalization;
using System.Threading;
using System;

namespace Shipfer.Views;

public partial class ChatPage : ContentPage
{
    private readonly ChatViewModel _viewModel;
    private CancellationToken cancellationToken = new();

    public ChatPage(ChatViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void MicrophineButton_Pressed(object sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        try
        {
            MicrophineButton.Color = Color.FromArgb("#F08080");
            var isGranted = await SpeechToText.Default.RequestPermissions(cancellationToken);
            if (!isGranted)
            {
                await Toast.Make("Permission not granted").Show(CancellationToken.None);
                return;
            }
            var currentCulture = CultureInfo.CurrentCulture;

            var recognitionResult = await SpeechToText.Default.ListenAsync(
                                        CultureInfo.GetCultureInfo(currentCulture.Name),
                                        new Progress<string>(partialText =>
                                        {
                                            _viewModel.UserMessage += partialText + " ";
                                        }), cts.Token);

            if (recognitionResult.IsSuccessful)
            {
                _viewModel.UserMessage = recognitionResult.Text;
            }
            else
            {
                await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
            }
        }
        finally
        {
            MicrophineButton.Color = Color.FromArgb("#52D681");
            cts.Dispose();
        }
    }
}
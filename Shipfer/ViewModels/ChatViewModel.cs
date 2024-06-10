using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shipfer.Helpers;
using Shipfer.Models;
using Shipfer.Services;
using System.Collections.ObjectModel;
using System.Text;

namespace Shipfer.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _userMessage = string.Empty;

    [ObservableProperty]
    private bool _playRecentBotResponse = true;

    private readonly LanguageUnderstandingService _languageUnderstandingService;
    private readonly IShipping360Service _shipping360Service;

    public ObservableCollection<Message> Messages { get; set; } = [];

    public ChatViewModel()
    {
        AddBotMessage(new Message { IsBot = true, Text = """
            👋 Welcome to our shipping bot!
            Getting Started: Type your query or choose an option from the menu.
            Asking Questions: Type "help" for assistance.
            Fastest Shipping Rates: Use "Get Fastest Shipping Rates". Include the destination country code (e.g., "US", "CA").
            Input Format:
            - Weight: Include weight and unit (e.g., "3 kg").
            - Dimensions: Use "LxWxH" (e.g., "14x9x7").
            - Address: Clearly mention the destination address.
            - Country Code: Use standard codes (e.g., "US", "CA", "UK").
            Happy shipping! 📦

            Note: Currently, each interaction is independent and uses your profile data for the best results.
            """ });

        _languageUnderstandingService = new LanguageUnderstandingService();
        _shipping360Service = new Shipping360Service();
    }

    [RelayCommand]
    public async Task SendMessage()
    {
        if (string.IsNullOrEmpty(UserMessage)) return;

        Messages.Add(new Message
        {
            Text = UserMessage
        });

        var userInput = UserMessage;
        UserMessage = string.Empty;
        try
        {
            await HandleUserInputAsync(userInput);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private async Task AddBotMessage(Message message)
    {
        Messages.Add(message with { IsBot = true });
        if (PlayRecentBotResponse)
        {
            await Play(message.Text);
        }
    }

    public async Task HandleUserInputAsync(string userInput)
    {
        var prediction = _languageUnderstandingService.GetIntentsAndEntities(userInput);
        var topIntent = prediction.Result.Prediction.TopIntent;

        switch (topIntent)
        {
            case "CheckShipmentStatus":
                await HandleCheckShipmentStatusAsync(prediction.Result.Prediction.Entities);
                break;
            case "PrintLabel":
                await HandlePrintLabelAsync(prediction.Result.Prediction.Entities);
                break;
            case "GetCheapestOptionShippingRates":
                await HandleGetCheapestOptionAsync(prediction.Result.Prediction.Entities);
                break;
            case "GetFastestOptionShippingRates":
                await HandleGetFastestOptionAsync(prediction.Result.Prediction.Entities);
                break;
            case "ProvideDetails":
                await HandleProvideDetailsAsync(prediction.Result.Prediction.Entities);
                break;
            case "GeneralHelp":
                ShowHelp();
                break;
            default:
                AddBotMessage(new Message { Text = "Sorry, I didn't understand that. Please try again." });
                break;
        }
    }


    private async Task HandleGetFastestOptionAsync(List<Entity> entities)
    {
        string weightText = entities.FirstOrDefault(e => e.Category == "Weight")?.Text;
        double weight = ExtractNumber(weightText);

        string countryCode = entities.FirstOrDefault(e => e.Category == "CountryCode" && e.ExtraInformation != null && e.ExtraInformation.Any(info => info.ExtraInformationKind == "RegexKey"))?.Text;

        string destinationAddress = entities.FirstOrDefault(e => e.Category == "DestinationAddress")?.Text;

        string postalCode = entities.FirstOrDefault(e => e.Category == "PostalCode")?.Text;

        string dimensionsText = entities.FirstOrDefault(e => e.Category == "Dimensions")?.Text;

        string[] dimensions = dimensionsText.Split('x');
        int length = int.Parse(dimensions[0]);
        int width = int.Parse(dimensions[1]);
        int height = int.Parse(dimensions[2]);

        var originProfile = PreferenceHelper.GetUserDetails().userProfile;

        RateRequest rateRequest = new()
        {
            DateOfShipment = DateTime.Now.ToString("yyyy-MM-dd"),
            ToAddress = new()
            {
                AddressLine1 = destinationAddress,
                CountryCode = countryCode,
                PostalCode = postalCode
            },
            Parcel = new()
            {
                DimUnit = "IN",
                WeightUnit = "OZ",
                Weight = weight,
                Height = height,
                Width = width,
                Length = length
            },
            FromAddress = new()
            {
                AddressLine1 = originProfile.Address,
                CountryCode = originProfile.CountryCode,
                PostalCode = originProfile.Postal
            },
            ParcelType = "PACK",
            CarrierAccounts =
            [
                "jg5Z5pgl29A", "gGNZD21Ll4B", "N37277g61m9", "7ZkJrggoPXK"
            ],
        };

        AddBotMessage(new Message { Text = "Getting rates now..." });

        var rates = await _shipping360Service.GetRates(rateRequest);

        if(rates == null)
        {
            AddBotMessage(new Message { Text = "Error: Couldn't get rate" });
            return;
        }

        var sortedRates = rates.Rates.OrderBy(rate =>
        {
            if (DateTime.TryParse(rate.DeliveryCommitment.EstimatedDeliveryDateTime, out DateTime deliveryDateTime))
            {
                return deliveryDateTime;
            }

            return DateTime.MaxValue;
        }).Take(5).ToList();

        StringBuilder messageBuilder = new StringBuilder();
        messageBuilder.AppendLine("Here are the top 5 rates arranged by nearest expected delivery time and date:");

        foreach (var rate in sortedRates)
        {
            int number = 1;
            messageBuilder.AppendLine($"{number}. Carrier: {rate.Carrier.ToUpper()}, Estimated Delivery Time: {rate.DeliveryCommitment.EstimatedDeliveryDateTime}");
        }

        messageBuilder.AppendLine("Type corresponding rate number to print shipment label.");

        AddBotMessage(new Message { Text = messageBuilder.ToString() });
    }

    private async Task HandleGetShippingRatesAsync(List<Entity> entities) { /* Implementation */ }
    private async Task HandleCheckShipmentStatusAsync(List<Entity> entities) { /* Implementation */ }
    private async Task HandlePrintLabelAsync(List<Entity> entities) { /* Implementation */ }
    private async Task HandleGetCheapestOptionAsync(List<Entity> entities) { /* Implementation */ }

    private async Task HandleProvideDetailsAsync(List<Entity> entities) { /* Implementation */ }

    private string ShowHelp()
    {
        return "Here are some things you can ask me:\n- Get Fastest shipping rates\n- Check shipment status\n- Print a label\n- Find the cheapest option\n- Find the fastest option";
    }

    public async Task Play(string message)
    {
        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();
        SpeechOptions options = new SpeechOptions()
        {
            Pitch = 1.2f,
            Volume = 0.55f,
            Locale = locales.FirstOrDefault()
        };

        await TextToSpeech.Default.SpeakAsync(message ?? "Hello! My name is Shipfer!", options);
    }

    static double ExtractNumber(string input)
    {
        string number = new string(input.Where(char.IsDigit).ToArray());
        double.TryParse(number, out double result);
        return result;
    }
}

public enum ConversationState
{
    None,
    GetFastestOptionShippingRates,
    GettingCheapestRates,
    GettingShippingRates,
    CheckingShipmentStatus,
    PrintingLabel,
    ProvidingDetails,
    GeneralHelp
}

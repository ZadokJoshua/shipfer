using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shipfer.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Shipfer.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isMessagesEmpty = false;

    [ObservableProperty]
    private string _userMessage = string.Empty;

    [ObservableProperty]
    private bool _playRecentBotResponse = true;
    private readonly ISpeechToText _speechToText;

    private CancellationTokenSource _cancellationTokenSource;

    public ObservableCollection<Message> Messages { get; set; } = [];

    public ChatViewModel(ISpeechToText speechToText)
    {
        Messages.Add(new Message { IsBot = true, Text = "Accusam ipsum magna duo invidunt nulla kasd amet amet voluptua duo takimata lorem odio ipsum stet. Sit et kasd justo ea invidunt invidunt vero. Autem dolore augue ea kasd diam elitr et clita sadipscing sadipscing et takimata lobortis. Et dignissim hendrerit sit erat velit ut nulla nulla consetetur dolores tempor erat no blandit. Quis sed tempor gubergren consequat et ut dolore erat et accusam et dolor tempor magna facilisi eum sit. Ipsum consectetuer ut volutpat te ad lorem nonumy velit kasd dolor diam mazim. Justo nihil quis stet augue diam takimata aliquam sadipscing nonumy stet ipsum consetetur ipsum et minim vero." });
        Messages.Add(new Message { IsBot = false, Text = "Sea duo dolore voluptua lorem dolor dignissim consectetuer. Eum vulputate tincidunt ut stet. Dolore laoreet no ipsum erat et. Vero consequat voluptua eos sed stet sed et vulputate vero eirmod et magna. Ut et blandit vel ipsum at odio doming ipsum kasd stet et." });
        Messages.Add(new Message { IsBot = true, Text = "Consetetur et feugait euismod nulla gubergren ea dolore sadipscing augue eos gubergren et sed. Nibh facilisi dolor stet iriure vero sed dolor diam sit. Facilisis et est adipiscing diam nobis nonumy tempor ea takimata diam justo accusam gubergren gubergren et stet. Sit clita gubergren vero. Facer ea kasd at enim accusam. Elitr et dolor assum labore amet. Nonummy velit et accusam et dolor sadipscing dolor sit magna gubergren. Sed sit dolor sit ut consetetur dolor et labore labore amet. Diam et diam ea sea et sit diam accusam sed est in magna dolor diam. Cum mazim stet dolor magna et aliquyam diam sit aliquip rebum quod dolore et sed nulla amet. Consequat justo consequat sed suscipit minim stet eirmod nibh. Elitr ipsum et clita consequat at iusto sed nisl esse rebum invidunt tempor et rebum takimata ut takimata." });
        Messages.Add(new Message { IsBot = false, Text = "I want to transport packages." });

        _speechToText = speechToText;
        Play(null);
    }

    #region Methods
    public async Task Play(string message)
    {
        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();
        SpeechOptions options = new SpeechOptions()
        {
            Pitch = 1.2f,
            Volume = 0.55f,
            Locale = locales.FirstOrDefault()
        };

        await TextToSpeech.Default.SpeakAsync(message ?? "Hello! My name is Shipfer!");
    }
    #endregion
}

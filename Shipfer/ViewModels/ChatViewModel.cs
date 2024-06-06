using CommunityToolkit.Mvvm.ComponentModel;
using Shipfer.Models;
using System.Collections.ObjectModel;

namespace Shipfer.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isMessagesEmpty = false;
    public ObservableCollection<Message> Messages { get; set; } = [];

    public ChatViewModel()
    {
        Messages.Add(new Message { IsBot = true, Text = "I want to transport packages." });
        Messages.Add(new Message { IsBot = false, Text = "I want to transport packages." });
        Messages.Add(new Message { IsBot = true, Text = "I want to transport packages." });
        Messages.Add(new Message { IsBot = false, Text = "I want to transport packages." });
    }


}

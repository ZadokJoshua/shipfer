using CommunityToolkit.Mvvm.Input;
using Shipfer.Views;

namespace Shipfer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [RelayCommand]
    async Task GoToCopilot()
    {
        await Shell.Current.GoToAsync(nameof(ChatPage));
    }
}

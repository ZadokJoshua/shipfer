using CommunityToolkit.Mvvm.Input;
using Shipfer.Views;

namespace Shipfer.ViewModels;

public partial class AuthViewModel : ViewModelBase
{
    [RelayCommand]
    public async Task GoToMainPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}

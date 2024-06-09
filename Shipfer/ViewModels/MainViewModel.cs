using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shipfer.Helpers;
using Shipfer.Views;

namespace Shipfer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _country = string.Empty;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty] 
    private string _postalCode = string.Empty;

    [ObservableProperty]
    private int _numOfdelivered;

    [ObservableProperty]
    private int _numOfInTransit;

    [ObservableProperty]
    private int _numOfPending;

    public MainViewModel()
    {
        GetUserDetails();
    }

    private void GetUserDetails()
    {
        var userDetails = PreferenceHelper.GetUserDetails();
        var userProfile = userDetails.userProfile;

        if (userProfile != null)
        {
            UserName = userProfile.Name;
            Country = CountryCodes.GetCountryByCode(userProfile.CountryCode);
            PostalCode = userProfile.Postal;
            Address = userProfile.Address;
        }
    }

    [RelayCommand]
    async Task GoToCopilot()
    {
        await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
    }
}

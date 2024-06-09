using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shipfer.Helpers;
using Shipfer.Models;
using Shipfer.Services;
using Supabase.Gotrue;
using System.Collections.ObjectModel;

namespace Shipfer.ViewModels;

public partial class AuthViewModel : ViewModelBase
{
    private readonly ISupabaseService _supabaseService;

    public AuthViewModel()
    {
        _supabaseService = new SupabaseService();
    }

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    private string _selectedCountryKey = string.Empty;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private bool _isLogin = true;

    public ObservableCollection<string> CountryNames => new(CountryCodes.Countries.Keys);

    private static bool IsPostalCodeValid(string postalCode)
    {
        if (postalCode.Length != 5)
        {
            return false;
        }

        if (int.TryParse(postalCode, out _))
        {
            return true;
        }

        return false;
    }

    [RelayCommand]
    void ChangeAuthView()
    {
        IsLogin = !IsLogin;
        Email = string.Empty;
        Password = string.Empty;
    }

    [RelayCommand]
    async Task SignUp()
    {
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Address))
        {
            await Application.Current.MainPage.DisplayAlert("Validation Error", "All fields are required", "Ok");
            return;
        }

        if (!IsPostalCodeValid(PostalCode))
        {
            await Application.Current.MainPage.DisplayAlert("Validation Error", "Invalid postal code", "Ok");
            return;
        }

        var response = await _supabaseService.SignUpAsync(Email, Password);

        if (response.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", response.Error, "Ok");
        }

        else
        {
            Supabase.Gotrue.Session? session = response.Session;
            var newProfile = new Profile
            {
                Id = session.User.Id,
                Name = UserName,
                Address = this.Address,
                CountryCode = CountryCodes.Countries[SelectedCountryKey],
                Postal = PostalCode
            };

            await _supabaseService.CreateProfile(newProfile);

            PreferenceHelper.AddUserDetails(newProfile, session);

            await GoToMainPageAsync();
        }
    }

    [RelayCommand]
    async Task Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Validation Error", "All fields are required", "Ok");
            return;
        }

        var response = await _supabaseService.LoginAsync(Email, Password);
        if (response.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", response.Error, "Ok");
        }
        else
        {
            var session = response.Session;
            var profile = await _supabaseService.GetProfile(session.User.Id);

            if (profile != null)
            {
                PreferenceHelper.AddUserDetails(profile, session);
                await GoToMainPageAsync();
            }
        }
    }

    [RelayCommand]
    public async Task GuestUserLogin()
    {
        Email = AppConfig.DEMO_USER_EMAIL;
        Password = AppConfig.DEMO_USER_PASSWORD;
        await Login();
    }

    [RelayCommand]
    public async Task GoToMainPageAsync()
    {
        Application.Current.MainPage = new AppShell();
    }

}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shipfer.Helpers;
using Shipfer.Models;
using Shipfer.Services;
using Shipfer.Views;
using System.Collections.ObjectModel;

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

    private readonly ISupabaseService _supabaseService;

    public ObservableCollection<Shipment> PendingShipments = new ObservableCollection<Shipment>();
    public ObservableCollection<Shipment> DeliveredShipments = new ObservableCollection<Shipment>();
    
    public MainViewModel()
    {
        GetUserDetails();
        _supabaseService = new SupabaseService();
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

    private async Task GetAllShipments()
    {
        var shipments = await _supabaseService.GetShipments();

        var deliveredShipments = shipments.Where(s => s.IsDelivered);
        var pendingShipments = shipments.Where(s => s.IsDelivered);

        if (shipments.Count() > 0)
        {
            
        }
    }

    [RelayCommand]
    async Task GoToCopilot()
    {
        await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
    }
}


using Shipfer.Models;
using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;

namespace Shipfer.Services;

public class SupabaseService : ISupabaseService
{
    private readonly Supabase.Client _client;
    public SupabaseService()
    {
        _client = new Supabase.Client(AppConfig.SUPABASE_URL, AppConfig.SUPABASE_KEY);
    }

    public async Task CreateProfile(Profile profile)
    {
        await _client.From<Profile>().Insert(profile);
    }

    public async Task<Profile> GetProfile(string profileId)
    {
        var reponse = await _client.From<Profile>().Where(p => p.Id == profileId).Get();
        return reponse.Model;
    }

    public async Task<IEnumerable<Shipment>> GetShipments()
    {
        var response = await _client.From<Shipment>().Get();
        return response.Models;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        try
        {
            var session = await _client.Auth.SignIn(email, password);
            if (session != null)
            {
                return new AuthResponse()
                {
                    Session = session
                    //AccessToken = session.AccessToken,
                    //UserId = session.User.Id
                };
            }
            else
            {
                return new AuthResponse()
                {
                    IsError = true,
                    Error = "Session is null"
                };
            }
        }
        catch (Exception ex)
        {
            return new AuthResponse()
            {
                IsError = true,
                Error = ex.Message
            };
        }
    }

    public async Task<AuthResponse> SignUpAsync(string email, string password)
    {
        try
        {
            var session = await _client.Auth.SignUp(email, password);
            if (session != null)
            {
                return new AuthResponse()
                {
                    Session = session
                };
            }
            else
            {
                return new AuthResponse()
                {
                    IsError = true,
                    Error = "Session is null"
                };
            }
        }
        catch (Exception ex)
        {
            return new AuthResponse()
            {
                IsError = true,
                Error = ex.Message
            };
        }
    }
}

using Shipfer.Models;
using Supabase.Interfaces;

namespace Shipfer.Services;

public interface ISupabaseService
{
    Task<AuthResponse> SignUpAsync(string email, string password);
    Task<AuthResponse> LoginAsync(string email, string password);
    Task CreateProfile(Profile profile);
    Task<Profile> GetProfile(string profileId);

}

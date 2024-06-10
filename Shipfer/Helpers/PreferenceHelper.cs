using Newtonsoft.Json;
using Shipfer.Models;
using Supabase.Gotrue;

namespace Shipfer.Helpers;

public static class PreferenceHelper
{
    private const string KEY_SESSION = "Session";
    private const string KEY_USER_PROFILE = "UserProfile";

    private const string KEY_ACCESS_TOKEN_RESPONSE = "AccessTokenResponse";
    private const string KEY_ACCESS_TOKEN_SAVED_TIME = "AccessTokenSavedTime";

    public static void AddUserDetails(Profile profile, Session session)
    {
        string userProfileJson = JsonConvert.SerializeObject(profile);
        string sessionJson = JsonConvert.SerializeObject(session);

        Preferences.Set(KEY_USER_PROFILE, userProfileJson);
        Preferences.Set(KEY_SESSION, sessionJson);
    }

    public static (Session session, Profile userProfile) GetUserDetails()
    {
        string userProfileJson = Preferences.Get(KEY_USER_PROFILE, string.Empty);
        string sessionJson = Preferences.Get(KEY_SESSION, string.Empty);

        Profile profileObject = JsonConvert.DeserializeObject<Profile>(userProfileJson);
        Session sessionObject = JsonConvert.DeserializeObject<Session>(sessionJson);

        return (sessionObject, profileObject);
    }

    public static void ClearUserDetails()
    {
        Preferences.Remove(KEY_USER_PROFILE);
        Preferences.Remove(KEY_SESSION);
    }

    public static void SaveAccessTokenResponse(GenerateAccessTokenResponse response)
    {
        string accessTokenResponseJson = JsonConvert.SerializeObject(response);
        Preferences.Set(KEY_ACCESS_TOKEN_RESPONSE, accessTokenResponseJson);
        Preferences.Set(KEY_ACCESS_TOKEN_SAVED_TIME, DateTime.UtcNow.ToString("o")); 
    }

    public static bool IsAccessTokenExpired()
    {
        string accessTokenResponseJson = Preferences.Get(KEY_ACCESS_TOKEN_RESPONSE, string.Empty);
        if (string.IsNullOrEmpty(accessTokenResponseJson))
        {
            return true;
        }

        var response = JsonConvert.DeserializeObject<GenerateAccessTokenResponse>(accessTokenResponseJson);
        string savedTimeString = Preferences.Get(KEY_ACCESS_TOKEN_SAVED_TIME, string.Empty);

        if (DateTime.TryParse(savedTimeString, out DateTime savedTime))
        {
            DateTime expiryTime = savedTime.AddSeconds(response.ExpiresIn);
            return DateTime.UtcNow >= expiryTime;
        }

        return true;
    }

    public static string GetAccessToken()
    {
        if (IsAccessTokenExpired())
        {
            return null; 
        }

        string accessTokenResponseJson = Preferences.Get(KEY_ACCESS_TOKEN_RESPONSE, string.Empty);
        var response = JsonConvert.DeserializeObject<GenerateAccessTokenResponse>(accessTokenResponseJson);
        return response.AccessToken;
    }
}

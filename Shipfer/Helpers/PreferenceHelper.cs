using Newtonsoft.Json;
using Shipfer.Models;
using Supabase.Gotrue;

namespace Shipfer.Helpers;

public static class PreferenceHelper
{
    private const string KeySession = "Session";
    private const string KeyUserProfile = "UserProfile";

    public static void AddUserDetails(Profile profile, Session session)
    {
        string userProfileJson = JsonConvert.SerializeObject(profile);
        string sessionJson = JsonConvert.SerializeObject(session);

        Preferences.Set(KeyUserProfile, userProfileJson);
        Preferences.Set(KeySession, sessionJson);
    }

    public static (Session session, Profile userProfile) GetUserDetails()
    {
        string userProfileJson = Preferences.Get(KeyUserProfile, string.Empty);
        string sessionJson = Preferences.Get(KeySession, string.Empty);

        Profile profileObject = JsonConvert.DeserializeObject<Profile>(userProfileJson);
        Session sessionObject = JsonConvert.DeserializeObject<Session>(sessionJson);

        return (sessionObject, profileObject);
    }

    public static void ClearUserDetails()
    {
        Preferences.Remove(KeyUserProfile);
        Preferences.Remove(KeySession);
    }
}

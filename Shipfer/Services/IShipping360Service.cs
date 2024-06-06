namespace Shipfer.Services;

public interface IShipping360Service
{
    Task<object> GenerateAuthToken();
}
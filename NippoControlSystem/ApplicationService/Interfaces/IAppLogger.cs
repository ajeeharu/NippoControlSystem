
namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface IAppLogger<TCategoryName>
    {
        void LogInfo(string message, params object?[] args);
        void LogWarning(string message, params object?[] args);
        void LogError(Exception? exception, string message, params object?[] args);
    }
}

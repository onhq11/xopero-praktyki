using Microsoft.Extensions.Configuration;

namespace NoteApp.Config.Builders;

public class DebugMode(IConfigurationRoot appSettings)
{
    private readonly string _debugMode = appSettings["DebugMode"] ?? "false";

    public bool IsDebugMode()
    {
        return _debugMode == "false";
    }
}
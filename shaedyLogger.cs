using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using System.Threading.Tasks;

namespace shaedyLogger;

public class shaedyLogger : BasePlugin
{
    public override string ModuleName => "shaedy Logger";
    public override string ModuleVersion => "4.1";
    public override string ModuleAuthor => "shaedy";

    private string _logFilePath = "";
    private static readonly object _fileLock = new object();

    public override void Load(bool hotReload)
    {
        _logFilePath = Path.Combine(ModuleDirectory, "local_chat.log");

        AddCommandListener("say", OnPlayerChatGlobal);
        AddCommandListener("say_team", OnPlayerChatTeam);
        AddCommand("css_svlog", "Logs a message to the local log file", OnCommandServerLog);

        Console.WriteLine($"[shaedyLogger] Logging to: {_logFilePath}");
    }

    [GameEventHandler]
    public HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        if (@event.Userid != null && !@event.Userid.IsBot)
            WriteLogAsync("server", "SERVER", $"[+] {@event.Userid.PlayerName} connected", 1);
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        if (@event.Userid != null && !@event.Userid.IsBot)
            WriteLogAsync("server", "SERVER", $"[-] {@event.Userid.PlayerName} disconnected", 1);
        return HookResult.Continue;
    }

    public void OnCommandServerLog(CCSPlayerController? player, CommandInfo info)
    {
        if (info.ArgCount < 2) return;
        string message = info.GetArg(1);
        if (info.ArgCount > 2) message = info.GetCommandString.Substring(info.GetCommandString.IndexOf(' ') + 1);

        WriteLogAsync("server", "SYSTEM", message, 1);
    }

    private HookResult OnPlayerChatGlobal(CCSPlayerController? player, CommandInfo info) => HandleChat(player, info.GetArg(1));
    private HookResult OnPlayerChatTeam(CCSPlayerController? player, CommandInfo info) => HandleChat(player, info.GetArg(1));

    private HookResult HandleChat(CCSPlayerController? player, string message)
    {
        if (player == null || !player.IsValid || player.IsBot || string.IsNullOrWhiteSpace(message))
            return HookResult.Continue;

        WriteLogAsync("chat", player.PlayerName, message.Trim('"'), player.TeamNum);
        return HookResult.Continue;
    }

    private void WriteLogAsync(string type, string name, string msg, int team)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logLine = $"[{timestamp}] [{type.ToUpper()}] {name}: {msg}";

        // File I/O runs in background to avoid blocking the game thread
        Task.Run(() =>
        {
            lock (_fileLock)
            {
                try
                {
                    File.AppendAllText(_logFilePath, logLine + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[shaedyLogger] Write Error: {ex.Message}");
                }
            }
        });
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public delegate void CommandHandler(string[] args);
public class ConsoleController : MonoBehaviour
{
    public AudioMixer am;

    public delegate void VisibilityChangedHandler(bool visible);
    public event VisibilityChangedHandler visibilityChanged;

    public delegate void VisionHandler(string param);
    public static event VisionHandler visionChanged;

    public delegate void LogChangedHadler(string[] args);
    public event LogChangedHadler logChanged;

    Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();
    List<string> commandHistory = new List<string>();
    public string[] log { get; private set; }
    const string repeatCmdName = "!!";
    const int scrollBackSize = 5;
    Queue<string> scrollBack = new Queue<string>(scrollBackSize);
    public ConsoleController()
    {
        registerCommand("/help", help, "Print this help.");
        //registerCommand("/change", change, "\n-h\tChange amount of health. Format: tankName(tankPurple, tankGreen) amount" +
        //   "\n-a\tChange amount of armor. Format: tankName(tankPurple, tankGreen) amount" +
        //    "");
        registerCommand("/restart", restart, "restart the game");
        registerCommand("/audio", audio, "Change volume (-80, 0)");
        registerCommand("/vision", showAIVision, "on/off");
        registerCommand("/menu", menu, "quit to the menu");
        registerCommand("/battlecity", battlecity, "");
    }
    class CommandRegistration
    {
        public string command { get; private set; }
        public CommandHandler handler { get; private set; }
        public string help { get; private set; }
        public CommandRegistration(string command, CommandHandler handler, string help)
        {
            this.command = command;
            this.handler = handler;
            this.help = help;
        }
    }
    void registerCommand(string command, CommandHandler handler, string help)
    {
        commands.Add(command, new CommandRegistration(command, handler, help));
    }

    public void runCommandString(string commandName)
    {
        if (commandName == "`") return;
        appendLogLine(">>> " + commandName);
        List<string> command = parseArgument(commandName);
        string[] args = new string[command.Count - 1];
        for (int i = 1; i < command.Count; i++)
        {
            args[i - 1] = command[i];
        }
        runCommand(command[0], args);
        commandHistory.Add(commandName);
    }

    static List<string> parseArgument(string commandString)
    {
        List<string> returnArg = new List<string>();
        bool inQuote = false;
        List<char> token = new List<char>();
        foreach (char c in commandString)
        {
            if (c == '"')
            {
                inQuote = !inQuote;
                continue;
            }
            if (!inQuote && c == ' ')
            {
                returnArg.Add(new string(token.ToArray()));
                token.Clear();
                continue;
            }
            token.Add(c);
        }
        returnArg.Add(new string(token.ToArray()));
        return returnArg;
    }

    public void runCommand(string command, string[] args)
    {
        CommandRegistration reg = null;
        if (!commands.TryGetValue(command, out reg))
        {
            appendLogLine(string.Format("Unknown command '{0}', type /help for list.", command));
        }
        else
        {
            if (reg.handler == null)
            {
                appendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
            }
            else reg.handler(args);
        }
    }

    public void appendLogLine(string line)
    {
        Debug.Log(line);
        if (scrollBack.Count >= scrollBackSize)
        {
            scrollBack.Dequeue();
        }
        scrollBack.Enqueue(line);
        log = scrollBack.ToArray();
        logChanged?.Invoke(log);
    }
    void help(string[] args)
    {
        List<string> text = new List<string>();
        foreach (KeyValuePair<string, CommandRegistration> element in commands)
        {
            if (element.Key == "/battlecity") continue;
            text.Add(string.Format("{0} - {1}", element.Key, element.Value.help));
        }
        appendLogLine(string.Join("\n", text));

    }

    void restart(string[] args)
    {
        ReloadStaticElements.Reload();
        Scenes.Load(SceneManager.GetActiveScene().name);
    }

    void menu(string[] args)
    {
        ReloadStaticElements.Reload();
        SceneManager.LoadScene("menu");
    }

    void change(string[] args)
    {

    }
     
    void audio(string[] args)
    {
        am.SetFloat("masterVolume", (float)Convert.ToDouble(args[0]));
    }

    void showAIVision(string[] args)
    {
        visionChanged?.Invoke(args[0]);
    }

    public static void Reload()
    {
        visionChanged = null;
    }

    void battlecity(string[] args)
    {
        ReloadStaticElements.Reload();
        SceneManager.LoadScene("battlecity");
        var player = Player.GetInstance();
        if (!player.CatchedEgg)
        {
            player.increaseScore(10000);
            player.CatchedEgg = true;
        }

    }
}


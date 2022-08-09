using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private static Dictionary<VRInputType, Command> commands;

    public static Dictionary<VRInputType, Command> Commands 
    {
        get 
        {
            if (commands == null) InitCommands();
            return commands;
        }

        set
        {
            commands = value;
        }
    }

    private static void InitCommands()
    {
        commands = new Dictionary<VRInputType, Command>();
    }

    public static void AddCommand(Command c)
    {
        Commands.Add(c.VRInput, c);
    }

    public static void ClearCommands()
    {
        Commands.Clear();
    }
}

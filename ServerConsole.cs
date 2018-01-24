using UnityEngine;
using System;
using System.Collections;

public class ServerConsole : MonoBehaviour
{
    private ConsoleTool.ConsoleWindow console = null;

    void Awake() 
	{
        if (Application.platform != RuntimePlatform.WindowsPlayer) return;
        //DontDestroyOnLoad( gameObject );
        console = new ConsoleTool.ConsoleWindow();
        console.Initialize();
        int codePage = Console.OutputEncoding.CodePage;
        console.CallSystem(string.Format("chcp {0}", codePage));
        console.CallSystem("cls");
        Application.logMessageReceived += new UnityEngine.Application.LogCallback(HandleLog);  
	}
 
	void HandleLog(string message, string stackTrace, LogType type)
	{
        if (type == LogType.Warning)
        {
            //System.Console.ForegroundColor = System.ConsoleColor.Yellow;
            //int index = message.IndexOf("\n");
            //string subString = message.Substring(0, index);
            //System.Console.WriteLine(subString);
        }
        else if (type == LogType.Error)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine(message);
        }
        else
        {
            // ÅÐ¶ÏÊÇ·ñÊÇlua±¨´í
            bool hasLuaException = message.IndexOf("Exception") > 0;
            if (hasLuaException)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine(message);
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.White;
                int subIndex = message.IndexOf("\n");
                string subString = message.Substring(0, subIndex);
                System.Console.WriteLine(subString);
            }
        }
	}

    void Update()
    {
        //if (Application.platform != RuntimePlatform.WindowsPlayer) return;
    }
	void OnDestroy()
	{
        CloseConsoleWindow();
    }
    void CloseConsoleWindow()
    {
        if (console != null)
        {
            console.Shutdown();
            console = null;
        }
    }
}
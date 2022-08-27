using System;
using UnityEngine;

namespace ServerSide
{
    public class ServerConsole : MonoBehaviour
    {

        public static ServerConsole instance;
        ConsoleInput input;

        //
        // Create console window, register callbacks
        //
        void Awake()
        {
            if (instance == null)
            {
                instance = this;

                if (!Application.isEditor)
                {
                    Console.Title = "Game Server";
                    Console.Clear();

                    input = new ConsoleInput();
                    input.OnInputText += OnInputText;
                }

                HandleLog("Console Started");
            }
            else if (instance != this)
            {
                //HandleLog("ServerConsole instance already exists, destroying object!");
                Destroy(gameObject);
            }
        }

        //
        // Text has been entered into the console
        // Run it as a console command
        //
        void OnInputText(string _msg)
        {
            Response<ResponseType, string> response = NetworkManager.Instance.HandleCommand(_msg);
            if(!String.IsNullOrEmpty(response.value))
            {
                Console.WriteLine(response.value);
            }
        }

        //
        // Debug.Log* callback
        //
        public void HandleLog(string message, LogType type = LogType.Log, bool isMsg = false)
        {
            if (Application.isEditor)
            {
                if (type == LogType.Error)
                    Debug.LogError(message);
                else if (type == LogType.Warning)
                    Debug.LogWarning(message);
                else if (!isMsg)
                    Debug.Log(message);
            }
            else
            {
                if (type == LogType.Warning)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (type == LogType.Error)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (isMsg)
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                // We're half way through typing something, so clear this line ..
                if (Console.CursorLeft != 0)
                    input.ClearLine();

                input.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.Gray;

                // If we were typing something re-add it.
                input.RedrawInputLine();
            }
        }

        //
        // Update the input every frame
        // This gets new key input and calls the OnInputText callback
        //
        void Update()
        {
            if (!Application.isEditor)
                input.Update();
        }
    }
}
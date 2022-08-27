using System;
using UnityEngine;

public class ConsoleInput
{
	//public delegate void InputText( string strInput );
	public event System.Action<string> OnInputText;
	public string inputString = "";

	public void ClearLine()
	{
		Console.CursorLeft = 0;
		Console.Write(new string(' ', Console.BufferWidth));
#if !UNITY_STANDALONE_LINUX
		if (Console.CursorTop > 0)
			Console.CursorTop--;
#endif
		Console.CursorLeft = 0;
	}

	public void RedrawInputLine()
	{
		if (inputString.Length == 0) return;

		if (Console.CursorLeft > 0)
			ClearLine();

		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(inputString);
		Console.ForegroundColor = ConsoleColor.White;
	}

	public void WriteLine(string message)
	{
		TimeSpan timeSpan = DateTime.Now.TimeOfDay;

		Console.WriteLine("[" + timeSpan.ToString(@"hh\:mm\:ss") + "] " + message);
	}

	internal void OnBackspace()
	{
		if (inputString.Length < 1) return;

		if (inputString.Length == 1) ClearLine();

		inputString = inputString.Substring(0, inputString.Length - 1);

		RedrawInputLine();
	}

	internal void OnEscape()
	{
		ClearLine();
		inputString = "";
	}

	internal void OnEnter()
	{
		if (inputString.Length == 0) return;

		ClearLine();
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("> " + inputString);
		Console.ForegroundColor = ConsoleColor.Gray;

		var strtext = inputString;
		inputString = "";

		if (OnInputText != null)
		{
			OnInputText(strtext);
		}
	}

	public void Update()
	{
		if (!Console.KeyAvailable) return;
		var key = Console.ReadKey();

		if (key.Key == ConsoleKey.Enter)
		{
			OnEnter();
			return;
		}

		if (key.Key == ConsoleKey.Backspace)
		{
			OnBackspace();
			return;
		}

		if (key.Key == ConsoleKey.Escape)
		{
			OnEscape();
			return;
		}

		if (key.KeyChar != '\u0000')
		{
			inputString += key.KeyChar;
			RedrawInputLine();
			return;
		}
	}
}
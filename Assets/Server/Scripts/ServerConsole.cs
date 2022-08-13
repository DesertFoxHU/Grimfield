using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ServerConsole : MonoBehaviour
{
    private Process process;

    public void StartConsole()
    {
        Thread thread = new Thread(Redirect);
        thread.Start();
    }

    public void Redirect()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = @"C:\Users\xy\Grimfield\ServerConsole\ServerConsole\ServerConsole\bin\Debug\net6.0\ServerConsole.exe";
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardInput = true;
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        startInfo.CreateNoWindow = false;
        startInfo.RedirectStandardError = true;

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();

            process.ErrorDataReceived += (sender, args) =>
            {
                Console.WriteLine(args.Data);
            };

            process.OutputDataReceived += (sender, line) =>
            {
                if (line.Data == null)
                {
                    process.StandardOutput.ReadLine();
                    return;
                }

                if (!line.Data.StartsWith("/"))
                {
                    process.StandardInput.WriteLine("Format error! Every command should start with '/' symbol");
                    process.StandardOutput.ReadLine();
                    return;
                }

                string command = line.Data.Split('/')[1];
                bool actionHappened = false;

                if (command.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    actionHappened = true;
                    process.StandardInput.WriteLine("Commands:");
                }

                if (!actionHappened)
                {
                    process.StandardInput.WriteLine("Unknown command: /" + command);
                }

                process.StandardOutput.ReadLine();
            };

            process.BeginOutputReadLine();
            //process.WaitForExit();
        }
    }
}

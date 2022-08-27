using System;

namespace ServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To help type: /help");
            string txt = "";
            do
            {
                txt = Console.ReadLine();
                Console.WriteLine(txt);
            }
            while (txt != "exit");
            Console.ReadKey();
            //ReadInput();
        }

        /*public static void ReadInput()
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                ReadInput();
                return;
            }

            if (!input.StartsWith("/"))
            {
                Console.WriteLine("Format error! Every command should start with '/' symbol");
                ReadInput();
            }

            string command = input.Split('/')[1];
            bool actionHappened = false;

            if(command.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                actionHappened = true;
                Console.WriteLine("Commands:");
            }

            if (!actionHappened)
            {
                Console.WriteLine("Unknown command: /" + command);
            }
            
            ReadInput();
        }*/
    }
}
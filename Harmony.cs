using System.Diagnostics;

namespace Injector
{
    internal class Harmony
    {
        private static void PrintInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static string GetAssemblyFolder(string process)
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process proc in processlist)
            {
                if (proc.ProcessName == process)
                {
                    try
                    {
                        string filePath = proc.MainModule.FileName;
                        string processName = proc.ProcessName;
                        string[] files = Directory.GetFiles(filePath.Replace(processName + ".exe", ""), "Assembly-CSharp.dll", SearchOption.AllDirectories);
                        if (files.Length == 0)
                            return string.Empty;
                        return files[0].Replace("Assembly-CSharp.dll", "");
                    }
                    catch
                    {
                        /* You will get access denied exception for system processes, We are skiping the system processes here */
                    }
                }
            }
            return string.Empty;
        }

        public static void AddHarmony(string gameName, int version, out bool flag)
        {
            try
            {
                if (version == 0)
                {
                    PrintInColor("No need to implement harmony", ConsoleColor.DarkYellow);
                    flag = true;
                    return;
                }
                string path = GetAssemblyFolder(gameName);
                if (path != string.Empty)
                {
                    if (File.Exists(path + "0Harmony.dll"))
                    {
                        PrintInColor("Harmony already exists", ConsoleColor.DarkGreen);
                        flag = true;
                        return;
                    }
                    File.Copy(@$"Harmony\net{version}\0Harmony.dll", path + "0Harmony.dll");
                    PrintInColor("Harmony successfully implemented!", ConsoleColor.DarkGreen);
                    flag = true;
                }
                else
                {
                    PrintInColor("Cannot find the game", ConsoleColor.DarkRed);
                    flag = false;
                }
            }
            catch
            {
                PrintInColor("Cannot implement Harmony", ConsoleColor.DarkRed);
                flag = false;
            }
        }
    }
}

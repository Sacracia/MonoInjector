using Injector;
using System.Text.Json;

namespace SharpMonoInjector
{
    internal class Program
    {
        static void Main()
        {
            byte[] assembly;
            try
            {
                Config? config = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"));
                assembly = File.ReadAllBytes(config.PathToDll);
                Harmony.AddHarmony(config.Exe, config.Versione, out bool flag);
                if (!flag)
                    throw new Exception("Cannot implement harmony!");
                Injector injector = new Injector(config.Exe);
                injector.Inject(assembly, config.Nspace, config.Klass, config.Method);
                Console.WriteLine("Successfully injected! Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //File.WriteAllText("error.txt", ex.Message);
                Console.WriteLine("Injection failed! Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
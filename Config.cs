using System.Text.Json.Serialization;

namespace SharpMonoInjector
{
    internal class Config
    {
        public string Exe { get; set; }
        public string PathToDll { get; set; }
        public string Nspace { get; set; }
        public string Klass { get; set; }
        public string Method { get; set; }
        public int Versione { get; set; }

        [JsonConstructor]
        public Config(string exe, string pathToDll, string nspace, string klass, string method, int versione)
        {
            Exe = exe;
            PathToDll = pathToDll;
            Nspace = nspace;
            Klass = klass;
            Method = method;
            Versione = versione;
        }
    }
}

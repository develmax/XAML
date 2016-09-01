namespace Xacml.Utils
{
    using System.IO;
    using System.Text;

    using Xacml.Exceptions;

    public class Logger
    {
        private static object log_obj = new object();
        private readonly string _name;
        private readonly StringBuilder sb = new StringBuilder();

        private Logger(string name)
        {
            this._name = name;
        }

        public static Logger GetLogger(string name)
        {
            return new Logger(name);
        }

        public void Log(object o, Indeterminate indeterminate)
        {
            this.sb.AppendLine("Name: " + this._name);
            this.sb.AppendLine("Object: " + (o != null ? o.ToString() : string.Empty));
            this.sb.AppendLine("Indeterminate: " + indeterminate.Message);
            lock (log_obj)
            {
                using (var writer = new StreamWriter("log.txt", true))
                {
                    writer.WriteLine(this.sb.ToString());
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
}
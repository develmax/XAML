namespace Xacml.Types.Properties
{
    using System.Collections.Generic;
    using System.IO;

    public class Properties
    {
        private readonly Dictionary<string, string> props
            = new Dictionary<string, string>();

        public bool Empty
        {
            get { return this.props.Count == 0; }
        }

        public string GetProperty(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                if (this.props.ContainsKey(property)) return this.props[property];
            }

            return null;
        }

        public void Load(FileStream fileInputStream)
        {
            this.props.Clear();

            var reader = new StreamReader(fileInputStream);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    line = line.Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line[0] != '#')
                        {
                            int index = line.IndexOf('=');
                            string name = index >= 0
                                              ? line.Substring(0, index)
                                              : line;
                            string value = index >= 0
                                               ? line.Substring(index + 1)
                                               : string.Empty;

                            name = name.Trim();
                            value = value.Trim();

                            if (!string.IsNullOrEmpty(name))
                            {
                                if (!this.props.ContainsKey(name)) this.props.Add(name, value);
                                else this.props[name] = value;
                            }
                        }
                    }
                }
            }
        }
    }
}
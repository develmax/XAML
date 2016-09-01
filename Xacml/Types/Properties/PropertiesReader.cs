namespace Xacml.Types.Properties
{
    using System.IO;

    using Xacml.Exceptions;

    public class PropertiesReader
    {
        private readonly string _fileName;
        private readonly Properties _properties = new Properties();

        public PropertiesReader(string fileName)
        {
            this._fileName = fileName;
        }

        private void Load()
        {
            if (this._properties.Empty)
            {
                try
                {
                    this._properties.Load(new FileStream(this._fileName, FileMode.Open));
                }
                catch (IOException)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
                }
            }
        }

        public virtual string GetProperty(string property)
        {
            try
            {
                this.Load();
            }
            catch (Indeterminate)
            {
                return string.Empty;
            }

            string prop = this._properties.GetProperty(property);

            if (prop == null)
            {
                return string.Empty;
            }

            return prop;
        }
    }
}
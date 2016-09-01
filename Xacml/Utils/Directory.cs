namespace Xacml.Utils
{
    using System.IO;
    using System.Linq;

    public class Directory
    {
        private readonly string _policydir;

        public Directory(string policydir)
        {
            this._policydir = policydir;
        }

        public string AbsolutePath
        {
            get { return Path.GetFullPath(this._policydir); }
        }

        public string[] GetList()
        {
            return System.IO.Directory.GetFiles(this._policydir)
                .ToList().Select(i => Path.GetFileName(i)).ToArray();
        }
    }
}
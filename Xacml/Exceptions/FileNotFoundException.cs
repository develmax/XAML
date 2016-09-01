namespace Xacml.Exceptions
{
    public class FileNotFoundException : Indeterminate
    {
        public FileNotFoundException(string msg)
            : base(msg)
        {
        }
    }
}
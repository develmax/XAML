namespace Xacml.Test
{
    using System;

    using Xacml.Elements.Context;
    using Xacml.Types.Properties;
    using Xacml.Types.Streams;
    using Xacml.Utils;

    class Program
    {
        static void Main(string[] args)
        {
            var xacml = new Xacml3();

            var reader = new PropertiesReader("config.properties");

            string reqfile = reader.GetProperty("Request");

            xacml.InitializePolicy(
                reader.GetProperty("PolicyDir"),
                reader.GetProperty("PolicyEntryID"));

            Response resp = xacml.Evaluate(reqfile);
            
            resp.Encode(new OutputStream(Console.Out), new Indentation());

            Console.ReadLine();
        }
    }
}
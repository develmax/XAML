namespace Xacml.Elements.DataType
{
    using System.Collections;

    public interface IDataType : IElement
    {
        IList Children { get; }
        bool ReturnIsBag();
    }
}
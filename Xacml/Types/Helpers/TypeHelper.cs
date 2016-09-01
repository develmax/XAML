namespace Xacml.Types.Helpers
{
    using System;

    using Xacml.Elements.DataType;

    public static class TypeHelper
    {
        public static bool IsInstance(this Type type, DataTypeValue dataTypeValue)
        {
            return (dataTypeValue != null) &&
                   (dataTypeValue.GetType().FullName == type.FullName);
        }

        public static bool IsInstance(this Type type, object dataTypeValue)
        {
            return (dataTypeValue != null) &&
                   (dataTypeValue.GetType().FullName == type.FullName);
        }
    }
}
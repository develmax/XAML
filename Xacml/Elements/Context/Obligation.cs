namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Obligation : IElement
    {
        public const string Identifer = "Obligation";

        private readonly List<AttributeAssignment> _attributeAssignments;
        private readonly string _obligationId;

        public Obligation(string obligationId)
        {
            this._obligationId = obligationId;
            this._attributeAssignments = new List<AttributeAssignment>();
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Obligation ObligationId=\"" + this._obligationId + "\">");
            indenter.Down();
            foreach (AttributeAssignment a in this._attributeAssignments)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Obligation>");
        }

        #endregion

        public virtual void AddAttributeAssignment(AttributeAssignment a)
        {
            this._attributeAssignments.Add(a);
        }
    }
}
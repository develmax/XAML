namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Advice : IElement
    {
        public const string Identifer = "Advice";

        private readonly string AdviceId;
        private readonly List<AttributeAssignment> _attributeAssignments;

        public Advice(string adviceId)
        {
            this.AdviceId = adviceId;
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
            psout.PrintLine(indenter + "<Advice AdviceId=\"" + this.AdviceId + "\">");
            indenter.Down();
            foreach (AttributeAssignment a in this._attributeAssignments)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Advice>");
        }

        #endregion

        public virtual void addAttributeAssignment(AttributeAssignment a)
        {
            this._attributeAssignments.Add(a);
        }
    }
}
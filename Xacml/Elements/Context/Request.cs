namespace Xacml.Elements.Context
{
    using System.Collections;
    using System.Collections.Generic;

    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class Request : IElement
    {
        public const string Identifer = "Request";
        private readonly List<Attributes> _attributes;
        private readonly BooleanDataType _combinedDecision;
        private readonly List<MultiRequests> _multiRequests;
        private readonly IDictionary _otherattributes;
        private readonly List<RequestDefaults> _requestDefaults;
        private readonly BooleanDataType _returnPolicyIdList;

        public Request(Request request)
        {
            this._returnPolicyIdList = request._returnPolicyIdList;
            this._combinedDecision = request._combinedDecision;
            this._otherattributes = request._otherattributes;
            this._attributes = new List<Attributes>(request._attributes);
            this._requestDefaults = new List<RequestDefaults>(request._requestDefaults);
            this._multiRequests = new List<MultiRequests>(request._multiRequests);
        }

        public Request(Node node)
        {
            if (node.NodeName.Equals(Identifer) == false)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            this._otherattributes = new Hashtable();
            NamedNodeMap attrs = node.Attributes;
            this._combinedDecision = BooleanDataType.False;
            for (int i = 0; i < attrs.Length; i++)
            {
                Node child = attrs.Item(i);
                if (child.NodeName.Equals("ReturnPolicyIdList"))
                {
                    string value = node.Attributes.GetNamedItem("ReturnPolicyIdList").NodeValue.Trim();
                    if (value.EqualsIgnoreCase("true"))
                    {
                        this._returnPolicyIdList = BooleanDataType.True;
                    }
                    else
                    {
                        this._returnPolicyIdList = BooleanDataType.False;
                    }
                }
                else if (child.NodeName.Equals("CombinedDecision"))
                {
                    string value = node.Attributes.GetNamedItem("CombinedDecision").NodeValue.Trim();
                    if (value.EqualsIgnoreCase("true"))
                    {
                        this._combinedDecision = BooleanDataType.True;
                    }
                    else
                    {
                        this._combinedDecision = BooleanDataType.False;
                    }
                }
                else
                {
                    this._otherattributes.Add(child.NodeName, child.NodeValue);
                }
            }

            this._attributes = new List<Attributes>();
            this._requestDefaults = new List<RequestDefaults>();
            this._multiRequests = new List<MultiRequests>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(Attributes.Identifer))
                {
                    this._attributes.Add(Attributes.GetInstance(child));
                }
                else if (child.NodeName.Equals(RequestDefaults.Identifer))
                {
                    this._requestDefaults.Add(new RequestDefaults(child));
                }
                else if (child.NodeName.Equals(MultiRequests.Identifer))
                {
                    this._multiRequests.Add(new MultiRequests(child));
                }
            }
        }

        public virtual Content Content
        {
            get
            {
                foreach (object attr in this._attributes)
                {
                    var attrs = (Attributes)attr;
                    Content content = attrs.Content;
                    if (content != null)
                    {
                        return content;
                    }
                }
                return null;
            }
        }

        public virtual IDictionary XMLHeaderAttributes
        {
            get { return this._otherattributes; }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var @out = new PrintStream(output);

            @out.PrintLine(indenter + "<Request");
            indenter.Down();
            IEnumerator itr = this._otherattributes.Keys.GetEnumerator();
            while (itr.MoveNext())
            {
                var name = (string)itr.Current;
                @out.PrintLine(indenter + name + "=\"" + this._otherattributes[name] + "\"");
            }
            @out.PrintLine(indenter + "ReturnPolicyIdList=" + "\"" + this._returnPolicyIdList.Encode() + "\"");
            @out.PrintLine(indenter + "CombinedDecision=" + "\"" + this._combinedDecision.Encode() + "\">");
            foreach (RequestDefaults r in this._requestDefaults)
            {
                r.Encode(output, indenter);
            }

            foreach (MultiRequests m in this._multiRequests)
            {
                m.Encode(output, indenter);
            }

            foreach (Attributes a in this._attributes)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();

            @out.PrintLine(indenter + "</Request>");
        }

        #endregion

        public static Request GetInstance(Node node)
        {
            return new Request(node);
        }

        public static Request GetInstance(InputStream input)
        {
            return GetInstance(InputStreamParser.Parse(input));
        }

        public virtual BagDataType Evaluate(
            EvaluationContext ctx,
            string Category,
            string AttributeId,
            string DataType,
            string MustBePresent,
            string Issuer)
        {
            var bag = new BagDataType();

            foreach (Attributes attrs in this._attributes)
            {
                BagDataType ret = attrs.Evaluate(Category, AttributeId, DataType);
                if (!ret.Empty)
                {
                    foreach (object o in ret.Children) bag.AddDataType((DataTypeValue)o);
                }
            }
            if (bag.Empty)
            {
                if ("urn:oasis:names:tc:xacml:1.0:environment:current-date".Equals(AttributeId))
                {
                    bag.AddDataType(ctx.CurrentDate);
                }
                else if ("urn:oasis:names:tc:xacml:1.0:environment:current-datetime".Equals(AttributeId))
                {
                    bag.AddDataType(ctx.CurrentDateTime);
                }
                else if ("urn:oasis:names:tc:xacml:1.0:environment:current-time".Equals(AttributeId))
                {
                    bag.AddDataType(ctx.CurrentTime);
                }
            }
            return bag;
        }

        public virtual void replaceAttributes(Attributes attrs)
        {
            int size = this._attributes.Count;
            for (int i = 0; i < size; i++)
            {
                if (attrs.Category.Equals(this._attributes[i].Category))
                {
                    this._attributes.RemoveAt(i);
                    i--;
                    size--;
                }
            }
            this._attributes.Add(attrs);
        }

        public static Request GetInstance(FileInputStream input)
        {
            return GetInstance(new InputStream(input.Stream));
        }
    }
}
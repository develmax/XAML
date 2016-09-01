namespace Xacml.Elements.Policy
{
    using System.Collections.Generic;

    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicySchema
    {
        public const string stringIdentifer = "PolicySchema";

        private static readonly Dictionary<string, IPolicyLanguageModel> _policyLanguageModels =
            new Dictionary<string, IPolicyLanguageModel>();

        private readonly string _rootPolicyId;

        public PolicySchema(string ID)
        {
            this._rootPolicyId = ID;
        }

        public PolicySchema(Node node, string ID)
        {
            this._rootPolicyId = ID;
            var p = (IPolicyLanguageModel)PolicyElementFactory.GetInstance(node);
            AddPolicyLanguageModel(p.ElementId, p);
        }

        public virtual string RootElementID
        {
            get { return this._rootPolicyId; }
        }

        public static void AddPolicyLanguageModel(string id, IPolicyLanguageModel e)
        {
            if (_policyLanguageModels.ContainsKey(id) == false)
            {
                _policyLanguageModels.Add(id, e);
            }
        }

        public static IPolicyLanguageModel GetPolicyLanguageModel(string id)
        {
            if (_policyLanguageModels.ContainsKey(id))
            {
                return _policyLanguageModels[id];
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        public static PolicySchema GetInstance(Node node, string ID)
        {
            return new PolicySchema(node, ID);
        }

        public static void AddPolicyLanguageModelFromURL(string url)
        {
            try
            {
                Node node = NodeFactory.GetInstanceFromURL(url);
                var p = (IPolicyLanguageModel)PolicyElementFactory.GetInstance(node);
                AddPolicyLanguageModel(p.ElementId, p);
            }
            catch (Indeterminate ex)
            {
                Logger.GetLogger(typeof(PolicySchema).Name).Log(null, ex);
            }
        }

        public static void AddPolicyLanguageModelFromDir(string policydir)
        {
            var file = new Directory(policydir);
            string[] fns = file.GetList();
            foreach (string name in fns)
            {
                if (name.EndsWith(".xml"))
                {
                    try
                    {
                        Node node = NodeFactory.GetInstanceFromFile(file.AbsolutePath + "/" + name);
                        var p = (IPolicyLanguageModel)PolicyElementFactory.GetInstance(node);
                        AddPolicyLanguageModel(p.ElementId, p);
                    }
                    catch (Indeterminate ex)
                    {
                        Logger.GetLogger(typeof(PolicySchema).Name).Log(null, ex);
                    }
                }
            }
        }

        public static PolicySchema GetInstance(string ID)
        {
            return new PolicySchema(ID);
        }

        public virtual void evluate(EvaluationContext ctx)
        {
            try
            {
                GetPolicyLanguageModel(this._rootPolicyId).Evaluate(ctx, this._rootPolicyId);
            }
            catch (Indeterminate ex)
            {
                Logger.GetLogger(typeof(PolicySchema).Name).Log(null, ex);
            }
        }

        public virtual void Encode(OutputStream output, Indentation indenter)
        {
            try
            {
                GetPolicyLanguageModel(this._rootPolicyId).Encode(output, indenter);
            }
            catch (Indeterminate ex)
            {
                Logger.GetLogger(typeof(PolicySchema).Name).Log(null, ex);
            }
        }
    }
}
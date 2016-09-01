namespace Xacml.Elements.Policy
{
    public class Effect
    {
        internal const string EffectPermit = "Permit";
        internal const string EffectDeny = "Deny";
        private string _effect;

        //Default effect
        public Effect()
        {
            this._effect = EffectDeny;
        }

        public Effect(string effect)
        {
            this.Effect_ = effect;
        }

        public string Effect_
        {
            set
            {
                if (value.Equals(EffectDeny))
                {
                    this._effect = EffectDeny;
                }
                else if (value.Equals(EffectPermit))
                {
                    this._effect = EffectPermit;
                }
                else
                {
                }
            }
        }

        public virtual bool isPermit
        {
            get { return EffectPermit.Equals(this._effect); }
        }

        public virtual bool isDeny
        {
            get { return EffectDeny.Equals(this._effect); }
        }

        public override string ToString()
        {
            return this._effect;
        }
    }
}
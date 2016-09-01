namespace Xacml.Elements.DataType
{
    using System;
    using System.Text;

    using Xacml.Exceptions;

    public class PortRange
    {
        public static int PortMin = 1;
        public static int PortMax = 65535;
        private readonly bool _isRange;

        private readonly int _portNumber;
        private readonly int _portNumberEnd;

        public PortRange(string str)
        {
            this._isRange = false;
            if (str.Contains("-"))
            {
                int index = str.IndexOf("-");
                string port1 = str.Substring(0, index);
                string port2 = str.Substring(index + 1);
                this._isRange = true;
                if (port1.Trim().Length > 0)
                {
                    try
                    {
                        this._portNumber = Convert.ToInt32(port1);
                    }
                    catch (FormatException)
                    {
                        throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                    }
                }
                else
                {
                    this._portNumber = PortMin;
                }

                if (port2.Trim().Length > 0)
                {
                    try
                    {
                        this._portNumberEnd = Convert.ToInt32(port2);
                    }
                    catch (FormatException)
                    {
                        throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                    }
                }
                else
                {
                    this._portNumberEnd = PortMax;
                }

                if ((this._portNumber < PortMin || this._portNumber > PortMax) ||
                    (this._portNumberEnd < PortMin || this._portNumberEnd > PortMax) ||
                    (this._portNumber >= this._portNumberEnd))
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
            }
            else
            {
                try
                {
                    this._portNumber = Convert.ToInt32(str);
                }
                catch (FormatException)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }

                if ((this._portNumber < PortMin || this._portNumber > PortMax))
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
                this._portNumberEnd = -1;
            }
        }

        public virtual int PortStart
        {
            get { return this._portNumber; }
        }

        public virtual int PortEnd
        {
            get { return this._portNumberEnd; }
        }

        public virtual string Encode()
        {
            if (this._isRange)
            {
                var str = new StringBuilder();
                if (this._portNumber != PortMin)
                {
                    str.Append(Convert.ToString(this._portNumber));
                }
                str.Append("-");
                if (this._portNumberEnd != PortMax)
                {
                    str.Append(Convert.ToString(this._portNumberEnd));
                }
                return str.ToString();
            }
            else
            {
                return Convert.ToString(this._portNumber);
            }
        }

        public override int GetHashCode()
        {
            int hash = 5;
            hash = 61 * hash + this._portNumber;
            hash = 61 * hash + this._portNumberEnd;
            return hash;
        }

        public override bool Equals(object o)
        {
            if (o == null || (o is PortRange) == false)
            {
                return false;
            }
            if (o == this)
            {
                return true;
            }
            if (this._portNumber == ((PortRange)o).PortStart && this._portNumberEnd == ((PortRange)o).PortEnd)
            {
                return true;
            }
            return false;
        }
    }
}
namespace Xacml.Utils
{
    using System.Text;

    public class Indentation
    {
        private const char defaultchar = ' ';
        private const int defaultsteps = 2;

        private readonly int _steps;
        private readonly char _tab;
        private int _size;

        public Indentation()
            : this(defaultsteps, defaultchar)
        {
        }

        public Indentation(int steps)
            : this(steps, defaultchar)
        {
        }

        public Indentation(int steps, char tab)
        {
            this._tab = tab;
            this._steps = steps;
            this._size = 0;
        }

        public virtual void Down()
        {
            this._size += this._steps;
        }

        public virtual void Up()
        {
            this._size -= this._steps;
        }

        public override string ToString()
        {
            if (this._size <= 0) return string.Empty;

            var s = new StringBuilder();

            for (int i = 0; i < this._size; i++) s.Append(this._tab);

            return s.ToString();
        }
    }
}
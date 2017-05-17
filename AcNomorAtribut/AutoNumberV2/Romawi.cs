using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcBlockAtributeIncrement
{
    public class Romawi : IComparable<Romawi>, IEquatable<Romawi>
    {
        private static Romawi.Pair[] pairs;

        public string Text
        {
            get;
            private set;
        }

        public int Value
        {
            get;
            set;
        }
        
        static Romawi()
        {
            Romawi.pairs = new Romawi.Pair[] { new Romawi.Pair(1000, "M"),
            new Romawi.Pair(900, "CM"), new Romawi.Pair(500, "D"),
            new Romawi.Pair(400, "CD"), new Romawi.Pair(100, "C"),
            new Romawi.Pair(90, "XC"), new Romawi.Pair(50, "L"),
            new Romawi.Pair(40, "XL"), new Romawi.Pair(10, "X"),
            new Romawi.Pair(9, "IX"), new Romawi.Pair(5, "V"),
            new Romawi.Pair(4, "IV"), new Romawi.Pair(1, "I")};
        }

        public Romawi(string value)
        {
            if (value == null || value.Trim() == "")
            {
                throw new ArgumentException("Null or Empty string.");
            }
            this.Text = value;
            Romawi.Pair[] pairArray = Romawi.pairs;
            for (int i = 0; i < (int)pairArray.Length; i++)
            {
                Romawi.Pair pair = pairArray[i];
                while (value.StartsWith(pair.StringValue))
                {
                    this.Value = this.Value + pair.Value;
                    value = value.Substring(pair.StringValue.Length);
                }
            }
            if (value != "")
            {
                throw new ArgumentException("Invalid Roman Number");
            }
        }

        public Romawi(int num)
        {
            if (num < 1)
            {
                throw new ArgumentOutOfRangeException("Zero or Negative integer.");
            }
            this.Value = num;
            StringBuilder stringbuilder = new StringBuilder("");
            Romawi.Pair[] pairArray = Romawi.pairs;
            for (int i = 0; i < (int)pairArray.Length; i++)
            {
                Romawi.Pair pair = pairArray[i];
                while (num >= pair.Value)
                {
                    stringbuilder.Append(pair.StringValue);
                    num = num - pair.Value;
                }
            }
            this.Text = stringbuilder.ToString();
        }

        public int CompareTo(Romawi other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Romawi))
            {
                return false;
            }
            return ((Romawi)obj).Value == this.Value;
        }

        public bool Equals(Romawi romawi)
        {
            return this.Value.Equals(romawi.Value);
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public static Romawi operator +(Romawi r1, Romawi r2)
        {
            return new Romawi(r1.Value + r2.Value);
        }

        public static Romawi operator +(Romawi r, int i)
        {
            return new Romawi(r.Value + i);
        }

        public static Romawi operator +(int i,  Romawi r)
        {
            return new Romawi(r.Value + i);
        }

        public static explicit operator Romawi(int i)
        {
            return new Romawi(i);
        }

        public static explicit operator Romawi(string s)
        {
            return new Romawi(s);
        }

        public override string ToString()
        {
            return this.Text;
        }

        public static bool TryParse(string value, out Romawi romawi)
        {
            bool flag;
            int num;
            try
            {
                romawi = new Romawi(value);
                flag = true;
            }
            catch
            {
                if (!int.TryParse(value, out num))
                {
                    romawi = null;
                    flag = false;
                }
                else
                {
                    try
                    {
                        romawi = new Romawi(num);
                        flag = true;
                    }
                    catch
                    {
                        romawi = null;
                        flag = false;
                    }
                }
            }
            return flag;
        }

        private class Pair
        {
            public string StringValue
            {
                get;
                private set;
            }

            public int Value
            {
                get;
                set;
            }

            public Pair(int value, string stringvalue)
            {
                this.Value = value;
                this.StringValue = stringvalue;
            }
        }
    }
}

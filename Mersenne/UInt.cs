using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mersenne {

    /// <summary>A wrapper for UInt32 that displays the value in binary</summary>
    public struct UInt {
        public const int WIDTH = 32;
        public static readonly Regex BinaryRegex = new Regex("([01]{4})*$");

        private readonly uint _value;

        public UInt(uint value) => this._value = value;

        public static UInt operator +(UInt a, uint b) => new UInt(a._value + b);

        public static UInt operator -(UInt a, uint b) => new UInt(a._value - b);

        public static UInt operator *(UInt a, uint b) => new UInt(a._value * b);

        public static UInt operator /(UInt a, uint b) => new UInt(a._value / b);

        public static UInt operator ^(UInt a, uint b) => new UInt(a._value ^ b);

        public static UInt operator >>(UInt a, int b) => new UInt(a._value >> b);

        public static UInt operator <<(UInt a, int b) => new UInt(a._value << b);

        public static implicit operator UInt(uint v) => new UInt(v);

        public static implicit operator uint(UInt v) => v._value;

        public static bool operator ==(UInt a, object b) => a.Equals(b);

        public static bool operator !=(UInt a, object b) => !(a == b);

        public override bool Equals(object obj) => obj is UInt b ? this._value.Equals(b._value) : this._value.Equals(obj);

        public override int GetHashCode() => this._value.GetHashCode();

        public override string ToString() {
            string s = $"{Convert.ToString(this._value, 2).PadLeft(UInt.WIDTH, '0')}";

            // Add commas
            s = $"{string.Join(",", UInt.BinaryRegex.Match(s).Groups[1].Captures.Cast<Capture>().Select(g => g.Value))}";

            return $"{s} ({this._value})";
        }
    }

    /// <summary>A wrapper for UInt64 that displays the value in binary</summary>
    public struct ULong {
        public const int WIDTH = 64;

        private readonly ulong _value;

        public ULong(ulong value) => this._value = value;

        public static ULong operator +(ULong a, ulong b) => new ULong(a._value + b);

        public static ULong operator -(ULong a, ulong b) => new ULong(a._value - b);

        public static ULong operator *(ULong a, ulong b) => new ULong(a._value * b);

        public static ULong operator /(ULong a, ulong b) => new ULong(a._value / b);

        public static ULong operator ^(ULong a, ulong b) => new ULong(a._value ^ b);

        public static ULong operator >>(ULong a, int b) => new ULong(a._value >> b);

        public static ULong operator <<(ULong a, int b) => new ULong(a._value << b);

        public static implicit operator ULong(ulong v) => new ULong(v);

        public static implicit operator ulong(ULong v) => v._value;

        public static bool operator ==(ULong a, object b) => a.Equals(b);

        public static bool operator !=(ULong a, object b) => !(a == b);

        public override bool Equals(object obj) => obj is ULong b ? this._value.Equals(b._value) : this._value.Equals(obj);

        public override int GetHashCode() => this._value.GetHashCode();
        
        public override string ToString() {
            string s = $"{Convert.ToString((long) this._value, 2).PadLeft(ULong.WIDTH, '0')}";

            // Add commas
            s = $"{string.Join(",", UInt.BinaryRegex.Match(s).Groups[1].Captures.Cast<Capture>().Select(g => g.Value))}";

            return $"{s} ({this._value})";
        }
    }
}

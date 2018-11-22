using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mersenne {

    public static class Extensions {

        public static void Dump(this object o) => Console.WriteLine(o?.ToString() ?? "null");

        public static void Dump(this object o, string name) => Console.WriteLine($"{name}:\n{o?.ToString() ?? "null"}");

        public static void Dump(this string o) => ((object) o).Dump();

        public static void Dump(this string o, string name) => ((object) o).Dump(name);

        public static void Dump<TSource>(this IEnumerable<TSource> o) => Console.WriteLine(string.Join(", ", o));

        public static void Dump<TSource>(this IEnumerable<TSource> o, string name) => Console.WriteLine($"{name}\n{string.Join(", ", o)}");

        public static void Dump<TKey, TValue>(this IDictionary<TKey, TValue> o) {
            int keyWidth = o.Keys.Max(k => k.ToString().Length);
            Console.WriteLine(string.Join("\n", o.Select(kv => $"{kv.Key.ToString().PadRight(keyWidth)}: {kv.Value}")));
        }

        public static void Dump<TKey, TValue>(this IDictionary<TKey, TValue> o, string name) {
            int keyWidth = o.Keys.Max(k => k.ToString().Length);
            Console.WriteLine($"{name}\n{string.Join("\n", o.Select(kv => $"{kv.Key.ToString().PadRight(keyWidth)}: {kv.Value}"))}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mersenne {

    public class Program {

        public static void Main(string[] args) {
            MersenneTwister twister = new MersenneTwister(5235108U, true);
            BigInteger seed = twister.PyGetSeed();
            twister.PyRandom().Dump("[1] - 1");
            twister.PyRandom().Dump("[1] - 2");
            twister.PyRandom().Dump("[1] - 3");
            twister.PyRandom().Dump("[1] - 4");
            twister.PyRandom().Dump("[1] - 5");
            "".Dump();
            twister.PySeed(seed);
            twister.PyRandom().Dump("[2] - 1");
            twister.PyRandom().Dump("[2] - 2");
            twister.PyRandom().Dump("[2] - 3");
            twister.PyRandom().Dump("[2] - 4");
            twister.PyRandom().Dump("[2] - 5");

            /*
            UInt factor = key ^ (key >> 30);
            UInt product = factor * 0x5D588B65U;
            product.Dump("product (original)");
            UInt state = constState ^ product;
            state.Dump("state");
            product = constState ^ state;
            product.Dump("product (solution)");
            */

            /*
            key.Dump("key (original)");
            UInt factor = Program.GetFactorFromKey(key);
            factor.Dump("factor (modified)");
            key = Program.GetKeyFromFactor(factor);
            key.Dump("key (solution)");
            */

            Console.ReadLine();
        }
    }
}

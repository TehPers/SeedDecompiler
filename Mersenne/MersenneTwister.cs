using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Mersenne {

    // Anything prefixed with "Py" was probably ported over from https://github.com/python/cpython (Python's source code).
    // Python's Random implementation (on the C side): https://github.com/python/cpython/blob/master/Modules/_randommodule.c

    public class MersenneTwister {
        public const int W = 32;
        public const int N = 624;
        public const int M = 397;
        public const int R = 31;
        public const uint A = 0b10011001_00001000_10110000_11011111;
        public const uint F = 0b01101100_00000111_10001001_01100101;

        // Shifts
        public const int U = 11;

        public const int S = 7;
        public const int T = 15;
        public const int L = 18;

        // Masks
        public const uint D = 0b11111111_11111111_11111111_11111111;

        public const uint B = 0b10011101_00101100_01010110_10000000;
        public const uint C = 0b11101111_11000110_00000000_00000000;

        public const uint LOWER_MASK = 0b01111111_11111111_11111111_11111111;
        public const uint UPPER_MASK = 0b10000000_00000000_00000000_00000000;

        private readonly UInt[] _mt = new UInt[MersenneTwister.N];
        private int _index = MersenneTwister.N + 1;

        public MersenneTwister(uint seed) {
            this.SetSeed(seed);
        }

        public MersenneTwister(uint seed, bool pySeed) {
            if (pySeed)
                this.PySeed(seed);
            else
                this.SetSeed(seed);
        }

        public MersenneTwister(BigInteger seed) {
            this.PySeed(seed);
        }

        private void SetSeed(uint seed) {
            this._index = MersenneTwister.N;
            this._mt[0] = seed;

            for (uint i = 1; i < MersenneTwister.N; ++i)
                this._mt[i] = MersenneTwister.F * (this._mt[i - 1] ^ (this._mt[i - 1] >> (MersenneTwister.W - 2))) + i;
        }

        public uint GetState() => this.GetState(this._index);
        public uint GetState(int i) => this._mt[i];

        public void SetState(UInt[] state) {
            Array.Copy(state, this._mt, Math.Min(state.Length, this._mt.Length));
            this._index = 0;
        }

        public void Backtrack(int amount = 1) {
            while (amount-- > 0) {
                if (this._index == 0) {
                    this.Untwist();
                    this._index = MersenneTwister.N - 1;
                } else {
                    this._index--;
                }
            }
        }

        public uint ExtractNumber() {
            if (this._index >= MersenneTwister.N) {
                if (this._index > MersenneTwister.N)
                    throw new Exception("Generator was never seeded");
                this.Twist();
            }

            UInt y = this._mt[this._index++];
            y = y ^ ((y >> MersenneTwister.U) & MersenneTwister.D);
            y = y ^ ((y << MersenneTwister.S) & MersenneTwister.B);
            y = y ^ ((y << MersenneTwister.T) & MersenneTwister.C);
            y = y ^ (y >> MersenneTwister.L);

            return y;
        }

        public double PyRandom() {
            // Why 53-bit numbers..?
            UInt a = this.ExtractNumber() >> 5; // Remove 5 bits, 27-bit number
            UInt b = this.ExtractNumber() >> 6; // Remove 6 bits, 26-bit number
            const double k = 1UL << 53; // Maximum value of a 53-bit number + 1
            ULong n = ((ulong) a << 26) + b; // Create a 53-bit number in the form ab
            return n / k; // Turn n into a fraction
        }

        public static (UInt a, UInt b) PyGetPossibleOutputs(double d) {
            const double k = 1UL << 53; // Maximum value of a 53-bit number + 1
            const uint bMask = (1U << 26) - 1; // A binary number containing 26 1s, used to extract b from d (the last 26 bits)
            ULong n = (ULong) (d * k); // Convert d back into a long by multiplying it by its max value + 1
            UInt b = (uint) (n & bMask) << 6; // Use bitwise AND to get b from n, then shift the bits back to the correct locations
            UInt a = (uint) (n >> 26) << 5; // The rest of the bits are a, so just use some shifting, then shift the bits back to the correct locations
            return (a, b); // Keep in mind that some bits were lost by PyRandom. Those bits don't matter to the algorithm though, so anything can go there and you'd get the same result
        }

        public void PySeed(uint seed) => this.PyInitByArray(new[] { seed });

        public void PySeed(ulong seed) => this.PyInitByArray(new[] { (uint) (seed & UInt32.MaxValue), (uint) ((seed >> UInt.WIDTH) & UInt32.MaxValue) });

        public void PySeed(BigInteger seed) {
            List<uint> key = new List<uint>();

            // Must be absolute value
            if (seed.Sign < 1)
                seed *= -1;

            // Split into 32-bit blocks
            while (seed > 0) {
                key.Add((uint) (seed & uint.MaxValue));
                seed >>= UInt.WIDTH;
            }

            if (!key.Any())
                key.Add(0);

            this.PyInitByArray(key.ToArray());
        }

        public void PyInitByArray(uint[] initKey) {
            UInt[] mt = this._mt;
            this.SetSeed(0x12BD6AAU); // init_genrand(self, 19650218U);
            uint i = 1;
            uint j = 0;
            uint k;

            // I wish I knew what this was doing
            // This might be brute forceable. It doesn't look like the current values have too much effect on each other

            // Values are only affected by the ones before them. mt[1] is affected by mt[N - 1] instead of mt[0], and mt[0] = mt[N - 1] usually
            for (k = Math.Max(MersenneTwister.N, (uint) initKey.Length); k > 0; k--) {
                mt[i] = (mt[i] ^ (mt[i - 1] ^ mt[i - 1] >> 30) * 0x19660DU) + initKey[j] + j;
                i++;
                j++;

                if (i >= MersenneTwister.N) {
                    mt[0] = mt[MersenneTwister.N - 1];
                    i = 1;
                }

                if (j >= initKey.Length) {
                    j = 0;
                }
            }

            //
            for (k = MersenneTwister.N - 1; k > 0; k--) {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 0x5D588B65U)) - i;
                i++;

                if (i < MersenneTwister.N)
                    continue;

                mt[0] = mt[MersenneTwister.N - 1];
                i = 1;
            }

            mt[0] = MersenneTwister.UPPER_MASK; // Python sets the first state value to 0x80000000. It gets changed when random() is first called anyway
        }

        public static UInt[] PySeedGetStateFor(string program) {
            if (program.Length * 2 > MersenneTwister.N)
                throw new ArgumentException($"Length of {nameof(program)} must be less than or equal to {MersenneTwister.N / 2}", nameof(program));

            Dictionary<char, double> neededRngs = new Dictionary<char, double>();
            const double chancePerChar = 1 / 96D;
            const double correction = chancePerChar / 10D;

            // Calculate required RNG output for each character
            for (char c = (char) 0; c < 96; c++)
                neededRngs[c == 95 ? '\n' : (char) (c + 32)] = chancePerChar * c + correction;

            UInt[] state = new UInt[MersenneTwister.N];
            int i = 0;
            foreach (char c in program) {
                if (!neededRngs.TryGetValue(c, out double rng))
                    continue;

                (UInt a, UInt b) states = MersenneTwister.PyGetPossibleOutputs(rng);
                state[i++] = MersenneTwister.GetState(states.a);
                state[i++] = MersenneTwister.GetState(states.b);
            }

            return state;
        }

        public BigInteger PyGetSeed(uint tryKey = 0, uint keyWidth = MersenneTwister.N) => MersenneTwister.PyGetSeed(this.PyGetInitKey(tryKey, keyWidth));

        public static BigInteger PyGetSeed(UInt[] initKey) {
            BigInteger seed = 0;

            // Convert initKey to a seed. Maximum width is N * 32 bits due to PySeed only using N values from initKey at most
            for (int i = initKey.Length - 1; i >= 0; i--)
                seed = (seed << UInt.WIDTH) | (uint) initKey[i];

            return seed;
        }

        /// <summary>Returns an initKey for the current state. If the first key value is incorrect, try a different <see cref="tryKey"/> to see if that works.</summary>
        /// <param name="tryKey">First initKey value</param>
        /// <param name="keyWidth">Width of the initKey. Only the first N values matter in it</param>
        /// <returns>A (probably working) initKey</returns>
        public UInt[] PyGetInitKey(uint tryKey = 0, uint keyWidth = MersenneTwister.N) {
            UInt[] initKey = new UInt[keyWidth];
            UInt[] mt = this._mt.ToArray();

            uint j = 0;
            uint k;

            // Emulate setting the state without actually setting it. This can be done mathematically
            for (k = Math.Max(MersenneTwister.N, (uint) initKey.Length); k > 0; k--) {
                j++;
                if (j >= initKey.Length)
                    j = 0;
            }

            // Reverse the process

            // This is the value for mt[0] after the first loop and throughout the second loop
            mt[0] = mt[MersenneTwister.N - 1];

            // Undo the last 'i++' that occured to set i to the last modified index (1)
            uint i = 1;

            // Undo second for loop
            // Hilariously enough, this looks almost exactly the same as the process used to get the state from the initKey
            for (k = 1; k < MersenneTwister.N; k++) {
                mt[i] += i;
                mt[i] = mt[i] ^ (mt[i - 1] ^ (mt[i - 1] >> 30)) * 0x5D588B65U;
                i--;

                if (i == 0) {
                    i = MersenneTwister.N - 1;
                }
            }

            // This is the value for mt[0]
            mt[0] = mt[MersenneTwister.N - 1];

            i = 1;
            j--;
            if (j > initKey.Length)
                j = (uint) initKey.Length - 1;

            // Undo first for loop and grab the initKey
            UInt[] origMt = new MersenneTwister(0x12BD6AAU)._mt;
            for (k = 1; k < Math.Max(MersenneTwister.N, (uint) initKey.Length); k++) {
                if (k == 1) {
                    // initKey[j] is actually a function of initKey[j0]. There is not a single solution for it, but many solutions (finite due to integer restriction).
                    const uint j2 = 0; // j when the first loop starts
                    UInt mtPrev = (origMt[i] ^ (origMt[i - 1] ^ origMt[i - 1] >> 30) * 0x19660DU) + tryKey + j2;
                    initKey[j] = mt[i] - (mtPrev ^ (mt[i - 1] ^ mt[i - 1] >> 30) * 0x19660DU) - j;
                    mt[i] = mtPrev;
                } else {
                    initKey[j] = mt[i] - (origMt[i] ^ (mt[i - 1] ^ mt[i - 1] >> 30) * 0x19660DU) - j;
                    mt[i] = origMt[i];
                }

                i--;
                j--;

                if (i == 0) {
                    mt[0] = origMt[0];
                    i = MersenneTwister.N - 1;
                }

                if (j == uint.MaxValue)
                    j = (uint) initKey.Length - 1;
            }

            return initKey;
        }

        private void Twist() {
            for (uint i = 0; i < MersenneTwister.N; ++i) {
                uint y = (this._mt[i] & MersenneTwister.UPPER_MASK) + (this._mt[(i + 1) % MersenneTwister.N] & MersenneTwister.LOWER_MASK);
                uint next = y >> 1;

                if (y % 2 != 0)
                    next = next ^ MersenneTwister.A;

                this._mt[i] = this._mt[(i + MersenneTwister.M) % MersenneTwister.N] ^ next;
            }

            this._index = 0;
        }

        // Source: https://jazzy.id.au/2010/09/25/cracking_random_number_generators_part_4.html
        private void Untwist() {
            for (int i = MersenneTwister.N - 1; i >= 0; i--) {
                uint result;
                // first we calculate the first bit
                uint tmp = this._mt[i];
                tmp ^= this._mt[(i + MersenneTwister.M) % MersenneTwister.N];
                // if the first bit is odd, unapply magic
                if ((tmp & MersenneTwister.UPPER_MASK) == MersenneTwister.UPPER_MASK) {
                    tmp ^= MersenneTwister.A;
                }
                // the second bit of tmp is the first bit of the result
                result = (tmp << 1) & MersenneTwister.UPPER_MASK;

                // work out the remaining 31 bits
                tmp = this._mt[(i + MersenneTwister.N - 1) % MersenneTwister.N];
                tmp ^= this._mt[(i + MersenneTwister.M - 1) % MersenneTwister.N];
                if ((tmp & MersenneTwister.UPPER_MASK) == MersenneTwister.UPPER_MASK) {
                    tmp ^= MersenneTwister.A;
                    // since it was odd, the last bit must have been 1
                    result |= 1;
                }
                // extract the final 30 bits
                result |= (tmp << 1) & MersenneTwister.LOWER_MASK;
                this._mt[i] = result;
            }
        }

        /// <summary>Gets a seed that will result in the current state</summary>
        public uint? GetSeed() {
            // Setup temporary twister
            MersenneTwister tmp = new MersenneTwister(0);
            tmp.SetState(this._mt);
            tmp.Backtrack();

            // Brute force the seed
            uint target = tmp.GetState(1);
            for (uint brute = UInt32.MinValue; brute < UInt32.MaxValue; brute++) {
                uint bruteTarget = MersenneTwister.F * (brute ^ (brute >> (MersenneTwister.W - 2))) + 1;
                if (bruteTarget == target)
                    return brute;
            }

            // Found no possible seed
            return null;
        }

        public static uint GetState(UInt n) {
            n = MersenneTwister.UntemperR(n, MersenneTwister.L);
            n = MersenneTwister.UntemperL(n, MersenneTwister.T, MersenneTwister.C);
            n = MersenneTwister.UntemperL(n, MersenneTwister.S, MersenneTwister.B);
            n = MersenneTwister.UntemperR(n, MersenneTwister.U, MersenneTwister.D);
            return n;
        }

        public static UInt Temper(UInt n, int shift) => n ^ (n >> shift);

        public static UInt Temper(UInt n, int shift, UInt mask) => n ^ ((n << shift) & mask);

        public static UInt UntemperR(UInt n, int shift) {
            UInt cur = n;
            for (int accuracy = shift; accuracy < UInt.WIDTH; accuracy += shift)
                cur = n ^ (cur >> shift);
            return cur;
        }

        public static UInt UntemperL(UInt n, int shift, UInt mask) {
            UInt cur = n;
            for (int accuracy = shift; accuracy < UInt.WIDTH; accuracy += shift)
                cur = n ^ ((cur << shift) & mask);
            return cur;
        }

        public static UInt UntemperR(UInt n, int shift, UInt mask) {
            UInt cur = n;
            for (int accuracy = shift; accuracy < UInt.WIDTH; accuracy += shift)
                cur = n ^ ((cur >> shift) & mask);
            return cur;
        }

        //////

        private static UInt GetOriginalStateFromProduct(UInt state, UInt product) => state ^ product;

        // Brute force the factor
        private static UInt GetFactorFromProduct(UInt product) {
            IEnumerable<BigInteger> Multiples(BigInteger factor, BigInteger maxMultiple)
            {
                maxMultiple /= factor;
                for (BigInteger i = 0; i <= maxMultiple; i++)
                    yield return factor * i;
            }

            const uint mask = 0xFFFFFFFF;
            foreach (BigInteger multiple in Multiples(0x5D588B65U, (ulong) 0x5D588B65U * 0xFFFFFFFF).Where(m => (m & mask) == product))
                return (uint) (multiple / 0x5D588B65U);

            throw new Exception("Failed to find a factor");
        }

        private static UInt GetKeyFromFactor(UInt factor) => factor ^ (factor >> 30);

        //////

        private static UInt GetFactorFromKey(UInt key) => key ^ (key >> 30);

        private static UInt GetProductFromFactor(UInt factor) => factor * 0x5D588B65U;

        private static UInt GetStateFromProduct(UInt product, UInt curState) => product ^ curState;

        //////
    }
}
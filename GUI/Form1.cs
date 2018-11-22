using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Modules;
using Mersenne;

namespace GUI {
    public partial class Form1 : Form {
        public Form1() {
            this.InitializeComponent();
        }

        private void btnSeed_Click(object sender, EventArgs e) {
            // Automatically populated with 0s
            UInt[] state = MersenneTwister.PySeedGetStateFor(this.rtbBefunge.Text);

            // Output program
            MersenneTwister twister = new MersenneTwister(0);
            twister.SetState(state);
            twister.Backtrack();
            BigInteger seed = twister.PyGetSeed((uint) this.nudInit.Value, (uint) this.nudWidth.Value);
            string program = $"{this.rtbBefunge.TextLength} {seed}";
            this.tbSeed.Text = program;

            // Reverse to befunge
            string converted = "";
            twister = new MersenneTwister(seed);
            int length = this.rtbBefunge.TextLength;
            for (int i = 0; i < length; i++) {
                char c = (char) (int) (twister.PyRandom() * 96);
                converted += c == 95 ? '\n' : (char) (c + 32);
            }

            this.rtbBefunge.Text += $"\n---\n{converted}";
        }

        private void btnBefunge_Click(object sender, EventArgs e) {
            string[] args = this.tbSeed.Text.Split(' ');

            MersenneTwister rand;
            if (args.Length != 2 || !uint.TryParse(args[0], out uint length)) {
                this.rtbBefunge.Text = "Invalid Seed program";
                return;
            }

            if (uint.TryParse(args[1], out uint seed))
                rand = new MersenneTwister(seed, true);
            else if (BigInteger.TryParse(args[1], out BigInteger seedBig))
                rand = new MersenneTwister(seedBig);
            else {
                this.rtbBefunge.Text = "Invalid Seed program";
                return;
            }

            string converted = "";
            for (int i = 0; i < length; i++) {
                char c = (char) (int) (rand.PyRandom() * 96);
                converted += c == 95 ? '\n' : (char) (c + 32);
            }

            this.rtbBefunge.Text = $"{converted}";
        }
    }
}

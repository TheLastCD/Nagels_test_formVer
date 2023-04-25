using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nagels_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //Create and Launch Form
            InitializeComponent();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //Run HexDecode Class
            HexDecode Decoder = new HexDecode(hexBox.Text);
            //Collect Outputs and set labels to the correct one
            BitAl.Text = Decoder.bitABCD[0].ToString();
            BitBl.Text = Decoder.bitABCD[1].ToString();
            BitCl.Text = Decoder.bitABCD[2].ToString();
            BitDl.Text = Decoder.bitABCD[3].ToString();
            TextAl.Text= Decoder.TextA;
            ShortAl.Text = Decoder.ShortA.ToString();
            DateTimeAl.Text = Decoder.DateTimeA.ToString();
        }
    }
}

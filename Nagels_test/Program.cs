using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

namespace Nagels_test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //HexDecode test = new HexDecode(hex1);


        }
    }
    //hex in format A /4E 61 67 65 6C 73 20 55 4B 20 4C 74 64 2E 2E 2E 2E 2E 2E 2E/ 39C1 /5AF43
    //     all 4 bools/TextA                                                      /ShortA/DateTimeA
    class HexDecode
    {
        //two lists one for holding the raw data, and another 2D List to hold the split data
        private List<char> data;
        private List<List<char>> split = new List<List<char>>();

        public List<bool> bitABCD = new List<bool>(); // hold all Bit variables in one list

        public string TextA = "EMPTY";
        public short ShortA;
        public DateTime DateTimeA = new DateTime(1000, 1, 1); //instantiates as the dat 01/01/1000

        public HexDecode(string unedited)
        {
            //opening formatting getting it ready for splitting
            string[] firstSplit = unedited.Split(' ');
            data = new List<char>();
            foreach (string i in firstSplit)
            {
                char[] temp = i.ToCharArray();
                data.Add(temp[0]);
                data.Add(temp[1]);
            }

            Splitter();
            BoolConn(String.Join("", split[0]));
            TextConn(String.Join("", split[1]));
            ShortConn(String.Join("", split[2]));
            DateConn(Numconn(String.Join("", split[3])));

            //PrintData();

        }

        // For splitting and appropriately formatting the input into:
        //[bitsA-D,TextA,ShortA,DateTimeA]
        private void Splitter()
        {
            //range set for the split
            int[] ranges = new int[8] { 0, 0, 1, 40, 41, 44, 45, 49 };

            List<char> subList = new List<char>();

            //loop through every other entry in ranges
            for (int i = 0; i < ranges.Length; i = i + 2)
            {
                //break at last value
                if (ranges[i] == 49) { break; }
                //slice list to within the range i and i+1
                subList = (data.Where((value, index) => index >= ranges[i] && index <= ranges[i + 1]).ToList());
                //Append it to a 2 dimensional list for further work
                split.Add(subList);
            }
        }

        //convert the first digit into the four bools
        private void BoolConn(string hex)
        {
            //convert the digit into a binary nibble
            string binarystring = String.Join(String.Empty, hex.Select
            (c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            //append the individual bits to the ABCD Array for future use
            foreach (char x in binarystring.ToCharArray()) { bitABCD.Add(Convert.ToBoolean(Convert.ToInt32(new string(x, 1)))); }
        }
        //convert hex to text
        private void TextConn(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            TextA = Encoding.ASCII.GetString(raw);
        }

        //Convert Hex into a Decimal number
        private int Numconn(string hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
        //Convert hext to short
        private void ShortConn(string hex)
        {
            ShortA = Convert.ToInt16(hex, 16);
        }

        //convert and calculate date
        //adds number of days from 01/01/1000
        private void DateConn(int DayAddition)
        {
            DateTimeA = DateTimeA.Add(new TimeSpan(DayAddition, 0, 0, 0));
        }
        private void PrintData()
        {
            string[] bitParts = new string[4] { "A", "B", "C", "D" };
            for (int i = 0; i < bitABCD.Count; i++) { Console.WriteLine(String.Join("Bit", bitParts[i], ": ") + bitABCD[i].ToString()); }
            Console.WriteLine("TextA: " + TextA);
            Console.WriteLine("ShortA: " + ShortA.ToString());
            Console.WriteLine("DateTimeA: " + DateTimeA.ToString());
            Console.WriteLine("\n");
        }
    }
}

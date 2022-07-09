using differential_cryptanalysis;

namespace differentialCryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var hlp = new Helpers();
            var hc = new HayesCipher();
            var ds = new DiffirentialSearch();
            var sk = new SearchKey();

            double[] pMins = new double[] { 0.01, 0.015, 0.0015, 4e-04, 4e-05, 4e-06 };
            string[] allBinArray = hlp.GetBinStringArray((int)Math.Pow(2, 16));
            int[][] plainTexts = hlp.GenereateRandomPlainTexts(1000);

            /* Check Hayes Cipher (pt --> pt.txt, key --> key.bin) */
            //string pt = "0011000101100001";
            //string key = "0110111100001110011101000111000001101010011001110110111101101001011010110110001001100111011011000110000101101101";

            //Console.WriteLine(hlp.ArrayToString(hc.Encrypt(hlp.StringToArray(pt), hlp.StringToArray(key), hlp.sBlock)));      

            /* Check inverse S-block transformation */
            //string pt = "0011000101100001";
            //Console.Write("1: ");
            //Console.WriteLine(pt);

            //string pt_1 = hlp.ArrayToString(hc.STransformation(hlp.StringToArray(pt), hlp.sBlock));
            //Console.Write("2: ");
            //Console.WriteLine(pt_1);

            //string pt_2 = hlp.ArrayToString(hc.STransformation(hlp.StringToArray(pt_1), hlp.sBlockInv));
            //Console.Write("3: ");
            //Console.WriteLine(pt_2);

            /* Check inverse Permutation */
            //string pt = "0011000101100001";
            //Console.Write("1: ");
            //Console.WriteLine(pt);

            //string pt_1 = hlp.ArrayToString(hc.Permutation(hlp.StringToArray(pt)));
            //Console.Write("2: ");
            //Console.WriteLine(pt_1);

            //string pt_2 = hlp.ArrayToString(hc.Permutation(hlp.StringToArray(pt_1)));
            //Console.Write("3: ");
            //Console.WriteLine(pt_2);


            /* Find key for alpha */
            string alpha = "0000101100000000";

            var diff = ds.DiffSearch(alpha, allBinArray, pMins);

            //var temp = new Dictionary<string, double>();

            var result = sk.Search(alpha, diff[4], plainTexts);

            Console.WriteLine("done");
        }
    }
}
using differential_cryptanalysis;

namespace differentialCryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //var hc = new HayesCipher();
            //Console.WriteLine(Helpers.ArrayToString(hc.Enrypt(Helpers.StringToArray("0011000101100001"), Helpers.StringToArray("0011000101100001001100100110001000110011011000110011010101100100001101100110011000110010001100010011010000110011"), 6, sBlock)));      

            double[] pMins = new double[] { 0.01, 0.009, 0.00085, 7e-05, 7e-06, 7e-06, 7e-06 };
            string[] allBinArray = Helpers.GetBinStringArray((int)Math.Pow(2, 16));
            double pr = 1 / Math.Pow(2, 16);

            var D = DiffirentialSearch.PreSearch(new string[] { "0000101100000000" }, allBinArray, pr);

            Console.WriteLine("done 1");

            var Diff = DiffirentialSearch.DiffSearch(D, "0000101100000000", allBinArray, pr, pMins);

            Console.WriteLine("done 2");
        }
    }
}
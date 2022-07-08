using differential_cryptanalysis;

namespace differentialCryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //var hc = new HayesCipher();
            //Console.WriteLine(Helpers.ArrayToString(hc.Enrypt(Helpers.StringToArray("0011000101100001"), Helpers.StringToArray("0011000101100001001100100110001000110011011000110011010101100100001101100110011000110010001100010011010000110011"), 6, sBlock)));      
            var hren = new DiffirentialSearch();
            hren.Temp();

            //Console.WriteLine(DiffirentialSearch.D_all);

            //Console.WriteLine(.Length);

            //var temp = Helpers.getBinStringArray((int)Math.Pow(2, 16));

            //foreach(string item in temp)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
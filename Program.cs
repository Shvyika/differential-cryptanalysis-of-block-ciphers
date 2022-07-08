using differential_cryptanalysis;

namespace differentialCryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var sBlock = new Dictionary<string, string> {
                { "0000", "1010" }, { "0001", "1001" }, { "0010", "1101" }, { "0011", "0110" },
                { "0100", "1110" }, { "0101", "1011" }, { "0110", "0100" }, { "0111", "0101" },
                { "1000", "1111" }, { "1001", "0001" }, { "1010", "0011" }, { "1011", "1100" },
                { "1100", "0111" }, { "1101", "0000" }, { "1110", "1000" }, { "1111", "0010" }
            };

            //var hc = new HayesCipher();
            //Console.WriteLine(Helpers.ArrayToString(hc.Enrypt(Helpers.StringToArray("0011000101100001"), Helpers.StringToArray("0011000101100001001100100110001000110011011000110011010101100100001101100110011000110010001100010011010000110011"), 6, sBlock)));      

            DiffirentialSearch.Temp();

            //Console.WriteLine(DiffirentialSearch.D_all);
        }
    }
}
using differential_cryptanalysis;

namespace differentialCryptanalysis
{
    class Program
    {

        static void Main(string[] args)
        { 
            var sBlock = new Dictionary<string, string> {
                { "0000", "1010" },
                { "0001", "1001" },
                { "0010", "1101" },
                { "0011", "0110" },
                { "0100", "1110" },
                { "0101", "1011" },
                { "0110", "0100" },
                { "0111", "0101" },
                { "1000", "1111" },
                { "1001", "0001" },
                { "1010", "0011" },
                { "1011", "1100" },
                { "1100", "0111" },
                { "1101", "0000" },
                { "1110", "1000" },
                { "1111", "0010" }
            };

            var hc = new HayesCipher();
            Console.WriteLine(Helpers.ArrayToString(hc.Enrypt(Helpers.StringToArray("0111110010000011"), Helpers.StringToArray("0011110010010000001111001001000000111100100100000011110010010000001111001001000000111100100100000011110010010000"), 6, sBlock)));
        }
    }
}
namespace differential_cryptanalysis
{
    class Helpers
    {
        public static Dictionary<string, string> sBlock = new Dictionary<string, string> {
            { "0000", "1010" }, { "0001", "1001" }, { "0010", "1101" }, { "0011", "0110" },
            { "0100", "1110" }, { "0101", "1011" }, { "0110", "0100" }, { "0111", "0101" },
            { "1000", "1111" }, { "1001", "0001" }, { "1010", "0011" }, { "1011", "1100" },
            { "1100", "0111" }, { "1101", "0000" }, { "1110", "1000" }, { "1111", "0010" }
        };

        public static string[] binArrayOneNonNullByte = new string[]
        {
            "0000000000000001", "0000000000000010", "0000000000000011", "0000000000000100", "0000000000000101",
            "0000000000000110", "0000000000000111", "0000000000001000", "0000000000001001", "0000000000001010",
            "0000000000001011", "0000000000001100", "0000000000001101", "0000000000001110", "0000000000001111",

            "0000000000010000", "0000000000100000", "0000000000110000", "0000000001000000", "0000000001010000",
            "0000000001100000", "0000000001110000", "0000000010000000", "0000000010010000", "0000000010100000",
            "0000000010110000", "0000000011000000", "0000000011010000", "0000000011100000", "0000000011110000",

            "0000000100000000", "0000001000000000", "0000001100000000", "0000010000000000", "0000010100000000",
            "0000011000000000", "0000011100000000", "0000100000000000", "0000100100000000", "0000101000000000",
            "0000101100000000", "0000110000000000", "0000110100000000", "0000111000000000", "0000111100000000",

            "0001000000000000", "0010000000000000", "0011000000000000", "0100000000000000", "0101000000000000",
            "0110000000000000", "0111000000000000", "1000000000000000", "1001000000000000", "1010000000000000",
            "1011000000000000", "1100000000000000", "1101000000000000", "1110000000000000", "1111000000000000",
        };

        public static int[] GenerateRandomBitArray(int length)
        {
            Random random = new();

            int[] bitArray = new int[length];

            for (int i = 0; i < length; i++)
            {
                bitArray[i] = random.Next(2);
            }

            return bitArray;
        }

        public static int[][] GenerateArrayOfBitArrays(int[] bitArray, int numberOfBitArrays)
        {
            int keyLength = bitArray.Length;
            int roundKeyLength = keyLength / numberOfBitArrays;

            int[][] roundKeys = new int[numberOfBitArrays][];

            for(int i = 0; i < numberOfBitArrays; i++)
            {
                int[] roundKey = new int[roundKeyLength];

                Array.Copy(bitArray, i * roundKeyLength, roundKey, 0, roundKeyLength);

                roundKeys[i] = roundKey;
            }

            return roundKeys;
        }

        public static int[] XOR(int[] x, int[] y)
        {
            int[] result = new int[x.Length];

            for(int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] ^ y[i];
            }

            return result;
        }

        public static string[]  GetBinStringArray(int amount)
        {
            string[] binStringArray = new string[amount];

            for(int i = 0; i < amount; i++)
            {
                binStringArray[i] = Convert.ToString(i, 2).PadLeft(16, '0');
            }

            return binStringArray;
        }

        public static List<int[]> GetBinStringWithOneNonNullByte()
        {
            List<int[]> x_4 = Vectora(4);

            List<int[]> arrays = new List<int[]>();
            for (int i = 0; i < x_4.Count; i++)
            {
                for (int j = 0; j < 16; j = j + 4)
                {
                    int[] zeroArray = new int[16];
                    Array.Copy(x_4[i], 0, zeroArray, 0, x_4[i].Length);
                    arrays.Add(zeroArray);
                }
            }

            return arrays;
        }

        public static List<int[]> Vectora(int n)
        {
            List<int[]> a = new List<int[]>();
            for (int i = 1; i < Math.Pow(2, n); i++)
            {
                string binary_string = "";
                binary_string = Convert.ToString(i, 2);
                
                int[] vector = new int[16];
                while (binary_string.Length < n)
                {
                    binary_string = "0" + binary_string;
                }
                vector = binary_string.Select(s => int.Parse(s.ToString())).ToArray();
                a.Add(vector);
            }
            return a;
        }

        public static string ArrayToString(int[] array)
        {
            return String.Concat(array);
        }

        public static int[] StringToArray(string str)
        {
            int[] array = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                array[i] = Int32.Parse(str[i].ToString());
            }

            return array;
        }

        public static string IntToBin(int number)
        {
            return Convert.ToString(number, 2).PadLeft(16, '0');
        }
    }
}

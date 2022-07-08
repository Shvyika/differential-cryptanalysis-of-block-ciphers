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

        public static string[] getBinStringArray(int amount)
        {
            string[] binStringArray = new string[amount];

            for(int i = 0; i < amount; i++)
            {
                binStringArray[i] = Convert.ToString(i, 2).PadLeft(16, '0');
            }

            return binStringArray;
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

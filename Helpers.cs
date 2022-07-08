namespace differential_cryptanalysis
{
    class Helpers
    {
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
            if(x.Length != y.Length)
            {
                throw new ArgumentException("x length does not match y length");
            }

            int[] result = new int[x.Length];

            for( int i = 0; i < x.Length; i++ )
            {
                result[i] = Convert.ToInt32(x[i] != y[i]);
            }

            return result;
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

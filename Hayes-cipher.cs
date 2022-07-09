using differential_cryptanalysis;

namespace differential_cryptanalysis
{
    class HayesCipher
    {
        private static Helpers hlp = new();

        public int[] STransformation(int[] xInput, Dictionary<string, string> sBlock)
        {
            int[][] xInputSeparated = hlp.GenerateArrayOfBitArrays(xInput);

            string xOutputString = "";

            for (int i = 0; i < xInputSeparated.Length; i++)
            {
                string xInputPartString = String.Concat(xInputSeparated[i]);

                xOutputString += sBlock.First((x) => x.Key == xInputPartString).Value;
            }

            int[] xOutput = hlp.StringToArray(xOutputString);

            return xOutput;
        }

        public int[] Permutation(int[] enterBlock)
        {
            int[][] enterBlockSeparated = hlp.GenerateArrayOfBitArrays(enterBlock);


            int[][] outputBlockSeparated = new int[4][];

            for (int i = 0; i < outputBlockSeparated.Length; i++)
            {
                outputBlockSeparated[i] = new int[enterBlock.Length / 4];
            }

            for (int i = 0; i < enterBlockSeparated.Length; i++)
            {
                for (int j = 0; j < enterBlockSeparated[i].Length; j++)
                {
                    outputBlockSeparated[j][i] = enterBlockSeparated[i][j];
                }
            }

            var outpuList = new List<int>();

            for (int i = 0; i < outputBlockSeparated.Length; i++)
            {
                for (int j = 0; j < outputBlockSeparated[i].Length; j++)
                {
                    outpuList.Add(outputBlockSeparated[i][j]);
                }
            }

            int[] outputBlock = outpuList.ToArray();

            return outputBlock;
        }

        public int[] HayesRound(int[] enterBlock, int[] roundKey, Dictionary<string, string> sBlock)
        {
            int[] enterBlockXORRoundKey;

            if (roundKey.Length > 0)
            {
                enterBlockXORRoundKey = hlp.XOR(enterBlock, roundKey);
            }
            else
            {
                enterBlockXORRoundKey = enterBlock;

            }

            int[] sTransformedBlock = STransformation(enterBlockXORRoundKey, sBlock);

            int[] permutedBlock = Permutation(sTransformedBlock);

            return permutedBlock;
        }


        public int[] Encrypt(int[] enterBlock, int[] roundKey, Dictionary<string, string> sBlock)
        { 
            int[] block = enterBlock;

            for (int i = 0; i < 6; i++)
            {
                int[] roundedBlock = HayesRound(block, roundKey, sBlock);

                block = roundedBlock;
            }

            int[] outputBlock = hlp.XOR(block, roundKey);

            return outputBlock;
        }

        public int[] EncryptForCheck(int[] enterBlock, int[] key, Dictionary<string, string> sBlock)
        {
            int[][] roundKeys = hlp.GenerateArrayOfBitArrays(key, 6 + 1);

            int[] block = enterBlock;

            for (int i = 0; i < 6; i++)
            {
                int[] roundedBlock = HayesRound(block, roundKeys[i], sBlock);

                block = roundedBlock;
            }

            int[] outputBlock = hlp.XOR(block, roundKeys[roundKeys.Length - 1]);

            return outputBlock;
        }
    }
}

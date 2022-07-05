using differential_cryptanalysis;

namespace differential_cryptanalysis
{
    class HayesCipher
    {
        private int[] STransformation(int[] xInput, Dictionary<string, string> sBlock, int numberOfBlocks)
        {
            int[][] xInputSeparated = Helpers.GenerateArrayOfBitArrays(xInput, numberOfBlocks);

            string xOutputString = "";

            for(int i = 0; i < xInputSeparated.Length; i++)
            {
                string xInputPartString = String.Concat(xInputSeparated[i]);

                xOutputString += sBlock.First((x) => x.Key == xInputPartString).Value;
            }

            int[] xOutput = Helpers.StringToArray(xOutputString);

            return xOutput;
        }

        private int[] Permutation(int[] enterBlock, int numberOfBlocks)
        {
            int[][] enterBlockSeparated = Helpers.GenerateArrayOfBitArrays(enterBlock, numberOfBlocks);

            
            int[][] outputBlockSeparated = new int[numberOfBlocks][];

            for(int i = 0; i < outputBlockSeparated.Length; i++)
            {
                outputBlockSeparated[i] = new int[enterBlock.Length / numberOfBlocks];
            }

            for(int i = 0; i < enterBlockSeparated.Length; i++)
            {
                for(int j = 0; j < enterBlockSeparated[i].Length; j++)
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

        private int[] HayesRound(int[] enterBlock, int[] roundKey, int numberOfBlocks, Dictionary<string, string> sBlock)
        {
            int[] enterBlockXORRoundKey = Helpers.XOR(enterBlock, roundKey);

            int[] sTransformedBlock = STransformation(enterBlockXORRoundKey, sBlock, numberOfBlocks);

            int[] permutedBlock = Permutation(sTransformedBlock, numberOfBlocks);

            return permutedBlock;
        }

        public int[] Enrypt(int[] enterBlock, int[] key, int numberOfRound, Dictionary<string, string> sBlock)
        {
            int numberOfBlocks = Int32.Parse(Math.Sqrt(enterBlock.Length).ToString());

            int[][] roundKeys = Helpers.GenerateArrayOfBitArrays(key, numberOfRound + 1);

            int[] block = enterBlock;

            for(int i = 0; i < numberOfRound; i++)
            {
                int[] roundedBlock = HayesRound(block, roundKeys[i], numberOfBlocks, sBlock);

                block = roundedBlock;
            }

            int[] outputBlock = Helpers.XOR(block, roundKeys[roundKeys.Length - 1]);

            return outputBlock;
        }
    }
}

namespace differential_cryptanalysis
{
    internal class SearchKey
    {
        private static readonly Helpers hlp = new();
        private static readonly HayesCipher hc = new();

        public Dictionary<string, int> Search(string alpha, Dictionary<string, double> diff, int[][] plainTexts)
        {
            Dictionary<string, int> keyCounts = new();

            int[] alphaBits = hlp.StringToArray(alpha);
            string[] allKeys = hlp.GetBinStringArray(hlp.max);

            for (int i = 0; i < allKeys.Length; i++)
            {
                for (int j = 0; j < plainTexts.Length; j++)
                {
                    // plainText XOR alpha
                    var plainTextAlpha = hlp.XOR(plainTexts[j], alphaBits);

                    // ptTuples.Add(Tuple.Create(plainText, plainTextAlpha));

                    // plainText --> cipherText
                    var cipherText = hc.Encrypt(plainTexts[j], hlp.StringToArray(allKeys[i]), hlp.sBlock);
                    var cipherTextAlpha = hc.Encrypt(plainTextAlpha, hlp.StringToArray(allKeys[i]), hlp.sBlock);

                    //ctTuples.Add(Tuple.Create(cipherText, cipherTextAlpha));

                    // ксор шт и ключа
                    var cipherText_xor_key = hlp.XOR(cipherText, hlp.StringToArray(allKeys[i]));
                    var cipherTextAlpha_xor_key = hlp.XOR(cipherTextAlpha, hlp.StringToArray(allKeys[i]));

                    //permutated
                    var permutedCipherText_xor_key = hc.Permutation(cipherText_xor_key);
                    var permutaedCipherTextAlpha_xor_key = hc.Permutation(cipherTextAlpha_xor_key);

                    //do inverse s block here
                    var invPermutedCipherText_xor_key = hc.STransformation(permutedCipherText_xor_key, hlp.sBlockInv);
                    var invpermutaedCipherTextAlpha_xor_key = hc.STransformation(permutaedCipherTextAlpha_xor_key, hlp.sBlockInv);

                    // xor results
                    var result = hlp.XOR(invPermutedCipherText_xor_key, invpermutaedCipherTextAlpha_xor_key);

                    var strResult = hlp.ArrayToString(result);

                    if (diff.ContainsKey(strResult))
                    {
                        if (keyCounts.ContainsKey(strResult))
                        {
                            keyCounts[strResult] += 1;
                        }
                        else
                        {
                            keyCounts.Add(strResult, 1);
                        }
                        
                    }
                }
            }

            return keyCounts;
        }
    }
}

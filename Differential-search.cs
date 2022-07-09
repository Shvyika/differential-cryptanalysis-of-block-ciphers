namespace differential_cryptanalysis
{
    internal class DiffirentialSearch
    {
        private static readonly Helpers hlp = new();
        private static readonly HayesCipher hc = new();
        private static readonly double pr = 1 / Math.Pow(2, 16);
        public Dictionary<string, Dictionary<string, double>> PreSearch(string[] binArray, string[] allBinArray)
        {
            int[] emptyArr = Array.Empty<int>();
            

            Dictionary<string, Dictionary<string, double>> D = new();

            foreach (string alpha in binArray)
            {
                Dictionary<string, double> deltaAlpha = new();

                foreach (string plaintext in allBinArray)
                {
                    int[] x = hc.HayesRound(hlp.XOR(hlp.StringToArray(alpha), hlp.StringToArray(plaintext)),emptyArr, hlp.sBlock);

                    int[] y = hc.HayesRound(hlp.StringToArray(plaintext), emptyArr, hlp.sBlock);

                    string beta = hlp.ArrayToString(hlp.XOR(x, y));

                    if (deltaAlpha.ContainsKey(beta))
                    {
                        deltaAlpha[beta] += pr;
                    }
                    else
                    {
                        deltaAlpha.Add(beta, pr);
                    }

                }

                D.Add(alpha, deltaAlpha);
            }

            return D;
        }

        public Dictionary<string, double>[] DiffSearch(string alpha, string[] allBinArray, double[] pMin)
        {
            Dictionary<string, double>[] result = new Dictionary<string, double>[6];

            Dictionary<string, double> gamma_0 = new() { { alpha, 1.0 } };

            result[0] = gamma_0;

            for(int i = 1; i <= 5; i++)
            {
                Dictionary<string, double> gamma_next = new();

                foreach(var betaPair in result[i - 1])
                {
                    var D = PreSearch(new string[] { betaPair.Key }, allBinArray);

                    foreach(var gammaPair in D.Where((el) => el.Key == betaPair.Key).First().Value)
                    {
                        if (gamma_next.ContainsKey(gammaPair.Key))
                        {
                            gamma_next[gammaPair.Key] += betaPair.Value * gammaPair.Value;
                        }
                        else
                        {
                            gamma_next.Add(gammaPair.Key, betaPair.Value * gammaPair.Value);
                        }
                    }
                }

                var gammaNextFiltered = gamma_next.Where((el) => el.Value > pMin[i]).ToDictionary(x => x.Key, x => x.Value);

                result[i] = gammaNextFiltered;
            }
            return result;
        }
    }
}
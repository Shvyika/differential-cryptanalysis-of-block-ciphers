namespace differential_cryptanalysis
{
    internal class DiffirentialSearch
    {
        public static List<Dictionary<string, Dictionary<string, double>>> D_all = new();
        public static int numberOfProcesoors = Environment.ProcessorCount;
        public static string[] binArray = new string[65536];
        // Dictionary<string, Dictionary<string, double>>

        public DiffirentialSearch()
        {
            binArray = Helpers.getBinStringArray((int)Math.Pow(2, 16));
        }
        public static void PreSearch(Object obj)
        {
            int max = (int)Math.Pow(2, 16);
            int basic = (int)max / 8;
            int count = (int)obj;
            int[] emptyArr = Array.Empty<int>();
            double probability = 1 / max;

            string[] outerBinArray = new string[basic];
            Array.Copy(binArray, count * basic, outerBinArray, 0, basic);

            Dictionary<string, Dictionary<string, double>> D = new();

            foreach(string alpha in outerBinArray)
            {
                Dictionary<string, double> deltaAlpha = new();

                foreach(string plaintext in DiffirentialSearch.binArray)
                {
                    int[] x = HayesCipher.HayesRound(
                        Helpers.XOR(Helpers.StringToArray(alpha), Helpers.StringToArray(plaintext)),
                        emptyArr,
                        4,
                        Helpers.sBlock
                    );

                    int[] y = HayesCipher.HayesRound(
                        Helpers.StringToArray(plaintext),
                        emptyArr,
                        4,
                        Helpers.sBlock
                    );

                    string beta = Helpers.ArrayToString(Helpers.XOR(x, y));

                    if (deltaAlpha.ContainsKey(beta))
                    {
                        deltaAlpha[beta] += probability;
                    } else
                    {
                        deltaAlpha.Add(beta, 0);
                    }
                    
                }

                Dictionary<string, double> filteredDeltaAlpha = deltaAlpha.Where((el) => el.Value >= 0.01).ToDictionary(x => x.Key, x => x.Value);

                D.Add(alpha, filteredDeltaAlpha);

            }

            DiffirentialSearch.D_all.Add(D);
        }

        public static List<Dictionary<string, Dictionary<string, double>>> Temp()
        {
            for(int i = 0; i < 8; i++)
            {
                var th = new Thread(ExecuteInForeground);

                th.Start(i);
            }

            return D_all;
        }

        private static void ExecuteInForeground(Object obj)
        {
            DiffirentialSearch.PreSearch(obj);
        }

        public static List<Tuple<int, double>> Search()
        {
            List<Tuple<int, double>> gamma = new();
            // var = tupleTuple.Create(a,b)
            // gamma.Add(tuple)

            return gamma;
        }
    }
}

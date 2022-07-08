using System.Collections.Concurrent;

namespace differential_cryptanalysis
{
    internal class DiffirentialSearch
    {
        //public async Task PreSearch(int count, string[] binArray)
        //{
        //    int max = binArray.Length;
        //    int basic = (int)max / numberOfTasks;
        //    int[] emptyArr = Array.Empty<int>();
        //    double probability = 1 / Math.Pow(2, 16);

        //    string[] outerBinArray = new string[basic];
        //    Array.Copy(binArray, count * basic, outerBinArray, 0, basic);

        //    Dictionary<string, Dictionary<string, double>> D = new();

        //    foreach (string alpha in outerBinArray)
        //    {
        //        Dictionary<string, double> deltaAlpha = new();

        //        foreach (string plaintext in binArray)
        //        {
        //            int[] x = HayesCipher.HayesRound(
        //                Helpers.XOR(Helpers.StringToArray(alpha), Helpers.StringToArray(plaintext)),
        //                emptyArr,
        //                4,
        //                Helpers.sBlock
        //            );

        //            int[] y = HayesCipher.HayesRound(
        //                Helpers.StringToArray(plaintext),
        //                emptyArr,
        //                4,
        //                Helpers.sBlock
        //            );

        //            string beta = Helpers.ArrayToString(Helpers.XOR(x, y));
        //            Console.WriteLine(beta);

        //            if (deltaAlpha.ContainsKey(beta))
        //            {
        //                deltaAlpha[beta] += probability;
        //            }
        //            else
        //            {
        //                deltaAlpha.Add(beta, 0.0);
        //            }

        //        }

        //        //Dictionary<string, double> filteredDeltaAlpha = deltaAlpha.Where((el) => el.Value >= 0.01).ToDictionary(x => x.Key, x => x.Value);
        //        //D.Add(alpha, filteredDeltaAlpha);

        //        D.Add(alpha, deltaAlpha);

        //        //Console.WriteLine($"{D.Count}");
        //    }

        //    D_all.Add(D);
        //}

        public static Dictionary<string, Dictionary<string, double>> PreSearch(string[] binArray, string[] allBinArray, double pr)
        {
            int[] emptyArr = Array.Empty<int>();
            

            Dictionary<string, Dictionary<string, double>> D = new();

            foreach (string alpha in binArray)
            {
                Dictionary<string, double> deltaAlpha = new();

                foreach (string plaintext in allBinArray)
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
                        deltaAlpha[beta] += pr;
                    }
                    else
                    {
                        deltaAlpha.Add(beta, pr);
                    }

                }

                //Dictionary<string, double> filteredDeltaAlpha = deltaAlpha.Where((el) => el.Value >= 0.01).ToDictionary(x => x.Key, x => x.Value);
                //D.Add(alpha, filteredDeltaAlpha);

                D.Add(alpha, deltaAlpha);
            }

            return D;
        }

        //public async Task Pre_Search()
        //{
        //    var binArray = Helpers.binArrayOneNonNullByte;
        //    List<Param> param = new List<Param>();
        //    List<Task> list = new();

        //    for (int i = 0; i < numberOfTasks; i++)
        //    {
        //        param.Add(new Param() { value = i });

        //    }

        //    foreach (var p in param)
        //    {
        //        var task = Task.Run(async () => await PreSearch(p.value, binArray));

        //        list.Add(task);
        //    }


        //    await Task.WhenAll(list);
        //}


        public static Dictionary<string, double>[] DiffSearch(Dictionary<string, Dictionary<string, double>> D, string alpha, string[] allBinArray, double pr, double[] pMin)
        {
            Dictionary<string, double>[] result = new Dictionary<string, double>[6];

            Dictionary<string, double> gamma_0 = new() { { alpha, 1.0 } };

            result[0] = gamma_0;

            for(int i = 1; i <= 5; i++)
            {
                Dictionary<string, double> gamma_next = new();

                foreach(var betaPair in result[i - 1])
                {
                    D = PreSearch(new string[] { betaPair.Key }, allBinArray, pr);

                    foreach(var gammaPair in D.Where((el) => el.Key == betaPair.Key).First().Value)
                    {
                        if (gamma_next.ContainsKey(gammaPair.Key))
                        {
                            //Console.WriteLine("=====");
                            //Console.WriteLine(gamma_next[gammaPair.Key]);
                            //Console.WriteLine("*=====");
                            //Console.WriteLine(betaPair.Value);
                            //Console.WriteLine(gammaPair.Value);
                            //Console.WriteLine("*=====");
                            gamma_next[gammaPair.Key] += betaPair.Value * gammaPair.Value;
                            //Console.WriteLine(gamma_next[gammaPair.Key]);
                            //Console.WriteLine("=====");
                        }
                        else
                        {
                            gamma_next.Add(gammaPair.Key, betaPair.Value * gammaPair.Value);
                        }
                    }
                }

                foreach (var gammaItem in gamma_next)
                {
                    if (gammaItem.Value <= pMin[i])
                    {
                        gamma_next.Remove(gammaItem.Key);
                    }
                }

                var gammaNextFiltered = gamma_next.Where((el) => el.Value > pMin[i]).ToDictionary(x => x.Key, x => x.Value);

                result[i] = gammaNextFiltered;
            }
            return result;
        }
    }
}
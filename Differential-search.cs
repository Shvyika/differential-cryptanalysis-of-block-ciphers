namespace differential_cryptanalysis
{
    internal class DiffirentialSearch
    {
        public static int numberOfProcesoors = Environment.ProcessorCount;
        public static List<Dictionary<string, Dictionary<string, double>>> D_all = new();

        // Dictionary<string, Dictionary<string, double>>
        public static  void PreSearch(Object obj)
        {
            int basic = (int)Math.Pow(2, 16) / DiffirentialSearch.numberOfProcesoors;

            int count = (int)obj;

            Dictionary<string, Dictionary<string, double>> D = new();

            for (int i = basic * count; i < (count == numberOfProcesoors - 1 ? (int)Math.Pow(2, 16) : basic * count + basic); i++)
            {
                Dictionary<string, double> deltaAlpha = new();

                for (int j = 0; j < Math.Pow(2, 16); j++)
                {
                    deltaAlpha.Add(Convert.ToString(j, 2).PadLeft(16, '0'), 0.0);
                }

                D.Add(Convert.ToString(i, 2).PadLeft(16, '0'), deltaAlpha);

            }

            DiffirentialSearch.D_all.Add(D);
        }

        public static List<Dictionary<string, Dictionary<string, double>>> Temp()
        {
            Task[] tasks = new Task[DiffirentialSearch.numberOfProcesoors];

            for(int i = 0; i < DiffirentialSearch.numberOfProcesoors; i++)
            {
                var task = Task.Factory.StartNew(() => PreSearch(i));

                tasks[i] = task;
            }

            Task.WaitAll(tasks);

            return D_all;
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

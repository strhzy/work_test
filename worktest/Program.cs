using Newtonsoft.Json;

namespace worktest
{
    class Program
    {
        private static string logWay;
        private static string sourceWay;
        private static string resultWay;
        private static string districtSort;
        private static string dateSort;

        static void logger(string message, string log)
        {
            string logPath = string.IsNullOrEmpty(log) ? Directory.GetCurrentDirectory() : log;
            string logFile = Path.Combine(logPath, "deliveryLog.log");

            if (!File.Exists(logFile))
            {
                File.Create(logFile).Dispose();
            }
            File.AppendAllTextAsync(logFile, message + " " + DateTime.Now.ToString() + '\n');
        }

        public static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-h")
                {
                    Console.WriteLine("-deliveryLog | -dl -- указание файла для логов или директории для файла");
                    Console.WriteLine("-deliveryData | -dd -- указание исходного файла в формате json");
                    Console.WriteLine("-deliveryOrder | -do -- указание файла с результатом или директории файла");
                    Console.WriteLine("-cityDistrict | -cd -- указание района для сортировки");
                    Console.WriteLine("-firstDeliveryDate | -fdd -- указание даты или времени для сортировки");
                }
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-deliveryLog" || args[i] == "-dl")
                    {
                        if (args.Length >= i + 1)
                        {
                            logWay = args[i + 1];
                        }
                    }
                    if (args[i] == "-deliveryData" || args[i] == "-dd")
                    {
                        if(args.Length >= i + 1)
                        {
                            sourceWay = args[i + 1];
                        }
                    }
                    if (args[i] == "-deliveryOrder" || args[i] == "-do")
                    {
                        if (args.Length >= i+1)
                        {
                            resultWay = args[i + 1];
                        }
                    }
                    if (args[i] == "-cityDistrict" || args[i] == "-cd")
                    {
                        if (args.Length >= i + 1)
                        {
                            districtSort = args[i + 1];
                        }
                    }
                    if (args[i] == "-firstDeliveryDate" || args[i] == "-fdd")
                    {
                        if (args.Length >= i + 1)
                        {
                            dateSort = args[i + 1];
                        }
                    }
                }
            }
            logger("Программа запущена", logWay);
            List<Order> source = new List<Order>();
            if (!string.IsNullOrEmpty(sourceWay) && File.Exists(sourceWay))
            {
                try
                {
                    string jsonContent = File.ReadAllText(sourceWay);
                    source = JsonConvert.DeserializeObject<List<Order>>(jsonContent);
                }
                catch
                {
                    logger("Исходный файл не соответствует формату программы", logWay);
                    return 0;
                }
                logger("Исходный файл загружен", logWay);
            }
            else
            {
                logger("Исходный файл пуст или не существует", logWay);
                logger("Программа остановлена", logWay);
                return 0;
            }

            List<Order> result= new List<Order>();

            if (source != null && (districtSort != null && districtSort != "") || (dateSort != null && dateSort != ""))
            {
                foreach (var item in source)
                {
                    if ((districtSort != null && districtSort != "" && item.district == districtSort) ||
                        (dateSort != null && dateSort != "" && item.deliverydate.Contains(dateSort)))
                    {
                        result.Add(item);
                    }
                }
                logger("Данные отсортированы", logWay);
            }
            else
            {
                logger("Ошибка сортировки", logWay);
            }


            if (File.Exists(resultWay) && resultWay!="" && resultWay!=null)
            {
                var jsonContent = JsonConvert.SerializeObject(result);
                File.WriteAllText(resultWay, jsonContent);
                logger("Результат записан",logWay);
            }
            else if(sourceWay.EndsWith(".json") && resultWay != "" && resultWay != null)
            {
                File.Create(resultWay);
                var jsonContent = JsonConvert.SerializeObject(result);
                File.WriteAllText(resultWay, jsonContent);
                logger("Результат записан", logWay);
            }
            else
            {
                
                File.Create("result.json");
                var jsonContent = JsonConvert.SerializeObject(result);
                File.WriteAllText(resultWay, jsonContent);
                logger("Результат записан", logWay);
            }

            return 1;
        }
    }
}

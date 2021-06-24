using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace lab_1_namespace
{
    public class ManageAllExceptions
    {
        private static int critical_exeptions_counter = 0;
        private static List<string> criticalExceptions = new List<string>();

        public static int GetCRITICAL_amount()
        {
            return critical_exeptions_counter;
        }

        public static void SetupExceptions()
        {
            criticalExceptions.Add("CRITICAL_exp");
            criticalExceptions.Add("LITTLE_CRITICAL_exp");
            criticalExceptions.Add("ABSOLUTELY_CRITICAL_exp");
            criticalExceptions.Add("THE_MOST_CRITICAL_exp");
        }

        public static void CheckIfCritical(Exception e)
        {
            if (criticalExceptions.Contains(e.GetType().Name))
            {
                increaseCriticalCounter(e);
            }
            else
            {
                increaseNonCriticalCounter(e);
            }
        }

        private static void increaseCriticalCounter(Exception e)
        {
           critical_exeptions_counter++;
        }

        private static void increaseNonCriticalCounter(Exception e)
        {
        }
    }


    class EXEPTIONS_THROWNER
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            ManageAllExceptions.SetupExceptions();
            while (true)
            {
                try
                {
                    if (rand.Next() % 100  == 0)
                    {
                        throw new NO_CRITICAL_exp();
                    }
                    else
                    {
                        throw new CRITICAL_exp();
                    }
                }
                catch (Exception e) {ManageAllExceptions.CheckIfCritical(e); }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo q = Console.ReadKey(true);
                    if (q.Key == ConsoleKey.Q)
                    {
                        break;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }


    public class NO_CRITICAL_exp : Exception
    {
    }


    public class CRITICAL_exp : Exception
    {
    }

    public class LITTLE_CRITICAL_exp : Exception
    {
    }

    public class  ABSOLUTELY_CRITICAL_exp : Exception
    {
    }

    public class THE_MOST_CRITICAL_exp : Exception
    {
    }
}

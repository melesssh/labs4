using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ExceptionHandler
{
    public class NonCriticalException : Exception
    {
    }

    public class CriticalException : Exception
    {
    }

    public class ExceptionManager
    {
        private static string exceptionServer = "http://www.someurl.com";
        public FileReader fileReader;
        private static int critExcCounter = 0;
        private static int failedCritExcReports = 0;
        private static List<string> criticalExceptions = new List<string>();

        public int GetCritExcCounter()
        {
            return critExcCounter;
        }

        public int GetFailedCritExcReports()
        {
            return failedCritExcReports;
        }

        public ExceptionManager(FileReader fileReader, string pathToConfig)
        {
           criticalExceptions=fileReader.readFile(pathToConfig);
           Console.WriteLine(criticalExceptions);
        }

        public ExceptionManager()
        {
        
        }

        public  void Handle(Exception e)
        {
            if (criticalExceptions.Contains(e.GetType().Name))
            {
                HandleCritical(e);
            }
            else
            {
                HandleNonCritical(e);
            }
        }

        private  void HandleCritical(Exception e)
        {
            Console.WriteLine("Critical exception thrown");
            critExcCounter++;
            HttpClient http = new HttpClient();
            var content = new StringContent(
                JsonSerializer.Serialize(e.GetType().FullName),
                Encoding.UTF8,
                "application/json");
            var resp = http.PostAsync(exceptionServer, content);
            HandleInternal(e);
            try
            {
                resp.Wait();
                if (resp.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Critical exception reported successfuly");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Failed to report critical exception to server");
                failedCritExcReports++;
            }
        }

        private void HandleNonCritical(Exception e)
        {
            Console.WriteLine("Non-Critical exception thrown");
            HandleInternal(e);
        }

        private void HandleInternal(Exception e)
        {
            //process exception itself
        }

        public void fillcriticalExeptions(string pathToConfig)
        {
           criticalExceptions=fileReader.readFile(pathToConfig);
           Console.WriteLine(criticalExceptions);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {   
            FileReader fileReader= new FileReader();
            ExceptionManager lab2 = new ExceptionManager(fileReader,"critical_exceptions.conf");
            Console.WriteLine("To stop exception generation press \"q\"");
            Random rand = new Random();
            while (true)
            {
                try
                {
                    if (rand.Next() % 2 == 0)
                    {
                        throw new NonCriticalException();
                    }
                    else
                    {
                        throw new CriticalException();
                    }
                }
                catch (Exception e) { lab2.Handle(e); }

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

    public class FileReader
    {
        public virtual List<string> readFile(string path)
        {
            List<string> criticalExceptions = new List<string>();
                Console.WriteLine("Reading critical exceptions config file...");
                string contents = File.ReadAllText(path);
                contents = contents.Replace("\r", "");
                string[] criticalExceptionsFromFile = contents.Split("\n");
                foreach (var item in criticalExceptionsFromFile)
                {
                    criticalExceptions.Add(item);
                }
                return criticalExceptions;
            }
        }
    }

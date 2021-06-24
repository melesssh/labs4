using System.ComponentModel;
using System;

namespace ExceptionHandler
{
    public class ExceptionManagerFactory
    {
        public FileReader fileReader;

        public ExceptionManagerFactory() { }

        public ExceptionManagerFactory WithFileReader(FileReader read)
        {
            fileReader = read;
            return this;
        }

        public ExceptionManager Build(string path )
        {
            return new ExceptionManager(fileReader, path);
        }
    }
}
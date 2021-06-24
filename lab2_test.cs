using System;
using System.ComponentModel;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;

namespace ExceptionHandler
{
    public class Tests
    {

        public class TestData
        {
            public static List<String> CriticalExceptions = new List<String>()
            {  
                "CriticalException",
            };
        }
        
        //use constructor
        [TestCase(typeof(CriticalException), true)]
        [TestCase(typeof(NonCriticalException), false)]
        [Test]
        [Combinatorial]
        public void Test0(Type exceptionType, bool expectedResult)
        {
            var MockFileReader = new Mock<FileReader>();
            MockFileReader.Setup(x => x.readFile(It.IsAny<string>())).Returns((String x) => TestData.CriticalExceptions);
            ExceptionManager lab2 = new ExceptionManager(MockFileReader.Object, "mocked");
            var ERR = (Exception)Activator.CreateInstance(exceptionType);
            int oldCounter = lab2.GetCritExcCounter();
            lab2.Handle(ERR);
            Assert.AreEqual(lab2.GetCritExcCounter() - oldCounter == 1, expectedResult);
        }

        //use constructor
        [TestCase(typeof(CriticalException), false)]
        [TestCase(typeof(NonCriticalException), true)]
        [Test]
        [Combinatorial]
        public void Test1(Type exceptionType, bool expectedResult)
        {
            var MockFileReader = new Mock<FileReader>();
            MockFileReader.Setup(x => x.readFile(It.IsAny<string>())).Returns((String x) => TestData.CriticalExceptions);
            ExceptionManager lab2 = new ExceptionManager(MockFileReader.Object, "mocked");
            var ERR = (Exception)Activator.CreateInstance(exceptionType);
            int oldCounter = lab2.GetCritExcCounter();
            lab2.Handle(ERR);
            Assert.AreEqual(lab2.GetCritExcCounter() - oldCounter == 0, expectedResult);
        }

         //use property add
        [TestCase(typeof(CriticalException), false)]
        [TestCase(typeof(NonCriticalException), true)]
        [Test]
        [Combinatorial]
        public void Test2(Type exceptionType, bool expectedResult)
        {
            var MockFileReader = new Mock<FileReader>();
            MockFileReader.Setup(x => x.readFile(It.IsAny<string>())).Returns((String x) => TestData.CriticalExceptions);
            ExceptionManager lab2 = new ExceptionManager();
            lab2.fileReader=MockFileReader.Object;
            lab2.fillcriticalExeptions("path");
            var ERR = (Exception)Activator.CreateInstance(exceptionType);
            int oldCounter = lab2.GetCritExcCounter();
            lab2.Handle(ERR);
            Assert.AreEqual(lab2.GetCritExcCounter() - oldCounter == 0, expectedResult);
        }

        //use property add
        [TestCase(typeof(CriticalException), true)]
        [TestCase(typeof(NonCriticalException), false)]
        [Test]
        [Combinatorial]
        public void Test3(Type exceptionType, bool expectedResult)
        {
            var MockFileReader = new Mock<FileReader>();
            MockFileReader.Setup(x => x.readFile(It.IsAny<string>())).Returns((String x) => TestData.CriticalExceptions);
            ExceptionManager lab2 = new ExceptionManager();
            lab2.fileReader=MockFileReader.Object;
            lab2.fillcriticalExeptions("path");
            var ERR = (Exception)Activator.CreateInstance(exceptionType);
            int oldCounter = lab2.GetCritExcCounter();
            lab2.Handle(ERR);
            Assert.AreEqual(lab2.GetCritExcCounter() - oldCounter == 1, expectedResult);
        }

        //use factory
        [TestCase(typeof(CriticalException), true)]
        [TestCase(typeof(NonCriticalException), false)]
        [Test]
        [Combinatorial]
        public void Test4(Type exceptionType, bool expectedResult)
        {
            var MockFileReader = new Mock<FileReader>();
            MockFileReader.Setup(x => x.readFile(It.IsAny<string>())).Returns((String x) => TestData.CriticalExceptions);
             var lab2 = new ExceptionManagerFactory()
                .WithFileReader(MockFileReader.Object)
                .Build("path");
            var ERR = (Exception)Activator.CreateInstance(exceptionType);
            int oldCounter = lab2.GetCritExcCounter();
            lab2.Handle(ERR);
            Assert.AreEqual(lab2.GetCritExcCounter() - oldCounter == 1, expectedResult);
        }

        //use factory
        [TestCase(typeof(CriticalException), false)]
        [TestCase(typeof(NonCriticalException), true)]
        [Test]
        [Combinatorial]
        public void Test5(Type exceptionType, bool expectedResult)
        {
            var MockFileReader = new Mock<FileReader>();
            MockFileReader.Setup(x => x.readFile(It.IsAny<string>())).Returns((String x) => TestData.CriticalExceptions);
             var lab2 = new ExceptionManagerFactory()
                .WithFileReader(MockFileReader.Object)
                .Build("path");
            var ERR = (Exception)Activator.CreateInstance(exceptionType);
            int oldCounter = lab2.GetCritExcCounter();
            lab2.Handle(ERR);
            Assert.AreEqual(lab2.GetCritExcCounter() - oldCounter == 0, expectedResult);
        }
               
    }
}
using NUnit.Framework;
using System;
using System.Collections.Generic;
namespace lab_1_namespace
{
    public class lab_work_1
    {

        [SetUp]
        public void BeforeAllTests()
        {
           ManageAllExceptions.SetupExceptions();
        }

        [TestCase(typeof(CRITICAL_exp), 1)]
        [TestCase(typeof(ABSOLUTELY_CRITICAL_exp), 2)]
        [Test]
        public void CheckIfCriticalIncreasesCriticalCounter(Type exceptionType, int expectedResult)
        {
            var EXCEPTION = (Exception)Activator.CreateInstance(exceptionType);
            int was = ManageAllExceptions.GetCRITICAL_amount();
            ManageAllExceptions.CheckIfCritical(EXCEPTION);
            Assert.AreEqual(ManageAllExceptions.GetCRITICAL_amount(), expectedResult);
        }
    }
}
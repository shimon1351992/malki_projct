using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace HETS1Design
{
    [TestClass]
     public class TestCasesTest
    {
        //Global variable used for testing
        string fileToCheckContent;
        string fileToCheckContent1;
        //string inputFileToCheckContent;
        //string outputFileToCheckContent;


        //Works in a similar manner to "Before" in JUnit
        [TestInitialize]
        public void Initialize()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
            fileToCheckContent = File.ReadAllText(@"..\..\..\Assets\Test Required FIles\TestCasesTest\GeneralTestCasesExample.txt");
            fileToCheckContent1 = File.ReadAllText(@"..\..\..\Assets\Test Required FIles\TestCasesTest\emptytestcase.txt");
            TestCases.ResetTestCases();
            
            //inputFileToCheckContent = File.ReadAllText(@"..\..\..\Assets\Test Required FIles\TestCasesTest\InputTestCasesExample.txt"); 
            //outputFileToCheckContent = File.ReadAllText(@"..\..\..\Assets\Test Required FIles\TestCasesTest\OutputTestCasesExample.txt");
        }

        [TestMethod]
        public void OnAddTestCaseTest()
        {
            //TODO            
        }
        [TestMethod]
        public void TestCommand()
        {
            string inputFileText = "findmax.c";
            string NoCommentsFile = @"C:\Users\97252\Desktop\לימודים\שנה שלישית סמסטר ב'\אימות ובדיקות תוכנה\פרוייקט 2\Project Code\HETS - Azo\HETS1Design.UnitTests\bin\Debug\test.txt";
            var outputFileTest = SingleSubmission.CheckComm(inputFileText);
            var outputFileTest1 = SingleSubmission.CheckComm(NoCommentsFile);
            Assert.IsTrue(outputFileTest);
            Assert.IsFalse(outputFileTest1);


        }

        [TestMethod]
        public void TestCasesBuilder_AddedSuccessfully()
        {
            //Arrange
            
            var inputFileText = "__[TC]\r\n3 4\r\n__[TC]\r\n35"; 
            var outputFileTest = "__[TC]\r\n1\r\n__[TC]\r\nWrong input"; 
            
            var listSize = 2; //1 test case added
            //Act
            TestCases.TestCasesBuilder(inputFileText, outputFileTest);
            //Assert
            Assert.AreEqual(listSize, TestCases.testCases.Count);            
        }
        [TestMethod]
        public void TestCasesBuilder_NoCases()
        {
            var inputFileText = "";
            var outputFileTest = "";

            //var listSize = 0;
           // TestCases.TestCasesBuilder(inputFileText, outputFileTest);
            //Assert
            // Assert.AreEqual(listSize, TestCases.testCases.Count);
            Assert.ThrowsException<Exception>(() => TestCases.TestCasesBuilder(inputFileText, outputFileTest));



        }


        [TestMethod]
        public void TestCasesBuilder_NotAddedSuccessfully()
        {
            TestCases.ResetTestCases();
            //Arrange
            var inputFileText = "__[TC]\r\n3 4\r\n__[TC]\r\n35"; 
            var outputFileTest = "__[TC]\r\n1";
            var listSize = 2; //Suposedely 2 test cases (but tester forgot to add the output field)
                              

            //Assert+Act
            Assert.ThrowsException<Exception>(() => TestCases.TestCasesBuilder(inputFileText, outputFileTest));
            Assert.AreNotEqual(listSize, TestCases.testCases.Count);
            Assert.AreEqual(0, TestCases.testCases.Count);
        }

        [TestMethod]
        public void CountTestCases_Test()
        {
            //Arrange
            //Act
            var result1 = 0;
            if(fileToCheckContent1 == "")
            {
                 result1 = 0;
            }else
            {
                result1 = TestCases.CountTestCases(fileToCheckContent1);
            }
            var result = TestCases.CountTestCases(fileToCheckContent);
           
            //Assert
            Assert.AreEqual(5, result);
            Assert.AreEqual(0,result1);
            Assert.AreNotEqual(4, result);
            Assert.AreNotEqual(6, result);
        }

        [TestMethod]
        public void TC_or_TNC_ContainsEither()
        {
            //Arrange
            //Act
            var result1 = TestCases.TC_or_TNC("__[TC]\r\n4 5 6");
            var result2 = TestCases.TC_or_TNC("__[TNC]\r\n7");
            //Assert
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void TestCasesSeparator_Seperates()
        {
            //Arrange
            //Act
            //List<string> result1 = new List<string>();
            var result = TestCases.TestCasesSeparator(fileToCheckContent);
            
           // var result1 = TestCases.TestCasesSeparator(fileToCheckContent1);
            //if (fileToCheckContent1 == "")
            //{
            //    result1[0] = "";
            //}
            //else
            //{
            //    result1 = TestCases.TestCasesSeparator(fileToCheckContent1);
            //}

            //Assert
            string[] expected = { "__[TC]\r\n9 5", "__[TNC]\r\n3 4\r\n1\r\n__[Bound] 5 9\r\n10", "__[TNC]\r\n7 3", "__[TC] 19\r\n", "__[TC] bla bla\r\n75" };
         //  string[] expected1 = {""};
            List<string> expectedList = new List<string>(expected);
           // List<string> expectedList1 = new List<string>(expected1);
            CollectionAssert.AreEqual(expectedList, result);
          //  CollectionAssert.AreEqual(expectedList1, result1);
        }

        [TestMethod]
        public void RemoveTCTNC_Removed()
        {
            //Arrange
            var testCase = "__[TC]\r\n3 4";
            //Act
            var result = TestCases.RemoveTCTNC(testCase);
            //Assert
            Assert.AreEqual("3 4", result);
        }

        [TestMethod]
        public void MultiplyTestCasesByBoundary_Multiplies()
        {
            //TODO (how to validate recursive method?)
        }

        [TestMethod]
        public void MultiplyTestCasesByEP_Multiplies()
        {
            //TODO (how to validate recursive method?)
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
        }

    }
}

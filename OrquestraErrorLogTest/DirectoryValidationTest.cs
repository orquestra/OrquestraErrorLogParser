using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrquestraErrorLogParses;

namespace OrquestraErrorLogTest
{
    [TestClass]
    public class DirectoryValidationTest
    {
        /// <summary>
        ///A test for DirectoryExists
        ///</summary>
        [TestMethod()]
        public void DirectoryExistsWithInvalidDirectoryTest()
        {
            DirectoryValidation target = new DirectoryValidation(); // TODO: Initialize to an appropriate value
            string DirectoryPath = "asdfa"; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DirectoryExists(DirectoryPath);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DirectoryExists
        ///</summary>
        [TestMethod()]
        public void DirectoryExistsWithValidDirectoryTest()
        {
            DirectoryValidation target = new DirectoryValidation(); // TODO: Initialize to an appropriate value
            string DirectoryPath = "D:\\Download\\newfolder"; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DirectoryExists(DirectoryPath);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DirectoryHasFiles
        ///</summary>
        [TestMethod()]
        public void DirectoryHasFilesWithEmptyDirectoryTest()
        {
            DirectoryValidation target = new DirectoryValidation(); // TODO: Initialize to an appropriate value
            string DirectoryPath = "D:\\Download\\newfolder"; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DirectoryHasFiles(DirectoryPath);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
        /// <summary>
        ///A test for DirectoryHasFiles
        ///</summary>
        [TestMethod()]
        public void DirectoryHasFilesWithPopulatedDirectoryTest()
        {
            DirectoryValidation target = new DirectoryValidation(); // TODO: Initialize to an appropriate value
            string DirectoryPath = "D:\\Download\\newfolder2"; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DirectoryHasFiles(DirectoryPath);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }
}

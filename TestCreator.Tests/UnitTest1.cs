using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCreator.Tests
{
    [TestClass]
    public class FindVarsTests
    {
        [TestMethod]
        public void FindNoVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars(""),
                new System.Collections.Generic.List<string>()
                );
        }
        [TestMethod]
        public void FindOneShortVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars("text whith {a}"),
                new System.Collections.Generic.List<string>() { "a" }
                );
        }
        [TestMethod]
        public void FindOneLongVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars("text whith {abc}"),
                new System.Collections.Generic.List<string>() { "abc" }
                );
        }
        [TestMethod]
        public void FindMoreLongVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars("text whith {ac}{d}"),
                new System.Collections.Generic.List<string>() { "ac", "d" }
                );
        }
        [TestMethod]
        public void FindMoreDublicateLongVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars("text whith {ac} {d} {ac} {d}"),
                new System.Collections.Generic.List<string>() { "ac", "d" }
                );
        }
        [TestMethod]
        public void FindRealyMoreLongVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars("text whith {Ab(C)_d}"),
                new System.Collections.Generic.List<string>() { "Ab(C)_d" }
                );
        }
        [TestMethod]
        public void FindDontCloseBracketVar()
        {
            CollectionAssert.AreEqual(
                TestCreator.Services.StringSplitter.FindVars("text whith {A"),
                new System.Collections.Generic.List<string>() { "A" }
                );
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FindWrongBracketVar()
        { 
            Services.StringSplitter.FindVars("text whith {a{c}");
        }
        [TestMethod]
        public void TestTimeFindVar()
        {
            Services.StringSplitter.FindVars("this is {so} long text whist {aa} and {bb} and more {aa} {aa} {aa} {aa}{aa}{aa}{aa}... and more {bb} {bb} {bb} {bb}{bb}{bb}{bb}{bb}{bb}... {so}");
            Assert.IsTrue(true);
        }
    }
}

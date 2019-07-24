using Microsoft.VisualStudio.TestTools.UnitTesting;
using dnatest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnatest.Tests
{
    [TestClass()]
    public class ReconstructFragmentsTests
    {
        [TestMethod()]
        public void ReconstructFragmentTest1()
        {
            List<string> l = new List<string>();
            l.Add("a l l i s w e l l");
            l.Add("e l l t h a t e n");
            l.Add("h a t e n d");
            l.Add("t e n d s w e l l");
            l.Add("e l l");
            string expected = "a l l i s w e l l t h a t e n d s w e l l";
            IReconstructFragmentsTools reconstructFragmentsTools = new ReconstructFragmentsTools();

            IReconstructFragments reconstructFragments = new ReconstructFragments(reconstructFragmentsTools);
            string actual = reconstructFragments.ReconstructFragmentSentences(l);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ReconstructFragmentTest2()
        {
            List<string> l = new List<string>();
            l.Add("a l l i s w e l l");
            l.Add("e l l t h a t e n");
            l.Add("h a t ");
            l.Add("t e n d s w e l l");
            l.Add("e l l");
            string expected = "a l l i s w e l l t h a t e n d s w e l l";
            IReconstructFragmentsTools reconstructFragmentsTools = new ReconstructFragmentsTools();

            IReconstructFragments reconstructFragments = new ReconstructFragments(reconstructFragmentsTools);
            string actual = reconstructFragments.ReconstructFragmentSentences(l);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ReconstructFragmentTest3()
        {
            List<string> l = new List<string>();
            l.Add("a l l i ");
            l.Add("e l l t h a t e n");
            l.Add("h a t ");
            l.Add("t e n d s w e l l");
            l.Add("s w e l h a t e n");
            string expected = "s w e l h a t e n d s w e l l t h a t e na l l i ";
            IReconstructFragmentsTools reconstructFragmentsTools = new ReconstructFragmentsTools();

            IReconstructFragments reconstructFragments = new ReconstructFragments(reconstructFragmentsTools);
            string actual = reconstructFragments.ReconstructFragmentSentences(l);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ReconstructFragmentTest4()
        {
            List<string> l = new List<string>();
            IReconstructFragmentsTools reconstructFragmentsTools = new ReconstructFragmentsTools();

            l = reconstructFragmentsTools.ReadFragmentsFromFile("D:/AllKindofTest/VSTest/dnaGraph/dnaTest/dnaTest/fragments/fragments.txt");
            Assert.AreEqual(5, l.Count);
        }
    }
}
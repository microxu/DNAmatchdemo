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
            IReadFragmentFiles readFragmentFiles = new ReadFragmentFiles();

            l = readFragmentFiles.ReadFragmentsFromFile(@"../../../dnaTest/fragments/fragments.txt");
            Assert.AreEqual(5, l.Count);
        }
        [TestMethod()]
        public void ReconstructFragmentTest5()
        {
            var fragments = "m quaerat voluptatem.;pora incidunt ut labore et d;, consectetur, adipisci velit;olore magnam aliqua;idunt ut labore et dolore magn;uptatem.;i dolorem ipsum qu;iquam quaerat vol;psum quia dolor sit amet, consectetur, a;ia dolor sit amet, conse;squam est, qui do;Neque porro quisquam est, qu;aerat voluptatem.;m eius modi tem;Neque porro qui;, sed quia non numquam ei;lorem ipsum quia dolor sit amet;ctetur, adipisci velit, sed quia non numq;unt ut labore et dolore magnam aliquam qu;dipisci velit, sed quia non numqua;us modi tempora incid;Neque porro quisquam est, qui dolorem i;uam eius modi tem;pora inc;am al"
                .Split(';')
                .ToList();

            IReconstructFragmentsTools reconstructFragmentsTools = new ReconstructFragmentsTools();
            IReconstructFragments reconstructFragments = new ReconstructFragments(reconstructFragmentsTools);
            string actual = reconstructFragments.ReconstructFragmentSentences(fragments);

            Assert.AreEqual("Neque porro quisquam est, qui dolorem i dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.", actual);

        }
    }
}
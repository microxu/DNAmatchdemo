using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace dnatest
{
    public interface IReconstructFragments
    {
        string ReconstructFragmentSentences(List<string> OriginalList);
        }
    public interface IReadFragmentFiles
    {
        /// <summary>
        /// read a file
        /// </summary>
        /// <param name="Path">the path of a file,including file name</param>
        /// <returns>list of each line</returns>
        List<string> ReadFragmentsFromFile(string Path);
    }
    public interface IReconstructFragmentsTools
    {
        /// <summary>
        /// KMP algorithm. Search target string in Source string
        /// </summary>
        /// <param name="target">target string</param>
        /// <param name="source">source string</param>
        /// <returns>The location of the target string in the source string</returns>
        int KMPSearch(string target, string source);
        /// <summary>
        /// depth copy a list
        /// </summary>
        /// <param name="OringalList">the original list</param>
        /// <returns>a copy of the list</returns>
        List<string> CopyOList(List<string> OringalList);
        /// <summary>
        /// the overlap of str2+str1
        /// </summary>
        /// <param name="str1">from front of the str1</param>
        /// <param name="str2">from back of the str2</param>
        /// <returns>overlap</returns>
        string GetOneMaxOverlapString(string str1, string str2);
        /// <summary>
        /// remove overlap from a string
        /// </summary>
        /// <param name="str">source string</param>
        /// <param name="maxStr">overlap string</param>
        /// <returns>the string get rid of overlap</returns>
        string RemoveMaxOverlapString(string str, string maxStr);
        /// <summary>
        /// merge two strings
        /// </summary>
        /// <param name="lMax">the store of str1,str2 and their overlap string</param>
        /// <returns>str2+str1</returns>
        string MergeMaxOverlapString(List<string> lMax);

    }
    public class ReadFragmentFiles : IReadFragmentFiles
    {
        public List<string> ReadFragmentsFromFile(string Path)
        {
            List<string> inputList = new List<string>();
            try
            {

                StreamReader sr = new StreamReader(@Path, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    inputList.Add(line.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Read file error", ex);
            }
            return inputList;
        }
    }
    public class ReconstructFragmentsTools : IReconstructFragmentsTools
    {
        public List<string> CopyOList(List<string> OringalList)
        {
            List<string> newList = new List<string>();
            foreach (var j in OringalList)
            {
                newList.Add(j);
            }
            return newList;
        }

        public string GetOneMaxOverlapString(string str1, string str2)
        {
            string maxStr = "";
            int l = str1.Length > str2.Length ? str2.Length : str1.Length;
            for (int i = l; i >= 1; i--)
            {
                string temp = str1.Substring(0, i);
                if (str2.Substring(str2.Length - i, i) == temp)
                {
                    maxStr = temp;
                    break;
                }
            }
            return maxStr;
        }
        /// <summary>
        /// Part of KMP algorithm. Get each char's back position when not match the source string.
        /// </summary>
        /// <param name="target">target string</param>
        /// <returns>Array</returns>
        private int[] getNext(string target)
        {
            int[] next = new int[target.Length];
            next[0] = -1;
            int k = -1;

            for (int i = 1; i < target.Length; i++)
            {
                while (k > -1 && target[k + 1] != target[i])
                {
                    k = next[k];
                }
                if (target[k + 1] == target[i])
                {
                    k = k + 1;
                }
                next[i] = k;
            }
            return next;
        }
        public int KMPSearch(string target, string source)
        {
            int[] next = this.getNext(target);
            int k = next[0];
            for (int i = 0; i < source.Length; i++)
            {
                while (k > -1 && target[k + 1] != source[i])
                {
                    k = next[k];
                }
                if (target[k + 1] == source[i])
                {
                    k = k + 1;
                }
                if (k == target.Length - 1)
                {
                    return i - target.Length + 1;
                }
            }
            return -1;
        }

        public string MergeMaxOverlapString(List<string> lMax)
        {
            return lMax[1] + (this.RemoveMaxOverlapString(lMax[0], lMax[2]));
        }

        public string RemoveMaxOverlapString(string str, string maxStr)
        {
            return maxStr == "" ? str : str.Substring(maxStr.Length, str.Length - maxStr.Length);
        }
    }
    public class ReconstructFragments: IReconstructFragments
    {
        private IReconstructFragmentsTools _ReconstructFragmentsTools;
        /// <summary>
        /// store the maximum weight of reconstruction and the reconstruct sentence.
        /// </summary>
        private List<string> _lResult = new List<string>() { "", "0" };
        public ReconstructFragments(IReconstructFragmentsTools ReconstructFragmentsTools)
        {
            this._ReconstructFragmentsTools = ReconstructFragmentsTools;
        }
        /// <summary>
        /// reconstruction sentence that has the maximum weight path 
        /// </summary>
        /// <param name="OringialList">original list</param>
        /// <returns>reconstruction sentence</returns>
        public string ReconstructFragmentSentences(List<string> OringialList)
        {

            List<List<string>> lMax = new List<List<string>>();
            List<string> initL = new List<string>() { "", "", "" };
            lMax.Add(initL);
            List<string> ltempResult = new List<string>() { "", "0" };
            List<string> l = OringialList;

            this.DfsReconstructFragment(l, lMax, ltempResult);
            return _lResult[0];
        }
        /// <summary>
        /// Depth-first search the maximum weight path
        /// </summary>
        /// <param name="OriginalList">original list</param>
        /// <param name="lMax">the store of all the str1,str2 and their overlap string which have the same length</param>
        /// <param name="ltempResult">the maximum weight (sum of overlap length) of reconstruction </param>
        private void DfsReconstructFragment(List<string> OriginalList, List<List<string>> lMax, List<string> ltempResult)
        {
            if (OriginalList.Count == 1)
            {
                ltempResult[0] = OriginalList[0];
                if (int.Parse(ltempResult[1]) > int.Parse(_lResult[1]))
                {
                    _lResult[0] = ltempResult[0];
                    _lResult[1] = ltempResult[1];
                }
                return;
            }
            lMax = this.GetAllMaxOverlapString(OriginalList, ltempResult);
            OriginalList = _ReconstructFragmentsTools.CopyOList(OriginalList);
            ltempResult = _ReconstructFragmentsTools.CopyOList(ltempResult);
            for (int i = 0; i < lMax.Count; i++)
            {
                OriginalList.Remove(lMax[i][0]);
                OriginalList.Remove(lMax[i][1]);
                OriginalList.Add(_ReconstructFragmentsTools.MergeMaxOverlapString(lMax[i]));
                DfsReconstructFragment(OriginalList, lMax, ltempResult);

                OriginalList.Add(lMax[i][0]);
                OriginalList.Add(lMax[i][1]);
                OriginalList.Remove(_ReconstructFragmentsTools.MergeMaxOverlapString(lMax[i]));

            }

        }
        /// <summary>
        /// store the max length of overlap part of string and original strings and the weight of current reconstruct.
        /// </summary>
        /// <param name="str1">original string</param>
        /// <param name="str2">original string</param>
        /// <param name="maxStr">overlap string and order is str2+str1</param>
        /// <param name="lMax">store str1,str2 and maxStr</param>
        /// <param name="ltempResult">store the weight of current path</param>
        private void SetMaxOverlapString(string str1, string str2, string maxStr, List<List<string>> lMax, List<string> ltempResult)
        {
            List<string> lMaxtemp;
            if (maxStr == "" && lMax[0][2] == "")
            {
                lMax.Clear();
                lMaxtemp = new List<string>() { str1, str2, maxStr };
                lMax.Add(lMaxtemp);
            }
            else
            if (maxStr.Length > lMax[0][2].Length)
            {
                lMax.Clear();
                lMaxtemp = new List<string>() { str1, str2, maxStr };
                lMax.Add(lMaxtemp);
                ltempResult[1] = (int.Parse(ltempResult[1]) + maxStr.Length).ToString();
            }
            else
            if (maxStr.Length == lMax[0][2].Length)
            {
                lMaxtemp = new List<string>() { str1, str2, maxStr };
                lMax.Add(lMaxtemp);
            }

        }
        /// <summary>
        /// Integration of some functions including KMP,GetOneMaxOverlapString,SetMaxOverlapString to process two strings.
        /// Store all the little frames in a collect.
        /// </summary>
        /// <param name="str1">original string</param>
        /// <param name="str2">original string</param>
        /// <param name="lMax">store str1,str2 and maxStr</param>
        /// <param name="sRedun">Store all the little frames </param>
        /// <param name="ltempResult">store the weight of current path</param>
        private void GetMaxOverlapString(string str1, string str2, List<List<string>> lMax, HashSet<string> sRedun, List<string> ltempResult)
        {
            string maxStr = str1.Length > str2.Length ? str1 : str2;
            string minStr = maxStr == str1 ? str2 : str1;
            if (_ReconstructFragmentsTools.KMPSearch(minStr, maxStr) > -1)
            {
                sRedun.Add(minStr);
            }
            else
            {

                string FrontSubStr = _ReconstructFragmentsTools.GetOneMaxOverlapString(maxStr, minStr);
                string BackSubStr = _ReconstructFragmentsTools.GetOneMaxOverlapString(minStr, maxStr);
                if (FrontSubStr.Length > BackSubStr.Length)
                {
                    this.SetMaxOverlapString(maxStr, minStr, FrontSubStr, lMax, ltempResult);
                }
                else
                {
                    this.SetMaxOverlapString(minStr, maxStr, BackSubStr, lMax, ltempResult);
                }
            }

        }
        /// <summary>
        /// Integrate the function of GetMaxOverlapString to get all the max length of overlap strings.
        /// Remove the little frames as well
        /// </summary>
        /// <param name="OriginalList">the original list</param>
        /// <param name="ltempResult">store the weight of pathes</param>
        /// <returns>the store of all the str1,str2 and maxStr which have the same length</returns>
        private List<List<string>> GetAllMaxOverlapString(List<string> OriginalList, List<string> ltempResult)
        {
            List<List<string>> lMax = new List<List<string>>();
            List<string> initL = new List<string>() { "", "", "" };
            lMax.Add(initL);
            HashSet<string> sRedun = new HashSet<string>();
            List<string> lMaxOverlap = new List<string>();
            for (int i = 0; i < OriginalList.Count - 1; i++)
                for (int j = i + 1; j < OriginalList.Count; j++)
                {
                    this.GetMaxOverlapString(OriginalList[i], OriginalList[j], lMax, sRedun, ltempResult);
                }
            foreach (string sItem in sRedun)
            {
                OriginalList.Remove(sItem);
            }
            return lMax;
        }

        static void Main(string[] args)
        {
            List<string> l = new List<string>();
            IReadFragmentFiles _readFragmentFiles = new ReadFragmentFiles();
            IReconstructFragmentsTools reconstructFragmentsTools = new ReconstructFragmentsTools();
            //l = _readFragmentFiles.ReadFragmentsFromFile(@"..\..\fragments\fragments.txt");
            l = "m quaerat voluptatem.;pora incidunt ut labore et d;, consectetur, adipisci velit;olore magnam aliqua;idunt ut labore et dolore magn;uptatem.;i dolorem ipsum qu;iquam quaerat vol;psum quia dolor sit amet, consectetur, a;ia dolor sit amet, conse;squam est, qui do;Neque porro quisquam est, qu;aerat voluptatem.;m eius modi tem;Neque porro qui;, sed quia non numquam ei;lorem ipsum quia dolor sit amet;ctetur, adipisci velit, sed quia non numq;unt ut labore et dolore magnam aliquam qu;dipisci velit, sed quia non numqua;us modi tempora incid;Neque porro quisquam est, qui dolorem i;uam eius modi tem;pora inc;am al"
            //l= "Neque porro quisquam est, qu;qui dolorem i;i dolorem ipsum qu"
    .Split(';')
    .ToList();
            IReconstructFragments reconstructFragments = new ReconstructFragments(reconstructFragmentsTools);           


            Console.WriteLine(reconstructFragments.ReconstructFragmentSentences(l));
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}

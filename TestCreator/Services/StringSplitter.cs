using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator.Services
{
    public class StringSplitter
    {
        public static List<string> FindVars(string text)
        {

            var openFlag = false;
            var lst = new List<string>();
            foreach (char i in text)
            {
                if (openFlag)
                {
                    if (i == '{')
                        throw new Exception("Wrong Brackets");
                    if (i == '}')
                        openFlag = false;
                    else
                        lst[lst.Count - 1] += i;
                }
                else
                {
                    if (i == '{')
                    {
                        openFlag = true;
                        lst.Add("");
                    }
                }
            }
            lst.RemoveAll(s => s.Trim() == "");

            return lst.Distinct().ToList();
        }
    }
}

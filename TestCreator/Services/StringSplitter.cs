﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static bool CheckVarValue(string value) 
        {
            var spaces = value.Split(';');
            foreach(string s in spaces) 
            {
                if (s.Contains('/')) 
                {
                    var borders = s.Split('/');
                    if(borders.Length != 2)
                        return false;
                    if (borders[0].All(char.IsDigit) == false || borders[1].All(char.IsDigit) == false)
                        return false;
                }
            }
            return true;
        }
    }
}

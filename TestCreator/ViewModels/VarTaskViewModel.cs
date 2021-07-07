using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Sevices;

namespace TestCreator.ViewModels
{
    public class VarTaskViewModel : TaskBaseViewModel
    {
        private string text = "";
        public ObservableCollection<Variable> Vars { get; set; } = new ObservableCollection<Variable>();
        public string TaskText 
        {
            get => text;
            set 
            {
                text = value;
                try
                {
                    var _stringvars = StringSplitter.FindVars(value);
                    Vars.Clear();
                    foreach (string s in _stringvars)
                    {
                        foreach (Variable v in Vars)
                        {
                            if (v.Name == s)
                            {
                                Vars.Add(v);
                                goto skip;
                            }
                        }
                        Vars.Add(new Variable() { Name = s, Value = "0/10" });
                    skip:
                        continue;
                    }
                }
                catch (Exception)
                {
                    Vars.Clear();
                }
            }
        }

    }
}

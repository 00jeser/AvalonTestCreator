using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Services;

namespace TestCreator.ViewModels
{
    public class VarTaskViewModel : TaskBaseViewModel
    {
        [JsonProperty("TaskText")]
        private string text = "";
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
                        Vars.Add(new Variable() { Name = s, Value = "0/10" });
                    }
                }
                catch (Exception)
                {
                    Vars.Clear();
                }
            }
        }
        [JsonProperty("Vars")]
        public ObservableCollection<Variable> Vars { get; set; } = new ObservableCollection<Variable>();


        private string message = "";
        public string WrongMessage
        {
            get
            {
                return message;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref message, value);
                base.BaseWrongMessage = value;
            }
        }
        public void SetMessage()
        {
            foreach(var v in Vars)
                if (!StringSplitter.CheckVarValue(v.Value)) 
                {
                    WrongMessage = $"Неверное значение переменной {v.Name}";
                    return;
                }
            WrongMessage = "";
        }

        public override TaskBaseViewModel Clone()
        {
            VarTaskViewModel rez = new();

            rez.TaskText = TaskText;
            rez.Vars = new ObservableCollection<Variable>();
            foreach(var i in Vars) 
            {
                rez.Vars.Add(new Variable() { Name = i.Name, Value = i.Value });
            }

            return rez;
        }

    }
}

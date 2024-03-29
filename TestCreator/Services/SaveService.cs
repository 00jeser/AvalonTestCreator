﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.ViewModels;
using Newtonsoft.Json;
using System.IO;

namespace TestCreator.Services
{
    public class SaveObject 
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }
    class SaveService
    {
        public static void Save(ObservableCollection<TaskBaseViewModel> Items, string Path) 
        {
            var rez = new List<SaveObject>();
            foreach(TaskBaseViewModel task in Items)
            {
                if (task is FewTaskViewModel)
                    rez.Add(new SaveObject() { Type = "Few", Content = JsonConvert.SerializeObject(task as FewTaskViewModel) });
                if (task is SelectTaskViewModel)
                    rez.Add(new SaveObject() { Type = "Select", Content = JsonConvert.SerializeObject(task as SelectTaskViewModel) });
                if (task is VarTaskViewModel)
                    rez.Add(new SaveObject() { Type = "Var", Content = JsonConvert.SerializeObject(task as VarTaskViewModel) });
            }
            File.WriteAllText(Path, JsonConvert.SerializeObject(rez));
        }
        public static ObservableCollection<TaskBaseViewModel> Load(string Path) 
        {
            var obj = new ObservableCollection<TaskBaseViewModel>();
            foreach(var o in JsonConvert.DeserializeObject<List<SaveObject>>(File.ReadAllText(Path)))
                switch (o.Type)
                {
                    case "Few":
                        var t1 = JsonConvert.DeserializeObject<FewTaskViewModel>(o.Content);
                        t1.SetMessage();
                        obj.Add(t1);
                        break;
                    case "Select":
                        var t2 = JsonConvert.DeserializeObject<SelectTaskViewModel>(o.Content);
                        t2.SetMessage();
                        obj.Add(t2);
                        break;
                    case "Var":
                        var t3 = JsonConvert.DeserializeObject<VarTaskViewModel>(o.Content);
                        t3.SetMessage();
                        obj.Add(t3);
                        break;
                }
            return obj;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.ViewModels;

namespace TestCreator.Services
{
    public class RenderPipline
    {
        public enum RenderType
        {
            Text,
            Word,
            MD
        }
        static Random r = new Random();

        public static string Render(ObservableCollection<TaskBaseViewModel> vm, int amo, RenderType type)
        {
            string rez = "";
            for (int i = 1; i <= amo; i++)
            {
                if (type == RenderType.Text)
                    rez += $"Вариант {i}\n";
                else
                    rez += $"## Вариант {i}\n";
                foreach (TaskBaseViewModel task in vm)
                {
                    if (task is VarTaskViewModel)
                    {
                        rez += renderVarTask(task as VarTaskViewModel, type);
                    }
                    if (task is SelectTaskViewModel)
                    {
                        rez += renderSelectTask(task as SelectTaskViewModel, type);
                    }
                    if (task is FewTaskViewModel)
                    {
                        rez += renderFewTask(task as FewTaskViewModel, type);
                    }
                    rez += '\n';
                    rez += '\n';
                }
                rez += '\n';
            }
            return rez;
        }

        //-----------------------------------


        private static string renderVarTask(VarTaskViewModel task, RenderType type)
        {
            var rez = "";

            if (type == RenderType.Text)
                rez += $"{task.Name}. \n";
            else
                rez += $"#### *{task.Name}* \n";
            Dictionary<string, string> vars = new();

            foreach (Variable v in task.Vars)
            {
                vars["{" + v.Name + "}"] = renderVar(v);
            }
            string qest = task.TaskText;
            foreach (var k in vars.Keys)
            {
                qest = qest.Replace(k, vars[k]);
            }
            rez += qest;

            return rez;
        }
        private static string renderSelectTask(SelectTaskViewModel task, RenderType type)
        {
            string rez = "";

            if (type == RenderType.Text)
                rez += $"{task.Name}. \n";
            else
                rez += $"#### *{task.Name}* \n";
            var wrl = new List<Answer>(task.WrongAnswers).OrderBy(x => r.Next()).Take(3).Select(x => x.value).ToList();
            wrl.Add(task.TrueAnswer);
            wrl = wrl.OrderBy(x => r.Next()).ToList();
            rez += task.Question;
            rez += '\n';
            int n = 1;
            foreach (var a in wrl)
            {
                rez += $"{n}) {a}\n";
                n++;
            }

            return rez;
        }
        private static string renderFewTask(FewTaskViewModel task, RenderType type)
        {

            if (type == RenderType.Text)
                return $"{task.Name}. \n{task.Questions[r.Next(task.Questions.Count)].Text}";
            else
                return $"#### *{task.Name}* \n{task.Questions[r.Next(task.Questions.Count)].Text}";
        }

        public static string renderVar(Variable v)
        {
            var splces = v.Value.Split(';').Select(x => x.Trim());
            List<string> rezs = new();
            foreach (var s in splces)
            {
                if (s != string.Empty)
                {
                    if (s.Contains('/'))
                    {
                        rezs.Add(r.Next(int.Parse(s.Split('/')[0]), int.Parse(s.Split('/')[1]) + 1).ToString());
                    }
                    else
                    {
                        rezs.Add(s);
                    }
                }
            }
            return rezs[r.Next(rezs.Count)];
        }
    }
}

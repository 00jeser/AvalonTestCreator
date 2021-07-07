using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator.ViewModels
{
    public class SelectTaskViewModel : TaskBaseViewModel
    {
        public SelectTaskViewModel() 
        {
            DoAdd = ReactiveCommand.Create(RunAdd);
        }


        public string Question { get; set; }
        public ObservableCollection<string> WrongAnswers { get; set; }
        public string TrueAnswer { get; set; }
        public ReactiveCommand<Unit, Unit> DoAdd { get; }

        void RunAdd()
        {
            WrongAnswers.Add("");
        }
    }
}

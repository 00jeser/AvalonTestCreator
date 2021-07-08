using Newtonsoft.Json;
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
    public class Answer : ViewModelBase
    {
        [JsonProperty("v")]
        public string value { get; set; } = "";
    }
    public class SelectTaskViewModel : TaskBaseViewModel
    {
        public SelectTaskViewModel() 
        {
            DoAdd = ReactiveCommand.Create(RunAdd);
            DoRemove = ReactiveCommand.Create(RunRemove);
        }


        [JsonProperty("Question")]
        public string Question { get; set; }
        [JsonProperty("WrongAnswers")]
        public ObservableCollection<Answer> WrongAnswers { get; set; } = new ObservableCollection<Answer>();
        [JsonProperty("TrueAnswer")]
        public string TrueAnswer { get; set; }
        public ReactiveCommand<Unit, Unit> DoAdd { get; }
        public ReactiveCommand<Unit, Unit> DoRemove { get; }

        void RunAdd()
        {
            WrongAnswers.Add(new Answer());
        }
        private void RunRemove()
        {
            if (WrongAnswers.Count >= 1)
                WrongAnswers.RemoveAt(WrongAnswers.Count - 1);
        }
    }
}

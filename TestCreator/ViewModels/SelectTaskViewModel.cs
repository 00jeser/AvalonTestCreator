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
            SetMessage();
        }


        [JsonProperty("Question")]
        public string Question { get; set; }
        [JsonProperty("WrongAnswers")]
        public ObservableCollection<Answer> WrongAnswers { get; set; } = new ObservableCollection<Answer>();
        [JsonProperty("TrueAnswer")]
        public string TrueAnswer { get; set; }
        public ReactiveCommand<Unit, Unit> DoAdd { get; }
        public ReactiveCommand<Unit, Unit> DoRemove { get; }
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

        void RunAdd()
        {
            WrongAnswers.Add(new Answer());
            SetMessage();
        }
        private void RunRemove()
        {
            if (WrongAnswers.Count >= 1)
                WrongAnswers.RemoveAt(WrongAnswers.Count - 1);
            SetMessage();
        }
        public override TaskBaseViewModel Clone()
        {
            SelectTaskViewModel rez = new();
            foreach (var i in WrongAnswers)
                rez.WrongAnswers.Add(new Answer() { value = i.value});
            rez.TrueAnswer = TrueAnswer;
            rez.Question = Question;
            return rez;
        }

        public void SetMessage()
        {
            if (WrongAnswers.Count == 0)
                WrongMessage = "Необходим хотябы один неверный ответ";
            else
                WrongMessage = "";
        }
    }
}

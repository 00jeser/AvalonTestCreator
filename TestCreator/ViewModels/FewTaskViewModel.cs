using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestCreator.ViewModels
{
    public class FewTaskViewModel : TaskBaseViewModel
    {
        public FewTaskViewModel()
        {
            DoAdd = ReactiveCommand.Create(RunAdd);
            DoRemove = ReactiveCommand.Create(RunRemove);
            SetMessage();
        }


        [JsonProperty("q")]
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
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
        public ReactiveCommand<Unit, Unit> DoAdd { get; }
        public ReactiveCommand<Unit, Unit> DoRemove { get; }

        void RunAdd()
        {
            Questions.Add(new Question());
            SetMessage();
        }
        private void RunRemove()
        {
            if (Questions.Count >= 1)
                Questions.RemoveAt(Questions.Count - 1);
            SetMessage();
        }
        public override TaskBaseViewModel Clone()
        {
            FewTaskViewModel rez = new();
            foreach (var i in Questions)
                rez.Questions.Add(new Question() { Text = i.Text });
            rez.SetMessage();
            return rez;
        }
        public void SetMessage()
        {
            if (Questions.Count == 0)
                WrongMessage = "Необходимо хотябы одно задание";
            else
                WrongMessage = "";
        }
    }
}

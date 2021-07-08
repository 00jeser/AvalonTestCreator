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
        }


        [JsonProperty("q")]
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
        public ReactiveCommand<Unit, Unit> DoAdd { get; }
        public ReactiveCommand<Unit, Unit> DoRemove { get; }

        void RunAdd()
        {
            Questions.Add(new Question());
        }
        private void RunRemove()
        {
            if (Questions.Count >= 1)
                Questions.RemoveAt(Questions.Count - 1);
        }
    }
}

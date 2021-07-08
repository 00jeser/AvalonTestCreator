using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestCreator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<TaskBaseViewModel> Items { get; set; }
        public MainWindowViewModel() 
        {
            Items = new ObservableCollection<TaskBaseViewModel>()
            {
                new VarTaskViewModel()
                {
                    Name="1",
                    TaskText = "Task{a}"
                },
                new VarTaskViewModel()
                {
                    Name="2",
                    TaskText = "Task{a}"
                },
                new VarTaskViewModel()
                {
                    Name="3",
                    TaskText = "Task{a}"
                },
                new SelectTaskViewModel()
                {
                    Name = "4",
                    Question = "1, 2, 3, 4, или 5",
                    TrueAnswer = "1",
                    WrongAnswers = new ObservableCollection<string>(){ "2", "3", "4", "5" }
                },
                new FewTaskViewModel()
                {
                    Name = "5",
                    Questions = new ObservableCollection<Question>()
                    {
                        new Question() {Text="„и? ƒа?" }, 
                        new Question() {Text="„и?" },
                        new Question() {Text="„и? »ли не чи?" }
                    }
                },
                new VarTaskViewModel()
                {
                    Name="1",
                    TaskText = "Task{a}"
                },
                new VarTaskViewModel()
                {
                    Name="2",
                    TaskText = "Task{a}"
                },
                new VarTaskViewModel()
                {
                    Name="3",
                    TaskText = "Task{a}"
                },
                new SelectTaskViewModel()
                {
                    Name = "4",
                    Question = "1, 2, 3, 4, или 5",
                    TrueAnswer = "1",
                    WrongAnswers = new ObservableCollection<string>(){ "2", "3", "4", "5" }
                },
                new FewTaskViewModel()
                {
                    Name = "5",
                    Questions = new ObservableCollection<Question>()
                    {
                        new Question()
                    }
                }
            };
        }

    }
}

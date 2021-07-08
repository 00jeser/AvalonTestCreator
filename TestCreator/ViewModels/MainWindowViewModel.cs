using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Newtonsoft.Json;
using Avalonia.Controls;
using System.IO;

namespace TestCreator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<TaskBaseViewModel> Items { get; set; }
        public TaskBaseViewModel Selected 
        { get;
            set; }
        public MainWindowViewModel() 
        {
            /*Items = new ObservableCollection<TaskBaseViewModel>()
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
            };*/
            Items = new ObservableCollection<TaskBaseViewModel>();

            DoDelete = ReactiveCommand.Create(RunDelete);
            DoAdd = ReactiveCommand.Create<string>(RunAdd);
            DoSave = ReactiveCommand.Create(RunSave);
            DoOpen = ReactiveCommand.Create(RunOpen);
        }


        public ReactiveCommand<string, Unit> DoAdd { get; }

        private void RunAdd(string obj)
        {
            switch (obj)
            {
                case "Few":
                    Items.Add(new FewTaskViewModel());
                    break;
                case "Select":
                    Items.Add(new SelectTaskViewModel());
                    break;
                case "Var":
                    Items.Add(new VarTaskViewModel());
                    break;

            }
            for(int i = 1; i <= Items.Count; i++) 
            {
                Items[i - 1].Name = i.ToString();
            }
        }
        public ReactiveCommand<Unit, Unit> DoDelete { get; }
        private void RunDelete()
        {
            if (Selected != null)
                Items.Remove(Selected);
            for (int i = 1; i <= Items.Count; i++)
            {
                Items[i - 1].Name = i.ToString();
            }
        }
        public ReactiveCommand<Unit, Unit> DoSave { get; }
        private async void RunSave()
        {
            var dialog = new SaveFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Test Creator", Extensions = { "tc" } });

            var result = await dialog.ShowAsync(Services.Singleton.MainWindow);
            Services.SaveService.Save(Items, result);
        }
        public ReactiveCommand<Unit, Unit> DoOpen { get; }
        private async void RunOpen()
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Test Creator", Extensions = { "tc" } });

            var result = await dialog.ShowAsync(Services.Singleton.MainWindow);
            Items.Clear();
            foreach (var t in Services.SaveService.Load(result[0])) 
            {
                Items.Add(t);
            }
        }
    }
}

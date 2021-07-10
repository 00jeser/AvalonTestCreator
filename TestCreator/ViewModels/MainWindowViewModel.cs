using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Newtonsoft.Json;
using Avalonia.Controls;
using System.IO;
using TestCreator.Services;

namespace TestCreator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public int NumberOfVariant { get; set; } = 5;
        private bool _showExport;
        public bool ShowExport
        {
            get => _showExport;
            set => this.RaiseAndSetIfChanged(ref _showExport, value);
        }
        private bool _showResult;
        public bool ShowResult
        {
            get => _showResult;
            set => this.RaiseAndSetIfChanged(ref _showResult, value);
        }
        private string _result;
        public string Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }

        public ObservableCollection<TaskBaseViewModel> Items { get; set; }
        public TaskBaseViewModel Selected
        {
            get;
            set;
        }
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<TaskBaseViewModel>();

            DoDelete = ReactiveCommand.Create(RunDelete);
            DoAdd = ReactiveCommand.Create<string>(RunAdd);
            DoSave = ReactiveCommand.Create(RunSave);
            DoOpen = ReactiveCommand.Create(RunOpen);
            DoExport = ReactiveCommand.Create<string>(RunExport);
            DoShow = ReactiveCommand.Create(RunShow);
            DoClose = ReactiveCommand.Create(RunClose);
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
            Rename();
        }
        public ReactiveCommand<Unit, Unit> DoDelete { get; }
        private void RunDelete()
        {
            if (Selected != null)
                Items.Remove(Selected);
            Rename();
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
        public ReactiveCommand<string, Unit> DoExport { get; }
        private async void RunExport(string type)
        {
            string e;
            switch (type)
            {
                case "txt":
                    e = RenderPipline.Render(Items, NumberOfVariant, RenderPipline.RenderType.Text);
                    ShowResult = true;
                    Result = e;
                    break;
                case "word":
                    e = RenderPipline.Render(Items, NumberOfVariant, RenderPipline.RenderType.Word);
                    // TODO сделать экспорт в Word через Microsoft.Office.Interpop.Word
                    break;
                case "md":
                    e = RenderPipline.Render(Items, NumberOfVariant, RenderPipline.RenderType.MD);
                    ShowResult = true;
                    Result = e;
                    break;
            }
        }
        public ReactiveCommand<Unit, Unit> DoShow { get; }
        private async void RunShow()
        {
            foreach (var i in Items)
            {
                if (i is VarTaskViewModel)
                {
                    (i as VarTaskViewModel).SetMessage();
                    if ((i as VarTaskViewModel).WrongMessage != "")
                        return;
                }
                else if (i.BaseWrongMessage != "")
                    return;
            }
            ShowExport = !ShowExport;
        }
        public ReactiveCommand<Unit, Unit> DoClose { get; }
        private async void RunClose()
        {
            ShowExport = false;
            ShowResult = false;
            Result = "";
        }

        public void Rename()
        {
            for (int i = 1; i <= Items.Count; i++)
            {
                Items[i - 1].Name = i.ToString();
            }
        }
    }
}

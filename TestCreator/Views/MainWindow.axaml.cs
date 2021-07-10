using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TestCreator.ViewModels;

namespace TestCreator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Services.Singleton.MainWindow = this;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ClickDelete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).DoDelete.Execute();
        }
        private void ClickDublicate(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).Items.Add((DataContext as MainWindowViewModel).Selected.Clone());
            (DataContext as MainWindowViewModel).Rename();
        }

    }
}

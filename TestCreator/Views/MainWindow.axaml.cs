using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
    }
}

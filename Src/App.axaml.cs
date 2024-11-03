using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Integra7AuralAlchemist.ViewModels;
using Integra7AuralAlchemist.Views;

namespace Integra7AuralAlchemist;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            var vm = new MainWindowViewModel();
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
            var mw = desktop.MainWindow as MainWindow;
            mw.ViewModel = vm;
            mw.RegisterDialogHandler();
            _ = vm.InitializeAsync();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
using Avalonia.Controls;
using DirStream.App.ViewModels;

namespace DirStream.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
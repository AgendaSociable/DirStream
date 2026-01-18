using System.ComponentModel;
using System.Collections.ObjectModel;
using DirStream.App.Models;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using System.Runtime.CompilerServices;



namespace DirStream.App.ViewModels;

public partial class MainWindowViewModel : INotifyPropertyChanged
{
    // Collection to hold media items
    public ObservableCollection<MediaItem> MediaItems { get; } = new();

    public void ScanFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            return;

        MediaItems.Clear();

        var extensions = new[] { ".mp3", ".wav", ".mp4", ".mkv", ".avi" };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var ext = Path.GetExtension(file).ToLowerInvariant();

            if (extensions.Contains(ext))
            {
                MediaItems.Add(new MediaItem(file));
            }
        }
    }

    private MediaItem? _selectedMedia;

    public MediaItem? SelectedMedia
    {
        get => _selectedMedia;
        set
        {
            if (_selectedMedia != value)
            {
                _selectedMedia = value;
                OnPropertyChanged();
                PlayCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public RelayCommand PlayCommand { get; }



    private void Play()
    {
        if (SelectedMedia == null)
            return;

        var path = SelectedMedia.FullPath;

        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true
        };

        process.Start();
    }



    // Constructor
    public MainWindowViewModel()
    {
        ScanFolder("/home/nael/music_video");
        PlayCommand = new RelayCommand(
            Play,
            () => SelectedMedia != null
        );

    }



    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

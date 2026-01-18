using System.IO;

namespace DirStream.App.Models;

public class MediaItem
{
    public string FileName { get; }
    public string FullPath { get; }

    public MediaItem(string fullPath)
    {
        FullPath = fullPath;
        FileName = Path.GetFileName(fullPath);
    }
}
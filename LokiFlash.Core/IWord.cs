using System.IO;

namespace LokiFlash.Core
{
    public interface IWord
    {
        string Display { get; set; }
        FileInfo PicturePath { get; set; }
        bool HasPicture { get; }
    }
}
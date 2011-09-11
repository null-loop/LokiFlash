using System.IO;

namespace LokiFlash.Core
{
    public class Word : IWord
    {
        public Word(string display, FileInfo picturePath)
        {
            Display = display;
            PicturePath = picturePath;
        }

        public Word(string display)
        {
            Display = display;
            PicturePath = null;
        }

        public string Display { get; set; }

        public FileInfo PicturePath { get; set; }

        public bool HasPicture
        {
            get { return PicturePath != null; }
        }
    }
}
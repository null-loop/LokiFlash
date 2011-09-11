using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LokiFlash.Core
{
    public class Words: IWordsList
    {
        public static IWordsList FromStrings(string listName, params string[] words)
        {
            return new Words(listName, words.Select(w => new Word(w)));
        }

        public static IWordsList FromPictureFiles(string listName, DirectoryInfo directory)
        {
            var files = directory.GetFiles().Where(f => f.Extension == ".jpg" || f.Extension == ".png").ToArray();
            
            return new Words(listName, files.Select(f=>new Word(GetWordFromFileName(f),f)));
        }

        public static string GetWordFromFileName(FileInfo file)
        {
            var w = Path.GetFileNameWithoutExtension(file.FullName);
            w = w.Replace("_", " ");
            return w.ToLower();
        }

        public Words(string name, IEnumerable<IWord> allItems)
        {
            Name = name;
            AllItems = allItems;
        }

        public string Name { get; private set; }

        public IEnumerable<IWord> AllItems { get; private set; }
    }
}
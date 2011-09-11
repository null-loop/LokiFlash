using System.Collections.Generic;
using System.IO;
using LokiFlash.Core;

namespace LokiFlash.wordlists
{
    public class BuiltInLists
    {

        public static IWordsList LokiFirst
        {
            get
            {
                return Words.FromStrings("Lokis First School Word List","is", "it", "in", "at", "and", "to", "the", "no", "go", "I", "he", "she", "we", "me", "be", "was", "my", "am", "went", "of", "see", "yes",
                                         "on", "cat", "dog", "big", "mum", "dad", "get", "can", "you", "they", "her", "look");
            }
        }

        public static IWordsList DefaultPictures
        {
            get { return Words.FromPictureFiles("Default Pictures", new DirectoryInfo("Pictures")); }
        }
    }


}

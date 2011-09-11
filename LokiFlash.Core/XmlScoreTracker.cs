using System;
using System.IO;
using System.Xml.Linq;

namespace LokiFlash.Core
{
    public class XmlScoreTracker : IScoreTracker
    {
        private FileInfo _xmlFilePath;
        private XDocument _resultsDocument;

        public void RecordGameResult(string type, string word, int attempts)
        {
            _resultsDocument.Root.Add(new XElement("GameResult", new XAttribute("GameType", type), new XAttribute("Word", word), new XAttribute("attempts", attempts)));

            Save();
        }

        public void StartSession()
        {
            var fileName = string.Format("Results_{0:ddMMyyyy}_{0:hhmmss}.xml", DateTime.Now);
            _xmlFilePath = new FileInfo(fileName);
            _resultsDocument = new XDocument(new XElement("Session", new XAttribute("Start", DateTime.Now)));

            Save();
        }

        private void Save()
        {
            _resultsDocument.Save(_xmlFilePath.FullName);
        }

        public void EndSession()
        {
            _resultsDocument.Root.Add(new XAttribute("End",DateTime.Now));

            Save();
        }
    }
}

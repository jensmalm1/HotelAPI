using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Domain
{
    public class Parser
    {
        private readonly AppConfiguration _appConfiguration;

        public Parser(AppConfiguration appConfiguration)
        {
            _appConfiguration = new AppConfiguration();

        }

        public List<string> SplitStringByLines(string textFile) =>
            textFile.Split('\n').ToList();

        public List<string> SortJsonFilesByDate() =>
            Directory.GetFiles(_appConfiguration.ImportPath, "*.json").OrderByDescending(x => x).ToList();

        public List<string> SortTextFilesByDate() =>
            Directory.GetFiles(_appConfiguration.ImportPath, "*.txt").OrderByDescending(x => x).ToList();
    }
}

using CovidTracker.Azure.Function.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CovidTracker.Azure.Function.Utility
{
    public class PathUtility
    {
        public IEnumerable<string> GetDirectories(string path)
        {
            return new List<string>(Directory.GetDirectories(path));
        }

        public string FindFile(string directoryPath, string fileName)
        {
            return Directory.GetFiles(directoryPath)
                .FirstOrDefault(x => Path.GetFileNameWithoutExtension(x) == fileName);
        }
    }
}

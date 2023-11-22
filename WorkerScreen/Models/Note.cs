using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WorkerScreen.Models
{
    internal class Note
    {
        public string FileName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public void Save() =>
        File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName), Text);
        public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName));
    }



}

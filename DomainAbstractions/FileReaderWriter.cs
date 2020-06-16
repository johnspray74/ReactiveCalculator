using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Libraries;
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// 
    /// </summary>
    public class FileReaderWriter : IDataFlow<string>, IEvent
    {
        // Public fields and properties
        public string InstanceName = "Default";

        public bool WatchFile
        {
            get => watcher.EnableRaisingEvents;
            set => watcher.EnableRaisingEvents = value;
        }

        // Private fields
        private string fullPath = "";
        private string textContent = "";
        private FileSystemWatcher watcher = new FileSystemWatcher();

        // Ports
        private IDataFlow<string> textContentOutput;

        public FileReaderWriter(string url)
        {
            fullPath = url;

            watcher.Path = Path.GetDirectoryName(fullPath);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = Path.GetFileName(fullPath);
            watcher.Changed += (sender, args) =>
            {
                try
                {
                    var contents = File.ReadAllText(fullPath);
                    if (contents != textContent)
                    {
                        textContent = contents;
                        Push(contents);
                    }
                }
                catch (IOException)
                {
                }
            };
        }

        private void WriteFileContents(string contents)
        {
            textContent = contents;

            if (!File.Exists(fullPath)) File.WriteAllText(fullPath, "");

            File.WriteAllText(fullPath, contents);
        }

        private void Push(string output)
        {
            if (textContentOutput != null) textContentOutput.Data = output;
        }

        // IDataFlow<string> implementation
        string IDataFlow<string>.Data
        {
            get => textContent;
            set
            {
                WriteFileContents(value);
            }
        }

        // IEvent implementation
        void IEvent.Execute()
        {
            WriteFileContents("");
        }
    }
}

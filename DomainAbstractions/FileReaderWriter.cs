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
        // Properties
        public string InstanceName { get; set; } = "Default";

        public bool WatchFile
        {
            get => watcher.EnableRaisingEvents;
            set => watcher.EnableRaisingEvents = value;
        }

        // Ports
        // IDataFlow<string> input
        // IEvent<string> clear
        private IDataFlow<string> output;
        private IBidirectionalDataflow<string> inputOutput;

        // Private fields
        private string fullPath = "";
        private string textContent = "";
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public FileReaderWriter(string url)
        {
            fullPath = url;

            watcher.Path = Path.GetDirectoryName(fullPath);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = Path.GetFileName(fullPath);
            watcher.Changed += (sender, args) =>
            {
                ReadFile();
            };
        }

        private void WriteFileContents(string contents)
        {
            textContent = contents;

            if (!File.Exists(fullPath)) File.WriteAllText(fullPath, "");

            File.WriteAllText(fullPath, contents);
        }

        private void ReadFile()
        {
            try
            {
                var contents = File.ReadAllText(fullPath);
                if (contents != textContent)
                {
                    textContent = contents;
                    if (output != null) output.Data = contents;
                    if (inputOutput != null) inputOutput.APushToB(contents);
                }
            }
            catch (IOException)
            {
            }
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
            // WriteFileContents("");
            ReadFile();
        }


        private void inputOutputPostWiringInitialize()
        {
            inputOutput.BPushToA += inputOutputChanged;

        }

        private void inputOutputChanged(object sender, string data)
        {
            WriteFileContents(data);
        }



    }
}

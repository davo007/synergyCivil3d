using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptTest
{
    public class CommandLineWrapper
    {
        private StreamWriter _input_writer;

        private static StringBuilder _buffered_output = new StringBuilder();
        private static ConcurrentQueue<string> _queued_outputs = new ConcurrentQueue<string>();

        public bool ShowAllOutputData { get; set; }

        public CommandLineWrapper()
        {
            ShowAllOutputData = true;
        }

        /// <summary>
        /// Connect to the server
        /// </summary>
        /// <param name="server"></param>
        public void Connect(string server)
        {

            string cmd_line = System.IO.Path.Combine(Environment.GetEnvironmentVariable("12dSynergyInstallationPath_x86"), "12dSynergyCmdline.exe");

            if (!System.IO.File.Exists(cmd_line))
                throw new FileNotFoundException("Unable to find the command line client at " + cmd_line);

            System.Diagnostics.Process p = new Process();
            p.StartInfo = new ProcessStartInfo();
            p.StartInfo.Arguments = string.Format("script -server {0} -mode interactive", server);
            p.StartInfo.FileName = cmd_line;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            _input_writer = p.StandardInput;

            var reader = new ConsoleReader(p.StandardOutput);
            reader.StandardInputRead += InputDataRead;
            reader.StartRead();

            // read up until the banner
            ReadBanner();
        }

        /// <summary>
        /// Handle some data being read
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="console_input_read_event_args"></param>
        private void InputDataRead(object sender, ConsoleInputReadEventArgs console_input_read_event_args)
        {
            lock (_buffered_output)
            {
                string input = console_input_read_event_args.Input;

                int idx = input.IndexOf("CMD>");
                if (idx < 0)
                {
                    if (ShowAllOutputData)
                        Console.Write(input);

                    _buffered_output.Append(console_input_read_event_args.Input);
                    return;
                }
                else if (idx != 0)
                {

                    string temp = input.Substring(0, idx);

                    if (ShowAllOutputData)
                        Console.Write(temp);

                    lock (_buffered_output)
                    {
                        _buffered_output.Append(temp);
                    }


                }

                string all_data = "";
                lock (_buffered_output)
                {
                    all_data = _buffered_output.ToString();
                }

                lock (_queued_outputs)
                {
                    _queued_outputs.Enqueue(all_data);
                }

                // clear and add anything left over
                lock (_buffered_output)
                {
                    _buffered_output.Clear();
                    if (idx < input.Length - 4)
                    {
                        var remainder = input.Substring(idx + 4);

                        if (ShowAllOutputData)
                            Console.Write(remainder);

                        _buffered_output.Append(remainder);
                    }

                }

            }
        }

        /// <summary>
        /// Execute a command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="input_writer"></param>
        /// <param name="wait_for_output"></param>
        /// <returns></returns>
        public string ExecuteCommand(string command, bool wait_for_output = true)
        {

            _input_writer.WriteLine(command);

            if (!wait_for_output)
                return "";

            return ReadData();

        }





        /// <summary>
        /// Get any pending output data
        /// </summary>
        /// <returns></returns>
        public string ReadData()
        {
            // wait till we get a cmd output
            string res;

            while (_queued_outputs.IsEmpty || !_queued_outputs.TryDequeue(out res))
            {
                System.Threading.Thread.Sleep(10);
            }


            return res;


        }

        public void Quit()
        {
            ExecuteCommand("Quit", false);
        }

        /// <summary>
        /// Read till the first input line
        /// </summary>
        /// <returns></returns>
        private string ReadBanner()
        {
            return ReadData();
        }

    }
}

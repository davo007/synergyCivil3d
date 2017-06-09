using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ScriptTest
{
    class Program
    {
        private static CommandLineWrapper _wrapper;


        static void Main(string[] args)
        {
            
            // start the command line wrapper
            _wrapper = new CommandLineWrapper();
            _wrapper.ShowAllOutputData = true;
            _wrapper.Connect("ASJWP04");

            // do some stuff

            string jobName = "110274 - 01 - Synergy Test Project 2";

            string [] data = GetAllJobItems(jobName);

            foreach (string a in data)
            {
                Debug.Write(a + "\n");
                //Console.WriteLine(a.ToString());
            }

            _wrapper.Quit();


        }

        /// <summary>
        /// Get all job items
        /// </summary>
        /// <param name="job"></param>
        /// <param name="input_writer"></param>
        /// <returns></returns>
        private static string[] GetAllJobItems(string job)
        {
            //string output = _wrapper.ExecuteCommand(string.Format("list -job \"{0}\"", job));
            string output = _wrapper.ExecuteCommand(string.Format("list", job));


            return output.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            //return null;
        }

        private static string[] GetAllFolderItems(string drgPath)
        {


            string output = _wrapper.ExecuteCommand(string.Format("list -job \"{0}\" -path \"{1}\"" , drgPath));


            return output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //return null;
        }




    }

   
}

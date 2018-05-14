/*
 * Written by: Nicholas Cockcroft
 * Date: March 29, 2018
 * Course: .NET Environment
 * Assignment: Lab 10
 * 
 * Description: Write a program that will take a series program names as its command line arguments.
 * Your goal is to make sure that these programs are always executing.  So, if they die, 
 * your program will restart them.  This is slightly trickier and uglier than UNIX.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Lab10
{
    class lab10
    {
        static void Main(string[] args)
        {
            // Create an array of 100 Process objects
            Process[] Proc = new Process[100];

            // Intialize the Process objects to be new processes
            for(int i = 0; i < Proc.Length; i++)
            {
                Proc[i] = new Process();
            }

            // Making sure the command line arguments aren't bigger than the Process array
            if(args.Length > 100)
            {
                Console.WriteLine("Too many processes at once.");
                return;
            }

            // If any of the command line arguments don't exist, write an error message and return
            for (int i = 0; i < args.Length; i++)
            {
                if (!File.Exists(args[i]))
                {
                    Console.WriteLine("One or more files do not exist!");
                    return;
                }
            }

            // Getting through error checking, assign each process object to a command line argument
            for (int j = 0; j < args.Length; j++)
            {
                Proc[j].StartInfo.FileName = args[j];
                Proc[j].Start();
            }

            // Always want to have these processes running so while true...
            while (true)
            {
                // If any of the processes have exited, then we restart them
                for (int i = 0; i < args.Length; i++)
                {
                    if (Proc[i].HasExited)
                    {
                        Proc[i].Start();
                    }
                }
                // Sleep for 5 second that way we aren't continuously running a while loop
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}

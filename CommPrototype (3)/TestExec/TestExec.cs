///////////////////////////////////////////////////////////////
// TestExec.cs: Test Executive package                       //
// ver 1.0                                                   //
// CSE681 - Software Modeling and Analysis, Project #4       //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
*  Package Operations
* --------------------
* This Package demonstrates the requirements.
*/
/*
* Maintainence
* -----------------
*                 
*  Build Process:devenv Project4code.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*/
/*
* Public Interface
* -------------------
*  WPF()
*- launch the WPF
*  Server()
* - the server is launched
* client()
*- the clinet is launched
* readers()
* - no of readers
* writers()
*- no of writers
*
*/
/*
 * Maintenance History:
 * --------------------
 * ver 1.0: 20 Nov 2015
 * - first release
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Project4Code
{
    class TestExecutive
    {
        private  int port_count = 3;

        // launch WPF
        public void WPF()
        {
            ProcessStartInfo wpf_client = new ProcessStartInfo(Path.GetFullPath("..\\..\\..\\WpfClient\\bin\\Debug\\WpfApplication1.exe"));
            Process.Start(wpf_client);
        }

        // launch server
        public void Server()
        {
            ProcessStartInfo server = new ProcessStartInfo(Path.GetFullPath("..\\..\\..\\Server\\bin\\Debug\\Server.exe"));
            Process.Start(server);
        }
        //launch sreaders
        public void Readers(string[] args)
        {
            int read_count = readers(args);
            string par_disp = "";
            if (processCommandLineForPartialLog(args))
                par_disp = " -Reader_Partial_Display";
            for (int i = 1; i <= read_count; i++)
            {
                ProcessStartInfo reader = new ProcessStartInfo(Path.GetFullPath("..\\..\\..\\Client2\\bin\\Debug\\Client2.exe"));
                reader.Arguments = "/L http://localhost:" + (8080 + port_count).ToString() + "/CommService" + par_disp;
                Process.Start(reader);
                ++port_count;
            }
        }

        //launch writers
        public void Writers(string[] args)
        {
            int write_count = writers(args);
            string send_log = "";
            if (processCommandLineForWriteLog(args))
                send_log = " -Writer_Send_Message_Log";
            for (int i = 1; i <= write_count; i++)
            {
                ProcessStartInfo writer = new ProcessStartInfo(Path.GetFullPath("..\\..\\..\\Client\\bin\\Debug\\Client.exe"));
                writer.Arguments = "/L http://localhost:" + (8080 + port_count).ToString() + "/CommService" + send_log;
                Process.Start(writer);
                ++port_count;
            }
        }



        public int readers(string[] args)
        {
            int readers = 1;
            for (int i = 0; i < args.Length; ++i)
            {
                Console.WriteLine("args = {0}", args[i]);
               if ((args.Length > i + 1) && args[i].ToUpper() == "-READER_COUNT")
                readers = Int32.Parse(args[i + 1]);
                        }
            return readers;
        }

        public int writers(string[] args)
        {
            int readers = 1;
            for (int i = 0; i < args.Length; ++i)
            {
                Console.WriteLine("args = {0}", args[i]);
               if ((args.Length > i + 1) && args[i].ToUpper() == "-WRITER_COUNT")
                    readers = Int32.Parse(args[i + 1]);
            }
            return readers;
        }

        public bool processCommandLineForPartialLog(string[] args)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].ToUpper() == "-READER_PARTIAL_DISPLAY")
                    return true;
            }
            return false;
        }

        public bool processCommandLineForWriteLog(string[] args)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].ToUpper() == "-WRITER_SEND_MESSAGE_LOG")
                    return true;
            }
            return false;
        }

        // main Function
        static void Main(string[] args)
        {
            Console.Title = " \n Test Executive ";
            Console.WriteLine(" Hello \n Welcome to Remote DataBase ");
            TestExecutive tst = new TestExecutive();
            tst.WPF();
            Thread.Sleep(500);
            tst.Server();
            Thread.Sleep(500);
            Console.WriteLine("\n Server and the Clients are launched to demonstrate the Remote Database operations ");
            tst.Writers(args);
            tst.Readers(args);
            
        }
    }
}

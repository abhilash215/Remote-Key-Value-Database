///////////////////////////////////////////////////////////////
// Server.cs -  Server operations                            //
// ver 2.4                                                   //
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
* -This package receives and sends the message
*  back to the client.
* - Takes care of all the remote db operations.
* -The performance also is calculated.
*
* Maintainence
* -----------------
*  Required Files:Processor.cs,HiResTimer.cs,Sender.cs
*                 Receiver.cs,Utilities.cs,CommService
*                 
*  Build Process:devenv Project4code.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*/
/*
* Public Interface
* -----------------
*  ProcessCommandLine()
* - retrieve urls from the CommandLine
* Main()
* -Receives the message.
* -Sends the reponse message back to clients. 
*
*/

/*
 * Maintenance History:
 * --------------------
 * ver 2.4:  21 Nov 2015
 * -  Namespace changed
 * -response message updated
 * ver 2.3 : 29 Oct 2015
 * - added handling of special messages: 
 *   "connection start message", "done", "closeServer"
 * ver 2.2 : 25 Oct 2015
 * - minor changes to display
 * ver 2.1 : 24 Oct 2015
 * - added Sender so Server can echo back messages it receives
 * - added verbose mode to support debugging and learning
 * - to see more detail about what is going on in Sender and Receiver
 *   set Utilities.verbose = true
 * ver 2.0 : 20 Oct 2015
 * - Defined Receiver and used that to replace almost all of the
 *   original Server's functionality.
 * ver 1.0 : 18 Oct 2015
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Project4Code
{
    using Util = Utilities;
    class Server
    {
        string address { get; set; } = "localhost";
        string port { get; set; } = "8080";

        public void ProcessCommandLine(string[] args)
        {
            if (args.Length > 0)
                port = args[0];
            if (args.Length > 1)
                address = args[1];
        }

        //function used to display the performance in WPF
        public void send_wpf_msg(ulong prc_time, Sender sndr)
        {
            Message wpf_msg = new Message();
            wpf_msg.toUrl = "http://localhost:8081/CommService";
            wpf_msg.fromUrl = "http://localhost:8080/CommService";

            string lat_msg = "Server(8080) : Processing time = " + prc_time;
            wpf_msg.content = lat_msg;
            sndr.sendMessage(wpf_msg);

        }
        //Main function parses and sends the messages to server//
        static void Main(string[] args)        {
            Util.verbose = false;
            Server srvr = new Server();
            srvr.ProcessCommandLine(args);
            Console.Title = "Server";
            Console.Write(String.Format("\n  Starting CommService server listening on port {0}", srvr.port));
            Console.Write("\n ====================================================\n");
            Sender sndr = new Sender(Util.makeUrl(srvr.address, srvr.port));
            Receiver rcvr = new Receiver(srvr.port, srvr.address);
            Action serviceAction = () =>               {
                Message msg = null;
                DBEngine<int, DBElement<int, string>> dbserver = new DBEngine<int, DBElement<int, string>>(); //new DBEngine
                QueryEngine QE = new QueryEngine();
                HiResTimer timer = new HiResTimer(); //new object for timer
                while (true)                 {
                    msg = rcvr.getMessage();   // note use of non-service method to deQ messages
                    Console.Write("\n  Received message:");
                    Console.Write("\n  sender is {0}", msg.fromUrl);
                    Console.Write("\n  content is {0}\n", msg.content);
                    if (msg.content == "connection start message")
                        continue; // don't send back start message
                    if (msg.content == "done")
                    { Console.Write("\n  client has finished\n");continue; }
                    if (msg.content == "closeServer")
                    {  Console.Write("received closeServer"); break; }
                    timer.Start();                  //start timer
                    XElement insertelem = XElement.Parse(msg.content);
                    XElement res = new XElement("result", "not found");
                    processor rdbserver = new processor();
                    Console.WriteLine("\n----------write client operations----------");
                    Console.WriteLine("\n");
                    //----------select the required method to perform operations------------//
                    if (insertelem.Element("Type").Value.Equals("Insert"))
                        res = rdbserver.insert(insertelem, dbserver);
                       else if (insertelem.Element("Type").Value.Equals("Delete"))
                        res = rdbserver.Delete(insertelem, dbserver);
                     else if (insertelem.Element("Type").Value.Equals("EditName"))
                        res = rdbserver.EditName(insertelem, dbserver);
                     else if (insertelem.Element("Type").Value.Equals("getvalue"))
                        res = rdbserver.getvalue(insertelem, dbserver, QE);
                     else if (insertelem.Element("Type").Value.Equals("EditDescr"))
                        res = rdbserver.editdescr(insertelem, dbserver);
                     else if (insertelem.Element("Type").Value.Equals("getchildren"))
                        res = rdbserver.getchildren(insertelem, dbserver, QE);
                    else if (insertelem.Element("Type").Value.Equals("Persist"))
                        res = rdbserver.persistdb(insertelem, dbserver);
                    else   Console.Write("   operation failed   ");
                     Console.WriteLine("\n-------------server response----------");
                    XElement response = new XElement("resonse");
                    response.Add(res);
                    timer.Stop();              //stop timer
                    Console.WriteLine("the time taken for operation is {0}", timer.ElapsedMicroseconds + " MicroSeconds ");
                    srvr.send_wpf_msg(timer.ElapsedMicroseconds, sndr);
                    Util.swapUrls(ref msg);
                    msg.content = response.ToString();
                    sndr.sendMessage(msg);             //sending message    
                      }   };
            if (rcvr.StartService())
            rcvr.doService(serviceAction); // This serviceAction is asynchronous so the call doesn't block.
            Util.waitForUser();        }    }
}

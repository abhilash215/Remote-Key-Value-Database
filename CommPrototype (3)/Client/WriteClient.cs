﻿///////////////////////////////////////////////////////////////
// Client1.cs:All the write client operations are taken care //
// ver 2.2                                                   //
// CSE681 - Software Modeling and Analysis, Project #4       //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////     

/*
 * Package Operations:
 * ----------------------
 * - in this incantation the client has Sender and now has Receiver to
 *   retrieve Server echo-back messages.
 * Additions to C# Console Wizard generated code:
 * - Added using System.Threading
 * - Added reference to ICommService, Sender, Receiver, Utilities
 */
/*
* Maintainence
* ---------------
* Required Files: ICommService.cs,Receiver,cs,Sender.cs,
*                Utilities.cs
*
* Build Process: devenv Project4code.sln /Rebuild debug
*                Run from Developer Command Prompt
*                To find: search for developer
*/
/* Public Interface
* ------------------
* doAction()
* -Based on the message received ,performs some operation.
* main()
* - loads the xml and sends it to server.
*processcommandline()
* -retrieve urls from the CommandLine
*/
/*
 * Maintenance History:
 * --------------------
 * ver 2.2:  20 Nov 2015
 * - Added the Functionalities to send to
 *   different queries and operations
 * ver 2.1 : 29 Oct 2015
 * - fixed bug in processCommandLine(...)
 * - added rcvr.shutdown() and sndr.shutDown() 
 * ver 2.0 : 20 Oct 2015
 * - replaced almost all functionality with a Sender instance
 * - added Receiver to retrieve Server echo messages.
 * - added verbose mode to support debugging and learning
 * - to see more detail about what is going on in Sender and Receiver
 *   set Utilities.verbose = true
 * ver 1.0 : 18 Oct 2015
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.Xml;
using static System.Console;

namespace Project4Code
{
    using Util = Utilities;

    ///////////////////////////////////////////////////////////////////////
    // Client class sends and receives messages in this version
    // - commandline format: /L http://localhost:8085/CommService 
    //                       /R http://localhost:8080/CommService
    //   Either one or both may be ommitted

    class Client
    {
        string localUrl { get; set; } = "http://localhost:8081/CommService";
        string remoteUrl { get; set; } = "http://localhost:8080/CommService";

        // to retrieve urls from the CommandLine
        public void processCommandLine(string[] args)
        {
            if (args.Length == 0)
                return;
            localUrl = Util.processCommandLineForLocal(args, localUrl);
            remoteUrl = Util.processCommandLineForRemote(args, remoteUrl);
        }

        public Action doAction(Receiver rcvr)
        {
            Action receiveAction = () =>
              {
                  Message msg = null;
                  while (true)
                  {
                      msg = rcvr.getMessage();
                      Console.WriteLine("\n message received");
                      Console.Write("\n");
                      if (msg.content == "closereceiver")
                          break;
                      if (msg.content == "connection start message")
                          continue;
                      XElement dbe = XElement.Parse(msg.content);
                      string resp = dbe.Element("result").Value;
                      Console.WriteLine("\n----------server response----------");
                      Console.WriteLine(resp);
                  }
              };
            return receiveAction;
        }

        // main function to send messages
        static void Main(string[] args)         {
            Thread.Sleep(2000);
            Console.Write("\n  starting CommService client");
            Console.Write("\n =============================\n");
            Console.Title = "Client #1";
            Client clnt = new Client();
            clnt.processCommandLine(args);
            string localPort = Util.urlPort(clnt.localUrl);
            string localAddr = Util.urlAddress(clnt.localUrl);
            Receiver rcvr = new Receiver(localPort, localAddr);
            if (rcvr.StartService())            {
                Action newact = clnt.doAction(rcvr);
                rcvr.doService(newact);             }
            Sender sndr = new Sender(clnt.localUrl);  // Sender needs localUrl for start message
            Message msg = new Message();
            msg.fromUrl = clnt.localUrl;
            msg.toUrl = clnt.remoteUrl;
            Console.Write("\n  sender's url is {0}", msg.fromUrl);
            Console.Write("\n  attempting to connect to {0}\n", msg.toUrl);
            if (!sndr.Connect(msg.toUrl))             {
                Console.Write("\n  could not connect in {0} attempts", sndr.MaxConnectAttempts);
                sndr.shutdown();
                rcvr.shutDown();                return;            }
            XDocument doc = XDocument.Load("./../../../dbload.xml");
            Console.WriteLine(doc.ToString());
            Console.WriteLine(" xml  is loaded ");
            Console.WriteLine();
            XElement dbe = doc.Element("root");
            int count = Int32.Parse(dbe.Element("count").Value);
            for (int i = 0; i < count; i++)             {
                foreach (var a in dbe.Elements("ClientMessage"))                 {
                    msg = new Message();
                    msg.fromUrl = clnt.localUrl;
                    msg.toUrl = clnt.remoteUrl;
                    Console.Write("\n  sending {0}\n", msg.content);
                    Console.Write("\n");
                    msg.content = a.ToString();
                    sndr.sendMessage(msg);
                    Thread.Sleep(200);                 }             }
            Thread.Sleep(200);   // Wait for user to press a key to quit.
            // Ensures that client has gotten all server replies.
            Util.waitForUser();   // shut down this client's Receiver and Sender by sending close messages
            rcvr.shutDown();            sndr.shutdown();            Console.Write("\n\n");
        }    } }

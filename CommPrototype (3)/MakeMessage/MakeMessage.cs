///////////////////////////////////////////////////////////////
// Server.cs - CommService server                            //
// ver 1.1                                                   //
// CSE681 - Software Modeling and Analysis, Project #4       //
// Application: Demonstration for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Purpose:
 *----------
 * This is a placeholder for application specific message construction
 *
 * Additions to C# Console Wizard generated code:
 * - references to ICommService and Utilities
 *
  Maintainence
* ---------------
* Required Files: ICommService.cs,Receiver,cs,Sender.cs,
*                Utilities.cs
*
* Build Process: devenv Project4code.sln /Rebuild debug
*                Run from Developer Command Prompt
*                To find: search for developer
*/
/*
 * Maintenance History:
 * --------------------
 * ver 1.1 : 23 Nov 2015
 * - Namespace changed
 * ver 1.0 : 29 Oct 2015
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Code
{
  public class MessageMaker
  {
    public static int msgCount { get; set; } = 0;
    public Message makeMessage(string fromUrl, string toUrl)
    {
      Message msg = new Message();
      msg.fromUrl = fromUrl;
      msg.toUrl = toUrl;
      msg.content = String.Format("\n  message #{0}", ++msgCount);
      return msg;
    }
#if (TEST_MESSAGEMAKER)
    static void Main(string[] args)
    {
      MessageMaker mm = new MessageMaker();
      Message msg = mm.makeMessage("fromFoo", "toBar");
      Utilities.showMessage(msg);
      Console.Write("\n\n");
    }
#endif
  }
}

///////////////////////////////////////////////////////////////
//UtilityExtensions.cs - Define methods to simplify project  //
//                       code                                //
//Ver 1.1                                                    //
// Application: Demonstration for CSE681-SMA, Project#4      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234  ,audayash@syr.edu           //
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements utility extensions that are not specific
 * to a single package.
 */
/*
*Public interface
*------------------
*title()
*-used to display the title with formatting
*Main()
*-main method of package
/*
 * Maintenance:
 * ------------
 * Required Files: UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.1:: 23 Nov 15
 * -  Namespace changed
 * ver 1.0 : 13 Sep 15
 * - first release
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project4Code
{
  public static class UtilityExtensions
  {
    public static void title(this string aString, char underline = '-')
    {
      Console.Write("\n  {0}", aString);
      Console.Write("\n {0}", new string(underline, aString.Length + 2));
    }
  }
  public class TestUtilityExtensions
  {    
        //Main method
    static void Main(string[] args)
    {
      "Testing UtilityExtensions.title".title();
      Write("\n\n");
    }
  }
}

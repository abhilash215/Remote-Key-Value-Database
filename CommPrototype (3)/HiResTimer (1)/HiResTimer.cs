///////////////////////////////////////////////////////////////
// Server.cs -  High Resolution Timer                        //
// ver 2.5                                                   //
// CSE681 - Software Modeling and Analysis, Project #4       //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
* Package Operations
*--------------------
* -Used to define the High Resolution timer for performance of database
*/
/*
* Public Interface 
*--------------------
* ElapsedMicroseconds()
* - Gives the time elapsed in microseconds
* Start()
* - start the timer
* stop()
* -stop the timer
*/

/* Maintainence
* ---------------
* Required Files: ICommService.cs,Receiver,cs,Sender.cs,
* Utilities.cs
*
* Build Process: devenv Project4code.sln /Rebuild debug
*                Run from Developer Command Prompt
* To find: search for developer
*/
/* Maintainence History
*----------------------
* ver 2.5 :23 Nov 2015
* Namespace changed
*/

using System;
using System.Runtime.InteropServices; // for DllImport attribute
using System.ComponentModel; // for Win32Exception class
using System.Threading; // for Thread.Sleep method

namespace Project4Code
{
  public  class HiResTimer
   {
     protected ulong a, b, f;
     
     public HiResTimer()
      {
         a = b = 0UL;
         if ( QueryPerformanceFrequency( out f) == 0) 
            throw new Win32Exception();
      }

      public ulong ElapsedTicks
      {
         get
         { return (b-a); }
      }

        // gives the elapsed time in microsecinds
      public ulong ElapsedMicroseconds
      {
         get
         { 
            ulong d = (b-a); 
            if (d < 0x10c6f7a0b5edUL) // 2^64 / 1e6
               return (d*1000000UL)/f; 
            else
               return (d/f)*1000000UL;
         }
      }

      public TimeSpan ElapsedTimeSpan
      {
         get
         { 
            ulong t = 10UL*ElapsedMicroseconds;
            if ((t&0x8000000000000000UL) == 0UL)
               return new TimeSpan((long)t);
            else
               return TimeSpan.MaxValue;
         }
      }

      public ulong Frequency
      {
         get
         { return f; }
      }

        // starts the timer
      public void Start()
      {
         Thread.Sleep(0);
         QueryPerformanceCounter( out a);
      }

        //  stop timer
      public ulong Stop()
      {
         QueryPerformanceCounter( out b);
         return ElapsedTicks;
      }

     // Here, C# makes calls into C language functions in Win32 API
     // through the magic of .Net Interop

      [ DllImport("kernel32.dll", SetLastError=true) ]
      protected static extern 
         int QueryPerformanceFrequency( out ulong x);

      [ DllImport("kernel32.dll") ]
      protected static extern 
         int QueryPerformanceCounter( out ulong x);
   }
}
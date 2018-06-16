using System;
using System.Collections.Generic;
using System.Text;

#if (WINCE)
namespace rwmk2
#elif (WIN3264)
using System.Linq;
namespace rwmk3
#endif
{

  public class clsDebug
  {

    public static string GetAppVersionString()
    {
      return String.Format("{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
    }

#if (WINCE)
    //Debug output out COM port on ADS systems 
    //
    [System.Runtime.InteropServices.DllImport("coredll.dll", EntryPoint = "NKDbgPrintfW")]
    private static extern void DebugMsg(string message);
#elif (WIN3264)
    //don't need anything here, is not an extern
#endif
    private static void DEBUGMSG(string message)
    {
      BreakString(message);
    }
    private static void DEBUGMSG(string callingFunction, string message)
    {
      BreakString(callingFunction + ": " + message);
    }
    private static void DEBUGMSG(string callingFunction, string message, bool condition)
    {
      if (condition)
      {
        BreakString(callingFunction + ": " + message);
      }
    }
    private static void BreakString(string message)
    {
      string stringout = "";
      //string stringlocal = message.Replace("%", "%%");
      string stringlocal = message; //.Replace("%", "%%");
      int intLength = stringlocal.Length;
      const int intChunkLength = 99; //s format has 21 (counting trailing space I add below)
      int intChunks = intLength / intChunkLength;
      int intRemainder = intLength % intChunkLength;
      if (intChunks > 0)
      {
        for (int intPiece = 0; intPiece < intChunks; intPiece++)
        {
          stringout = DateTime.Now.ToString("G") + " " + stringlocal.Substring(intPiece * intChunkLength, intChunkLength);
#if DEBUG
          System.Diagnostics.Debug.WriteLine(stringout);
#endif
          WriteStringToLogFile(stringout);
        }
        if (intRemainder > 0)
        {
          stringout = DateTime.Now.ToString("G") + " " + stringlocal.Substring((intChunks * intChunkLength), intLength - (intChunks * intChunkLength));
#if DEBUG
          System.Diagnostics.Debug.WriteLine(stringout);
#endif
          WriteStringToLogFile(stringout);
        }
      }
      else
      {
        stringout = DateTime.Now.ToString("G") + " " + stringlocal;
#if DEBUG
        System.Diagnostics.Debug.WriteLine(stringout);
#endif
        WriteStringToLogFile(stringout);
      }
    }



    public static void LineOut(string stringMessage)
    {
      DEBUGMSG(stringMessage);
    }
    public static void LineOut(string stringfunction, string stringMessage)
    {
      DEBUGMSG(stringfunction, stringMessage);
    }
    public static void LineOut(string stringfunction, Exception e)
    {
      DEBUGMSG("** ERROR ========================================================= ");
      if (stringfunction != null)
      {
        DEBUGMSG("  Function : " + stringfunction);
      }
      if (e.Message != null)
      {
        string stringtemp = e.Message;
        if (stringtemp.IndexOf("\n") > 0)
        {
          stringtemp = stringtemp.Replace("\n", "\n" + DateTime.Now.ToString("G") + "            : ");
        }
        DEBUGMSG("  Error    : " + stringtemp);
      }
      if (e.StackTrace != null)
      {
        string stringtemp = e.StackTrace;
        if (stringtemp.IndexOf("\r\n") > 0)
        {
          stringtemp = stringtemp.Replace("\r\n", " ");
        }
        DEBUGMSG("  Stack    : " + stringtemp);
      }
#if(MAINAPP)
      clsSystem.OutMemory(false, "AAA");
#endif
      return;
    }
    public static void LineOut(string stringfunction, string stringLine, Exception e)
    {
      DEBUGMSG("** ERROR ========================================================= ");
      if (stringfunction != null)
        DEBUGMSG("  Function : " + stringfunction);
      if (stringLine != null)
      {
        string stringtemp = stringLine;
        if (stringtemp.IndexOf("\n") > 0)
        {
          stringtemp = stringtemp.Replace("\n", "\n" + DateTime.Now.ToString("G") + "            : ");
        }
        DEBUGMSG("  Line     : " + stringtemp);
      }
      if (e.Message != null)
      {
        string stringtemp = e.Message;
        if (stringtemp.IndexOf("\n") > 0)
        {
          stringtemp = stringtemp.Replace("\n", "\n" + DateTime.Now.ToString("G") + "            : ");
        }
        DEBUGMSG("  Error    : " + stringtemp);
      }
      if (e.StackTrace != null)
      {
        string stringtemp = e.StackTrace;
        if (stringtemp.IndexOf("\r\n") > 0)
        {
          stringtemp = stringtemp.Replace("\r\n", " ");
        }
        DEBUGMSG("  Stack    : " + stringtemp);
      }
#if(MAINAPP)
      clsSystem.OutMemory(false, "BBB");
#endif
      return;
    }


    public static void LineOutHexAndASCII(string stringMessage)
    {
      string sOutA = "ASC: ";
      string sOutH = "HEX: ";

      foreach (char c in stringMessage)
      {
        int tmp = c;
        sOutA += tmp.ToString("000") + " ";
        //sOutH += String.Format("{0:x3}", (uint)System.Convert.ToUInt32(tmp.ToString())) + " ";
        sOutH += String.Format("{0:x3}", System.Convert.ToUInt32(tmp.ToString())) + " ";
      }
      DEBUGMSG(sOutA);
      DEBUGMSG(sOutH);
      DEBUGMSG(stringMessage.Replace("\0", "<NULL>").Replace("\r", "<CR>").Replace("\n", "<LF>"));
    }

    public static void MsgOut(string stringfunction, string stringMessage)
    {
      LineOut(stringfunction, stringMessage);
#if(CONSOLE)
      //nothing
#else
      System.Windows.Forms.MessageBox.Show(stringMessage, stringfunction, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
#endif
    }
    public static void MsgOut(string stringfunction, string stringMessage, Exception e)
    {
      //string stringOut = LineOut(stringfunction, stringMessage, e);
      LineOut(stringfunction, stringMessage, e);
#if(CONSOLE)
      //nothing
#else
      System.Windows.Forms.MessageBox.Show(stringMessage, stringfunction, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
#endif
    }


    public static void WriteStringToLogFile(string stringout)
    {
      string stringFile = clsFileSystemFunctions.GetApplicationLogFilePath();

      bool boolappend = true;

      try
      {
        if (System.IO.File.Exists(stringFile))
        {
          System.IO.FileInfo fi = new System.IO.FileInfo(stringFile);
          //if (fi.Length > (32000000))
          if (fi.Length > (5000000))
          {
            //clsFileSystemFunctions.DeleteFileInfo(fi, false);
            clsFileSystemFunctions.MigrateLogToLogBackupFile();
            boolappend = false;
          }
        }
        else
        {
          boolappend = false;
        }
        System.IO.StreamWriter sw = new System.IO.StreamWriter(stringFile, boolappend);
        sw.WriteLine(stringout);
        sw.Flush();
        sw.Close();
      }
      catch (Exception e1)
      {
#if DEBUG
        if (e1.Message != null)
        {
          string stringtemp = e1.Message;
          if (stringtemp.IndexOf("\n") > 0)
          {
            stringtemp = stringtemp.Replace("\n", "\n" + DateTime.Now.ToString("G") + "            : ");
          }
          System.Diagnostics.Debug.WriteLine("  Error    : " + stringtemp);
        }
        if (e1.StackTrace != null)
        {
          string stringtemp = e1.StackTrace;
          if (stringtemp.IndexOf("\r\n") > 0)
          {
            stringtemp = stringtemp.Replace("\r\n", " ");
          }
          System.Diagnostics.Debug.WriteLine("  Stack    : " + stringtemp);
        }
#endif
      }
    }



  }
}

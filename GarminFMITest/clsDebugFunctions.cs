using System;
using System.Collections.Generic;
using System.Text;

namespace Garmin_FMI_Test
{
  class clsDebugFunctions
  {

    private static void WriteStringToLogFile(string stringLines)
    {
      string stringFile = clsApplicationFunctions.GetApplicationLogFileName();
      
      if (System.IO.File.Exists(stringFile.ToString()) == true)
      {
        System.IO.FileInfo fi = new System.IO.FileInfo(stringFile.ToString());
        if (fi.Length > (32000000))
        {
          try
          {
            fi.Delete();
            System.Windows.Forms.Application.DoEvents();
          }
          catch (Exception e1)
          {
            // error while deleting log file...
            System.Windows.Forms.MessageBox.Show("Error Deleting Log File " + stringFile + "\r\nError: " + e1.Message, "WriteStringToLogFile() Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
          }
        }
      }
      string stringLocal = DateTime.Now.ToString("s") + " " + stringLines;
      System.Diagnostics.Debug.WriteLine(stringLocal.ToString());
      // it looks like calling this with boolappend = true overrides the default behavior .. explained in the tooltip in vs2017
      //  => removing the booltrue
      //System.IO.StreamWriter sw = new System.IO.StreamWriter(stringFile.ToString(), true);
      System.IO.StreamWriter sw = new System.IO.StreamWriter(stringFile.ToString());
      sw.WriteLine(stringLocal);
      sw.Flush();
      sw.Close();
    }

    public static void LineOut(string stringMessage)
    {
      WriteStringToLogFile(stringMessage);
    }
    public static void LineOut(string stringFunction, string stringMessage)
    {
      WriteStringToLogFile(stringFunction.ToString() + " " + stringMessage.ToString());
    }
    public static string LineOut(string stringFunction, Exception e)
    {
      string stringOut = "ERROR ============================================== ";
      string stringThisLine = "";
      WriteStringToLogFile(stringOut.ToString());
      if (stringFunction != null)
      {
        stringThisLine = "Function:  " + stringFunction.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.Message != null)
      {
        stringThisLine = "Error:  " + e.Message.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.InnerException != null)
      {
        stringThisLine = "InnerException:  " + e.InnerException.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.TargetSite != null)
      {
        stringThisLine = "TargetSite:  " + e.TargetSite.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.Source != null)
      {
        stringThisLine = "Source:  " + e.Source.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      return stringOut;
    }
    public static string LineOut(string stringFunction, string stringLine, Exception e)
    {
      string stringOut = "ERROR ============================================= ";
      string stringThisLine = "";
      WriteStringToLogFile(stringOut.ToString());
      if (stringFunction != null)
      {
        stringThisLine = "Function:  " + stringFunction.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (stringLine != null)
      {
        stringThisLine = "Line:  " + stringLine.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.Message != null)
      {
        stringThisLine = "Error:  " + e.Message.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.InnerException != null)
      {
        stringThisLine = "InnerException:  " + e.InnerException.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.TargetSite != null)
      {
        stringThisLine = "TargetSite:  " + e.TargetSite.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      if (e.Source != null)
      {
        stringThisLine = "Source:  " + e.Source.ToString();
        stringOut = stringOut + "\r\n" + stringThisLine;
        WriteStringToLogFile(stringThisLine.ToString());
      }
      return stringOut;
    }

    public static void MsgOut(string stringFunction, string stringMessage)
    {
      LineOut(stringFunction, stringMessage);
      System.Windows.Forms.MessageBox.Show(stringMessage, stringFunction, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
    }
    public static void MsgOut(string stringFunction, Exception e)
    {
      string stringOut = LineOut(stringFunction, e);
      System.Windows.Forms.MessageBox.Show(stringOut, stringFunction, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
    }
    public static void MsgOut(string stringFunction, string stringMessage, Exception e)
    {
      string stringOut = LineOut(stringFunction, stringMessage, e);
      System.Windows.Forms.MessageBox.Show(stringOut, stringFunction, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
    }
    public static bool MsgOutYes(string stringFunction, string stringMessage)
    {
      bool boolreturn = false;
      LineOut(stringFunction, stringMessage);
      System.Windows.Forms.DialogResult  result = System.Windows.Forms.MessageBox.Show(stringMessage, stringFunction, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
      if (result == System.Windows.Forms.DialogResult.Yes)
      {
        boolreturn = true;
      }
      return boolreturn;
    }




  }
}

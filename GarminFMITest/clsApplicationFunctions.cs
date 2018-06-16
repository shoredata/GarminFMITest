using System;
using System.Collections.Generic;
using System.Text;

namespace Garmin_FMI_Test
{
  class clsApplicationFunctions
  {

    [System.Runtime.InteropServices.DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    [System.Runtime.InteropServices.DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    public static string GetAppSetting(string stringsettingname, string stringdefaultvalue)
    {
      StringBuilder stringreturnvalue = new StringBuilder(255);
      int intreturn = GetPrivateProfileString("Main", stringsettingname.ToString(), stringdefaultvalue, stringreturnvalue, 255, System.Windows.Forms.Application.ExecutablePath.ToString() + "-" + GetThisComputerName().ToString().ToLower() + ".ini");
      return stringreturnvalue.ToString();
    }
    public static long SaveAppSetting(string stringsettingname, string stringsettingvalue)
    {
      return WritePrivateProfileString("Main", stringsettingname.ToString(), stringsettingvalue.ToString(), System.Windows.Forms.Application.ExecutablePath.ToString() + "-" + GetThisComputerName().ToString().ToLower() + ".ini");
    }

    public static string GetApplicationVersionString()
    {
      return String.Format("Version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }
    public static string GetApplicationVersionWithoutBuildString()
    {
      string stringreturn = "";
      int intcount = 0;
      string[] stringvers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');

      foreach (string stringchar in System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.'))
      {
        intcount++;
        if (intcount < 4)
        {
          stringreturn += stringchar;
          if (intcount < 3)
          {
            stringreturn += ".";
          }
        }
      }
      return stringreturn;
    }
    public static string GetApplicationProductName()
    {
      //this is the "PRODUCT NAME" on the 'Assembly Informaiton' page (button) accessed from the MainOutput/Properties/Application Tab
      return System.Windows.Forms.Application.ProductName;
    }
    public static string GetApplicationLogFileName()
    {
      string stringprogramname = GetApplicationProductName();
      //THIS IS A BAD IDEA, THE PATH ISN'T NECESSARILY WRITIABLE, NEEDS TO BE IN APP DATA ..
      //return System.Windows.Forms.Application.ExecutablePath.ToString() + ".log.txt";
      return GetApplicationDataFolderPath() + GetApplicationProductName().ToLower() + ".log.txt";
    }
    public static string GetApplicationDataFolderPath()
    {
      //needs to be put into other vs2kx projects, including X8Pro
      string stringappdatawithbsandproduct = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"BS\" + GetApplicationProductName() + @"\");
      return stringappdatawithbsandproduct;
    }

    public static bool VerifyAndCreateAppDataFolders()
    {
      string stringfunction = "VerifyAndCreateAppDataFolders";
      bool boolreturn = true;
      try
      {
        System.IO.DirectoryInfo difind = new System.IO.DirectoryInfo(GetApplicationDataFolderPath());
        if (!difind.Exists)
        {
          mkDirs(GetApplicationDataFolderPath());
        }
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, e1);
        boolreturn = false;
      }
      return boolreturn;
    }
    public static bool mkDirs(string sPath)
    {
      if (sPath.EndsWith(@"\")) sPath = String.Format("{0}\\", sPath);
      try
      {
        string sPath2Check;
        int nPos = sPath.IndexOf(@"\");
        while (nPos > 0)
        {
          sPath2Check = sPath.Substring(0, nPos);
          if (!(System.IO.Directory.Exists(sPath2Check)))
            System.IO.Directory.CreateDirectory(sPath2Check);

          nPos = sPath.IndexOf(@"\", nPos + 1);
        }
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut("mkDirs", e1);
        return false;
      }
      return true;
    }




    public static string GetThisComputerName()
    {
      return System.Environment.MachineName.ToString();
    }
    public static string GetMachineName()
    {
      return System.Net.Dns.GetHostName();
    }
    public static string GetCurrentUserID()
    {
      return System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
    }




    public static void ShellProgram(string stringProgram, string stringArguments)
    {
      try
      {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.EnableRaisingEvents = false;
        proc.StartInfo.FileName = stringProgram;
        proc.StartInfo.Arguments = stringArguments;
        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
        proc.Start();
      }
      catch (Exception e1)
      {
        clsDebugFunctions.MsgOut("ShellProgram", "Program: " + stringProgram + "\r\nArguments: " + stringArguments, e1);
      }
    }
    public static void ShellProgram(string stringProgram, string stringArguments, bool boolminimized)
    {
      try
      {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.EnableRaisingEvents = false;
        proc.StartInfo.FileName = stringProgram;
        proc.StartInfo.Arguments = stringArguments;
        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
        proc.Start();
      }
      catch (Exception e1)
      {
        clsDebugFunctions.MsgOut("ShellProgram", "Program: " + stringProgram + "\r\nArguments: " + stringArguments + "\r\nMinimized = True", e1);
      }
    }

    public static string InputBox(string prompt, string title, string defaultValue)
    {
      Int32 XPos = ((System.Windows.Forms.SystemInformation.WorkingArea.Width / 2) - 200); 
      Int32 YPos = ((System.Windows.Forms.SystemInformation.WorkingArea.Height / 2) - 100);

      //if you get a type or namespace warning here need to "Add Reference" for "Microsoft.VisualBasic" ...
      string question = Microsoft.VisualBasic.Interaction.InputBox(prompt, title, defaultValue, XPos, YPos);
      string s = "";
      if (question.Length > 0)
      {
        s = question;
      }
      return s;
    }


    public static bool CheckForApplicationUpdate(bool boolsilent, string stringversionfile, string stringinstallfile, ref bool boolshutdownprogram)
    {
      //note 

      bool boolreturn = false;
      //string stringfunction = "CheckForApplicationUpdate";
      string stringaddress = stringversionfile;
      try
      {
        //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

        Uri myUri = new Uri(stringaddress);

        // Create a request for the URL. 		
        System.Net.WebRequest request = System.Net.WebRequest.Create(myUri);

        // If required by the server, set the credentials.
        request.Credentials = System.Net.CredentialCache.DefaultCredentials;

        // Get the response.
        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

        if ((response.StatusDescription.ToLower().CompareTo("ok")!=0) && (!boolsilent))
        {
          // Display the status.
          clsDebugFunctions.MsgOut("CheckForApplicationUpdate", "StatusDescription = " + response.StatusDescription);
          return false;
        }
        else if ((response.StatusDescription.ToLower().CompareTo("ok")!=0) && (boolsilent))
        {
          // Log the status.
          clsDebugFunctions.LineOut("CheckForApplicationUpdate", "StatusDescription = " + response.StatusDescription);
          return false;
        }

        // Get the stream containing content returned by the server.
        System.IO.Stream dataStream = response.GetResponseStream();
        
        // Open the stream using a StreamReader for easy access.
        System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
        
        // Read the content.
        string responseFromServer = reader.ReadToEnd();

        bool boolnewer = false;
        string[] stringversions = responseFromServer.Split('.');
        int intwebmajor = int.Parse(stringversions[0], System.Globalization.NumberStyles.Any);
        int intwebminor = int.Parse(stringversions[1], System.Globalization.NumberStyles.Any);
        int intwebbuild = int.Parse(stringversions[2], System.Globalization.NumberStyles.Any);
        int intwebrev = int.Parse(stringversions[3], System.Globalization.NumberStyles.Any);

        if (intwebmajor > System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major)
        {
          boolnewer = true;
        }
        if (intwebminor > System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor)
        {
          boolnewer = true;
        }
        if (intwebbuild > System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build)
        {
          boolnewer = true;
        }
        if (intwebrev > System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision)
        {
          boolnewer = true;
        }

        // Cleanup the streams and the response.
        reader.Close();
        dataStream.Close();
        response.Close();

        bool boolcontinue = true;

        if ((boolnewer) && (!boolsilent))
        {
          // Display the status.
          //clsDebugFunctions.MsgOut("CheckForApplicationUpdate", "There is a newer version available.");
          string stringprompt = "There is a newer version of the application available - do you want to download and install it at this time?\r\n\r\nFollow the prompts in the web browser as necessary to download and install the updated application.  A reboot may be necessary if files to be updated are in use during the install.";


        //stringprompt = "There is a newer version of the application available - do you want to download and install it at this time?\r\n\r\nFollow the prompts in the web browser as necessary to download and install the updated application.  A reboot may be necessary if files to be updated are in use during the install.";

        if (boolcontinue)
          {
            if (System.Windows.Forms.MessageBox.Show(stringprompt, "Check for Application Update", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
              string website = stringinstallfile + responseFromServer.ToString() + ".msi";
              website.Replace("\n", "");
              website.Replace("\r", "");
              System.Diagnostics.Process.Start("iexplore.exe", website);

              clsDebugFunctions.LineOut("Check For Application Update", "Shutting down the program....");
              boolshutdownprogram = true;

            }
          }
          boolreturn = true;
        }
        else if ((boolnewer) && (boolsilent))
        {
          // Log the status.
          clsDebugFunctions.LineOut("Check For Application Update", "There is a newer version available.");
          boolreturn = true;
        }
        else
        {
          if (!boolsilent)
          {
            clsDebugFunctions.MsgOut("Check For Application Update", "Your version is up to date.");
          }
          boolreturn = false;
        }

        //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

      }
      catch (Exception e1)
      {
        if (!boolsilent)
        {
          clsDebugFunctions.MsgOut("Check For Application Update", "stringversionfile = " + stringversionfile + "\r\nstringinstallfile = " + stringinstallfile, e1);
        }
        else
        {
          clsDebugFunctions.LineOut("CheckForApplicationUpdate", "stringversionfile = " + stringversionfile + "\r\nstringinstallfile = " + stringinstallfile, e1);
        }
        boolreturn = false;
      }
      return boolreturn;
    }

  }
}

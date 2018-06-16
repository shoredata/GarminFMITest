using System;
using System.Collections.Generic;
using System.Text;

namespace Garmin_FMI_Test
{
  class clsStringFunctions
  {


    public static string DDtoDMS(double dincoordinate, bool boollatitude)
    {
      double coordinate = dincoordinate;
      if (double.IsNaN(coordinate))
      {
        return string.Empty;
      }

      if (boollatitude)
      {
        if (Math.Abs(coordinate) > 90.0000)
        {
          return string.Empty;
        }
      }
      else
      {
        if (Math.Abs(coordinate) > 180.0000)
        {
          return string.Empty;
        }
      }

      // Set flag if number is negative
      bool neg = coordinate < 0d;

      // Work with a positive number
      coordinate = Math.Abs(coordinate);

      // Get d/m/s components
      double d = Math.Floor(coordinate);
      coordinate -= d;
      coordinate *= 60;
      double m = Math.Floor(coordinate);
      coordinate -= m;
      //coordinate *= 60;
      double s = Math.Round(coordinate*10000);

      //// Create padding character
      //char pad;
      //char.TryParse("0", out pad);

      // Create d/m/s strings
      //string dd = d.ToString();
      //string mm = m.ToString().PadLeft(2, pad);
      //string ss = s.ToString().PadLeft(2, pad);

      // Append d/m/s
      ///string dms = string.Format("{0}°{1}'{2}\"", dd, mm, ss);
      string dms = "";

      // Append compass heading
      if (!boollatitude)
      {
        dms = string.Format("{0:000}{1:00}{2:0000}", d, m, s);
        dms += neg ? "W" : "E";
      }
      else
      {
        dms = string.Format("{0:00}{1:00}{2:0000}", d, m, s);
        dms += neg ? "S" : "N";
      }

      // Return formated string
      return dms;
    }

    public static double DMStoDD(string ddmmssin, bool boollatitude)
    {
      double doublereturn = 0;
      string stringLatDegrees;
      string stringLatMinutes;
      string stringLatSeconds;
      double doubleLatDegrees;
      double doubleLatMinutes;
      double doubleLatSeconds;

      string stringLongDegrees;
      string stringLongMinutes;
      string stringLongSeconds;
      double doubleLongDegrees;
      double doubleLongMinutes;
      double doubleLongSeconds;

      if (boollatitude)
      {
        if (ddmmssin.Length == 9)
        {
          if (clsStringFunctions.IsNumeric(ddmmssin.Substring(0, 8)))
          {
            stringLatDegrees = ddmmssin.Substring(0, 2);
            stringLatMinutes = ddmmssin.Substring(2, 2);
            stringLatSeconds = ddmmssin.Substring(4, 4);
            doubleLatDegrees = Double.Parse(stringLatDegrees);
            doubleLatMinutes = Double.Parse(stringLatMinutes + "." + stringLatSeconds);
            doubleLatSeconds = Double.Parse(stringLatSeconds);
            doubleLatDegrees = doubleLatDegrees + (doubleLatMinutes / 60);
            if (ddmmssin.Substring(8, 1) == "S")
            {
              doubleLatDegrees = -1 * doubleLatDegrees;
            }
            doublereturn = doubleLatDegrees;
          }
        }
      }
      else
      {
        if (ddmmssin.Length == 10)
        {
          if (clsStringFunctions.IsNumeric(ddmmssin.Substring(0, 9)))
          {
            stringLongDegrees = ddmmssin.Substring(0, 3);
            stringLongMinutes = ddmmssin.Substring(3, 2);
            stringLongSeconds = ddmmssin.Substring(5 , 4);
            doubleLongDegrees = Double.Parse(stringLongDegrees);
            doubleLongMinutes = Double.Parse(stringLongMinutes + "." + stringLongSeconds);
            doubleLongSeconds = Double.Parse(stringLongSeconds);
            doubleLongDegrees = doubleLongDegrees + doubleLongMinutes / 60;
            if (ddmmssin.Substring(9, 1) == "W")
            {
              doubleLongDegrees = -1 * doubleLongDegrees;
            }
            doublereturn = doubleLongDegrees;
          }
        }
      }
      return doublereturn;
    }


    public static string ReturnSubstringAfterChar(char charparse, string stringin)
    {
      string stringreturn = stringin;
      string stringlocal = stringin;
      //if in string > -1 then split and return string 1
      //else return string
      string[] stringsplit = stringlocal.Split(charparse);
      if (stringsplit.GetUpperBound(0) >= 1)
      {
        stringreturn = stringsplit[1];
      }
      return stringreturn.Trim();
    }

    public static string Parse(ref string stringtoparse, string stringseperator)
    {
      //string stringlocal = stringtoparse;
      string stringreturn = "";
      int intinstring = 0;
      if (stringtoparse.Length == 0)
      {
        stringreturn = "";
        stringtoparse = "";
      }
      else
      {
        intinstring = stringtoparse.IndexOf(stringseperator);
        if (intinstring == 0)
        {
          stringreturn = "";
          if (stringtoparse.Length > stringseperator.Length)
          {
            stringtoparse = stringtoparse.Substring(stringseperator.Length, stringtoparse.Length - stringseperator.Length);
          }
          else
          {
            stringtoparse = "";
          }
        }
        else if (intinstring == -1)
        {
          stringreturn = stringtoparse.ToString();
          stringtoparse = "";
        }
        else
        {
          stringreturn = stringtoparse.Substring(0, intinstring);
          if (intinstring + stringseperator.Length >= stringtoparse.Length)
          {
            stringtoparse = "";
          }
          else
          {
            stringtoparse = stringtoparse.Substring(intinstring + stringseperator.Length, stringtoparse.Length - intinstring - stringseperator.Length);
          }
        }
      }
      return stringreturn;
    }


    public static string Chr(int intchar)
    {
      return String.Format("{0}", (char)intchar);
    }
    public static int Asc(string stringin)
    {
      return (int)stringin.Substring(0, 1)[0];
    }

    public static string BoolToYesNoString(bool boolValue)
    {
      if (boolValue)
        return "1";
      else
        return "0";
    }

    public static string GetFixedLengthPaddedString(string stringin, char charpad, int intlength, bool boolprepadnumeric, bool boolremovedecimalchars, bool boolpostpadalphanumeric)
    {
      string stringlocal = stringin;
      if (boolremovedecimalchars)
        stringlocal = stringin.Replace(".", "");
      string stringreturn = "";
      if (stringlocal.Length < intlength)
      {
        if (boolpostpadalphanumeric)
        {
          stringreturn = stringlocal.PadRight(intlength, charpad);
        }
        else if (boolprepadnumeric)
        {
          stringreturn = stringlocal.PadLeft(intlength, charpad);
        }
        else
        {
          stringreturn = stringlocal.PadRight(intlength, ' ');
        }
      }
      else if (stringlocal.Length > intlength)
      {
        if (boolprepadnumeric)
        {
          stringreturn = stringlocal.Substring(stringlocal.Length - intlength, intlength);
        }
        else
        {
          stringreturn = stringlocal.Substring(0, intlength);
        }
      }
      else
      {
        stringreturn = stringlocal.ToString();
      }
      if (stringreturn.Length != intlength)
      {
        clsDebugFunctions.LineOut("GetFixedLengthPaddedString", "stringreturn.Length<>intlength: " + stringreturn.Length.ToString() + "," + intlength.ToString());
      }
      return stringreturn;
    }


    public static string GetSQLValidString(string stringToProcess)
    {
      try
      {
        int intlength = stringToProcess.Length;
        //string stringreplacechars =        @"!""&'(),/:;<=>?[\]^`{|}";
        //newFileName = Regex.Replace(title, @"[?:\/*""<>|]", "");
        //newFileName = Regex.Replace(title, @"[!&'(),?:;\/=""<>^`{}|]", "");
        string stringnewchar = " ";
        //string stringlocal = System.Text.RegularExpressions.Regex.Replace(stringToProcess, @"[!&'(),?:;\/=""<>^`{}|]", stringnewchar);
        //need , and need &
        string stringlocal = System.Text.RegularExpressions.Regex.Replace(stringToProcess, @"[!'()?:;\/=""<>^`{}|]", stringnewchar);
        for (int idx = 0; idx < stringlocal.Length; idx++)
        {
          if (((int)stringlocal[idx] < 32) || ((int)stringlocal[idx] > 126))
            stringlocal = stringlocal.Replace(stringlocal[idx], ' ');
        }
        stringlocal = stringlocal.Trim();
        //if (stringlocal.Length != intlength)
        //  clsDebugFunctions.LineOut("Truncated '" + stringToProcess + "' --> '" + stringlocal + "'");
        return stringlocal;
      }
      catch (Exception e1)
      {
        clsDebugFunctions.MsgOut("GetSQLValidString", stringToProcess, e1);
        return new string(' ', stringToProcess.Length);
      }
    }


    public static string ReturnPadFixedString(string stringin, int intlength, bool boolpadright)
    {
      string stringfunction = "ReturnPadFixedString";
      string stringlocal = stringin;
      string stringreturn = "";

      try
      {
        if (intlength <= 0)
        {
          stringreturn = "";
        }
        else
        {

          if (stringlocal.Length < intlength)
          {
            if (boolpadright)
            {
              stringreturn = stringlocal.PadRight(intlength);
            }
            else
            {
              stringreturn = stringlocal.PadLeft(intlength);
            }
          }
          else if (stringlocal.Length > intlength)
          {
            stringreturn = stringlocal.Substring(0, intlength);
          }
          else
          {
            stringreturn = stringlocal;
          }
        }
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, e1);
        stringreturn = NewString(' ', intlength);
      }
      return stringreturn;
    }

    public static DateTime ReturnValidDateTime(string stringin)
    {
      if (stringin.Length <= 0)
        return DateTime.Now;
      try
      {
        DateTime dtdelivend = DateTime.Parse(stringin);
        return dtdelivend;
      }
      catch
      {
        return DateTime.Now;
      }
    }

    public static double ReturnValidDouble(string stringin)
    {
      if (stringin.Length <= 0)
        return 0.0;
      try
      {
        //stringin = stringin.Replace("-","");

        double retNum = Double.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
        return retNum;
      }
      catch
      {
        return 0.0;
      }
    }

    public static int ReturnValidInt(string stringin)
    {
      if (stringin.Length <= 0)
        return 0;
      try
      {
        int retNum = int.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
        return retNum;
      }
      catch
      {
        return 0;
      }
    }


    public static long ReturnValidLong(string stringin)
    {
      if (stringin.Length <= 0)
        return 0;
      try
      {
        long retNum = int.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
        return retNum;
      }
      catch
      {
        return 0;
      }
    }


    
    public static string NewString(char chartorepeat, int intrepeatcount)
    {
      return new string(chartorepeat, intrepeatcount);
    }






    public static bool IsDate(string stringin)
    {
      DateTime stringoutdate;
      return DateTime.TryParse(stringin, out stringoutdate);
    }
    public static bool IsNumeric(string stringin)
    {
      if (stringin.Length <= 0)
        return false;
      try
      {
        double retNum;
        //isNum = Double.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        retNum = Double.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
        return true;
      }
      catch
      {
        return false;
      }
    }
    public static bool IsNumericAndPositive(string stringin)
    {
      //const string stringfunction = "IsNumericAndPositive";

      bool boolreturn = false;

      try
      {
        if (clsStringFunctions.IsNumeric(stringin))
        {
          if (Double.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo) > 0)
          {
            boolreturn = true;
          }
        }
      }
      catch 
      {
      }
      return boolreturn;
    }


    public static bool IsNumericAndNonZero(string Expression)
    {
      const string stringfunction = "IsNumericAndNonZero";

      bool boolreturn = false;

      try
      {
        if (clsStringFunctions.IsNumeric(Expression))
        {
          double dval = Double.Parse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
          if (Math.Abs(dval) > 0.00000000001)
          {
            boolreturn = true;
          }
        }
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, Expression, e1);
      }
      return boolreturn;
    }







    public static byte GetBitIntFromBitNumber(int intBit)
    {
      const double dtwo = 2.0;
      double dbit = Double.Parse(intBit.ToString());
      double dpow = Math.Pow(dtwo, dbit);
      return byte.Parse(dpow.ToString("0"));
    }

    // C# to convert a string to a byte array.
    public static byte[] StringToByteArray(string str)
    {
      //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      //return encoding.GetBytes(str);

      //how about this .. ??
      byte[] bytearrayreturn = new byte[str.Length];
      for (int intidx = 0; intidx < str.Length; intidx++)
      {
        //build binary array if not 7-bit ascii
        int intchar = clsStringFunctions.Asc(str.Substring(intidx, 1));
        bytearrayreturn[intidx] = byte.Parse(intchar.ToString());
      }
      return bytearrayreturn;
    }

    // C# to convert a byte array to a string.
    public static string ByteArrayToString(byte[] byteArray)
    {
      ////string str;
      ////don't use this if the data is above 7bit ASCII .. 
      ////so ASC<=127 = OK, else = fucked
      //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      ////Encoding encoding = Encoding.GetEncoding(1252);
      //string str = encoding.GetString(byteArray, 0, byteArray.Length);
      //return str;


      //how about this ...
      string stringreturn = "";
      for (int intidx = 0; intidx < byteArray.Length; intidx++)
      {
        stringreturn += clsStringFunctions.Chr(int.Parse(byteArray[intidx].ToString()));
      }
      return stringreturn;
    }

    public static int ByteToInt(byte bytein)
    {
      return int.Parse(bytein.ToString());
    }

    public static int ByteArrayIndexOf(byte[] batoserach, int inttofind)
    {
      int intreturn = -1;
      for (int idx = 0; idx<batoserach.GetUpperBound(0); idx++)
      {
        if ((int)batoserach[idx]==inttofind)
        {
          intreturn = idx;
          break;
        }
      }
      return intreturn;
    }
    public static int ByteArrayIndexOf(byte[] batoserach, int inttofind, int inttostart)
    {
      int intreturn = -1;
      for (int idx = inttostart; idx < batoserach.GetUpperBound(0); idx++)
      {
        if ((int)batoserach[idx] == inttofind)
        {
          intreturn = idx;
          break;
        }
      }
      return intreturn;
    }

    public static string ByteArrayToHexStringWithPad(byte[] data)
    {
      StringBuilder sb = new StringBuilder(data.Length * 3);
      foreach (byte b in data)
        sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
      return sb.ToString().ToUpper();
    }
    public static string ByteArrayToHexStringNoPad(byte[] data)
    {
      StringBuilder sb = new StringBuilder(data.Length * 2);
      foreach (byte b in data)
        sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
      return sb.ToString().ToUpper();
    }

    public static byte[] ByteArrayConcatenate(byte[] a, byte[] b)
    {
      byte[] c = new byte[a.Length + b.Length]; // just one array
      Buffer.BlockCopy(a, 0, c, 0, a.Length);
      Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
      return c;
    }

    public static int ByteArrayIndexOf(byte[] ByteArrayToSearch, byte[] ByteArrayToFind)
    {
      // Any encoding will do, as long as all bytes represent a unique character.
      Encoding encoding = Encoding.GetEncoding(1252);

      string toSearch = encoding.GetString(ByteArrayToSearch, 0, ByteArrayToSearch.Length);
      string toFind = encoding.GetString(ByteArrayToFind, 0, ByteArrayToFind.Length);
      int result = toSearch.IndexOf(toFind, StringComparison.Ordinal);
      return result;
    }

    public static byte[] ByteArraySubarray(byte[] a, int intstart, int intlength)
    {
      const string stringfunction = "ByteArraySubarray";
      byte[] c = new byte[intlength]; // just one array
      try
      {
        Buffer.BlockCopy(a, intstart, c, 0, intlength);
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, e1);
      }
      return c;
    }






    public static UInt32 CalculateCRC32(string stringin)
    {
      //initialize
      UInt32 unit32initialchecksum = 0;
      UInt32 unit32actualchecksum = 0;

      //accumulate
      unit32actualchecksum = AccumulateCRC32(stringin, unit32initialchecksum);

      //complete
      unit32actualchecksum = CompleteCRC32(unit32actualchecksum);

      //return actual;
      return unit32actualchecksum;
    }

    private static UInt32 AccumulateCRC32(string stringin, UInt32 unit32startchecksum)
    {
      byte[] bytearraylocal = clsStringFunctions.StringToByteArray(stringin);
      UInt32 unit32localchecksum = unit32startchecksum;

      for (int idx = 0; idx < stringin.Length; idx++)
      {
        unit32localchecksum = CRC32Table[bytearraylocal[idx] ^ unit32localchecksum & 255] ^ (0xFFFFFF & (unit32localchecksum >> 8));
      }
      return unit32localchecksum;
    }
    private static UInt32 CompleteCRC32(UInt32 unit32startchecksum)
    {
      return ~unit32startchecksum;
    }

    
    public static UInt32[] CRC32Table = new UInt32[] 
    {
      0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419,
      0x706af48f, 0xe963a535, 0x9e6495a3, 0x0edb8832, 0x79dcb8a4,
      0xe0d5e91e, 0x97d2d988, 0x09b64c2b, 0x7eb17cbd, 0xe7b82d07,
      0x90bf1d91, 0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de,
      0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856,
      0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9,
      0xfa0f3d63, 0x8d080df5, 0x3b6e20c8, 0x4c69105e, 0xd56041e4,
      0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b,
      0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3,
      0x45df5c75, 0xdcd60dcf, 0xabd13d59, 0x26d930ac, 0x51de003a,
      0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599,
      0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924,
      0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 0x76dc4190,
      0x01db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f,
      0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0x0f00f934, 0x9609a88e,
      0xe10e9818, 0x7f6a0dbb, 0x086d3d2d, 0x91646c97, 0xe6635c01,
      0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed,
      0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950,
      0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3,
      0xfbd44c65, 0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2,
      0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a,
      0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5,
      0xaa0a4c5f, 0xdd0d7cc9, 0x5005713c, 0x270241aa, 0xbe0b1010,
      0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f,
      0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17,
      0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad, 0xedb88320, 0x9abfb3b6,
      0x03b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x04db2615,
      0x73dc1683, 0xe3630b12, 0x94643b84, 0x0d6d6a3e, 0x7a6a5aa8,
      0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1, 0xf00f9344,
      0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb,
      0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a,
      0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5,
      0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1,
      0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c,
      0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef,
      0x4669be79, 0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236,
      0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe,
      0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31,
      0x2cd99e8b, 0x5bdeae1d, 0x9b64c2b0, 0xec63f226, 0x756aa39c,
      0x026d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x05005713,
      0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38, 0x92d28e9b,
      0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21, 0x86d3d2d4, 0xf1d4e242,
      0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1,
      0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c,
      0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45, 0xa00ae278,
      0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7,
      0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66,
      0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,
      0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605,
      0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8,
      0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b,
      0x2d02ef8d
    };






    //public static DateTime ConvertJulianToDateTime(double julianDate)
    //{
    //  DateTime date;
    //  double dblA, dblB, dblC, dblD, dblE, dblF;
    //  double dblZ, dblW, dblX;
    //  int day, month, year;
    //  try
    //  {
    //    dblZ = Math.Floor(julianDate + 0.5);
    //    dblW = Math.Floor((dblZ - 1867216.25) / 36524.25);
    //    dblX = Math.Floor(dblW / 4);
    //    dblA = dblZ + 1 + dblW - dblX;
    //    dblB = dblA + 1524;
    //    dblC = Math.Floor((dblB - 122.1) / 365.25);
    //    dblD = Math.Floor(365.25 * dblC);
    //    dblE = Math.Floor((dblB - dblD) / 30.6001);
    //    dblF = Math.Floor(30.6001 * dblE);
    //    day = Convert.ToInt32(dblB - dblD - dblF);
    //    if (dblE > 13)
    //    {
    //      month = Convert.ToInt32(dblE - 13);
    //    }
    //    else
    //    {
    //      month = Convert.ToInt32(dblE - 1);
    //    }
    //    if ((month == 1)(month == 2))
    //    {
    //      year = Convert.ToInt32(dblC - 4715);
    //    }
    //    else
    //    {
    //      year = Convert.ToInt32(dblC - 4716);
    //    }
    //    date = new DateTime(year, month, day);
    //    return date;
    //  }
    //  catch (ArgumentOutOfRangeException ex)
    //  {
    //    MessageBox.Show("Julian date could not be converted:\n" + ex.Message, "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    date = new DateTime(0);
    //  }
    //  catch (Exception ex)
    //  {
    //    MessageBox.Show("Error converting Julian date:\n" +
    //    ex.Message, "Conversion Error", MessageBoxButtons.OK,
    //    MessageBoxIcon.Error);
    //    date = new DateTime(0);
    //  }
    //  return date;
    //}


    //public double ConvertDateTimeToJulianDouble(DateTime datetimein)
    //{
    //  //customized for sqlite:

    //  //12. DDDDDDDDDD
    //  //Format 12 is the Julian day number expressed as a floating point value. 

    //  //The Julian date (JD) is the interval of time in days and fractions of a day, since January 1, 4713 BC Greenwich noon, Julian proleptic calendar.
    //  //In precise work, the timescale, e.g., Terrestrial Time (TT) or Universal Time (UT), should be specified.

    //  //The Julian day number (JDN)[3] is the integral part of the Julian date (JD).[4] Negative values can also be used, 
    //  //although those predate all recorded history. Now, at 08:18, Saturday February 28, 2009 (UTC) the Julian day number is 2454890.

    //  //A Julian date of 2454115.05486 means that the date and Universal Time is Sunday January 14, 2007 at 13:18:59.9.

    //  //The decimal parts of a Julian date:
    //  //0.1 = 2.4 hours or 144 minutes or 8640 seconds
    //  //0.01 = 0.24 hours or 14.4 minutes or 864 seconds
    //  //0.001 = 0.024 hours or 1.44 minutes or 86.4 seconds
    //  //0.0001 = 0.0024 hours or 0.144 minutes or 8.64 seconds
    //  //0.00001 = 0.00024 hours or 0.0144 minutes or 0.864 seconds.

    //  //Almost 2.5 million Julian days have elapsed since the initial epoch. JDN 2,400,000 was November 16, 1858. 
    //  //JD 2,500,000.0 will occur on August 31, 2132 at noon UT.

    //  //If the Julian date of noon is applied to the entire midnight-to-midnight civil day centered on that noon,[5] 
    //  //rounding Julian dates (fractional days) for the twelve hours before noon up while rounding those after noon down, 
    //  //then the remainder upon division by 7 represents the day of the week, with 0 representing Monday, 1 representing Tuesday, 
    //  //and so forth. Now at 08:18, Saturday February 28, 2009 (UTC) the nearest noon JDN is 2454891 yielding a remainder of 5.

    //  //The Julian day number can be considered a very simple calendar, where its calendar date is just an integer. 
    //  //This is useful for reference, computations, and conversions. It allows the time between any two dates in history 
    //  //to be computed by simple subtraction.

    //  //The Julian day system was introduced by astronomers to provide a single system of dates that could be used when working 
    //  //with different calendars and to unify different historical chronologies. Apart from the choice of the zero point and 
    //  //name, this Julian day and Julian date are not directly related to the Julian calendar, although it is possible to 
    //  //convert any date from one calendar to the other.

    //  double doubleyear = 0;
    //  double doublemonth = 0;
    //  double doubleday = 0;
    //  double doublefractionalday = 0;

    //  //doubleyear = double.Parse(datetimein.D.ToString());
    //  //doublemonth = double.Parse(datetimein.Year.ToString());
    //  //doubleday = double.Parse(datetimein.Year.ToString());


    //}



  }
}

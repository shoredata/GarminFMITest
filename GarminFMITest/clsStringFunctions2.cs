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
  public class clsStringFunctions
  {
    public static string GetSQLValidString(string stringToProcess)
    {
      //as of 3/7/09 this is the same as the rwdb function ... required for importing sd files
      //03/6/2011 copied from rw project (same as rwdb)

      try
      {
        //int intlength = stringToProcess.Length;
        //string stringreplacechars =        @"!""&'(),/:;<=>?[\]^`{|}";
        //newFileName = Regex.Replace(title, @"[?:\/*""<>|]", "");
        //newFileName = Regex.Replace(title, @"[!&'(),?:;\/=""<>^`{}|]", "");
        const string stringnewchar = " ";
        //string stringlocal = System.Text.RegularExpressions.Regex.Replace(stringToProcess, @"[!&'(),?:;\/=""<>^`{}|]", stringnewchar);
        //need , and need &
        string stringlocal = System.Text.RegularExpressions.Regex.Replace(stringToProcess, @"[!'()?:;\/=""<>^`{}|]", stringnewchar);
        for (int idx = 0; idx < stringlocal.Length; idx++)
        {
          // ReSharper disable RedundantCast
          if (((int)stringlocal[idx] < 32) || ((int)stringlocal[idx] > 126))
            // ReSharper restore RedundantCast
            stringlocal = stringlocal.Replace(stringlocal[idx], ' ');
        }
        stringlocal = stringlocal.Trim();
        //if (stringlocal.Length != intlength)
        //  clsDebugFunctions.LineOut("Truncated '" + stringToProcess + "' --> '" + stringlocal + "'");
        return stringlocal;
      }
      catch (Exception e1)
      {
        clsDebug.MsgOut("GetSQLValidString", stringToProcess, e1);
        return new string(' ', stringToProcess.Length);
      }
    }


    public void TestGPSFormats()
    {
      //this is current as of 2011/09/09

      double doublelat = 42.512047;
      double doublelong = -71.034106;
      string stringoutputddformat = doublelat.ToString() + "," + doublelong.ToString();

      string stringtemplat = clsStringFunctions.DDtoDMmmmm(doublelat, true);
      string stringtemplong = clsStringFunctions.DDtoDMmmmm(doublelong, false);
      string stringoutputsdforamt = stringtemplat + stringtemplong;

      double doublefinallat = clsStringFunctions.DMmmmtoDD(stringtemplat, true);
      double doublefinallong = clsStringFunctions.DMmmmtoDD(stringtemplong, false);
      string stringoutputtestddformat = doublefinallat.ToString() + "," + doublefinallong.ToString();

      clsDebug.LineOut(stringoutputddformat);
      clsDebug.LineOut(stringoutputsdforamt);
      clsDebug.LineOut(stringoutputtestddformat);
    }

    public static string DDtoDMmmmm(double ddecimaldegrees, bool boollatitude)
    {
      double dddm = ddecimaldegrees;
      if (double.IsNaN(dddm))
      {
        return string.Empty;
      }

      if (boollatitude)
      {
        if (Math.Abs(dddm) > 90.0000)
        {
          return string.Empty;
        }
      }
      else
      {
        if (Math.Abs(dddm) > 180.0000)
        {
          return string.Empty;
        }
      }

      bool neg = dddm < 0d;
      dddm = Math.Abs(dddm);

      double d = Math.Floor(dddm);
      dddm -= d;
      dddm *= 60;
      double m = Math.Floor(dddm);
      dddm -= m;
      double s = Math.Round(dddm * 10000);

      string dms = "";
      if (boollatitude)
      {
        dms = string.Format("{0:00}{1:00}{2:0000}", d, m, s);
        dms += neg ? "S" : "N";
      }
      else
      {
        dms = string.Format("{0:000}{1:00}{2:0000}", d, m, s);
        dms += neg ? "W" : "E";
      }

      return dms;
    }

    public static double DMmmmtoDD(string sDddMMmmmmin, bool boollatitude)
    {
      double doublereturn = 0;
      int intlength = 10;
      int intdegrees = 3;
      if (boollatitude)
      {
        intlength = 9;
        intdegrees = 2;
      }

      string stringDegrees;
      string stringMinutes;
      string stringmmmm;
      double doubleDegrees;
      double doubleMinutes;

      if (sDddMMmmmmin.Length == intlength)
      {
        if (clsStringFunctions.IsNumeric(sDddMMmmmmin.Substring(0, intlength - 1)))
        {
          stringDegrees = sDddMMmmmmin.Substring(0, intdegrees);
          stringMinutes = sDddMMmmmmin.Substring(intdegrees, 2);
          stringmmmm = sDddMMmmmmin.Substring(intdegrees + 2, 4);
          doubleDegrees = Double.Parse(stringDegrees);
          doubleMinutes = Double.Parse(stringMinutes + "." + stringmmmm);
          doubleDegrees = doubleDegrees + (doubleMinutes / 60);
          if ("SW".IndexOf(sDddMMmmmmin.Substring(intlength - 1, 1)) > -1)
          {
            doubleDegrees = -1 * doubleDegrees;
          }
          doublereturn = doubleDegrees;
        }
      }
      return doublereturn;
    }




    //public static string GetSQLValidString(string stringToProcess)
    //{
    //  //as of 3/7/09 this is the same as the rwdb function ... required for importing sd files

    //  try
    //  {
    //    //int intlength = stringToProcess.Length;
    //    //string stringreplacechars =        @"!""&'(),/:;<=>?[\]^`{|}";
    //    //newFileName = Regex.Replace(title, @"[?:\/*""<>|]", "");
    //    //newFileName = Regex.Replace(title, @"[!&'(),?:;\/=""<>^`{}|]", "");
    //    const string stringnewchar = " ";
    //    //string stringlocal = System.Text.RegularExpressions.Regex.Replace(stringToProcess, @"[!&'(),?:;\/=""<>^`{}|]", stringnewchar);
    //    //need , and need &
    //    string stringlocal = System.Text.RegularExpressions.Regex.Replace(stringToProcess, @"[!'()?:;\/=""<>^`{}|]", stringnewchar);
    //    for (int idx = 0; idx < stringlocal.Length; idx++)
    //    {
    //      // ReSharper disable RedundantCast
    //      if (((int)stringlocal[idx] < 32) || ((int)stringlocal[idx] > 126))
    //        // ReSharper restore RedundantCast
    //        stringlocal = stringlocal.Replace(stringlocal[idx], ' ');
    //    }
    //    stringlocal = stringlocal.Trim();
    //    //if (stringlocal.Length != intlength)
    //    //  clsDebugFunctions.LineOut("Truncated '" + stringToProcess + "' --> '" + stringlocal + "'");
    //    return stringlocal;
    //  }
    //  catch (Exception e1)
    //  {
    //    clsDebug.MsgOut("GetSQLValidString", stringToProcess, e1);
    //    return new string(' ', stringToProcess.Length);
    //  }
    //}


    public static string ReturnTitleString(string stringin)
    {
      const string stringequals = "==";
      return stringequals + " " + stringin + " " + stringequals;
    }


    public static string ConvertHexCharactersToASCIIString(string hexstring)
    {
      // An object storing the string value
      string StrValue = "";
      // While there's still something to convert in the hex string
      while (hexstring.Length > 0)
      {
        // Use ToChar() to convert each ASCII value (two hex digits) to the actual character
        StrValue += System.Convert.ToChar(System.Convert.ToUInt32(hexstring.Substring(0, 2), 16)).ToString();
        // Remove from the hex object the converted value
        hexstring = hexstring.Substring(2, hexstring.Length - 2);
      }
      System.Windows.Forms.MessageBox.Show("ConvertHexCharactersToASCIIString: Untested");
      return StrValue;
    }

    public static string ConvertStringToHexString(string asciiString, bool booltrailingspace)
    {
      string hex = "";
      string stringend = "";
      if (booltrailingspace)
        stringend = " ";
      foreach (char c in asciiString)
      {
        int tmp = c;
        // ReSharper disable RedundantCast
        hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString())) + stringend;
        // ReSharper restore RedundantCast
      }
      return hex;
    }


    public static string Chr(int intchar)
    {
      if ((intchar < 0) || (intchar > 255))
      {
        throw new ArgumentOutOfRangeException("intchar", "Must be between 0 and 255.");
      }
      byte[] bytBuffer = new byte[] { (byte)intchar };
      return Encoding.GetEncoding(1252).GetString(bytBuffer, 0, 1);
    }
    public static int Asc(string stringin)
    {
      // ReSharper disable RedundantCast
      //return (int)stringin.Substring(0, 1)[0];
      return (int)stringin[0];
      // ReSharper restore RedundantCast
    }
    public static int Asc(char charin)
    {
      // ReSharper disable RedundantCast
      return (int)charin;
      // ReSharper restore RedundantCast
    }
    //public static int Asc(char ch)
    //{
    //  return (int)ch;
    //}

    public static bool IsNumeric(string stringin)
    {
      if (stringin.Length <= 0)
        return false;
      try
      {

        //isNum = Double.Parse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
#pragma warning disable 168
        double retNum = Double.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
#pragma warning restore 168
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static bool IsDate(string stringin)
    {
      bool boolreturn = false;
      try
      {
        if (stringin.Length > 0)
        {
#pragma warning disable 168
          DateTime dtout = DateTime.Parse(stringin);
#pragma warning restore 168
          boolreturn = true;
        }
      }
      // ReSharper disable EmptyGeneralCatchClause
      catch
      // ReSharper restore EmptyGeneralCatchClause
      {
      }
      return boolreturn;
    }


    public static decimal ReturnValidDecimal(string stringin)
    {
      if (stringin.Length <= 0)
        return 0;
      try
      {
        decimal retNum = Decimal.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
        return retNum;
      }
      catch
      {
        return 0;
      }
    }
    public static double ReturnValidDouble(string stringin)
    {
      if (stringin.Length <= 0)
        return 0.0;
      try
      {
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
        long retNum = long.Parse(Convert.ToString(stringin), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
        return retNum;
      }
      catch
      {
        return 0;
      }
    }

    public static string ReturnValidNmemonicString(string stringtodoubleampersands)
    {
      return stringtodoubleampersands.Replace("&", "&&");
    }

    public static bool IsNumericAndPositive(string Expression)
    {
      const string stringfunction = "IsNumericAndPositive";

      bool boolreturn = false;

      try
      {
        if (clsStringFunctions.IsNumeric(Expression))
        {
          if (Double.Parse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo) > 0)
          {
            boolreturn = true;
          }
        }
      }
      catch (Exception e1)
      {
        clsDebug.LineOut(stringfunction, Expression, e1);
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
        clsDebug.LineOut(stringfunction, Expression, e1);
      }
      return boolreturn;
    }


    public static int GetAccountPageNumberFromString(string stringpage)
    {
      //int intchar = -1;
      int intpage = -1;
      if (stringpage.Length > 0)
      {
        int intchar = clsStringFunctions.Asc(stringpage);
        intpage = -1;
        //lots of fuckin magic numbers
        //A=0, 1-9=1-9, 0=10, B-Z=11-35
        if ((intchar >= 65) && (intchar <= 90))
        {
          //letters, only some valid
          if (intchar == 65)
          {
            intpage = 0;
          }
          else if ((intchar > 65) && (intchar <= 70)) //70=F = last
          {
            intpage = intchar - 55;
          }
        }
        else if ((intchar >= 48) && (intchar <= 57))
        {
          //numbers, all valid
          if (intchar == 48)
          {
            intpage = 10;
          }
          else if ((intchar >= 49) && (intchar <= 57))
          {
            intpage = intchar - 48;
          }
        }
      }
      return intpage;
    }

    public static string GetAccountPageStringFromNumber(int intpage)
    {
      // se the fn above for the reverse
      //lots of fuckin magic numbers
      //0=A(65), 1-9=1-9(49-57), 10=0(48), 11-35=B-Z(66-90)
      string stringpage = "";
      if (intpage == 0)
      {
        stringpage = "A";
      }
      else if ((intpage >= 1) && (intpage <= 9))
      {
        stringpage = clsStringFunctions.Chr(intpage + 48);
      }
      else if (intpage == 10)
      {
        stringpage = "0";
      }
      else if ((intpage >= 11) && (intpage <= 35))
      {
        stringpage = clsStringFunctions.Chr(intpage + 55);
      }
      return stringpage;
    }

    public static int GetTestPageNumberFromString(string stringpage)
    {
      int intpage = -1;
      if (stringpage.Length > 0)
      {
        int intchar = clsStringFunctions.Asc(stringpage);
        intpage = -1;
        //lots of fuckin magic numbers
        //1-9=1-9
        if ((intchar >= 65) && (intchar <= 90))
        {
          //letters
          if (intchar == 81)
          {
            intpage = 11; //q=11
          }
          else if (intchar == 87)
          {
            intpage = 12; //w=12
          }
          else if (intchar == 69)
          {
            intpage = 13; //e=13
          }
          else if (intchar == 82)
          {
            intpage = 14; //r=14
          }
        }
        else if ((intchar >= 48) && (intchar <= 57))
        {
          //numbers, not 0
          if ((intchar >= 49) && (intchar <= 57))
          {
            intpage = intchar - 48;
          }
        }
      }
      return intpage;
    }
    //public static string GetTestPageStringFromNumber(int intpage)
    //{
    //  // se the fn above for the reverse
    //  //lots of fuckin magic numbers
    //  //1-9=1-9(49-57)
    //  string stringpage = "";
    //  if ((intpage >= 1) && (intpage <= 9))
    //  {
    //    stringpage = clsStringFunctions.Chr(intpage + 48);
    //  }
    //  return stringpage;
    //}

    //public static string Pad(string stringin, int intlength, bool boolright)
    //{
    //  string stringreturn = "";
    //  if (boolright)
    //  {
    //    stringreturn = stringin.PadRight(intlength);
    //  }
    //  else
    //  {
    //    stringreturn = stringin.PadLeft(intlength);
    //  }
    //  return stringreturn;
    //}
    //public static string Pad(string stringin, int intlength, bool boolright, char chartopadwith)
    //{
    //  string stringreturn = "";
    //  if (boolright)
    //  {
    //    stringreturn = stringin.PadRight(intlength, chartopadwith);
    //  }
    //  else
    //  {
    //    stringreturn = stringin.PadLeft(intlength, chartopadwith);
    //  }
    //  return stringreturn;
    //}


    public static string GetFixedLengthPaddedString(string stringin, char charpad, int intlength, bool boolprepad, bool boolremovedecimalchar)
    {
      string stringlocal = stringin;
      if (boolremovedecimalchar)
        stringlocal = stringin.Replace(".", "");
      string stringreturn = "";
      if (stringlocal.Length < intlength)
      {
        if (boolprepad)
        {
          stringreturn = stringlocal.PadLeft(intlength, charpad);
        }
        else
        {
          stringreturn = stringlocal.PadRight(intlength, charpad);
        }
      }
      else if (stringlocal.Length > intlength)
      {
        if (boolprepad)
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
        clsDebug.LineOut("GetFixedLengthPaddedString", "stringreturn.Length<>intlength: " + stringreturn.Length.ToString() + "," + intlength.ToString());
      }
      return stringreturn;
    }





    public static string Mid(string stringin, int intstart, int intcount)
    {
      string stringreturn = "";
      if (stringin != null)
      {
        if (stringin.Length <= intstart)
        {
          //nothing
        }
        else
        {
          if (stringin.Length - intstart - intcount < 0)
          {
            stringreturn = stringin.Substring(intstart, stringin.Length - intstart);
          }
          else
          {
            stringreturn = stringin.Substring(intstart, intcount);
          }
        }
      }
      return stringreturn;
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

    public static string SafeBitsByteArrayToString(byte[] input)
    {
      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
      return enc.GetString(input, 0, input.Length);
    }

    public static byte[] ByteArrayToUpper(byte[] byteArray)
    {
      ////string str;
      ////don't use this if the data is above 7bit ASCII .. 
      ////so ASC<=127 = OK, else = fucked
      //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      ////Encoding encoding = Encoding.GetEncoding(1252);
      //string str = encoding.GetString(byteArray, 0, byteArray.Length);
      //return str;

      byte[] c = new byte[byteArray.Length]; // just one array

      //how about this ...
      //string stringreturn = "";
      for (int intidx = 0; intidx < byteArray.Length; intidx++)
      {
        c[intidx] = byteArray[intidx];
        if ((c[intidx] <= 122) && (c[intidx] >= 97))
          c[intidx] -= 32;
      }
      return c;
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

    public static int ByteToInt(byte bytein)
    {
      return int.Parse(bytein.ToString());
    }

    public static byte[] ByteArrayConcatenate(byte[] a, byte[] b)
    {
      byte[] c = new byte[a.Length + b.Length]; // just one array
      Buffer.BlockCopy(a, 0, c, 0, a.Length);
      Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
      return c;
    }

    //public static void ByteArrayToUppercase(ref byte[] data)
    //{
    //  int intcount = -1;
    //  foreach (byte b in data)
    //  {
    //    //condition ? first_expression : second_expression;
    //    intcount++;
    //    ((b>=97) && (b<=122)) ? data[intcount] = b-32 : data[intcount] = b;
    //  }
    //  return;
    //}
    public static bool SearchByteForPattern(byte[] pattern, byte[] bytes)
    {
      //int matches = 0;
      //bool boolfound = false;
      for (int i = 0; i < bytes.Length; i++)
      {
        if (pattern[0] == bytes[i] && bytes.Length - i >= pattern.Length)
        {
          bool ismatch = true;
          for (int j = 1; j < pattern.Length && ismatch == true; j++)
          {
            if (bytes[i + j] != pattern[j])
              ismatch = false;
          }
          if (ismatch)
          {
            //matches++;
            //i += pattern.Length - 1;
            return true;
          }
        }
      }
      //return matches;
      return false;
    }


    public static int ByteArrayIndexOfByteArrayCaseSensitive(byte[] ByteArrayToSearch, byte[] ByteArrayToFind)
    {
      // Any encoding will do, as long as all bytes represent a unique character.
      Encoding encoding = Encoding.GetEncoding(1252);

      string toSearch = encoding.GetString(ByteArrayToSearch, 0, ByteArrayToSearch.Length);
      string toFind = encoding.GetString(ByteArrayToFind, 0, ByteArrayToFind.Length);
      int result = toSearch.IndexOf(toFind, StringComparison.Ordinal);
      return result;
    }
    public static int ByteArrayIndexOfByteArrayIgnoreCase(byte[] ByteArrayToSearch, byte[] ByteArrayToFind)
    {
      // Any encoding will do, as long as all bytes represent a unique character.
      Encoding encoding = Encoding.GetEncoding(1252);

      string toSearch = encoding.GetString(ByteArrayToSearch, 0, ByteArrayToSearch.Length);
      string toFind = encoding.GetString(ByteArrayToFind, 0, ByteArrayToFind.Length);
      int result = toSearch.IndexOf(toFind, StringComparison.OrdinalIgnoreCase);
      return result;
    }
    public static int ByteArrayIndexOfByteArrayIgnoreCase(byte[] ByteArrayToSearch, byte[] ByteArrayToFind, int intStartPosition)
    {
      // Any encoding will do, as long as all bytes represent a unique character.
      Encoding encoding = Encoding.GetEncoding(1252);

      string toSearch = encoding.GetString(ByteArrayToSearch, 0, ByteArrayToSearch.Length);
      string toFind = encoding.GetString(ByteArrayToFind, 0, ByteArrayToFind.Length);
      int result = toSearch.IndexOf(toFind, intStartPosition, StringComparison.OrdinalIgnoreCase);
      return result;
    }
    public static int ByteArrayIndexOfStringIgnoreCase(byte[] ByteArrayToSearch, string stringToFind)
    {
      // Any encoding will do, as long as all bytes represent a unique character.
      Encoding encoding = Encoding.GetEncoding(1252);

      string toSearch = encoding.GetString(ByteArrayToSearch, 0, ByteArrayToSearch.Length);
      //string toFind = encoding.GetString(ByteArrayToFind, 0, ByteArrayToFind.Length);
      int result = toSearch.IndexOf(stringToFind, StringComparison.OrdinalIgnoreCase);
      return result;
    }

    public static int ByteArrayIndexOfInt(byte[] batoserach, int inttofind)
    {
      int intreturn = -1;
      for (int idx = 0; idx < batoserach.GetUpperBound(0); idx++)
      {
        if ((int)batoserach[idx] == inttofind)
        {
          intreturn = idx;
          break;
        }
      }
      return intreturn;
    }
    public static int ByteArrayIndexOfInt(byte[] batoserach, int inttofind, int inttostart)
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
        clsDebug.LineOut(stringfunction, e1);
      }
      return c;
    }



    public static string InsertDecimalCharForTenths(string stringin)
    {
      if (stringin.Length > 1)
        return stringin.Substring(0, stringin.Length - 1) + "." + stringin.Substring(stringin.Length - 1, 1);
      if (stringin.Length == 1)
        return "." + stringin;
      return "";
    }

    public static string GetString(char charin, int intlength)
    {
      return new string(charin, intlength);
    }
    //public static string GetStringOfChars(string stringChar, int intCount)
    //{
    //  string stringReturn="";
    //  int i;
    //  if (intCount == 0)
    //    return "";
    //  for ( i=0; i<intCount; i++ )
    //    stringReturn += stringChar;
    //  StringBuilder sb = new StringBuilder(stringReturn);
    //  return sb.ToString();
    //}

    public static string ReturnPadFixedString(string stringin, int intlength, bool boolpadright)
    {
      const string stringfunction = "ReturnPadFixedString";
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
        clsDebug.LineOut(stringfunction, e1);
        stringreturn = GetString(' ', intlength);
      }
      return stringreturn;
    }




    public static string ReturnDiscountAndTaxRateDisplay(string stringunitshort, string stringrate, string stringtype, bool booladdunits, string stringmaxdp)
    {
      string stringreturn = "";

      //if ((boolistax) && (stringtype.CompareTo("$") == 0))
      //{
        double doublelocalrate = clsStringFunctions.ReturnValidDouble(stringrate);
        string stringlocalrate = doublelocalrate.ToString("#0.00###");
        stringrate = stringlocalrate;
      //}

      if (stringrate.Trim().Length > 0)
      {
        if (stringtype.CompareTo("$") == 0)
        {
          stringreturn = "$" + stringrate;
          if (booladdunits)
          {
            stringreturn += "/" + stringunitshort;
          }
        }
        else
        {
          stringreturn = stringrate + "%";
        }
      }
      return stringreturn;
    }


    public static string FormatPriceString(string stringin)
    {

      //used for tax rates, discount rates, and prices

      const string stringfunction = "FormatPriceString";
      string stringlocal = stringin;
      string stringreturn = "";

      int intlength = stringlocal.Length;

      try
      {
        if (intlength <= 0)
        {
          stringreturn = "";
        }
        else
        {
          double doublelocal = ReturnValidDouble(stringlocal);
          stringreturn = doublelocal.ToString("#0.00###"); //THIS DOESN'T WORK WHEN CALLED TO FIX DP FOR WRITING PRIOCE TO SD FIEL IN WIN7
        }
      }
      catch (Exception e1)
      {
        clsDebug.LineOut(stringfunction, e1);
        stringreturn = GetString(' ', intlength);
      }
      return stringreturn;
    }


    public static string FormatLatLongString(string stringin)
    {

      const string stringfunction = "FormatLatLongString";
      string stringlocal = stringin;
      string stringreturn = "";

      int intlength = stringlocal.Length;

      try
      {
        if (intlength <= 0)
        {
          stringreturn = "";
        }
        else
        {
          double doublelocal = ReturnValidDouble(stringlocal);
          stringreturn = doublelocal.ToString("#0.000000");
        }
      }
      catch (Exception e1)
      {
        clsDebug.LineOut(stringfunction, e1);
        stringreturn = GetString(' ', intlength);
      }
      return stringreturn;
    }
    public static string GetLatLongDDStringForSDFile(double doublelat, double doublelong)
    {
      const string stringfunction = "GetLatLongDDStringForSDFile";
      string stringreturn = "";

      try
      {
        string stringlat = doublelat.ToString("#0.000000");
        if (stringlat.Length > 10)
        {
          stringlat = stringlat.Substring(0, 10);
        }
        string stringlong = doublelong.ToString("#0.000000");
        if (stringlong.Length > 11)
        {
          stringlong = stringlong.Substring(0, 11);
        }
        stringreturn = stringlat + "," + stringlong;
        if (stringreturn.Length < 22)
        {
          stringreturn = stringreturn + GetString(' ', 22 - stringreturn.Length);
        }
        else if (stringreturn.Length > 22)
        {
          stringreturn = stringreturn.Substring(0, 22);
        }
      }
      catch (Exception e1)
      {
        clsDebug.LineOut(stringfunction, e1);
        stringreturn = GetString('0', 22);
      }
      return stringreturn;
    }



    public static string FormatDollarsString(string stringin)
    {
      const string stringfunction = "FormatDollarsString";
      string stringlocal = stringin;
      string stringreturn = "";

      int intlength = stringlocal.Length;

      try
      {
        if (intlength <= 0)
        {
          stringreturn = "";
        }
        else
        {
          double doublelocal = ReturnValidDouble(stringlocal);
          //stringreturn = doublelocal.ToString("#,##0.00");
          stringreturn = doublelocal.ToString("###0.00");
        }
      }
      catch (Exception e1)
      {
        clsDebug.LineOut(stringfunction, e1);
        stringreturn = GetString(' ', intlength);
      }
      return stringreturn;
    }


    public static string FormatVolumeString(string stringin)
    {
      const string stringfunction = "FormatVolumeString";
      string stringlocal = stringin;
      string stringreturn = "";

      int intlength = stringlocal.Length;

      try
      {
        if (intlength <= 0)
        {
          stringreturn = "";
        }
        else
        {
          double doublelocal = ReturnValidDouble(stringlocal);
          stringreturn = doublelocal.ToString("#0.0");
        }
      }
      catch (Exception e1)
      {
        clsDebug.LineOut(stringfunction, e1);
        stringreturn = GetString(' ', intlength);
      }
      return stringreturn;
    }









  }
}

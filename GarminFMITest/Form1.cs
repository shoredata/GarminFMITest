using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace Garmin_FMI_Test
{
  public partial class Form1 : Form
  {



    public Form1()
    {
      InitializeComponent();

      comboport.Items.Clear();
      foreach (string stringtemplabel in System.IO.Ports.SerialPort.GetPortNames())
      {
        comboport.Items.Add(stringtemplabel);
      }

      clsApplicationFunctions.VerifyAndCreateAppDataFolders();

      //OnCOMDataAcquiredEvent += new DataCOMAcquired(ThreadReportsCOMDataAquiredEvent);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      const string stringfunction = "Form1_FormClosing";
      //timer1.Enabled = false;
      if (serialPort1.IsOpen)
      {
        e.Cancel = true; 
        System.Threading.Thread CloseDown = new System.Threading.Thread(new System.Threading.ThreadStart(CloseSerialOnExit)); //close port in new thread to avoid hang
        CloseDown.Start(); 
      }
    }

    bool boolactivated = false;
    private void Form1_Activated(object sender, EventArgs e)
    {
      if (boolactivated) return;

      boolactivated = true;
      if (comboport.SelectedIndex < 0)
        comboport.SelectedIndex = 0;
    }


    




    private byte[] bacomportbuffer = new byte[] { };
    //private int intcomportbuffercount = 0;

    // This is the format the delegate method
    // will use when invoking back to the main gui
    // thread.
    public delegate void DataCOMAcquired(object sender);

    // The main thread will check for events
    // invoked by subscribed threads here.
    // This is the subscription point for the 
    // threads.
    //public event DataCOMAcquired OnCOMDataAcquiredEvent;

    //private void ThreadReportsCOMDataAquiredEvent(object sender)
    //{
    //  boolbytesreceived = true;
    //  byte[] bacomportbuffer = bacomportbuffer;
    //  labelrx.Text = clsStringFunctions.ByteArrayToString(bacomportbuffer);
    //  //stringcomportbuffer = ""; //the 'sender' needs to handle this data ...
    //}

    private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
      //stringcomportbuffer = stringcomportbuffer + serialPort1.ReadExisting();
      //labelrx.BeginInvoke(this.OnCOMDataAcquiredEvent, new object[] { stringcomportbuffer });

      const string stringfunction = "serialPort1_DataReceived";
      try
      {
        int bytes = serialPort1.BytesToRead;
        byte[] buffer = new byte[bytes];
        serialPort1.Read(buffer, 0, bytes);
        if (bacomportbuffer.Length > 0)
        {
          byte[] tempbuffer1 = new byte[bacomportbuffer.Length];
          bacomportbuffer.CopyTo(tempbuffer1, 0);
          bacomportbuffer = new byte[bytes + bacomportbuffer.Length];
          bacomportbuffer = clsStringFunctions.ByteArrayConcatenate(tempbuffer1, buffer);
        }
        else
        {
          bacomportbuffer = new byte[bytes];
          buffer.CopyTo(bacomportbuffer, 0);
        }
        if (bacomportbuffer.Length > serialPort1.ReadBufferSize)
        {
          bacomportbuffer = new byte[] { };
        }
      }
      catch (Exception e1)
      {
        //clsDebug.LineOut(stringfunction, "Reading Port.InputBuffer Bytes", e1);
      }
    }






    private void CloseSerialOnExit()
    {
      try
      {
        if (serialPort1.IsOpen)
        {
          SetSerialPort(serialPort1, false);
        }
      }
      catch (Exception e1)
      {
        //clsDebugFunctions.LineOut("CloseSerialOnExit", "Error Closing COM Ports", e1);
      }
      this.Invoke(new EventHandler(NowClose)); 
    }
    private void NowClose(object sender, EventArgs e)
    {
      this.Close(); 
    }


    private void SetSerialPort(System.IO.Ports.SerialPort sspport, bool benable)
    {
      try
      {
        //clsDebug.LineOut("SetSerialPort(" + sspport.PortName.ToString() + ", " + benable.ToString() + ")");
        if (benable)
        {
          if (sspport.IsOpen)
          {
            sspport.Close();
          }

          ////Garmin com-port settings:
          //
          //dcb.fBinary = TRUE;
          //dcb.BaudRate = CBR_9600;
          //dcb.ByteSize = 8;
          //dcb.StopBits = ONESTOPBIT;
          //dcb.Parity = NOPARITY;
          //dcb.fParity = FALSE;
          //dcb.fAbortOnError = FALSE;
          //dcb.fOutxCtsFlow = FALSE;
          //dcb.fOutxDsrFlow = FALSE;
          //dcb.fDtrControl = DTR_CONTROL_DISABLE;
          //dcb.fRtsControl = RTS_CONTROL_DISABLE;
          //dcb.fDsrSensitivity = FALSE;
          //dcb.fTXContinueOnXoff = FALSE;
          //dcb.fOutX = FALSE;
          //dcb.fInX = FALSE;
          //dcb.fNull = FALSE;
          //
          //time_outs.ReadIntervalTimeout = 0;
          //time_outs.ReadTotalTimeoutMultiplier = 0;
          //time_outs.ReadTotalTimeoutConstant = 0;
          //time_outs.WriteTotalTimeoutMultiplier = 0;
          //time_outs.WriteTotalTimeoutConstant = 0;



          if (comboport.SelectedIndex > -1)
            sspport.PortName = comboport.SelectedItem.ToString();

          sspport.BaudRate = 9600; //intbaudrate; //variable, from screen
          sspport.Parity = System.IO.Ports.Parity.None;
          sspport.DataBits = 8;
          sspport.StopBits = System.IO.Ports.StopBits.One;
          //sspport.ReadBufferSize = 4096; 
          //sspport.WriteBufferSize = 4096; 
          //sspport.DtrEnable = false;
          //sspport.RtsEnable = false;
          //sspport.ReceivedBytesThreshold = 1; //1=default , 82=?
          //sspport.Handshake = System.IO.Ports.Handshake.None;
          if (!sspport.IsOpen)
          {
            sspport.Open();
            AddToList("Port Opened");
            sspport.DiscardInBuffer();
          }

          if (checkpcm.Checked)
          {
            //connect host to aux port
            byte[] bytes = new byte[] { 31, 4 };
            for (int idx = 1; idx <= 5; idx++)
            {
              ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytes));
              System.Threading.Thread.Sleep(10);
            }
            AddToList("PCM AUX Opened");
          }
        }
        else
        {
          if (sspport.IsOpen)
          {
            if (checkpcm.Checked)
            {
              //disconect host from aux port
              byte[] bytes = new byte[] { 255 };
              for (int idx = 1; idx <= 5; idx++)
              {
                ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytes));
                System.Threading.Thread.Sleep(10);
              }
              AddToList("PCM AUX Closed");
            }
            sspport.DtrEnable = false; //true;
            sspport.RtsEnable = false;
            System.Windows.Forms.Application.DoEvents();

            sspport.DiscardInBuffer();
            System.Windows.Forms.Application.DoEvents();

            sspport.DiscardInBuffer();
            System.Windows.Forms.Application.DoEvents();

            if (sspport.BytesToRead > 0)
            {
              string sshit = sspport.ReadExisting(); //hopefully help close
              System.Windows.Forms.Application.DoEvents();
            }

            sspport.Close();
            AddToList("Port Closed");
            System.Windows.Forms.Application.DoEvents();
          }
        }
      }
      catch (Exception e1)
      {
        string stringsetserialexceptionmessage = "";
        if (benable)
        {
          stringsetserialexceptionmessage = "Error Opening Port " + sspport.PortName.ToString(); ;
        }
        else
        {
          stringsetserialexceptionmessage = "Error Closing Port " + sspport.PortName.ToString(); ;
        }
        AddToList(stringsetserialexceptionmessage);
      }
    }

    
    
    public void ExecuteSend(System.IO.Ports.SerialPort serialporttouse, string stringtosend)
    {
      const string stringfunction = "ExecuteSend";
      string stringout = stringtosend;

      try
      {
        byte[] bytearraybytes = new byte[stringout.Length];
        bool boolnon7bit = false;
        for (int intidx = 0; intidx < stringout.Length; intidx++)
        {
          //build binary array if not 7-bit ascii
          int intchar = clsStringFunctions.Asc(stringtosend.Substring(intidx, 1));
          if (intchar > 127)
          {
            boolnon7bit = true;
          }
          bytearraybytes[intidx] = byte.Parse(intchar.ToString());
        }
        if (boolnon7bit)
        {
          serialporttouse.Write(bytearraybytes, 0, stringout.Length);
        }
        else
        {
          serialporttouse.Write(stringout);
        }
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, stringtosend, e1);
      }
    }



    public byte[] GetFMIChecksum(byte[] bytestosend)
    {
      const string stringfunction = "GetFMIChecksum";
      byte[] bytesreturn = new byte[] { 0 };
      byte bytesum = 0;

      //their documentation is bullshit - don't use stupid crc32 function

      try
      {
        for (int idx = 0; idx <= bytestosend.GetUpperBound(0); idx++)
        {
          bytesum += bytestosend[idx];
        }
        int intsum = Convert.ToInt16(bytesum);
        intsum -= 256;
        intsum = Math.Abs(intsum);
        bytesum = Convert.ToByte(intsum);
      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, "Error Calculating FMIChecksum", e1);
      }
      bytesreturn[0] = bytesum;
      return bytesreturn;
    }

    
    
    private void AddToList(string stringtext)
    {
      InsertToList(listoutput, stringtext);
    }
    private void AddHexToList(byte[] bytearray)
    {
      InsertToList(listoutput, clsStringFunctions.ByteArrayToHexStringWithPad(bytearray));
    }
    private void AddHexToList(string stringtext)
    {
      byte[] bytearray = clsStringFunctions.StringToByteArray(stringtext);
      //InsertToList(listoutput, stringtext);
      InsertToList(listoutput, clsStringFunctions.ByteArrayToHexStringWithPad(bytearray));
    }
    private void InsertToList(ListBox listin, string stringin)
    {
      if (listin.Items.Count > 1000)
      {
        listin.Items.Clear();
      }
      listin.Items.Add(DateTime.Now + " " + stringin);
      listin.SelectedIndex = listin.Items.Count - 1;
      clsDebugFunctions.LineOut(DateTime.Now + " " + stringin);
    }




    //private enum uint16_fmi_id_type
    //{
    //  FMI_ID_ENABLE                       = 0x0000,
    //  FMI_ID_PRODUCT_ID_SUPPORT_RQST      = 0x0001,
    //  FMI_ID_PRODUCT_ID_DATA              = 0x0002,
    //  FMI_ID_PROTOCOL_DATA                = 0x0003,

    //  FMI_ID_TEXT_MSG_ACK                 = 0x0020,
    //  FMI_ID_TEXT_MSG_OPEN_FROM_SERVER    = 0x0021,
    //  FMI_ID_TEXT_MSG_SIMPLE_ACK          = 0x0022,
    //  FMI_ID_TEXT_MSG_YES_NO_RES          = 0x0023,
    //  FMI_ID_TEXT_MSG_OPEN_FROM_CLIENT    = 0x0024,
    //  FMI_ID_TEXT_MSG_RECEIPT             = 0x0025,

    //  FMI_ID_A602_STOP                    = 0x0100,
    //  FMI_ID_A603_STOP                    = 0x0101,

    //  FMI_ID_ETA_DATA_REQ                 = 0x0200,
    //  FMI_ID_ETA_DATA                     = 0x0201,
    //  FMI_ID_ETA_DATA_RECEIPT             = 0x0202,

    //  FMI_ID_STOP_STATUS_REQ              = 0x0210,
    //  FMI_ID_STOP_STATUS                  = 0x0211,
    //  FMI_ID_STOP_STATUS_RECEIPT          = 0x0212,

    //  FMI_ID_AUTO_ARRIVAL                 = 0x0220,
    //  FMI_ID_DATA_DELETION                = 0x0230,

    //  FMI_ID_END                          = 0xFFFF
    //}; //uint16_fmi_id_type



    //sadata[0]0=fmipacketid
    //sadata[1-n] = data based on fmipacketid
    public bool ParseBufferForData(ref byte[] barxbuffer, ref bool boolackonly, ref bool boolnackonly, ref string[] sadata, ref byte[] basinglemessage)
    {
      const string stringfunction = "ParseBufferForData";

    Retry_ParseBufferForData:

      boolackonly = false;
      boolnackonly = false;
      sadata = new string[] { };
      basinglemessage = new byte[] { };


      //string stringlocal = "";
      bool boolreturn = false;
      int intmincommandlength = 6; //minimum
      int intthiscommandlength = 0;

      try
      {
        if (barxbuffer.Length < intmincommandlength)
        {
          //bail
          AddToList(" -- Reponse too short: " + barxbuffer.Length.ToString() + " bytes");
        }
        else
        {
          //continue
          AddToList(" -- Testing " + barxbuffer.Length.ToString() + " bytes");
          AddHexToList(barxbuffer);

          // 0   1        2    3                4         5       6   7 
          //DLE PacketID Size FMIPacketID+FMIPacketData Checksum DLE EOT
          //
          //            size     FMIID+data             checksum        can be dle-stuffed ...


          AddToList(" -- Looking for first DLE");
          int intfirstdle = clsStringFunctions.ByteArrayIndexOf(barxbuffer, 16);
          if (intfirstdle <= -1)
          {
            //bail
            AddToList(" -- DLE not found");
          }
          else
          {
            //continue
            AddToList(" -- DLE in 0-based position: " + intfirstdle.ToString());
            if (intfirstdle + intmincommandlength > barxbuffer.Length)
            {
              //bail
              AddToList(" -- Response too short (a)");
            }
            else
            {
              //continue

              //make sure next char isn't ETX...
              if (barxbuffer[intfirstdle + 1] == 3)
              {
                //dump to after EOT and start over ...
                AddToList(" -- DLE EOT found, dumping both (a) ...");
                barxbuffer = clsStringFunctions.ByteArraySubarray(barxbuffer, intfirstdle + 2, barxbuffer.Length - intfirstdle - 2);
                goto Retry_ParseBufferForData;
              }

              //make sure next char isn't DLE, otherwise was stuffed im middle of other message = skip both
              if (barxbuffer[intfirstdle + 1] == 16)
              {
                //dump to after 2nd DLE and start over ...
                AddToList(" -- DLE DLE orphan found, dumping both (b) ...");
                barxbuffer = clsStringFunctions.ByteArraySubarray(barxbuffer, intfirstdle + 2, barxbuffer.Length - intfirstdle - 2);
                goto Retry_ParseBufferForData;
              }

              //check size for dle-stuff
              //if size is dle, dump next char from buffer
              if (barxbuffer[intfirstdle + 2] == 16)
              {
                AddToList(" -- DLE DLE stuffed in size, removing 2nd (c) ...");
                RemoveItemFromByteArray(ref barxbuffer, intfirstdle + 3);
              }

              int intsize = clsStringFunctions.ByteToInt(barxbuffer[intfirstdle + 2]);
              AddToList(" -- Data-Packet Size = " + intsize.ToString() + " bytes ");

              intthiscommandlength = 1 + 1 + 1 + intsize + 1 + 1 + 1;
              if (barxbuffer.Length < intthiscommandlength)
              {
                //bail
                AddToList(" -- Not enough data for command (a)");
              }
              else
              {
                //continue

                int intpacketid = clsStringFunctions.ByteToInt(barxbuffer[intfirstdle + 1]);
                AddToList(" -- Packet ID = " + intpacketid.ToString());

                int intfmipacketid = TwoBytesToInt(barxbuffer, intfirstdle + 3);
                AddToList(" -- FMI Packet ID = " + intfmipacketid.ToString());

                //check packet-data for dle-stuff
                //if fmipacketid/packet-data is dle, dump next char from buffer
                for (int idx = 0; idx < intsize; idx++)
                {
                  if (barxbuffer[intfirstdle + 3 + idx] == 16)
                  {
                    AddToList(" -- DLE DLE stuffed in packet-data[" + idx.ToString() + "], removing 2nd (d) ...");
                    RemoveItemFromByteArray(ref barxbuffer, intfirstdle + 4 + idx);
                  }
                }

                //check checksum for dle-stuff
                //if checksum is dle, dump next char from buffer
                if (barxbuffer[intfirstdle + 3 + intsize] == 16)
                {
                  AddToList(" -- DLE DLE stuffed in checksum removing 2nd (e) ...");
                  RemoveItemFromByteArray(ref barxbuffer, intfirstdle + 4 + intsize);
                }

                byte[] batest = clsStringFunctions.ByteArraySubarray(barxbuffer, intfirstdle, intthiscommandlength);
                if (intfirstdle + intthiscommandlength < barxbuffer.Length)
                {
                  //more chars in buffer, leave them
                  barxbuffer = clsStringFunctions.ByteArraySubarray(barxbuffer, intfirstdle + intthiscommandlength, barxbuffer.Length - intfirstdle - intthiscommandlength);
                  AddToList(" -- Leaving " + barxbuffer.Length.ToString() + " extra bytes: ");
                  AddHexToList(barxbuffer);
                }
                else
                {
                  AddToList(" -- RX Buffer emptied ");
                  barxbuffer = new byte[] { };
                }


                AddToList(" == Final Message to Test: ");
                AddHexToList(batest);

                int intcharstartdle_0 = clsStringFunctions.ByteToInt(batest[0]);
                int intcharpacketid_1 = clsStringFunctions.ByteToInt(batest[1]);
                int intcharchecksum = clsStringFunctions.ByteToInt(batest[batest.Length - 3]);
                int intcharenddle_6 = clsStringFunctions.ByteToInt(batest[batest.Length - 2]);
                int intcharendeot_7 = clsStringFunctions.ByteToInt(batest[batest.Length - 1]);

                //checksum:
                // 0 1 2 3 4 5 6 7 
                // N Y Y Y Y N N N 
                byte[] batestforchecksum = clsStringFunctions.ByteArraySubarray(batest, 1, batest.Length - 4);
                //bytechecksum = GetFMIChecksum(bytearrayheader);
                int intactualchecksum = clsStringFunctions.ByteToInt(GetFMIChecksum(batestforchecksum)[0]);

                if (intcharstartdle_0 != 16)
                {
                  AddToList(" -- Invalid message, char 0 != DLE: " + intcharstartdle_0.ToString());
                }
                else
                {
                  if (intcharenddle_6 != 16)
                  {
                    AddToList(" -- Invalid message, char 6 != DLE: " + intcharenddle_6.ToString());
                  }
                  else
                  {
                    if (intcharendeot_7 != 3)
                    {
                      AddToList(" -- Invalid message, char 7 != ETX: " + intcharendeot_7.ToString());
                    }
                    else
                    {
                      if (intcharchecksum != intactualchecksum)
                      {
                        AddToList(" -- Invalid message, char 5 checksum mismatch .. sent: " + intcharchecksum.ToString() + ", actual: " + intactualchecksum.ToString());

                        //TODO:
                        //    //if checksum <> calc checksum (all but byte 0)
                        //    // send nack
                        //    //else
                        //    // send ack
                        //    //end if

                      }
                      else
                      {
                        bool boolmessageskipped = true;

                        AddToList(" ### PACKETID = " + intpacketid.ToString() + " ###");
                        AddToList(" ### FMIPACKETID = " + intfmipacketid.ToString() + " ###");
                        if (intpacketid == 6)
                        {
                          AddToList("** ACK **");

                          sadata = new string[2];
                          sadata[0] = intpacketid.ToString();
                          sadata[1] = intfmipacketid.ToString();

                          basinglemessage = new byte[batest.Length];
                          batest.CopyTo(basinglemessage, 0);

                          boolackonly = true;

                          boolmessageskipped = false;
                        }
                        else if (intpacketid == 21)
                        {
                          AddToList("** NACK **");

                          sadata = new string[2];
                          sadata[0] = intpacketid.ToString();
                          sadata[1] = intfmipacketid.ToString();

                          basinglemessage = new byte[batest.Length];
                          batest.CopyTo(basinglemessage, 0);

                          boolnackonly = true;

                          boolmessageskipped = false;
                        }
                        else if (intpacketid == 161)
                        {
                          switch (intfmipacketid)
                          {

                            case 2:
                              AddToList("** Product ID and Software Version **");

                              sadata = new string[4];
                              sadata[0] = intpacketid.ToString();
                              sadata[1] = intfmipacketid.ToString();

                              basinglemessage = new byte[batest.Length];
                              batest.CopyTo(basinglemessage, 0);

                              //int intproductid = TwoBytesToInt(batest, intfirstdle + 5);
                              byte[] baprodid = clsStringFunctions.ByteArraySubarray(batest,5,2);
                              int intproductid = BitConverter.ToInt16(baprodid, 0);
                              sadata[2] = intproductid.ToString();
                              AddToList(" PRODUCT_ID =  " + sadata[2]);

                              //int intsoftwareversion = TwoBytesToInt(batest, intfirstdle + 7);
                              byte[] basoftver = clsStringFunctions.ByteArraySubarray(batest, 7, 2);
                              int intsoftwareversion = BitConverter.ToInt16(basoftver, 0);
                              double doubleversion = intsoftwareversion / 100;
                              sadata[3] = doubleversion.ToString("#0.00");
                              AddToList(" SOFTWARE_VERSION =  " + sadata[3]);

                              boolmessageskipped = false;
                              break;

                            case 3:
                              AddToList("** Support Protocol List **");
                              int intmessages = (intsize - 2) / 3;
                              if (intmessages == 0)
                              {
                                AddToList(" -- ERROR .. Size = " + intsize.ToString());
                                sadata = new string[2];
                                sadata[0] = intpacketid.ToString();
                                sadata[1] = intfmipacketid.ToString();
                              }
                              else
                              {
                                AddToList(" -- " + intmessages.ToString() + " Protocols Supported:");
                                sadata = new string[intmessages + 2];
                                sadata[0] = intpacketid.ToString();
                                sadata[1] = intfmipacketid.ToString();
                                int intmessagenumber = 0;
                                for (int idx = 5; idx < (intsize - 2) + 5; idx = idx + 3)
                                {
                                  byte[] bletter = clsStringFunctions.ByteArraySubarray(batest, idx, 1);
                                  byte[] badata = clsStringFunctions.ByteArraySubarray(batest, idx + 1, 2);
                                  string stringletter = clsStringFunctions.ByteArrayToString(bletter);
                                  int intdata = TwoBytesToInt(badata, 0);
                                  string stringprotocol = stringletter + intdata.ToString("000");
                                  intmessagenumber++;
                                  sadata[intmessagenumber+1] = stringprotocol;
                                  AddToList(" -- Protocol #" + intmessagenumber.ToString() + ": " + stringprotocol);
                                }
                              }
                              basinglemessage = new byte[batest.Length];
                              batest.CopyTo(basinglemessage, 0);

                              boolmessageskipped = false;
                              break;


                            case 4:
                              AddToList("** Unicode Support Request **");
                              AddToList(" -- Ignore for now so it sends only ASCII");

                              sadata = new string[2];
                              sadata[0] = intpacketid.ToString();
                              sadata[1] = intfmipacketid.ToString();

                              basinglemessage = new byte[batest.Length];
                              batest.CopyTo(basinglemessage, 0);

                              boolmessageskipped = false;
                              break;


                          }//end-case
                        }
                        else if (intpacketid == 51)
                        {
                          //gps dta from pvt

                          sadata = new string[10];
                          sadata[0] = intpacketid.ToString();
                          sadata[1] = intfmipacketid.ToString();

                          basinglemessage = new byte[batest.Length];
                          batest.CopyTo(basinglemessage, 0);

                          int intfix = TwoBytesToInt(batest, intfirstdle + 3 + 16);
                          sadata[2] = intfix.ToString();
                          AddToList(" -- FIX = " + intfix.ToString());


                          //byte[] batlat1 = new byte[] { 123, 212, 20, 49, 172, 227, 231, 63 };
                          //double doubletestlt = EightBytesToDouble(batlat1, 0);
                          //double doubletestlatitude = doubletestlt * 180 / Math.PI;
                          //string stringtestlat1 = doubletestlatitude.ToString("0.000000#");
                          //AddToList(" -- Latitude test 1= " + stringtestlat1);

                          double doublelatrad = EightBytesToDouble(batest, intfirstdle + 3 + 26);
                          double doublelatitude = doublelatrad * 180 / Math.PI;
                          sadata[3] = doublelatitude.ToString("0.000000#");
                          AddToList(" -- Latitude = " + sadata[3]);

                          double doublelongrad = EightBytesToDouble(batest, intfirstdle + 3 + 34);
                          double doublelongitude = doublelongrad * 180 / Math.PI;
                          sadata[4] = doublelongitude.ToString("0.000000#");
                          AddToList(" -- Longitude = " + sadata[4]);



                          boolmessageskipped = false;
                        }
                        //else other messages here ...



                        if (boolmessageskipped)
                        {
                          AddToList("** UNHANDLED MESSAGE **");

                          sadata = new string[2];
                          sadata[0] = intfmipacketid.ToString();
                          sadata[1] = intfmipacketid.ToString();

                          basinglemessage = new byte[batest.Length];
                          batest.CopyTo(basinglemessage, 0);
                        }//end-unhandled response

                        boolreturn = true;
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception e1)
      {
        AddToList(".. ERROR .. ");
        clsDebugFunctions.LineOut(stringfunction, e1);
      }
      return boolreturn;
    }



    //public byte[] IntegerToByteArray(int intinput)
    //{
    //  return BitConverter.GetBytes(intinput);
    //}
    public byte[] IntegerToByteArray(int intinput)
    {
      return BitConverter.GetBytes(intinput);
    }
    public byte[] LongToByteArray(long longinput)
    {
      return BitConverter.GetBytes(longinput);
    }
    public byte[] FloatToByteArray(float floatinput)
    {
      return BitConverter.GetBytes(floatinput);
    }

    public int TwoBytesToInt(byte[] bainput, int intstartposition)
    {
      byte[] balocal = clsStringFunctions.ByteArraySubarray(bainput, intstartposition, 2);
      return BitConverter.ToInt16(balocal, 0);
    }
    public double EightBytesToDouble(byte[] bainput, int intstartposition)
    {
      byte[] balocal = clsStringFunctions.ByteArraySubarray(bainput, intstartposition, 8);
      return BitConverter.ToDouble(balocal, 0);
    }

    public void RemoveItemFromByteArray(ref byte[] baincoming, int int0baseditemtoremove)
    {
      byte[] baleft = new byte[] { };
      byte[] baright = new byte[] { };
      if (int0baseditemtoremove == 0)
      {
        baright = clsStringFunctions.ByteArraySubarray(baincoming, 1, baincoming.Length - 1);
      }
      else if (int0baseditemtoremove >= baincoming.Length - 1)
      {
        baleft = clsStringFunctions.ByteArraySubarray(baincoming, 0, baincoming.Length - 1);
      }
      else
      {
        baleft = clsStringFunctions.ByteArraySubarray(baincoming, 0, int0baseditemtoremove);
        baright = clsStringFunctions.ByteArraySubarray(baincoming, int0baseditemtoremove + 1, baincoming.Length - int0baseditemtoremove - 1);
      }
      byte[] bareturn = clsStringFunctions.ByteArrayConcatenate(baleft, baright);
      baincoming = bareturn;
    }





    public enum gps_fix_type
    {
      GPS_FIX_UNUSABLE = 0,
      GPS_FIX_INVALID = 1,
      GPS_FIX_2D = 2,
      GPS_FIX_3D = 3,
      GPS_FIX_2D_DIFF = 4,
      GPS_FIX_3D_DIFF = 5
    };





    private void button1_Click(object sender, EventArgs e)
    {
      const string stringfunction = "button1_Click";

      try
      {

        //send FMI command
        // 0   1        2    3                4         5       6   7 
        //DLE PacketID Size FMIPacketID FMIPacketData Checksum DLE EOT
        //16  161      2    0                 0        checksum 16  3

        //wait for ACK response

        //future:
        //retry?
        //disable PVT?
        //Disable fmi?

        //rx
        bool boolackonly = false;
        bool boolnackonly = false;
        string[] stringdata = new string[] { };
        byte[] bathismessage = new byte[] { };

        //tx
        byte[] bytedle = new byte[] { 16 };
        byte[] bytedleeot = new byte[] { 16, 3 };
        byte[] bytearrayheader = new byte[] { 0 };
        byte[] bytechecksum = new byte[] { 0 };
        byte[] bytearraytosend = new byte[] { 0 };

        AddToList("<" + "OPEN PORT" + ">");

        SetSerialPort(serialPort1, true);
        if (!serialPort1.IsOpen)
        {
          AddToList(" -- Unable to open COM port");
          return;
        }

        AddToList("<" + "SEND ENABLE_FMI PACKET" + ">");

        bytearrayheader = new byte[] { 161, 2, 0, 0 };
        bytechecksum = GetFMIChecksum(bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);

        AddToList(" -- Sending:"); 
        AddHexToList(bytearraytosend);

        bool boolresponse = false;
        PackDLEs(ref bytearraytosend);
        ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraytosend));

        AddToList(" .. wait for ACK"); //ack should be .. 10 06 02 a1 00 57 10 03

        int inttenths = 0;
        while ((inttenths < 100) && (bacomportbuffer.Length < 8))// && (serialPort1.BytesToRead == 0) && (!boolbytesreceived))
        {
          inttenths++;
          System.Windows.Forms.Application.DoEvents();
          System.Threading.Thread.Sleep(10);
        }

        bool boolenablefmiack = false;
        if (bacomportbuffer.Length>=8)
        {
          boolresponse = true;
          boolenablefmiack = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
          if (boolenablefmiack)
          {
            AddToList(" -- ACK OK -- ");
          }
        }

        AddToList("<" + "CLOSE PORT" + ">");
        SetSerialPort(serialPort1, false);

        if (bacomportbuffer.Length > 0)
        {
          //try to process them
          bool boolunicodeprompt = false;
          boolunicodeprompt = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
        }

        if (bacomportbuffer.Length>0)
        {
          //clear the buffer 
          AddToList("<" + "CLEAR RX BUFFER OF UNPROCESSED CHARS" + ">");
          bacomportbuffer = new byte[] { };
        }

        if (!boolresponse)
        {
          AddToList("<" + "THERE WAS NO RESPONSE" + ">");
        }

      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, "ERROR");
      }

    }//end-button1





    private void button2_Click(object sender, EventArgs e)
    {
      const string stringfunction = "button2_Click";

      try
      {

        //request product id and support protocol

        // 0     1       2        3         4         5      6   7
        //DEL PacketID Size FMIPacketID FMIPayload Checksum DLE ETX
                              
        //N                        //fmipacketid                   //FMIPacketDataType
        //0  server --> client       0x0001=ProductIDRequest             None
        //1  server <-- client       0x0002=ProductID                   product_id_data_type
        //2  server <-- client       0x0003=ProtocolSupportData         protocol_array_data_type

        // 0   1        2    3                4         5       6   7 
        //DLE PacketID Size FMIPacketID FMIPacketData Checksum DLE EOT
        //16  161      2    1             0         checksum 16  3

        //rx
        bool boolackonly = false;
        bool boolnackonly = false;
        string[] stringdata = new string[] { };
        byte[] bathismessage = new byte[] { };

        //tx
        byte[] bytedle = new byte[] { 16 };
        byte[] bytedleeot = new byte[] { 16, 3 };
        byte[] bytearrayheader = new byte[] { 0 };
        byte[] bytechecksum = new byte[] { 0 };
        byte[] bytearraytosend = new byte[] { 0 };


        AddToList("<" + "OPEN PORT" + ">");

        SetSerialPort(serialPort1, true);
        if (!serialPort1.IsOpen)
        {
          AddToList(" -- Unable to open COM port");
          return;
        }

        AddToList("<" + "SEND PRODUCT_ID_REQUEST PACKET" + ">");

        bytearrayheader = new byte[] { 161, 2, 1, 0 };
        bytechecksum = GetFMIChecksum(bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);

        AddToList(" .. sending:");
        AddHexToList(bytearraytosend);

        bool boolresponse = false;
        PackDLEs(ref bytearraytosend);
        ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraytosend));

        AddToList(" .. wait for ACK"); 

        int inttenths = 0;
        while ((inttenths < 100) && (bacomportbuffer.Length<8))
        {
          inttenths++;
          System.Windows.Forms.Application.DoEvents();
          System.Threading.Thread.Sleep(10);
        }

        bool boolproductidrequestack = false;
        bool boolvalidproductid = false;
        bool boolvalidprotocol = false;

        if (bacomportbuffer.Length >= 8)
        {
          boolresponse = true;
          boolproductidrequestack = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
          if (boolproductidrequestack)
          {
            AddToList(" -- ACK OK -- ");
          }
        }

        if (boolproductidrequestack)
        {
          //give it a little more time for more data to come in just in case ..
          //the number of bytes will be variable depending on the supported protocol
          inttenths = 0;
          while (inttenths < 20)
          {
            inttenths++;
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(10);
          }

          boolvalidproductid = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
          if (boolvalidproductid)
          {
            AddToList(" -- PRODUCT_ID and SOFTWARE_VERSION = OK -- ");
          }
        }


        if (boolvalidproductid)
        {

          boolvalidprotocol = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
          if (boolvalidprotocol)
          {
            AddToList(" -- PROTOCOL_SUPPORT_LIST = OK -- ");
          }
        }


        AddToList("<" + "CLOSE PORT" + ">");
        SetSerialPort(serialPort1, false);


        //clear the buffer 
        if (bacomportbuffer.Length > 0)
        {
          AddToList("<" + "CLEAR RX BUFFER OF UNPROCESSED CHARS" + ">");
          bacomportbuffer = new byte[] { };
        }


        if (!boolresponse)
        {
          AddToList("<" + "THERE WAS NO RESPONSE" + ">");
        }

      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, "ERROR");
      }

    }//end-button2-click





    private void buttontestbadmessages_Click(object sender, EventArgs e)
    {
      //test

      //DLE PacketID Size FMIPacketID FMIPacketData Checksum DLE EOT
      //10  a1   06   02  00   44=66   02    2c  01        18   10  03 
      byte[] batest1 = new byte[] { 16, 161, 06, 02, 00, 16, 16, 02, 44, 01, 24, 16, 03 };
      //int intcount = 13;          
      //byte[] batest2 = new byte { 10, 161, 16, 16,  02, 00,  16, 16, 44,   16, 16                   
      //boolvalidproductid = ParseBufferForData(ref batest1, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);

    }




    private void button3_Click(object sender, EventArgs e)
    {
      const string stringfunction = "button3_Click";

      try
      {

        //request pvt messages until rx data, regardless of fix
        //parse gps data
        //disable pvt messages


        // 0     1       2        3         4         5      6   7
        //DEL PacketID Size FMIPacketID FMIPayload Checksum DLE ETX

        //N                        //packetid                   //FMIPacketDataType
        //0  server --> client     10,49=Turn ON PVT              None
        //                         10,50=Turn OFF PVT             None
        //1  server <-- client     10,51=PVT Data packet ID       pvt_data_type

        // 0   1        2    3                4         5       6   7 
        //DLE PacketID Size FMIPacketID FMIPacketData Checksum DLE EOT
        //16     10      2    49/50             0     checksum 16   3

        //rx
        bool boolackonly = false;
        bool boolnackonly = false;
        string[] stringdata = new string[] { };
        byte[] bathismessage = new byte[] { };

        //tx
        byte[] bytedle = new byte[] { 16 };
        byte[] bytedleeot = new byte[] { 16, 3 };
        byte[] bytearrayheader = new byte[] { 0 };
        byte[] bytechecksum = new byte[] { 0 };
        byte[] bytearraytosend = new byte[] { 0 };


        AddToList("<" + "OPEN PORT" + ">");

        SetSerialPort(serialPort1, true);
        if (!serialPort1.IsOpen)
        {
          AddToList(" -- Unable to open COM port");
          return;
        }

        AddToList("<" + "SEND ENABLE_PVT PACKET" + ">");

        bytearrayheader = new byte[] { 10, 2, 49, 0 };
        bytechecksum = GetFMIChecksum(bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);

        AddToList(" .. sending:");
        AddHexToList(bytearraytosend);

        bool boolresponse = false;
        PackDLEs(ref bytearraytosend);
        ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraytosend));

        AddToList(" .. wait for ACK");

        int inttenths = 0;
        while ((inttenths < 100) && (bacomportbuffer.Length < 8))
        {
          inttenths++;
          System.Windows.Forms.Application.DoEvents();
          System.Threading.Thread.Sleep(10);
        }

        bool boolpvtenableack = false;
        bool boolpvtdata = false;
        bool boolpvtdisableack = false;

        if (bacomportbuffer.Length >= 8)
        {
          boolresponse = true;
          boolpvtenableack = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
          if (boolpvtenableack)
          {
            AddToList(" -- ACK OK -- ");
          }
        }

        if (boolpvtenableack)
        {
          //give it a little more time for more data to come in ~1/seconds worth of data ...
          //the number of bytes will be variable depending on the supported protocol

          bool boolretrypvt = false;
        Retry_PVT:
          inttenths = 0;
          while ((inttenths < 200) && (bacomportbuffer.Length < 70))
          {
            inttenths++;
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(10);
          }

          if (bacomportbuffer.Length >= 70)
          {
            boolpvtdata = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
            if (boolpvtdata)
            {
              AddToList(" -- PVT DATA = OK -- ");
            }
            else if (!boolretrypvt)
            {
              boolretrypvt = true;
              AddToList(" -- RETRY PVT ONE TIME -- ");
              goto Retry_PVT;
            }
          }
          else
          {
            AddToList(" -- ERROR: NOT ENOUGH DATA FOR PVT ");
          }
        }



        if (boolpvtenableack)
        {
          //need to disable pvt ...

          AddToList("<" + "SEND DISABLE_PVT PACKET" + ">");

          bytearrayheader = new byte[] { 10, 2, 50, 0 };
          bytechecksum = GetFMIChecksum(bytearrayheader);

          bytearraytosend = new byte[] { 0 };
          bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
          bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
          bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);

          AddToList(" .. sending:");
          AddHexToList(bytearraytosend);

          //bool boolnopvtresponse = false;
          PackDLEs(ref bytearraytosend);
          ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraytosend));

          //AddToList(" .. wait for ACK");

          //maybe we don't care?
          //just dump all data after 500 ms or so..

          inttenths = 0;
          while (inttenths < 50)
          {
            inttenths++;
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(10);
          }

        }



        AddToList("<" + "CLOSE PORT" + ">");
        SetSerialPort(serialPort1, false);


        //clear the buffer 
        if (bacomportbuffer.Length > 0)
        {
          AddToList("<" + "CLEAR RX BUFFER OF UNPROCESSED CHARS" + ">");
          bacomportbuffer = new byte[] { };
        }


        if (!boolresponse)
        {
          AddToList("<" + "THERE WAS NO RESPONSE" + ">");
        }

      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, "ERROR");
      }

    }//end-button3-click





    private void button4_Click(object sender, EventArgs e)
    {
      const string stringfunction = "button4_Click";

      try
      {

        //send a602 stop

        // -or

        //send a603 stop
        //activate stop

        // 0     1       2        3         4         5      6   7
        //DEL PacketID Size FMIPacketID FMIPayload Checksum DLE ETX

        //N                        //fmipacketid                   //FMIPacketDataType
        //0  server --> client       0x0100=A602 Stop               A602_Stop_Packet
        //0  server --> client       0x0101=A603 Stop               A603_Stop_Packet

        // 0   1        2    3                4         5       6   7 
        //DLE PacketID Size FMIPacketID FMIPacketData Checksum DLE EOT
        //16     161   len+2   256       602_data     checksum 16   3

        //rx
        bool boolackonly = false;
        bool boolnackonly = false;
        string[] stringdata = new string[] { };
        byte[] bathismessage = new byte[] { };

        //tx
        byte[] bytedle = new byte[] { 16 };
        byte[] bytedleeot = new byte[] { 16, 3 };
        byte[] bytearrayheader = new byte[] { 0 };
        byte[] bytechecksum = new byte[] { 0 };
        byte[] bytearraytosend = new byte[] { 0 };


        AddToList("<" + "OPEN PORT" + ">");

        SetSerialPort(serialPort1, true);
        if (!serialPort1.IsOpen)
        {
          AddToList(" -- Unable to open COM port");
          return;
        }

        AddToList("<" + "SEND A602 STOP" + ">");
        //AddToList("<" + "SEND A603 STOP" + ">");

        DateTime dtnew = new DateTime(1990, 01, 01);
        TimeSpan sinceMidnight = DateTime.Now.ToUniversalTime().Subtract(dtnew);
        double secs = sinceMidnight.TotalSeconds;
        UInt32 usecs = Convert.ToUInt32(secs);
        byte[] batime = BitConverter.GetBytes(usecs);

        //double doublelat = 44.1234;
        double doublelat = 44.334455;
        long longlat = DoubleDegreesToLongSemicircles(doublelat);
        int intlat = Convert.ToInt32(longlat);
        //double doublelong = -71.6543;
        double doublelong = -71.223344;
        long longlong = DoubleDegreesToLongSemicircles(doublelong);
        int intlong = Convert.ToInt32(longlong);
        byte[] baflat = BitConverter.GetBytes(intlat);
        byte[] baflong = BitConverter.GetBytes(intlong);

        int intuniqueid = 1;
        byte[] bauniqueid = IntegerToByteArray(intuniqueid);

        //string stringname = "12345678  BILL & DONNA MAGNUSSON";
        string stringname = "11223344  DAVE SMITH";
        byte[] baname = clsStringFunctions.StringToByteArray(stringname); //602=51 max incl null, 603=200max inl nul
        byte[] banull = new byte[] { 0 };
        byte[] bafinalname = clsStringFunctions.ByteArrayConcatenate(baname, banull);

        ////              fmi      4               4                4                 4                    10
        //int intsize = 2 + batime.Length + baflat.Length + baflong.Length + bauniqueid.Length + bafinalname.Length;
        //              fmi      4               4                4              10
        int intsize = 2 + batime.Length + baflat.Length + baflong.Length + bafinalname.Length;
        byte[] basize = new byte[1];
        basize[0] = Convert.ToByte(intsize);

        //checksum:
        // 0 1 2 3 4 5 6 7 
        // N Y Y Y Y N N N 
        bytearrayheader = new byte[] { 161};
        bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, basize);
        byte[] bacommand = new byte[] { 00, 01 };
        //byte[] bacommand = new byte[] { 01, 01 };
        bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, bacommand);

        bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, batime);
        bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, baflat);
        bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, baflong);
        //bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, bauniqueid);
        bytearrayheader = clsStringFunctions.ByteArrayConcatenate(bytearrayheader, bafinalname);
        
        bytechecksum = GetFMIChecksum(bytearrayheader);

        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
        bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);

        AddToList(" .. sending:");
        AddHexToList(bytearraytosend);

        bool boolresponse = false;
        PackDLEs(ref bytearraytosend);
        ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraytosend));

        AddToList(" .. wait for ACK");

        int inttenths = 0;
        while ((inttenths < 100) && (bacomportbuffer.Length < 8))
        {
          inttenths++;
          System.Windows.Forms.Application.DoEvents();
          System.Threading.Thread.Sleep(10);
        }

        bool boolstopack = false;

        if (bacomportbuffer.Length >= 8)
        {
          boolresponse = true;
          boolstopack = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
          if (boolstopack)
          {
            AddToList(" -- ACK OK -- ");
          }
        }


        //if (boolstopack)
        //{

        //  //need to set active ...

        //  AddToList("<" + "SEND STOP_STATUS_PROTOCOL PACKET TO ACTIVATE STOP" + ">");

        //  bytearrayheader = new byte[] { 10, 2, 50, 0 };
        //  bytechecksum = GetFMIChecksum(bytearrayheader);

        //  bytearraytosend = new byte[] { 0 };
        //  bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
        //  bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
        //  bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);

        //  AddToList(" .. sending:");
        //  AddHexToList(bytearraytosend);

        //  //bool boolnopvtresponse = false;
        //  PackDLEs(ref bytearraytosend);
        //  ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraytosend));

        //  AddToList(" .. wait for ACK");

        //  inttenths = 0;
        //  while ((inttenths < 100) && (bacomportbuffer.Length < 8))
        //  {
        //    inttenths++;
        //    System.Windows.Forms.Application.DoEvents();
        //    System.Threading.Thread.Sleep(10);
        //  }

        //  if (bacomportbuffer.Length >= 8)
        //  {
        //    bool boolstatusack = ParseBufferForData(ref bacomportbuffer, ref boolackonly, ref boolnackonly, ref stringdata, ref bathismessage);
        //    if (boolstatusack)
        //    {
        //      AddToList(" -- ACK OK -- ");
        //    }
        //  }
        //}


        AddToList("<" + "CLOSE PORT" + ">");
        SetSerialPort(serialPort1, false);


        //clear the buffer 
        if (bacomportbuffer.Length > 0)
        {
          AddToList("<" + "CLEAR RX BUFFER OF UNPROCESSED CHARS" + ">");
          bacomportbuffer = new byte[] { };
        }


        if (!boolresponse)
        {
          AddToList("<" + "THERE WAS NO RESPONSE" + ">");
        }

      }
      catch (Exception e1)
      {
        clsDebugFunctions.LineOut(stringfunction, "ERROR");
      }

    }//end-button4-click


    private long DoubleDegreesToLongSemicircles(double degrees)
    {
      double dblcalc = degrees * (0x80000000 / 180);
      return Convert.ToInt64(dblcalc);
    }

    private void PackDLEs(ref byte[] batopack)
    {
      if (batopack.Length <= 2)
        return;

      byte[] bytepacker = new byte[] { 16 };
      byte[] bareturn = new byte[batopack.Length];
      //DLE packed in size, checksum, packet data

      int intstartlength = batopack.Length;

      for (int idx = intstartlength - 3; idx > 1; idx--)
      {
        if (batopack[idx] == 16)
        {
          InsertByteInPosition(ref batopack, idx + 1, bytepacker);
        }
      }
      return;
    }


    private void InsertByteInPosition(ref byte[] bain, int int0basedposition, byte[] baadd)
    {
      byte[] bareturn = new byte[bain.Length + 1];

      if (int0basedposition == 0)
      {
        bareturn = clsStringFunctions.ByteArrayConcatenate(baadd, bain);
      }
      else if (int0basedposition == bain.Length)
      {
        bareturn = clsStringFunctions.ByteArrayConcatenate(bain, baadd);
      }
      else
      {
        byte[] baleft = clsStringFunctions.ByteArraySubarray(bain, 0, int0basedposition);
        byte[] baright = clsStringFunctions.ByteArraySubarray(bain, bain.Length - int0basedposition, int0basedposition);
        bareturn = clsStringFunctions.ByteArrayConcatenate(baleft, baadd);
        bareturn = clsStringFunctions.ByteArrayConcatenate(bareturn, baright);
      }
      bain = bareturn;
      return;
    }





  }
}
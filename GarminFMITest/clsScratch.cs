using System;
using System.Collections.Generic;
using System.Text;

namespace Garmin_FMI_Test
{
  class clsScratch
  {


    //const int MAX_PAYLOAD_SIZE = 255;
    //const int MIN_PACKET_SIZE = 6;   // 1 +   // DLE                  
    //                                 // 1 +   // Packet ID            
    //                                 // 1 +   // Size of Packet Data  
    //                                 // 0 +   // Payload Data         
    //                                 // 1 +   // Checksum             
    //                                 // 1 +   // DLE                  
    //                                 // 1     // ETX                  

    //const int SIZE_OF_HEADER = 3;    // 1 +   // DLE                  
    //                                 // 1 +   // Packet ID            
    //                                 // 1     // Size of Packet Data  

    //const int SIZE_OF_FOOTER = 2;    // 1 +   // DLE                  
    //                                 // 1     // ETX               

    //private enum byte_id_type
    //{
    //  ID_ETX_BYTE = 3,
    //  ID_ACK_BYTE = 6,
    //  ID_DLE_BYTE = 16,
    //  ID_NAK_BYTE = 21,

    //  ID_COMMAND_BYTE = 10,
    //  ID_UNIT_ID      = 38,

    //  ID_DATE_TIME_DATA = 14,
    //  ID_PVT_DATA       = 51,

    //  ID_LEGACY_STOP_MSG = 135,
    //  ID_LEGACY_TEXT_MSG = 136,
    //  ID_FMI_PACKET      = 161
    //}; //byte_id_type 

    //private enum uint16_command_type
    //{
    //  COMMAND_REQ_DATE_TIME     = 5,
    //  COMMAND_REQ_UNIT_ID_ESN   = 14,
    //  COMMAND_TURN_ON_PVT_DATA  = 49,
    //  COMMAND_TURN_OFF_PVT_DATA = 50
    //}; //uint16_command_type

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





    //private int intinputbuffercount = 0; //count of bytes in binary array holding data for parsing
    //private byte[] bainputbufferarray;   //byte array of received data, count above
    //private string stringcomportbuffer = "";   //variable pased to/from threads for control update ...

    //private int intbaudrate = 9600;


    //FMIPacket[] packetrxbuffer = new FMIPacket[10];
    //int intlastvalidrxpacket = -1;

    //FMIPacket packettx = new FMIPacket();

    //public class FMIPacket
    //{
    //  bool boolispopulated = false;

    //  bool boolischecksumvalid = false;
    //  bool boolisnackacksent = false;
    //  DateTime datetimerx = DateTime.Now;

    //  bool boolissent = false;
    //  DateTime datetimetx = DateTime.Now;

    //  byte[] bytearrayasrecieved = new byte[] { 0 };

    //  byte[] bytearraytosend = new byte[] { 0 };

    //  byte bytedleheader = 16;
    //  byte byteapcketid = 0;
    //  byte bytepayloadsize = 0;
    //  byte[] bytearraypayload = new byte[] { 0 };
    //  byte bytechecksum = 0;
    //  byte bytedlefooter = 16;
    //  byte byteetxfooter = 3;
    //};




    //private void timer1_Tick(object sender, EventArgs e)
    //{
    //  //if (intinputbuffercount == 0) return;      

    //  //AddToList(intinputbuffercount.ToString() + " bytes received ... clearing");

    //  //ProcessBufferForPackets();

    //}



    //private void ProcessBufferForPackets()
    //{
    //  string stringlocal = clsStringFunctions.ByteArrayToString(bainputbufferarray);

    //  while ((stringlocal.Length > 0))
    //  {
    //    /*------------------------------------------------------
    //    If there is not enough data in the FIFO for the smallest
    //    packet, the exit and try again later.
    //    ------------------------------------------------------*/
    //    //if local.len == too small leave it and bail
    //    // copy the remains back to inpoutbufferarray
    //    // return
    //    //end if

    //    /*------------------------------------------------------
    //    Check for a valid packet header.  A valid header starts
    //    with a DLE byte, and the second byte is not an ETX byte.
    //    ------------------------------------------------------*/
    //    //find first dle
    //    //if dle+1 == etx
    //    // dump up to etx since start of buffer not found
    //    //end if

    //    /*------------------------------------------------------
    //    Now, search for the packet's footer.
    //    ------------------------------------------------------*/
    //    //istartbyte = dle
    //    //ipacketidbyte = dle+1
    //    //isizebyte = dle + 2
    //    //if isizebyte = dle then remove from string
    //    //if buf.len < isizebyte + 3 then 
    //    // copy the remains back to inpoutbufferarray
    //    // return
    //    //end if

    //    //packet data ..
    //    //if isizebyte>0
    //    // start at sizebyte + 1, until # bytes copied = isizebyte
    //    //  if byte[0]=dle and byte[1]=dle then 
    //    //   dump byte 1
    //    //   start over
    //    //  end if
    //    //  copy current byte to payloadbuffer
    //    //  inc bytes copied
    //    // next byte

    //    //checksum = next byte after payload
    //    //if checksum = dle then next char should be dle, discard second dle

    //    //next should be dle
    //    //last should be etx

    //    //set buffer to start at next char

    //    //if checksum <> calc checksum (all but byte 0)
    //    // send nack
    //    //else
    //    // send ack
    //    //end if

    //  }
    //}


    //private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    //{
    //  //NOTE: Make sure you assign this to the event for the control...
    //  const string stringfunction = "sregisterport_DataReceived";

    //  try
    //  {
    //    int bytes = serialPort1.BytesToRead;
    //    byte[] buffer = new byte[bytes];
    //    serialPort1.Read(buffer, 0, bytes);
    //    if (intinputbuffercount > 0)
    //    {
    //      byte[] tempbuffer1 = new byte[intinputbuffercount];
    //      bainputbufferarray.CopyTo(tempbuffer1, 0);
    //      bainputbufferarray = new byte[bytes + intinputbuffercount];
    //      bainputbufferarray = clsStringFunctions.ByteArrayConcatenate(tempbuffer1, buffer);
    //    }
    //    else
    //    {
    //      bainputbufferarray = new byte[bytes];
    //      buffer.CopyTo(bainputbufferarray, 0);
    //    }
    //    if (bainputbufferarray.Length > serialPort1.ReadBufferSize)
    //    {
    //      bainputbufferarray = new byte[0];
    //    }
    //    intinputbuffercount = bainputbufferarray.Length;
    //  }
    //  catch (Exception e1)
    //  {
    //    clsDebugFunctions.LineOut(stringfunction, "Reading Port.InputBuffer Bytes", e1);
    //  }
    //  return;
    //}


    //private void SendCommand(string stringcommand)
    //{
    //  byte[] bytedle = new byte[] { 16 };
    //  byte[] bytedleeot = new byte[] { 16, 3 };
    //  byte[] bytearrayheader = new byte[] { 0 };
    //  byte[] bytechecksum = new byte[] { 0 };
    //  byte[] bytearraytosend  = new byte[] { 0 };

    //  int inttargettimeouttenths = 0;
    //  int inttargetresponsechars = 0;
    //  string stringresponse = "";
    //  int inttargettenthstimeout = 0;
    //  bool booltimeoutexpired = false;

    //  byte[] bytearrayresponse = new byte[] {0};



    //  switch (stringcommand)
    //  {

    //    case "START":
    //      AddToList("<" + stringcommand + ">");
    //      SetSerialPort(serialPort1, !serialPort1.IsOpen);
    //      if (!serialPort1.IsOpen)
    //        SetSerialPort(serialPort1, true);
    //      break;

    //    case "ENABLE FMI":
    //      AddToList("<" + stringcommand + ">");

    //      bytearrayheader = new byte[] { 161, 2, 0, 0 };
    //      bytechecksum = GetFMIChecksum(bytearrayheader);
    //      bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
    //      bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
    //      bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);
    //      //SendChars(bytearraytosend);

    //      AddToList("Send + Wait for ACK ... "); //ack should be .. 10 06 02 a1 00 57 10 03

    //      inttargetresponsechars = 8;
    //      stringresponse = "";
    //      inttargettenthstimeout = 10;
    //      booltimeoutexpired = false;
    //      SendChars(bytearraytosend, inttargetresponsechars, ref stringresponse, inttargettenthstimeout, ref booltimeoutexpired);
    //      if (booltimeoutexpired)
    //      {
    //        clsDebugFunctions.LineOut("ERROR = Invalid Reponse, " + inttargettenthstimeout + " Tenths-of-Seconds Timeout"); //ack should be .. 10 06 02 a1 00 57 10 03
    //      }
    //      if (stringresponse.Length > 0)
    //      {
    //        clsDebugFunctions.LineOut("RX: " + stringresponse.Length.ToString() + " bytes"); //ack should be .. 10 06 02 a1 00 57 10 03
    //        bytearrayresponse = clsStringFunctions.StringToByteArray(stringresponse);
    //        AddHexToList(bytearrayresponse);
    //      }


    //      break;


    //    case "PRODUCT & SUPPORT":
    //      AddToList("<" + stringcommand + ">");

    //      bytearrayheader = new byte[] { 161, 2, 1, 0 };
    //      bytechecksum = GetFMIChecksum(bytearrayheader);
    //      bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytedle, bytearrayheader);
    //      bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytechecksum);
    //      bytearraytosend = clsStringFunctions.ByteArrayConcatenate(bytearraytosend, bytedleeot);
    //      SendChars(bytearraytosend);


    //      break;



    //    default:
    //      AddToList ("Unknown Command: " + stringcommand);
    //      break;
    //  }
    //}

    //private void buttonproductandsupport_Click(object sender, EventArgs e)
    //{
    //  SendCommand("PRODUCT & SUPPORT");
    //}

    //private void buttonstart_Click(object sender, EventArgs e)
    //{
    //  SendCommand("START");
    //}

    //private void buttontestbadmessages_Click(object sender, EventArgs e)
    //{
    //  SendCommand("ENABLE FMI");
    //}


    //private void SendChars(string stringsend)
    //{
    //  if (serialPort1.IsOpen)
    //  {
    //    AddToList("TX:" + stringsend);
    //    ExecuteSend(serialPort1, stringsend);
    //  }
    //  else
    //  {
    //    AddToList("COM PORT IS CLOSED");
    //  }
    //}
    //private void SendChars(byte[] bytearraysend)
    //{
    //  //make sure this does thread-safe stuff...
    //  ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraysend));
    //}

    //private void SendChars(string stringsend, int intresponsecharstolookfor, ref string stringresponse, int inttenthstimeout, ref bool booltimeoutexpired)
    //{
    //  const string stringfunction = "SendChars(WaitForResponse)";

    //  //clear the current buffer
    //  //send chars
    //  //if response desired wait for it
    //  byte[] babytes = new byte[0];
    //  try
    //  {
    //    SendChars(stringsend);
    //    if ((intresponsecharstolookfor == 0) && (inttenthstimeout == 0)) return;
    //    WaitForResponse(intresponsecharstolookfor, ref babytes, inttenthstimeout, ref booltimeoutexpired);
    //    if (!booltimeoutexpired)
    //    {
    //      stringresponse = clsStringFunctions.ByteArrayToString(babytes);
    //    }
    //  }
    //  catch (Exception e1)
    //  {
    //    clsDebugFunctions.LineOut(stringfunction, e1);
    //  }
    //  return;
    //}
    //private void SendChars(byte[] bytearraysend, int intresponsecharstolookfor, ref string stringresponse, int inttenthstimeout, ref bool booltimeoutexpired)
    //{
    //  const string stringfunction = "SendChars(ByteArray, WaitForResponse)";

    //  //make sure this does thread-safe stuff...

    //  //clear the current buffer
    //  //send chars
    //  //if response desired wait for it
    //  byte[] babytes = new byte[0];
    //  try
    //  {
    //    ExecuteSend(serialPort1, clsStringFunctions.ByteArrayToString(bytearraysend));
    //    if ((intresponsecharstolookfor == 0) && (inttenthstimeout == 0)) return;
    //    WaitForResponse(intresponsecharstolookfor, ref babytes, inttenthstimeout, ref booltimeoutexpired);
    //    if (babytes.GetUpperBound(0) > -1)
    //    {
    //      stringresponse = clsStringFunctions.ByteArrayToString(babytes);
    //    }
    //    //if (!booltimeoutexpired)
    //    //{
    //    //  stringresponse = clsStringFunctions.ByteArrayToString(babytes);
    //    //}
    //    //else
    //    //{
    //    //  if (babytes.GetUpperBound[0] == intresponsecharstolookfor)
    //    //  {
    //    //    stringresponse = clsStringFunctions.ByteArrayToString(babytes);
    //    //  }
    //    //}
    //  }
    //  catch (Exception e1)
    //  {
    //    //clsDebugFunctions.LineOut(stringfunction, e1);
    //    //not thread safe
    //  }
    //  return;
    //}



    //public int WaitForResponse(int intresponsecharstolookfor, ref byte[] bareturned, int intcommandtenthstowait, ref bool booltimeoutexpired)
    //{
    //  const string stringfunction = "WaitForResponse";

    //  int intreturn = 0;
    //  int idx = 0;
    //  byte[] barxbuffer = new byte[0];

    //  bool boolbailedearly = true;

    //  //'Returns:
    //  //'0 = no error = ok
    //  //'-1 = Byte Count not reached in timeout

    //  intreturn = -1;
    //  for (idx = 0; idx < intcommandtenthstowait; idx++)
    //  {
    //    for (int idy = 0; idy < 20; idy++)
    //    {
    //      System.Windows.Forms.Application.DoEvents();
    //      System.Threading.Thread.Sleep(5);
    //      System.Windows.Forms.Application.DoEvents();

    //      if (intinputbuffercount > 0)
    //      {
    //        GetAllInputBytes(ref barxbuffer);
    //      }

    //      if (barxbuffer.Length >= intresponsecharstolookfor)
    //      {
    //        //set intreturn = 0 if ok ...
    //        intreturn = 0;
    //      }//end-buffer.len>0

    //    }//end-for idy = 5ms
    //  }//end-for idx = 100ms

    //  bareturned = new byte[barxbuffer.Length];
    //  barxbuffer.CopyTo(bareturned, 0);
    //  booltimeoutexpired = boolbailedearly;

    //  return intreturn;
    //}

    //private void GetAllInputBytes(ref byte[] buffer)
    //{
    //  string stringfunction = "GetAllInputBytes";
    //  clsDebugFunctions.LineOut(stringfunction, "bainputbufferarray.Length = " + bainputbufferarray.Length);
    //  clsDebugFunctions.LineOut(stringfunction, "buffer.Length = " + buffer.Length);

    //  if (bainputbufferarray.Length > 0)
    //  {
    //    if (buffer.Length == 0)
    //    {
    //      buffer = new byte[bainputbufferarray.Length];
    //      try
    //      {
    //        bainputbufferarray.CopyTo(buffer, 0);
    //      }
    //      catch (Exception e1)
    //      {
    //        clsDebugFunctions.LineOut(stringfunction, e1);
    //      }
    //    }
    //    else
    //    {
    //      byte[] btemp = new byte[buffer.Length + bainputbufferarray.Length];
    //      btemp = clsStringFunctions.ByteArrayConcatenate(buffer, bainputbufferarray);
    //      buffer = new byte[btemp.Length];
    //      try
    //      {
    //        btemp.CopyTo(buffer, 0);
    //      }
    //      catch (Exception e1)
    //      {
    //        clsDebugFunctions.LineOut(stringfunction, "btemp.Length = " + btemp.Length);
    //        clsDebugFunctions.LineOut(stringfunction, e1);
    //      }
    //    }
    //  }
    //  bainputbufferarray = new byte[0];
    //  intinputbuffercount = 0;
    //  return;
    //}











    //public bool TestForEnableFMIACK(ref byte[] bainput)
    //{


    //  string stringfunction = "TestForEnableFMIACK";
    //  bool boolreturn = false;

    //  string stringlocal = "";

    //  try
    //  {
    //    int intlength = bainput.GetUpperBound(0) + 1;

    //    if (intlength < 8)
    //    {
    //      //ack should be .. 10 06 02 a1 00 57 10 03
    //      AddToList(" -- reponse too short: " + intlength.ToString() + " bytes");
    //    }
    //    else
    //    {
    //      AddToList(" -- testing " + intlength.ToString() + " bytes");
    //      AddHexToList(bainput);


    //      byte[] batest = new byte[] { };
    //      if (intlength >= 8)
    //      {
    //        //ack should be .. 10 06 02 a1 00 57 10 03
    //        AddToList(" -- extra bytes received, total = " + intlength.ToString() + ", using first 8 that start with DLE");
    //        int intfirstdle = clsStringFunctions.ByteArrayIndexOf(bainput, 16);
    //        if (intfirstdle > -1)
    //        {
    //          AddToList(" -- DLE in position " + intfirstdle.ToString());
    //          if (intfirstdle + 8 > intlength)
    //          {
    //            AddToList(" -- response too short");
    //          }
    //          else
    //          {
    //            //make sure next char isn't ETX...
    //            //int intnext = clsStringFunctions.ByteArrayIndexOf(bainput, intfirstdle + 1, 3);
    //            if (bainput[intfirstdle + 1]==3)
    //            {
    //              //dump to next and start over ...
    //              bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 2, intlength - intfirstdle - 2);
    //              return false;
    //            }
    //            //if (intnext == 0)
    //            //{
    //            //  //dump to next and start over ...
    //            //  bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 2, intlength - intfirstdle - 2);
    //            //  return false;
    //            //}


    //            batest = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle, 8);
    //            if (intfirstdle + 8 < intlength)
    //            {
    //              //more chars in buffer, leave them
    //              //goddammint
    //              bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 8, intlength - intfirstdle - 8);
    //              int intnewlength = bainput.GetUpperBound(0) + 1;
    //              AddToList(" -- leaving " + intnewlength.ToString() + " bytes: ");
    //              AddHexToList(bainput);
    //            }
    //            else
    //            {
    //              bainput = new byte[] { };
    //            }

    //            AddToList("Testing: ");
    //            AddHexToList(batest);

    //            //how to care:
    //            //a. starts with DLE
    //            //b. ends with DLE ETX
    //            //c. checksum is valid
    //            //d. packet id = ACK

    //            //note: checksum can change since packetid ack is for can change ...

    //            int intchar0 = clsStringFunctions.ByteToInt(batest[0]);
    //            int intchar1 = clsStringFunctions.ByteToInt(batest[1]);
    //            int intcharchecksum = clsStringFunctions.ByteToInt(batest[5]);
    //            int intchar6 = clsStringFunctions.ByteToInt(batest[6]);
    //            int intchar7 = clsStringFunctions.ByteToInt(batest[7]);

    //            //checksum:
    //            // 0 1 2 3 4 5 6 7 
    //            // N Y Y Y Y N N N 
    //            byte[] batestforchecksum = clsStringFunctions.ByteArraySubarray(batest, 1, 4);
    //            //bytechecksum = GetFMIChecksum(bytearrayheader);
    //            int intactualchecksum = clsStringFunctions.ByteToInt(GetFMIChecksum(batestforchecksum)[0]);

    //            if (intchar0 != 16)
    //            {
    //              AddToList(" -- char 0 != DLE: " + intchar0.ToString());
    //            }
    //            else
    //            {
    //              if (intchar6 != 16)
    //              {
    //                AddToList(" -- char 6 != DLE: " + intchar6.ToString());
    //              }
    //              else
    //              {
    //                if (intchar7 != 3)
    //                {
    //                  AddToList(" -- char 7 != ETX: " + intchar7.ToString());
    //                }
    //                else
    //                {
    //                  if (intcharchecksum != intactualchecksum)
    //                  {
    //                    AddToList(" -- char 5 checksum mismatch .. sent: " + intcharchecksum.ToString() + ", atual: " + intactualchecksum.ToString());
    //                  }
    //                  else
    //                  {
    //                    if (intchar1 != 6)
    //                    {
    //                      AddToList(" -- char 1 != ACK: " + intchar1.ToString());
    //                    }
    //                    else
    //                    {
    //                      AddToList("ACK received!");
    //                      boolreturn = true;
    //                    }
    //                  }
    //                }
    //              }
    //            }
    //          }
    //        }
    //        else
    //        {
    //          AddToList(" -- DLE not found");
    //        }
    //      }
    //    }
    //  }
    //  catch (Exception e1)
    //  {
    //    AddToList(".. ERROR .. ");
    //    clsDebugFunctions.LineOut(stringfunction, e1);
    //  }
    //  return boolreturn;
    //}




    //           tx
    //server --> client  packetid  size  fmipacketid+data
    // enable fmi        a1        02    01 00

    //server <-- client  packetid  size  fmipacketid+data
    // ack               06        02    a1 00

    //    public bool ParseResponseForProductID(ref byte[] bainput)//, ref int intbuffercount)
    //    {
    //      string stringfunction = "ParseResponseForProductID";
    //      bool boolreturn = false;

    //Retry_ParseResponseForProductID:

    //      //string stringlocal = "";

    //      int intthiscommandlength = 12;

    //      try
    //      {
    //        if (bainput.Length < intthiscommandlength)  //          product_id:0244x0 = 580d = 5.80  software_version: 012Cx0 = 300d = 3.00
    //        {                              //                  --unnt16--                       --sint16--
    //          //product id data should be .. 10 a1 06 02 00    44 02                            2c 01        e4 10 03 
    //          AddToList(" -- reponse too short: " + bainput.Length.ToString() + " bytes");
    //        }
    //        else
    //        {
    //          AddToList(" -- testing " + bainput.Length.ToString() + " bytes");
    //          AddHexToList(bainput);

    //          // 0   1        2    3                4         5       6   7 
    //          //DLE PacketID Size FMIPacketID+FMIPacketData Checksum DLE EOT
    //          //
    //          //            size     FMIID+data             checksum        can be dle-stuffed ...

    //          byte[] batest = new byte[] { };
    //          if (bainput.Length >= intthiscommandlength)
    //          {
    //            AddToList(" -- extra bytes possibly received, total = " + bainput.Length.ToString() + ", looking for first DLE");
    //            int intfirstdle = clsStringFunctions.ByteArrayIndexOf(bainput, 16);
    //            if (intfirstdle > -1)
    //            {
    //              AddToList(" -- DLE in position " + intfirstdle.ToString());
    //              if (intfirstdle + 12 > bainput.Length)
    //              {
    //                AddToList(" -- response too short");
    //              }
    //              else
    //              {
    //                //make sure next char isn't ETX...
    //                //int intnext = clsStringFunctions.ByteArrayIndexOf(bainput, intfirstdle + 1, 3);
    //                if (bainput[intfirstdle + 1] == 3)
    //                {
    //                  //dump to after ETX and start over ...
    //                  bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 2, bainput.Length - intfirstdle - 2);
    //                  //intbuffercount = bainput.GetUpperBound(0) + 1;
    //                  goto Retry_ParseResponseForProductID;
    //                }

    //                //make sure next char isn't DLE, otherwise was stuffed = skip both
    //                if (bainput[intfirstdle + 1] == 16)
    //                {
    //                  //dump to after 2nd DLE and start over ...
    //                  bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 2, bainput.Length - intfirstdle - 2);
    //                  //intbuffercount = bainput.GetUpperBound(0) + 1;
    //                  goto Retry_ParseResponseForProductID;
    //                }

    //                //check size for dle-stuff
    //                //if size is dle, dump next char from buffer
    //                if (bainput[intfirstdle + 2] == 16)
    //                {
    //                  RemoveItemFromByteArray(ref bainput, intfirstdle + 3);//, ref intbuffercount);
    //                  //intbuffercount = bainput.Length;
    //                }

    //                //check fmipacketid/packet-data for dle-stuff
    //                //if fmipacketid/packet-data is dle, dump next char from buffer
    //                int intstart = 0;
    //                //ReDoSizeTest:
    //                int intsize = clsStringFunctions.ByteToInt(bainput[intfirstdle + 2]) - 2;
    //                for (int idx = intstart; idx < intsize; idx++)
    //                {
    //                  if (bainput[intfirstdle + 5 + idx] == 16)
    //                  {
    //                    RemoveItemFromByteArray(ref bainput, intfirstdle + 6 + idx);
    //                    //intbuffercount = bainput.Length;
    //                    //intstart = idx + 1;
    //                  }
    //                }


    //                //check checksum for dle-stuff
    //                //if checksum is dle, dump next char from buffer
    //                if (bainput[intfirstdle + 5 + intsize] == 16)
    //                {
    //                  RemoveItemFromByteArray(ref bainput, intfirstdle + 6 + intsize);
    //                }

    //                intthiscommandlength = 1 + 1 + 1 + 2 + intsize + 1 + 1 + 1;


    //                batest = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle, intthiscommandlength);
    //                if (intfirstdle + intthiscommandlength < bainput.Length)
    //                {
    //                  //more chars in buffer, leave them
    //                  //goddammint
    //                  bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + intthiscommandlength, bainput.Length - intfirstdle - intthiscommandlength);
    //                  int intnewlength = bainput.Length;
    //                  AddToList(" -- leaving " + intnewlength.ToString() + " bytes: ");
    //                  AddHexToList(bainput);
    //                }
    //                else
    //                {
    //                  bainput = new byte[] { };
    //                }


    //                AddToList("Testing: ");
    //                AddHexToList(batest);

    //                //how to care:
    //                //a. starts with DLE
    //                //b. ends with DLE ETX
    //                //c. checksum is valid
    //                //d. packet id = ACK

    //                //note: checksum can change since packetid ack is for can change ...

    //                int intchar0 = clsStringFunctions.ByteToInt(batest[0]);
    //                int intchar1 = clsStringFunctions.ByteToInt(batest[1]);
    //                int intcharchecksum = clsStringFunctions.ByteToInt(batest[batest.Length-3]);
    //                int intchar6 = clsStringFunctions.ByteToInt(batest[batest.Length - 2]);
    //                int intchar7 = clsStringFunctions.ByteToInt(batest[batest.Length - 1]);

    //                //checksum:
    //                // 0 1 2 3 4 5 6 7 
    //                // N Y Y Y Y N N N 
    //                byte[] batestforchecksum = clsStringFunctions.ByteArraySubarray(batest, 1, batest.Length-4);
    //                //bytechecksum = GetFMIChecksum(bytearrayheader);
    //                int intactualchecksum = clsStringFunctions.ByteToInt(GetFMIChecksum(batestforchecksum)[0]);

    //                if (intchar0 != 16)
    //                {
    //                  AddToList(" -- char 0 != DLE: " + intchar0.ToString());
    //                }
    //                else
    //                {
    //                  if (intchar6 != 16)
    //                  {
    //                    AddToList(" -- char 6 != DLE: " + intchar6.ToString());
    //                  }
    //                  else
    //                  {
    //                    if (intchar7 != 3)
    //                    {
    //                      AddToList(" -- char 7 != ETX: " + intchar7.ToString());
    //                    }
    //                    else
    //                    {
    //                      if (intcharchecksum != intactualchecksum)
    //                      {
    //                        AddToList(" -- char 5 checksum mismatch .. sent: " + intcharchecksum.ToString() + ", atual: " + intactualchecksum.ToString());
    //                      }
    //                      else
    //                      {
    //                        //AddToList("ACK received!");
    //                        int intproductid = TwoBytesToInt(batest, intfirstdle + 5);
    //                        AddToList(" PRODUCT_ID =  " + intproductid.ToString());

    //                        int intsoftwareversion = TwoBytesToInt(batest, intfirstdle + 7);
    //                        AddToList(" SOFTWARE_VERSION =  " + intsoftwareversion.ToString());

    //                        boolreturn = true;
    //                      }
    //                    }
    //                  }
    //                }
    //              }
    //            }
    //            else
    //            {
    //              AddToList(" -- DLE not found");
    //            }
    //          }
    //        }
    //      }
    //      catch (Exception e1)
    //      {
    //        AddToList(".. ERROR .. ");
    //        clsDebugFunctions.LineOut(stringfunction, e1);
    //      }
    //      return boolreturn;
    //    }







    //string[] saprotocoldata;

    //public bool ParseResponseForProtocolData(ref byte[] bainput)//, ref int intbuffercount)
    //{

    //  string stringfunction = "ParseResponseForProtocolData";
    //  bool boolreturn = false;

    //Retry_ParseResponseForProductID:

    //  //string stringlocal = "";

    //  int intthiscommandlength = 6; //minimum

    //  try
    //  {
    //    if (bainput.Length < intthiscommandlength)  
    //    {
    //      AddToList(" -- reponse too short: " + bainput.Length.ToString() + " bytes");
    //    }
    //    else
    //    {
    //      AddToList(" -- testing " + bainput.Length.ToString() + " bytes");
    //      AddHexToList(bainput);

    //      // 0   1        2    3                4         5       6   7 
    //      //DLE PacketID Size FMIPacketID+FMIPacketData Checksum DLE EOT
    //      //
    //      //            size     FMIID+data             checksum        can be dle-stuffed ...

    //      byte[] batest = new byte[] { };
    //      if (bainput.Length >= intthiscommandlength)
    //      {
    //        AddToList(" -- extra bytes possibly received, total = " + bainput.Length.ToString() + ", looking for first DLE");
    //        int intfirstdle = clsStringFunctions.ByteArrayIndexOf(bainput, 16);
    //        if (intfirstdle > -1)
    //        {
    //          AddToList(" -- DLE in position " + intfirstdle.ToString());
    //          if (intfirstdle + 12 > bainput.Length)
    //          {
    //            AddToList(" -- response too short");
    //          }
    //          else
    //          {
    //            //make sure next char isn't ETX...
    //            //int intnext = clsStringFunctions.ByteArrayIndexOf(bainput, intfirstdle + 1, 3);
    //            if (bainput[intfirstdle + 1] == 3)
    //            {
    //              //dump to after ETX and start over ...
    //              bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 2, bainput.Length - intfirstdle - 2);
    //              //intbuffercount = bainput.GetUpperBound(0) + 1;
    //              goto Retry_ParseResponseForProductID;
    //            }

    //            //make sure next char isn't DLE, otherwise was stuffed = skip both
    //            if (bainput[intfirstdle + 1] == 16)
    //            {
    //              //dump to after 2nd DLE and start over ...
    //              bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + 2, bainput.Length - intfirstdle - 2);
    //              //intbuffercount = bainput.GetUpperBound(0) + 1;
    //              goto Retry_ParseResponseForProductID;
    //            }

    //            //check size for dle-stuff
    //            //if size is dle, dump next char from buffer
    //            if (bainput[intfirstdle + 2] == 16)
    //            {
    //              RemoveItemFromByteArray(ref bainput, intfirstdle + 3);//, ref intbuffercount);
    //              //intbuffercount = bainput.Length;
    //            }

    //            //check fmipacketid/packet-data for dle-stuff
    //            //if fmipacketid/packet-data is dle, dump next char from buffer
    //            int intstart = 0;
    //            //ReDoSizeTest:
    //            int intsize = clsStringFunctions.ByteToInt(bainput[intfirstdle + 2]) - 2;
    //            for (int idx = intstart; idx < intsize; idx++)
    //            {
    //              if (bainput[intfirstdle + 5 + idx] == 16)
    //              {
    //                RemoveItemFromByteArray(ref bainput, intfirstdle + 6 + idx);
    //                //intbuffercount = bainput.Length;
    //                //intstart = idx + 1;
    //              }
    //            }


    //            //check checksum for dle-stuff
    //            //if checksum is dle, dump next char from buffer
    //            if (bainput[intfirstdle + 5 + intsize] == 16)
    //            {
    //              RemoveItemFromByteArray(ref bainput, intfirstdle + 6 + intsize);
    //            }

    //            intthiscommandlength = 1 + 1 + 1 + 2 + intsize + 1 + 1 + 1;


    //            batest = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle, intthiscommandlength);
    //            if (intfirstdle + intthiscommandlength < bainput.Length)
    //            {
    //              //more chars in buffer, leave them
    //              //goddammint
    //              bainput = clsStringFunctions.ByteArraySubarray(bainput, intfirstdle + intthiscommandlength, bainput.Length - intfirstdle - intthiscommandlength);
    //              int intnewlength = bainput.Length;
    //              AddToList(" -- leaving " + intnewlength.ToString() + " bytes: ");
    //              AddHexToList(bainput);
    //            }
    //            else
    //            {
    //              bainput = new byte[] { };
    //            }


    //            AddToList("Testing: ");
    //            AddHexToList(batest);

    //            //how to care:
    //            //a. starts with DLE
    //            //b. ends with DLE ETX
    //            //c. checksum is valid
    //            //d. packet id = ACK

    //            //note: checksum can change since packetid ack is for can change ...

    //            int intchar0 = clsStringFunctions.ByteToInt(batest[0]);
    //            int intchar1 = clsStringFunctions.ByteToInt(batest[1]);
    //            int intcharchecksum = clsStringFunctions.ByteToInt(batest[batest.Length - 3]);
    //            int intchar6 = clsStringFunctions.ByteToInt(batest[batest.Length - 2]);
    //            int intchar7 = clsStringFunctions.ByteToInt(batest[batest.Length - 1]);

    //            //checksum:
    //            // 0 1 2 3 4 5 6 7 
    //            // N Y Y Y Y N N N 
    //            byte[] batestforchecksum = clsStringFunctions.ByteArraySubarray(batest, 1, batest.Length - 4);
    //            //bytechecksum = GetFMIChecksum(bytearrayheader);
    //            int intactualchecksum = clsStringFunctions.ByteToInt(GetFMIChecksum(batestforchecksum)[0]);

    //            if (intchar0 != 16)
    //            {
    //              AddToList(" -- char 0 != DLE: " + intchar0.ToString());
    //            }
    //            else
    //            {
    //              if (intchar6 != 16)
    //              {
    //                AddToList(" -- char 6 != DLE: " + intchar6.ToString());
    //              }
    //              else
    //              {
    //                if (intchar7 != 3)
    //                {
    //                  AddToList(" -- char 7 != ETX: " + intchar7.ToString());
    //                }
    //                else
    //                {
    //                  if (intcharchecksum != intactualchecksum)
    //                  {
    //                    AddToList(" -- char 5 checksum mismatch .. sent: " + intcharchecksum.ToString() + ", atual: " + intactualchecksum.ToString());
    //                  }
    //                  else
    //                  {

    //                    //AddToList("ACK received!");
    //                    //AddToList(stringdata);
    //                    //AddHexToList(stringdata);

    //                    AddToList("--==PROTOCOL_LIST==--" + intchar7.ToString());

    //                    for (int idx = 5; idx < intsize + 5; idx = idx + 3)
    //                    {
    //                      byte[] bletter = clsStringFunctions.ByteArraySubarray(batest, idx, 1);
    //                      byte[] badata = clsStringFunctions.ByteArraySubarray(batest, idx + 1, 2);
    //                      string stringletter = clsStringFunctions.ByteArrayToString(bletter);
    //                      int intdata = TwoBytesToInt(badata,0);
    //                      AddToList(stringletter + intdata.ToString("000"));
    //                    }

    //                    boolreturn = true;
    //                  }
    //                }
    //              }
    //            }
    //          }
    //        }
    //        else
    //        {
    //          AddToList(" -- DLE not found");
    //        }
    //      }
    //    }
    //  }
    //  catch (Exception e1)
    //  {
    //    AddToList(".. ERROR .. ");
    //    clsDebugFunctions.LineOut(stringfunction, e1);
    //  }
    //  return boolreturn;
    //}






  }
}

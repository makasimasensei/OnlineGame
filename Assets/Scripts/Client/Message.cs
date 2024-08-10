using Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;


public class Message
{
    byte[] bytes = new byte[1024];
    int remainSize = 1024;

    public byte[] Bytes { get => bytes; set => bytes = value; }

    /// <summary>
    /// The remaining size of Byte array.
    /// </summary>
    /// <param name="len">The size of the data segment in the current byte array.</param>
    void RemainSize(int len)
    {
        remainSize -= len;
    }

    /// <summary>
    /// Parsing information.
    /// </summary>
    public void ReadMessage(Action<ActionCode, string> processDataCallback)
    {
        while (BitConverter.ToInt32(bytes, 0) > 0)
        {
            int len = BitConverter.ToInt32(bytes, 0);
            if (remainSize - 4 < len) return;

            ActionCode actionCode = (ActionCode)BitConverter.ToInt32(bytes, 4);
            string s = Encoding.UTF8.GetString(bytes, 8, len - 4);
            Debug.WriteLine(s);
            processDataCallback(actionCode, s);

            RemainSize(4 + len);

            byte[] newBytes = new byte[1024];
            Array.Copy(bytes, 4 + len, newBytes, 0, 1024 - 4 - len);
            bytes = newBytes;
        }
    }

    /// <summary>
    /// Pack data.
    /// </summary>
    /// <param name="requestData">Request code.</param>
    /// <param name="actionCode">Action code.</param>
    /// <param name="data">Data.</param>
    /// <returns></returns>
    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requestDataBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionDataBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int length = requestDataBytes.Length + actionDataBytes.Length + dataBytes.Length;
        byte[] lengthBytes = BitConverter.GetBytes(length);
        return lengthBytes.Concat(requestDataBytes).Concat(actionDataBytes).Concat(dataBytes).ToArray();
    }
}

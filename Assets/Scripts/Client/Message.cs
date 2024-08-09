using Common;
using System;
using System.Linq;
using System.Text;

public class Message
{
    byte[] bytes = new byte[1024];
    int remainSize = 1024;

    public byte[] Bytes { get => bytes; set => bytes = value; }

    /// <summary>
    /// 字节数组剩余大小
    /// </summary>
    /// <param name="len">当前字节数组中数据段的长度</param>
    void RemainSize(int len)
    {
        remainSize -= len;
    }

    /// <summary>
    /// 解析信息
    /// </summary>
    public void ReadMessage(Action<ActionCode, string> processDataCallback)
    {
        while (BitConverter.ToInt32(bytes, 0) > 0)
        {
            //数据长度
            int len = BitConverter.ToInt32(bytes, 0);
            //解码的长度和实际长度不相同时，返回
            if (remainSize - 4 < len) return;

            ActionCode actionCode = (ActionCode)BitConverter.ToInt32(bytes, 4);
            string s = Encoding.UTF8.GetString(bytes, 8, len - 4);
            processDataCallback(actionCode, s);

            RemainSize(4 + len);

            byte[] newBytes = new byte[1024];
            Array.Copy(bytes, 4 + len, newBytes, 0, 1024 - 4 - len);
            bytes = newBytes;
        }
    }

    public static byte[] PackData(RequestCode requestData, ActionCode actionCode, string data)
    {
        byte[] requestDataBytes = BitConverter.GetBytes((int)requestData);
        byte[] actionDataBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int length = requestDataBytes.Length + actionDataBytes.Length + dataBytes.Length;
        byte[] lengthBytes = BitConverter.GetBytes(length);
        return lengthBytes.Concat(requestDataBytes).Concat(actionDataBytes).Concat(dataBytes).ToArray();
    }
}

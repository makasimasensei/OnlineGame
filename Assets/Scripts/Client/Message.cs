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
    /// �ֽ�����ʣ���С
    /// </summary>
    /// <param name="len">��ǰ�ֽ����������ݶεĳ���</param>
    void RemainSize(int len)
    {
        remainSize -= len;
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    public void ReadMessage(Action<ActionCode, string> processDataCallback)
    {
        while (BitConverter.ToInt32(bytes, 0) > 0)
        {
            //���ݳ���
            int len = BitConverter.ToInt32(bytes, 0);
            //����ĳ��Ⱥ�ʵ�ʳ��Ȳ���ͬʱ������
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

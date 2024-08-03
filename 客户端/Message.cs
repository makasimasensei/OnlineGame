using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 客户端
{
    internal class Message
    {
        static public byte[] GetBytes(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            int length = bytes.Length;
            byte[] bytesLength = BitConverter.GetBytes(length);
            return [.. bytesLength, .. bytes];
        }
    }
}

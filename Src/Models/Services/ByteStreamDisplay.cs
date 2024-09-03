using System.Diagnostics;
using System.Text;
using Serilog;

public class ByteStreamDisplay
{
    public static void Display(string prefix, byte[] data)
    {
        StringBuilder hex = new StringBuilder(data.Length * 2);
        hex.Append(prefix);
        for (int i = 0; i < data.Length; i++)
        {
            hex.AppendFormat("{0:x2} ", data[i]);
        }
        Log.Debug(hex.ToString());
    }
}
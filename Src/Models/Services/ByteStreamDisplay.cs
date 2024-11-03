using System.Text;
using Serilog;

public class ByteStreamDisplay
{
    public static void Display(string prefix, byte[] data)
    {
        var hex = new StringBuilder(data.Length * 3);
        hex.Append(prefix);
        for (var i = 0; i < data.Length; i++) hex.AppendFormat("{0:x2} ", data[i]);
        Log.Debug(hex.ToString());
    }
}
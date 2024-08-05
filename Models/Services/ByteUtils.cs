using System.Linq;

namespace Integra7AuralAlchemist.Models.Services;
public class ByteUtils
{
    // helper method to flatten sysex fragments into one long sysex byte array
    public static byte[] Flatten(params byte[][] arrays)
    {
        byte[] rv = new byte[arrays.Sum(a => a.Length)];
        int offset = 0;
        foreach (byte[] array in arrays)
        {
            System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
            offset += array.Length;
        }
        return rv;
    }
}
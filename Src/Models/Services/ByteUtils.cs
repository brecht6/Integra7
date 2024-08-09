using System;
using System.Diagnostics;
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

    public static byte LtlEnd_FirstByte7(long value)
    {
        Debug.Assert(value < 0x80000000);
        return (byte)(value & 0x7f);
    }

    public static byte LtlEnd_SecondByte7(long value)
    {
        Debug.Assert(value < 0x80000000);
        return (byte)((value >> 7) & 0x7f);
    }

    public static byte LtlEnd_ThirdByte7(long value)
    {
        Debug.Assert(value < 0x80000000);
        return (byte)((value >> 14) & 0x7f);
    }

    public static byte LtlEnd_FourthByte7(long value)
    {
        Debug.Assert(value < 0x80000000);
        return (byte)((value >> 21) & 0x7f);
    }

    public static byte[] IntToBytes7_2(long value)
    {
        Debug.Assert(value < 0x8000);
        return [LtlEnd_SecondByte7(value), LtlEnd_FirstByte7(value)];
    }

    public static byte[] IntToBytes7_4(long value)
    {
        Debug.Assert(value < 0x80000000);
        return [LtlEnd_FourthByte7(value), LtlEnd_ThirdByte7(value),
                LtlEnd_SecondByte7(value), LtlEnd_FirstByte7(value)];
    }

    public static byte[] AddressWithOffset(byte[] StartAddress, byte[] Offset)
    {
        Debug.Assert(StartAddress.Length >= Offset.Length);
        long saddr = ByteUtils.Bytes7ToInt(StartAddress);
        long offs = ByteUtils.Bytes7ToInt(Offset);
        long sum = saddr + offs;
        byte[] tempresult = ByteUtils.IntToBytes7_4(sum);
        int toremove = tempresult.Length - StartAddress.Length;
        if (toremove == 0)
        {   
            return tempresult;
        }
        byte[] result = new byte[StartAddress.Length];
        Array.Copy(tempresult, toremove, result, 0, result.Length);
        return result;
    }

    public static byte[] AddressWithOffset(byte[] StartAddress, byte[] Offset, byte[] ParameterAddress)
    {
        return ByteUtils.AddressWithOffset(ByteUtils.AddressWithOffset(StartAddress, Offset), ParameterAddress);
    }

    public static long Bytes7ToInt(byte[] data)
    {
        long total = 0;
        for (int i=0; i < data.Length; i++)
        {
            total <<= 7;
            total += data[i];
        }
        return total;
    }

    public static byte CheckSum(byte[] data)
    {
        int sum = 0;
        // & 0x7f is the same as modulo 128 and (a mod N + b mod N + ... + n mod N) mod N = (a + b + ... + n) mod N
        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i]; // (should should be data[i] mod 128 or data[i] & 0x7f but data[i] is 7-bit so this will always yield back data[i])
            sum &= 0x7f; 
        }
        int checksum = 0x80 - sum;
        return (byte)checksum;
    }

    public static byte[] IntToNibbled(long value, int noOfNibbles)
    {
        byte[] result = new byte[noOfNibbles];
        for (int i = 0; i < noOfNibbles; i++)
        {
            byte nibble = (byte)(value & 0x0f);
            result[noOfNibbles - 1 - i] = (byte)nibble;
            value >>= 4;
        }
        return result;
    }

    public static long NibbledToInt(byte[] data)
    {
        long result = 0;
        for (int i = 0; i < data.Length; i++)
        {
            result += (data[data.Length - 1 - i] << (i*4));
        }
        return result;
    }

    public static byte[] Concat(byte[] data1, byte[] data2)
    {
        byte[] result = new byte[data1.Length + data2.Length];
        System.Buffer.BlockCopy(data1, 0, result, 0, data1.Length);
        System.Buffer.BlockCopy(data2, 0, result, data1.Length, data2.Length);
        return result;
    }

    public static byte[] Slice(byte[] data, int from, int length)
    {
        Debug.Assert(from + length <= data.Length);

        byte[] result = new byte[length];
        System.Buffer.BlockCopy(data, from, result, 0, length);
        return result;
    }
}
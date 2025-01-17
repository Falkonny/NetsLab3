﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetsReal3
{
    public static class Utils
    {
        public static int CheckSum(BitArray arr)
        {
            var k = 0;
            for (var i = 0; i < arr.Length; i++)
                if (arr[i])
                    k += 1;
            return k;
        }

        public static BitArray DecimalToBinary(int number)
        {
            var array = new BitArray(new[] {BitConverter.GetBytes(number)[0]});

            return array;
        }

        public static BitArray[] SplitMessage(BitArray receivedMessage)
        {
            var result = new BitArray[receivedMessage.Length / 10];

            for (var i = 0; i < receivedMessage.Length / 10; i++)
            for (var j = 0; j < 10; j++)
            for (var k = 0; k < receivedMessage.Length; k++)
                result[i][j] = receivedMessage[k];

            return result;
        }

        public static BitArray Subsequence(this BitArray array, int offset, int length)
        {
            var result = new BitArray(length);

            for (var i = 0; i < length; i++) result[i] = array[offset + i];

            return result;
        }

        public static void Write(this BitArray array, int offset, BitArray data)
        {
            for (var i = 0; i < data.Count; i++) array[offset + i] = data[i];
        }

        public static Frame CreateFalseConnection()
        {
            var resp = new Frame();

            resp.Control = new BitArray(16);
            resp.Control.Write(0, DecimalToBinary(202));
            resp.Checksum = DecimalToBinary(0);
            resp.Data = new BitArray(16);
            return resp;
        }

        public static Frame CreateTrueConnection()
        {
            var resp = new Frame();

            resp.Control = new BitArray(16);
            resp.Control.Write(0, DecimalToBinary(201));
            resp.Checksum = DecimalToBinary(0);
            resp.Data = new BitArray(16);
            return resp;
        }


        public static List<byte[]> SplitFile(byte[] source)
        {
            var offset = 0;
            var list = new List<byte[]>();
            while (offset + 8 < source.Length)
            {
                list.Add(source.Skip(offset).Take(8).ToArray());
                offset += 8;
            }

            if (offset < source.Length) list.Add(source.Skip(offset).Take(source.Length - offset).ToArray());

            return list;
        }

        public static bool CompareBitArrays(BitArray arr1, BitArray arr2)
        {
            for (var i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i])
                    return false;
            return true;
        }
    }
}
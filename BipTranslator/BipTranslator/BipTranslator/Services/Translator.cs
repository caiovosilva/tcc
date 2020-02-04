using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BipTranslator.Services
{
    class Translator
    {
        public static BitArray GetBinaryFile(string fileName)
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            return new BitArray(bytes);
        }

        public static void SaveBitArrayToFile(BitArray bitArray, string fileName)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                for (int i = 0; i < bitArray.Count; i++)
                {
                    writer.Write(bitArray[i]);
                }
            }
        }
    }
}

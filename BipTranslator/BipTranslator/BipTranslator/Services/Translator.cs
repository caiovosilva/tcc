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
    }
}

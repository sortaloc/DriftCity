﻿using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace Shared.Util
{
    public class BinaryReaderExt : BinaryReader
    {
        public BinaryReaderExt(Stream stream)
            : base(stream, Encoding.Unicode)
        {
        }

        public BinaryReaderExt(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public Vector4 ReadVector4()
        {
            var vec = new Vector4();
            vec.X = ReadSingle();
            vec.Y = ReadSingle();
            vec.Z = ReadSingle();
            vec.W = ReadSingle();

            return vec;
        }

        public Vector2 ReadVector2()
        {
            var vec = new Vector2();
            vec.X = ReadSingle();
            vec.Y = ReadSingle();
            return vec;
        }

        public string ReadUnicode()
        {
            var sb = new StringBuilder();
            char val;
            do
            {
                val = ReadChar();
                if (val > 0)
                    sb.Append(val);
            } while (val > 0);
            return sb.ToString();
        }
        
        public string ReadUnicodeStatic(int maxLength)
        {
            var buf = ReadBytes(maxLength * 2);
            var str = Encoding.Unicode.GetString(buf);

            if (str.Contains("\0"))
                str = str.Substring(0, str.IndexOf('\0'));

            return str;
        }

        public string ReadUnicodePrefixed()
        {
            var length = ReadUInt16();
            return ReadUnicodeStatic(length);
        }

        public string ReadAscii()
        {
            var sb = new StringBuilder();
            byte val;
            do
            {
                val = ReadByte();
                sb.Append((char) val);
            } while (val > 0);
            return sb.ToString();
        }

        public string ReadAsciiStatic(int maxLength)
        {
            var buf = ReadBytes(maxLength);
            var str = Encoding.ASCII.GetString(buf);

            if (str.Contains("\0"))
                str = str.Substring(0, str.IndexOf('\0'));

            return str;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace DPF.Benchmark
{
    [Config(typeof(DPFConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class StructParsing
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct TcpHeader
        {
            public uint Source;
            public uint Destination;
            public ushort SourcePort;
            public ushort DestinationPort;
            public uint Flags;
            public uint Checksum;
        }

        public byte[] Bytes;

        public TcpHeader Header = new TcpHeader
        {
            Source = 444444444,
            Destination = 555555555,
            SourcePort = 5000,
            DestinationPort = 5000,
            Flags = 64,
            Checksum = 12345679,
        };

        [GlobalSetup]
        public void Setup()
        {
            Bytes = Serialize(Header);
        }

        [Benchmark]
        public TcpHeader Reflection()
        {
            var type = typeof(TcpHeader);
            int offset = 0;
            object data = new TcpHeader();
            foreach (var field in type.GetFields())
            {
                if (field.FieldType == typeof(uint))
                {
                    field.SetValue(data, BitConverter.ToUInt32(Bytes, offset));
                    offset += 4;
                }
                if (field.FieldType == typeof(ushort))
                {
                    field.SetValue(data, BitConverter.ToUInt16(Bytes, offset));
                    offset += 2;
                }
            }

            return (TcpHeader)data;
        }

        [Benchmark]
        public TcpHeader Manual()
        {
            int offset = 0;
            var data = new TcpHeader();

            data.Source = BitConverter.ToUInt32(Bytes, offset);
            offset += 4;

            data.Destination = BitConverter.ToUInt32(Bytes, offset);
            offset += 4;

            data.SourcePort = BitConverter.ToUInt16(Bytes, offset);
            offset += 2;

            data.DestinationPort = BitConverter.ToUInt16(Bytes, offset);
            offset += 2;

            data.Flags = BitConverter.ToUInt32(Bytes, offset);
            offset += 4;

            data.Checksum = BitConverter.ToUInt32(Bytes, offset);
            offset += 4;

            return data;
        }

        [Benchmark]
        public TcpHeader ManualWithUnsafe()
        {
            int offset = 0;
            var data = new TcpHeader();

            data.Source = ToUInt32(Bytes, ref offset);
            data.Destination = ToUInt32(Bytes, ref offset);
            data.SourcePort = ToUInt16(Bytes, ref offset);
            data.DestinationPort = ToUInt16(Bytes, ref offset);
            data.Flags = ToUInt32(Bytes, ref offset);
            data.Checksum = ToUInt32(Bytes, ref offset);

            return data;
        }


        private static unsafe ushort ToUInt16(byte[] bytes, ref int offset)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                offset += sizeof(ushort);
                ushort* primitivePtr = (ushort*)ptr;
                return *primitivePtr;
            }
        }

        private static unsafe uint ToUInt32(byte[] bytes, ref int offset)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                offset += sizeof(uint);
                uint* primitivePtr = (uint*)ptr;
                return *primitivePtr;
            }
        }

        [Benchmark]
        public TcpHeader PtrToStructure_GCHandle()
        {
            var handle = GCHandle.Alloc(Bytes, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = handle.AddrOfPinnedObject();
                return Marshal.PtrToStructure<TcpHeader>(ptr);
            }
            finally
            {
                handle.Free();
            }
        }

        [Benchmark]
        public unsafe TcpHeader PtrToStructure_Fixed()
        {
            fixed (byte* ptr = &Bytes[0])
            {
                return Marshal.PtrToStructure<TcpHeader>(new IntPtr(ptr));
            }
        }

        [Benchmark]
        public unsafe TcpHeader Pointer()
        {
            fixed (byte* ptr = &Bytes[0])
            {
                TcpHeader* tcpHeaderPtr = (TcpHeader*)ptr;
                return *tcpHeaderPtr;
            }
        }

        private static byte[] Serialize(TcpHeader header)
        {
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(header.Source));
            bytes.AddRange(BitConverter.GetBytes(header.Destination));
            bytes.AddRange(BitConverter.GetBytes(header.SourcePort));
            bytes.AddRange(BitConverter.GetBytes(header.DestinationPort));
            bytes.AddRange(BitConverter.GetBytes(header.Flags));
            bytes.AddRange(BitConverter.GetBytes(header.Checksum));
            return bytes.ToArray();
        }
    }
}

using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DPF.ArrayPool
{
    class Program
    {
        public static async Task Main()
        {
            //await Default();
            await SharedArrayPool();
            // await OwnArrayPool();
        }

        private static async Task Default()
        {
            var buffer = new byte[8 * 1024];
            while (true)
            {
                foreach (var file in Directory.EnumerateFiles("images"))
                {
                    var fileInfo = new FileInfo(file);
                    var array = new byte[fileInfo.Length];
                    await using var stream = File.OpenRead(file);
                    await ReadStreamToArray(fileInfo, stream, buffer, array);
                }
            }
        }

        private static async Task SharedArrayPool()
        {
            var arrayPool = ArrayPool<byte>.Shared;
            var buffer = arrayPool.Rent(8 * 1024);
            try
            {
                while (true)
                {
                    foreach (var file in Directory.EnumerateFiles("images"))
                    {
                        var fileInfo = new FileInfo(file);
                        var array = arrayPool.Rent(Convert.ToInt32(fileInfo.Length));
                        try
                        {
                            await using var stream = File.OpenRead(file);
                            await ReadStreamToArray(fileInfo, stream, buffer, array);
                        }
                        finally
                        {
                            arrayPool.Return(array);
                        }
                    }
                }
            }
            finally
            {
                arrayPool.Return(buffer);
            }
        }

        private static async Task OwnArrayPool()
        {
            var biggestFile = Directory.GetFiles("images").Select(x => new FileInfo(x)).Select(x => x.Length).Max();

            var arrayPool = ArrayPool<byte>.Create(maxArrayLength: Convert.ToInt32(biggestFile), maxArraysPerBucket: 1);
            var buffer = arrayPool.Rent(8 * 1024);
            try
            {
                while (true)
                {
                    foreach (var file in Directory.EnumerateFiles("images"))
                    {
                        var fileInfo = new FileInfo(file);
                        var array = arrayPool.Rent(Convert.ToInt32(fileInfo.Length));
                        try
                        {
                            await using var stream = File.OpenRead(file);
                            await ReadStreamToArray(fileInfo, stream, buffer, array);
                        }
                        finally
                        {
                            arrayPool.Return(array);
                        }
                    }
                }
            }
            finally
            {
                arrayPool.Return(buffer);
            }
        }

        private static async ValueTask ReadStreamToArray(FileInfo fileInfo, FileStream stream, byte[] buffer, byte[] array)
        {
            var read = 0;
            while (read != fileInfo.Length)
            {
                int bytes = await stream.ReadAsync(buffer);
                buffer.AsSpan(0, bytes).CopyTo(array.AsSpan(read));
                read += bytes;
            }
        }

        private static ArrayPool<byte> GetArrayPool()
        {
            return ArrayPool<byte>.Shared;
        }
    }
}

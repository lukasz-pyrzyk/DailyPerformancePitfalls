using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DPF.ArrayPool
{
    class Program
    {
        private const int Iterations = 1000;

        public static async Task Main()
        {
            //await Default();
            // await SharedArrayPool();
            await OwnArrayPool();
        }

        private static async Task Default()
        {
            var buffer = new byte[8 * 1024];
            for (int i = 0; i < Iterations; i++)
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
                for (int i = 0; i < Iterations; i++)
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
                for (int i = 0; i < Iterations; i++)
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
            var position = 0;
            while (position != fileInfo.Length)
            {
                int bytes = await stream.ReadAsync(buffer);
                Array.Copy(buffer, 0, array, position, bytes);
                position += bytes;
            }
        }

        private static ArrayPool<byte> GetArrayPool()
        {
            return ArrayPool<byte>.Shared;
        }
    }
}

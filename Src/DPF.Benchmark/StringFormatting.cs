using System;
using System.Buffers;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace DPF.Benchmark
{
    [MemoryDiagnoser]
    public class StringFormatting
    {
        private const int Count = 100;
        private readonly StringBuilder _cachedStringBuilder = new StringBuilder(70 * Count);

        [Benchmark(Baseline = true)]
        public string Default()
        {
            var html = "<p>";
            html += "<h2>";
            for (int i = 0; i < Count; i++)
            {
                html += $"Generated guid \"{Guid.NewGuid()}\"";
            }
            html += "</h2>";
            html += "</p>";
            return html;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var html = new StringBuilder("<p>");
            html.Append("<h2>");
            for (int i = 0; i < Count; i++)
            {
                html.Append($"Generated guid \"{Guid.NewGuid()}\"");
            }
            html.Append("</h2>");
            html.Append("</p>");
            return html.ToString();
        }

        [Benchmark]
        public string StringBuilderWithInitializedSize()
        {
            var html = new StringBuilder(70 * Count);
            html.Append("<p>");
            html.Append("<h2>");
            for (int i = 0; i < Count; i++)
            {
                html.Append($"Generated guid \"{Guid.NewGuid()}\"");
            }
            html.Append("</h2>");
            html.Append("</p>");
            return html.ToString();
        }

        [Benchmark]
        public string StringBuilderWithInitializedSizeAndFormat()
        {
            var html = new StringBuilder(70 * Count);
            html.Append("<p>");
            html.Append("<h2>");
            for (int i = 0; i < Count; i++)
            {
                html.AppendFormat("Generated guid \"{0}\"", Guid.NewGuid());
            }
            html.Append("</h2>");
            html.Append("</p>");
            return html.ToString();
        }

        [Benchmark]
        public string CachedStringBuilderWithFormat()
        {
            _cachedStringBuilder.Clear();
            _cachedStringBuilder.Append("<p>");
            _cachedStringBuilder.Append("<h2>");
            for (int i = 0; i < Count; i++)
            {
                _cachedStringBuilder.AppendFormat("Generated guid \"{0}\"", Guid.NewGuid());
            }
            _cachedStringBuilder.Append("</h2>");
            _cachedStringBuilder.Append("</p>");
            return _cachedStringBuilder.ToString();
        }

        [Benchmark]
        public string ValueStringBuilder()
        {
            using var stringBuilder = new ValueStringBuilder(70 * Count);
            stringBuilder.Append("<p>");
            stringBuilder.Append("<h2>");
            for (int i = 0; i < Count; i++)
            {
                stringBuilder.Append("Generated guid \"");
                stringBuilder.Append(Guid.NewGuid());
                stringBuilder.Append("\"");
            }
            stringBuilder.Append("</h2>");
            stringBuilder.Append("</p>");
            return _cachedStringBuilder.ToString();
        }

        [Benchmark]
        public string NewValueStringBuilder()
        {
            using var stringBuilder = new ValueStringBuilder(70 * Count);
            stringBuilder.Append("<p>");
            stringBuilder.Append("<h2>");
            for (int i = 0; i < Count; i++)
            {
                stringBuilder.Append("Generated guid \"");
                stringBuilder.Append(Guid.NewGuid());
                stringBuilder.Append("\"");
            }
            stringBuilder.Append("</h2>");
            stringBuilder.Append("</p>");
            return _cachedStringBuilder.ToString();
        }
    }

    ref struct ValueStringBuilder
    {
        private readonly char[] _characters;
        private int _position;

        public ValueStringBuilder(int capacity)
        {
            _characters = ArrayPool<char>.Shared.Rent(capacity);
            _position = 0;
        }

        public void Append(string text)
        {
            foreach (char c in text)
            {
                _characters[_position++] = c;
            }
        }

        public void Append(Guid id)
        {
            id.TryFormat(_characters.AsSpan().Slice(0, _position), out var written);
            _position += written;
        }

        public override readonly string ToString()
        {
            return new string(_characters.AsSpan().Slice(0, _position));
        }

        public void Dispose()
        {
            ArrayPool<char>.Shared.Return(_characters);
            _position = 0;
        }
    }
}

﻿/*
 * https://github.com/LokiHonoo/development-resources
 * Copyright (C) Loki Honoo 2015. All rights reserved.
 *
 * This code page is published by the MIT license.
 */

using System;
using System.Globalization;
using System.Text;

#pragma warning disable IDE0130 // 命名空间与文件夹结构不匹配
namespace Honoo.IO
#pragma warning restore IDE0130 // 命名空间与文件夹结构不匹配
{
    /// <summary>
    /// 二进制对象辅助。
    /// </summary>
    internal static class Binaries
    {
        #region 比较

        /// <summary>
        /// 比较字节数组。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static bool SequenceEqual(byte[] first, byte[] second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);
            if (first.Length == second.Length)
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (first[i] != second[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 比较字节数组。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="firstOffset"></param>
        /// <param name="second"></param>
        /// <param name="secondOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static bool SequenceEqual(byte[] first, int firstOffset, byte[] second, int secondOffset, int length)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);
            if (first.Length - firstOffset >= length && second.Length - secondOffset >= length)
            {
                for (int i = 0; i < length; i++)
                {
                    if (first[firstOffset + i] != second[secondOffset + i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion 比较

        #region 转换

        /// <summary>
        /// 以大端序读取，指定要读取的字节数组长度（最大 2 字节）转换为 Int16 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static short BEToInt16(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 2)
            {
                throw new ArgumentException("Input length must be between 1 - 2.");
            }
            int result;
            if (length == 2)
            {
                result = (buffer[offset] & 0xFF) << 8;
                result |= buffer[offset + 1] & 0xFF;
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return (short)result;
        }

        /// <summary>
        /// 以大端序读取，指定要读取的字节数组长度（最大 4 字节）转换为 Int32 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static int BEToInt32(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 4)
            {
                throw new ArgumentException("Input length must be between 1 - 4.");
            }
            int result;
            if (length == 4)
            {
                result = (buffer[offset] & 0xFF) << 24;
                result |= (buffer[offset + 1] & 0xFF) << 16;
                result |= (buffer[offset + 2] & 0xFF) << 8;
                result |= buffer[offset + 3] & 0xFF;
            }
            else if (length == 3)
            {
                result = (buffer[offset] & 0xFF) << 16;
                result |= (buffer[offset + 1] & 0xFF) << 8;
                result |= buffer[offset + 2] & 0xFF;
            }
            else if (length == 2)
            {
                result = (buffer[offset] & 0xFF) << 8;
                result |= buffer[offset + 1] & 0xFF;
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return result;
        }

        /// <summary>
        /// 以大端序读取，指定要读取的字节数组长度（最大 8 字节）转换为 Int64 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static long BEToInt64(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 8)
            {
                throw new ArgumentException("Input length must be between 1 - 8.");
            }
            long result;
            if (length == 8)
            {
                result = (buffer[offset] & 0xFFL) << 56;
                result |= (buffer[offset + 1] & 0xFFL) << 48;
                result |= (buffer[offset + 2] & 0xFFL) << 40;
                result |= (buffer[offset + 3] & 0xFFL) << 32;
                result |= (buffer[offset + 4] & 0xFFL) << 24;
                result |= (buffer[offset + 5] & 0xFFL) << 16;
                result |= (buffer[offset + 6] & 0xFFL) << 8;
                result |= buffer[offset + 7] & 0xFFL;
            }
            else if (length == 7)
            {
                result = (buffer[offset] & 0xFFL) << 48;
                result |= (buffer[offset + 1] & 0xFFL) << 40;
                result |= (buffer[offset + 2] & 0xFFL) << 32;
                result |= (buffer[offset + 3] & 0xFFL) << 24;
                result |= (buffer[offset + 4] & 0xFFL) << 16;
                result |= (buffer[offset + 5] & 0xFFL) << 8;
                result |= buffer[offset + 6] & 0xFFL;
            }
            else if (length == 6)
            {
                result = (buffer[offset] & 0xFFL) << 40;
                result |= (buffer[offset + 1] & 0xFFL) << 32;
                result |= (buffer[offset + 2] & 0xFFL) << 24;
                result |= (buffer[offset + 3] & 0xFFL) << 16;
                result |= (buffer[offset + 4] & 0xFFL) << 8;
                result |= buffer[offset + 5] & 0xFFL;
            }
            else if (length == 5)
            {
                result = (buffer[offset] & 0xFFL) << 32;
                result |= (buffer[offset + 1] & 0xFFL) << 24;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
                result |= (buffer[offset + 3] & 0xFFL) << 8;
                result |= buffer[offset + 4] & 0xFFL;
            }
            else if (length == 4)
            {
                result = (buffer[offset] & 0xFFL) << 24;
                result |= (buffer[offset + 1] & 0xFFL) << 16;
                result |= (buffer[offset + 2] & 0xFFL) << 8;
                result |= buffer[offset + 3] & 0xFFL;
            }
            else if (length == 3)
            {
                result = (buffer[offset] & 0xFFL) << 16;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= buffer[offset + 2] & 0xFFL;
            }
            else if (length == 2)
            {
                result = (buffer[offset] & 0xFFL) << 8;
                result |= buffer[offset + 1] & 0xFFL;
            }
            else
            {
                result = buffer[offset] & 0xFFL;
            }
            return result;
        }

        /// <summary>
        /// 以大端序读取，指定要读取的字节数组长度（最大 2 字节）转换为 UInt16 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static ushort BEToUInt16(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 2)
            {
                throw new ArgumentException("Input length must be between 1 - 2.");
            }
            int result;
            if (length == 2)
            {
                result = (buffer[offset] & 0xFF) << 8;
                result |= buffer[offset + 1] & 0xFF;
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return (ushort)result;
        }

        /// <summary>
        /// 以大端序读取，指定要读取的字节数组长度（最大 4 字节）转换为 UInt32 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static uint BEToUInt32(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 4)
            {
                throw new ArgumentException("Input length must be between 1 - 4.");
            }
            uint result;
            if (length == 4)
            {
                result = (buffer[offset] & 0xFFU) << 24;
                result |= (buffer[offset + 1] & 0xFFU) << 16;
                result |= (buffer[offset + 2] & 0xFFU) << 8;
                result |= buffer[offset + 3] & 0xFFU;
            }
            else if (length == 3)
            {
                result = (buffer[offset] & 0xFFU) << 16;
                result |= (buffer[offset + 1] & 0xFFU) << 8;
                result |= buffer[offset + 2] & 0xFFU;
            }
            else if (length == 2)
            {
                result = (buffer[offset] & 0xFFU) << 8;
                result |= buffer[offset + 1] & 0xFFU;
            }
            else
            {
                result = buffer[offset] & 0xFFU;
            }
            return result;
        }

        /// <summary>
        /// 以大端序读取，指定要读取的字节数组长度（最大 8 字节）转换为 UInt64 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static ulong BEToUInt64(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 8)
            {
                throw new ArgumentException("Input length must be between 1 - 8.");
            }
            ulong result;
            if (length == 8)
            {
                result = (buffer[offset] & 0xFFUL) << 56;
                result |= (buffer[offset + 1] & 0xFFUL) << 48;
                result |= (buffer[offset + 2] & 0xFFUL) << 40;
                result |= (buffer[offset + 3] & 0xFFUL) << 32;
                result |= (buffer[offset + 4] & 0xFFUL) << 24;
                result |= (buffer[offset + 5] & 0xFFUL) << 16;
                result |= (buffer[offset + 6] & 0xFFUL) << 8;
                result |= buffer[offset + 7] & 0xFFUL;
            }
            else if (length == 7)
            {
                result = (buffer[offset] & 0xFFUL) << 48;
                result |= (buffer[offset + 1] & 0xFFUL) << 40;
                result |= (buffer[offset + 2] & 0xFFUL) << 32;
                result |= (buffer[offset + 3] & 0xFFUL) << 24;
                result |= (buffer[offset + 4] & 0xFFUL) << 16;
                result |= (buffer[offset + 5] & 0xFFUL) << 8;
                result |= buffer[offset + 6] & 0xFFUL;
            }
            else if (length == 6)
            {
                result = (buffer[offset] & 0xFFUL) << 40;
                result |= (buffer[offset + 1] & 0xFFUL) << 32;
                result |= (buffer[offset + 2] & 0xFFUL) << 24;
                result |= (buffer[offset + 3] & 0xFFUL) << 16;
                result |= (buffer[offset + 4] & 0xFFUL) << 8;
                result |= buffer[offset + 5] & 0xFFUL;
            }
            else if (length == 5)
            {
                result = (buffer[offset] & 0xFFUL) << 32;
                result |= (buffer[offset + 1] & 0xFFUL) << 24;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
                result |= (buffer[offset + 3] & 0xFFUL) << 8;
                result |= buffer[offset + 4] & 0xFFUL;
            }
            else if (length == 4)
            {
                result = (buffer[offset] & 0xFFUL) << 24;
                result |= (buffer[offset + 1] & 0xFFUL) << 16;
                result |= (buffer[offset + 2] & 0xFFUL) << 8;
                result |= buffer[offset + 3] & 0xFFUL;
            }
            else if (length == 3)
            {
                result = (buffer[offset] & 0xFFUL) << 16;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= buffer[offset + 2] & 0xFFUL;
            }
            else if (length == 2)
            {
                result = (buffer[offset] & 0xFFUL) << 8;
                result |= buffer[offset + 1] & 0xFFUL;
            }
            else
            {
                result = buffer[offset] & 0xFFUL;
            }
            return result;
        }

        /// <summary>
        /// 移除多个指定的字符串，将十六进制字符串转换为字节数组。
        /// </summary>
        /// <param name="hex">十六进制字符串。</param>
        /// <param name="replaces">要移除的字符串集合。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static byte[] GetBytes(string hex, params string[] replaces)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(hex);
            ArgumentNullException.ThrowIfNull(replaces);
            foreach (var replace in replaces)
            {
                hex = hex.Replace(replace, string.Empty, StringComparison.Ordinal);
            }
            return GetBytes(hex);
        }

        /// <summary>
        /// 将十六进制字符串转换为字节数组。字符串必须是无分隔符的表示形式。
        /// </summary>
        /// <param name="hex">无分隔符的十六进制字符串。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static byte[] GetBytes(string hex)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(hex);
            if (hex.Length % 2 > 0)
            {
                throw new ArgumentException("Hex string length must be multiple of 2.");
            }
            byte[] result = new byte[hex.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return result;
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <param name="uppercase">字符转换为大写。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:将字符串规范化为大写", Justification = "<挂起>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:请删除不必要的忽略", Justification = "<挂起>")]
        internal static string GetHex(byte[] bytes, bool uppercase)
        {
            ArgumentNullException.ThrowIfNull(bytes);
            string hex = Convert.ToHexString(bytes);
            return uppercase ? hex : hex.ToLowerInvariant();
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数组长度。</param>
        /// <param name="uppercase">字符转换为大写。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:将字符串规范化为大写", Justification = "<挂起>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:请删除不必要的忽略", Justification = "<挂起>")]
        internal static string GetHex(byte[] buffer, int offset, int length, bool uppercase)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            string hex = Convert.ToHexString(buffer, offset, length);
            return uppercase ? hex : hex.ToLowerInvariant();
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数组长度。</param>
        /// <param name="uppercase">字符转换为大写，不影响分割符、缩进符。</param>
        /// <param name="split">指定每个字节之间的分隔符。</param>
        /// <param name="lineBreaks">转换指定的字节个数后换行。设置为 0 不换行。</param>
        /// <param name="indents">指定每行缩进字符。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static string GetHex(byte[] buffer, int offset, int length, bool uppercase, string split, int lineBreaks, string indents)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            split ??= string.Empty;
            indents ??= string.Empty;
            var result = new StringBuilder();
            bool newLine = true;
            int count = 0;
            while (offset < length)
            {
                if (newLine)
                {
                    if (indents.Length > 0)
                    {
                        result.Append(indents);
                    }
                    newLine = false;
                }
                else if (lineBreaks > 0 && count >= lineBreaks)
                {
                    result.Append(Environment.NewLine);
                    newLine = true;
                    count = 0;
                }
                else
                {
                    string h = buffer[offset].ToString("x2", CultureInfo.InvariantCulture);
                    result.Append(uppercase ? h.ToUpperInvariant() : h);
                    count++;
                    offset++;
                    if (split.Length > 0 && offset < length)
                    {
                        result.Append(split);
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 将 Int16 类型转换为大端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] Int16ToBE(short value)
        {
            return [(byte)(value >> 8), (byte)value];
        }

        /// <summary>
        /// 将 Int16 类型转换为小端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] Int16ToLE(short value)
        {
            return [(byte)value, (byte)(value >> 8)];
        }

        /// <summary>
        /// 将 Int32 类型转换为大端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] Int32ToBE(int value)
        {
            return [(byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value];
        }

        /// <summary>
        /// 将 Int32 类型转换为小端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] Int32ToLE(int value)
        {
            return [(byte)value, (byte)(value >> 8), (byte)(value >> 16), (byte)(value >> 24)];
        }

        /// <summary>
        /// 将 Int64 类型转换为大端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] Int64ToBE(long value)
        {
            return
              [
                (byte)(value >> 56),
                (byte)(value >> 48),
                (byte)(value >> 40),
                (byte)(value >> 32),
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value,
            ];
        }

        /// <summary>
        /// 将 Int64 类型转换为小端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] Int64ToLE(long value)
        {
            return
            [
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24),
                (byte)(value >> 32),
                (byte)(value >> 40),
                (byte)(value >> 48),
                (byte)(value >> 56),
            ];
        }

        /// <summary>
        /// 以小端序读取，指定要读取的字节数组长度（最大 2 字节）转换为 Int16 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static short LEToInt16(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 2)
            {
                throw new ArgumentException("Input length must be between 1 - 2.");
            }
            int result;
            if (length == 2)
            {
                result = buffer[offset] & 0xFF;
                result |= (buffer[offset + 1] & 0xFF) << 8;
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return (short)result;
        }

        /// <summary>
        /// 以小端序读取，定要读取的字节数组长度（最大 4 字节）转换为 Int32 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static int LEToInt32(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 4)
            {
                throw new ArgumentException("Input length must be between 1 - 4.");
            }
            int result;
            if (length == 4)
            {
                result = buffer[offset] & 0xFF;
                result |= (buffer[offset + 1] & 0xFF) << 8;
                result |= (buffer[offset + 2] & 0xFF) << 16;
                result |= (buffer[offset + 3] & 0xFF) << 24;
            }
            else if (length == 3)
            {
                result = buffer[offset] & 0xFF;
                result |= (buffer[offset + 1] & 0xFF) << 8;
                result |= (buffer[offset + 2] & 0xFF) << 16;
            }
            else if (length == 2)
            {
                result = buffer[offset] & 0xFF;
                result |= (buffer[offset + 1] & 0xFF) << 8;
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return result;
        }

        /// <summary>
        /// 以小端序读取，指定要读取的字节数组长度（最大 8 字节）转换为 Int64 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static long LEToInt64(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 8)
            {
                throw new ArgumentException("Input length must be between 1 - 8.");
            }
            long result;
            if (length == 8)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
                result |= (buffer[offset + 3] & 0xFFL) << 24;
                result |= (buffer[offset + 4] & 0xFFL) << 32;
                result |= (buffer[offset + 5] & 0xFFL) << 40;
                result |= (buffer[offset + 6] & 0xFFL) << 48;
                result |= (buffer[offset + 7] & 0xFFL) << 56;
            }
            else if (length == 7)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
                result |= (buffer[offset + 3] & 0xFFL) << 24;
                result |= (buffer[offset + 4] & 0xFFL) << 32;
                result |= (buffer[offset + 5] & 0xFFL) << 40;
                result |= (buffer[offset + 6] & 0xFFL) << 48;
            }
            else if (length == 6)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
                result |= (buffer[offset + 3] & 0xFFL) << 24;
                result |= (buffer[offset + 4] & 0xFFL) << 32;
                result |= (buffer[offset + 5] & 0xFFL) << 40;
            }
            else if (length == 5)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
                result |= (buffer[offset + 3] & 0xFFL) << 24;
                result |= (buffer[offset + 4] & 0xFFL) << 32;
            }
            else if (length == 4)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
                result |= (buffer[offset + 3] & 0xFFL) << 24;
            }
            else if (length == 3)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
                result |= (buffer[offset + 2] & 0xFFL) << 16;
            }
            else if (length == 2)
            {
                result = buffer[offset] & 0xFFL;
                result |= (buffer[offset + 1] & 0xFFL) << 8;
            }
            else
            {
                result = buffer[offset] & 0xFFL;
            }
            return result;
        }

        /// <summary>
        /// 以小端序读取，指定要读取的字节数组长度（最大 2 字节）转换为 UInt16 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static ushort LEToUInt16(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 2)
            {
                throw new ArgumentException("Input length must be between 1 - 2.");
            }
            int result;
            if (length == 2)
            {
                result = buffer[offset] & 0xFF;
                result |= (buffer[offset + 1] & 0xFF) << 8;
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return (ushort)result;
        }

        /// <summary>
        /// 以小端序读取，指定要读取的字节数组长度（最大 4 字节）转换为 UInt32 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static uint LEToUInt32(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 4)
            {
                throw new ArgumentException("Input length must be between 1 - 4.");
            }
            uint result;
            if (length == 4)
            {
                result = buffer[offset] & 0xFFU;
                result |= (buffer[offset + 1] & 0xFFU) << 8;
                result |= (buffer[offset + 2] & 0xFFU) << 16;
                result |= (buffer[offset + 3] & 0xFFU) << 24;
            }
            else if (length == 3)
            {
                result = buffer[offset] & 0xFFU;
                result |= (buffer[offset + 1] & 0xFFU) << 8;
                result |= (buffer[offset + 2] & 0xFFU) << 16;
            }
            else if (length == 2)
            {
                result = buffer[offset] & 0xFFU;
                result |= (buffer[offset + 1] & 0xFFU) << 8;
            }
            else
            {
                result = buffer[offset] & 0xFFU;
            }
            return result;
        }

        /// <summary>
        /// 以小端序读取，指定要读取的字节数组长度（最大 8 字节）转换为 UInt64 类型输出。
        /// </summary>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        internal static ulong LEToUInt64(byte[] buffer, int offset, int length)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            if (length <= 0 || length > 8)
            {
                throw new ArgumentException("Input length must be between 1 - 8.");
            }
            ulong result;
            if (length == 8)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
                result |= (buffer[offset + 3] & 0xFFUL) << 24;
                result |= (buffer[offset + 4] & 0xFFUL) << 32;
                result |= (buffer[offset + 5] & 0xFFUL) << 40;
                result |= (buffer[offset + 6] & 0xFFUL) << 48;
                result |= (buffer[offset + 7] & 0xFFUL) << 56;
            }
            else if (length == 7)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
                result |= (buffer[offset + 3] & 0xFFUL) << 24;
                result |= (buffer[offset + 4] & 0xFFUL) << 32;
                result |= (buffer[offset + 5] & 0xFFUL) << 40;
                result |= (buffer[offset + 6] & 0xFFUL) << 48;
            }
            else if (length == 6)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
                result |= (buffer[offset + 3] & 0xFFUL) << 24;
                result |= (buffer[offset + 4] & 0xFFUL) << 32;
                result |= (buffer[offset + 5] & 0xFFUL) << 40;
            }
            else if (length == 5)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
                result |= (buffer[offset + 3] & 0xFFUL) << 24;
                result |= (buffer[offset + 4] & 0xFFUL) << 32;
            }
            else if (length == 4)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
                result |= (buffer[offset + 3] & 0xFFUL) << 24;
            }
            else if (length == 3)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
                result |= (buffer[offset + 2] & 0xFFUL) << 16;
            }
            else if (length == 2)
            {
                result = buffer[offset] & 0xFFUL;
                result |= (buffer[offset + 1] & 0xFFUL) << 8;
            }
            else
            {
                result = buffer[offset] & 0xFFUL;
            }
            return result;
        }

        /// <summary>
        /// 将 UInt16 类型转换为大端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] UInt16ToBE(ushort value)
        {
            return [(byte)(value >> 8), (byte)value];
        }

        /// <summary>
        /// 将 UInt16 类型转换为小端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] UInt16ToLE(ushort value)
        {
            return [(byte)value, (byte)(value >> 8)];
        }

        /// <summary>
        /// 将 UInt32 类型转换为大端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] UInt32ToBE(uint value)
        {
            return [(byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value];
        }

        /// <summary>
        /// 将 UInt32 类型转换为小端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] UInt32ToLE(uint value)
        {
            return [(byte)value, (byte)(value >> 8), (byte)(value >> 16), (byte)(value >> 24)];
        }

        /// <summary>
        /// 将 UInt64 类型转换为大端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] UInt64ToBE(ulong value)
        {
            return
            [
                (byte)(value >> 56),
                (byte)(value >> 48),
                (byte)(value >> 40),
                (byte)(value >> 32),
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value,
            ];
        }

        /// <summary>
        /// 将 UInt64 类型转换为小端序字节数组。
        /// </summary>
        /// <param name="value">要转换的值。</param>
        /// <returns></returns>
        internal static byte[] UInt64ToLE(ulong value)
        {
            return
            [
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24),
                (byte)(value >> 32),
                (byte)(value >> 40),
                (byte)(value >> 48),
                (byte)(value >> 56),
            ];
        }

        #endregion 转换
    }
}
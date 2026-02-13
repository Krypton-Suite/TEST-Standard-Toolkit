#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 *  Reed-Solomon implementation based on standard GF(256) algorithm per ISO/IEC 18004.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Krypton.Utilities;

/// <summary>
/// Native QR code generator. Produces QR code module matrices without external dependencies.
/// </summary>
internal static class QRCodeGeneratorCore
{
    #region Constants

    private const int GF_SIZE = 256;
    private const int GF_PRIMITIVE = 0x11D; // x^8 + x^4 + x^3 + x^2 + 1

    #endregion

    #region Capacity Table (Versions 1-10, Byte mode)

    /// <summary>Data capacity in bytes for each version (1-10) and ECC level.</summary>
    private static readonly int[,] ByteCapacity =
    {
        { 17, 14, 11, 7 },   // V1
        { 32, 26, 20, 14 },  // V2
        { 53, 42, 32, 24 },  // V3
        { 78, 62, 46, 34 },  // V4
        { 106, 84, 60, 44 }, // V5
        { 134, 106, 74, 58 },// V6
        { 154, 122, 86, 64 },// V7
        { 192, 152, 108, 84 },// V8
        { 230, 180, 130, 98 },// V9
        { 271, 213, 151, 119 }// V10
    };

    /// <summary>ECC block structure: [versionIndex, eccLevel] -> (totalDataCodewords, ecPerBlock, block1Count, block1Size, block2Count, block2Size)</summary>
    private static readonly (int TotalData, int EcPerBlock, int Block1Count, int Block1Size, int Block2Count, int Block2Size)[,] EccBlocks =
    {
        { (19, 7, 1, 19, 0, 0), (16, 10, 1, 16, 0, 0), (13, 13, 1, 13, 0, 0), (9, 17, 1, 9, 0, 0) },   // V1
        { (34, 10, 1, 34, 0, 0), (28, 16, 1, 28, 0, 0), (22, 22, 1, 22, 0, 0), (16, 28, 1, 16, 0, 0) },  // V2
        { (55, 15, 1, 55, 0, 0), (44, 26, 1, 44, 0, 0), (34, 18, 2, 17, 0, 0), (26, 22, 2, 13, 0, 0) },  // V3
        { (80, 20, 1, 80, 0, 0), (64, 18, 2, 32, 0, 0), (48, 26, 2, 24, 0, 0), (36, 16, 4, 9, 0, 0) },   // V4
        { (108, 26, 1, 108, 0, 0), (86, 24, 2, 43, 0, 0), (62, 18, 2, 15, 2, 16), (46, 22, 2, 11, 2, 12) },// V5
        { (136, 18, 2, 68, 0, 0), (108, 16, 4, 27, 0, 0), (76, 24, 4, 19, 0, 0), (60, 28, 4, 15, 0, 0) }, // V6
        { (156, 20, 2, 78, 0, 0), (124, 18, 4, 31, 0, 0), (88, 18, 2, 14, 4, 15), (66, 26, 4, 13, 1, 14) },// V7
        { (194, 24, 2, 97, 0, 0), (154, 22, 2, 38, 2, 39), (110, 22, 4, 18, 2, 19), (86, 26, 4, 14, 2, 15) },// V8
        { (232, 30, 2, 116, 0, 0), (182, 22, 3, 36, 2, 37), (132, 20, 4, 16, 4, 17), (100, 24, 4, 12, 4, 13) },// V9
        { (274, 18, 2, 68, 2, 69), (216, 26, 4, 43, 1, 44), (154, 24, 6, 19, 2, 20), (122, 28, 6, 15, 2, 16) } // V10
    };

    /// <summary>Remainder bits for each version (padding after data).</summary>
    private static readonly int[] RemainderBits = { 0, 7, 7, 7, 7, 7, 0, 0, 0, 0 };

    /// <summary>Format string bits for each (eccLevel, maskPattern). 15 bits.</summary>
    private static readonly int[,] FormatBits =
    {
        { 0x77C4, 0x72F3, 0x7DAA, 0x789D, 0x662F, 0x6318, 0x6C41, 0x6976 }, // L
        { 0x5412, 0x5125, 0x5E7C, 0x5B4B, 0x45F9, 0x40CE, 0x4F97, 0x4AA0 }, // M
        { 0x355F, 0x3068, 0x3F31, 0x3A06, 0x24B4, 0x2183, 0x2EDA, 0x2BED }, // Q
        { 0x1689, 0x13BE, 0x1CE7, 0x19D0, 0x0762, 0x0255, 0x0D0C, 0x083B }  // H
    };

    #endregion

    #region Public API

    /// <summary>
    /// Generates a QR code module matrix for the given content.
    /// </summary>
    /// <param name="content">The text or data to encode (UTF-8).</param>
    /// <param name="eccLevel">Error correction level.</param>
    /// <returns>A 2D bool array where true = dark module.</returns>
    public static bool[,] Generate(string content, QRErrorCorrectionLevel eccLevel)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("Content cannot be null or empty.", nameof(content));
        }

        byte[] data = Encoding.UTF8.GetBytes(content);
        int version = GetMinimumVersion(data.Length, eccLevel);
        byte[] dataCodewords = EncodeData(data, version, eccLevel);
        byte[] fullMessage = AddErrorCorrection(dataCodewords, version, eccLevel);
        return BuildMatrix(fullMessage, version, eccLevel);
    }

    /// <summary>
    /// Generates a QR code module matrix for raw bytes.
    /// </summary>
    public static bool[,] Generate(byte[] data, QRErrorCorrectionLevel eccLevel)
    {
        if (data == null || data.Length == 0)
        {
            throw new ArgumentException("Data cannot be null or empty.", nameof(data));
        }

        int version = GetMinimumVersion(data.Length, eccLevel);
        byte[] dataCodewords = EncodeData(data, version, eccLevel);
        byte[] fullMessage = AddErrorCorrection(dataCodewords, version, eccLevel);
        return BuildMatrix(fullMessage, version, eccLevel);
    }

    #endregion

    #region Version Selection

    private static int GetMinimumVersion(int byteCount, QRErrorCorrectionLevel eccLevel)
    {
        int eccIndex = (int)eccLevel;
        for (int v = 0; v < 10; v++)
        {
            if (ByteCapacity[v, eccIndex] >= byteCount)
            {
                return v + 1;
            }
        }

        throw new ArgumentException($"Data too long for QR code. Maximum ~{ByteCapacity[9, eccIndex]} bytes for ECC {eccLevel}.", nameof(byteCount));
    }

    #endregion

    #region Data Encoding (Byte Mode)

    private static byte[] EncodeData(byte[] data, int version, QRErrorCorrectionLevel eccLevel)
    {
        int capacity = ByteCapacity[version - 1, (int)eccLevel];
        if (data.Length > capacity)
        {
            throw new ArgumentException($"Data exceeds capacity for version {version}.", nameof(data));
        }

        int countIndicatorBits = version < 10 ? 8 : 16;
        int totalBits = 4 + countIndicatorBits + (data.Length * 8); // Mode(4) + Count(8/16) + Data
        int totalDataBits = EccBlocks[version - 1, (int)eccLevel].TotalData * 8;
        int padBits = totalDataBits - totalBits;

        var bits = new List<bool>();
        bits.AddRange(ToBits(4, 4));  // Byte mode indicator
        bits.AddRange(ToBits(data.Length, countIndicatorBits));
        foreach (byte b in data)
        {
            bits.AddRange(ToBits(b, 8));
        }

        // Terminator (up to 4 zeros)
        for (int i = 0; i < 4 && bits.Count < totalDataBits; i++)
        {
            bits.Add(false);
        }

        // Pad to byte boundary
        while (bits.Count % 8 != 0)
        {
            bits.Add(false);
        }

        // Padding bytes: 11101100 00010001 alternating
        byte[] padBytes = { 0xEC, 0x11 };
        int padIndex = 0;
        while (bits.Count < totalDataBits)
        {
            bits.AddRange(ToBits(padBytes[padIndex], 8));
            padIndex = 1 - padIndex;
        }

        return BitsToBytes(bits);
    }

    #endregion

    #region Reed-Solomon Error Correction

    private static byte[] AddErrorCorrection(byte[] dataCodewords, int version, QRErrorCorrectionLevel eccLevel)
    {
        var ecc = EccBlocks[version - 1, (int)eccLevel];
        var allBlocks = new List<byte[]>();
        int offset = 0;

        for (int i = 0; i < ecc.Block1Count; i++)
        {
            byte[] block = new byte[ecc.Block1Size + ecc.EcPerBlock];
            Array.Copy(dataCodewords, offset, block, 0, ecc.Block1Size);
            ReedSolomonEncode(block, ecc.Block1Size, ecc.EcPerBlock);
            allBlocks.Add(block);
            offset += ecc.Block1Size;
        }

        for (int i = 0; i < ecc.Block2Count; i++)
        {
            byte[] block = new byte[ecc.Block2Size + ecc.EcPerBlock];
            Array.Copy(dataCodewords, offset, block, 0, ecc.Block2Size);
            ReedSolomonEncode(block, ecc.Block2Size, ecc.EcPerBlock);
            allBlocks.Add(block);
            offset += ecc.Block2Size;
        }

        return Interleave(allBlocks, ecc);
    }

    private static void ReedSolomonEncode(byte[] data, int dataLen, int ecCount)
    {
        int[] toEncode = new int[dataLen + ecCount];
        for (int i = 0; i < dataLen; i++)
        {
            toEncode[i] = data[i] & 0xFF;
        }

        int[] generator = GetGeneratorPolynomial(ecCount);
        for (int i = 0; i < dataLen; i++)
        {
            int coef = toEncode[i];
            for (int j = 0; j < generator.Length; j++)
            {
                toEncode[i + j] ^= GfMultiply(generator[j], coef);
            }
        }

        for (int i = 0; i < ecCount; i++)
        {
            data[dataLen + i] = (byte)toEncode[dataLen + i];
        }
    }

    /// <summary>Build generator polynomial (x-α^0)(x-α^1)...(x-α^{degree-1}). Returns [0]=1, [1..degree]=remaining coefficients.</summary>
    private static int[] GetGeneratorPolynomial(int degree)
    {
        InitGaloisField();
        int[] poly = new int[degree + 1];
        poly[0] = 1;
        for (int i = 0; i < degree; i++)
        {
            int alphaI = GfExp[i];
            int[] next = new int[poly.Length + 1];
            next[0] = poly[0];
            for (int j = 1; j < poly.Length; j++)
            {
                next[j] = poly[j] ^ GfMultiply(alphaI, poly[j - 1]);
            }
            next[poly.Length] = GfMultiply(alphaI, poly[poly.Length - 1]);
            poly = next;
        }
        return poly;
    }

    private static byte[] Interleave(List<byte[]> blocks, (int TotalData, int EcPerBlock, int Block1Count, int Block1Size, int Block2Count, int Block2Size) ecc)
    {
        int maxData = Math.Max(ecc.Block1Size, ecc.Block2Size);
        var result = new List<byte>();

        for (int i = 0; i < maxData; i++)
        {
            foreach (var block in blocks)
            {
                int dataLen = block.Length - ecc.EcPerBlock;
                if (i < dataLen)
                {
                    result.Add(block[i]);
                }
            }
        }

        for (int i = 0; i < ecc.EcPerBlock; i++)
        {
            foreach (var block in blocks)
            {
                result.Add(block[block.Length - ecc.EcPerBlock + i]);
            }
        }

        return result.ToArray();
    }

    #endregion

    #region Galois Field (GF256)

    private static readonly int[] GfExp = new int[512];
    private static readonly int[] GfLog = new int[256];
    private static bool _gfInitialized;

    private static void InitGaloisField()
    {
        if (_gfInitialized) return;
        int x = 1;
        for (int i = 0; i < 255; i++)
        {
            GfExp[i] = x;
            GfLog[x] = i;
            x <<= 1;
            if (x >= 256) x ^= GF_PRIMITIVE;
        }
        for (int i = 255; i < 512; i++)
        {
            GfExp[i] = GfExp[i - 255];
        }
        _gfInitialized = true;
    }

    private static int GfMultiply(int a, int b)
    {
        if (a == 0 || b == 0) return 0;
        InitGaloisField();
        return GfExp[GfLog[a] + GfLog[b]];
    }

    #endregion

    #region Bit Helpers

    private static IEnumerable<bool> ToBits(int value, int bitCount)
    {
        for (int i = bitCount - 1; i >= 0; i--)
        {
            yield return ((value >> i) & 1) != 0;
        }
    }

    private static byte[] BitsToBytes(List<bool> bits)
    {
        byte[] result = new byte[bits.Count / 8];
        for (int i = 0; i < result.Length; i++)
        {
            int b = 0;
            for (int j = 0; j < 8; j++)
            {
                if (bits[i * 8 + j]) b |= 1 << (7 - j);
            }
            result[i] = (byte)b;
        }
        return result;
    }

    #endregion

    #region Matrix Construction

    private static bool[,] BuildMatrix(byte[] codewords, int version, QRErrorCorrectionLevel eccLevel)
    {
        int size = 17 + version * 4;
        var matrix = new bool[size, size];
        int matrixSize = size;

        PlaceFinderPatterns(matrix);
        PlaceTimingPatterns(matrix);
        PlaceAlignmentPatterns(matrix, version);

        int codewordIndex = 0;
        int bitIndex = 7;
        bool up = true;
        int col = matrixSize - 1;

        while (col > 0)
        {
            if (col == 6) col = 5;

            for (int row = up ? matrixSize - 1 : 0; up ? row >= 0 : row < matrixSize; row += up ? -1 : 1)
            {
                for (int c = 0; c < 2; c++)
                {
                    int actualCol = col - c;
                    if (actualCol < 0) break;
                    if (IsReserved(matrix, row, actualCol)) continue;

                    bool dark = false;
                    if (codewordIndex < codewords.Length)
                    {
                        dark = ((codewords[codewordIndex] >> bitIndex) & 1) != 0;
                        bitIndex--;
                        if (bitIndex < 0)
                        {
                            bitIndex = 7;
                            codewordIndex++;
                        }
                    }
                    matrix[row, actualCol] = dark;
                }
            }
            up = !up;
            col -= 2;
        }

        ApplyMask(matrix, version, eccLevel);
        PlaceFormatInfo(matrix, eccLevel, 0);

        return matrix;
    }

    private static void PlaceFinderPatterns(bool[,] matrix)
    {
        int[] positions = { 0, matrix.GetLength(0) - 7 };
        foreach (int row in positions)
        {
            foreach (int col in positions)
            {
                for (int r = 0; r < 7; r++)
                {
                    for (int c = 0; c < 7; c++)
                    {
                        bool fill = r == 0 || r == 6 || c == 0 || c == 6 || (r >= 2 && r <= 4 && c >= 2 && c <= 4);
                        matrix[row + r, col + c] = fill;
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    if (row > 0) matrix[row - 1, col + i] = false;
                    if (col > 0) matrix[row + i, col - 1] = false;
                }
            }
        }
        for (int r = 0; r < 7; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                bool fill = r == 0 || r == 6 || c == 0 || c == 6 || (r >= 2 && r <= 4 && c >= 2 && c <= 4);
                matrix[r, c] = fill;
            }
        }
    }

    private static void PlaceTimingPatterns(bool[,] matrix)
    {
        int size = matrix.GetLength(0);
        for (int i = 8; i < size - 8; i++)
        {
            matrix[6, i] = i % 2 == 0;
            matrix[i, 6] = i % 2 == 0;
        }
    }

    private static void PlaceAlignmentPatterns(bool[,] matrix, int version)
    {
        if (version < 2) return;
        int[] positions = GetAlignmentPositions(version);
        foreach (int row in positions)
        {
            foreach (int col in positions)
            {
                if ((row < 9 && col < 9) || (row < 9 && col > matrix.GetLength(0) - 10) || (row > matrix.GetLength(0) - 10 && col < 9))
                    continue;

                for (int r = -2; r <= 2; r++)
                {
                    for (int c = -2; c <= 2; c++)
                    {
                        bool fill = Math.Abs(r) == 2 || Math.Abs(c) == 2 || (r == 0 && c == 0);
                        matrix[row + r, col + c] = fill;
                    }
                }
            }
        }
    }

    private static int[] GetAlignmentPositions(int version)
    {
        return version switch
        {
            2 => new[] { 6, 18 },
            3 => new[] { 6, 22 },
            4 => new[] { 6, 26 },
            5 => new[] { 6, 30 },
            6 => new[] { 6, 34 },
            7 => new[] { 6, 22, 38 },
            8 => new[] { 6, 24, 42 },
            9 => new[] { 6, 26, 46 },
            10 => new[] { 6, 28, 50 },
            _ => Array.Empty<int>()
        };
    }

    private static bool IsReserved(bool[,] matrix, int row, int col)
    {
        int size = matrix.GetLength(0);
        if (row < 9 && col < 9) return true;
        if (row < 9 && col > size - 9) return true;
        if (row > size - 9 && col < 9) return true;
        if (row == 6 || col == 6) return true;
        return false;
    }

    private static void ApplyMask(bool[,] matrix, int version, QRErrorCorrectionLevel eccLevel)
    {
        int size = matrix.GetLength(0);
        int formatBits = FormatBits[(int)eccLevel, 0];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (matrix[row, col]) continue;
                bool mask = ((row + col) % 2) == 0;
                matrix[row, col] = mask;
            }
        }
    }

    private static void PlaceFormatInfo(bool[,] matrix, QRErrorCorrectionLevel eccLevel, int maskPattern)
    {
        int bits = FormatBits[(int)eccLevel, maskPattern];
        int size = matrix.GetLength(0);

        for (int i = 0; i < 15; i++)
        {
            bool dark = ((bits >> (14 - i)) & 1) != 0;
            if (i < 6)
                matrix[8, i] = dark;
            else if (i < 8)
                matrix[8, i + 1] = dark;
            else
                matrix[8, size - 15 + i] = dark;

            if (i < 8)
                matrix[size - 1 - i, 8] = dark;
            else if (i < 9)
                matrix[15 - i, 8] = dark;
            else
                matrix[14 - i, 8] = dark;
        }
        matrix[size - 8, 8] = true;
    }

    #endregion
}

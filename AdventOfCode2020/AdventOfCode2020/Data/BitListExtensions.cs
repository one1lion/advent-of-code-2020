using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Data {
  public static class BitListExtensions {
    /// <summary>
    /// Convert a list of boolean (bits) to long
    /// </summary>
    /// <param name="bitList">The boolean list (bit list in Big Endian)</param>
    /// <returns>The decimal value of the bit list</returns>
    public static long ToLong(this List<bool> bitList) {
      long sumBits = 0;
      var index = bitList.Count - 1;
      foreach (var bit in bitList) {
        if (bit) { sumBits += (long)Math.Pow(2, index); }
        index--;
      }
      return sumBits;
    }
  }
}

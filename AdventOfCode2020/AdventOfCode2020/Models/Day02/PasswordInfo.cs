using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Models.Day02 {
  public class PasswordInfo {
    public int MinCount { get; set; } // 3
    public int MaxCount { get; set; } // 5
    public char WhichChar { get; set; } // a
    public string Password { get; set; } // abcde

    public bool IsValid => 
      !new[] { 
        MinCount - 1, // 2
        MaxCount + 1  // 6
      }
        .Contains(
          Math.Clamp(
            Password.Count(x => x == WhichChar), 
            MinCount - 1, // 2
            MaxCount + 1)); // 6

    public bool IsValid2 => (Password[MinCount - 1] == WhichChar ? 1 : 0) + (Password[MaxCount - 1] == WhichChar ? 1 : 0) == 1;
  }
}

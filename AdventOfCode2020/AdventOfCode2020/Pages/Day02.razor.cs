using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace AdventOfCode2020.Pages {
  public partial class Day02 : ComponentBase {
  }

  public class PasswordInfo {
    public int MinCount { get; set; }
    public int MaxCount { get; set; }
    public char WhichChar { get; set; }
    public string Password { get; set; }

    public bool IsValid =>
      !new[] {
        MinCount - 1,
        MaxCount + 1
      }
        .Contains(
          Math.Clamp(
            Password.Count(x => x == WhichChar),
            MinCount - 1,
            MaxCount + 1));

    public bool IsValid2 => (Password[MinCount - 1] == WhichChar ? 1 : 0) + (Password[MaxCount - 1] == WhichChar ? 1 : 0) == 1;
  }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Pages {
  public partial class Day04 : ComponentBase {
  }

  public static class Day04Vars {
    public static Regex HairColorRegex = new Regex("[0-9a-f]");

    public static List<string> ValidEyeColors = new List<string>() {
      "amb"
      ,"blu"
      ,"brn"
      ,"gry"
      ,"grn"
      ,"hzl"
      ,"oth"
    };

    public static List<string> RequiredFields = new List<string>() {
      "byr"
      ,"iyr"
      ,"eyr"
      ,"hgt"
      ,"hcl"
      ,"ecl"
      ,"pid"
      /* ,"cid" */
    };
  }

  public class PassportProperty {
    public string Field { get; set; }
    public string Value { get; set; }

    public bool IsValidPart2 => Field == "cid" || (
      !string.IsNullOrWhiteSpace(Value) &&
      Field switch {
        "byr" => Value.Length == 4 && (int.TryParse(Value, out int by) && 1920 <= by && by <= 2002),
        "iyr" => Value.Length == 4 && (int.TryParse(Value, out int by) && 2010 <= by && by <= 2020),
        "eyr" => Value.Length == 4 && (int.TryParse(Value, out int by) && 2020 <= by && by <= 2030),
        "hgt" => int.TryParse(Value[0..^2], out int outVal) &&
          (
            (Value.EndsWith("cm") && 150 <= outVal && outVal <= 193)
            || (Value.EndsWith("in") && 59 <= outVal && outVal <= 76)
          ),
        "hcl" => Value.StartsWith("#") && Value.Length == 7 && Day04Vars.HairColorRegex.Matches(Value).Count == 6, // #FFFFFF
        "ecl" => Day04Vars.ValidEyeColors.Contains(Value),
        "pid" => int.TryParse(Value, out int outVal) && Value.Length == 9,
        _ => false
      });
  }

  public class PassportInfo {
    public string BirthYear { get; set; }
    public string IssueYear { get; set; }
    public string ExpireYear { get; set; }
    public string Height { get; set; }
    public string HairColor { get; set; }
    public string EyeColor { get; set; }
    public string PassportId { get; set; }
    public string CountryId { get; set; }

    public bool IsValidPart1 => !string.IsNullOrWhiteSpace(BirthYear)
      && !string.IsNullOrWhiteSpace(IssueYear)
      && !string.IsNullOrWhiteSpace(ExpireYear)
      && !string.IsNullOrWhiteSpace(Height)
      && !string.IsNullOrWhiteSpace(HairColor)
      && !string.IsNullOrWhiteSpace(EyeColor)
      && !string.IsNullOrWhiteSpace(PassportId);

    public bool IsValidPart2() {
      if (!IsValidPart1) {
        return false;
      }
      //byr (Birth Year) - four digits; at least 1920 and at most 2002.
      if (!(BirthYear.Length == 4 && (int.TryParse(BirthYear, out int by) && 1920 <= by && by <= 2002))) {
        return false;
      }
      //iyr (Issue Year) - four digits; at least 2010 and at most 2020.
      if (!(IssueYear.Length == 4 && (int.TryParse(IssueYear, out int iy) && 2010 <= iy && iy <= 2020))) {
        return false;
      }
      //eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
      if (!(ExpireYear.Length == 4 && (int.TryParse(ExpireYear, out int ey) && 2020 <= ey && ey <= 2030))) {
        return false;
      }
      //hgt (Height) - a number followed by either cm or in:
      //If cm, the number must be at least 150 and at most 193.
      //If in, the number must be at least 59 and at most 76.
      if (!(int.TryParse(Height[0..^2], out int h)
        && (
          (Height.EndsWith("cm") && 150 <= h && h <= 193)
          || (Height.EndsWith("in") && 59 <= h && h <= 76)
        ))) {
        return false;
      }
      //hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
      if (!(HairColor.StartsWith("#") && HairColor.Length == 7 && Day04Vars.HairColorRegex.Matches(HairColor).Count == 6)) {
        return false;
      }// #FFFFFF
       //ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
      if (!(Day04Vars.ValidEyeColors.Contains(EyeColor))) {
        return false;
      }
      //pid (Passport ID) - a nine-digit number, including leading zeroes.
      if (!(int.TryParse(PassportId, out int pi) && PassportId.Length == 9)) {
        return false;
      }

      return true;
    }
  }

  public class Passport {

    public PassportInfo PassportInfo { get; set; }
    public List<PassportProperty> PassportProperties { get; set; }

    public bool IsValidPart1 => PassportInfo.IsValidPart1;
    public bool IsValidPart1V2 => PassportProperties.Select(x => x.Field).Intersect(Day04Vars.RequiredFields).Count() == Day04Vars.RequiredFields.Count;

    public bool IsValidPart2 => PassportInfo.IsValidPart2();
    public bool IsValidPart2V2 => PassportProperties.All(x => x.IsValidPart2);

  }
}

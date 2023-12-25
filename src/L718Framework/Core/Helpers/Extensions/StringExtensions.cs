using System.Text.RegularExpressions;

namespace L718Framework.Core.Helpers.Extensions;

/// <summary>
/// Extensions to common string functions
/// </summary>
public static class StringExtensions
{


    /// <summary>
    /// Generate Slugs by strings
    /// Credits to https://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
    /// </summary>
    /// <param name="phrase"></param>
    /// <returns></returns>
    public static string GenerateSlug(this string phrase)
    {
        string str = phrase.RemoveAccents().ToLower();
        // invalid chars           
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        // convert multiple spaces into one space   
        str = Regex.Replace(str, @"\s+", " ").Trim();
        // cut and trim 
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        str = Regex.Replace(str, @"\s", "-"); // hyphens   
        return str;
    }

    /// <summary>
    /// Remove accent characters like áéíóúç... 
    /// converting them to aeiouc...
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static string RemoveAccents(this string txt)
    {
        byte[] bytes = System.Text.Encoding.Latin1.GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }

}
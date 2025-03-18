using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utils
{

    public static IEnumerable<string> GetCSVLine(string path)
    {
        foreach (var line in File.ReadLines(path))
        {
            yield return line;
        }
    }

    public static void DebugStringArray(this string[] array)
    {
        foreach (var s in array)
        {
            Debug.Log(s);
        }
    }
}

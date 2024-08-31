using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSV
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { ' ' };

    public static List<Dictionary<string, string>> Read(string name)
    {
        if (string.IsNullOrEmpty(name)) 
            throw new System.ArgumentNullException("name");

        StringBuilder sb = new();
        name = System.IO.Path.GetFileNameWithoutExtension(name);
        sb.Append("Data/");
        sb.Append(name);
        TextAsset data = Resources.Load<TextAsset>(sb.ToString());
        if (!data) 
            throw new System.Exception(string.Format("Not Find TextAsset : {0}", name));

        var list = new List<Dictionary<string, string>>();
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) 
            return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") 
                continue;

            var entry = new Dictionary<string, string>();
            var count = Mathf.Min(header.Length, values.Length);
            for (var j = 0; j < count; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS);
                value = value.Replace("<br>", "\n");
                value = value.Replace("<c>", ",");
                entry[header[j]] = value;
            }
            list.Add(entry);
        }
        return list;
    }
}

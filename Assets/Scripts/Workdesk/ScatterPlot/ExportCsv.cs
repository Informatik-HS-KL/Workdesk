using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

[Serializable]
public class ExportCsv
{
    public string test;
    public List<string> columns;
    public List<List<string>> data;

    public override string ToString()
    {

        string csvstring = "";
        StringBuilder builder = new StringBuilder(csvstring);
        int i = 0;
        foreach(string value in columns)
        {
            if(i == columns.Count)
            {
                builder.Append(value);
            }
            else
            {
                builder.Append(value);
                builder.Append(",");
            }
            i++;
        }
        builder.Append("\n");
        foreach (List<string> valueList in data)
        {
            int j = 0;
            foreach(string value in valueList)
            {
               // Debug.Log("Test");
              //  Debug.Log(value);
                if (j == columns.Count)
                {
                    builder.Append(value);
                }
                else
                {
                    builder.Append(value);
                    builder.Append(",");
                }
                j++;
            }
            builder.Append("\n");
        }
        csvstring = builder.ToString();
        return csvstring;
    }
}
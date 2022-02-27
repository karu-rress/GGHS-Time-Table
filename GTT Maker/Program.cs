﻿Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("==================GTT 5 Creator==================");

Dictionary<string, string> subjectDict = new()
{
    ["언매"] = "Korean",
    ["화작"] = "Korean",

    ["확통"] = "Math",
    ["미적분"] = "Math",

    ["동사"] = "Social",
    ["한지"] = "Social",
    ["사문"] = "Social",

    ["스문"] = "Language",
    ["중문"] = "Language",
    ["일문"] = "Language",

    ["사탐방"] = "Global1",
    ["한사이"] = "Global1",

    ["세문미"] = "Global2",
    ["윤연"] = "Global2",

    ["영문"] = "EnglishLiterature",
    ["체육"] = "Sports",
    ["독의"] = "Reading",
    ["심영"] = "AdvancedEnglish",
    ["창체"] = "Others",
};

const int cls = 1; // 8

for (int i = 0; i < cls; i++)
{
    Console.WriteLine($"##### Class {i + 1}");
    Console.Write("{\n\t");
    string[] lines = File.ReadAllLines($@"..\..\..\class{i + 1}.txt");
    int j = 0;
    foreach (string subject in lines)
    {
        char? post = null;
        string key = subject;
        if (subject[^1] is 'A' or 'B' or 'C' or 'D')
        {
            post = subject[^1];
            key = subject[..^1];
        }

        Console.Write($"Subjects.{subjectDict[key]}{(post is null ? "" : $".{post}()")}, ");

        if (++j % 7 is 0)
            Console.Write("\n\t");
    }

    Console.WriteLine("Subjects.Homecoming");
    Console.WriteLine("}");
}
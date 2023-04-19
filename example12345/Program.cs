using System;



var lsFirst = new List<Rgb>()
{
    new Rgb(1,2,3)
};
var lsSecond = new Dictionary<string, Rgb>()
{
    {"newTag",new Rgb(1,2,3) },
    {"newTag1",new Rgb(1,2,3) },
    {"newTag2",new Rgb(1,2,3) }
};
var result = new Dictionary<Rgb, List<string>>();
int rangeSize = 10;
foreach (var item in lsFirst)
{
    double? previewRange = null;
    string temp = null;
    foreach (var element in lsSecond)
    {
        var obj = GetRange(item, element.Value);
        if (previewRange == null || previewRange > obj)
        {
            previewRange = obj;
            temp = element.Key;
        }

    }

    if (result.TryGetValue(item, out var rs))
        rs.Add(temp);
    else
        result.Add(item, new List<string>() { temp });
}
Console.ReadKey();
double GetRange(Rgb rgbFirst, Rgb rgbSecond)
{
    return Math.Sqrt(
    Math.Pow(rgbFirst.R - rgbSecond.R, 2) +
    Math.Pow(rgbFirst.B - rgbSecond.B, 2) +
    Math.Pow(rgbFirst.G - rgbSecond.G, 2));
}
record struct Rgb(byte R,byte B,byte G);
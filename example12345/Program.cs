using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;
using System;



var lsFirst = new List<Rgb>()
{
    new Rgb(1,2,3)
};
var lsSecond = new Dictionary<string,Rgb>()
{
    {"newTag",new Rgb(1,2,3) }
};
var result = new Dictionary<Rgb, List<string>>();
int rangeSize = 10;
foreach (var item in lsFirst)
{
    foreach (var element in lsSecond)
    {
        var range = GetRange(item, element.Value);
        if(range > 10)
        {
            if (result.TryGetValue(item, out var rs))
                rs.Add(element.Key);
            else
                result.Add(item, new List<string>() { element.Key });
        }    

    }
}

double GetRange(Rgb rgbFirst,Rgb rgbSecond)
{
    return Math.Sqrt(
    Math.Pow(rgbFirst.R - rgbSecond.R, 2) +
    Math.Pow(rgbFirst.B - rgbSecond.B, 2) +
    Math.Pow(rgbFirst.G - rgbSecond.G, 2));
}

using System.IO;
using Newtonsoft.Json;

namespace Nasa.Domain.UnitTests;

public class JsonHelper
{
    public static T LoadJson<T>(string fileName)
    {
        return JsonConvert.DeserializeObject<T>(new StreamReader(fileName).ReadToEnd());
    }
}
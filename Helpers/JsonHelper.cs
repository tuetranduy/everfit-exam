using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EverfitExam.Helpers;

public class JsonHelper
{
    public static T LoadJson<T>(string testDataFileName)
    {
        using (StreamReader r = new StreamReader($"{FileHelper.GetProjectFolderPath()}/TestData/{testDataFileName}.json"))
        {
            string json = r.ReadToEnd();
            var result = JsonConvert.DeserializeObject<T>(json);
            
            return result;
        }
    }
}
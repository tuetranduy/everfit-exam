using System;
using NUnit.Framework;

namespace EverfitExam.Helpers;

public static class FileHelper
{
    public static string GetProjectFolderPath()
    {
        var dir = TestContext.CurrentContext.TestDirectory + "\\";
        var actualPath = dir.Substring(0, dir.LastIndexOf("bin"));
        var projectPath = new Uri(actualPath).LocalPath;
        
        return projectPath;
    }
}
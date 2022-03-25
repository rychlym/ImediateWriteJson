using ImediateWriteJson.Ut;
using System;
using System.IO;

namespace ImediateWriteJson;

public class AppSettingsExt
{
    private const string HomeDirPlaceHolder = "{HOME}";
    private const string MyDocsDirPlaceHolder = "{MYDOCS}";
    private const string FilePathPlaceHolder = "{FILEPATH}";
    private const string VariablePlaceHolder = "{VAR}";
    public static readonly string JsonExt = ".json";

    private static readonly string homeDir = Environment.GetEnvironmentVariable("HOMEDIR") ?? "~";
    private static readonly string mydocsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public static string GetDir(string dirTemplate)
    {
        if (dirTemplate.IndexOf(HomeDirPlaceHolder) != -1)
        {
            return dirTemplate.Replace(HomeDirPlaceHolder, homeDir);
        }
        if (dirTemplate.IndexOf(MyDocsDirPlaceHolder) != -1)
        {
            return dirTemplate.Replace(MyDocsDirPlaceHolder, mydocsDir);
        }

        return dirTemplate;
    }

    public static string GetResultCommand(string template, string dirTemplate, string varExpr)
    {
        var fileNamePart = StringUt.CapsRemove(varExpr, new char[] { '.', '[', ']', '(', ')' });
        fileNamePart = StringUt.RemoveDiacritics(fileNamePart);
        var filePath = Path.Combine(AppSettingsExt.GetDir(dirTemplate), fileNamePart + AppSettingsExt.JsonExt);

        var result = template;
        if (result.IndexOf(FilePathPlaceHolder) != -1)
        {
            result = result.Replace(FilePathPlaceHolder, "@\"" + filePath + "\"");
        }
        if (result.IndexOf(VariablePlaceHolder) != -1)
        {
            result = result.Replace(VariablePlaceHolder, varExpr);
        }
        return result;
    }
}

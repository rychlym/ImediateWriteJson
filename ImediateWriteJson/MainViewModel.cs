using ImediateWriteJson.VM;
using System.IO;
using Utf8Json;
using Utf8Json.Resolvers;

namespace ImediateWriteJson;

public class MainViewModel : ViewModelBase
{
    private readonly AppSettings appSettings;

    public MainViewModel()
    {
        appSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("AppSettings.json"), StandardResolver.CamelCase);
    }

    /// <summary>The <see cref="VarExpression" /> property's backing field.</summary>
    private string varExpression = "myVar";

    /// <summary>Gets and sets the var expression property. Changes to that property's value raise the PropertyChanged event.</summary>
    public string VarExpression
    {
        get { return varExpression; }
        set
        {
            if (SetProperty(ref varExpression, value))
            {
                CalcResults(value);
            }
        }
    }

    /// <summary>The <see cref="NewtonsoftResult" /> property's backing field.</summary>
    private string newtonsoftResult = "";

    /// <summary>Gets and sets the newtonsoftResult property. Changes to that property's value raise the PropertyChanged event.</summary>
    public string NewtonsoftResult
    {
        get { return newtonsoftResult; }
        set { SetProperty(ref newtonsoftResult, value); }
    }


    /// <summary>The <see cref="SysTextJsonResult" /> property's backing field.</summary>
    private string sysTextJsonResult = "";

    /// <summary>Gets and sets the sys text json result property. Changes to that property's value raise the PropertyChanged event.</summary>
    public string SysTextJsonResult
    {
        get { return sysTextJsonResult; }
        set { SetProperty(ref sysTextJsonResult, value); }
    }


    /// <summary>The <see cref="Utf8JsonResult" /> property's backing field.</summary>
    private string utf8JsonResult = "";

    /// <summary>Gets and sets the utf8 json result property. Changes to that property's value raise the PropertyChanged event.</summary>
    public string Utf8JsonResult
    {
        get { return utf8JsonResult; }
        set { SetProperty(ref utf8JsonResult, value); }
    }

    private void CalcResults(string varExpr)
    {
        NewtonsoftResult = AppSettingsExt.GetResultCommand(appSettings.NewtonsoftJsonTemplate, appSettings.DirTemplate, varExpr);
        SysTextJsonResult = AppSettingsExt.GetResultCommand(appSettings.SysTextJsonTemplate, appSettings.DirTemplate, varExpr);
        Utf8JsonResult = AppSettingsExt.GetResultCommand(appSettings.Utf8JsonTemplate, appSettings.DirTemplate, varExpr);
    }
}

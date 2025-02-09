using Reqnroll;

namespace EverfitExam.Helpers;

public static class DataContextExtensions
{
    public static void SetScenarioContext(this ScenarioContext ctx, string key, object value)
    {
        if (ctx.ContainsKey(key))
        {
            ctx.Set(value, key);
        }
        else
        {
            ctx.Add(key, value);
        }
    }

    public static T GetScenarioContext<T>(this ScenarioContext ctx, string key, T defaultValue)
    {
        return ctx.ContainsKey(key) ? ctx.Get<T>(key) : defaultValue;
    }
}
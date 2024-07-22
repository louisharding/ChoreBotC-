using System;
using dotenv.net;

public static class EnvLoader
{
    static EnvLoader()
    {
        DotEnv.Load();
    }

    public static string GetEnv(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }

}

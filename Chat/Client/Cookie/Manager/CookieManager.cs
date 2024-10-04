using System;
using System.IO;
using System.Collections.Generic;
using WebSocketSharp.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

public class CookieManager
{
    public static CookieManager Instance => _instance.Value;

    private static readonly Lazy<CookieManager> _instance = new Lazy<CookieManager>(() => new CookieManager());
    private string _cookiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + @"\Cookie\";

    private CookieManager() { }

    public void SaveCookies(IEnumerable<Cookie> cookies, CookieType type)
    {
        SaveCookies(cookies.ToList(), type);
    }

    public string GetFilePath(CookieType type)
    {
        return _cookiesFilePath + type + "_cookies.json";
    }

    public void SaveCookies(List<Cookie> cookies, CookieType type)
    {
        string filePath = GetFilePath(type);
        string json = JsonConvert.SerializeObject(cookies, Formatting.Indented);
        File.WriteAllText(filePath, json);
        //Console.WriteLine(json);
    }

    public List<Cookie> LoadCookies(CookieType type)
    {
        string filePath = GetFilePath(type);
        if (File.Exists(filePath) == false)
            return null;

        string json = File.ReadAllText(filePath, Encoding.UTF8);
        List<Cookie> cookies = JsonConvert.DeserializeObject<List<Cookie>>(json);
        //Console.WriteLine(json);
        return cookies;
    }
}

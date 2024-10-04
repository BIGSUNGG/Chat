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

    public void SaveCookies(List<Cookie> cookies, CookieType type)
    {
        string json = JsonConvert.SerializeObject(cookies, Formatting.Indented);
        File.WriteAllText(_cookiesFilePath + type + "_cookies.txt", json);
        //Console.WriteLine(json);
    }

    public List<Cookie> LoadCookies(CookieType type)
    {
        string json = File.ReadAllText(_cookiesFilePath + type + "_cookies.txt", Encoding.UTF8);
        List<Cookie> cookies = JsonConvert.DeserializeObject<List<Cookie>>(json);
        //Console.WriteLine(json);
        return cookies;
    }
}

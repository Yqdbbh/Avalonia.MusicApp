namespace NeteasyCloud.Sdk;

public class HttpUtils
{
    public static HttpClient httpClient = new HttpClient();

    static HttpUtils()
    {
        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36 Edg/136.0.0.0");
    }
    

    public static HttpClient GetHttpClient()
    {
        return httpClient;
    }
}
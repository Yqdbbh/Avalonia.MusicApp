using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using NeteasyCloud.Sdk.VO;

namespace NeteasyCloud.Sdk;

public class SearchMusic
{
    
    /// <summary>
    /// 搜索歌曲
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public async Task<List<Song>> Search([Required]string keyword,int type =1,int limit=30,int offset=0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return [];
            var api = $"{Apis.baseUrl}/search?keywords={keyword}&type={type}&limit={limit}&offset={offset}";
            var httpClient = HttpUtils.GetHttpClient();  
            var response = await httpClient.GetFromJsonAsync<NCMBaseResponse<NCMResponse>>(api);
            return response.result.songs;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<AlbumDetails> GetAlbumDetails([Required] long albumId)
    {
        try
        {
            var api = $"{Apis.baseUrl}/album?id={albumId}";
            var httpClient = HttpUtils.GetHttpClient();
            var rsp = await httpClient.GetFromJsonAsync<AblumResult>(api);
            return rsp.album;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
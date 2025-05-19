using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NeteasyCloud.Sdk;

namespace Avalonia.MusicApp.Models;

public class Album
{
    private static SearchMusic _serarchMusic = new SearchMusic();
    private string CachePath => $"./Cache/Album/{AlbumId}";
    public string Artist { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }
    public long AlbumId { get; set; }

    public Album(string artist,string title,string coverUrl,long albumId)
    {
        this.Artist = artist;
        this.Title = title;
        this.CoverUrl = coverUrl;
        this.AlbumId  = albumId;
    }

    public static async Task<IEnumerable<Album>> Search(string keyword)
    {
        var result = await _serarchMusic.Search(keyword,1,10);
        return result.Select(e =>
        {
            return new Album(e.artists[0].name, e.album.name, e.artists[0].img1v1Url, e.album.id);
        });
    }

    public async Task<Stream> LoadCoverBitmapAsync()
    {
        if (File.Exists(CachePath))
        {
            return File.OpenRead(CachePath+".bmp");
        }
        else
        {
            var details = await _serarchMusic.GetAlbumDetails(AlbumId);
            var client = HttpUtils.GetHttpClient();
            var data = await client.GetByteArrayAsync(details.picUrl);
            return new MemoryStream(data);
        }
    }

    public async Task SaveAsync()
    {
        if (!Directory.Exists("./Cache/Album"))
        {
            Directory.CreateDirectory("./Cache/Album");
        }

        using (var fs = File.OpenWrite((CachePath)))
        {
            await SaveToStreamAsync(this, fs);
        }
    }

    public Stream SaveCoverBitmapStream()
    {
        return File.OpenWrite(CachePath+".bmp");
    }

    private static async Task SaveToStreamAsync(Album data, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, data).ConfigureAwait(false);
    }

    public static async Task<Album> LoadFromStream(Stream stream)
    {
        return (await JsonSerializer.DeserializeAsync<Album>(stream).ConfigureAwait(false))!;
    }

    public static async Task<IEnumerable<Album>> LoadCachedAsync()
    {
        if (!Directory.Exists("./Cache/Album"))
        {
            Directory.CreateDirectory("./Cache/Album");
        }

        var result = new List<Album>();
        foreach (var file in Directory.EnumerateFiles("./Cache/Album"))
        {
            if(!string.IsNullOrWhiteSpace(new DirectoryInfo(file).Extension))
                continue;
            
            await using var fs = File.OpenRead(file);
            result.Add(await Album.LoadFromStream(fs).ConfigureAwait(false));
        }
        return result;
    }
}
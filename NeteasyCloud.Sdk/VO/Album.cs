namespace NeteasyCloud.Sdk.VO;

/// <summary>
/// 专辑
/// </summary>
public class Album
{
    public long id { get; set; }
    public string name { get; set; }
    public Artist artist { get; set; }
    public long publishTime { get; set; }
    public long size { get; set; }
    public long copyrightId { get; set; }
    public long status { get; set; }
    public long picId { get; set; }
    public long mark { get; set; }
}


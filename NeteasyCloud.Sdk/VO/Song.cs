namespace NeteasyCloud.Sdk.VO;

/// <summary>
/// 歌曲
/// </summary>
public class Song
{
    public long id { get; set; }
    public string name { get; set; }
    public Artist[] artists { get; set; }
    public Album album { get; set; }
    public int duration { get; set; }
    public long copyrightId { get; set; }
    public int status { get; set; }
    public object[] alias { get; set; }
    public int rtype { get; set; }
    public int ftype { get; set; }
    public int mvid { get; set; }
    public int fee { get; set; }
    public object rUrl { get; set; }
    public long mark { get; set; }
}
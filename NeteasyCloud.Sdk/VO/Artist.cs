namespace NeteasyCloud.Sdk.VO;

/// <summary>
/// 作家
/// </summary>
public class Artist
{
    public long id { get; set; }
    public string name { get; set; }
    public string picUrl { get; set; }
    public object[] alias { get; set; }
    public long albumSize { get; set; }
    public long picId { get; set; }
    public object fansGroup { get; set; }
    public string img1v1Url { get; set; }
    public long img1v1 { get; set; }
    public object trans { get; set; }
}
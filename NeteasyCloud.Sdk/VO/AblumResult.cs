namespace NeteasyCloud.Sdk.VO;

public class AblumResult
{
    public bool resourceState { get; set; }
    public int code { get; set; }
    public AlbumDetails album { get; set; }
    // todo 解析songs
    // public object songs { get; set; }
}
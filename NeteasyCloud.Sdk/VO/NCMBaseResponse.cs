namespace NeteasyCloud.Sdk.VO;

/// <summary>
/// 基础返回
/// </summary>
public class NCMBaseResponse<T>
{
    public T result { get; set; }
    public int code { get; set; }
}

public class NCMResponse
{
    public List<Song> songs { get; set; }
    public bool hasMore { get; set; }
    public int songCount { get; set; }
}
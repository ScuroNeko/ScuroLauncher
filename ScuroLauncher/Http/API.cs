using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.IO;

namespace ScuroLauncher.API;

class API
{
    private static HttpClient _client = new ();
    public static async Task<T> HoYoRequest<T>(string url)
    {
        var response = await _client.GetFromJsonAsync<HoYoVerseResponse<T>>(url);
        return response == null ? throw new Exception() : response.data;
    }
    public static async Task<T> Request<T>(string url) => await _client.GetFromJsonAsync<T>(url) ?? throw new Exception();

    public static async Task<long> GetFileSize(string? url)
    {
        ArgumentNullException.ThrowIfNull(url);
        using var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        return response.Content.Headers.ContentLength ?? 0L;
    }

    public static async IAsyncEnumerable<long> Download(string? url)
    {
        ArgumentNullException.ThrowIfNull(url);
        var filename = Path.GetFileName(url);
        using var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        var alreadyWrited = 0L;
        if (File.Exists(filename)) alreadyWrited = new FileInfo(filename).Length;

        await using Stream contentStream = new Lib.ReadSeekableStream(await response.Content.ReadAsStreamAsync(), 0), 
            fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 8192, true);

        var buffer = new byte[8192];
        var isMoreToRead = true;
        var readed = 0L;
        if (alreadyWrited < 0)
        {
            contentStream.Seek(alreadyWrited, SeekOrigin.Begin);
            fileStream.Seek(alreadyWrited, SeekOrigin.Begin);
            readed = alreadyWrited;
        }

        do
        {
            var read = await contentStream.ReadAsync(buffer);
            if (read == 0)
            {
                isMoreToRead = false;
            }
            else
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, read));
                readed += read;
                yield return readed;
            }
        }
        while (isMoreToRead);
    }
    
}
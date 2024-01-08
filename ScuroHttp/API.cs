using System.Buffers;
using System.Net.Http.Json;
using ScuroLib;

namespace ScuroHttp;

public static class Api
{
    private static HttpClient _client = new ();
    public static async Task<T> HoYoRequest<T>(string url)
    {
        var response = await _client.GetFromJsonAsync<HoYoVerseResponse<T>>(url);
        return response == null ? throw new Exception() : response.data;
    }
    public static async Task<T> Request<T>(string url) => await _client.GetFromJsonAsync<T>(url) ?? throw new Exception();

    public static async Task<long> GetFileSize(string url)
    {
        using var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        return response.Content.Headers.ContentLength ?? 0L;
    }

    public static async IAsyncEnumerable<long> Download(string url, string outDirectory)
    {
        if (!Directory.Exists(outDirectory))
            Directory.CreateDirectory(outDirectory);
        var filePath = Path.Combine(outDirectory, Path.GetFileName(url));
        using var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        const int bufferSize = 32768*4;
        await using var contentStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize);
        
        var isMoreToRead = true;
        var readed = 0L;
        do
        {
            var buffer = new byte[bufferSize];
            var read = contentStream.Read(buffer);
            // Console.WriteLine(read);
            if (read == 0) isMoreToRead = false;
            else
            { 
                fileStream.Write(buffer.AsSpan(0, read));
                readed += read;
                yield return readed;
            }
        }
        while (isMoreToRead);
    }
    
}
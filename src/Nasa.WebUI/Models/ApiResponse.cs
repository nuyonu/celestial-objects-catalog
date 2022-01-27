namespace Nasa.WebUI.Models;

public class ApiResponse<T>
{
    public bool Succeeded { get; set; }

    public T Result { get; set; }

    public IEnumerable<string> Errors { get; set; }
}
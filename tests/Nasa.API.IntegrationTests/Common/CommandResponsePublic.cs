using System.Collections.Generic;

namespace Nasa.API.IntegrationTests.Common;

public class CommandResponsePublic<T>
{
    public bool Succeeded { get; set; }

    public T Result { get; set; }

    public IEnumerable<string> Errors { get; set; }
}
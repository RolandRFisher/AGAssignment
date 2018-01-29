using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ITextFIle
    {
        IEnumerable<string> GetData(string connectionstring);
    }
}
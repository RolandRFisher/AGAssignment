using System.Collections.Generic;

namespace Repository.DAL
{
    public interface ITextFIle
    {
        IEnumerable<string> GetData(string connectionstring);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Repository.DAL
{
    public class TextFile : ITextFIle
    {
        public TextFile()
        {
        }

        public IEnumerable<string> GetData(string connectionstring)
        {
            ICollection<string> result = new Collection<string>();
            try
            {
                var userFIle = connectionstring;
                if (!File.Exists(userFIle))
                {
                    throw new ArgumentException($"{connectionstring} does not exists.");
                }

                using (var streamReader = File.OpenText(userFIle))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                //TODO: Use Logger
                Console.WriteLine($"Error Message: {e.Message}.");
                throw;
            };

            return result;
        }
    }
}
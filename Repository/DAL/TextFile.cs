using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Core.Interfaces;
using NLog;

namespace Repository.DAL
{
    public class TextFile : ITextFIle
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<string> GetData(string connectionstring)
        {
            ICollection<string> result = new Collection<string>();
            try
            {
                var userFIle = connectionstring;
                if (!File.Exists(userFIle))
                {
                    var message = $"{connectionstring} does not exists.";
                    Logger.Error(message);
                    throw new ArgumentException(message);
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
                Logger.Error(e);
                throw;
            };

            return result;
        }
    }
}
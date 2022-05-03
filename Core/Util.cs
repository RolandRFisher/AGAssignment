using System.Text;

namespace AGAssignment
{//Test3
    public static class Util
    {
        public static string Tb => "\t";
        public static string Cr => "\r";
        public static string Lf => "\n";
        public static string EoL => "\r\n";
        public static string Gt => "> ";

        public static string Add(this string item, string character, int numberOfCharacters)
        {
            var sb = new StringBuilder();
            sb.Append(item);
            for (var i = 0; i < numberOfCharacters; i++)
            {
                sb.Append(character);
            }

            return sb.ToString();
        }
    }
}
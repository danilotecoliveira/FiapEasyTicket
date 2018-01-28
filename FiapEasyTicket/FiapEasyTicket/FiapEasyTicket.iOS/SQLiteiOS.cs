using SQLite;
using System;
using System.IO;
using FiapEasyTicket.iOS;
using FiapEasyTicket.Data;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteiOS))]
namespace FiapEasyTicket.iOS
{
    public class SQLiteiOS : ISQLite
    {
        public SQLiteConnection PegarConexao()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return new SQLiteConnection(Path.Combine(libFolder, "EasyTicket.db3"));
        }
    }
}

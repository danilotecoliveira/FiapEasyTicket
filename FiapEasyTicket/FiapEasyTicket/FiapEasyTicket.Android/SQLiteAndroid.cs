using SQLite;
using System.IO;
using FiapEasyTicket.Data;
using FiapEasyTicket.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteAndroid))]
namespace FiapEasyTicket.Droid
{
    public class SQLiteAndroid : ISQLite
    {
        private const string nomeArquivoDB = "EasyTicket.db3";

        public SQLiteConnection PegarConexao()
        {
            var caminhoDB = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, nomeArquivoDB);
            return new SQLiteConnection(caminhoDB);
        }
    }
}
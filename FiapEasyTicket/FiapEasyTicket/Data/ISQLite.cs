using SQLite;

namespace FiapEasyTicket.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}

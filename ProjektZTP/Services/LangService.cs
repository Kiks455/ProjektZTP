using ProjektZTP.Data;

namespace ProjektZTP.Services
{
    public class LangService
    {
        private readonly DbConnection _connection;

        public LangService(DbConnection connection)
        {
            _connection = connection;
        }

        public void SetUserLang(string email, string lang)
        {
            _connection.SetUserLang(email, lang);
        }
    }
}
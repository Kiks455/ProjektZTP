using ProjektZTP.Data;
using ProjektZTP.Models;

namespace ProjektZTP.Services
{
    public class UserService
    {
        #region Properties

        private readonly DbConnection _connection;

        #endregion Properties

        #region Constructors

        public UserService(DbConnection connection)
        {
            _connection = connection;
        }

        #endregion Constructors

        #region Methods

        public ApplicationUser GetUserByEmail(string userName)
        {
            var user = _connection.GetUserByEmail(userName);

            return user;
        }

        public int GetUserScore(string id)
        {
            int score = _connection.GetUserScore(id);

            return score;
        }

        public int GetUserLevel(string id)
        {
            int level = _connection.GetUserLevel(id);

            return level;
        }

        public void SetUserScore(string id, int score)
        {
            _connection.SetUserScore(id, score);
        }

        public void SetUserLevel(string id, int level)
        {
            _connection.SetUserLevel(id, level);
        }

        #endregion Methods
    }
}
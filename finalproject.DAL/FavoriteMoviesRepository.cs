using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace finproject.DAL
{
    public class FavoriteMovieRepository
    {
        private readonly string _connStr =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=McDonalds;Integrated Security=True;";
        public void Add(FavoriteMovie movie)
        {
            string sql = "INSERT INTO FavoriteMovies (Title) VALUES (@Title)";
            using var db = new SqlConnection(_connStr);
            db.Execute(sql, movie);
        }

        public void AddMany(IEnumerable<FavoriteMovie> movies)
        {
            string sql = "INSERT INTO FavoriteMovies (Title) VALUES (@Title)";
            using var db = new SqlConnection(_connStr);
            db.Execute(sql, movies);
        }

        public IEnumerable<FavoriteMovie> GetAll()
        {
            using var db = new SqlConnection(_connStr);
            return db.Query<FavoriteMovie>("SELECT * FROM FavoriteMovies").ToList();
        }

        public void DeleteAll()
        {
            using var db = new SqlConnection(_connStr);
            db.Execute("DELETE FROM FavoriteMovies");
            db.Execute("DBCC CHECKIDENT ('FavoriteMovies', RESEED, 0)");
        }
    }
}

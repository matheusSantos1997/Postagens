using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Repository.Repositories.DbQueries
{
    public class ImagemDbScript
    {
        public static string SelectAllImagens()
        {
            const string sql = @"SELECT * FROM imagens as i LEFT JOIN posts as p ON p.Id = i.PostId";

            return sql;
        }

        public static string SelectImagemById()
        {
            const string sql = @"SELECT * FROM imagens as i LEFT JOIN posts as p ON p.Id = i.PostId WHERE i.Id = @Id";

            return sql;
        }
    }
}

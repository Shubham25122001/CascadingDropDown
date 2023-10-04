using CascadingDropDown.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CascadingDropDown.Repository
{
    public class Cascade : ICascade
    {
        private readonly IDbConnection db;

        public Cascade(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DBConn"));
        }
        public Data Add(Data data)
        {
            try
            {
                var sql = "SHUBHAM_Data_Create";
                var parameters = new
                {
                    Country = data.Country,
                    State = data.State,
                    City = data.City
                };
                var affectedRows = db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                if (affectedRows > 0)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Data> GetAll()
        {
            return db.Query<Data>("SHUBHAM_Data_GetAll", commandType: CommandType.StoredProcedure).ToList();
        }

        public Data GetById(int id)
        {
            var parameters = new { Id = id };

            var data = db.Query<Data>("SHUBHAM_Data_GetById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return data;
        }

        public void Remove(int id)
        {
            var parameters = new { Id = id };
            db.Execute("SHUBHAM_Data_Delete", parameters, commandType: CommandType.StoredProcedure);

        }

        public Data Update(Data data)
        {
            try
            {
                var sql = "SHUBHAM_Data_Update";
                var parameters = new
                {
                    Id = data.Id,
                    Country = data.Country,
                    State = data.State,
                    City = data.City
                };
                var affectedRows = db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                if (affectedRows > 0)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

using CascadingDropDown.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CascadingDropDown.Repository
{
    public class Cascade : ICascade
    {
        private readonly IDbConnection db;


        /// <summary>
        /// Constructor of the Cascase class
        /// </summary>
        /// <param name="configuration">Get the Configuration Settings</param>
        public Cascade(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DBConn"));
        }

        /// <summary>
        /// Add the record inside the database
        /// </summary>
        /// <param name="data">instance of the Data model</param>
        /// <returns>data</returns>
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


        /// <summary>
        /// Get All the records
        /// </summary>
        /// <returns>Data list</returns>
        public List<Data> GetAll()
        {
            return db.Query<Data>("SHUBHAM_Data_GetAll", commandType: CommandType.StoredProcedure).ToList();
        }

        /// <summary>
        /// Get the Record By Id 
        /// </summary>
        /// <param name="id">Primary key of the record</param>
        /// <returns>Return the data</returns>
        public Data GetById(int id)
        {
            var parameters = new { Id = id };

            var data = db.Query<Data>("SHUBHAM_Data_GetById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return data;
        }


        /// <summary>
        /// Remove the record 
        /// </summary>
        /// <param name="id">Primary key of the record</param>
        public void Remove(int id)
        {
            var parameters = new { Id = id };
            db.Execute("SHUBHAM_Data_Delete", parameters, commandType: CommandType.StoredProcedure);

        }

        /// <summary>
        /// Update the existing record
        /// </summary>
        /// <param name="data">Instance of the Data</param>
        /// <returns>data</returns>
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

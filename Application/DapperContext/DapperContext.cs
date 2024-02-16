using Dapper;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class DapperContext
    {
       
        private readonly IConfiguration _config;

        private readonly string _connectionString;

        public DapperContext(IConfiguration config)
        {
             _config= config;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        
        //it return only one table based on id.
       public async Task<Car> QuerySingleAsync(string sql, object param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QuerySingleAsync<Car>(sql, param);
        }



        //non query sql statement
        public async Task<int> ExecuteAsync(string sql, object param)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.ExecuteAsync(sql, param);
        }






        //here Iconfiguration , IDbConnection aree by default
        /*
         Types of Methods in Dapper: 
            Execute 
            Query 
            QueryFirst 
            QueryFirstOrDefault 
            QuerySingle 
            QuerySingleOrDefault 
            QueryMultiple 
         
         */














    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EFCoreTest
{
    public static class EFDataTableExtensions
    {
        public static DataTable GetDataTable(this DatabaseFacade database, string sql, params NpgsqlParameter[] parameters)
       {
           try
           {              

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, database.GetConnectionString() ))
                {
                    DataTable dt = new DataTable();
                    if(parameters!=null&&parameters.Length>0)
                          adapter.SelectCommand.Parameters.AddRange(parameters);
                    adapter.Fill(dt);
                    return dt;
                }
           }
           catch (Exception ex)
           {

               throw ex;
            }
       }

    }
}

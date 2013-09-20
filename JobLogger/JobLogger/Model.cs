using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace JobLogger
{
    internal class Model
    {
        private static readonly string INSERT_QUERY = "INSERT INTO Log Values(@levelString, @dateString, @message)";

        private SqlConnection connection;

        internal Model(String connString)
        {
            try
            {
                connection = new SqlConnection(connString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating the model!" + e.Message);
            }
        }

        internal bool Insert(LogItem logItem)
        {
            bool ret = false;
            if (connection != null && logItem != null)
            {
                using (SqlCommand command = new SqlCommand(INSERT_QUERY, connection))
                {
                    command.Parameters.Add("@levelString", SqlDbType.Text).Value = logItem.LogLevel.NAME;
                    command.Parameters.Add("@dateString", SqlDbType.Text).Value = logItem.DateString;
                    command.Parameters.Add("@message", SqlDbType.Text).Value = logItem.Message;
                    try
                    {
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            ret = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(String.Format("Error {0} while logging to db.", e.ToString()));
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine(String.Format("Error {0} while closing db connection.", e.ToString()));
                        }
                    }
                }
            }
            return ret;
        }
    }
}

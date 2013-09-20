using System;
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
            connection = new SqlConnection(connString);
        }

        internal void insert(LogItem logItem)
        {
            if (connection != null)
            {
                using (SqlCommand command = new SqlCommand(INSERT_QUERY, connection))
                {
                    command.Parameters.Add("@levelString", SqlDbType.Text).Value = logItem.LogLevel.NAME;
                    command.Parameters.Add("@dateString", SqlDbType.Text).Value = logItem.DateString;
                    command.Parameters.Add("@message", SqlDbType.Text).Value = logItem.Message;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
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
        }
    }
}

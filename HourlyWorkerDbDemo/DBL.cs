// DBL.cs
//         Title: DBL - Data Base Layer for Hourly Payroll
// Last Modified: November 26, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2019
// 
// This is a module with a set of classes allowing for interaction between
// Hourly Worker data objects and a database.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HourlyWorkerDbDemo
{
    class DBL
    {

        #region "Connection String"

        /// <summary>
        /// Return connection string
        /// </summary>
        /// <returns>Connection string</returns>
        private static string GetConnectionString()
        {
            return Properties.Settings.Default.connectionString;
        }

        #endregion

        #region "Constants for Totals"

        /// <summary>
        /// Document values used for returning totals (so as to not identify them as arbitrary strings)
        /// These can be used within the assembly by calling DBL.Totals.Employees, etc.
        /// </summary>
        internal class Totals
        {
            internal const string Employees = "Employees";
            internal const string Hours = "Hours";
            internal const string Overtime = "Overtime";
            internal const string Pay = "Pay";
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Function used to select one worker from database, uses workerId as the primary key
        /// </summary>
        /// <param name="id">an id representing workers stored in the database</param>
        /// <returns>a worker object</returns>
        internal static HourlyWorker GetOneRow(int workerId)
        {
            // Declare new worker object
            HourlyWorker returnWorker = new HourlyWorker();

            // Declare new SQL connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command
            SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM [tblEntries] WHERE [EntryId] = " + workerId, dbConnection);

            // Try to connect to the database, create a datareader. If successful, read from the database and fill created row
            // with information from matching record
            try
            {
                dbConnection.Open();
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    returnWorker = new HourlyWorker(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    returnWorker.Id = workerId;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Error in GetOneRow", ex);
            }
            finally
            {
                dbConnection.Close();
            }

            // Return the populated row
            return returnWorker;

        }

        /// <summary>
        /// Function that returns all workers in the database as a DataTable for display
        /// </summary>
        /// <returns>a DataTable containing all workers in the database</returns>
        internal static DataTable GetEmployeeList()
        {
            // Declare the connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command
            SqlCommand command = new SqlCommand("SELECT * FROM [tblEntries]", dbConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            // Declare a DataTable object that will hold the return value
            DataTable employeeTable = new DataTable();

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                adapter.Fill(employeeTable);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in GetEmployeeList", ex);
            }
            finally
            {
                dbConnection.Close();
            }

            // Return the populated DataTable
            return employeeTable;
        }

        /// <summary>
        /// Function to add a new worker to the worker database
        /// </summary>
        /// <param name="insertWorker">a worker object to be inserted</param>
        /// <returns>true if successful</returns>
        internal static bool InsertNewRecord(HourlyWorker insertWorker)
        {
            // Create return value
            bool returnValue = false;

            // Declare the connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command and assign it paramaters
            SqlCommand command = new SqlCommand("INSERT INTO tblEntries VALUES(@firstName, @lastName, @rate, @hours, @pay, @entryDate)", dbConnection);
            command.Parameters.AddWithValue("@firstName", insertWorker.FirstName);
            command.Parameters.AddWithValue("@lastName", insertWorker.LastName);
            command.Parameters.AddWithValue("@rate", insertWorker.Rate);
            command.Parameters.AddWithValue("@hours", insertWorker.Hours);
            command.Parameters.AddWithValue("@pay", insertWorker.Pay);
            command.Parameters.AddWithValue("@entryDate", insertWorker.EntryDate);

            // Try to insert the new record, return result
            try
            {
                dbConnection.Open();
                returnValue = (command.ExecuteNonQuery() == 1);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in InsertNewRecord", ex);
            }
            finally
            {
                dbConnection.Close();
            }

            // Return the true if this worked, false if it failed
            return returnValue;
        }

        /// <summary>
        /// Function to update an existing worker in the worker database; if they don't exist, it instead attempts to add this worker as a new worker
        /// </summary>
        /// <param name="updateWorker">a worker object to be updated</param>
        /// <returns>true if successful</returns>
        internal static bool UpdateExistingRow(HourlyWorker updateWorker)
        {
            // Create return value
            bool returnValue = false;

            // If the worker exists, create dbConnection
            if (updateWorker.Id > 0)
            {
                // Declare the connection
                SqlConnection dbConnection = new SqlConnection(GetConnectionString());

                // Create new SQL command and assign it paramaters
                SqlCommand command = new SqlCommand("UPDATE tblEntries Set FirstName = @firstName, LastName = @lastName, Rate = @rate, Hours = @hours, Pay = @pay WHERE EntryId = @entryId", dbConnection);
                command.Parameters.AddWithValue("@workerId", updateWorker.Id);
                command.Parameters.AddWithValue("@firstName", updateWorker.FirstName);
                command.Parameters.AddWithValue("@lastName", updateWorker.LastName);
                command.Parameters.AddWithValue("@rate", updateWorker.Rate);
                command.Parameters.AddWithValue("@hours", updateWorker.Hours);
                command.Parameters.AddWithValue("@pay", updateWorker.Pay);
                command.Parameters.AddWithValue("@entryDate", updateWorker.EntryDate);

                // Try to open a connection to the database and update the record. Return result.
                try
                {
                    dbConnection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        returnValue = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new DataException("Error in UpdateExistingRow", ex);
                }
                finally
                {
                    dbConnection.Close();
                }

            }

            // If the worker does not exist, attempt to insert it instead
            else
            {
                if (InsertNewRecord(updateWorker))
                {
                    returnValue = true;
                }
            }

            // Returns true if the query executed; always false if the row is invalid
            return returnValue;
        }

        /// <summary>
        /// This method returns totals depending on what input is provided. For example
        /// providing 'Pay' as an argument returns total pay
        /// </summary>
        /// <param name="input">"Employees" returns total employees, "Hours" returns total hours, "Overtime" returns total overtime, "Pay" returns total pay</param>
        /// <returns>string form of specified total</returns>
        internal static string GetTotal(string input)
        {
            // Declare the result
            string result = "0";

            // Declare the connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandType = CommandType.Text;

            // Check which total is required and set the statement accordingly
            if (input == Totals.Employees)
            {
                command.CommandText = "SELECT COUNT(EntryId) FROM tblEntries";
            }
            else if (input == Totals.Hours)
            {
                command.CommandText = "SELECT SUM(Hours) FROM tblEntries";
            }
            else if (input == Totals.Overtime)
            {
                command.CommandText = "SELECT (SUM(Hours - 40)) FROM tblEntries WHERE (Hours > 40)";
            }
            else if (input == Totals.Pay)
            {
                command.CommandText = "SELECT SUM(Pay) FROM tblEntries";
            }
            else
            {
                return "ERROR";
            }

            // Try to open a connection to the database and read the total. Return result.

            try
            {
                dbConnection.Open();

                result = command.ExecuteScalar().ToString();

                if (result == String.Empty)
                {
                    return "0";
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Error in GetTotal " + input,ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        #endregion

    }

}
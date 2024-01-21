// EmployeeList.aspx.cs
//         Title: IncInc Payroll (Hourly)
// Last Modified: November 26, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2017
// 
// This form is designed to display a list of worker objects from a database.
// The database is called via the Worker class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HourlyWorkerDbDemo
{
    public partial class EmployeeList : Page
    {
        /// <summary>
        /// When the Employee List is loaded, populate the GridView with data
        /// from the class (ultimately derived from the database).
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gvWorkerList.DataSource = Worker.AllWorkers.DefaultView;
                gvWorkerList.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "Database Access Error! \n\n" + ex.Source + "\n\n" + ex.Message + "\n\n" + ex.StackTrace;
            }
        }
    }
}
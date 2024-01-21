// Summary.aspx.cs
//         Title: IncInc Payroll (Hourly)
// Last Modified: November 26, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2017
// 
// This form is designed to display summary data related to worker objects
// retrieved from a database. The database is called via the Worker class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HourlyWorkerDbDemo
{
    public partial class Summary : Page
    {
        /// <summary>
        /// On page load, populate the summary values
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTotalWorkersOutput.Text = Worker.TotalWorkers.ToString();
            lblTotalHoursOutput.Text = Worker.TotalHours.ToString();
            lblTotalPayOutput.Text = Worker.TotalPay.ToString("c");
            lblAveragePayOutput.Text = Worker.AveragePay.ToString("c");
        }
    }
}
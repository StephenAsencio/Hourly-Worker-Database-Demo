// PayrollEntry.aspx.cs
//         Title: IncInc Payroll (Hourly)
// Last Modified: November 27, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2017
// 
// This ASP page is designed to receive information about hourly or salaried workers
// in order to determine their pay. Validators are used to help ensure entered
// names, pay rates / salaries and hours are recorded with additional validation
// messages stored in an error label. Worker type is selected with radio buttons.
// The Calculate button initiates the calculation and the Clear button clears all
// data.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HourlyWorkerDbDemo
{
    public partial class PayrollEntry : Page
    {
        /// <summary>
        /// On page load, set the default focus
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            txtWorkerFirstName.Focus();
        }

        /// <summary>
        /// When Calculate is clicked, instantiate a worker based on the user's
        /// selection and pass in entered values as arugments. When the worker
        /// is created and inserted into the database, use these values to
        /// populate output fields. Disables input fields as well.
        /// </summary>
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                HourlyWorker myWorker = new HourlyWorker();

                if (rdoHourlyWorker.Checked)
                {
                    myWorker = new HourlyWorker(txtWorkerFirstName.Text, txtWorkerLastName.Text, txtRate.Text, txtHours.Text);
                }
                else if (rdoSalariedWorker.Checked)
                {
                    myWorker = new SalariedWorker(txtWorkerFirstName.Text, txtWorkerLastName.Text, txtRate.Text, txtHours.Text);
                }

                lblWorkerConfirmation.Text = myWorker.ToString();
                lblWorkerPay.Text = myWorker.Pay.ToString("c");

                lblTotalPay.Text = Worker.TotalPay.ToString("c");
                lblTotalWorkers.Text = Worker.TotalWorkers.ToString();

                // Disable input fields
                txtWorkerFirstName.Enabled = false;
                txtWorkerLastName.Enabled = false;
                txtRate.Enabled = false;
                txtHours.Enabled = false;
                btnCalculate.Enabled = false;

                lblError.Text = String.Empty;
                btnClear.Focus();
            }
            // Check for ArgumentOutOfRangeException
            catch (ArgumentOutOfRangeException ex)
            {
                // If this exception is thrown by rate, set error message and user assistance
                if (ex.ParamName == "Rate")
                {
                    lblError.Text = "Rate entry out-of-range! " + ex.Message;
                    txtRate.Focus();
                }
                // If this exception is thrown by hours, set error message and user assistance
                if (ex.ParamName == "Hours")
                {
                    lblError.Text = "Hours entry out-of-range! " + ex.Message;
                    txtHours.Focus();
                }
                // If this exception is not thrown by rate or hours, show generic error message
                else
                {
                    lblError.Text = "Entry out-of-range! " + ex.Message;
                }
            }
            // Check for ArgumentException
            catch (ArgumentException ex)
            {
                // If this exception is thrown by hours, set error message and user assistance
                if (ex.ParamName == "Hours")
                {
                    lblError.Text = "Entry error in hours! " + ex.Message;
                    txtHours.Focus();
                }
                // If this exception is thrown by rate, set error message and user assistance
                if (ex.ParamName == "Rate")
                {
                    lblError.Text = "Entry error in rate! " + ex.Message;
                    txtRate.Focus();
                }

                // If this exception is thrown by first name, set error message and user assistance
                if (ex.ParamName == "FirstName")
                {
                    lblError.Text = "Entry error in first name! " + ex.Message;
                    txtWorkerFirstName.Focus();
                }
                // If this exception is thrown by last name, set error message and user assistance
                if (ex.ParamName == "LastName")
                {
                    lblError.Text = "Entry error in last name! " + ex.Message;
                    txtWorkerLastName.Focus();
                }
                // If this exception is thrown by something else, show generic error message
                else
                {
                    lblError.Text = "Entry error! " + ex.Message;
                }
            }
            // Respond to DataExceptions referencing the database and the inner
            // exception thrown during the database operation
            catch (DataException ex)
            {
                lblError.Text = "Database error! " + ex.Message + "\n\n" + ex.InnerException.Message + "\n\n" + ex.Source + "\n\n" + ex.Message + "\n\n" + ex.StackTrace;
            }
            // Respond to other (unanticipated) exceptions with a thorough debug message
            catch (Exception ex)
            {
                lblError.Text = "An unknown has occured! Please contact your IT service provider and include the following information: \n\n" + ex.Source + "\n\n" + ex.Message + "\n\n" + ex.StackTrace;
            }
        }

        /// <summary>
        /// Clears input fields and the last worker's pay, and re-enables controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all input fields
            txtWorkerFirstName.Text = String.Empty;
            txtWorkerLastName.Text = String.Empty;
            txtRate.Text = String.Empty;
            txtHours.Text = String.Empty;
            lblError.Text = String.Empty;

            // Clear the last worker's pay
            lblWorkerConfirmation.Text = String.Empty;
            lblWorkerPay.Text = String.Empty;

            // Re-enable input fields
            txtWorkerFirstName.Enabled = true;
            txtWorkerLastName.Enabled = true;
            txtRate.Enabled = true;
            txtHours.Enabled = true;
            btnCalculate.Enabled = true;

            // Set focus
            txtWorkerFirstName.Focus();
        }
    }
}
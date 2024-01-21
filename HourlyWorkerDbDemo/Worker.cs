// Worker.cs
//         Title: IncInc Payroll (Hourly/Salaried)
// Last Modified: November 27, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2017
// 
// This is an abstract class representing a generic worker object
// to be inherited to create (at least) hourly and salary-paid workers.
// Workers store their own name, pay rates, number of messages, and
// number of hours, and a class method will exist to allow for
// calculation of the worker's pay and for updating of shared summary
// values. Most properties will be received as strings.
// This is intended to be used as part of a payroll application.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HourlyWorkerDbDemo
{
    public abstract class Worker
    {
        #region "Variable declarations"

        // Instance variables
        protected int employeeId;
        protected string employeeFirstName;
        protected string employeeLastName;
        protected decimal employeeRate;
        protected decimal employeeHours;
        protected decimal employeePay = 0M;
        protected DateTime entryDate;

        // Protected constants
        protected const double MaximumHours = 168.0;
        protected const double MinimumHours = 0.0;

        #endregion
        #region "Constructors"

        /// <summary>
        /// Worker constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        protected internal Worker()
        {

        }

        #endregion

        #region "Class methods"

        /// <summary>
        /// The findPay() method is used to calculate the pay for
        /// workers in varying ways depending on the inheiriting class.
        /// </summary>
        protected abstract void findPay();

        /// <summary>
        /// Returns a string describing the worker
        /// </summary>
        /// <returns>A string describing the worker</returns>
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName + " - " + this.Pay.ToString("c");
        }

        #endregion

        #region "Property Procedures"

        /// <summary>
        /// Gets and sets a worker's first name
        /// </summary>
        /// <returns>an employee's first name</returns>
        protected internal string FirstName
        {
            get
            {
                return employeeFirstName;
            }
            set
            {

                // Check if the name entered by the user is blank

                if (!(value == string.Empty))
                {
                    // If it's not blank, set it
                    employeeFirstName = value;
                }
                else
                {
                    // If it is blank, declare and throw an exception
                    ArgumentException ex = new ArgumentException("First name is required", "name");
                    throw ex;
                }

            }
        }

        /// <summary>
        /// Gets and sets a worker's last name
        /// </summary>
        /// <returns>an employee's last name</returns>
        protected internal string LastName
        {
            get
            {
                return employeeLastName;
            }
            set
            {

                // Check if the name entered by the user is blank

                if (!(value == string.Empty))
                {
                    // If it's not blank, set it
                    employeeLastName = value;
                }
                else
                {
                    // If it is blank, declare and throw an exception
                    ArgumentException ex = new ArgumentException("Last name is required", "name");
                    throw ex;
                }

            }
        }

        /// <summary>
        /// Gets and sets the worker's hourly pay rate
        /// </summary>
        /// <returns>an employee's hourly pay rate</returns>
        internal string Rate
        {
            get
            {
                return employeeRate.ToString();
            }
            set
            {
                const decimal MinimumRate = 15M;

                // If the pay rate is not a decimal value, the worker is not valid
                if (!decimal.TryParse(value, out employeeRate))
                {
                    ArgumentException ex = new ArgumentException("Please enter the worker's pay rate as a numeric value.", "Rate");
                    throw ex;
                }
                // If the pay rate is out of range, the worker is not valid
                else if (employeeRate < MinimumRate)
                {
                    ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("Rate", "Please enter the worker's pay rate above " + MinimumRate.ToString("c") + ".");
                    throw ex;
                }

                // If the pay rate parses and is in range, it's already stored in employeeRate

            }
        }

        /// <summary>
        /// Gets and sets the hours worked by a worker
        /// </summary>
        /// <returns>an employee's hours worked</returns>
        protected internal string Hours
        {
            get
            {
                return employeeHours.ToString();
            }
            set
            {
                // Check if the messages entered by the user is are numeric and positive

                if (!decimal.TryParse(value, out employeeHours))
                {
                    ArgumentException ex = new ArgumentException("Hours must be numeric", "hours");
                    throw ex;
                }
                if (employeeHours < (decimal)MinimumHours || employeeHours > (decimal)MaximumHours)
                {
                    ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("hours", "Hours must be in range of " + MinimumHours + " - " + MaximumHours);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Gets or sets the worker's pay
        /// </summary>
        /// <returns>a worker's pay</returns>
        protected internal decimal Pay
        {
            get
            {
                return employeePay;
            }
            set
            {
                employeePay = value;
            }
        }

        /// <summary>
        /// Gets or sets the worker's id
        /// </summary>
        /// <returns>a worker's id</returns>
        protected internal int Id
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the worker's entry date
        /// </summary>
        /// <returns>a worker's entry date</returns>
        protected internal DateTime EntryDate
        {
            get
            {
                return entryDate;
            }
            set
            {
                entryDate = value;
            }
        }

        /// <summary>
        /// Gets the overall total pay among all workers
        /// </summary>
        /// <returns>the overall total pay among all workers</returns>
        protected internal static decimal TotalPay
        {
            get
            {
                return Convert.ToDecimal(DBL.GetTotal(DBL.Totals.Pay));
            }
        }

        /// <summary>
        /// Gets the overall number of workers
        /// </summary>
        /// <returns>the overall number of workers</returns>
        protected internal static int TotalWorkers
        {
            get
            {
                return Convert.ToInt32(DBL.GetTotal(DBL.Totals.Employees));
            }
        }

        /// <summary>
        /// Gets the overall number of hours worked
        /// </summary>
        /// <returns>the overall number of hours worked</returns>
        protected internal static double TotalHours
        {
            get
            {
                return Convert.ToDouble(DBL.GetTotal(DBL.Totals.Hours));
            }
        }

        /// <summary>
        /// Gets the overall number of overtime hours worked
        /// </summary>
        /// <returns>the overall number of overtime hours worked</returns>
        protected internal static double TotalOvertimeHours
        {
            get
            {
                return Convert.ToDouble(DBL.GetTotal(DBL.Totals.Overtime));
            }
        }

        /// <summary>
        /// Calculates and returns an average pay among all workers
        /// </summary>
        /// <returns>the average pay among all workers</returns>
        protected internal static decimal AveragePay
        {
            get
            {
                if (TotalWorkers == 0)
                {
                    return 0;
                }
                else
                {
                    return TotalPay / TotalWorkers;
                }
            }
        }

        /// <summary>
        /// Returns a list of all workers in the database as a DataTable
        /// </summary>
        /// <returns>a DataTable containing all worker objects</returns>
        internal static DataTable AllWorkers
        {
            get
            {
                return DBL.GetEmployeeList();
            }
        }

        #endregion


    }
}
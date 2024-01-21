// HourlyWorker.cs
//         Title: IncInc Payroll (Hourly)
// Last Modified: November 27, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2017
// 
// This is a class representing individual worker objects. Each stores
// their own name, hours and pay rate and the class methods allow for
// calculation of the worker's pay and for updating of shared summary
// values. Name, hours and rate are received as strings.  Instantiated
// workers are inserted into a database.
// Totals are also associated with this class, returned from the database.
// This is being used as part of a payroll application.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HourlyWorkerDbDemo
{
    public class HourlyWorker : Worker
    {
        #region "Variable declarations"

        // Everything's inherited

        #endregion

        #region "Constructors"

        /// <summary>
        /// HourlyWorker constructor: uses properties to create a new HourlyWorker
        /// </summary>
        protected internal HourlyWorker(string firstNameValue, string lastNameValue, string payRateValue, string hoursValue)
        {
            base.FirstName = firstNameValue;
            base.LastName = lastNameValue;
            base.Hours = hoursValue;
            base.Rate = payRateValue;

            findPay();

            base.EntryDate = DateTime.Now;

            DBL.InsertNewRecord(this);
        }

        /// <summary>
        /// HourlyWorker constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        protected internal HourlyWorker()
        {

        }

        #endregion

        #region "Class methods"

        /// <summary>
        /// The findPay() method is used to calculate the pay for
        /// workers in varying ways depending on the inheiriting class.
        /// </summary>
        protected override void findPay()
        {
            // Constants related to the overtime pay calculation
            const decimal overtimeThreshold = 40.0M;
            const decimal overtimeRate = 1.5M;

            // If the employee is in overtime
            if (employeeHours > overtimeThreshold)
            {
                // The first 40 hours are paid at regular rate
                // Hours over 40 are paid extra
                employeePay = (employeeRate * (decimal)overtimeThreshold) + (employeeRate * (decimal)(employeeHours - overtimeThreshold)) * overtimeRate;
            }
            else
            {
                employeePay = (employeeRate * (decimal)employeeHours);
            }
        }

        /// <summary>
        /// Returns a string describing the worker
        /// </summary>
        /// <returns>A string describing the worker</returns>
        public override string ToString()
        {
            return base.ToString() + " - Hourly Worker";
        }

        #endregion

        #region "Property Procedures"

        // Property procedures are inherited!

        #endregion

    }
}
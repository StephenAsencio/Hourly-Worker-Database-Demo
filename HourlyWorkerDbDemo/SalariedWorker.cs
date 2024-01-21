// SalariedWorker.cs
//         Title: IncInc Payroll (Salaried)
// Last Modified: November 27, 2019
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2017
// 
// This is a class representing individual worker objects. Each stores
// their own name, hours and pay rate and the class methods allow for
// calculation of the worker's pay and for updating of shared summary
// values. Name, hours and salary are received as strings.
// This is being used as part of a payroll application.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HourlyWorkerDbDemo
{
    public class SalariedWorker : HourlyWorker
    {
        #region "Variable declarations"

        // Instance variables
        protected decimal employeeSalary = 0M;

        // Protected constants
        protected const decimal MinimumSalary = 27300M;

        #endregion

        #region "Constructors"

        /// <summary>
        /// HourlyWorker constructor: uses properties to create a new HourlyWorker
        /// </summary>
        protected internal SalariedWorker(string firstNameValue, string lastNameValue, string salaryValue, string hoursValue)
        {
            base.FirstName = firstNameValue;
            base.LastName = lastNameValue;
            base.Hours = hoursValue;
            this.Salary = salaryValue;

            findPay();

            base.EntryDate = DateTime.Now;

            DBL.InsertNewRecord(this);
        }

        /// <summary>
        /// HourlyWorker constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        protected internal SalariedWorker()
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

            // Determine hours missed
            if (employeeHours <= overtimeThreshold)
            {
                employeePay = (employeeRate * employeeHours);
            }
            // If the employee is in overtime
            else
            {
                // The first 40 hours are paid at regular rate
                // Hours over 40 are paid extra
                employeePay = (employeeRate * (decimal)overtimeThreshold) + (employeeRate * (decimal)(employeeHours - overtimeThreshold)) * overtimeRate;
            }
        }

        /// <summary>
        /// Returns a string describing the worker
        /// </summary>
        /// <returns>A string describing the worker</returns>
        public override string ToString()
        {
            return base.ToString() + " (Salaried)";
        }
        #endregion

        #region "Property Procedures"

        // Most property procedures are inherited!

        /// <summary>
        /// Gets and sets the salary for a worker, as well as their calculated hourly pay rate
        /// </summary>
        /// <returns>an employee's salary</returns>
        private string Salary
        {
            get
            {
                return employeeSalary.ToString();
            }
            set
            {
                const int HoursInWorkDay = 8;
                const int WorkingDaysInYear = 253;

                // Check if the messages entered by the user is are numeric and in range

                if (!decimal.TryParse(value, out employeeSalary))
                {
                    ArgumentException ex = new ArgumentException("Salary must be numeric", "salary");
                    throw ex;
                }
                else if (employeeSalary < MinimumSalary)
                {
                    ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("salary", "Salary must be at least " + MinimumSalary.ToString("c"));
                    throw ex;
                }
                else
                {
                    // Determine hourly pay rate
                    employeeRate = employeeSalary / (decimal)(WorkingDaysInYear * HoursInWorkDay);
                }
            }
        }

        #endregion

    }
}
using System;
using System.Globalization;
using System.Linq;

namespace NamePronunciationTool.Models
{
    public class EmployeeName
    {
        public string Employee_legal_Nm { get; set; }
        public string Emp_usr_prf_Nm { get; set; }
        public string Emp_usr_nm_rec_path { get; set; }
    }
    public class EmployeeDetails
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string EmpAddress { get; set; }
        public string EmpCity { get; set; }
        public string Mobile { get; set; }
        public string Employee_legal_Nm { get; set; }
        public string Emp_usr_prf_Nm { get; set; }
        public string Emp_usr_nm_country { get; set; }
        public string Emp_usr_nm_rec_path { get; set; }
    }

    public static class CountryDetails
    {
       
        public static string GetCountryNameByCode(string countryCode)
        {
            switch (countryCode)
            {
                case "en_US":
                    return "American English Pronunciation";
              
                case "en_GB":
                    return "British English Pronunciation";
                case "en_IN":
                    return "Indian English Pronunciation";
                default:
                    return "NO VALUE GIVEN";
            }

        }
       
    }
}

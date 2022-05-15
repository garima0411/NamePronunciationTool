using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IO;
using Dapper;
using System.Collections.Generic;
using NamePronunciationTool.Models;

namespace NamePronunciationTool
{
    public class DataAccessLayer
    {
        public static string _connStr;
        public DataAccessLayer()
        {
            APIConfiguration.BuildConfig(new ConfigurationBuilder());
            var connectionstring = APIConfiguration.GetConfigString(QueryHelper.connectionString, QueryHelper.DBConnctnName);
            if (connectionstring != null)
            {
                _connStr = connectionstring;
            }
        }
       
        /// <summary>
        /// Get User login status
        /// </summary>
        /// <param name="ueserID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public EmployeeName GetLogInSuccess(string ueserID, string pwd)
        {
            EmployeeName result = new EmployeeName();
            var parameters = new DynamicParameters();
            parameters.Add("@userid", ueserID, DbType.String, ParameterDirection.Input);
            parameters.Add("@pwd", pwd, DbType.String, ParameterDirection.Input);

            using (var connection = new SqlConnection(_connStr))
            {
               
                connection.Open();
                result = connection.QueryFirstOrDefault<EmployeeName>(QueryHelper.getLoginSuccess, parameters, commandType: CommandType.Text, commandTimeout: 120);
               
                connection.Close();


            }
           
                return result;
        }
        /// <summary>
        /// Get User login status
        /// </summary>
        /// <param name="ueserID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public EmployeeDetails GetSearchedEmployee(string ueserID)
        {
            EmployeeDetails result = new EmployeeDetails();
            var parameters = new DynamicParameters();
            parameters.Add("@userid", ueserID, DbType.String, ParameterDirection.Input);

            using (var connection = new SqlConnection(_connStr))
            {

                connection.Open();
                result = connection.QueryFirstOrDefault<EmployeeDetails>(QueryHelper.getsearchedEmployee, parameters, commandType: CommandType.Text, commandTimeout: 120);

                connection.Close();


            }

            return result;
        }

        public EmployeeDetails UpdateandGetEmployeeNameDtks(string ueserID,  string userPrfName, string countryCode)
        {
            EmployeeDetails result = new EmployeeDetails();
            var parameters = new DynamicParameters();
            parameters.Add("@userid", ueserID, DbType.String, ParameterDirection.Input);
            parameters.Add("@prfname", userPrfName, DbType.String, ParameterDirection.Input);
            parameters.Add("@country", countryCode, DbType.String, ParameterDirection.Input);

            using (var connection = new SqlConnection(_connStr))
            {

                connection.Open();
                result = connection.QueryFirstOrDefault<EmployeeDetails>(QueryHelper.updatePrfName, parameters, commandType: CommandType.Text, commandTimeout: 120);

                connection.Close();


            }

            return result;
        }



    }
    public static class APIConfiguration
    {
        private static IConfiguration _config;
        public static void BuildConfig(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            
        }
        public static  string GetConfigString(string section, string key)
        {
            string retValue = string.Empty;
            try
            {
                if (_config != null && !string.IsNullOrWhiteSpace(key))
                {
                    var config = _config.GetSection(section);
                    if(config != null && !string.IsNullOrWhiteSpace(config[key]))
                    {
                        return config[key];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;

        }
    }

    public static class QueryHelper
    {
        public const string connectionString = "ConnectionStrings";
        public const string DBConnctnName = "DBConnectionString";
        public const string getLoginSuccess = @"select distinct Employee_legal_Nm,Emp_usr_prf_Nm,Emp_usr_nm_rec_path from Employee_Login_Dtls ln inner join
Employee_Name_Pronounce_HLP em on em.Employee_uid = ln.EMPLOYEE_ID where ln.EMPLOYEE_ID= @userid and ln.pwd = @pwd";
        public const string getsearchedEmployee = @"select distinct 
EmployeeName ,
Email ,
EmpAddress ,
EmpCity,
Mobile,
Employee_legal_Nm,
Emp_usr_nm_country,
Emp_usr_prf_Nm,
Emp_usr_nm_rec_path from Employee emp inner join
Employee_Name_Pronounce_HLP Hlpname on emp.EmployeeUID = Hlpname.Employee_uid where emp.EmployeeUID= @userid";

        public const string updatePrfName = @"BEGIN

 update Employee_Name_Pronounce_HLP set Emp_usr_nm_country= @country, Emp_usr_prf_Nm = @prfname where Employee_uid =@userid;

select distinct 
EmployeeName ,
Email ,
EmpAddress ,
EmpCity,
Mobile,
Employee_legal_Nm,
Emp_usr_prf_Nm,
Emp_usr_nm_country,
Emp_usr_nm_rec_path from Employee emp inner join
Employee_Name_Pronounce_HLP Hlpname on emp.EmployeeUID = Hlpname.Employee_uid where emp.EmployeeUID= @userid;


END";

    }
}

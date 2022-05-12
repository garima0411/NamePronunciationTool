using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NamePronunciationTool.Models;

namespace NamePronunciationTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Employee_Profile()
        {
            return View();
        }

        public IActionResult User_Profile()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult UserLogin(string UserName, string password)
        {
            DataAccessLayer dal = new DataAccessLayer();
            var result = dal.GetLogInSuccess(UserName, password);
            if (result != null)
            {
                HttpContext.Session.SetString("username", result.Employee_legal_Nm);
                HttpContext.Session.SetString("isLogin", "true");
                HttpContext.Session.SetString("userID", UserName);
                ViewBag.isLoggedin = true;

                var userDtls = dal.GetSearchedEmployee(UserName);
                if(userDtls != null)
                {
                    var fullname = userDtls.EmployeeName;
                    HttpContext.Session.SetString("fullname", fullname);
                    var legalname = string.IsNullOrWhiteSpace(userDtls.Employee_legal_Nm) ? fullname : userDtls.Employee_legal_Nm;

                    HttpContext.Session.SetString("legalname", legalname);
                    var prfname = string.IsNullOrWhiteSpace(userDtls.Emp_usr_prf_Nm) ? legalname : userDtls.Emp_usr_prf_Nm;
                    HttpContext.Session.SetString("prfname", prfname);
                    var mobile = userDtls.Mobile;
                    HttpContext.Session.SetString("mobile", mobile);
                   var email = userDtls.Email;
                    HttpContext.Session.SetString("email", email);
                    var address = userDtls.EmpAddress + " , " + userDtls.EmpCity;
                    HttpContext.Session.SetString("address", address);

                }
               
                
                
                return View("Index");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                ViewBag.isLoggedin = false;
                return View("Index");
            }
        }

       [HttpPost]
        public IActionResult SearchByUid(string userID)
        {
            DataAccessLayer dal = new DataAccessLayer();
            var userDtls = dal.GetSearchedEmployee(userID);
            if (userDtls != null)
            {
                HttpContext.Session.SetString("Employeeusername", userDtls.EmployeeName);
                HttpContext.Session.SetString("EmployeeuserID", userID);
                var fullname = userDtls.EmployeeName;
                HttpContext.Session.SetString("empfullname", fullname);
                var legalname = string.IsNullOrWhiteSpace(userDtls.Employee_legal_Nm) ? fullname : userDtls.Employee_legal_Nm;

                HttpContext.Session.SetString("emplegalname", legalname);
                var prfname = string.IsNullOrWhiteSpace(userDtls.Emp_usr_prf_Nm) ? legalname : userDtls.Emp_usr_prf_Nm;
                HttpContext.Session.SetString("empprfname", prfname);
                var mobile = userDtls.Mobile;
                HttpContext.Session.SetString("empmobile", mobile);
                var email = userDtls.Email;
                HttpContext.Session.SetString("empemail", email);
                var address = userDtls.EmpAddress + " , " + userDtls.EmpCity;
                HttpContext.Session.SetString("empaddress", address);

                return View("Employee_Profile");
            }
            else
            {
                if(HttpContext.Session.GetString("isLogin") == "true")
                {
                    return View("User_Profile");
                }
                else {
                    ViewBag.error = "Invalid User";
                    return View("Index");
                }
               
            }
        }

        public IActionResult Profile()
        {
            return View();
        }
        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("isLogin");
            HttpContext.Session.Remove("fullname");
            HttpContext.Session.Remove("legalname");
            HttpContext.Session.Remove("prfname");
            HttpContext.Session.Remove("mobile");
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("address");
            ViewBag.isLoggedin = false;
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
        [HttpGet]
        public IActionResult RecordYourName()
        {
            ViewBag.isUserprofile = true;
            DataAccessLayer dal = new DataAccessLayer();
            string recPath = HttpContext.Session.GetString("userID") + "_recording.wav";
            var result = dal.AddRecordingPath(HttpContext.Session.GetString("userID"), recPath);
            if(result != null)
            {
                HttpContext.Session.SetString("recPath", result.Emp_usr_nm_rec_path);
                APIHandler aPIHandler = new APIHandler();
                string userName = aPIHandler.RecordYourName(HttpContext.Session.GetString("userID")).Result;
                return View("User_Profile");
            }
            else
            {
                ViewBag.error = "Recording ";
                ViewBag.isRec = false;
                return View("User_Profile");
            }
            
           
        }
        [HttpGet]
        public IActionResult DeleteRecordedYourName()
        {
            ViewBag.isUserprofile = true;
            DataAccessLayer dal = new DataAccessLayer();
            string recPath = "";
            var result = dal.AddRecordingPath(HttpContext.Session.GetString("userID"), recPath);
            if (result != null)
            {
                if (!string.IsNullOrWhiteSpace(result.Emp_usr_nm_rec_path))
                    HttpContext.Session.Remove("recPath");

              
                return View("User_Profile");
            }
            else
            {
                ViewBag.error = "Recording ";
                ViewBag.isRec = false;
                return View("User_Profile");
            }


        }

        public IActionResult Admin()
        {
          
            return View("Admin");
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
            ViewBag.isEmployeeprofile = true;
            return View();
        }

        public IActionResult User_Profile()
        {
            ViewBag.isUserprofile = true;
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
                if (userDtls != null)
                {
                    var fullname = userDtls.EmployeeName;
                    HttpContext.Session.SetString("fullname", fullname);
                    var legalname = string.IsNullOrWhiteSpace(userDtls.Employee_legal_Nm) ? fullname : userDtls.Employee_legal_Nm;

                    HttpContext.Session.SetString("legalname", legalname);
                    var prfname = string.IsNullOrWhiteSpace(userDtls.Emp_usr_prf_Nm) ? legalname : userDtls.Emp_usr_prf_Nm;
                    HttpContext.Session.SetString("prfname", prfname);

                    if(!string.IsNullOrWhiteSpace(userDtls.Emp_usr_nm_rec_path))
                    HttpContext.Session.SetString("recPath", userDtls.Emp_usr_nm_rec_path);




                    if (string.IsNullOrWhiteSpace(userDtls.Emp_usr_nm_country))
                        HttpContext.Session.SetString("country", "Preferred Sign Language");
                    else
                        HttpContext.Session.SetString("country", CountryDetails.GetCountryNameByCode(userDtls.Emp_usr_nm_country));

                    var mobile = userDtls.Mobile;

                    HttpContext.Session.SetString("mobile", mobile);
                    var email = userDtls.Email;
                    HttpContext.Session.SetString("email", email);
                    var address = userDtls.EmpAddress + " , " + userDtls.EmpCity;
                    HttpContext.Session.SetString("address", address);

                    if (!string.IsNullOrWhiteSpace(userDtls.Emp_usr_prf_Nm))
                    {
                        HttpContext.Session.SetString("usrprfname", prfname);
                        if (string.IsNullOrWhiteSpace(userDtls.Emp_usr_nm_country))
                            HttpContext.Session.SetString("usrcountry", "Preferred Sign Language");
                        else
                            HttpContext.Session.SetString("usrcountry", CountryDetails.GetCountryNameByCode(userDtls.Emp_usr_nm_country));


                    }

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

       
        public IActionResult ClearPreferredName(string prfName = "", string country = "")
        {
            DataAccessLayer dal = new DataAccessLayer();
            var userDtls = dal.UpdateandGetEmployeeNameDtks(HttpContext.Session.GetString("userID"), prfName, country);
            if (userDtls != null)
            {
                HttpContext.Session.Remove("usrprfname");
                HttpContext.Session.Remove("usrcountry");
                ViewBag.saveStatus = "Successfully Saved";
                ViewBag.isUserprofile = true;
                ViewBag.isEmployeeprofile = true;
                return View("User_Profile");

            }
            else
            {
                ViewBag.error = "Invalid Account";
                ViewBag.isLoggedin = false;
                return View("Index");
            }
        }
        [HttpGet]
        public IActionResult SaveUserPreferredName(string prfName = "", string country = "")
        {
            DataAccessLayer dal = new DataAccessLayer();
            var userDtls = dal.UpdateandGetEmployeeNameDtks(HttpContext.Session.GetString("userID"), prfName, country);
            if (userDtls != null)
            {

                var prfname = string.IsNullOrWhiteSpace(userDtls.Emp_usr_prf_Nm) ? userDtls.Employee_legal_Nm : userDtls.Emp_usr_prf_Nm;
                HttpContext.Session.SetString("usrprfname", prfname);

                if (string.IsNullOrWhiteSpace(userDtls.Emp_usr_nm_country))
                    HttpContext.Session.SetString("usrcountry", "Preferred Sign Language");
                else
                    HttpContext.Session.SetString("usrcountry", CountryDetails.GetCountryNameByCode(userDtls.Emp_usr_nm_country));


                ViewBag.saveStatus = "Successfully Saved";
                ViewBag.isUserprofile = true;
                ViewBag.isEmployeeprofile = true;
                return View("User_Profile");

            }
            else
            {
                ViewBag.error = "Invalid Account";
                ViewBag.isLoggedin = false;
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult SavePreferredName(string prfName = "", string country = "")
        {
            DataAccessLayer dal = new DataAccessLayer();
            var userDtls = dal.UpdateandGetEmployeeNameDtks(HttpContext.Session.GetString("userID"),prfName, country);
            if (userDtls != null)
            {
                    
                    var prfname = string.IsNullOrWhiteSpace(userDtls.Emp_usr_prf_Nm) ? userDtls.Employee_legal_Nm : userDtls.Emp_usr_prf_Nm;
                    HttpContext.Session.SetString("usrprfname", prfname);
                    
                if (string.IsNullOrWhiteSpace(userDtls.Emp_usr_nm_country))
                    HttpContext.Session.SetString("usrcountry", "Preferred Sign Language");
                else
                    HttpContext.Session.SetString("usrcountry", CountryDetails.GetCountryNameByCode(userDtls.Emp_usr_nm_country));


                ViewBag.saveStatus = "Successfully Saved";
                    ViewBag.isUserprofile = true;
                    ViewBag.isEmployeeprofile = true;
                return PartialView("_ProfilePartial", userDtls);

            }
            else
            {
                ViewBag.error = "Invalid Account";
                ViewBag.isLoggedin = false;
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                if(string.IsNullOrEmpty(userDtls.Emp_usr_nm_country))
                {
                    HttpContext.Session.SetString("empcountry", "American English Pronunciation");
                }
                else if (string.IsNullOrWhiteSpace(userDtls.Emp_usr_nm_country))
                {
                    HttpContext.Session.SetString("empcountry", "Preferred Sign Language");
                }
                else { 

                    HttpContext.Session.SetString("empcountry", CountryDetails.GetCountryNameByCode(userDtls.Emp_usr_nm_country));
                    
                }

                var email = userDtls.Email;
                HttpContext.Session.SetString("empemail", email);
                var address = userDtls.EmpAddress + " , " + userDtls.EmpCity;
                HttpContext.Session.SetString("empaddress", address);
                ViewBag.isUserprofile = true;
                ViewBag.isEmployeeprofile = true;
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

            ViewBag.isUserprofile = true;
            return Redirect("/signLangRec/SignLang.html");
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

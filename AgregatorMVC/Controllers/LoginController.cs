using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgregatorMVC.Models;
using AgregatorMVC.Data.Repositories;
using Microsoft.AspNetCore.Http;

namespace AgregatorMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public LoginController(IAccountRepository accountRepositorys)
        {
            _accountRepository = accountRepositorys;
            _accountRepository.GetAll();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(UserModel UserModel)
        {
            var result = _accountRepository.Get(UserModel.Login, UserModel.Password);

            if (result != null)
            {
                HttpContext.Session.SetInt32("UserID", (Int32)result.ID);

                return RedirectToAction("Index", "Link");
            }
            else
            {
                TempData["msg"] = "<script>alert('Nie poprawny login lub hasło');</script>";
                return RedirectToAction(nameof(Index));
            }
            
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(RegistrationModel registrationModel)
        {
            var result = _accountRepository.GetByLogin(registrationModel.Login);

            if (result == null && ModelState.IsValid)
            {
                UserModel account = new() { Login = registrationModel.Login, Password = registrationModel.Password };
                _accountRepository.Add(account);

                TempData["msg"] = "<script>alert('Konto zostało utworzone');</script>";
                return RedirectToAction(nameof(Index));
            }
            else if (result != null)
            {
                ViewBag.RegistrationError = "Istnieje konto z podanym adresem mail";
                return View("Register");
            }
            else
            {
                return View("Register");
            }
        
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}

using AgregatorMVC.Data;
using AgregatorMVC.Data.Repositories;
using AgregatorMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly ILinkRepository _linkRepository;
        public MyAccountController(ILinkRepository LinkRepository)
        {
            _linkRepository = LinkRepository;
        }

        // GET: MyAccountController
        public async Task<ActionResult> Index(LinkModel linkModel, int? pageNumber)
        {
            List<LinkModel> LinksList = new List<LinkModel>();
            int UserID = (int)HttpContext.Session.GetInt32("UserID");

            var result = _linkRepository.GetUserLinks(UserID).OrderByDescending(x => x.Points);            
            MyAccountModel DateToView = new() { Links = await PaginatedList<LinkModel>.CreateAsync(result, pageNumber ?? 1, 100), Link = linkModel };

            return View(DateToView);
        }


        // POST: MyAccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyAccountModel myAccountModel)
        {
            LinkModel link = new LinkModel() { Title = myAccountModel.Link.Title, Link = myAccountModel.Link.Link };
            link.UserID = (long)HttpContext.Session.GetInt32("UserID");
            link.DateTime = DateTime.Now;

            var cos = ModelState.GetValidationState("Link.Link");

            foreach(var item in ModelState)
            {
                var cos2 = item.Value.Errors.Select(x => x.ErrorMessage);
            }


            if (ModelState.IsValid)
            {
                _linkRepository.Add(link);

                TempData["msg"] = "<script>alert('Link dodany');</script>";
                return RedirectToAction("Index");
            }
            else 
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);

                if (errors.Any(x => x == "Brak odpowiedzi z podanej strony"))
                    TempData["LinkError"] = "Brak odpowiedzi z podanej strony";
                else if (ModelState.GetValidationState("Link.Link") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    TempData["LinkError"] = "To pole jest wymagane";

                if (ModelState.GetValidationState("Link.Title") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    TempData["TitleError"] = "To pole jest wymagane";
            }

            return RedirectToAction("Index", link);
        }
        
        // GET: MyAccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_linkRepository.Get(id));
        }

        // POST: MyAccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LinkModel linkModel)
        {
            if (ModelState.IsValid)
            {
                _linkRepository.Update(id, linkModel);
                TempData["msg"] = "<script>alert('Zmiany zapisane');</script>";
            }
            else
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MyAccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_linkRepository.Get(id));
        }

        // POST: MyAccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _linkRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }        
    }
}

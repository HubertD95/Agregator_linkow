using AgregatorMVC.Data;
using AgregatorMVC.Data.Repositories;
using AgregatorMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Controllers
{
    public class LinkController : Controller
    {        
        private readonly ILinkRepository _linkRepository;

        public LinkController(ILinkRepository LinkRepository)
        {
            _linkRepository = LinkRepository;
        }

        // GET: LinkController
        public async Task<ActionResult> Index(int? pageNumber)
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            }

            var result = _linkRepository.GetAllLinks().OrderByDescending(x => x.Points);
            
            return View(await PaginatedList<LinkModel>.CreateAsync(result, pageNumber ?? 1, 100));
        }
        
        public ActionResult UpVote(int id)
        {
            LinkModel link = _linkRepository.Get(id);
                        
            ++link.Points;
            if (link.DownVote == true)
            {
                link.DownVote = false;
            }
            else
            {
                link.UpVote = true;
                link.DownVote = false;
            }           

            _linkRepository.Update(link.ID, link);
            _linkRepository.AddVotes(link.ID, (long)HttpContext.Session.GetInt32("UserID"), true, null);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownVote(int id)
        {
            LinkModel link = _linkRepository.Get(id);

            --link.Points;
            if (link.UpVote == true)
            {
                link.UpVote= false;
            }
            else
            {
                link.UpVote = false;
                link.DownVote = true;
            }

            _linkRepository.Update(link.ID, link);
            _linkRepository.AddVotes(link.ID, (long)HttpContext.Session.GetInt32("UserID"), null, true);

            return RedirectToAction(nameof(Index));
        }

        // GET: LinkController/RedirectToURL/5
        public ActionResult RedirectToURL(string linkk)
        {
            if (!linkk.Contains("https://"))
            {
                return Redirect("https://" + linkk);
            }
            else
            {
                return Redirect(linkk);
            }
        }               
    }
}

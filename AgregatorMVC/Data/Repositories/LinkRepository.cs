using AgregatorMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Data.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly AgregatorMVCContext _context;
        public LinkRepository(AgregatorMVCContext context)
        {
            _context = context;
        }

        public LinkModel Get(long? id)
            => _context.Links.SingleOrDefault(x => x.ID == id);

        public IQueryable<LinkModel> GetAllLinks()
        {
            if (_context.Links != null)
            {
                foreach (var item in _context.Links)
                {
                    item.UpVote = _context.UsersVotes.FirstOrDefault(x => x.LinkID == item.ID && item.UpVote == true) != null;
                    item.DownVote = _context.UsersVotes.FirstOrDefault(x => x.LinkID == item.ID && item.DownVote == true) != null;
                }
            }

            return _context.Links;
        }

        public IQueryable<LinkModel> GetUserLinks(int id)
            => _context.Links.Where(x => x.UserID == id);

        public void Add(LinkModel linkModel)
        {
            _context.Links.Add(linkModel);
            _context.SaveChanges();
        }

        public void Delete(long? id)
        {
            var result = _context.Links.SingleOrDefault(x => x.ID == id);

            if (result != null)
            {
                _context.Links.Remove(result);
            }

            _context.SaveChanges();
        }

        public void Update(long? id, LinkModel linkModel)
        {
            var result = _context.Links.SingleOrDefault(x => x.ID == id);
            if (result != null)
            {
                result.Link = linkModel.Link;
                result.Title = linkModel.Title;

                _context.SaveChanges();
            }
        }
        public IQueryable<UsersVotesModel> GetAllVotes()
            => _context.UsersVotes;

        public IQueryable<UsersVotesModel> GetUserVotes(long id)
            => _context.UsersVotes.Where(x => x.UserID == id);

        public void AddVotes(long linkID, long userID, bool? upVote, bool? downVote)
        {
            _context.UsersVotes.Add(new UsersVotesModel() { UserID = userID, LinkID = linkID, UpVote = upVote, DownVote = downVote });

            _context.SaveChanges();
        }

        public void DeleteVotes(long? linkID, long? userID)
        {
            if (linkID != null)
            {
                var result = _context.UsersVotes.SingleOrDefault(x => x.LinkID == linkID);

                if (result != null)
                {
                    _context.UsersVotes.Remove(result);
                }

                _context.SaveChanges();
            }
            else if (userID != null)
            {
                _context.UsersVotes.RemoveRange(_context.UsersVotes.Where(x => x.UserID == userID));

                _context.SaveChanges();
            }
        }

    }
}

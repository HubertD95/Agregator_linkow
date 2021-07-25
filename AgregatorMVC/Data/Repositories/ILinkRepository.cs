using AgregatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Data.Repositories
{
    public interface ILinkRepository
    {
        LinkModel Get(long? id);
        IQueryable<LinkModel> GetAllLinks();
        IQueryable<LinkModel> GetUserLinks(int id);
        void Add(LinkModel linkModel);
        void Update(long? id, LinkModel linkModel);
        void Delete(long? id);


        IQueryable<UsersVotesModel> GetAllVotes();
        IQueryable<UsersVotesModel> GetUserVotes(long id);
        void AddVotes(long linkID, long userID, bool? upVote, bool? downVote);
        void DeleteVotes(long? LinkID, long? UserID);
    }
}

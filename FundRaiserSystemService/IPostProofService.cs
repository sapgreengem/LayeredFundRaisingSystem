using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IPostProofService
    {
        IEnumerable<PostProof> GetAll(bool includeDonationOnPosts = false);
        PostProof Get(int id, bool includeDonationOnPosts = false);
        int Insert(PostProof postProof);
        int Update(PostProof postProof);
        int Delete(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class PostProofService : IPostProofService
    {
        private IPostProofDataAccess data;
        public PostProofService(IPostProofDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<PostProof> GetAll(bool includeDonationOnPosts = false)
        {
            return this.data.GetAll(includeDonationOnPosts);
        }
        public PostProof Get(int id, bool includeDonationOnPosts = false)
        {
            return this.data.Get(id, includeDonationOnPosts);
        }
        public int Insert(PostProof postProof)
        {
            return this.data.Insert(postProof);
        }
        public int Update(PostProof postProof)
        {
            return this.data.Update(postProof);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}

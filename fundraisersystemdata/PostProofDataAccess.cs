using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class PostProofDataAccess : IPostProofDataAccess
    {
        private FundRaiserDBContext context;
        public PostProofDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<PostProof> GetAll(bool includeDonationOnPosts = false)
        {
            if (includeDonationOnPosts)
            {
                return this.context.PostProofs.Include("DonationOnPost").ToList();
            }
            else
            {
                return this.context.PostProofs.ToList();
            }
        }
        public PostProof Get(int id, bool includeDonationOnPosts = false)
        {
            if (includeDonationOnPosts)
            {
                return this.context.PostProofs.Include("DonationOnPost").SingleOrDefault(x => x.ProofId == id);
            }
            else
            {
                return this.context.PostProofs.SingleOrDefault(x => x.ProofId == id);
            }
        }
        public int Insert(PostProof postProof)
        {
            this.context.PostProofs.Add(postProof);
            return this.context.SaveChanges();

        }
        public int Update(PostProof postProof)
        {
            PostProof proof = this.context.PostProofs.SingleOrDefault(x => x.ProofId == postProof.ProofId);
            proof.PictureForProof = postProof.PictureForProof;
            proof.PostId = postProof.PostId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            PostProof proof = this.context.PostProofs.SingleOrDefault(x => x.ProofId == id);
            this.context.PostProofs.Remove(proof);

            return this.context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class TransectionMethodNameDataAccess : ITransectionMethodNameDataAccess
    {
        private FundRaiserDBContext context;
        public TransectionMethodNameDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<TransectionMethodName> GetAll()
        {
            return this.context.TransectionMethodNames.ToList();
        }
        public TransectionMethodName Get(int id)
        {
            return this.context.TransectionMethodNames.SingleOrDefault(x => x.MethodId == id);
        }
        public int Insert(TransectionMethodName transectionMethodName)
        {
            this.context.TransectionMethodNames.Add(transectionMethodName);
            return this.context.SaveChanges();
        }
        public int Update(TransectionMethodName transectionMethodName)
        {
            TransectionMethodName methodName = this.context.TransectionMethodNames.SingleOrDefault(x => x.MethodId == transectionMethodName.MethodId);
            methodName.MethodName = transectionMethodName.MethodName;
            methodName.BankName = transectionMethodName.BankName;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            TransectionMethodName methodName = this.context.TransectionMethodNames.SingleOrDefault(x => x.MethodId == id);
            this.context.TransectionMethodNames.Remove(methodName);
            return this.context.SaveChanges();
        }

    }
}

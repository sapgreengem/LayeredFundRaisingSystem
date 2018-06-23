using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class TransectionMethodNameService: ITransectionMethodNameService
    {
        private ITransectionMethodNameDataAccess data;
        public TransectionMethodNameService(ITransectionMethodNameDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<TransectionMethodName> GetAll()
        {
            return this.data.GetAll();
        }
        public TransectionMethodName Get(int id)
        {
            return this.data.Get(id);
        }
        public int Insert(TransectionMethodName transectionMethodName)
        {
            return this.data.Insert(transectionMethodName);
        }
        public int Update(TransectionMethodName transectionMethodName)
        {
            return this.data.Update(transectionMethodName);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}

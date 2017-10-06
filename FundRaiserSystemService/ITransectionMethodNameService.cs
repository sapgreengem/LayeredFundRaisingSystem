using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface ITransectionMethodNameService
    {
        IEnumerable<TransectionMethodName> GetAll();
        TransectionMethodName Get(int id);
        int Insert(TransectionMethodName transectionMethodName);
        int Update(TransectionMethodName transectionMethodName);
        int Delete(int id);
    }
}

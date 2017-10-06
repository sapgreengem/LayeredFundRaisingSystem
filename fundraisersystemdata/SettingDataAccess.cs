using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class SettingDataAccess : ISettingDataAccess
    {
        private FundRaiserDBContext context;
        public SettingDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Setting> GetAll()
        {
            return this.context.Settings.ToList();
        }
        public Setting Get(int id)
        {
            return this.context.Settings.SingleOrDefault(x => x.SettingId == id);
        }
        public int Insert(Setting setting)
        {
            this.context.Settings.Add(setting);
            return this.context.SaveChanges();

        }
        public int Update(Setting setting)
        {
            Setting newSettings = this.context.Settings.SingleOrDefault(x => x.SettingId == setting.SettingId);
            newSettings.ServiceCharge = setting.ServiceCharge;
            newSettings.RefundCharge = setting.RefundCharge;
            newSettings.SystemContactNo = setting.SystemContactNo;
            newSettings.SystemAddress = setting.SystemAddress;
            newSettings.TotalIncome = setting.TotalIncome;
            newSettings.SystemBankAccount = setting.SystemBankAccount;
            newSettings.CollectedAmount = setting.CollectedAmount;

            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            Setting setting = this.context.Settings.SingleOrDefault(x => x.SettingId == id);
            this.context.Settings.Remove(setting);

            return this.context.SaveChanges();
        }
    }
}

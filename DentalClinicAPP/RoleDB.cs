using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class RoleDB
    {
        private InvoiceEF _InvoiceEF;

        public Role GetRecord(int? Roleid)
        {
            Role RoleObj = new Role();
            using (_InvoiceEF = new InvoiceEF())
            {
                RoleObj = _InvoiceEF.Roles
                    .Where(x => x.Id == Roleid).FirstOrDefault();

            }


            return RoleObj;
        }

        public int SaveRecord(Role record)
        {

            using (_InvoiceEF = new InvoiceEF())
            {
                _InvoiceEF.Roles.Add(record);
                _InvoiceEF.SaveChanges();
            }

            return record.Id;
        }

        public bool UpdateRecord(Role record)
        {

            Role RoleObj = new Role();
            using (_InvoiceEF = new InvoiceEF())
            {
                RoleObj = _InvoiceEF.Roles.Where(x => x.Id == record.Id).FirstOrDefault();

                RoleObj.Name = record.Name;

                int updateflag = _InvoiceEF.SaveChanges();

                if (updateflag > 0)
                    return true;
                else
                    return false;
            }



        }


        public bool DeleteRecord(Role record)
        {

            Role _RoleObj = new Role();
            using (_InvoiceEF = new InvoiceEF())
            {
                _RoleObj = _InvoiceEF.Roles.Where(x => x.Id == record.Id).FirstOrDefault();

                _InvoiceEF.Roles.Remove(_RoleObj);
                int deleteflag = _InvoiceEF.SaveChanges();
                if (deleteflag > 0)
                    return true;
                else
                    return false;
            }


        }

        public List<Role> GetAll()
        {
            List<Role> RoleList = new List<Role>();
            using (_InvoiceEF = new InvoiceEF())
            {
                RoleList = _InvoiceEF.Roles.ToList();
            }
            return RoleList;
        }
    }
}

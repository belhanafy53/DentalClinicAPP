using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class WorkerDB
    {
          private InvoiceEF _InvoiceEF;
        public Worker GetRecordByUnPwd(string UserName, string Password)
        {
            Worker WorkerObj = new Worker();
            using (_InvoiceEF = new InvoiceEF())
            {
                WorkerObj = _InvoiceEF.Workers.Where(x => x.UserName.ToUpper() == UserName.ToUpper() && x.PassWord == Password).FirstOrDefault();

            }


            return WorkerObj;
        }

        public Worker GetRecord(int workid)
        {
            Worker WorkerObj = new Worker();
            using (_InvoiceEF = new InvoiceEF())
            {
                WorkerObj = _InvoiceEF.Workers
                    .Include("UserGroup")
                    .Where(x => x.Id == workid && (x.IsDelete == false || x.IsDelete == null)).FirstOrDefault();

            }


            return WorkerObj;
        }

        public bool GetRecordByUserName(string Username)
        {
            Worker WorkerObj = new Worker();
            using (_InvoiceEF = new InvoiceEF())
            {
                WorkerObj = _InvoiceEF.Workers
                    .Where(x => x.UserName == Username).FirstOrDefault();

            }
            if (WorkerObj != null)
                return true;
            else
                return false;
        }
        public int SaveRecord(Worker record)
        {

            using (_InvoiceEF = new InvoiceEF())
            {
                _InvoiceEF.Workers.Add(record);
                _InvoiceEF.SaveChanges();
            }

            return record.Id;
        }

        public bool UpdateRecord(Worker record)
        {

            Worker WorkerObj = new Worker();
            using (_InvoiceEF = new InvoiceEF())
            {
                WorkerObj = _InvoiceEF.Workers.Where(x => x.Id == record.Id).FirstOrDefault();

                WorkerObj.Name = record.Name;
                WorkerObj.MangmentId = record.MangmentId;
                WorkerObj.PassWord = record.PassWord;
                WorkerObj.UserGroup = record.UserGroup;
                WorkerObj.UserName = record.UserName;
                WorkerObj.RoleId = record.RoleId;

                int updateflag = _InvoiceEF.SaveChanges();

                if (updateflag > 0)
                    return true;
                else
                    return false;
            }



        }

        public bool WorkerhaveDat(int id)
        {

            List<InvoiceHeader> WorkerList = new List<InvoiceHeader>();
            using (_InvoiceEF = new InvoiceEF())
            {
                WorkerList = _InvoiceEF.InvoiceHeaders.Where(x => x.InsertedBy == id || x.UpdateBy == id).ToList();
            }
            if (WorkerList.Count > 0)
                return true;
            else
                return false;

        }
        public bool RemoveRecord(int Id)
        {

            Worker _WorkerObj = new Worker();
            using (_InvoiceEF = new InvoiceEF())
            {
                _WorkerObj = _InvoiceEF.Workers.Where(x => x.Id == Id).FirstOrDefault();

                _InvoiceEF.Workers.Remove(_WorkerObj);
                int deleteflag = _InvoiceEF.SaveChanges();
                if (deleteflag > 0)
                    return true;
                else
                    return false;
            }


        }

        public bool DeleteRecord(int Id)
        {

            Worker _WorkerObj = new Worker();
            using (_InvoiceEF = new InvoiceEF())
            {
                _WorkerObj = _InvoiceEF.Workers.Where(x => x.Id == Id).FirstOrDefault();
                _WorkerObj.IsDelete = true;
                int deleteflag = _InvoiceEF.SaveChanges();
                if (deleteflag > 0)
                    return true;
                else
                    return false;
            }


        }

        public List<Worker> GetAll()
        {
            List<Worker> WorkerList = new List<Worker>();
            using (_InvoiceEF = new InvoiceEF())
            {
                WorkerList = _InvoiceEF.Workers.Include("Role")
                    .Where(x => x.IsDelete == false || x.IsDelete == null).ToList();
            }
            return WorkerList;
        }


    }
}

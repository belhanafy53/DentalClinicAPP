using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;


namespace DentalClinicAPP.Classes
{
    class ViewTree
    {

        static DataTable PDataset(string Select_Statment, SqlConnection cn)
        {
            cn.Open();
            SqlDataAdapter ad = new SqlDataAdapter(Select_Statment, cn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            cn.Close();
            return dt;
        }
        public static void fillTreeMulti(TreeView treeview, SqlConnection cn, string SqlStatment, string IDCOLUMN, string NAMECOLUMN, string SUPIDCOLUMN, string Stx)
        {
            DataTable PrSet = PDataset(SqlStatment, cn);
            treeview.Nodes.Clear();
            foreach (DataRow dr in PrSet.Rows)
            {
                string TID = dr[SUPIDCOLUMN].ToString();
                if (TID == "" || TID == "0")
                {

                    TreeNode tnParent = new TreeNode
                    {
                        Text = dr[NAMECOLUMN].ToString(),
                        Name = dr[SUPIDCOLUMN].ToString(),
                        Tag = dr[IDCOLUMN].ToString()

                    };
                    string Value = dr[IDCOLUMN].ToString();
                    tnParent.Expand();
                    treeview.Nodes.Add(tnParent);
                    fillChildMulti(tnParent, cn, Value, SqlStatment, IDCOLUMN, NAMECOLUMN, SUPIDCOLUMN, Stx);
                }
            }
        }
        public static void fillChildMulti(TreeNode Parent, SqlConnection cn, string IID, string TABLE, string IDCOLUMN, string NAMECOLUMN, string SUPIDCOLUMN, string xxxx)
        {
            DataTable Ds = PDataset("Select * From (" + TABLE + ")Chaild WHERE Chaild." + SUPIDCOLUMN + " ='" + IID + "'", cn);
            if (Ds.Rows.Count > 0)
            {
                foreach (DataRow dr in Ds.Rows)
                {
                    string Vstr_xcolumn = "";
                    if (TABLE == "Select * From Tbl_Management" )
                    {

                        //if (dr[xxxx].ToString().Trim() != "")
                        //{
                        //    Vstr_xcolumn = dr[NAMECOLUMN].ToString().Trim() + " ( " + dr[xxxx].ToString().Trim() + " )";
                        //}
                        //else
                        //{ 
                        string temp = dr[IDCOLUMN].ToString();
                        Vstr_xcolumn = dr[NAMECOLUMN].ToString().Trim();
                        //}
                    }
                    else
                    {
                        Vstr_xcolumn = dr[NAMECOLUMN].ToString().Trim();
                    }
                    if (dr[IDCOLUMN].ToString() != dr[SUPIDCOLUMN].ToString())
                    {
                        TreeNode Child = new TreeNode
                        {

                            Text = Vstr_xcolumn,
                            Name = dr[SUPIDCOLUMN].ToString().Trim(),
                            Tag = dr[IDCOLUMN].ToString()
                        };
                        string temp = dr[IDCOLUMN].ToString();
                        Child.Collapse();
                        Parent.Nodes.Add(Child);
                        fillChildMulti(Child, cn, temp, TABLE, IDCOLUMN, NAMECOLUMN, SUPIDCOLUMN, xxxx);
                    }
                }

            }
            else
            {

            }
        }

        public static string SelectFullHirachyMngmnt(TreeView treeview, int Management_ID, int Parent_Id)
        {
            Model1 FsDb = new Model1();
            ModelEvents FsEvDb = new ModelEvents();
            ModelSecurity ScDb = new ModelSecurity();

            string Result = "";

            int TableCount = FsDb.Tbl_Management.Count();
            int ParentItemID = 0;
            int Parent1ItemID = 0;
            string SelectedResult = "";
            string SelectedResult1 = "";

            if (Parent_Id != null)
            {


                var ListitemName = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Management_ID);
                Result = ListitemName.Name;
                var ListParent = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Management_ID);
                if (ListParent.Parent_ID != null)
                {
                    ParentItemID = int.Parse(ListParent.Parent_ID.ToString());
                    var ListParentName = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == ParentItemID);
                    SelectedResult = ListParentName.Name;
                    Result = SelectedResult + " - " + Result;
                    if (ListParentName.Parent_ID != null)
                    {
                        int Vint_parent1id = int.Parse(ListParentName.Parent_ID.ToString());
                        var listParent1 = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Vint_parent1id);
                         var SelectedResult0 = listParent1.Name;
                        Result = SelectedResult0 + " - " + Result;
                        if (listParent1.Parent_ID != null)
                        {
                            Parent1ItemID = int.Parse(listParent1.Parent_ID.ToString());
                            var ListParent1Name = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Parent1ItemID);
                            SelectedResult1 = ListParent1Name.Name;
                            Result = SelectedResult1 + " - " + Result;

                            if (ListParent1Name.Parent_ID != null)
                            {

                                int Vint_parent2id = int.Parse(ListParent1Name.Parent_ID.ToString());
                                var listParent2 = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Vint_parent2id);
                                string SelectedResult2 = listParent2.Name;
                                Result = SelectedResult2 + " - " + Result;
                                if (listParent2.Parent_ID != null)
                                {
                                    int Parent2ItemID = int.Parse(listParent2.Parent_ID.ToString());
                                    var ListParent3 = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Parent2ItemID);
                                    string SelectedResult3 = ListParent3.Name;
                                    Result = SelectedResult3 + " - " + Result;

                                    if (ListParent3.Parent_ID != null)
                                    {
                                        int Parent3ItemID = int.Parse(ListParent3.Parent_ID.ToString()); ;
                                        var ListParent4 = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Parent3ItemID);
                                        string SelectedResult4 = ListParent4.Name;
                                        Result = SelectedResult4 + " - " + Result; ;
                                        if (ListParent4.Parent_ID != null)
                                        {
                                            int Parent4ItemID = int.Parse(ListParent4.Parent_ID.ToString());
                                            var ListParent5 = FsDb.Tbl_Management.FirstOrDefault(x => x.Management_ID == Parent4ItemID);
                                            string SelectedResult5 = ListParent5.Name;
                                            Result = SelectedResult5 + " - " + Result; ;


                                        }

                                    }

                                }

                            }


                        }
                    }
                }


                //var listResultF = FsDb.Tbl_Management.FirstOrDefault(x => x.ID == Management_ID);
                //listResultF.Note = Result;
                //FsDb.SaveChanges();


            }
            return Result;

        }
       
    }
}

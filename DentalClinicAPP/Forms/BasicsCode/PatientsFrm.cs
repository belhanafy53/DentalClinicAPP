using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DentalClinicAPP.Classes;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

using DevExpress.XtraTreeList;
using System.Threading;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;
using System.IO;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class PatientsFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Patientid = 0;
        int Vint_governorateID = 0;
        int Vint_CityId = 0;
        int Vint_SexId = 0;
        int Vint_GovernorateId = 0;
        int Vint_ChronicDiseaseID = 0;
        string Image_Path = "";
        public PatientsFrm()
        {
            InitializeComponent();
            

        }

        private void PatientsFrm_Load(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                LangTxtBox.Text = "en";
                simpleButton3_Click(sender, e);

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                LangTxtBox.Text = "ar-EG";
                simpleButton4_Click(sender, e);
            }
            // TODO: This line of code loads data into the 'dentalClinicDataSet.Tbl_ChronicDiseases' table. You can move, or remove it, as needed.
            //this.tbl_ChronicDiseasesTableAdapter1.Fill(this.dentalClinicDataSet.Tbl_ChronicDiseases);
            // TODO: This line of code loads data into the 'chronicDiseasesDataSet.Tbl_ChronicDiseases' table. You can move, or remove it, as needed.
            //this.tbl_ChronicDiseasesTableAdapter.Fill(this.chronicDiseasesDataSet.Tbl_ChronicDiseases);
            //var listAnalysis = DNtMDB.Tbl_Patients.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;

            //dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["ImagePath"].Visible = false;
            //dataGridView1.Columns["GovernorateID"].Visible = false;
            //dataGridView1.Columns["CityID"].Visible = false;
            //dataGridView1.Columns["PatientAge"].Visible = true;
            //dataGridView1.Columns["PatientAge"].HeaderText = "Patient Age";
            //dataGridView1.Columns["PatientWeight"].Visible = false;
            //dataGridView1.Columns["PatientTall"].Visible = false;
            //dataGridView1.Columns["PatientSex"].Visible = false;
            //dataGridView1.Columns["Tbl_ChronicDiseasesPatient"].Visible = false;
            //dataGridView1.Columns["Tbl_ChronicDiseasesPatient"].Visible = false;
            //dataGridView1.Columns["Tbl_MedicalVisite"].Visible = false;

            //dataGridView1.Columns["Name"].Width = 300;

            //comboBox1.DataSource = DNtMDB.Tbl_Governorates.ToList();
            //comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "ID";
            //comboBox1.Text = "Choose a Governorate";

            //comboBox2.DataSource = DNtMDB.Tbl_Cities.ToList();
            //comboBox2.DisplayMember = "Name";
            //comboBox2.ValueMember = "ID";
            //comboBox2.Text = "Choose a City";


            PatientNameTxt.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "en";

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "ar-EG";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (PatientNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter  Clinic name want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم العيادة المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    Vint_GovernorateId = int.Parse(comboBox1.SelectedValue.ToString());
                    Vint_CityId = int.Parse(comboBox2.SelectedValue.ToString());
                    Vint_SexId = int.Parse(comboBox3.SelectedIndex.ToString());
                    //Tbl_Patients PatientAd = new Tbl_Patients
                    //{
                    //    Name = PatientNameTxt.Text,
                    //    GovernorateID = Vint_GovernorateId,
                    //    CityID = Vint_CityId,
                    //    PatientAge = int.Parse(PatientAgeTxt.Text),
                    //    PatientWeight = int.Parse(PatientWeightTxt.Text),
                    //    PatientTall = int.Parse(PatientTallTxt.Text),

                    //    ImagePath = Image_Path,
                    //    PatientSex = Vint_SexId

                    //};

                    //DNtMDB.Tbl_Patients.Add(PatientAd);
                    //DNtMDB.SaveChanges();
                    //Vint_Patientid = PatientAd.ID;
                    //if (treeList1.Nodes.Count() > 0)
                    //{
                    //    foreach (TreeListNode n in treeList1.GetAllCheckedNodes())
                    //    {
                    //        Vint_ChronicDiseaseID = int.Parse(n.GetValue("ID").ToString());

                    //        Tbl_ChronicDiseasesPatient ChDesPatient = new Tbl_ChronicDiseasesPatient
                    //        {
                    //            PatientID = Vint_Patientid,
                    //            Chronic_DiseaseID = Vint_ChronicDiseaseID
                    //        };
                    //        DNtMDB.Tbl_ChronicDiseasesPatient.Add(ChDesPatient);
                    //        DNtMDB.SaveChanges();
                    //    }
                    //}


                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Patient Add",
                        TableName = "Tbl_Patients",
                        TableRecordId = Vint_Patientid.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "PatientsFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Saved");
                    }
                    else
                    {
                        MessageBox.Show("تم الحفظ");
                    }
                    List<TreeListNode> nodes = treeList1.GetNodeList();
                    foreach (TreeListNode item in nodes)
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                    textBox1.Text = "";
                    PatientNameTxt.Text = "";
                    PicturePath.ImageLocation = "";
                    PatientAgeTxt.Text = "";
                    PatientWeightTxt.Text = "";
                    PatientTallTxt.Text = "";
                    comboBox1.SelectedValue = -1;
                    comboBox1.Text = "Choose governorate";

                    comboBox2.SelectedValue = -1;
                    comboBox2.Text = "Choose City";

                    comboBox3.SelectedValue = -1;
                    comboBox3.Text = "Choose Sex";

                    PatientNameTxt.Focus();
                    PatientNameTxt.Select();
                    this.ActiveControl = PatientNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Patients.ToList();
                    PatientNameTxt.Focus();
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Analysis");
                    //}
                }
                else if (textBox1.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    //Vint_GovernorateId = int.Parse(comboBox1.SelectedValue.ToString());
                    //Vint_CityId = int.Parse(comboBox2.SelectedValue.ToString());
                    //Vint_SexId = int.Parse(comboBox3.SelectedIndex.ToString());
                    //Vint_Patientid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_Patients.SingleOrDefault(x => x.ID == Vint_Patientid);
                    //result.Name = PatientNameTxt.Text;
                    //result.ImagePath = Environment.CurrentDirectory + "\\Images\\Patients\\" + result.ID + ".jpg";
                    ////Image_Path = Environment.CurrentDirectory + "\\Images\\Clinics\\" + result.ID + ".jpg";
                    //result.GovernorateID = Vint_GovernorateId;
                    //result.CityID = Vint_CityId;
                    //result.PatientAge = int.Parse(PatientAgeTxt.Text);
                    //result.PatientWeight = int.Parse(PatientWeightTxt.Text);
                    //result.PatientTall = int.Parse(PatientTallTxt.Text);
                    //result.PatientSex = Vint_SexId;
                    //DNtMDB.SaveChanges();
                    //var listRemoveCDP = DNtMDB.Tbl_ChronicDiseasesPatient.Where(x => x.PatientID == Vint_Patientid && x.Chronic_DiseaseID == Vint_ChronicDiseaseID);
                    //DNtMDB.Tbl_ChronicDiseasesPatient.RemoveRange(listRemoveCDP);
                    //if (treeList1.Nodes.Count() > 0)
                    //{
                    //    foreach (TreeListNode n in treeList1.GetAllCheckedNodes())
                    //    {
                    //        Vint_ChronicDiseaseID = int.Parse(n.GetValue("ID").ToString());

                    //        Tbl_ChronicDiseasesPatient ChDesPatient = new Tbl_ChronicDiseasesPatient
                    //        {
                    //            PatientID = Vint_Patientid,
                    //            Chronic_DiseaseID = Vint_ChronicDiseaseID
                    //        };
                    //        DNtMDB.Tbl_ChronicDiseasesPatient.Add(ChDesPatient);
                    //        DNtMDB.SaveChanges();
                    //    }
                    //}
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Clinic Update",
                        TableName = "Tbl_Patients",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "PatientsFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Updated");
                    }
                    else
                    {
                        MessageBox.Show("تم التعديل");
                    }
                    List<TreeListNode> nodes = treeList1.GetNodeList();
                    foreach (TreeListNode item in nodes)
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                    textBox1.Text = "";
                    PatientNameTxt.Text = "";
                    PicturePath.ImageLocation = "";
                    PatientAgeTxt.Text = "";
                    PatientWeightTxt.Text = "";
                    PatientTallTxt.Text = "";
                    comboBox1.SelectedValue = -1;
                    comboBox1.Text = "Choose governorate";

                    comboBox2.SelectedValue = -1;
                    comboBox2.Text = "Choose City";

                    comboBox3.SelectedValue = -1;
                    comboBox3.Text = "Choose Sex";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Patients.ToList();
                    //PatientNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Analysis");
                    //}
                }
                if (Image_Path != "")
                {


                    string NewPath = Environment.CurrentDirectory + "\\Images\\Patients\\" + Vint_Patientid + ".jpg";

                    File.Copy(Image_Path, NewPath, true);
                    NewPath = Image_Path;
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 4 && w.ProcdureId == 4);
            //if (validationSaveUser != null)
            //{
            int Vint_D1rows = dataGridView1.RowCount;

            if (Vint_D1rows != 0 && textBox1.Text != "")
            {

                //    if (LangTxtBox.Text == "en")
                //    {
                //        var result1 = MessageBox.Show("Do you want to delete this analys  ؟", "Delete Analys ", MessageBoxButtons.YesNo);
                //        if (result1 == DialogResult.Yes)
                //        {

                //            Vint_Patientid = int.Parse(textBox1.Text);
                //            var listRemoveCDP = DNtMDB.Tbl_ChronicDiseasesPatient.Where(x => x.PatientID == Vint_Patientid);
                //            if (listRemoveCDP != null)
                //            {
                //                DNtMDB.Tbl_ChronicDiseasesPatient.RemoveRange(listRemoveCDP);
                //                DNtMDB.SaveChanges();
                //            }

                //            var resultR = DNtMDB.Tbl_Patients.Find(Vint_Patientid);
                //            DNtMDB.Tbl_Patients.Remove(resultR);
                //            DNtMDB.SaveChanges();
                //            //---------------خفظ ااحداث 
                //            SecurityEvent sev = new SecurityEvent
                //            {
                //                ActionName = "Delete Clinic",
                //                TableName = "Tbl_Patients",
                //                TableRecordId = resultR.ID.ToString(),
                //                Description = "",
                //                ManagementName = Program.GlbV_Management,
                //                ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                //                EmployeeName = Program.GlbV_EmpName,
                //                User_ID = Program.GlbV_UserId,
                //                UserName = Program.GlbV_UserName,
                //                FormName = "PatientsFrm"


                //            };
                //            DNtEVDB.SecurityEvents.Add(sev);
                //            DNtEVDB.SaveChanges();
                //            //************************************

                //            if (LangTxtBox.Text == "en")
                //            {
                //                MessageBox.Show(" Deleted");
                //            }
                //            else
                //            {
                //                MessageBox.Show("تم الحذف");
                //            }

                //        }
                //        if (Image_Path != "")
                //        {


                //            Image_Path = Environment.CurrentDirectory + "\\Images\\Clinics\\" + Vint_Patientid + ".jpg";

                //            File.Delete(Image_Path);
                //            PicturePath.ImageLocation = "";

                //        }
                //        List<TreeListNode> nodes = treeList1.GetNodeList();
                //        foreach (TreeListNode item in nodes)
                //        {
                //            item.CheckState = CheckState.Unchecked;
                //        }
                //        textBox1.Text = "";
                //        PatientNameTxt.Text = "";
                //        PicturePath.ImageLocation = "";
                //        PatientAgeTxt.Text = "";
                //        PatientWeightTxt.Text = "";
                //        PatientTallTxt.Text = "";
                //        comboBox1.SelectedValue = -1;
                //        comboBox1.Text = "Choose governorate";

                //        comboBox2.SelectedValue = -1;
                //        comboBox2.Text = "Choose City";

                //        comboBox3.SelectedValue = -1;
                //        comboBox3.Text = "Choose Sex";



                //        PatientNameTxt.Select();
                //        this.ActiveControl = PatientNameTxt;

                //        dataGridView1.DataSource = DNtMDB.Tbl_Patients.ToList();
                //        PatientNameTxt.Focus();
                //    }
                //    else
                //    {
                //        var result1 = MessageBox.Show("هل تريد حذف هذا العيادة  ؟", "حذف العيادة ", MessageBoxButtons.YesNo);
                //        if (result1 == DialogResult.Yes)
                //        {

                //            Vint_Patientid = int.Parse(textBox1.Text);
                //            var resultR = DNtMDB.Tbl_Patients.Find(Vint_Patientid);
                //            DNtMDB.Tbl_Patients.Remove(resultR);
                //            DNtMDB.SaveChanges();
                //            //---------------خفظ ااحداث 
                //            SecurityEvent sev = new SecurityEvent
                //            {
                //                ActionName = "Delete Clinic",
                //                TableName = "Tbl_Patients",
                //                TableRecordId = resultR.ID.ToString(),
                //                Description = "",
                //                ManagementName = Program.GlbV_Management,
                //                ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                //                EmployeeName = Program.GlbV_EmpName,
                //                User_ID = Program.GlbV_UserId,
                //                UserName = Program.GlbV_UserName,
                //                FormName = "PatientsFrm"


                //            };
                //            DNtEVDB.SecurityEvents.Add(sev);
                //            DNtEVDB.SaveChanges();
                //            //************************************

                //            if (LangTxtBox.Text == "en")
                //            {
                //                MessageBox.Show(" Deleted");
                //            }
                //            else
                //            {
                //                MessageBox.Show("تم الحذف");
                //            }
                //            if (Image_Path != "")
                //            {


                //                Image_Path = Environment.CurrentDirectory + "\\Images\\Clinics\\" + Vint_Patientid + ".jpg";

                //                File.Delete(Image_Path);
                //                PicturePath.ImageLocation = "";

                //            }
                //        }
                //        textBox1.Text = "";
                //        PatientNameTxt.Text = "";
                //        PicturePath.ImageLocation = "";
                //        PatientAgeTxt.Text = "";
                //        PatientWeightTxt.Text = "";
                //        PatientTallTxt.Text = "";
                //        comboBox1.SelectedValue = -1;
                //        comboBox1.Text = "Choose governorate";

                //        comboBox2.SelectedValue = -1;
                //        comboBox2.Text = "Choose City";

                //        comboBox3.SelectedValue = -1;
                //        comboBox3.Text = "Choose Sex";



                //        PatientNameTxt.Select();
                //        this.ActiveControl = PatientNameTxt;

                //        dataGridView1.DataSource = DNtMDB.Tbl_Patients.ToList();
                //        PatientNameTxt.Focus();
                //    }
                //}
                //else
                //{
                //    if (LangTxtBox.Text == "en")
                //    {
                //        MessageBox.Show("Please choose medical analys to want to delete");
                //    }
                //    else
                //    {
                //        MessageBox.Show("من فضلك اختر العيادة المراد حذفه");
                //    }

                //    textBox1.Select();
                //    this.ActiveControl = textBox1;
                //    textBox1.Focus();
                //}
                //}
                //else
                //{
                //    MessageBox.Show("You Don't have permition to add Medical Analysis");
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Patientid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            PatientNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            PicturePath.ImageLocation = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            comboBox2.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            PatientAgeTxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            PatientWeightTxt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            PatientTallTxt.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox3.SelectedIndex = int.Parse(dataGridView1.CurrentRow.Cells[8].Value.ToString());
            if (dataGridView1.CurrentRow.Cells[2].Value != null)
            {
                PicturePath.ImageLocation = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            textBox1.Text = Vint_Patientid.ToString();
            int Vint_TreelistCount = treeList1.Nodes.Count();
            List<TreeListNode> nodes = treeList1.GetNodeList();
            foreach (TreeListNode item in nodes)
            {


                Vint_ChronicDiseaseID = int.Parse(item.GetValue("ID").ToString());


                //var listMs = DNtMDB.Tbl_ChronicDiseasesPatient.FirstOrDefault(x => x.PatientID == Vint_Patientid && x.Chronic_DiseaseID == Vint_ChronicDiseaseID);

                //if (listMs != null)
                //{
                //    item.CheckState = CheckState.Checked;
                //}
                //else
                //{
                //    item.CheckState = CheckState.Unchecked;
                //}

            }

        }

        private void PicturePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PicturePath.ImageLocation = dialog.FileName;
                Image_Path = dialog.FileName;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Vint_governorateID = int.Parse(comboBox1.SelectedValue.ToString());
            //var ListCity = DNtMDB.Tbl_Cities.Where(x => x.GovernorateID == Vint_governorateID).OrderBy(x => x.Name).ToList();
            //comboBox2.DataSource = ListCity;
            //comboBox2.DisplayMember = "Name";
            //comboBox2.ValueMember = "ID";
            //comboBox2.Text = "Choose a City";
        }

        private void PatientNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PatientAgeTxt.Focus();
            }
        }

        private void PatientAgeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PatientTallTxt.Focus();
            }
        }

        private void TallTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PatientWeightTxt.Focus();
            }
        }

        private void PatientWeightTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox3.Focus();
            }
        }

        private void radioGroup1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                treeList1.Focus();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            if (edit.SelectedIndex == 0)
            {

                Vint_SexId = 1;

            }
            else if (edit.SelectedIndex == 1)
            {
                Vint_SexId = 2;
            }


        }

        private void PatientNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listPatientSerch = DNtMDB.Tbl_Patients.Where(x => x.Name.Contains(PatientNameTxt.Text)).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listPatientSerch;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
             {
                //PicturePath.ImageLocation = "E:\\DentalClinic\\DentalClinicAPP\\DentalClinicAPP\\Images\\Man.jpg";
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                //PicturePath.ImageLocation = "E:\\DentalClinic\\DentalClinicAPP\\DentalClinicAPP\\Images\\Woman.jpg";
            }
        }
    }
}
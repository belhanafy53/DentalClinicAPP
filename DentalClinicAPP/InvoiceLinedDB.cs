using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class InvoiceLinedDB
    {


        private InvoiceEF _InvoiceEF;

        //////public int SaveRecord(InvoiceLined record)
        //////{
        //////    using (_InvoiceEF = new InvoiceEF())
        //////    {
        //////        _InvoiceEF.InvoiceLineds.Add(record);
        //////        _InvoiceEF.SaveChanges();
        //////    }

        //////    return record.ID;
        //////}

        //////public bool UpdateRecord(InvoiceLined record)
        //////{

        //////    InvoiceLined InvoiceLinedObj = new InvoiceLined();
        //////    using (_InvoiceEF = new InvoiceEF())
        //////    {
        //////        InvoiceLinedObj = _InvoiceEF.InvoiceLineds.Where(x => x.ID == record.ID).FirstOrDefault();

        //////        InvoiceLinedObj.description = record.description;
        //////        InvoiceLinedObj.discount_amount = record.discount_amount;
        //////        InvoiceLinedObj.discount_rate = record.discount_rate;
        //////        InvoiceLinedObj.internalCode = record.internalCode;
        //////        InvoiceLinedObj.InvoiceHeaderID = record.InvoiceHeaderID;
        //////        InvoiceLinedObj.itemCode = record.itemCode;
        //////        InvoiceLinedObj.itemsDiscount = record.itemsDiscount;
        //////        InvoiceLinedObj.itemType = record.itemType;
        //////        InvoiceLinedObj.netTotal = record.netTotal;
        //////        InvoiceLinedObj.quantity = record.quantity;
        //////        InvoiceLinedObj.salesTotal = record.salesTotal;
        //////        InvoiceLinedObj.taxableItems_amount = record.taxableItems_amount;
        //////        InvoiceLinedObj.taxableItems_rate = record.taxableItems_rate;
        //////        InvoiceLinedObj.taxableItems_subType = record.taxableItems_subType;
        //////        InvoiceLinedObj.taxableItems_taxType = record.taxableItems_taxType;
        //////        InvoiceLinedObj.total = record.total;
        //////        InvoiceLinedObj.totalTaxableFees = record.totalTaxableFees;
        //////        InvoiceLinedObj.unitType = record.unitType;
        //////        InvoiceLinedObj.unitValue_amountEGP = record.unitValue_amountEGP;
        //////        InvoiceLinedObj.unitValue_currencySold = record.unitValue_currencySold;
        //////        InvoiceLinedObj.valueDifference = record.valueDifference;

        //////        int updateflag = _InvoiceEF.SaveChanges();

        //////        if (updateflag > 0)
        //////            return true;
        //////        else
        //////            return false;
        //////    }


        //////}
        public int SaveRecord(InvoiceLined record)
        {
            try
            {
                using (_InvoiceEF = new InvoiceEF())
                {
                    _InvoiceEF.InvoiceLineds.Add(record);
                    int result = _InvoiceEF.SaveChanges();

                    if (result > 0)
                    {
                        //MessageBox.Show($"Record saved successfully with ID: {record.ID}");
                        return record.ID;
                    }
                    else
                    {
                        MessageBox.Show("Failed to save the record.");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving record: {ex.Message}");
                return 0;
            }
        }

        public bool UpdateRecord(InvoiceLined record)
        {
            try
            {
                using (_InvoiceEF = new InvoiceEF())
                {
                    var InvoiceLinedObj = _InvoiceEF.InvoiceLineds.Where(x => x.ID == record.ID).FirstOrDefault();

                    if (InvoiceLinedObj == null)
                    {
                        MessageBox.Show("Record not found for update.");
                        return false;
                    }

                    // Update fields
                    InvoiceLinedObj.description = record.description;
                    InvoiceLinedObj.discount_amount = record.discount_amount;
                    InvoiceLinedObj.discount_rate = record.discount_rate;
                    InvoiceLinedObj.internalCode = record.internalCode;
                    InvoiceLinedObj.InvoiceHeaderID = record.InvoiceHeaderID;
                    InvoiceLinedObj.itemCode = record.itemCode;
                    InvoiceLinedObj.itemsDiscount = record.itemsDiscount;
                    InvoiceLinedObj.itemType = record.itemType;
                    InvoiceLinedObj.netTotal = record.netTotal;
                    InvoiceLinedObj.quantity = record.quantity;
                    InvoiceLinedObj.salesTotal = record.salesTotal;
                    InvoiceLinedObj.taxableItems_amount = record.taxableItems_amount;
                    InvoiceLinedObj.taxableItems_rate = record.taxableItems_rate;
                    InvoiceLinedObj.taxableItems_subType = record.taxableItems_subType;
                    InvoiceLinedObj.taxableItems_taxType = record.taxableItems_taxType;
                    InvoiceLinedObj.total = record.total;
                    InvoiceLinedObj.totalTaxableFees = record.totalTaxableFees;
                    InvoiceLinedObj.unitType = record.unitType;
                    InvoiceLinedObj.unitValue_amountEGP = record.unitValue_amountEGP;
                    InvoiceLinedObj.unitValue_currencySold = record.unitValue_currencySold;
                    InvoiceLinedObj.valueDifference = record.valueDifference;

                    int updateflag = _InvoiceEF.SaveChanges();

                    if (updateflag > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the record.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"Error updating record: {ex.Message}");
                return false;
            }
        }

        public bool DeleteRecord(InvoiceLined record)
        {

            InvoiceLined _InvoiceLinedObj = new InvoiceLined();
            using (_InvoiceEF = new InvoiceEF())
            {
                _InvoiceLinedObj = _InvoiceEF.InvoiceLineds.Where(x => x.ID == record.ID).FirstOrDefault();

                _InvoiceEF.InvoiceLineds.Remove(_InvoiceLinedObj);
                int deleteflag = _InvoiceEF.SaveChanges();
                if (deleteflag > 0)
                    return true;
                else
                    return false;
            }


        }

        public List<InvoiceLined> GetAll()
        {
            List<InvoiceLined> InvoiceLinedList = new List<InvoiceLined>();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceLinedList = _InvoiceEF.InvoiceLineds.ToList();
            }
            return InvoiceLinedList;
        }

        public InvoiceLined GetRecordById(int Id)
        {
            InvoiceLined InvoiceLinedObj = new InvoiceLined();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceLinedObj = _InvoiceEF.InvoiceLineds.Where(x => x.ID == Id).FirstOrDefault();
            }
            return InvoiceLinedObj;
        }

        public InvoiceLined GetRecordByLineInternal(string InternalId)
        {
            InvoiceLined InvoiceLinedObj = new InvoiceLined();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceLinedObj = _InvoiceEF.InvoiceLineds.Where(x => x.internalCode == InternalId).FirstOrDefault();
            }
            return InvoiceLinedObj;
        }
        public List<InvoiceLined> GetRecordByInvoiceHeaderID(int InvoiceHeaderID)
        {
            List<InvoiceLined> InvoiceLinedObj = new List<InvoiceLined>();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceLinedObj = _InvoiceEF.InvoiceLineds.Where(x => x.InvoiceHeaderID == InvoiceHeaderID).ToList();
            }
            return InvoiceLinedObj;
        }


    }
}

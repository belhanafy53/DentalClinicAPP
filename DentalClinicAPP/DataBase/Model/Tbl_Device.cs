namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Device
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string DeviceName { get; set; }

        [StringLength(150)]
        public string DeviceNO { get; set; }
    }
}

namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHoi")]
    public partial class CauHoi
    {
        [Key]
        public int iMaCauHoi { get; set; }

        [StringLength(500)]
        public string sNoiDungCauHoi { get; set; }

        [StringLength(200)]
        public string sCauTraLoi1 { get; set; }

        [StringLength(200)]
        public string sCauTraLoi2 { get; set; }

        [StringLength(200)]
        public string sCauTraLoi3 { get; set; }

        [StringLength(200)]
        public string sCauTraLoi4 { get; set; }

        public int? iDapAn { get; set; }

        [StringLength(50)]
        public string sMaMonHoc { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}

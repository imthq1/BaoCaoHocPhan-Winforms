namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diem")]
    public partial class Diem
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string sMaMonHoc { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string sMssv { get; set; }

        public decimal? dDiem { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}

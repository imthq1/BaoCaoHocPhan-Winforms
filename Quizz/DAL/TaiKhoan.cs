namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            Diems = new HashSet<Diem>();
        }

        [Key]
        [StringLength(50)]
        public string sMssv { get; set; }

        [StringLength(100)]
        public string sMatKhau { get; set; }

        [StringLength(100)]
        public string sHoTen { get; set; }

        [StringLength(50)]
        public string sMaKhoa { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dNgaySinh { get; set; }

        [StringLength(10)]
        public string sGioiTinh { get; set; }

        public int? iMaQuyen { get; set; }

        [Column(TypeName = "image")]
        public byte[] AvatarID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diem> Diems { get; set; }

        public virtual Khoa Khoa { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}

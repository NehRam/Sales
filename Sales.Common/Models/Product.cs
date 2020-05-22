namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        [Key]
        public int ProducId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [DataType (DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal Price { get; set; }
         
        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Publis On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

        [Display (Name ="Image")]
        public string ImagePath { get; set; }
        public override string ToString()
        {
            return this.Description; 
        }

        public string ImageFullPath {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }
                return $"https://salesbackend20200510.azurewebsites.net/{this.ImagePath.Substring(1)}";
            } 
        }
    }

}

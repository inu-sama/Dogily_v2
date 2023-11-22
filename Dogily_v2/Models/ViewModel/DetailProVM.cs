using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dogily_v2.Models.ViewModel
{
    public class DetailProVM
    {
        [Key]
        //ProductDetail
        public int ProDeID { get; set; }
        public Nullable<byte> RemainAmount { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }

        //Product
        public string IDPro { get; set; }
        public string Title { get; set; }
        public Nullable<double> Price { get; set; }

        //ProImg
        public int ImgID { get; set; }
        public string ImgURL { get; set; }
        public Nullable<bool> MainImg { get; set; }

        //Category
        public int IDCate { get; set; }
        public string NameCate { get; set; }

        //Brand
        public string BrandName { get; set; }
        public string About { get; set; }

        //Country
        public int? IDCountry { get; set; }
        public string CountryName { get; set; }

        //Weight
        public int? Weight { get; set; }

        //Feedback
        public int IDFB { get; set; }
        public Nullable<byte> Rate { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<DetailProVM> RelatedProduct { get; set;}
        public ICollection<DetailProVM> GetFeedback { get; set;}
        public ICollection<DetailProVM> ImgList { get; set;}
    }
}
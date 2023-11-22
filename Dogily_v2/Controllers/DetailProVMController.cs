using Dogily_v2.Models;
using Dogily_v2.Models.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dogily_v2.Controllers
{
    public class DetailProVMController : Controller
    {
        private DogilyEntities6 db = new DogilyEntities6();
        // GET: DetailProVM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailProduct(string id)
        {
            if (id != null)
            {
                ProductDetail pro = db.ProductDetails.FirstOrDefault(p => p.IDPro == id);
                DetailProVM detailProVM = new DetailProVM
                {
                    //Product Detail
                    ProDeID = pro.ProDeID,
                    RemainAmount = pro.RemainAmount,
                    Status = pro.Status,
                    Description = pro.Description,
                    Ingredients = pro.Ingredients,

                    //Product
                    IDPro = pro.IDPro,
                    Title = pro.Product.Title,
                    Price = pro.Product.Price,

                    //Category
                    IDCate = pro.Product.IDCate,
                    NameCate = pro.Product.Category.NameCate,

                    //Brand
                    BrandName = pro.BrandName,
                    About = pro.Brand.About,

                    //Country   
                    IDCountry = pro.Brand.IDCountry,
                    CountryName = pro.Brand.Country.CountryName,

                    //Weight
                    Weight = pro.Weight,

                    //Feedback
                    //Image
                    ImgList = ImgList(pro.Product.IDPro),

                    //RelatedProduct
                    RelatedProduct = RelatedProduct(pro.Product.IDCate)
                };
                return View(detailProVM);
            }
            return Content("Missing ID!");
        }
        public List<DetailProVM> ImgList(string ProID)
        {
            List<ProImg> images = db.ProImgs.Where(i => i.IDPro == ProID).ToList();
            List<DetailProVM> imgList = images.Select(
                image => new DetailProVM
                {
                    //ProImgs
                    ImgID = image.ImgID,
                    ImgURL = image.ImgURL,
                    MainImg = image.MainImg,
                }).ToList();
            return imgList;
        }
        public ActionResult GetImgList(string ProID)
        {
            return View(ImgList(ProID));
        }
        public List<DetailProVM> RelatedProduct(int? IDCate)
        {
            List<Product> proList = db.Products.Where(d => d.IDCate == IDCate).ToList();
            List<DetailProVM> relatedProduct = proList.Select(
                detail => new DetailProVM()
                {
                    IDPro = detail.IDPro,
                    Title = detail.Title,
                    Price = detail.Price,
                }).ToList();
            return relatedProduct;
        }
        public ActionResult GetRelatedPro(int? IDCate)
        {
            return View(RelatedProduct(IDCate));
        }
    }
}
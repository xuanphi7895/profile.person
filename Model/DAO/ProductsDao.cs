using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class ProductsDao
    {
        OnlineShopDbContext db = null;
        public ProductsDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Product> ListProducts()
        {
            return db.Products.ToList();
        }
        //public IEnumerable<Product> ListProducts()
        //{
        //    return db.Products.ToList();
        //}

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public Product GetDetail(long id)
        {
            return db.Products.Find(id);
        }
        public Product GetById(long id)
        {
            return db.Products.Find(id);
        }
        
        public bool Update(Product entity)
        {
            try
            {
                var products = db.Products.Find(entity.ID);
                products.CategoryID = entity.CategoryID;
                products.Code = entity.Code;
                products.CreatedBy = entity.CreatedBy;
                products.CreatedDate = entity.CreatedDate;
                products.Description = entity.Description;
                products.Detail = entity.Detail;
                products.Image = entity.Image;
                products.IncludedVAT = entity.IncludedVAT;
                products.MetaDescriptions = entity.MetaDescriptions;
                products.MetaKeywords = entity.MetaKeywords;
                products.MetaTitle = entity.MetaTitle;
                products.ModifiedBy = entity.ModifiedBy;
                products.ModifiedDate = entity.ModifiedDate;
                products.MoreImages = entity.MoreImages;
                products.Name = entity.Name;
                products.Price = entity.Price;
                products.PromotionPrice = entity.PromotionPrice;
                products.Quantity = entity.Quantity;
                products.Status = entity.Status;
                products.TopHot = entity.TopHot;
                products.ViewCount = entity.ViewCount;
                products.Warranty = entity.Warranty;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        //public bool IsUpdate(Product entity)
        //{
        //    try
        //    {
        //        var model = db.Products.Find(entity);
        //        model.Price = entity.Price;
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}
        
        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

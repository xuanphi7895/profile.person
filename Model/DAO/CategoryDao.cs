using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.DAO
{
   public  class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db =new OnlineShopDbContext();
        }
        public IEnumerable<Category> ListCategories()
        {
            return db.Categories.ToList();
        }
        public Category GetById(long id)
        {
            return db.Categories.Find(id);
        }

        //public bool GetReturnValue(Category entity)
        //{  

        //    try
        //    {
        //        var categories = db.Categories.Find(entity.ID);
        //          entity.Name = categories.Name;
        //        entity.ParentID = categories.ParentID;   
        //     entity.SeoTitle=   categories.SeoTitle;   
        //      entity.CreatedBy=  categories.CreatedBy;
        //     entity.CreatedDate=   categories.CreatedDate;
        //      entity.DisplayOrder=   categories.DisplayOrder;
        //         entity.Language=   categories.Language;
        //      entity.MetaDescriptions=   categories.MetaDescriptions;
        //        entity.MetaTitle=   categories.MetaTitle;   
        //       entity.ModifiedBy=   categories.ModifiedBy;
        //             entity.ModifiedDate=   categories.ModifiedDate;
        //         entity.ShowOnHome=   categories.ShowOnHome;  
        //     entity.Status=   categories.Status;    
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
            
        //}
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Category entity)
        {
            try
            {
                var categories = db.Categories.Find(entity.ID);
                categories.Name              = entity.Name;
                categories.ParentID          = entity.ParentID;
                categories.SeoTitle          = entity.SeoTitle;
                categories.CreatedBy         = entity.CreatedBy;
                categories.CreatedDate       = entity.CreatedDate;
                categories.DisplayOrder     = entity.DisplayOrder;
                categories.Language          = entity.Language;
                categories.MetaDescriptions  = entity.MetaDescriptions;
                categories.MetaTitle         = entity.MetaTitle;
                categories.ModifiedBy       = entity.ModifiedBy;
                categories.ModifiedDate      = entity.ModifiedDate;
                categories.ShowOnHome       = entity.ShowOnHome;
                categories.Status           = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var categories = db.Categories.Find(id);
                db.Categories.Remove(categories);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}

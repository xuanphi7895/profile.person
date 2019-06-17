using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class UsersDao
    {
        OnlineShopDbContext db = null;
        public UsersDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        //Use PagedList
        //public  IEnumerable<User>  ListUsers(int page, int pageSize)
        //{
        //    return db.Users.OrderByDescending(x=>x.ID).ToPagedList(page,pageSize);
        //}
        //Return list
        public IEnumerable<User> ListUsers()
        {

            return db.Users.ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(User entity)
        {
            try
            {
                var user          = db.Users.Find(entity.ID);
                user.Name         = entity.Name;
                user.Phone        = entity.Phone;
                user.Address      = entity.Address;
                user.CreatedBy    = entity.CreatedBy;
                user.DistrictID   = entity.DistrictID;
                user.Email        = entity.Email;
                user.GroupID      = entity.GroupID;
                user.ModifiedBy   = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.ProvinceID   = entity.ProvinceID;
                user.Status       = entity.Status;
                user.Password     = entity.Password;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        //Method Delete one recored in 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User UserGetById(string userName)
        {
            return db.Users.SingleOrDefault(u=>u.UserName == userName);
        }
        //Look up for primary key ID
        public User UserGetById(long id)
        {
            return db.Users.Find(id);
        }
        public bool ChangStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public int Login(string userName, string passWord)
        {
            //0: Tài khoản hoặc mật khẩu không đúng
            //1: Đăng nhập thành công!
            //-1: Tài khoản bị khóa.
            //-2: Tài khoản chưa đăng kí.
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
               
                return -2;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        
    }
}

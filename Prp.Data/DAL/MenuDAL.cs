using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class MenuDAL : PrpDBConnect
    {

        public Menu GetById(int menuId)
        {
            Menu obj = new Menu();
            try
            {
                var objt = db.tvwMenus.FirstOrDefault(x => x.menuId == menuId);
                obj = MapMenu.ToEntity(objt);

            }
            catch (Exception ex)
            {
                obj = new Menu();
            }
            return obj;
        }

        public List<Menu> GetAll()
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
                list = new List<Menu>();
            }
            return list;
        }

        public List<Menu> GetByType(int typeId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.Where(x => x.typeId == typeId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Menu> GetByApp(int appId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.Where(x => x.appId == appId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityDDL> GetMenuForDDL(DDLMenu obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spMenuForDDL(obj.typeId, obj.parentId, obj.reffId, obj.userId, obj.reffIds, obj.condition).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Menu> GetParent(int appId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.Where(x => x.appId == appId && x.parentId == 0).OrderBy(x => x.sortOrder).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public List<Menu> Search(int appId, int parentId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.spMenuSearch(appId, parentId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Menu> GetByUser(int userId, int appId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.spMenuGetByUserId(userId, appId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public bool CheckPageHasRight(int appId, int userId, string url)
        {
            bool result = false;
            try
            {
                var itemd = db.spMenuGetRightByUrl(url, userId, appId).ToList();
                if (itemd != null && itemd.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }


        public Message AddUpdate(Menu obj)
        {
            Message msg = new Message();
            try
            {
                tblMenu menu = new tblMenu();

                if (obj.menuId == 0)
                {
                    menu = MapMenu.ToTable(obj);
                    db.tblMenus.Add(menu);
                }
                else
                {
                    menu = db.tblMenus.FirstOrDefault(x => x.menuId == obj.menuId);
                    menu.menuId = obj.menuId;
                    menu.name = obj.name;
                    menu.nameDisplay = obj.nameDisplay;
                    menu.url = obj.url;
                    menu.iconClass = obj.iconClass;
                    menu.sortOrder = obj.sortOrder;
                    menu.isMenu = obj.isMenu;
                    menu.typeId = obj.typeId;
                    menu.appId = obj.appId;
                    menu.isActive = obj.isActive;
                    menu.parentId = obj.parentId;
                    menu.dated = obj.dated;
                    menu.adminId = obj.adminId;
                }
                db.SaveChanges();

                msg.id = menu.menuId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        #region Access

        public Message AddUpdateUserAccess(int userId, string menuIds)
        {
            Message msg = new Message();

            try
            {
                db.spMenuAccessAddUpdate(userId, menuIds);
                msg.status = true;
                msg.msg = "Data saved";
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.status = false;
            }

            return msg;
        }

        public Message AddUpdateUserTypeAccess(int typeId, string menuIds)
        {
            Message msg = new Message();

            try
            {
                db.spMenuUserTypeAddUpdate(typeId, menuIds);
                msg.status = true;
                msg.msg = "Data saved";
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.status = false;
            }
            return msg;
        }


        public List<Menu> GetMenuListForUserId(int userId)
        {
            List<Menu> listMenu = new List<Menu>();
            try
            {
                var list = db.spMenuGetAllForUser(userId).ToList();
                listMenu = MapMenu.ToEntityList(list);
            }
            catch (Exception ex)
            {
            }
            return listMenu;
        }

        public List<Menu> GetMenuListForUserType(int userType)
        {
            List<Menu> listMenu = new List<Menu>();
            try
            {
                var list = db.spMenuGetAllForUserType(userType).ToList();
                listMenu = MapMenu.ToEntityList(list);
            }
            catch (Exception ex)
            {
            }
            return listMenu;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVATreeviewOndemand.Controllers
{
    public class TreeviewController : Controller
    {
        //
        // GET: /Treeview/

        public ActionResult OnDemand()
        {
            List<SiteMenu> all = new List<SiteMenu>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                all = dc.SiteMenus.Where(a => a.ParentMenuID.Equals(0)).ToList();
            }

            return View(all);
        }

        public JsonResult GetSubMenu(string pid)
        {
            // this action for Get Sub Menus from database and return as json data
            //System.Threading.Thread.Sleep(5000);
            List<SiteMenu> subMenus = new List<SiteMenu>();
            int pID = 0;
            int.TryParse(pid, out pID);
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                subMenus = dc.SiteMenus.Where(a => a.ParentMenuID.Equals(pID)).OrderBy(a => a.MenuName).ToList();
            }

            return new JsonResult { Data= subMenus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}

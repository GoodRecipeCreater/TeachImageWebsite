using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeachImageWebsite.Models;

namespace TeachImageWebsite.Controllers
{
    public class HomeController : Controller
    {
        projectsqlEntities1 db = new projectsqlEntities1();
        public ActionResult Index()
        {
            var Members = db.cMember.ToList();
            if (Session["Member"] == null)
            {
                return View("Index", "_Layout", Members);
            }
            return View("Index", "_LayoutMember", Members);
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Menu2()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Reservation()
        {
            return View();
        }
        public ActionResult Stuff()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Blog_Single()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string cEmail, string cPwd)
        {
            var member = db.cMember
                .Where(m => m.cEmail == cEmail && m.cPwd == cPwd)
                .FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "密碼錯誤，登入失敗";
                return View();
            }
            Session["Welcom"] = member.cName + "歡迎光臨";
            Session["Member"] = member;
            Session["member_id"] = member.cId;
            return RedirectToAction("index");

        }
        public ActionResult Logout()
        {
            Session.Clear();//清除Session變數資料
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "cId,cName,cEmail,cPwd")] cMember pmember)
        {
            //if (pmember.cPwd != pmember.cPPwd)
            //{
            //    ModelState.AddModelError("cPPwd", "密碼和確認密碼不一致");
            //    return View(pmember);
            //}
            //若模型沒有通過驗證則顯示目前的View
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // 依帳號取得會員並指定給member
            var member = db.cMember
                .Where(m => m.cEmail == pmember.cEmail)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                db.cMember.Add(pmember);
                db.SaveChanges();
                //執行Home控制器的Login動作方法
                return RedirectToAction("Login");
            }
            ViewBag.Message = pmember.cEmail + "已有人使用，請重新註冊";
            return View();
        }
        
        public ActionResult Recipe()
        {
            return View();
        }
        public ActionResult Recipe2()
        {
            return View();
        }
    }
}
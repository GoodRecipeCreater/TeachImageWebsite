using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeachImageWebsite.Models;
using TeachImageWebsite.Models.ViewModel;
using System.IO;

namespace TeachImageWebsite.Controllers
{
    public class RecipeController : Controller
    {
        projectsqlEntities1 db = new projectsqlEntities1();
        // GET: Recipe
        [HttpGet]
        public ActionResult Recipe(int ? id)
        {
            
            var recipe1 = from c in db.aRecipe//view傳的aid=recipe1.aid
                            where c.aId == id
                            select c;
            aRecipe aRecipe = recipe1.FirstOrDefault();//找到的第一個值
            var ingredient1 = from c in db.Ingredient
                              where c.aId == aRecipe.aId//根據arecipe抓aid=食材的aid
                              select c;
            Ingredient ingredient = ingredient1.FirstOrDefault();

            var steps1 = from c in db.Steps
                         where c.aId == aRecipe.aId//根據arecipe抓aid=步驟的aid
                         select c;
            Steps steps = steps1.FirstOrDefault();
            string r_name = aRecipe.aName;
            ViewBag.r_name = r_name;//把資料指定在背包裡(去view)
            string i_name = ingredient.iName;
            ViewBag.i_name = i_name;
            string s_content = steps.sContent;
            ViewBag.s_content = s_content;
            
            return View(new TeachImageWebsite.Models.ViewModel.RecipeViewModel());//重新定義
        }
        [HttpGet]
        public ActionResult CreateRecipe()
        {
            ViewBag.className = db.RClass.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateRecipe([Bind(Include = "aId,aName,classId,aIntroduce,aImg,aQuantity,aTime,cId")] HttpPostedFileBase file, aRecipe aRecipe)
        {
            //var newbook = db.book.Add(book);


            if (ModelState.IsValid)
            {
                string _FileName = "";
                try
                {
                    if (file.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/FileUploads"), _FileName);
                        file.SaveAs(_path);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
                aRecipe.aImg = _FileName;
                db.aRecipe.Add(aRecipe);
                db.SaveChanges();

                Session["recipe_id"] = aRecipe.aId;
                return RedirectToAction("CreateIngredient");
            }

            return View();

        }
        public ActionResult CreateIngredient()
        {
            Ingredient ingredient = new Ingredient();
            return View();
        }
        [HttpPost]
        public ActionResult saveIngredient([Bind(Include = "iId,iName,iNum,iUnit,aId")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                //20230528待思考
                    db.Ingredient.Add(ingredient);
                    db.SaveChanges();
            }
            return RedirectToAction("CreateIngredient");
        }
        [HttpPost]
        public ActionResult CreateIngredient([Bind(Include = "iId,iName,iNum,iUnit,aId")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                //20230528待思考
                return RedirectToAction("CreateSteps");

            }
            return View();
        }
        public ActionResult CreateSteps()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSteps([Bind(Include = "sId,sContent,aId")] Steps steps)
        {
            if (ModelState.IsValid)
            {
                db.Steps.Add(steps);
                db.SaveChanges();
                return View();
            }
            return View();
        }
        [HttpGet]

        public ActionResult RecipeList()
        {
            int aid = (Int32)Session["member_id"];
            aRecipe recipes = new aRecipe();
            var RecipeList = db.aRecipe.Where(m => m.cId == aid).ToList();
            return View(RecipeList);
        }
        public ActionResult Rookie()
        {
            return View();
        }
        public ActionResult Rookie_1()
        {
            // 處理Rookie_1動作的邏輯
            return View();
        }
        //public ActionResult Index0518(int classIda = 1)
        //{
        //    ViewBag.className = db.RClass
        //        .Where(m => m.classId == classIda)
        //        .FirstOrDefault().className + "分類";
        //    CVMRClass vm = new CVMRClass()
        //    {
        //        className = db.RClass.ToList(),
        //        recipe = db.aRecipe.ToList()
        //        //.Where(m => m.classId == classIda)
        //        //.ToList()

        //    };
        //    return View(vm);
        //}
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    ViewBag.DepName = dbWeek14.tDepartment.ToList();
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(tEmployee emp)
        //{
        //    try
        //    {
        //        dbWeek14.tEmployee.Add(emp);
        //        dbWeek14.SaveChanges();
        //        return RedirectToAction("Index", new { depId = emp.fDepId });
        //    }
        //    catch (Exception ex)
        //    {
        //        string errStr = ex.Message;
        //    }
        //    return View(emp);


    }
}
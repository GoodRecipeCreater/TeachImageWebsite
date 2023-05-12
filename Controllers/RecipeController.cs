using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeachImageWebsite.Models;
using System.IO;

namespace TeachImageWebsite.Controllers
{
    public class RecipeController : Controller
    {
        projectsqlEntities1 db = new projectsqlEntities1();
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateRecipe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRecipe([Bind(Include = "aId,aName,classId,aIntroduce,aImg,aQuantity,aTime,cId")] HttpPostedFileBase file ,aRecipe aRecipe)
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
            return View();
        }
        [HttpPost]
        public ActionResult CreateIngredient([Bind(Include = "iId,iName,iNum,iUnit,aId")]Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                
                db.Ingredient.Add(ingredient);
                db.SaveChanges();
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
    }
}
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {

           // BlogDbContext database = new BlogDbContext();
           //var articles = database.Articles.ToList();

            return View();
        }

        public ActionResult List ()
        {
            using (var database = new BlogDbContext())
            {
                var articles = database.Articles
                .Include(a => a.Author)
                .ToList();
                return View(articles);
            }
                       
        }

        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var database = new BlogDbContext();

            var article = database.Articles
                .Where(a => a.Id == id)
                .First();
          
            return View(article);
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            using (var db = new BlogDbContext())
            {
               
                var model = new ArticleViewModel();
                model.Categories = db.Categories.ToList();
                return View(model);
            } 
        }

        //Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel model)
        {
            using (var db = new BlogDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName.Equals(this.User.Identity.Name));
                var article = new Article(user.Id, model.Title, model.Content, model.CategoryId);
                
                db.Articles.Add(article);
                db.SaveChanges();

                return RedirectToAction("List");
            }
            
        }

        //Get: Edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new BlogDbContext())
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == id);
                if (!this.IsAuthorizedToEdit(article))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                if (article == null)
                {
                    return HttpNotFound();
                }

                var model = new ArticleViewModel();
                model.AuthorId = article.AuthorId;
                model.Title = article.Title;
                model.Content = article.Content;
                model.CategoryId = article.CategoryId;
                model.Categories = db.Categories.ToList();


                return View(model);
            }
            
        }

        private bool IsAuthorizedToEdit(Article article)
        {
            bool isAuthor = article.IsUserAuthor(User.Identity.Name);
            bool isAdmin = User.IsInRole("Admin");
            return isAdmin || isAuthor;
        }

        //post: Edit
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int? id, ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BlogDbContext())
                {
                    var article = db.Articles.FirstOrDefault(a => a.Id == id);
                    article.Title = model.Title;
                    article.Content = model.Content;
                    article.CategoryId = model.CategoryId;

                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }

            return View(model);
        }

        //Get: Delete
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new BlogDbContext())
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == id);

                if (article == null)
                {
                    return HttpNotFound();
                }
                return View(article);
            }
        }

        //Post: Delete
        [HttpPost]
        [Authorize]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new BlogDbContext())
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == id);

                if (article == null)
                {
                    return HttpNotFound();
                }

                db.Articles.Remove(article);
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }
    }
}
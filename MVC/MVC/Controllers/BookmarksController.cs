using MVC.Models;
using ReadLater.Entities;
using ReadLater.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BookmarksController : Controller
    {
        IBookmarkService _bookmarkService;
        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        // GET: Bookmarks
        public ActionResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetBookmarks(null);
            List<BookmarkCategoryViewModel> viewkModel = new List<BookmarkCategoryViewModel>();

            foreach(var bookmark in model)
            {
                var bookmarkCategoryModel = new BookmarkCategoryViewModel { ID = bookmark.ID, URL = bookmark.URL, ShortDescription = bookmark.ShortDescription };
                if(bookmark.Category != null)
                {
                    bookmarkCategoryModel.CategoryName = bookmark.Category.Name;
                    bookmarkCategoryModel.CategoryId = bookmark.Category.ID;
                }

                viewkModel.Add(bookmarkCategoryModel);
            }
           
       
            return View(viewkModel);
        }

        // GET: Bookmarks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookmarks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,URL,ShortDescription,CategoryName")] BookmarkCategoryViewModel bookmarkCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var bookmark = new Bookmark {
                    URL = bookmarkCategoryModel.URL,
                    ShortDescription = bookmarkCategoryModel.ShortDescription,
                };

                _bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return View(bookmarkCategoryModel);
        }


        // GET: Bookmarks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            var bookmarkCategoryModel = new BookmarkCategoryViewModel { ID = bookmark.ID, URL = bookmark.URL, ShortDescription = bookmark.ShortDescription };
            if(bookmark.Category != null)
            {
                bookmarkCategoryModel.CategoryName = bookmark.Category.Name;
                TempData["CategoryName"] = bookmark.Category.Name;
            }
            return View(bookmarkCategoryModel);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,URL,ShortDescription,CategoryName,CategoryId")] BookmarkCategoryViewModel bookmarkCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var bookmark = new Bookmark
                {
                    ID = bookmarkCategoryModel.ID.Value,
                    ShortDescription = bookmarkCategoryModel.ShortDescription,
                    URL = bookmarkCategoryModel.URL,
                    CategoryId  = bookmarkCategoryModel.CategoryId,
                    Category = new Category { ID = bookmarkCategoryModel.CategoryId.Value, Name = bookmarkCategoryModel.CategoryName }
                };

                if(TempData["CategoryName"].ToString() != bookmarkCategoryModel.CategoryName)
                {
                    bookmark.CategoryId = null;
                }

                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(bookmarkCategoryModel);
        }

        // GET: Bookmarks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }

            var bookmarkViewModel = new BookmarkCategoryViewModel { ID = bookmark.ID, URL = bookmark.URL, ShortDescription = bookmark.ShortDescription, CategoryName = bookmark.Category.Name };
            return View(bookmarkViewModel);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookmark category = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(category);
            return RedirectToAction("Index");
        }

    }
}
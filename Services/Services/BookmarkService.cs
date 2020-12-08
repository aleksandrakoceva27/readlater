using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadLater.Entities;
using ReadLater.Repository;

namespace ReadLater.Services
{
    public class BookmarkService : IBookmarkService
    {
        protected IUnitOfWork _unitOfWork;
        ICategoryService _categoryService;

        public BookmarkService(IUnitOfWork unitOfWork, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
        }


        public List<Bookmark> GetBookmarks(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return _unitOfWork.Repository<Bookmark>().Query()
                                                        .OrderBy(l => l.OrderByDescending(b => b.CreateDate))
                                                        .Get()
                                                        .ToList();
            }
            else
            {
                return _unitOfWork.Repository<Bookmark>().Query()
                                                            .Filter(b => b.Category != null && b.Category.Name == category)
                                                            .Get()
                                                            .ToList();
            }
        }

        public Bookmark GetBookmark(int Id)
        {
            return _unitOfWork.Repository<Bookmark>().Query()
                                                    .Filter(c => c.ID == Id)
                                                    .Get()
                                                    .FirstOrDefault();
        }
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            if(bookmark.Category != null)
            {
                GetOrCreateBookmarkCategory(bookmark);
            }

            bookmark.CreateDate = DateTime.Now;
            _unitOfWork.Repository<Bookmark>().Insert(bookmark);
            _unitOfWork.Save();
            return bookmark;
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            if(bookmark.CategoryId == null)
            {
                GetOrCreateBookmarkCategory(bookmark);
            }

            bookmark.CreateDate = DateTime.Now;
            _unitOfWork.Repository<Bookmark>().Update(bookmark);
            _unitOfWork.Save();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _unitOfWork.Repository<Bookmark>().Delete(bookmark);
            _unitOfWork.Save();
        }

        private Bookmark GetOrCreateBookmarkCategory(Bookmark bookmark)
        {
            var category = _categoryService.GetCategory(bookmark.Category.Name);
            if (category != null)
            {
                bookmark.CategoryId = category.ID;
                bookmark.Category = category;
            }
            else
            {
                var newCategory = _categoryService.CreateCategory(bookmark.Category);
                bookmark.Category = newCategory;
                bookmark.CategoryId = newCategory.ID;
            }

            return bookmark;
        }

    }
}

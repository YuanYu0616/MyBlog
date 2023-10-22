using MyBlog.DbAccess;
using MyBlog.Models;
using MyBlog.ViewModels.ArticleService;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Applications.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;

        public ArticleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateArticle(UpdateArticleViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                ArticleContent = model.ArticleContent,
                isDelete = model.IsDelete
            };

            _db.Articles.Add(article);
            await _db.SaveChangesAsync();

        }

        public async Task DeleteArticle(long id)
        {
            var data = await _db.Articles.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (data is not null)
            {
                _db.Articles.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ArticleViewModel> GetArticle(long id)
        {
            return await _db.Articles
                .Where(x => x.isDelete == false && x.Id == id)
                .Select(x => new ArticleViewModel
                {
                    Title = x.Title,
                    ArticleContent = x.ArticleContent,
                    Id = id
                }).FirstOrDefaultAsync() ?? new();
        }

        public async Task<IList<ArticleViewModel>> GetArticleList()
        {
            return await _db.Articles
                .Where(o => o.isDelete == false)
                .Select(o => new ArticleViewModel
                {
                    Title = o.Title,
                    ArticleContent = o.ArticleContent,
                    Id = o.Id
                }).ToListAsync();
        }

        public async Task<UpdateArticleViewModel> GetUpdateArticle(long id)
        {
            var data = await _db.Articles
                .Where(o => o.Id == id)
                .Select(o => new UpdateArticleViewModel
                {
                    Title = o.Title,
                    ArticleContent = o.ArticleContent,
                    Id = o.Id,
                    IsDelete = o.isDelete
                }).FirstOrDefaultAsync();
            return data ?? throw new Exception("Search error");
        }

        public async Task<IList<UpdateArticleViewModel>> GetUpdateArticleList()
        {
            return await _db.Articles
                .Select(o => new UpdateArticleViewModel
                {
                    Title = o.Title,
                    ArticleContent = o.ArticleContent,
                    Id = o.Id,
                    IsDelete = o.isDelete
                }).ToListAsync();
        }

        public async Task UpdateArticle(UpdateArticleViewModel model)
        {
            var data = await _db.Articles.Where(o => o.Id == model.Id).FirstOrDefaultAsync();
            if (data is null)
                throw (new Exception("serch error"));

            data.Title = model.Title;
            data.ArticleContent = model.ArticleContent;
            data.isDelete = model.IsDelete;

            _db.Articles.Update(data);
            await _db.SaveChangesAsync();

        }


        public async Task<ImageUploadResponse> UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null!;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/images",
                fileName);
            using (var stream = File.Create(filePath))
            {
                //程式寫入本地資料夾
                await upload.CopyToAsync(stream);
            }
            var url = $"{"/images/"}{fileName}";
            var result = new ImageUploadResponse
            {
                Uploaded = 1,
                FileName = fileName,
                Url = url,
                Msg = "sucess"
            };

            return result;
        }
    }
}

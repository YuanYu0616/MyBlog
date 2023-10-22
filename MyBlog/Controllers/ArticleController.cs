using MyBlog.Applications.ArticleService;
using MyBlog.ViewModels.ArticleService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MyBlog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _article;
        public ArticleController(IArticleService articleService)
        {
            _article = articleService;
        }

      
        /// <summary>
        /// 取得文章詳細內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Article/{id}")]
        public async Task<IActionResult> Index(long id)
        {
            var model = await _article.GetArticle(id);

            if (model != null)
            {
                return View(model);
            }

            return Redirect("/");
        }

        [Authorize]
        [HttpGet("CreateArticle")]
        public IActionResult CreateArticle()
        {
            return View();
        }

        /// <summary>
        /// 新增文章頁
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateArticle")]
        public async Task<IActionResult> CreateArticle(UpdateArticleViewModel model)
        {
            await _article.CreateArticle(model);
            return Redirect("/");
        }

        /// <summary>
        /// 取得文章列表
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetArticleList")]
        public async Task<IActionResult> GetArticleList()
        {
            var model = await _article.GetUpdateArticleList();
            return View(model);
        }

        /// <summary>
        /// 查詢想要編輯的文章詳細資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("UpdateArticle/{id}")]
        public async Task<IActionResult> UpdateArticle(long id)
        {
            var model = await _article.GetUpdateArticle(id);
            if (model != null)
            {

                return View(model);
            }
            return Redirect("/");
        }

        /// <summary>
        /// 上傳編輯文章內容
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostUpdateArticle( UpdateArticleViewModel model )
        {
            await _article.UpdateArticle(model);
            return RedirectToAction($"GetArticleList");
        }

        /// <summary>
        /// 編輯文章
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public async Task<IActionResult> Uploads(IFormFile upload)
        {
            var obj = await _article.UploadImage(upload);

            return Json(new
            {
                uploaded = obj.Uploaded,
                fileName = obj.FileName,
                url = obj.Url,
                error = new
                {
                    message = obj.Msg
                }
            });
        }

        [Authorize]
        [HttpGet("GetDeleteArticleList")]
        public async Task<IActionResult> GetDeleteArticleList()
        {
            var model = await _article.GetUpdateArticleList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(long id)
        {
            await _article.DeleteArticle(id);
            return RedirectToAction($"GetArticleList");

        }

        
    }
}

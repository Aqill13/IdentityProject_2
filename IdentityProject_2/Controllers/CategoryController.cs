using IdentityProject_2.Data;
using IdentityProject_2.Models;
using IdentityProject_2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject_2.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult IndexCate(string addMessage, string deleteMessage, string updateMessage)
        {
            var categoryList = categoryRepository.GetList();
            ViewBag.AddMessage = addMessage;
            ViewBag.DeleteMessage = deleteMessage;
            ViewBag.UpdateMessage = updateMessage;
            return View(categoryList);
        }

        //add process

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Insert(category);
                return RedirectToAction(nameof(IndexCate), new
                {
                    addMessage = "Category added"
                });
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            ViewBag.Errors = errors;
            return View();
        }

        //delete process
        public IActionResult Delete(int id)
        {
            var removeRow = categoryRepository.Get(id);
            categoryRepository.Delete(removeRow);
            return RedirectToAction(nameof(IndexCate), new
            {
                deleteMessage = $"ID: {id} deleted"
            });
        }

        //update process
        [HttpGet]
        public IActionResult Update(int id)
        {
            var updateRow = categoryRepository.Get(id);
            return View(updateRow);
        }
        [HttpPost]
        public IActionResult Update(Category upCategory)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Update(upCategory);
                return RedirectToAction(nameof(IndexCate), new
                {
                    updateMessage = "Category updated"
                });
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            ViewBag.Errors = errors;
            return View();
        }

        //details process
        public IActionResult Details(int id)
        {
            using var context = new Context();
            var selectProduct = context.Products.Where(x => x.CategoryId == id).ToList();
            var categoryName = context.Categories.Where(x => x.Id == id)
                .Select(x => x.Name).FirstOrDefault();
            ViewBag.cName = categoryName;
            return View(selectProduct);
        }
    }
}

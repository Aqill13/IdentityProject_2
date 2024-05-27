using IdentityProject_2.Data;
using IdentityProject_2.Models;
using IdentityProject_2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityProject_2.Controllers
{
    //[Authorize(Roles = "Aqil")]
    [Authorize]
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        public IActionResult IndexPrdc(string addMessage, string deleteMessage, string updateMessage)
        {
            var productList = productRepository.GetList("Category");
            ViewBag.AddMessage = addMessage;
            ViewBag.DeleteMessage = deleteMessage;
            ViewBag.UpdateMessage = updateMessage;
            return View(productList);
        }

        private void CategoryList()
        {
            using var context = new Context();
            List<SelectListItem> categories = context.Categories.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }).ToList();
            ViewBag.Categories = categories;
        }

        //add process
        [HttpGet]
        public IActionResult AddProduct()
        {
            CategoryList();
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductCopy product)
        {
            Product p = new Product();
            if (product.ImageUrl != null)
            {
                var extension = Path.GetExtension(product.ImageUrl.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures/",newImageName);
                var stream = new FileStream(location, FileMode.Create);
                product.ImageUrl.CopyTo(stream);
                p.ImageUrl = newImageName;
            }
            p.Price = product.Price;
            p.Description = product.Description;
            p.CategoryId = product.CategoryId;
            p.Name = product.Name;
            p.Stock = product.Stock;

            productRepository.Insert(p);
            return RedirectToAction(nameof(IndexPrdc), new
            {
                addMessage = "Product added"
            });
        }

        //delete process
        public IActionResult Delete(int id)
        {
            var deleteRow = productRepository.Get(id);
            productRepository.Delete(deleteRow);
            return RedirectToAction(nameof(IndexPrdc), new
            {
                deleteMessage = $"ID: {id} deleted"
            });
        }

        //update process
        [HttpGet]
        public IActionResult Update(int id)
        {
            CategoryList();
            var updateRow = productRepository.Get(id);
            var img = updateRow.ImageUrl;
            ViewBag.img = img;
            return View(updateRow);
        }
        [HttpPost]
        public IActionResult Update(ProductCopy product)
        {
            var item = productRepository.Get(product.Id);
            item.Stock = product.Stock;
            item.Name = product.Name;
            item.Price = product.Price;
            item.Description = product.Description;
            item.CategoryId = product.CategoryId;
            if (product.ImageUrl != null)
            {
                var extension = Path.GetExtension(product.ImageUrl.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                product.ImageUrl.CopyTo(stream);
                item.ImageUrl = newImageName;
            }
            productRepository.Update(item);
            return RedirectToAction(nameof(IndexPrdc), new
            {
                updateMessage = "Product updated"
            });
        }
    }
}

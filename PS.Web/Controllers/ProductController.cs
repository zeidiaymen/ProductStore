using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PS.Domain;
using PS.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Web.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductService productService;
        readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        // GET: ProductController
        public ActionResult Index(string filter)
        {
            if (!String.IsNullOrEmpty(filter))
                return View(productService.GetMany(p => p.Name.Contains(filter)));
            
            return View(productService.GetAll());
        }

     

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
          
            Product p = productService.GetById(id);
            if (p == null)
                return NotFound();
            return View(p);
        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.mycategories = new SelectList(//source,value,text);
            ViewBag.mycategories = new SelectList(categoryService.GetAll(), "CategoryId", "Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IFormFile file)
        {
            if (file != null)
            {
                product.Image = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads",
                file.FileName);
                using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                productService.Add(product);
                productService.Commit();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productService.Add(product);
                productService.Commit();
                return RedirectToAction(nameof(Index));
            }
            

        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
         
            Product p = productService.GetById(id);
            if (p == null)
                return NotFound();
            ViewBag.mycategories = new SelectList(categoryService.GetAll(), "CategoryId", "Name", p.CategoryId);
            return View(p);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p)
        {
            try
            {
                productService.Update(p);
                productService.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
      

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                productService.Delete(productService.GetById(id));
                productService.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Product p = productService.GetById((int)id);
            if (p == null)
                return NotFound();
            return View(p);
        }
    }
}

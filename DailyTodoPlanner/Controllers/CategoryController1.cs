using DailyTodoPlanner.Data;
using DailyTodoPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyTodoPlanner.Controllers
{
    public class CategoryController1 : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController1( ApplicationDbContext db)
        {
            _context = db; 
        }
        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _context.Categories;
            
            return View(ObjCategoryList);
        }
        //GET
        public IActionResult Create()
        {
        

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                //code to save to database
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Todo created successfully";




                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int ? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);
            //  var categoryFromDbFirst = _context.Categories.FirstOrDefault(u => u.Id == id);
            // var categorFromDbSingle=_context.Categories.SingleOrDefault(u=> u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                //code to save to database
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Todo updated successfully";




                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);
            //  var categoryFromDbFirst = _context.Categories.FirstOrDefault(u => u.Id == id);
            // var categorFromDbSingle=_context.Categories.SingleOrDefault(u=> u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        //POST
        [HttpPost ,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int ? id)
        {
            var obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
                //code to save to database
                _context.Categories.Remove(obj);
                _context.SaveChanges();
            TempData["success"] = "Todo deleted successfully";
            return RedirectToAction("Index");







        }
    }
}

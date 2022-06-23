using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
namespace BulkyBookWeb.Controllers;
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController (ApplicationDbContext db){
        _db = db;
    }
    public IActionResult Index ()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        return View(objCategoryList);
    }
    //GET
    public IActionResult Create ()
    {
        return View();
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create (Category objCategory){
        if(objCategory.Name == objCategory.DisplayerOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }
        if(ModelState.IsValid)
        {
            _db.Categories.Add(objCategory);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(objCategory);
    }
    //GET
    public IActionResult Edit (int id){
        if(id == 0){
            return NotFound();
        }
        Category ? cat = _db.Categories.Find(id);
        if(cat == null){
            return NotFound();
        }
        return View(cat);
    }
    //POST
    [HttpPost]
    public IActionResult Edit(Category cattoupdate){
        _db.Categories.Update(cattoupdate);
        _db.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    //GET
    public IActionResult ? Delete (int id){
        if (id == 0){
            return NotFound();
        }
        Category ? cat = _db.Categories.Find(id);
        if(cat == null){
            return NotFound();
        }
        return View(cat);
    }
    //POST
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost (Category cattodelete){
        _db.Categories.Remove(cattodelete);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
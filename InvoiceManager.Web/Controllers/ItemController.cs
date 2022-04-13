using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using InvoiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IRepository _db;

        public ItemController(IRepository db) => _db = db;

        // GET: Item
        public IActionResult Index() => View(_db.GetItems());

        // GET: Item/Create
        public IActionResult Create() => View(new CreateItemViewModel());

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateItemViewModel createItemViewModel)
        {
            if (ModelState.IsValid)
            {
                decimal totalPrice = createItemViewModel.Amount * createItemViewModel.Price;

                Item item = new Item()
                {
                    Name = createItemViewModel.Name,
                    Description = createItemViewModel.Description,
                    Amount = createItemViewModel.Amount,
                    Price = createItemViewModel.Price,
                    TotalPrice = totalPrice
                };

                _db.CreateItem(item);

                return RedirectToAction(nameof(Index));
            }
            return View(createItemViewModel);
        }

        // GET: Item/Edit/1
        public IActionResult Edit(int id) => View(new EditItemViewModel(_db.GetItem(id)));

        // POST: Item/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditItemViewModel editItemViewModel)
        {
            if (ModelState.IsValid)
            {
                decimal totalPrice = editItemViewModel.Amount * editItemViewModel.Price;

                Item item = _db.GetItem(editItemViewModel.Id);

                item.Name = editItemViewModel.Name;
                item.Description = editItemViewModel.Description;
                item.Amount = editItemViewModel.Amount;
                item.Price = editItemViewModel.Price;
                item.TotalPrice = totalPrice;

                try
                {
                    _db.UpdateItem(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_db.ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editItemViewModel);
        }

        // GET: Item/Delete/1
        public IActionResult Delete(int id) => View(_db.GetItem(id));

        // POST: Item/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.RemoveItem(_db.GetItem(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
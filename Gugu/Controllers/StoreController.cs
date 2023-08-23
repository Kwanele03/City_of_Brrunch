﻿using Gugu.Data.Services;
using Gugu.Data.Static;
using Gugu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Gugu.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class StoreController : Controller
    {
      
            private readonly IStoreService _service;

            public StoreController(IStoreService service)
            {
                _service = service;
            }

            [AllowAnonymous]
            public async Task<IActionResult> Index()
            {
                var allStores = await _service.GetAllAsync();
                return View(allStores);
            }


            ////Get: Cinemas/Create
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create([Bind("Logo,Name,Address, EmailAdress, ContactsDetails")] Store store)
            {
                if (!ModelState.IsValid) return View(store);
                await _service.AddAsync(store);
                return RedirectToAction(nameof(Index));
            }

            //Get: Cinemas/Details/1
            [AllowAnonymous]
            public async Task<IActionResult> Details(int id)
            {
                var storeDetails = await _service.GetByIdAsync(id);
                if (storeDetails == null) return View("NotFound");
                return View(storeDetails);
            }

            //Get: Cinemas/Edit/1
            public async Task<IActionResult> Edit(int id)
            {
                var storeDetails = await _service.GetByIdAsync(id);
                if (storeDetails == null) return View("NotFound");
                return View(storeDetails);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Store store)
            {
                if (!ModelState.IsValid) return View(store);
                await _service.UpdateAsync(id, store);
                return RedirectToAction(nameof(Index));
            }

            ////Get: Cinemas/Delete/1
            //public async Task<IActionResult> Delete(int id)
            //{
            //    var storeDetails = await _service.GetByIdAsync(id);
            //    if (storeDetails == null) return View("NotFound");
            //    return View(storeDetails);
            //}

            //[HttpPost, ActionName("Delete")]
            //public async Task<IActionResult> DeleteConfirm(int id)
            //{
            //    var storeDetails = await _service.GetByIdAsync(id);
            //    if (storeDetails == null) return View("NotFound");

            //    await _service.DeleteAsync(id);
            //    return RedirectToAction(nameof(Index));
            //}
        }
    }


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using DogGo.Models;
using DogGo.Controllers;
using System.Collections.Generic;
using DogGo.Models.ViewModels;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        //private readonly IOwnerRepository _ownerRepo;
        private readonly IWalkerRepository _walkerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository)
        {
            _walkerRepo = walkerRepository;
        }

        //GET: Walkers
        // GET: Walkers
        public ActionResult Index()
        {

            int currUserId = 0;
            List<Walker> walkers = new List<Walker>();

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
            {
                currUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }


            if (currUserId != 0)
            {
                walkers = _walkerRepo.GetAllWalkersInHoodByOwnerId(currUserId);
            }
            else
            {
                walkers = _walkerRepo.GetAllWalkers();
            }

            return View(walkers);
        }


        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            { return -1; }
            else
            {
                return int.Parse(id);
            }
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

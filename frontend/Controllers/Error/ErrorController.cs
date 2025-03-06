﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_MVC.Controllers.Error
{
    //public class ErrorrController : Controller
    //{
    //    [Route("Error/{statusCode}")]
    //    public IActionResult HttpStatusCodeHandler(int statusCode)
    //    {
    //        switch (statusCode)
    //        {
    //            case 404:
    //                ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
    //                return View("NotFound");
    //            default:
    //                ViewBag.ErrorMessage = "An unexpected error occurred.";
    //                return View("Error");
    //        }
    //    }
    //}

    public class ErrorController : Controller
    {
        // GET: ErrorController
        public ActionResult Error401()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }










        //----------------------------------    template   ----------------------


        // GET: ErrorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ErrorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ErrorController/Create
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

        // GET: ErrorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ErrorController/Edit/5
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

        // GET: ErrorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ErrorController/Delete/5
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

using CascadingDropDown.Models;
using CascadingDropDown.Repository;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace CascadingDropDown.Controllers
{
    public class CascadingController : Controller
    {
        private readonly IDbConnection db;
        private readonly ICascade _data;
        

        public CascadingController(ICascade cascade, IConfiguration configuration)
        {
            _data = cascade;
            this.db = new SqlConnection(configuration.GetConnectionString("DBConn"));
        }
        public IActionResult Index()
        {
            try
            {
                List<Data> list = _data.GetAll();
                return View(list);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Data data)
        {
           _data.Add(data);
           return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           Data data = _data.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Data data)
        {
            _data.Update(data);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Data data = _data.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _data.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

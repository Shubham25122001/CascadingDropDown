using CascadingDropDown.Models;
using CascadingDropDown.Repository;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CascadingDropDown.Controllers
{
    public class CascadingController : Controller
    {
        private readonly IDbConnection db;
        private readonly ICascade _data;

        /// <summary>
        /// Constructor of CascadingController Class
        /// </summary>
        /// <param name="cascade">Instance of ICascade</param>
        /// <param name="configuration">Instance of IConfiguration</param>
        public CascadingController(ICascade cascade, IConfiguration configuration)
        {
            _data = cascade;
            this.db = new SqlConnection(configuration.GetConnectionString("DBConn"));
        }

        /// <summary>
        /// Index Action method
        /// </summary>
        /// <returns>List as a view</returns>
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

        /// <summary>
        /// Create Action Method
        /// </summary>
        /// <returns>Create View</returns>
        public IActionResult Create()
        {
            List<Country> Countries = db.Query<Country>("Select * from SHUBHAM_Country").ToList();
            ViewBag.Countries = new SelectList(Countries, "CountryId", "CountryName");
            return View();
        }


        /// <summary>
        /// Get the States By using CountryId
        /// </summary>
        /// <param name="countryId">Country id of Country</param>
        /// <returns>States in json formate</returns>
        
        public ActionResult GetStateList(int countryId)
        {
            string query = "SELECT StateId, StateName FROM SHUBHAM_State WHERE CountryId = @CountryId";
            List<State> selectList = db.Query<State>(query, new { CountryId = countryId }).ToList();
            ViewBag.Slist = new SelectList(selectList, "StateId", "StateName");
            return PartialView("DisplayStates");
        }


        /// <summary>
        /// Get the City List
        /// </summary>
        /// <param name="stateId">Primary Key of The City</param>
        /// <returns>Partial View of DisplayCities</returns>
        public ActionResult GetCityList(int stateId)
        {
            string query = "SELECT CityId, CityName FROM SHUBHAM_City WHERE StateId = @StateId";
            List<City> selectList= db.Query<City>(query, new { StateId = stateId }).ToList();
            ViewBag.CityList = new SelectList(selectList, "CityId", "CityName");
            return PartialView("DisplayCities");
        }



        /// <summary>
        /// Save the record 
        /// </summary>
        /// <param name="data">Instance of Data model class</param>
        /// <returns>Redirect to the Action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Data data)
        {
           _data.Add(data);
           return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// Edit the Record
        /// </summary>
        /// <param name="id">Primary key of the Record</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Country> Countries = db.Query<Country>("Select * from SHUBHAM_Country").ToList();
            ViewBag.Countries = new SelectList(Countries, "CountryId", "CountryName");
            Data data = _data.GetById(id);
            return View(data);
        }

        /// <summary>
        /// Update the Data to the Database
        /// </summary>
        /// <param name="data">Instance of Data Model</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Data data)
        {
            _data.Update(data);
            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        /// Show the view for the Deletion of the Record
        /// </summary>
        /// <param name="id">Primary key of the record</param>
        /// <returns>Data as a view</returns>
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


        /// <summary>
        /// Confirm Deletion of the record
        /// </summary>
        /// <param name="id">Priamry key of the Record</param>
        /// <returns>Redirect to the Index</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _data.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

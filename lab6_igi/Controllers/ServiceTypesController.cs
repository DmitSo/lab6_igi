using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab1_ef;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lab6_igi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        HotelContext db;
        public ValuesController(HotelContext context)
        {
            this.db = context;
            if (!db.ServiceTypes.Any())            
                Initializer.Initialize(db);            
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ServiceType> Get()
        {
            return db.ServiceTypes.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ServiceType serviceType = db.ServiceTypes.FirstOrDefault(x => x.ServiceTypeId == id);
            if (serviceType == null)
                return NotFound();
            return new ObjectResult(serviceType);
        }

        // POST api/serviceTypes
        [HttpPost]
        public IActionResult Post([FromBody]ServiceType serviceType)
        {
            if (serviceType == null)
            {
                return BadRequest();
            }

            db.ServiceTypes.Add(serviceType);
            db.SaveChanges();
            return Ok(serviceType);
        }

        // PUT api/serviceTypes/
        [HttpPut]
        public IActionResult Put([FromBody]ServiceType serviceType)
        {
            if (serviceType == null)
            {
                return BadRequest();
            }
            if (!db.ServiceTypes.Any(x => x.ServiceTypeId == serviceType.ServiceTypeId))
            {
                return NotFound();
            }

            db.Update(serviceType);
            db.SaveChanges();
            return Ok(serviceType);
        }

        // DELETE api/serviceTypes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ServiceType serviceType = db.ServiceTypes.FirstOrDefault(x => x.ServiceTypeId == id);//Include(c => c.Room).
            if (serviceType == null)
            {
                return NotFound();
            }
            db.ServiceTypes.Remove(serviceType);
            db.SaveChanges();
            return Ok(serviceType);
        }
    }
}

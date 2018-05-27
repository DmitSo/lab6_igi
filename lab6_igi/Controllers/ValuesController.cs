using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab1_ef;
using Microsoft.AspNetCore.Mvc;
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
            if (!db.Clients.Any())
                Initializer.Initialize(db);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            var t = db.Clients.Include(o => o.Room).Select(o => new Client
            {
                ClientId = o.ClientId,
                Name = o.Name,
                Passport = o.Passport,
                DepartureDate = o.DepartureDate,
                OccupancyDate = o.OccupancyDate,
                RoomId = o.RoomId,
                Room = new Room()
                {
                    Capacity = o.Room.Capacity,
                    Cost = o.Room.Cost,
                    CostDate = o.Room.CostDate,
                    Description = o.Room.Description,
                    RoomNo = o.Room.RoomNo,
                    RoomId = o.Room.RoomId,
                    RoomTypeId = o.Room.RoomTypeId
                }
            }).ToList();
            return t;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Client client = db.Clients.Include(o => o.Room).FirstOrDefault(x => x.ClientId == id);
            client.Room.Clients = null;
            client.Room.RoomType = null;
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }

        // POST api/clients
        [HttpPost]
        public IActionResult Post([FromBody]Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            db.Clients.Add(client);
            db.SaveChanges();
            return Ok(client);
        }

        // PUT api/clients/
        [HttpPut]
        public IActionResult Put([FromBody]Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            if (!db.Clients.Any(x => x.ClientId == client.ClientId))
            {
                return NotFound();
            }

            db.Update(client);
            db.SaveChanges();
            return Ok(client);
        }

        // DELETE api/clients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Client client = db.Clients.FirstOrDefault(x => x.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            db.Clients.Remove(client);
            db.SaveChanges();
            return Ok(client);
        }
    }
}

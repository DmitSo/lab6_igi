using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab1_ef;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lab6_igi.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        HotelContext db;
        public RoomsController(HotelContext context)
        {
            this.db = context;
            if (!db.Rooms.Any())
                Initializer.Initialize(db);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return db.Rooms.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Room room = db.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room == null)
                return NotFound();
            return new ObjectResult(room);
        }

        // POST api/rooms
        [HttpPost]
        public IActionResult Post([FromBody]Room room)
        {
            if (room == null)
            {
                return BadRequest();
            }

            db.Rooms.Add(room);
            db.SaveChanges();
            return Ok(room);
        }

        // PUT api/rooms/
        [HttpPut]
        public IActionResult Put([FromBody]Room room)
        {
            if (room == null)
            {
                return BadRequest();
            }
            if (!db.Rooms.Any(x => x.RoomId == room.RoomId))
            {
                return NotFound();
            }

            db.Update(room);
            db.SaveChanges();
            return Ok(room);
        }

        // DELETE api/rooms/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Room room = db.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }
            db.Rooms.Remove(room);
            db.SaveChanges();
            return Ok(room);
        }
    }
}

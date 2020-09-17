using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FishyGame.Data;
using FishyGame.Models;
using System.Security.Cryptography;
using System.Text;

namespace FishyGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishController : ControllerBase
    {
        private readonly FishContext _context;

        public FishController(FishContext context)
        {
            _context = context;
        }

        // GET: api/Fish
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fish>>> GetFish()
        {
            var fishes = await _context.Fish.ToListAsync();

            var len = fishes.Count;

            for(int i = 0; i < len; i++)
            {
                fishes[i].del = null;
            }

            return fishes;
        }

        // GET: api/Fish/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fish>> GetFish(int id)
        {
            var fish = await _context.Fish.FindAsync(id);

            if (fish == null)
            {
                return NotFound();
            }

            fish.del = null;

            return fish;
        }

        // PUT: api/Fish/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFish(int id, Fish fish)
        {
            if (id != fish.fishID)
            {
                return BadRequest();
            }

            var sha1 = new SHA1CryptoServiceProvider();
            ASCIIEncoding ascii = new ASCIIEncoding();

            var del = fish.del;

            if(del == null)
            {
                del = "";
            }

            var data = Encoding.ASCII.GetBytes(del);
            var sha1data = sha1.ComputeHash(data);

            var hashed = ascii.GetString(sha1data);

            var original = await _context.Fish.FindAsync(id);

            if (original == null)
            {
                return NotFound();
            }

            var password = original.del;

            if (password == null)
            {
                var emptyStr = Encoding.ASCII.GetBytes("");
                var emptyHash = sha1.ComputeHash(emptyStr);
                password = ascii.GetString(emptyHash);
            }

            if (password == hashed)
            {
                _context.Fish.Remove(original);
                await _context.SaveChangesAsync();
                return NoContent();
            } else
            {
                return BadRequest();
            }
            // ignore rest of method
            _context.Entry(fish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fish
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fish>> PostFish(Fish fish)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            ASCIIEncoding ascii = new ASCIIEncoding();

            var del = fish.del;

            var data = Encoding.ASCII.GetBytes(del);
            var sha1data = sha1.ComputeHash(data);

            var hashed = ascii.GetString(sha1data);

            fish.del = hashed;

            _context.Fish.Add(fish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFish", new { id = fish.fishID }, fish);
        }

        // DELETE: api/Fish/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fish>> DeleteFish(int id)
        {
            return Forbid();

            // forbidden method
            var fish = await _context.Fish.FindAsync(id);
            if (fish == null)
            {
                return NotFound();
            }

            _context.Fish.Remove(fish);
            await _context.SaveChangesAsync();

            return fish;
        }

        private bool FishExists(int id)
        {
            return _context.Fish.Any(e => e.fishID == id);
        }
    }
}

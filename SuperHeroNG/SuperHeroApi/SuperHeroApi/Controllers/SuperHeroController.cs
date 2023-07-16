using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Data;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> GetSuperHeroes()
        {
            return Ok(await _context.Superheros.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Superhero>>> CreateSuperHero(Superhero superhero)
        {
            _context.Superheros.Add(superhero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.Superheros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Superhero>>> UpdateSuperHero(Superhero superhero)
        {
            var dbHero = await _context.Superheros.FindAsync(superhero.Id);
            if(dbHero == null)
            {
                return NotFound();
            }
            dbHero.Name = superhero.Name;
            dbHero.FirstName = superhero.FirstName;
            dbHero.LastName = superhero.LastName;
            dbHero.Place = superhero.Place;
            await _context.SaveChangesAsync();
            
            return Ok(await _context.Superheros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Superhero>>> DeleteSuperhero(int id)
        {
            var dbHero = await _context.Superheros.FindAsync(id);
            if(dbHero == null)
            {
                return NotFound();
            }
            _context.Superheros.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Superheros.ToListAsync());
        }

    }
}

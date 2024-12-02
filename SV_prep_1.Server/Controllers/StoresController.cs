using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SV_prep_1.Server.Data;
using SV_prep_1.Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV_prep_1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ILogger<StoresController> _logger;

        public StoresController(AppDbContext context, ILogger<StoresController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: api/<StoresController>
        [HttpGet]
        public async Task<IEnumerable<StoreDto>> GetStores()
        {
            var stores = await _context.Stores
               .Include(s => s.Address) // Eagerly load the associated Address
               .ToListAsync();

            _logger.LogInformation("Retrieved stores: {@Stores}", stores);

            var storeDtos = new List<StoreDto>();
            stores.ForEach((store) =>
            {
                var storeDto = new StoreDto();
                storeDto.Id = store.Id;
                storeDto.Name = store.Name;
                storeDto.FormatedAddress = store.Address.City + ',' + store.Address.Street + "," + store.Address.HouseNumber + "," + store.Address.PostalCode;
                Console.WriteLine(storeDto);
                storeDtos.Add(storeDto);
            });

            return storeDtos;
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StoresController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

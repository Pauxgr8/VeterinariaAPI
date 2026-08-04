using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Models;
using VeterinariaAPI.Repositories;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly MascotaRepository _repository;

        public MascotasController(MascotaRepository repository)
        {
            _repository = repository;
        }

        //=================================
        // LEER TODAS
        //=================================
        [HttpGet]
        public IActionResult Get()
        {
            var mascotas = _repository.LeerMascotas();
            return Ok(mascotas);
        }

        //=================================
        // BUSCAR POR ID
        //=================================
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var mascota = _repository.BuscarPorId(id);

            if (mascota == null)
                return NotFound();

            return Ok(mascota);
        }

        //=================================
        // CREAR
        //=================================
        [HttpPost]
        public IActionResult Post([FromBody] Mascota mascota)
        {
            var id = _repository.CrearMascota(mascota);
            return Ok(id);
        }

        //=================================
        // MODIFICAR
        //=================================
        [HttpPut]
        public IActionResult Put([FromBody] Mascota mascota)
        {
            _repository.ModificarMascota(mascota);
            return Ok();
        }

        //=================================
        // ELIMINAR
        //=================================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.EliminarMascota(id);
            return Ok();
        }
    }
}
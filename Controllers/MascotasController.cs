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
            try
            {
                var mascotas = _repository.LeerMascotas();
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=================================
        // BUSCAR POR ID
        //=================================
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var mascota = _repository.BuscarPorId(id);

                if (mascota == null)
                    return NotFound();

                return Ok(mascota);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=================================
        // CREAR
        //=================================
        [HttpPost]
        public IActionResult Post([FromBody] Mascota mascota)
        {
            try
            {
                var id = _repository.CrearMascota(mascota);
                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=================================
        // MODIFICAR
        //=================================
        [HttpPut]
        public IActionResult Put([FromBody] Mascota mascota)
        {
            try
            {
                _repository.ModificarMascota(mascota);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=================================
        // ELIMINAR
        //=================================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.EliminarMascota(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Models;
using VeterinariaAPI.Repositories;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropietariosController : ControllerBase
    {
        private readonly PropietarioRepository _repository;

        public PropietariosController(PropietarioRepository repository)
        {
            _repository = repository;
        }

        //=========================================
        // LEER TODOS
        //=========================================

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var propietarios = _repository.LeerPropietarios();
                return Ok(propietarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=========================================
        // BUSCAR POR ID
        //=========================================

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var propietario = _repository.BuscarPorId(id);

                if (propietario == null)
                    return NotFound();

                return Ok(propietario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=========================================
        // INSERTAR
        //=========================================

        [HttpPost]
        public IActionResult Post([FromBody] Propietario propietario)
        {
            try
            {
                var id = _repository.CrearPropietario(propietario);

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=========================================
        // MODIFICAR
        //=========================================

        [HttpPut]
        public IActionResult Put([FromBody] Propietario propietario)
        {
            try
            {
                _repository.ModificarPropietario(propietario);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //=========================================
        // ELIMINAR
        //=========================================

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.EliminarPropietario(id);

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
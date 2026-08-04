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
            var propietarios = _repository.LeerPropietarios();
            return Ok(propietarios);
        }

        //=========================================
        // BUSCAR POR ID
        //=========================================

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var propietario = _repository.BuscarPorId(id);

            if (propietario == null)
                return NotFound();

            return Ok(propietario);
        }

        //=========================================
        // INSERTAR
        //=========================================

        [HttpPost]
        public IActionResult Post([FromBody] Propietario propietario)
        {
            var id = _repository.CrearPropietario(propietario);

            return Ok(id);
        }

        //=========================================
        // MODIFICAR
        //=========================================

        [HttpPut]
        public IActionResult Put([FromBody] Propietario propietario)
        {
            _repository.ModificarPropietario(propietario);

            return Ok();
        }

        //=========================================
        // ELIMINAR
        //=========================================

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.EliminarPropietario(id);

            return Ok();
        }
    }
}
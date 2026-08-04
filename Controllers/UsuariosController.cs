using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Models;
using VeterinariaAPI.Repositories;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuariosController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        // =========================================
        // LEER TODOS
        // GET: api/Usuarios
        // =========================================
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _repository.LeerUsuarios();
            return Ok(usuarios);
        }

        // =========================================
        // BUSCAR POR ID
        // GET: api/Usuarios/1
        // =========================================
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usuario = _repository.BuscarPorId(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // =========================================
        // CREAR
        // POST: api/Usuarios
        // =========================================
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            int id = _repository.CrearUsuario(usuario);

            usuario.Id = id;

            return CreatedAtAction(nameof(Get), new { id = id }, usuario);
        }

        // =========================================
        // MODIFICAR
        // PUT: api/Usuarios/1
        // =========================================
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            usuario.Id = id;

            _repository.ModificarUsuario(usuario);

            return NoContent();
        }

        // =========================================
        // ELIMINAR
        // DELETE: api/Usuarios/1
        // =========================================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.EliminarUsuario(id);

            return NoContent();
        }
    }
}
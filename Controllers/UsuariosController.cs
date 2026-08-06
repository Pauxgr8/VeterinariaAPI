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
            try
            {
                var usuarios = _repository.LeerUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        // =========================================
        // BUSCAR POR ID
        // GET: api/Usuarios/1
        // =========================================
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var usuario = _repository.BuscarPorId(id);

                if (usuario == null)
                    return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        // =========================================
        // CREAR
        // POST: api/Usuarios
        // =========================================
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                int id = _repository.CrearUsuario(usuario);

                usuario.Id = id;

                return CreatedAtAction(nameof(Get), new { id = id }, usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        // =========================================
        // MODIFICAR
        // PUT: api/Usuarios/1
        // =========================================
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                usuario.Id = id;

                _repository.ModificarUsuario(usuario);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        // =========================================
        // ELIMINAR
        // DELETE: api/Usuarios/1
        // =========================================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.EliminarUsuario(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        // =========================================
        // LOGIN
        // POST: api/Usuarios/Login
        // =========================================
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login request)
        {
            try
            {
                var usuario = _repository.Autenticar(request.Usuario, request.Clave);

                if (usuario == null)
                    return Unauthorized(new { mensaje = "Usuario o clave incorrectos" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }
    }
}
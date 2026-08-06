using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Models;
using VeterinariaAPI.Repositories;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HojaClinicaController : ControllerBase
    {
        private readonly HojaClinicaRepository _repository;

        public HojaClinicaController(HojaClinicaRepository repository)
        {
            _repository = repository;
        }

        //==========================
        // LEER TODOS
        //==========================
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.LeerHojaClinica());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //==========================
        // BUSCAR POR ID
        //==========================
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var hoja = _repository.BuscarPorId(id);

                if (hoja == null)
                    return NotFound();

                return Ok(hoja);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //==========================
        // INSERTAR
        //==========================
        [HttpPost]
        public IActionResult Post(HojaClinica hoja)
        {
            try
            {
                int id = _repository.Crear(hoja);

                hoja.Id = id;

                return Ok(hoja);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //==========================
        // MODIFICAR
        //==========================
        [HttpPut]
        public IActionResult Put(HojaClinica hoja)
        {
            try
            {
                _repository.Modificar(hoja);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        //==========================
        // ELIMINAR
        //==========================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Eliminar(id);

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
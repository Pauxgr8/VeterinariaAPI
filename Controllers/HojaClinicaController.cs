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
            return Ok(_repository.LeerHojaClinica());
        }

        //==========================
        // BUSCAR POR ID
        //==========================
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var hoja = _repository.BuscarPorId(id);

            if (hoja == null)
                return NotFound();

            return Ok(hoja);
        }

        //==========================
        // INSERTAR
        //==========================
        [HttpPost]
        public IActionResult Post(HojaClinica hoja)
        {
            int id = _repository.Crear(hoja);

            hoja.Id = id;

            return Ok(hoja);
        }

        //==========================
        // MODIFICAR
        //==========================
        [HttpPut]
        public IActionResult Put(HojaClinica hoja)
        {
            _repository.Modificar(hoja);

            return Ok();
        }

        //==========================
        // ELIMINAR
        //==========================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Eliminar(id);

            return Ok();
        }
    }
}
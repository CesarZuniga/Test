using Microsoft.AspNetCore.Mvc;
using System.Net;
using test.api.Models;
using test.api.Repositories;

namespace test.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : Controller
    {

        /// <summary>
        /// interface movimientos repository
        /// </summary>
        private IMovimientosRepository _repository;

        public MovimientosController(IMovimientosRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        /// <summary>
        /// Get all movimientos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MovimientosBO>> Get()
        {
            //var helper to return result
            IEnumerable<MovimientosBO> result; ;
            try
            {
                // get all movimientos
                result = _repository.ReadsItems();
            }
            catch (Exception ex)
            {
                // Analize exception type and prepared custom exception to return
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex.Message);
            }

            return Ok(result);
        }

        // GET api/values/xxx--xxxx-xxx-xxxx
        /// <summary>
        /// Get movimiento by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{idCuenta}/{idTipo}")]
        public ActionResult<IEnumerable<MovimientosBO>> Get(int idCuenta, int idTipo = 0)
        {
            //var helper to return result
            IEnumerable<MovimientosBO> result; ;
            try
            {
                if (idTipo == 0)
                {
                    result = _repository.ReadsItems(x => x.CuentaId == idCuenta);
                }
                else {
                    result = _repository.ReadsItems(x => x.CuentaId == idCuenta && x.TipoId == idTipo);
                }
                
            }
            catch (Exception ex)
            {
                // Analize exception type and prepared custom exception to return
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex.Message);
            }

            return Ok(result);
        }

        /// <summary>
        /// create movimiento
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(Movimientos))]
        public async Task<ActionResult<Movimientos>> Post([FromBody] Movimientos json)
        {
            //var helper to return result
            Movimientos result = null;
            try
            {
                result = await _repository.CreateItemAsync(json);
            }
            catch (Exception ex)
            {
                // Analize exception type and prepared custom exception to return
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex.Message);
            }

            return Ok(result);
        }

    }
}

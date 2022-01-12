using Microsoft.AspNetCore.Mvc;
using System.Net;
using test.api.Models;
using test.api.Repositories;

namespace test.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : Controller
    {

        /// <summary>
        /// interface cuentas repository
        /// </summary>
        private ICuentasRepository _repository;

        public CuentasController(ICuentasRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        /// <summary>
        /// Get all cuentas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CuentasBO>> Get()
        {
            //var helper to return result
            IEnumerable<Cuentas> result; ;
            try
            {
                // get all cuentas
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
        /// Get cuenta by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{idCliente}")]
        public ActionResult<IEnumerable<CuentasBO>> Get(int idCliente)
        {
            //var helper to return result
            IEnumerable<Cuentas> result; ;
            try
            {
                // get all cuentas
                result = _repository.ReadsItems(x=> x.ClienteId == idCliente);
            }
            catch (Exception ex)
            {
                // Analize exception type and prepared custom exception to return
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex.Message);
            }

            return Ok(result);
        }

        /// <summary>
        /// create cuenta
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(Cuentas))]
        public async Task<ActionResult<Cuentas>> Post([FromBody] Cuentas json)
        {
            //var helper to return result
            Cuentas result = null;
            try
            {
                // check if exist cuenta
                bool exist = _repository.ReadsItems().Any(x => x.ClienteId == json.ClienteId && x.NumeroCuenta.Equals(json.NumeroCuenta));
                if (exist)
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
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

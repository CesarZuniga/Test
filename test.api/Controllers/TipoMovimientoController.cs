using Microsoft.AspNetCore.Mvc;
using System.Net;
using test.api.Models;
using test.api.Repositories;

namespace test.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovimientoController : Controller
    {

        /// <summary>
        /// interface tipoMovimiento repository
        /// </summary>
        private ITipoMovimientoRepository _repository;

        public TipoMovimientoController(ITipoMovimientoRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        /// <summary>
        /// Get all tipoMovimiento
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<TipoMovimiento>> Get()
        {
            //var helper to return result
            IEnumerable<TipoMovimiento> result; ;
            try
            {
                // get all tipoMovimiento
                result = _repository.ReadsItems();
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

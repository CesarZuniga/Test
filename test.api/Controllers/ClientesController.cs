using Microsoft.AspNetCore.Mvc;
using System.Net;
using test.api.Models;
using test.api.Repositories;

namespace test.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {

        /// <summary>
        /// interface clientes repository
        /// </summary>
        private IClientesRepository _repository;

        public ClientesController(IClientesRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        /// <summary>
        /// Get all clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Clientes>> Get()
        {
            //var helper to return result
            IEnumerable<Clientes> result; ;
            try
            {
                // get all clientes
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
        /// Get cliente by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Clientes> Get(int id)
        {
            //var helper to return result
            Clientes result = new Clientes();
            try
            {
                // get cliente by id
                result = _repository.ReadsItems(x => x.ClienteId == id).FirstOrDefault() ?? result;
            }
            catch (Exception ex)
            {
                // Analize exception type and prepared custom exception to return
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex.Message);
            }

            return Ok(result);
        }

        /// <summary>
        /// create cliente
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(Clientes))]
        public async Task<ActionResult<Clientes>> Post([FromBody] Clientes json)
        {
            //var helper to return result
            Clientes result = null;
            try
            {
                
                // check if exist cliente
                bool exist = _repository.ReadsItems().Any(x => x.Nombre.Equals(json.Nombre) && x.NumeroIdentificacion.Equals(json.NumeroIdentificacion));
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

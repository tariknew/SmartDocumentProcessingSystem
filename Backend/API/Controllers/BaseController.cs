using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TEntity, TSearch, TInsert, TUpdate> : ControllerBase where TEntity : class where TSearch : class where TInsert : class where TUpdate : class
    {
        protected readonly IBaseService<TEntity, TSearch, TInsert, TUpdate> _service;

        public BaseController(IBaseService<TEntity, TSearch, TInsert, TUpdate> service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<TEntity> Get([FromQuery] TSearch search = null)
        {
            return _service.Get(search);
        }
        [HttpPost]
        public virtual TEntity Insert([FromBody] TInsert request)
        {
            return _service.Insert(request);
        }
        [HttpGet("{id}")]
        public TEntity GetById(int id)
        {
            return _service.GetById(id);
        }
        [HttpDelete("{id}")]
        public IEnumerable<TEntity> Delete(int id)
        {
            return _service.Delete(id);
        }
        [HttpPut("{id}")]
        virtual public TEntity Update(int id, [FromBody] TUpdate request)
        {
            return _service.Update(id, request);
        }
    }
}

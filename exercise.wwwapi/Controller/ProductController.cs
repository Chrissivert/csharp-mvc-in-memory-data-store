using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(){
            try{
                var products = _repository.GetAll();
                return Ok(products); 
            }
            catch (ProductExceptions.NoProductException ex){
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("category/{category}")]
        public IActionResult GetAllByCategory(string category){
            try{
                var products = _repository.GetAllByCategory(category);
                return Ok(products);
            }
            catch (ProductExceptions.NoProductException ex){
                return BadRequest(ex.Message);
            }
            catch (ProductExceptions.NoProductWithCategory ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            try{
                var product = _repository.GetById(id);
                return Ok(product); 
            }
            catch (ProductExceptions.NoProductException ex){
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            try
            {
                var created = _repository.Create(product);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ProductExceptions.DuplicateNameException ex){
                return BadRequest(ex.Message);
            }
            catch (ProductExceptions.InvalidPriceException ex){
                return BadRequest(ex.Message);
            } //I dont know if this is best practice?
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updated){
            try{
                var product = _repository.Update(id, updated);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product); 
            }
            catch (ProductExceptions.NoProductException ex){
                return BadRequest(ex.Message);
            }
            catch (ProductExceptions.DuplicateNameException ex){
                return BadRequest(ex.Message);
            }
            catch (ProductExceptions.InvalidPriceException ex){
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            try{
                var deleted = _repository.Delete(id);
                return Ok(deleted); 
            }
            catch (ProductExceptions.NoProductException ex){
                return BadRequest(ex.Message);

            }
        }
    }
}
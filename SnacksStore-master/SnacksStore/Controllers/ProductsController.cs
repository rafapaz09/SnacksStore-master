using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using SnacksStore.Helpers;

namespace SnacksStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPriceLogRepository _productPriceLogRepository;

        public ProductsController(IProductRepository productRepository, IProductPriceLogRepository productPriceLogRepository)
        {
            _productRepository = productRepository;
            _productPriceLogRepository = productPriceLogRepository;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetProducts([FromQuery]int? draw, [FromQuery] int? start, [FromQuery] int? length, [FromQuery] string search, [FromQuery] int? page, [FromQuery] string order = "desc")
        {

            search = search ?? Request.Query["search[value]"].ToString();
            order = Request.Query["order[0][dir]"].ToString() ?? order;
            length = length ?? 10;
            var totalRecords = 0;
            var recordsFiltered = 0;
            start = start.HasValue ? start / length : 0;
            start = page.HasValue ? page - 1: start;

            try
            {
                var list = _productRepository.GetPaginated(search,start.Value, length.Value, order, out totalRecords, out recordsFiltered);

                return new JsonResult(
                    new { draw , start, length, totalRecords, recordsFiltered, data = list }
                    );
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/5
        [HttpGet("{id}/pricehistory")]
        [Authorize(Roles = "Admin")]

        public IEnumerable<Product> GetProductPriceHistory(int id)
        {
            var product = _productRepository.GetByIdWithProductPriceLog(id);

            return product;
        }

        // POST: api/Products
        [HttpPost]
        [Authorize(Roles ="Admin")]

        public ActionResult<Product> PostProduct(Product product)
        {
            product.Sku =  product.Sku ?? Guid.NewGuid().ToString();
            product.Active = product.Active ?? true;
            product.CreatedAt = DateTime.Now;
            product.CreatedBy = int.Parse(User.Identity.Name);

            _productRepository.Create(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                var updProduct = _productRepository.GetById(id);
                updProduct.Sku = product.Sku ?? updProduct.Sku;
                updProduct.Name = product.Name ?? updProduct.Name;
                updProduct.Description = product.Description ?? updProduct.Description;
                //Save log if price is updated
                if (updProduct.Price.HasValue && product.Price != null &&
                    !updProduct.Price.Equals(product.Price))
                {
                    _productPriceLogRepository.Create(
                        new ProductPriceLog
                        {
                            ProductId = updProduct.Id,
                            OldPrice = (decimal)updProduct.Price,
                            NewPrice = (decimal)product.Price,
                            CreatedAt = DateTime.Now,
                            CreatedBy = int.Parse(User.Identity.Name) //Logged user
                }
                    );
                }
                updProduct.Price = product.Price ?? updProduct.Price;
                updProduct.Stock = product.Stock ?? updProduct.Stock;
                updProduct.Likes = product.Likes ?? updProduct.Likes;
                updProduct.Active = product.Active ?? updProduct.Active;
                updProduct.CreatedAt = product.CreatedAt;
                updProduct.UpdatedAt = DateTime.Now;
                updProduct.UpdatedBy = int.Parse(User.Identity.Name);//Logged user

                _productRepository.Update(updProduct);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult<Product> Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            //If product has log 
            if (_productPriceLogRepository.Count(pl => pl.ProductId == product.Id) > 0) {
                var productPriceLogs = _productPriceLogRepository.Find(pl => pl.ProductId == product.Id).ToList();
                foreach (var item in productPriceLogs)
                {
                    _productPriceLogRepository.Delete(item);
                }
            }

            _productRepository.Delete(product);
            return product;
        }

        [HttpPut("{id}/like")]
        public IActionResult LikeProduct(int id)
        {
            var updProduct = _productRepository.GetById(id);
            if (updProduct == null)
            {
                return NotFound();
            }

            try
            {
                updProduct.UpdatedAt = DateTime.Now;
                updProduct.UpdatedBy = int.Parse(User.Identity.Name);
                _productRepository.AddLike(updProduct,1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }


    }
}

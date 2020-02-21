using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnacksStore.Data.DTO;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;

namespace SnacksStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPurchaseProductRepository _purchaseProductRepository;

        public PurchaseController(
            IProductRepository productRepository,
            IPurchaseRepository purchaseRepository, 
            IPurchaseProductRepository purchaseProductRepository)
        {
            _productRepository = productRepository;
            _purchaseRepository = purchaseRepository;
            _purchaseProductRepository = purchaseProductRepository;
        }

        // POST: api/Purchase
        [HttpPost]
        public ActionResult<Purchase> PostPurchase(List<PurchaseDTO> purchaseDetail)
        {
            if (!ModelState.IsValid && purchaseDetail != null && purchaseDetail.Count > 0)
                return BadRequest(new { Message = "Required data missing" });

            //Check available quantity
            foreach (var item in purchaseDetail) {
                //Valid
                if (item.ProductQuantity <= 0)
                    return BadRequest(new
                    {
                        Message = string.Concat("ProductId <", item.ProductId, "> quantity must be greater  than 0")
                    });

                if (!_productRepository.CheckQuantityAvailable(item.ProductId, item.ProductQuantity))
                    return BadRequest(new {
                        Message = string.Concat("ProductId <",item.ProductId, "> unavailable or insufficient  stock")
                    });
            }
            var userId = int.Parse(User.Identity.Name);
            var clientId = purchaseDetail.First().ClientId ?? userId;

            var newPurchase = new Purchase();
            newPurchase.ClientId = clientId;
            newPurchase.NumberOfProducts = purchaseDetail.Count();
            newPurchase.Total = 0; //Zero by default
            newPurchase.CreatedAt = DateTime.Now;
            newPurchase.CreatedBy = userId;

            _purchaseRepository.Create(newPurchase);

            
            foreach (var item in purchaseDetail)
            {
                var product = _productRepository.GetById(item.ProductId);

                //Create purchase detail
                var newPurchaseProduct = new PurchaseProducts();
                newPurchaseProduct.PurchaseId = newPurchase.Id;
                newPurchaseProduct.ProductId = product.Id;
                newPurchaseProduct.ProductQuantity = item.ProductQuantity;
                newPurchaseProduct.Price = (decimal)product.Price;
                newPurchaseProduct.SubTotal = decimal.Round( (newPurchaseProduct.ProductQuantity * newPurchaseProduct.Price),2);
                newPurchaseProduct.CreatedBy = userId;
                newPurchaseProduct.CreatedAt = DateTime.Now;

                _purchaseProductRepository.Create(newPurchaseProduct);

                //Update total purchase
                newPurchase.Total += newPurchaseProduct.SubTotal;

                //Decrement product stock
                product.Stock -= newPurchaseProduct.ProductQuantity;
                product.UpdatedAt = DateTime.Now;
                product.UpdatedBy = userId;

                _productRepository.Update(product);

            }
            //Update total purchase
            newPurchase.UpdatedAt = DateTime.Now;
            newPurchase.UpdatedBy = userId;
            _purchaseRepository.Update(newPurchase);

            return CreatedAtAction("GetPurchase", new { id = newPurchase.Id }, newPurchase);
        }

        // GET: api/Purchase/5
        [HttpGet("{id}")]
        public ActionResult<Purchase> GetPurchase(int id)
        {
            var product = _purchaseRepository.GetByIdWithProducts(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		private readonly ProductService _productService;
		private readonly CustomerService _customerService;

        public Parte2Controller(ProductService productService, CustomerService customerService)
        {
            _productService = productService;
            _customerService = customerService;
        }


        [HttpGet("products")]
		public ActionResult<PagedList<Product>> ListProducts(int page)
		{
            if (page < 1)
            {
                return BadRequest("O número da página deve ser maior ou igual a 1.");
            }

            return Ok(_productService.ListProducts(page));
		}

		[HttpGet("customers")]
		public ActionResult<PagedList<Customer>> ListCustomers(int page)
		{
            if (page < 1)
            {
                return BadRequest("O número da página deve ser maior ou igual a 1.");
            }

            return Ok(_customerService.ListCustomers(page));
		}
	}
}

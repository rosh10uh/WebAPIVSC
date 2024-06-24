using Microsoft.AspNetCore.Mvc;
using ProductWebAPIVS.Models;

namespace ProductWebAPIVS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly DemoContext _dbContext;
 
    public ProductController(DemoContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IActionResult Get()
    {
        var product=this._dbContext.Products.ToList();
        return Ok(product);
    }

    [HttpGet("GetProductById/{id}")]
    public IActionResult GetProductById(int id)
    {
        var product=this._dbContext.Products.FirstOrDefault(x=>x.Id==id);
        return Ok(product);
    }

    [HttpGet("Remove/{id}")]
    public IActionResult Remove(int id)
    {
        var product=this._dbContext.Products.FirstOrDefault(x=>x.Id==id);

        if(product!=null)
        {
            this._dbContext.Remove(product);
            this._dbContext.SaveChanges();
            return Ok(Get());
        }
        return Ok(false);
    }

     [HttpPost("AddProducts")]
    public IActionResult Create([FromBody]Product _product)
    {
      var product=this._dbContext.Products.FirstOrDefault(x=>x.Id==_product.Id);
      if(product!=null)
      {
        product.Name=_product.Name;
        product.Category=_product.Category;
        product.Price=_product.Price;
        this._dbContext.SaveChanges();
        
      }
      else
      {
        this._dbContext.Add(_product);
        this._dbContext.SaveChanges();
      }
        
        return Ok(Get());
    }
}

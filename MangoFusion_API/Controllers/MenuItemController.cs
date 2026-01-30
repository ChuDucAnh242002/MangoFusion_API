using MangoFusion_API.Data;
using MangoFusion_API.Models;
using MangoFusion_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MangoFusion_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItemController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _response = new ApiResponse();
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult GetMenuItems()
        {
            _response.Result = _db.MenuItems.ToList();
            List<MenuItem> menuItems = _db.MenuItems.ToList();
            List<OrderDetail> orderDetailsWithRatings = _db.OrderDetails.Where(u => u.Rating != null).ToList();

            foreach (var menuItem in menuItems)
            {
                var ratings = orderDetailsWithRatings.Where(u => u.MenuItemId == menuItem.Id).Select(u => u.Rating.Value);
                double avgRating = ratings.Any() ? ratings.Average() : 0;
                menuItem.Rating = avgRating;

            }
            _response.Result = menuItems;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}",Name = "GetMenuItem")]
        public IActionResult GetMenuItem(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            MenuItem? menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == id);
            List<OrderDetail> orderDetailsWithRatings = _db.OrderDetails.Where(u => u.Rating != null && u.MenuItemId == menuItem.Id).ToList();
            var ratings = orderDetailsWithRatings.Select(u => u.Rating.Value);
            double avgRating = ratings.Any() ? ratings.Average() : 0;
            menuItem.Rating = avgRating;
            _response.Result = menuItem;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm]MenuItemCreateDto menuItemCreateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemCreateDto.File == null || menuItemCreateDto.File.Length == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages = ["File is required."];
                        return BadRequest(_response);

                    }

                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
                    var filePath = Path.Combine(imagePath, menuItemCreateDto.File.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    // Uploading the image
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItemCreateDto.File.CopyToAsync(stream);
                    }

                    MenuItem menuItem = new MenuItem()
                    {
                        Name = menuItemCreateDto.Name,
                        Description = menuItemCreateDto.Description,
                        Category = menuItemCreateDto.Category,
                        SpecialTag = menuItemCreateDto.SpecialTag,
                        Price = menuItemCreateDto.Price,
                        Image = "images/" + menuItemCreateDto.File.FileName
                    };

                    _db.MenuItems.Add(menuItem);
                    await _db.SaveChangesAsync();

                    _response.Result = menuItemCreateDto;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItem.Id }, _response);

                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);

                }
            }
            catch (Exception ex) 
            { 
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.Message];
                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id, [FromForm] MenuItemUpdateDto menuItemUpdateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemUpdateDto == null || menuItemUpdateDto.Id != id)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);

                    }

                    MenuItem? menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);

                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return BadRequest(_response);
                    }

                    menuItemFromDb.Name = menuItemUpdateDto.Name;
                    menuItemFromDb.Description = menuItemUpdateDto.Description;
                    menuItemFromDb.Category = menuItemUpdateDto.Category;
                    menuItemFromDb.SpecialTag = menuItemUpdateDto.SpecialTag;
                    menuItemFromDb.Price = menuItemUpdateDto.Price;

                    if (menuItemUpdateDto.File != null)
                    {
                        if(menuItemUpdateDto.File.Length > 0)
                        {
                            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
                            if (!Directory.Exists(imagePath))
                            {
                                Directory.CreateDirectory(imagePath);
                            }
                            var filePath = Path.Combine(imagePath, menuItemUpdateDto.File.FileName);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            var filePath_OldFile = Path.Combine(_hostEnvironment.WebRootPath, menuItemFromDb.Image);
                            if (System.IO.File.Exists(filePath_OldFile))
                            {
                                System.IO.File.Delete(filePath_OldFile);
                            }

                            // Uploading the image
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await menuItemUpdateDto.File.CopyToAsync(stream);
                            }

                            menuItemFromDb.Image = "images/" + menuItemUpdateDto.File.FileName;
                        }
                    }

                    

                    _db.MenuItems.Update(menuItemFromDb);
                    await _db.SaveChangesAsync();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);

                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.Message];
                return BadRequest(_response);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    MenuItem? menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);

                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return BadRequest(_response);
                    }

                    var filePath_OldFile = Path.Combine(_hostEnvironment.WebRootPath, menuItemFromDb.Image);
                    if (System.IO.File.Exists(filePath_OldFile))
                    {
                        System.IO.File.Delete(filePath_OldFile);
                    }
                    
                    _db.MenuItems.Remove(menuItemFromDb);
                    await _db.SaveChangesAsync();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);

                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.Message];
                return BadRequest(_response);
            }
        }
    }
}

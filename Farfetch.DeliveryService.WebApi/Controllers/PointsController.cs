using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Farfetch.DeliveryService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farfetch.DeliveryService.WebApi.Controllers
{
    [Route("api/v1/points")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly IPointProvider _pointProvider;
        public PointsController(IPointProvider pointProvider)
        {
            _pointProvider = pointProvider;
        }

        /// <summary>
        /// Create point
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("point")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePointAsync(string name)
        {
            try
            {
                var isExists = await _pointProvider.IsPointExistsAsync(name);

                if (isExists) return StatusCode((int)HttpStatusCode.BadRequest, "Point name already registered!");

                await _pointProvider.CreatePointAsync(name);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,  ex);
            }
        }

        /// <summary>
        /// Update point
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("point")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePointNameAsync(string name, long id)
        {
            try
            {
                var isExists = await _pointProvider.IsPointExistsAsync(id);

                if(!isExists) return StatusCode((int)HttpStatusCode.BadRequest, "Point name is not registered!");

                await _pointProvider.ChangePointAsync(name, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("point")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePointNameAsync(long id)
        {
            try
            {
                var isExists = await _pointProvider.IsPointExistsAsync(id);

                if (!isExists) return StatusCode((int)HttpStatusCode.BadRequest, "Point name is not registered!");

                await _pointProvider.DeletePointAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all points
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetPointsAsync()
        {
            try
            {
                var points = await _pointProvider.GetPointsAsync();

                if (!points.Any()) return new NotFoundObjectResult("No points registered !");

                return new OkObjectResult(points);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
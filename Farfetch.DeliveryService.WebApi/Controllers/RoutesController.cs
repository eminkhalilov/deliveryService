using Farfetch.DeliveryService.Models.Routes;
using Farfetch.DeliveryService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.WebApi.Controllers
{
    [Route("api/v1/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteProvider _routeProvider;
        public RoutesController(IRouteProvider routeProvider)
        {
            _routeProvider = routeProvider;
        }

        /// <summary>
        /// Creating route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("route")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRouteAsync(Route route)
        {
            try
            {
                var isExists = await _routeProvider.IsRouteExistsAsync(route.SourcePointId, route.DestinationPointId);

                if (isExists) return StatusCode((int)HttpStatusCode.BadRequest, "Route already registered!");

                await _routeProvider.CreateRouteAsync(route);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        
        /// <summary>
        /// Update existing route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("route")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRouteAsync(Route route)
        {
            try
            {
                var isExists = await _routeProvider.IsRouteExistsAsync(route.Id);

                if (!isExists) return StatusCode((int)HttpStatusCode.BadRequest, "Route is not registered!");

                await _routeProvider.UpdateRouteAsync(route);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete route
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("route")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRouteAsync(long id)
        {
            try
            {
                var isExists = await _routeProvider.IsRouteExistsAsync(id);

                if (!isExists) return StatusCode((int)HttpStatusCode.BadRequest, "Route is not registered!");

                await _routeProvider.DeleteRouteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }


        /// <summary>
        /// Get all routes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetRoutesAsync()
        {
            try
            {
                var routes = await _routeProvider.GetRoutesAsync();

                if (!routes.Any()) return new NotFoundObjectResult("No routes registered !");

                return new OkObjectResult(routes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get routes by source and destionation point id
        /// </summary>
        /// <param name="sourcepointid"></param>
        /// <param name="destinationpointid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{sourcepointid}/{destinationpointid}")]
        [Authorize]
        public async Task<IActionResult> GetRoutesAsync(long sourcepointid, long destinationpointid)
        {
            try
            {
                var routes = await _routeProvider.GetRoutesBySourceAndDestinationAsync(sourcepointid, destinationpointid);

                if (!routes.Any()) return new NotFoundObjectResult("No routes registered !");

                return new OkObjectResult(routes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get route smallest time using source and destination point id
        /// </summary>
        /// <param name="sourcepointid"></param>
        /// <param name="destinationpointid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("route/time/{sourcepointid}/{destinationpointid}")]
        [Authorize]
        public async Task<IActionResult> GetRouteBySmallestTimeAsync(long sourcepointid, long destinationpointid)
        {
            try
            {
                var route = await _routeProvider.GetRouteBySmallestTimeAsync(sourcepointid, destinationpointid);

                if (route == null) return new NotFoundObjectResult("Route is not registered !");

                return new OkObjectResult(route);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get route lowest cost using source and destination point id
        /// </summary>
        /// <param name="sourcepointid"></param>
        /// <param name="destinationpointid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("route/cost/{sourcepointid}/{destinationpointid}")]
        [Authorize]
        public async Task<IActionResult> GetRouteByLowestCostAsync(long sourcepointid, long destinationpointid)
        {
            try
            {
                var route = await _routeProvider.GetRouteByLowestCostAsync(sourcepointid, destinationpointid);

                if (route == null) return new NotFoundObjectResult("Route is not registered !");

                return new OkObjectResult(route);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
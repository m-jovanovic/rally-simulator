using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RallySimulator.Api.Contracts;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Api.Infrastructure
{
    /// <summary>
    /// Represents the base API controller.
    /// </summary>
    [Route("api")]
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected ApiController(ISender sender) => Sender = sender;

        /// <summary>
        /// Gets the sender.
        /// </summary>
        protected ISender Sender { get; }

        /// <summary>
        /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
        /// response based on the specified <see cref="Result"/>.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
        protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));

        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> that produces a <see cref="StatusCodes.Status200OK"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
        protected new IActionResult Ok(object value) => base.Ok(value);

        /// <summary>
        /// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
        /// </summary>
        /// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
        protected new IActionResult NotFound() => NotFound("The requested resource was not found.");
    }
}

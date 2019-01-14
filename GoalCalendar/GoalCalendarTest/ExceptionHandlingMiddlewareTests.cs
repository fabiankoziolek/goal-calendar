using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using GoalCalendar.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace GoalCalendarTest
{
    public class ExceptionHandlingMiddlewareTests
    {
        [Fact]
        public async Task CustomExceptionIsRaised_ReturnCustomErrorResponseAndCorrectHttpStatus()
        {
            // Arrange
            var middleware = new ExceptionHandlingMiddleware((innerHttpContext) => throw new ObjectNotFoundException("Test", "Test"));

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await middleware.Invoke(context).ConfigureAwait(false);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<CustomErrorResponse>(streamText);
            var expectedStatusCode = context.Response.StatusCode;
            var expectedResponseBody = new CustomErrorResponse { Message = "Test", Description = "Test" };

            // Assert
            Assert.Equal(expectedResponseBody, objResponse);
            Assert.Equal(expectedStatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenAnUnExpectedExceptionIsRaised_CustomExceptionMiddlewareShouldHandleItToCustomErrorResponseAndInternalServerErrorHttpStatus()
        {
            // Arrange
            var middleware = new ExceptionHandlingMiddleware((innerHttpContext) => throw new Exception("Test"));

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await middleware.Invoke(context).ConfigureAwait(false);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<CustomErrorResponse>(streamText);
            var expectedStatusCode = context.Response.StatusCode;
            var expectedResponseBody = new CustomErrorResponse { Message = "Unexpected error", Description = "Unexpected error" };

            // Assert
            Assert.Equal(expectedResponseBody, objResponse);
            Assert.Equal(expectedStatusCode, (int)HttpStatusCode.InternalServerError);
        }
    }
}

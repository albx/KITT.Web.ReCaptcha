using KITT.Web.ReCaptcha.Http.v2;
using KITT.Web.ReCaptcha.Sample.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace KITT.Web.ReCaptcha.Sample.v2.Wasm.Api
{
    public class ReCaptchaFunction
    {
        private readonly ILogger _logger;

        public ReCaptchaService ReCaptchaService { get; }

        public ReCaptchaFunction(ILoggerFactory loggerFactory, ReCaptchaService reCaptchaService)
        {
            _logger = loggerFactory.CreateLogger<ReCaptchaFunction>();
            ReCaptchaService = reCaptchaService ?? throw new ArgumentNullException(nameof(reCaptchaService));
        }

        [Function(nameof(Verify))]
        public async Task<HttpResponseData> Verify(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "verify")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var model = await req.ReadFromJsonAsync<ReCaptchaModel>();
            if (model is null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var reCaptchaResult = await ReCaptchaService.VerifyAsync(model.CaptchaResponse);
            if (!reCaptchaResult.Success)
            {
                var errorResponse = req.CreateResponse();
                await errorResponse.WriteAsJsonAsync(reCaptchaResult.ErrorCodes);

                errorResponse.StatusCode = HttpStatusCode.BadRequest;
                return errorResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}

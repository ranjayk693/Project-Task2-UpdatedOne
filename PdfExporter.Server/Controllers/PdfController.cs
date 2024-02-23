
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using PdfExporter.Model;

//creating api for receiving url and return the pdf of the url content using puppeteerSharp lib.
namespace PdfExporter.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]     //Router to api/Pdf 
    public class PdfController : ControllerBase
        {
            private readonly ILogger<PdfController> _logger;

            public PdfController(ILogger<PdfController> logger)
            {
                _logger = logger;
            }

        //Get Method is used for receiving getPDF and returning pdf
        [HttpGet]
        public async Task<IActionResult> GetPdf(string url)
        {
            try
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();

                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });

                await using var page = await browser.NewPageAsync();
                await page.GoToAsync(url, new NavigationOptions { Timeout = 30000, WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } });

                var pdfStream = await page.PdfStreamAsync();

                _logger.LogInformation("PDF generated successfully.");

                return new FileStreamResult(pdfStream, "application/pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the PDF.");
                return StatusCode(500, "An error occurred while generating the PDF.");
            }
        }

    }
}

using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aspose.Pdf.Devices;
using System.Reflection;
using Aspose.Pdf;

namespace AsposeThumbnailingLinuxRepro;

public static class Function1
{
    [FunctionName("Function1")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest request,
        ILogger log)
    {
        var pageNumber = 1;
        if (request.Query.ContainsKey("page"))
            pageNumber = int.Parse(request.Query["page"]);

        log.LogInformation($"Generating thumbnail for page {pageNumber}.");

        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = $"{assembly.GetName().Name}.floorplan.pdf";
        using var inputStream = assembly.GetManifestResourceStream(resourcePath);
        using var document = new Document(inputStream);

        using var outputStream = new MemoryStream();
        var resolution = new Resolution(300);
        var jpegDevice = new JpegDevice(442, 286, resolution);
        jpegDevice.Process(document.Pages[pageNumber], outputStream);

        return new FileContentResult(outputStream.ToArray(), "image/jpeg");
    }
}

# AsposeThumbnailingLinuxRepro

This repository demonstrates an issue where Aspose.PDF's `JpegDevice` or
`PngDevice` generates a blank image for a specific PDF page.

## Running on Windows

1. Open the solution file in Visual Studio.
2. Run the function app.
3. Go to http://localhost:7071/api/Function1?page=1 in your browser. The page
   number can be 1, 2, or 3.

**Expected & actual behavior:** The function returns a JPEG thumbnail of the PDF
page. (It works correctly on Windows.)

## Running on Linux via Docker

1. Build the solution in Visual Studio.
2. From the repository root, run:
   `docker build bin/Debug/net6.0 -f Dockerfile --tag srmagura/aspose-thumbnailing-linux-repro`
3. Run: `docker run -p 7071:80 srmagura/aspose-thumbnailing-linux-repro`
4. Go to http://localhost:7071/api/Function1?page=1 in your browser. The page
   number can be 1, 2, or 3.

**Expected behavior:** Aspose generates a thumbnail for all pages.

**Actual behavior:**

- The page 1 thumbnail is blank white image.
- The following warning is printed mulitple times: "Path conversion requested
  528000 bytes (2560 x 1650). Maximum size is 262144 bytes."

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileUpload.Controllers
{
    [Route("Upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("")]
        [HttpPost]
        public async Task<string> Post([FromForm] IList<IFormFile> files)
        {
            try
            {
                if (files != null)
                {
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    else
                    {
                        foreach (var file in files)
                        {

                            var arquivo = Path.Combine(path, file.FileName);

                            using (FileStream fileStream = new FileStream(arquivo, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                fileStream.Flush();
                            }


                        }
                        return "Uploaded";
                    }

                }
                else
                {
                    return "Not Uploaded";
                }

            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
            return null;
        }
    }
}

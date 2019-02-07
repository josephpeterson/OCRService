using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OCRService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        IOCRService _ocrService;

        public ConvertController(IOCRService ocrService)
        {
            _ocrService = ocrService;
        }
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _ocrService.ConvertImage("trest");
        }
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post()
        {
            return new string[] { "value1", "value2" };
        }
    }
}

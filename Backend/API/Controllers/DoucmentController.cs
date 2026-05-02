using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Requests;
using Model.Dtos.Responses;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : BaseController<ParserResponse, object, object, DocumentUpdateRequest>
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService) : base(documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var result = _documentService.Process(file);

            return Ok(result);
        }
        [HttpGet("currency-total")]
        public IEnumerable<ParserResponse> CalculateTotalsByCurrency()
        {
            return _documentService.CalculateTotalsByCurrency();
        }
    }
}

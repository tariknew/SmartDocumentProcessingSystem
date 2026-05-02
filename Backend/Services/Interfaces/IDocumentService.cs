using Microsoft.AspNetCore.Http;
using Model.Dtos.Requests;
using Model.Dtos.Responses;

namespace Services.Interfaces
{
    public interface IDocumentService : IBaseService<ParserResponse, object, object, DocumentUpdateRequest>
    {
        ParserResponse Process(IFormFile fileStream);
        IEnumerable<ParserResponse> CalculateTotalsByCurrency();
    }
}

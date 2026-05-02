using Database.Context;
using Database.Entities;
using Helpers.Utilities;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Requests;
using Model.Dtos.Responses;
using Model.Utilities;
using Services.Interfaces;
using Services.Interfaces.Parsers;

namespace Services.Services
{
    public class DocumentService : BaseService<ParserResponse, Document, object, object, DocumentUpdateRequest>, IDocumentService
    {
        private readonly IEnumerable<IParserService> _parsers;

        public DocumentService(IEnumerable<IParserService> parsers, SDPSContext context, IMapper mapper) : base(context, mapper)
        {
            _parsers = parsers;
        }

        public IEnumerable<ParserResponse> CalculateTotalsByCurrency()
        {
            return _context.Documents
                .Where(d => d.Total != null && d.Currency != null)
                .GroupBy(d => d.Currency)
                .Select(g => new ParserResponse
                {
                    Currency = g.Key,
                    Total = Math.Round(g.Sum(x => x.Total.Value), 2)
                }).ToList();
        }

        public override IEnumerable<ParserResponse> Get(object search = null)
        {
            var list = _context.Documents
                .Include(d => d.LineItems);

            var mapped = _mapper.Map<List<ParserResponse>>(list);

            foreach (var doc in mapped)
            {
                DocumentValidator.Validate(doc);
            }

            return mapped;
        }
        public ParserResponse Process(IFormFile fileStream)
        {
            var extension = Path.GetExtension(fileStream.FileName);

            var parser = _parsers.FirstOrDefault(p => p.CanParse(extension));

            if (parser == null)
                throw new Exception("No parser found");

            ParserResponse result;

            try
            {
                using (var stream = fileStream.OpenReadStream())
                {
                    result = parser.Parse(stream);
                }

                var exists = _context.Documents
               .Any(d => d.DocumentNumber == result.DocumentNumber);

                if (exists)
                {
                    throw new UserException($"Document with number '{result.DocumentNumber}' already exists");
                }
            }
            catch
            {
                // Log exception (optional)
                throw;
            }

            SaveFile(fileStream);

            DocumentValidator.CleanStrings(result);

            DocumentValidator.Validate(result);

            // If document does not exist in database - it means it was never uploaded (set the status to "Uploaded")
            result.Status = "Uploaded";

            var entity = _mapper.Map<Document>(result);

            _context.Documents.Add(entity);
            _context.SaveChanges();

            var response = _mapper.Map<ParserResponse>(entity);

            response.Errors = result.Errors;
            response.TaxRate = result.TaxRate;

            return response;
        }
        private void SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}


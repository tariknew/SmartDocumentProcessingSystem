using Model.Dtos.Responses;

namespace Services.Interfaces.Parsers
{
    public interface IParserService
    {
        ParserResponse Parse(Stream fileStream);
        bool CanParse(string fileType);
    }
}

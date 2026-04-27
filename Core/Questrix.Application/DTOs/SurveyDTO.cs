namespace Questrix.Application.DTOs
{
    public class SurveyDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<SurveyNodeDTO> Nodes { get; set; }
    }
}

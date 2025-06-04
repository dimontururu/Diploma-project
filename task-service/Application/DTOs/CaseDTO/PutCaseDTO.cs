namespace task_service.Application.DTOs.CaseDTO
{
    public class PutCaseDTO
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public DateTime DateEnd { get; set; }
    }
}

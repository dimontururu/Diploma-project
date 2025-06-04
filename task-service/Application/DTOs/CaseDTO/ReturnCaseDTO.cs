namespace task_service.Application.DTOs.CaseDTO
{
    public class ReturnCaseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DateEnd { get; set; }
    }
}

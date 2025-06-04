namespace task_service.Application.DTOs.CaseDTO
{
    public class NewCaseDTO
    {
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DateEnd { get; set; }

        public Guid id_to_do_list { get; set; }
    }
}

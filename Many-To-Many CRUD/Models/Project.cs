namespace Many_To_Many_CRUD.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectDetails { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }

    }
}

namespace ToDoList.Dtos.Paging
{
    public class Meta
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCounts { get; set; }
        public int TotalPages { get; set; }
    }
}

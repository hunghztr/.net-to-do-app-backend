namespace ToDoList.Dtos.Paging
{
    public class ResultDto<T>
    {
        public Meta Meta { get; set; }
        public List<T> Datas { get; set; }

    }
}

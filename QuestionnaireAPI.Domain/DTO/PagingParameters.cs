namespace QuestionnaireAPI.Domain.DTO
{
    public class PagingParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;
        public int? SubjectId { get; set; }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}

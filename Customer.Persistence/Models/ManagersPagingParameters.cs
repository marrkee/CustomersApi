namespace Customers.Persistence.Models
{
    public class ManagersPagingParameters
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        public int MinId { get; set; } = 0;

        private int defaultPageSize = 10;

        public int PageSize
        {
            get
            {
                return this.defaultPageSize;
            }

            set
            {
                this.defaultPageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}

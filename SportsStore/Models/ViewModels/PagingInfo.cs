namespace SportsStore.Models.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        //The type is decimal because we are expecting to have a lot of products
        public int TotalPages =>
        (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}

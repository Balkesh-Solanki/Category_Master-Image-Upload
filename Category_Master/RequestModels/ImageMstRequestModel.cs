namespace Category_Master.RequestModels
{
    public class ImageMstRequestModel
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

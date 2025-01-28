namespace Category_Master.ResponseModels
{
    public class ImageMstResponseModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public long ImageSize { get; set; }
        public string ImageMIMEType { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}

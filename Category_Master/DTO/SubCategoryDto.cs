namespace Category_Master.DTO
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}

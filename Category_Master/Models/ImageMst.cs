using System;
using System.Collections.Generic;

namespace Category_Master.Models;

public partial class ImageMst
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string ImageType { get; set; } = null!;

    public long ImageSize { get; set; }

    public string ImageMimetype { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual CategoryMst Category { get; set; } = null!;

    public virtual SubCategoryMst SubCategory { get; set; } = null!;
}

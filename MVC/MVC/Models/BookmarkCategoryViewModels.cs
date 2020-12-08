using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class BookmarkCategoryViewModel
    {
        public int? ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public string CategoryName { get; set; }

        public int? CategoryId { get; set; }
    }


}

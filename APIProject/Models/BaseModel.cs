using System;

namespace APIProject.Models
{
    public class BaseModel
    {
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

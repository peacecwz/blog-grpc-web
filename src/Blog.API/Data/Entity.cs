using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Data
{
    public class Entity<T> where T : struct
    {
        [Key] public T Id { get; set; } = default;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedOn { get; set; }
    }
}
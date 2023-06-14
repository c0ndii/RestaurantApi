using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Entities
{
    public class Address
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}

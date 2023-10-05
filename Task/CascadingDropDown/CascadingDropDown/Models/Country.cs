using System.ComponentModel.DataAnnotations;

namespace CascadingDropDown.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
    }



    public class Data
    {
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
    }
}

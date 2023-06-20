using System.ComponentModel.DataAnnotations;

namespace Dish.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
        public string Name { get; set; }

        
        
      

        [Required]
        [Range(1, 10, ErrorMessage = "Tastiness must be between 1 and 10.")]
        public int Tastiness { get; set; }

        [Required]
        public int Calories { get; set; }
        [Display(Name = "Chef")]
        public int ChefId {get; set;}

        public Chef? Creator {get; set;}

       

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
    public class Chef
{
    [Key]
    public int ChefId { get; set; }       
    public string FirstName { get; set; } 
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }



     public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Dish> AllDishes {get; set;} = new List<Dish>();
      

}
}

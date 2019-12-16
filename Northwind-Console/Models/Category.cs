using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindConsole.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The category can not be created without a name")]
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }

        
        //create new category
        public Category createNewCategory(string userInput)
        {
            Category category = new Category();

            category.CategoryName = userInput;

            System.Console.WriteLine("Category Description: ");
            userInput = System.Console.ReadLine();

            category.Description = userInput;

            return category;
        }
        public int validateInt(string userInput)
        {
            int userInt;
            while (!(System.Int32.TryParse(userInput, out userInt)))
            {
                System.Console.WriteLine("That is not a valid number please try again");
                System.Console.WriteLine("Enter the ID: ");
                userInput = System.Console.ReadLine();
            }
            return userInt;
        }
    }
}

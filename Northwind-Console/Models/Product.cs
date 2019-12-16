using System;

namespace NorthwindConsole.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }


        //does not include category or supplier IDs
        public Product createNewProduct(String userInput)
        {
            Product product = new Product();

            //should of already verified that product name is unique
            product.ProductName = userInput;

            Console.WriteLine("Enter Quantity Per Unit: ");
            product.QuantityPerUnit = Console.ReadLine();

            Console.WriteLine("Enter Unit Price: ");
            userInput = Console.ReadLine();
            handleUnitPriceConversion(userInput, product);

            Console.WriteLine("Enter Units in Stock");
            userInput = Console.ReadLine();
            handleUnitsInStockConversion(userInput, product);

            Console.WriteLine("Are there units on order? (Y/N) ");
            userInput = Console.ReadLine();
            if(userInput.ToLower() == "y")
            {
                Console.WriteLine("Enter number of units on order: ");
                userInput = Console.ReadLine();
                handleUnitsOnOrderConversion(userInput, product);
            }
            else
            {
                product.UnitsOnOrder = null;
            }

            Console.WriteLine("Enter the reoder level");
            userInput = Console.ReadLine();
            handleReorderLevelConversion(userInput, product);


            return product;
        }

        //does not have option to change category or supplier IDs
        public Product editExistingProduct(Product product)
        {
            string userInput;

            //product name
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            //change product name
            if (userInput == "y")
            {
                Console.WriteLine("New Product Name: ");
                userInput = Console.ReadLine();
                product.ProductName = userInput;
            }

            //unit price
            Console.WriteLine($"\nUnit Price: {product.UnitPrice}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            if (userInput == "y")
            {
                Console.WriteLine("New Unit Price: ");
                userInput = Console.ReadLine();
                //convert to decimal
                handleUnitPriceConversion(userInput, product);

            }

            //quantity per unit
            Console.WriteLine($"Quantity per Unit: {product.QuantityPerUnit}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            //change quantity per unit
            if (userInput == "y")
            {
                Console.WriteLine("New Quantity per Unit: ");
                userInput = Console.ReadLine();
                product.QuantityPerUnit = userInput;
            }

            //units in stock
            Console.WriteLine($"Units in Stock: {product.UnitsInStock}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            //change units in stock
            if (userInput == "y")
            {
                Console.WriteLine("New Amount of Units in stock: ");
                userInput = Console.ReadLine();
                //convert to integer
                handleUnitsInStockConversion(userInput, product);
            }

            //units on order
            Console.WriteLine($"Units on order: {product.UnitsOnOrder}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            //change value
            if (userInput == "y")
            {
                Console.WriteLine("New Amount of Units on order: ");
                userInput = Console.ReadLine();
                //convert to integer
                handleUnitsOnOrderConversion(userInput, product);
            }

            //Reorder level
            Console.WriteLine($"Reorder Level: {product.ReorderLevel}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            //change value
            if (userInput == "y")
            {
                Console.WriteLine("New Reorder Level: ");
                userInput = Console.ReadLine();
                //convert to short
                handleReorderLevelConversion(userInput, product);
            }

            //discontinued
            Console.WriteLine($"Discontinued: {product.Discontinued}");
            Console.WriteLine("Change (Y/N)");
            userInput = Console.ReadLine().ToLower();
            //change value
            if (userInput == "y")
            {
                Console.WriteLine($"New discontinued statuse(true/false): ");
                userInput = Console.ReadLine();
                if (Boolean.TryParse(userInput, out Boolean userBool))
                {
                    product.Discontinued = userBool;
                }
                else
                {
                    Console.WriteLine("That is not a valid entry the discontinued status will remain the same");
                }
            }


            return product;
        }

        //verify and set unit price
        public void handleUnitPriceConversion(String userInput, Product product)
        {
            if (Decimal.TryParse(userInput, out decimal userDecimal))
            {
                product.UnitPrice = userDecimal;
            }
            else
            {
                Console.WriteLine("That is not a valid decimal unit price is now 'null'");
                product.UnitPrice = null;
            }
        }

        //verify and set units in stock
        public void handleUnitsInStockConversion(String userInput, Product product)
        {
            if (short.TryParse(userInput, out short userShort))
            {
                product.UnitsInStock = userShort;
            }
            else
            {
                Console.WriteLine("That is not a valid number units in stock in now 'null'");
                product.UnitsInStock = null;
            }
        }

        //verify and set units on order
        public void handleUnitsOnOrderConversion(String userInput, Product product)
        {
            if (short.TryParse(userInput, out var userShort))
            {
                product.UnitsOnOrder = userShort;
            }
            else
            {
                Console.WriteLine("That is not a valid number units on order is now set to null");
                product.UnitsOnOrder = null;
            }
        }
        
        //verify and set reorder level
        public void handleReorderLevelConversion(String userInput, Product product)
        {
            if (short.TryParse(userInput, out var userShort))
            {
                product.ReorderLevel = userShort;
            }
            else
            {
                Console.WriteLine("That was an invalid number reorder level is now set to 1");
                product.ReorderLevel = 1;
            }
        }

        //validate and ensure an int is entered 
        public int validateInt(String userInput)
        {
            int userInt;
            while (!(Int32.TryParse(userInput, out userInt)))
            {
                Console.WriteLine("That is not a valid number please try again");
                Console.WriteLine("Enter the ID: ");
                userInput = Console.ReadLine();
            }
            return userInt;
        }


        //display all info on a product except supplier and category IDs and names
        public void displayProductInfo(Product product, bool discontinued)
        {
            Console.WriteLine($"Name: {product.ProductName} ");
            Console.WriteLine($"ID: {product.ProductID}");

            //loop that displays either discontinued or active instead of true or false
            if (!discontinued)
            {
                Console.WriteLine("Active");
            }
            else
            {
                Console.WriteLine("Discontinued");
            }

            Console.WriteLine($"Unit Price: ${product.UnitPrice}");
            Console.WriteLine($"Quantity Per Unit: {product.QuantityPerUnit}");
            Console.WriteLine($"Units in Stock: {product.UnitsInStock}");
            Console.WriteLine($"Reorder Level: {product.ReorderLevel}");
            Console.WriteLine($"Units on Order: {product.UnitsOnOrder}");
        }

        


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    //only annotation validation!!
    //extra credit =detele all tables and add additiona annotation then add new annotationss
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
             try
            {
                

                //will only add saves once sure everything works       db.SaveChanges();

                String choice;

                //final project
                do
                {
                    //determine table user wants to work with
                    Console.WriteLine("\n1) Products");
                    Console.WriteLine("2) Categories");
                    Console.WriteLine("\"q\" to quite ");

                    choice = Console.ReadLine();
                    Console.Clear();
                    logger.Info($"Option {choice} selected for table selection menu");

                    //validate users selection
                    while (!((choice == "1") || (choice == "2") || (choice.ToLower() == "q")))
                    {
                        Console.WriteLine("\nInvalid selection please choose again");
                        Console.WriteLine("1) Products");
                        Console.WriteLine("2) Categories");
                        Console.WriteLine("\"q\" to quite ");
                        choice = Console.ReadLine();
                        Console.Clear();
                        logger.Info($"Option {choice} selected for table selection menu previouse choice was invalid");
                    }

                    //create database connection
                    var db = new NorthwindContext();

                    //variables to hold users input
                    string userInput;
                    int userInt;

                    //products
                    if(choice == "1")
                    {
                        Console.WriteLine("\n1) Add new product");
                        Console.WriteLine("2) Display products");
                        Console.WriteLine("3) Edit a product");
                        Console.WriteLine("4) Delete a product");
                        Console.WriteLine("\"q\" to quite ");
                        choice = Console.ReadLine();
                        Console.Clear();
                        logger.Info($"Option {choice} selected for product selection menu");


                        //so each choice doesnt need a new product object
                        Product product = new Product();
                        //validate users selection
                        while (!((choice == "1") || (choice == "2") || (choice == "3") || (choice == "4") || (choice.ToLower() == "q")))
                        {
                            Console.WriteLine("\nInvalid selection please choose again");
                            Console.WriteLine("1) Add new product");
                            Console.WriteLine("2) Display products");
                            Console.WriteLine("3) Edit a product");
                            Console.WriteLine("4) Delete a product");
                            Console.WriteLine("\"q\" to quite ");
                            choice = Console.ReadLine();
                            Console.Clear();
                            logger.Info($"Option {choice} selected for product selection menu previuse selection was invalid");
                        }

                        //add new product
                        if(choice == "1")
                        {
                            
                            //get the name of product the user would like to enter
                            Console.WriteLine("\nName of New Product: ");
                            userInput = Console.ReadLine();

                            
                            //find out if product already exists if not...
                            if(db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower())){

                                logger.Info($"Product {userInput} already exists");
                                Console.WriteLine("That product already exists");
                            }
                            //...create new product
                            else
                            {
                                product = product.createNewProduct(userInput);


                                //CategoryID
                                
                                Console.WriteLine("Enter the corresponding Category ID: ");
                                userInput = Console.ReadLine();
                                //make sure they entered a number
                                userInt = product.validateInt(userInput);

                                //make sure category ID is valid
                                var query = db.Categories.Any(c => c.CategoryId == userInt);
                                while (!query)
                                {
                                    Console.WriteLine("That Category ID does not match any existing Caregories please try again\n");
                                    Console.WriteLine("Enter the corresponding Category ID: ");
                                    userInput = Console.ReadLine();
                                    //make sure they entered a number
                                    userInt = product.validateInt(userInput);

                                }
                                product.CategoryId = userInt;

                                //SupplierID
                                Console.WriteLine("Enter the corresponding Supplier ID: ");
                                userInput = Console.ReadLine();
                                //make sure they entered a number
                                userInt = product.validateInt(userInput);

                                //make sure they entered a valid Supplier ID
                                query = db.Suppliers.Any(s => s.SupplierId == userInt);
                                while (!query)
                                {
                                    Console.WriteLine("That Supplier ID does not match any existing Suppliers please try again\n");
                                    Console.WriteLine("Enter the corresponding Supplier ID: ");
                                    userInput = Console.ReadLine();
                                    //make sure they entered a number
                                    userInt = product.validateInt(userInput);

                                }
                                product.SupplierId = userInt;


                                //database save
                                db.Products.Add(product);
                                db.SaveChanges();
                            }

                                  
                        }
                        //display products
                        else if(choice == "2")
                        {
                            Console.WriteLine("\n1) Display all products");
                            Console.WriteLine("2) Display discontinued products");
                            Console.WriteLine("3) Display active products");
                            Console.WriteLine("4) Display specific product");
                            choice = Console.ReadLine();
                            Console.Clear();
                            logger.Info($"Option {choice} selected for product display menu");

                            //validate users selection
                            while (!((choice == "1") || (choice == "2") || (choice == "3") || (choice == "4")))
                            {
                                Console.WriteLine("\nInvalid selection please choose again");
                                Console.WriteLine("1) Display all procucts");
                                Console.WriteLine("2) Display discontinued products");
                                Console.WriteLine("3) Display active products");
                                Console.WriteLine("4) Display specific product");
                                choice = Console.ReadLine();
                                Console.Clear();
                                logger.Info($"Option {choice} selected for product display menu previuse selection was invalid");
                            }

                            //display all products
                            if (choice == "1")
                            {
                                var query = db.Products.OrderBy(p => p.CategoryId);
                                Console.WriteLine("\nAll Products");
                                Console.WriteLine("Products organized by Category\n");
                                foreach (var item in query)
                                {
                                    Console.WriteLine($"{item.ProductName}");
                                }

                            }
                            //display discontinued products
                            else if (choice == "2")
                            {
                                var query = db.Products.Where(p => p.Discontinued == true).OrderBy(p => p.CategoryId);

                                Console.WriteLine("\nDiscontinued Products");
                                Console.WriteLine("Products organized by Category\n");
                                foreach (var item in query)
                                {
                                    Console.WriteLine($"{item.ProductName}");
                                }
                            }
                            //display active products
                            else if (choice == "3")
                            {
                                var query = db.Products.Where(p => p.Discontinued == false).OrderBy(p => p.CategoryId);

                                Console.WriteLine("\nActive Products");
                                Console.WriteLine("Products organized by Category\n");
                                foreach (var item in query)
                                {
                                    Console.WriteLine($"{item.ProductName}");
                                }
                            }
                            //display specific product
                            else if (choice == "4")
                            {
                                Console.WriteLine("\n1) Find product by name");
                                Console.WriteLine("2) Find product by ID");
                                choice = Console.ReadLine();
                                Console.Clear();
                                logger.Info($"Option {choice} selected for specific product display menu");

                                //validate users selection
                                while (!((choice == "1") || (choice == "2")))
                                {
                                    Console.WriteLine("\nInvalid selection please choose again\n");
                                    Console.WriteLine("1) Find product by name");
                                    Console.WriteLine("2) Find product by ID");
                                    choice = Console.ReadLine();
                                    Console.Clear();
                                    logger.Info($"Option {choice} selected for specific product display menu previuse selection was invalid");
                                }

                                //user wants to search by name
                                if(choice == "1")
                                {
                                    Console.WriteLine("\nEnter desired product's name");
                                    userInput = Console.ReadLine();
                                    logger.Info($"{userInput} was entered for the name of the desired product to be displayed");
                                    Console.Clear();

                                    //check user entered a valid name
                                    var query = db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower());
                                    while (!query)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nThat was not a valid product name please try again\n");

                                        Console.WriteLine("Enter desired product's name");
                                        userInput = Console.ReadLine();
                                        logger.Info($"{userInput} was entered for the name of the desired product to be displayed last entry was invalid");
                                        query = db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower());
                                    }

                                    //display all info on desired product
                                    var query2 = db.Products.FirstOrDefault(p => p.ProductName.ToLower() == userInput.ToLower());
                                    //variable to hold actual category and supplier names instead of IDs
                                    var connectedCategory = db.Categories.FirstOrDefault(c => c.CategoryId == query2.CategoryId);
                                    var connectedSupplier = db.Suppliers.FirstOrDefault(s => s.SupplierId == query2.SupplierId);

                                    product.displayProductInfo(query2, query2.Discontinued);
                                    Console.WriteLine($"Category: {connectedCategory.CategoryName}");
                                    Console.WriteLine($"Supplier Company Name: {connectedSupplier.CompanyName}");

                                }
                                //user wants to search by ID
                                else if(choice == "2")
                                {
                                    Console.WriteLine("\nEnter desired product's ID");
                                    userInput = Console.ReadLine();
                                    //convert to int
                                    userInt = product.validateInt(userInput);
                                    logger.Info($"{userInt} was entered for the ID of the desired product to be displayed");

                                    //check user entered a valid Id
                                    var query = db.Products.Any(p => p.ProductID == userInt);
                                    while (!query)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nThat was not a valid product ID please try again\n");

                                        Console.WriteLine("Enter desired product's ID: ");
                                        userInput = Console.ReadLine();
                                        //convert to int
                                        userInt = product.validateInt(userInput);
                                        logger.Info($"{userInput} was entered for the ID of the desired product to be displayed last entry was invalid");
                                        query = db.Products.Any(p => p.ProductID == userInt);
                                    }

                                    //display all info on desired product
                                    var query2 = db.Products.FirstOrDefault(p => p.ProductID == userInt);
                                    //variable to hold actual category and supplier names instead of IDs
                                    var connectedCategory = db.Categories.FirstOrDefault(c => c.CategoryId == query2.CategoryId);
                                    var connectedSupplier = db.Suppliers.FirstOrDefault(s => s.SupplierId == query2.SupplierId);

                                    product.displayProductInfo(query2, query2.Discontinued);
                                    Console.WriteLine($"Category: {connectedCategory.CategoryName}");
                                    Console.WriteLine($"Supplier Company Name: {connectedSupplier.CompanyName}");
                                }

                            }
                        }
                        //edit a product
                        else if(choice == "3")
                        {
                            Console.WriteLine("\n1) Find product by name");
                            Console.WriteLine("2) Find product by ID");
                            choice = Console.ReadLine();
                            logger.Info($"Option {choice} selected for how to find the product to edit menu");
                            Console.Clear();

                            //validate users selection
                            while (!((choice == "1") || (choice == "2")))
                            {
                                Console.WriteLine("\nInvalid selection please choose again\n");
                                Console.WriteLine("1) Find product by name");
                                Console.WriteLine("2) Find product by ID");
                                choice = Console.ReadLine();
                                logger.Info($"Option {choice} selected for how to find the product to edit menu menu previuse selection was invalid");
                                Console.Clear();
                            }

                            //user wants to search by name
                            if(choice == "1")
                            {
                                
                                Console.WriteLine("\nEnter desired product's name");
                                userInput = Console.ReadLine();
                                logger.Info($"{userInput} was entered for the name of the desired product to be edited");
                                Console.Clear();

                                //check user entered a valid name
                                var query = db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower());
                                while (!query)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nThat was not a valid product name please try again\n");

                                    Console.WriteLine("Enter desired product's name");
                                    userInput = Console.ReadLine();
                                    logger.Info($"{userInput} was entered for the name of the desired product to be edited last entry was invalid");
                                    query = db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower());
                                    Console.Clear();

                                }

                                //add product info to local product object for editing purposes
                                var query2 = db.Products.FirstOrDefault(p => p.ProductName.ToLower() == userInput.ToLower());
                                product = query2;

                                //display product ID and name
                                Console.WriteLine($"Product ID: {product.ProductID} \nProduct Name: {product.ProductName} ");

                                //make sure the user really wants to edit this product
                                Console.WriteLine("\n1) This is the correct product");
                                Console.WriteLine("2) No longer want to edit this product");
                                choice = Console.ReadLine();
                                Console.Clear();
                                logger.Info($"{choice} was entered for the verification of editing menu");

                                while (!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\nInvalid selection please choose again\n");
                                    Console.WriteLine("1) This is the correct product");
                                    Console.WriteLine("2) No longer want to edit a product");
                                    choice = Console.ReadLine();
                                    Console.Clear();
                                    logger.Info($"{choice} was entered for the verification of editing menu previouse selection was invalid");
                                }

                                //user does want to edit this product
                                if(choice == "1")
                                {
                                    //find out what user wants to change

                                    product = product.editExistingProduct(product);

                                    
                                    //Category id
                                    var query1 = db.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                                    Console.WriteLine($"Category ID: {product.CategoryId}");
                                    Console.WriteLine($"Associated category name: {query1.CategoryId}");
                                    Console.WriteLine("Change (Y/N)");
                                    userInput = Console.ReadLine().ToLower();
                                    logger.Info($"{userInput} was response to weather or not they wanted to edit the categoryID");
                                    //change value
                                    if(userInput == "y")
                                    {
                                        Console.WriteLine($"New Category ID: ");
                                        userInput = Console.ReadLine();
                                        if(int.TryParse(userInput, out int userNum))
                                        {
                                            query = db.Categories.Any(c => c.CategoryId == userNum);
                                            if (query)
                                            {
                                                product.CategoryId = userNum;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The number entered was not a valid category ID so the value will remain the same");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The value ented was not a valid number so that Category ID will not change");
                                        }

                                    }

                                    //SupplierID
                                    var queryS = db.Suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);
                                    Console.WriteLine($"Supplier ID: {product.SupplierId}");
                                    Console.WriteLine($"Associated Supplier company name: {queryS.CompanyName}");
                                    Console.WriteLine("Change (Y/N)");
                                    userInput = Console.ReadLine().ToLower();
                                    logger.Info($"{userInput} was response to weather or not they wanted to edit the supplierID");
                                    if(userInput == "y")
                                    {
                                        Console.WriteLine("New Supplier ID: ");
                                        userInput = Console.ReadLine();
                                        if (int.TryParse(userInput, out int userNum))
                                        {
                                            query = db.Suppliers.Any(s => s.SupplierId == userNum);
                                            if (query)
                                            {
                                                product.SupplierId = userNum;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The number entered was not a valid supplier ID so the value will remain the same");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The value ented was not a valid number so that supplier ID will not change");
                                        }
                                    }

                                    Console.Clear();
                                    //ProductID
                                    //display only
                                    Console.WriteLine($"Product ID: {product.ProductID} this product has now been updated");
                                    logger.Info($"Product {product.ProductID} {product.ProductName} has been updated");

                                    //remove old version of product
                                    db.Products.Remove(query2);
                                    //add new version of product
                                    db.Products.Add(product);
                                    db.SaveChanges();

                                    //save to database
                                }
                                //user no longer wants to edit a product
                                else if(choice == "2")
                                {
                                    logger.Info($"{product.ProductName} was not edited");
                                    Console.Clear();
                                }

                            }
                            //user wants to search by ID
                            else if(choice == "2")
                            {
                                Console.WriteLine("\nEnter desired product's ID");
                                userInput = Console.ReadLine();
                                //convert to int
                                userInt = product.validateInt(userInput);
                                logger.Info($"{userInput} was entered for the ID of the desired product to be edited");

                                //check user entered a valid name
                                var query = db.Products.Any(p => p.ProductID == userInt);
                                while (!query)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nThat was not a valid product ID please try again\n");

                                    Console.WriteLine("Enter desired product's ID");
                                    userInput = Console.ReadLine();
                                    product.validateInt(userInput);
                                    logger.Info($"{userInput} was entered for the ID of the desired product to be edited last entry was invalid");
                                    query = db.Products.Any(p => p.ProductID == userInt);
                                }

                                //add product info to local product object for editing purposes
                                var query2 = db.Products.FirstOrDefault(p => p.ProductID == userInt);
                                product = query2;

                                //display product ID and name
                                Console.WriteLine($"Product ID: {product.ProductID} \nProduct Name: {product.ProductName} ");

                                //make sure the user really wants to edit this product
                                Console.WriteLine("\n1) This is the correct product");
                                Console.WriteLine("2) No longer want to edit this product");
                                choice = Console.ReadLine();
                                Console.Clear();
                                logger.Info($"{choice} was entered for the verification of editing menu");

                                while (!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\nInvalid selection please choose again\n");
                                    Console.WriteLine("1) This is the correct product");
                                    Console.WriteLine("2) No longer want to edit a product");
                                    choice = Console.ReadLine();
                                    Console.Clear();
                                    logger.Info($"{choice} was entered for the verification of editing menu previouse selection was invalid");
                                }

                                //user does want to edit this product
                                if (choice == "1")
                                {
                                    //find out what user wants to change

                                    product = product.editExistingProduct(product);


                                    //Category id
                                    var query1 = db.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                                    Console.WriteLine($"Category ID: {product.CategoryId}");
                                    Console.WriteLine($"Associated category name: {query1.CategoryId}");
                                    Console.WriteLine("Change (Y/N)");
                                    userInput = Console.ReadLine().ToLower();
                                    logger.Info($"{userInput} was response to weather or not they wanted to edit the categoryID");
                                    //change value
                                    if (userInput == "y")
                                    {
                                        Console.WriteLine($"New Category ID: ");
                                        userInput = Console.ReadLine();
                                        if (int.TryParse(userInput, out int userNum))
                                        {
                                            query = db.Categories.Any(c => c.CategoryId == userNum);
                                            if (query)
                                            {
                                                product.CategoryId = userNum;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The number entered was not a valid category ID so the value will remain the same");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The value ented was not a valid number so that Category ID will not change");
                                        }

                                    }

                                    //SupplierID
                                    var queryS = db.Suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);
                                    Console.WriteLine($"Supplier ID: {product.SupplierId}");
                                    Console.WriteLine($"Associated Supplier company name: {queryS.CompanyName}");
                                    Console.WriteLine("Change (Y/N)");
                                    userInput = Console.ReadLine().ToLower();
                                    logger.Info($"{userInput} was response to weather or not they wanted to edit the supplierID");
                                    if (userInput == "y")
                                    {
                                        Console.WriteLine("New Supplier ID: ");
                                        userInput = Console.ReadLine();
                                        if (int.TryParse(userInput, out int userNum))
                                        {
                                            query = db.Suppliers.Any(s => s.SupplierId == userNum);
                                            if (query)
                                            {
                                                product.SupplierId = userNum;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The number entered was not a valid supplier ID so the value will remain the same");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The value ented was not a valid number so that supplier ID will not change");
                                        }
                                    }

                                    Console.Clear();
                                    //ProductID
                                    //display only
                                    Console.WriteLine($"Product ID: {product.ProductID} this product has now been updated");
                                    logger.Info($"Product {product.ProductID} {product.ProductName} has been updated");

                                    //remove old version of product
                                    db.Products.Remove(query2);
                                    //add new version of product
                                    db.Products.Add(product);
                                    db.SaveChanges();
                                    

                                    //save to database
                                }
                                //user no longer wants to edit a product
                                else if (choice == "2")
                                {
                                    logger.Info("{product.ProductName} was not edited");
                                    Console.Clear();
                                }
                            }
                        }
                        //delete product
                        else if(choice == "4")
                        {
                            Console.WriteLine("\n1) Find product by name");
                            Console.WriteLine("2) Find product by ID");
                            choice = Console.ReadLine();
                            logger.Info($"Option {choice} selected for how to find the product to delete menu");
                            Console.Clear();

                            //validate users selection
                            while (!((choice == "1") || (choice == "2")))
                            {
                                Console.WriteLine("\nInvalid selection please choose again\n");
                                Console.WriteLine("1) Find product by name");
                                Console.WriteLine("2) Find product by ID");
                                choice = Console.ReadLine();
                                logger.Info($"Option {choice} selected for how to find the product to delete menu menu previuse selection was invalid");
                                Console.Clear();
                            }

                            //user wants to search by name
                            if(choice == "1")
                            {
                                Console.WriteLine("\nDesired product's name: ");
                                userInput = Console.ReadLine();
                                logger.Info($"{userInput} was entered for the name of the desired product to be deleted");
                                Console.Clear();

                                //check user entered a valid name
                                var query = db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower());
                                while (!query)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nThat was not a valid product name please try again\n");

                                    Console.WriteLine("Enter desired product's name");
                                    userInput = Console.ReadLine();
                                    logger.Info($"{userInput} was entered for the name of the desired product to be edited last entry was invalid");
                                    query = db.Products.Any(p => p.ProductName.ToLower() == userInput.ToLower());
                                    Console.Clear();

                                }

                                var query2 = db.Products.FirstOrDefault(p => p.ProductName.ToLower() == userInput.ToLower());
                                product = query2;

                                Console.WriteLine($"\n{product.ProductID} {product.ProductName}\n");
                                Console.WriteLine("1) This is the product to be deleted");
                                Console.WriteLine("2) No longer want to delete this product");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} was entered as the selection of weather or not the user wanted to delete the product");
                                Console.Clear();

                                //verify choice
                                while (!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\nInvalid selection please try agian\n");
                                    Console.WriteLine($"{product.ProductID} {product.ProductName}\n");
                                    Console.WriteLine("1) This is the product to be deleted");
                                    Console.WriteLine("2) No longer want to delete this product");
                                    choice = Console.ReadLine();
                                    logger.Info($"{choice} was entered as the selection of weather or not the user wanted to delete the product, previouse selection was invalid");
                                    Console.Clear();
                                }
                                
                                //user wants to delete product
                                if(choice == "1")
                                {
                                    db.Products.Remove(query2);
                                    logger.Info($"{product.ProductName} has been deleted");
                                    Console.WriteLine($"{product.ProductName} has been deleted");

                                    //database save
                                    db.SaveChanges();
                                }
                                //user no longer wants to delete product
                                else if(choice == "2")
                                {
                                    logger.Info($"{product.ProductName} was not deleted");
                                    Console.Clear();
                                }

                            }
                            //user wants to search by id
                            else if(choice == "2")
                            {
                                Console.WriteLine("\nDesired product's ID: ");
                                userInput = Console.ReadLine();
                                userInt = product.validateInt(userInput);
                                logger.Info($"{userInput} was entered for the ID of the desired product to be deleted");
                                Console.Clear();

                                //check user entered a valid ID
                                var query = db.Products.Any(p => p.ProductID == userInt);
                                while (!query)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nThat was not a valid product ID please try again\n");

                                    Console.WriteLine("Enter desired product's ID");
                                    userInput = Console.ReadLine();
                                    userInt = product.validateInt(userInput);
                                    logger.Info($"{userInput} was entered for the ID of the desired product to be edited last entry was invalid");
                                    query = db.Products.Any(p => p.ProductID == userInt);
                                    Console.Clear();

                                }

                                var query2 = db.Products.FirstOrDefault(p => p.ProductID == userInt);
                                product = query2;

                                Console.WriteLine($"\n{product.ProductID} {product.ProductName}\n");
                                Console.WriteLine("1) This is the product to be deleted");
                                Console.WriteLine("2) No longer want to delete this product");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} was entered as the selection of weather or not the user wanted to delete the product");
                                Console.Clear();

                                //verify choice
                                while (!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\nInvalid selection please try agian\n");
                                    Console.WriteLine($"{product.ProductID} {product.ProductName}\n");
                                    Console.WriteLine("1) This is the product to be deleted");
                                    Console.WriteLine("2) No longer want to delete this product");
                                    choice = Console.ReadLine();
                                    logger.Info($"{choice} was entered as the selection of weather or not the user wanted to delete the product, previouse selection was invalid");
                                    Console.Clear();
                                }

                                //user wants to delete product
                                if (choice == "1")
                                {
                                    db.Products.Remove(query2);
                                    logger.Info($"{product.ProductName} has been deleted");
                                    Console.WriteLine($"{product.ProductName} has been deleted");

                                    //database save
                                    db.SaveChanges();

                                }
                                //user no longer wants to delete product
                                else if (choice == "2")
                                {
                                    logger.Info($"{product.ProductName} was not deleted");
                                    Console.Clear();
                                }
                            }
                        }
                     

                    }
                    //categories
                    else if(choice == "2")
                    {
                        
                        Console.WriteLine("\n1) Add new Category");
                        Console.WriteLine("2) Display Categories");
                        Console.WriteLine("3) Edit a Category");
                        Console.WriteLine("4) Delete a Category");
                        Console.WriteLine("\"q\" to quite ");
                        choice = Console.ReadLine();
                        Console.Clear();
                        logger.Info($"Option {choice} selected for Category selection menu");

                        //so each category doesnt need a new category object
                        Category category = new Category();



                        while (!(choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice.ToLower() == "q"))
                        {
                            Console.WriteLine("\nPreviouse selection invalid please try again");

                            Console.WriteLine("\n1) Add new Category");
                            Console.WriteLine("2) Display Categories");
                            Console.WriteLine("3) Edit a Category");
                            Console.WriteLine("4) Delete a Category");
                            Console.WriteLine("\"q\" to quite ");
                            choice = Console.ReadLine();
                            Console.Clear();
                            logger.Info($"Option {choice} selected for Category selection menu previouse selection invalid");
                        }

                        //add category
                        if(choice == "1")
                        {
                            //get the name of the new category
                            Console.WriteLine("\nNew category name: ");
                            userInput = Console.ReadLine();

                            //make sure does not already exist
                            var query = db.Categories.Any(c => c.CategoryName.ToLower() == userInput.ToLower());

                            //if it exists 
                            if (query)
                            {
                                Console.WriteLine("\nThat category already exists");
                                logger.Info("Category entered already existed no new category will be created");
                            }
                            //if category does not exist create it
                            else
                            {
                                category = category.createNewCategory(userInput);
                                db.Categories.Add(category);
                                db.SaveChanges();
                            }

                        }
                        //display categories
                        else if(choice == "2")
                        {
                            Console.WriteLine("\n1) Display Category and Description");
                            Console.WriteLine("2) Display Category and its Products");
                            Console.WriteLine("3) Display Specific Category");
                            choice = Console.ReadLine();
                            logger.Info($"User selected {choice} in the category menu");
                            Console.Clear();

                            //validate users choice
                            while(!(choice == "1" || choice == "2" || choice == "3"))
                            {
                                Console.WriteLine("\n Previouse selection invalid please try again");
                                Console.WriteLine("\n1) Display Category Name and Description");
                                Console.WriteLine("2) Display Category and its Products");
                                Console.WriteLine("3) Display Specific Category");
                                choice = Console.ReadLine();
                                logger.Info($"User selected {choice} in the category menu previouse selection was invalid");
                                Console.Clear();
                            }

                            //display category name and description
                            if(choice == "1")
                            {
                                var query = db.Categories.OrderBy(c => c.CategoryId);
                                foreach(var item in query)
                                {
                                    Console.WriteLine($"Category: {item.CategoryName}");
                                    Console.WriteLine($"\nDescription: {item.Description}\n");
                                }
                            }
                            //display category name and its products
                            else if(choice == "2")
                            {

                                
                                int categoryID = 0;

                                var query = db.Categories.OrderBy(c => c.CategoryId).ToList();

                                //regular for loop????
                                for (int i = 0; i < query.Count(); i++)
                                {
                                    Console.WriteLine($"\nCategory: {query[i].CategoryName}");
                                    Console.WriteLine("Products: ");
                                    categoryID = query[i].CategoryId;
                                    var products = db.Products.Where(p => p.CategoryId == categoryID && p.Discontinued == false).ToList();

                                    for (int j = 0; j < products.Count(); j++)
                                    {
                                        Console.WriteLine($"{products[j].ProductName}");
                                    }
                                }
                                
                            }
                            //display specific category
                            else if(choice == "3")
                            {
                                Console.WriteLine("\n1)Search by category name");
                                Console.WriteLine("2)Search by category ID");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} selected on how user wants to search for category to display menu");
                                Console.Clear();

                                //verify user input
                                while(!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\n Invalid selection please try again");
                                    Console.WriteLine("\n1)Search by category name");
                                    Console.WriteLine("2)Search by category ID");
                                    choice = Console.ReadLine();
                                    logger.Info($"{choice} selected on how user wants to search for category to display menu, previouse selection invalid");
                                    Console.Clear();
                                }

                                //search by name
                                if(choice == "1")
                                {
                                    Console.WriteLine("\nEnter desired categories name: ");
                                    userInput = Console.ReadLine();
                                    logger.Info($"{userInput} entered as the name of the category to be displayed");
                                    Console.Clear();

                                    //check user ented a valid name
                                    var query = db.Categories.Any(c => c.CategoryName.ToLower() == userInput.ToLower());

                                    while (!query)
                                    {
                                        Console.WriteLine("\n That is an invalid Category name please try again");
                                        Console.WriteLine("\nEnter desired categories name: ");
                                        userInput = Console.ReadLine();
                                        logger.Info($"{userInput} entered as the name of the category to be displayed, prevoise input was invalid");
                                        Console.Clear();
                                    }

                                    var desiredCategory = db.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == userInput.ToLower());
                                    var connectedProducts = db.Products.Where(p => p.CategoryId == desiredCategory.CategoryId  && p.Discontinued == false);

                                    Console.WriteLine($"Category: {desiredCategory.CategoryName}");

                                    foreach(var item in connectedProducts)
                                    {
                                        Console.WriteLine($"{item.ProductName}");
                                    }

                                    


                                }
                                //search by ID
                                else if(choice == "2")
                                {
                                    Console.WriteLine("\nEnter desired categories ID: ");
                                    userInput = Console.ReadLine();
                                    userInt = category.validateInt(userInput);
                                    logger.Info($"{userInput} entered as the ID of the category to be displayed");
                                    Console.Clear();

                                    //check user ented a valid name
                                    var query = db.Categories.Any(c => c.CategoryId == userInt);

                                    while (!query)
                                    {
                                        Console.WriteLine("\n That is an invalid Category ID please try again");
                                        Console.WriteLine("\nEnter desired categories ID: ");
                                        userInput = Console.ReadLine();
                                        userInt = category.validateInt(userInput);
                                        logger.Info($"{userInput} entered as the ID of the category to be displayed, prevoise input was invalid");
                                        Console.Clear();
                                    }

                                    var desiredCategory = db.Categories.FirstOrDefault(c => c.CategoryId == userInt);
                                    var connectedProducts = db.Products.Where(p => p.CategoryId == desiredCategory.CategoryId && p.Discontinued == false);

                                    Console.WriteLine($"Category: {desiredCategory.CategoryName}");

                                    foreach (var item in connectedProducts)
                                    {
                                        Console.WriteLine($"{item.ProductName}");
                                    }
                                }
                            }
                        }
                        //edit categories
                        else if(choice == "3")
                        {
                            Console.WriteLine("\n1) Find Category by name");
                            Console.WriteLine("2) Find Category by Id");
                            choice = Console.ReadLine();
                            logger.Info($"{choice} was ented for the menu deciding how they want to search for a category to edit");
                            Console.Clear();

                            //validate user input
                            while(!(choice == "1" || choice == "2"))
                            {
                                Console.WriteLine("\nInvalid selection please try again");
                                Console.WriteLine("\n1) Find Category by name");
                                Console.WriteLine("2) Find Category by Id");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} was ented for the menu deciding how they want to search for a category to edit, previouse selection invalid");
                                Console.Clear();
                            }

                            //search by name
                            if(choice == "1")
                            {
                                Console.WriteLine("\nEnter name of desired Category");
                                userInput = Console.ReadLine();
                                logger.Info($"{userInput} is the category to user would like to edit");
                                Console.Clear();

                                //validate category name
                                var query = db.Categories.Any(c => c.CategoryName.ToLower() == userInput.ToLower());
                                while (!query)
                                {
                                    Console.WriteLine("\n That is not a valid Category name please try again");
                                    Console.WriteLine("\nEnter name of desired Category");
                                    userInput = Console.ReadLine();
                                    logger.Info($"{userInput} is the category to user would like to edit, previose name invalid");
                                    query = db.Categories.Any(c => c.CategoryName.ToLower() == userInput.ToLower());
                                    Console.Clear();
                                }

                                //assign category to local category variable so it can be worked with
                                var desiredCategory = db.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == userInput.ToLower());
                                category = desiredCategory;

                                Console.WriteLine($"\nCategory Name: {category.CategoryName}");
                                Console.WriteLine("Edit (Y/N)");
                                userInput = Console.ReadLine();
                                if(userInput.ToLower() == "y")
                                {
                                    Console.WriteLine("New Category Name: ");
                                    userInput = Console.ReadLine();
                                    category.CategoryName = userInput;

                                }

                                Console.WriteLine($"\nCategory Description: {category.Description}");
                                Console.WriteLine("Edit (Y/N)");
                                userInput = Console.ReadLine();
                                if(userInput.ToLower() == "y")
                                {
                                    Console.WriteLine("New Description: ");
                                    userInput = Console.ReadLine();
                                    category.Description = userInput;
                                }

                                Console.WriteLine($"\nCategory {category.CategoryId} {category.CategoryName} has been edited");
                                logger.Info($"\nCategory {category.CategoryId} {category.CategoryName} has been edited");

                                db.Categories.Remove(desiredCategory);
                                db.Categories.Add(category);
                                db.SaveChanges();
                            }
                            //search byId
                            else if(choice == "2")
                            {
                                Console.WriteLine("\nEnter Id of desired Category");
                                userInput = Console.ReadLine();
                                userInt = category.validateInt(userInput);
                                logger.Info($"{userInput} is the category ID the user would like to edit");
                                Console.Clear();

                                //validate category name
                                var query = db.Categories.Any(c => c.CategoryId == userInt);
                                while (!query)
                                {
                                    Console.WriteLine("\n That is not a valid Category Id please try again");
                                    Console.WriteLine("\nEnter Id of desired Category");
                                    userInput = Console.ReadLine();
                                    userInt = category.validateInt(userInput);
                                    logger.Info($"{userInput} is the category id the user would like to edit, previose name invalid");
                                    Console.Clear();
                                    query = db.Categories.Any(c => c.CategoryId == userInt);
                                }

                                //assign category to local category variable so it can be worked with
                                var desiredCategory = db.Categories.FirstOrDefault(c => c.CategoryId == userInt);
                                category = desiredCategory;

                                Console.WriteLine($"\nCategory Name: {category.CategoryName}");
                                Console.WriteLine("Edit (Y/N)");
                                userInput = Console.ReadLine();
                                if (userInput.ToLower() == "y")
                                {
                                    Console.WriteLine("New Category Name: ");
                                    userInput = Console.ReadLine();
                                    category.CategoryName = userInput;

                                }

                                Console.WriteLine($"\nCategory Description: {category.Description}");
                                Console.WriteLine("Edit (Y/N)");
                                userInput = Console.ReadLine();
                                if (userInput.ToLower() == "y")
                                {
                                    Console.WriteLine("New Description: ");
                                    userInput = Console.ReadLine();
                                    category.Description = userInput;
                                }

                                Console.WriteLine($"\nCategory {category.CategoryId} {category.CategoryName} has been edited");
                                logger.Info($"\nCategory {category.CategoryId} {category.CategoryName} has been edited");

                                db.Categories.Remove(desiredCategory);
                                db.Categories.Add(category);
                                db.SaveChanges();
                            }
                        }
                        //delete category
                        else if(choice == "4")
                        {
                            Console.WriteLine("\n1) Find Category by Name");
                            Console.WriteLine("2) Find Categoyry by Id");
                            choice = Console.ReadLine();
                            logger.Info($"{choice} entered on the menu for how the user wants to search for the category to delete");
                            Console.Clear();


                            //validate choice
                            while(!(choice == "1" || choice == "2"))
                            {
                                Console.WriteLine("\n Invalid selection please try agian");
                                Console.WriteLine("\n1) Find Category by Name");
                                Console.WriteLine("2) Find Categoyry by Id");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} entered on the menu for how the user wants to search for the category to delete, previouse selection invalid");
                                Console.Clear();
                            }

                            //user wants to search by name
                            if(choice == "1")
                            {
                                Console.WriteLine("\nCategory Name: ");
                                userInput = Console.ReadLine();
                                logger.Info($"{userInput} is the category name of the category the user wants to delete");

                                //validate name
                                var query = db.Categories.Any(c => c.CategoryName.ToLower() == userInput.ToLower());
                                while (!query)
                                {
                                    Console.WriteLine("\nInvalid Category Name, please try again");
                                    Console.WriteLine("\nCategory Name: ");
                                    userInput = Console.ReadLine();
                                    logger.Info($"{userInput} is the category name of the category the user wants to delete, previouse selection invalid");
                                    query = db.Categories.Any(c => c.CategoryName.ToLower() == userInput.ToLower());
                                }

                                category = db.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == userInput.ToLower());

                                Console.WriteLine($"\n{category.CategoryName}");
                                Console.WriteLine("1) This is the right category");
                                Console.WriteLine("2) No longer want to delete this category");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} is the users response to weather or not the still want to delete the category");
                                Console.Clear();

                                //validate choice
                                while(!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\n Invalid choice please try again");
                                    Console.WriteLine($"\n{category.CategoryName}");
                                    Console.WriteLine("1) This is the right product");
                                    Console.WriteLine("2) No longer want to delete this product");
                                    choice = Console.ReadLine();
                                    logger.Info($"{choice} is the users response to weather or not the still want to delete the category, previouse selection invalid");
                                    Console.Clear();
                                }

                                //delete cateory
                                if(choice == "1")
                                {
                                    //remove category
                                    db.Categories.Remove(category);

                                    logger.Info($"{category.CategoryName} deleted from the database along with its products");
                                    //remove categories products
                                    var associatedProducts = db.Products.Where(p => p.CategoryId == category.CategoryId);
                                    foreach(var item in associatedProducts)
                                    {
                                        db.Products.Remove(item);
                                        logger.Info($"{item.ProductName} removed from database");
                                    }

                                    
                                    db.SaveChanges();
                                }
                                //do not delete category
                                else if(choice == "2")
                                {
                                    logger.Info($"User decided not to delete {category.CategoryName} and its associated products");
                                }
                            }
                            //user wants to search by id
                            else if(choice == "2")
                            {
                                Console.WriteLine("\nCategory Id: ");
                                userInput = Console.ReadLine();
                                userInt = category.validateInt(userInput);
                                logger.Info($"{userInput} is the category Id of the category the user wants to delete");

                                //validate Id
                                var query = db.Categories.Any(c => c.CategoryId == userInt);
                                while (!query)
                                {
                                    Console.WriteLine("\nInvalid Category Id, please try again");
                                    Console.WriteLine("\nCategory Id: ");
                                    userInput = Console.ReadLine();
                                    userInt = category.validateInt(userInput);
                                    logger.Info($"{userInput} is the category Id of the category the user wants to delete, previouse selection invalid");
                                    query = db.Categories.Any(c => c.CategoryId == userInt);
                                }

                                category = db.Categories.FirstOrDefault(c => c.CategoryId == userInt);

                                Console.WriteLine($"\n{category.CategoryName}");
                                Console.WriteLine("1) This is the right category");
                                Console.WriteLine("2) No longer want to delete this category");
                                choice = Console.ReadLine();
                                logger.Info($"{choice} is the users response to weather or not the still want to delete the category");
                                Console.Clear();

                                //validate choice
                                while (!(choice == "1" || choice == "2"))
                                {
                                    Console.WriteLine("\n Invalid choice please try again");
                                    Console.WriteLine($"\n{category.CategoryName}");
                                    Console.WriteLine("1) This is the right product");
                                    Console.WriteLine("2) No longer want to delete this product");
                                    choice = Console.ReadLine();
                                    logger.Info($"{choice} is the users response to weather or not the still want to delete the category, previouse selection invalid");
                                    Console.Clear();
                                }

                                //delete cateory
                                if (choice == "1")
                                {
                                    //remove category
                                    db.Categories.Remove(category);

                                    logger.Info($"{category.CategoryName} deleted from the database along with its products");
                                    //remove categories products
                                    var associatedProducts = db.Products.Where(p => p.CategoryId == category.CategoryId);
                                    foreach (var item in associatedProducts)
                                    {
                                        db.Products.Remove(item);
                                        logger.Info($"{item.ProductName} removed from database");
                                    }


                                    db.SaveChanges();
                                }
                                //do not delete category
                                else if (choice == "2")
                                {
                                    logger.Info($"User decided not to delete {category.CategoryName} and its associated products");
                                }
                            }
                        }





                    }
                    
                    

                } while (choice.ToLower() != "q");

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}

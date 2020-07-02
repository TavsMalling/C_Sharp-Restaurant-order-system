using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afleveringsopgave_2
{
    class Program
    {
        /* Lav et program, der fungerer som menukort:

         Burger: 33,-

         Pizza: 65,-

         Fritter: 20,-

         Dyppelse til fritter: 10,-

           - Mayo

           - Remo

           - Ketchup

         Fadøl: 30,-

         Fadøl, lux: 40,-

         Sodavand:

        50cl : 20,-

        25cl: 12,-

           - Jolly Cola

           - Tuborg Squash

           - Faxe kondi

        Brugeren skal kunne sammensætte sin egen ordre vha menuvalg og få en regning hvor der står hvad han har bestilt og hvad den samlede pris er.

        Funktioner/metoder er obligatoriske.

        Arrays kan være en stor hjælp.*/


        public enum MenuItems
        {
            /*This enum contains all the names on the menu and their numeric values*/

            Burger = 1,
            Pizza,
            Fries,
            Mayonnaise = 10,
            Remoulade,
            Ketchup,
            Pint = 20,
            Luxury_Pint,
            Jolly_Cola,
            Tuborg_Squash,
            Faxe_Kondi
        }
        public struct Prices
        {

            public MenuItems item;
            public int price;
            public int qty;
        }

        static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
            {
                MenuGraphics();
                List<int> inputQuantity = MenuInputQuantity();
                List<bool> size = MenuInputSize();
                int total = GetPrice(inputQuantity, size);
                Console.Clear();
                ReceiptBorder();
                ReceiptText(inputQuantity, size, total);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Do you want to place another order? y/n");
                char exitKey = Console.ReadKey().KeyChar;
                if (exitKey != 'y')
                {
                    exit = true;
                }
            }


        }

        static void MenuGraphics()
        {
            //This is a collection of void methods that make up the static parts of the menu
            MenuBorder();
            MenuTextLeft();
            MenuTextRight();
        }
        static void MenuTextLeft()
        {

            /*This method prints the names of the items and their price*/
            List<Prices> itemPrice = StructPrices();
            string title = "Doc's Quick and Dirty";
            Console.SetCursorPosition((61 / 2) - (title.Length / 2), 1);
            Console.Write(title);

            Console.SetCursorPosition(1, 3);
            Console.Write("FOOD......................DKK");//21 .s 1-29 range
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(1, 4 + i);
                Console.Write("{0}", itemPrice[i].item);
                //The line below calculates how many '.'s to place with the use of a known lenghth and the lengths of the price and names of the items
                //The same equation is used a few times in this method.
                for (int u = 0; u < (30 - (Convert.ToString(itemPrice[i].price).Length + Convert.ToString(itemPrice[i].item).Length)); u++)
                {
                    Console.Write('.');
                }

                Console.SetCursorPosition(30 - Convert.ToString(itemPrice[i].price).Length, 4 + i);
                Console.Write("{0}", itemPrice[i].price);
            }

            Console.SetCursorPosition(1, 8);
            Console.Write("SAUCES....................DKK");//21 .s 1-29 range
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(1, 6 + i);
                Console.Write("{0}", itemPrice[i].item);

                for (int u = 0; u < (30 - (Convert.ToString(itemPrice[i].price).Length + Convert.ToString(itemPrice[i].item).Length)); u++)
                {
                    Console.Write('.');
                }

                Console.SetCursorPosition(30 - Convert.ToString(itemPrice[i].price).Length, 6 + i);
                Console.Write("{0}", itemPrice[i].price);
            }

            Console.SetCursorPosition(1, 13);
            Console.Write("DRINKS....................DKK");//21 .s 1-29 range
            for (int i = 6; i < 11; i++)
            {
                Console.SetCursorPosition(1, 8 + i);
                Console.Write("{0}", (Convert.ToString(itemPrice[i].item)).Replace('_', ' '));

                for (int u = 0; u < (30 - (Convert.ToString(itemPrice[i].price).Length + Convert.ToString(itemPrice[i].item).Length)); u++)
                {
                    Console.Write('.');
                }

                if (i > 7)
                {
                    Console.SetCursorPosition((30 - Convert.ToString(itemPrice[i].price).Length) - 3, 8 + i);
                    Console.Write("{0}/20", itemPrice[i].price);
                }
                else
                {
                    Console.SetCursorPosition(30 - Convert.ToString(itemPrice[i].price).Length, 8 + i);
                    Console.Write("{0}", itemPrice[i].price);
                }


            }

        }
        static void MenuTextRight()
        {
            int borderStart = 31;
            int borderEnd = 45;
            int borderStart2 = 47;
            int borderEnd2 = 59;

            /*With the help of known borders I place strings in the middle of a self made section in the border*/
            Console.SetCursorPosition((borderStart + (borderEnd - borderStart) / 2) - (("QUANTITY").Length / 2), 3);
            Console.Write("QUANTITY");
            Console.SetCursorPosition((borderStart2 + (borderEnd2 - borderStart2) / 2) - (("SIZE - S/L").Length / 2), 3);
            Console.Write("SIZE - S/L");



            for (int i = 4; i < 16; i++)
            {
                Console.SetCursorPosition((borderStart2 + (borderEnd2 - borderStart2) / 2) - 1, i);

                if ((i == 7 || i == 8 || i == 12 || i == 13))
                {
                    continue;
                }
                else
                {
                    Console.Write("N/A");
                }
            }
        }
        static List<int> MenuInputQuantity()
        {
            int borderStart = 31;
            int borderEnd = 45;
            List<int> quantityChoices = new List<int>();

            /*I create a list to store user input from the menu where they choose how many they want of each item.
             If the input isn't a number it gets erased and the user can either press enter to skip or type a real number.*/

            for (int i = 4; i < 19
                ; i++)
            {
                if ((i == 7 || i == 8 || i == 12 || i == 13))
                {
                    continue;
                }

                Console.SetCursorPosition((borderStart + (borderEnd - borderStart) / 2) - 1, i);

                string quantity = Console.ReadLine();
                bool validInput = int.TryParse(quantity, out int userInput);
                if (quantity == "")
                {
                    Console.SetCursorPosition((borderStart + (borderEnd - borderStart) / 2) - 1, i);
                    Console.Write("0");
                    userInput = 0;

                }
                if (validInput != true && quantity.Length != 0)
                {
                    Console.SetCursorPosition(borderStart, i);
                    Console.Write("              ");
                    i--;
                    continue;

                }
                else
                {
                    quantityChoices.Add(userInput);

                }


            }

            return quantityChoices;
            /*Make list that gets filled with 0 automatically if you don't chooose that food, use 
            * the 0 as quantity and use that to calculate the price for the food.*/
        }
        static List<bool> MenuInputSize()
        {
            /*I create a boolean list which will contain the bool for if the user wants a big or a small soda, I also automatically place a capital
             * 'S' in case you press enter, erase non-useable input and .ToUpper() and print the input is it's either 's' or 'l'*/
            List<bool> sizeChoices = new List<bool>();

            int borderStart2 = 46;
            int borderEnd2 = 59;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition((borderStart2 + (borderEnd2 - borderStart2) / 2) + 1, i + 16);
                string size = (Console.ReadLine()).ToLower();


                if (size.Length == 0)
                {
                    Console.SetCursorPosition((borderStart2 + (borderEnd2 - borderStart2) / 2) + 1, i + 16);
                    Console.Write("S");
                    size = "s";

                }

                if (size != "l" && size != "s")
                {
                    i--;
                    Console.SetCursorPosition(borderStart2, i + 17);
                    Console.Write("              ");
                    continue;
                }

                if (size == "l")
                {
                    Console.SetCursorPosition((borderStart2 + (borderEnd2 - borderStart2) / 2) + 1, i + 16);
                    Console.Write("L");
                    sizeChoices.Add(true);
                }
                else
                {
                    Console.SetCursorPosition((borderStart2 + (borderEnd2 - borderStart2) / 2) + 1, i + 16);
                    Console.Write("S");
                    sizeChoices.Add(false);
                }
            }
            return sizeChoices;
        }
        static void MenuBorder()
        {
            //Tons of loops and setCursorPosition to draw the border for the menu
            for (int i = 0; i < 18; i++)
            {

                Console.SetCursorPosition(0, i + 1);
                Console.Write("│");
            }

            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(60, i + 1);
                Console.Write("│");
            }

            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(3 * (61 / 4), i + 3);
                Console.Write("│");
            }

            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(61 / 2, i + 3);
                Console.Write("│");
            }


            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            for (int i = 0; i < 59; i++)
            {
                Console.SetCursorPosition(i + 1, 0);
                Console.Write("─");
            }
            Console.Write("┐");

            Console.SetCursorPosition(0, 2);
            Console.Write("├");
            for (int i = 0; i < 59; i++)
            {
                Console.SetCursorPosition(i + 1, 2);
                Console.Write("─");
            }
            Console.Write("┤");

            Console.SetCursorPosition(61 / 2, 2);
            Console.Write("┬");

            Console.SetCursorPosition(3 * (61 / 4), 2);
            Console.Write("┬");


            Console.SetCursorPosition(0, 19);
            Console.Write("└");
            for (int i = 0; i < 59; i++)
            {
                Console.SetCursorPosition(i + 1, 19);
                Console.Write("─");
            }
            Console.Write("┘");

            Console.SetCursorPosition(61 / 2, 19);
            Console.Write("┴");

            Console.SetCursorPosition(3 * (61 / 4), 19);
            Console.Write("┴");
            Console.SetCursorPosition(0, 2);

        }
        static void ReceiptBorder()
        {
            /*The border for the receipt, loops, Writes and setCursor.*/

            for (int i = 0; i < 26; i++)
            {

                Console.SetCursorPosition(0, i + 1);
                Console.Write("│");
            }

            for (int i = 0; i < 26; i++)
            {
                Console.SetCursorPosition(30, i + 1);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            for (int i = 0; i < 29; i++)
            {
                Console.SetCursorPosition(i + 1, 0);
                Console.Write("─");
            }
            Console.Write("┐");

            Console.SetCursorPosition(0, 27);
            Console.Write("└");
            for (int i = 0; i < 29; i++)
            {
                Console.SetCursorPosition(i + 1, 27);
                Console.Write("─");
            }
            Console.Write("┘");
        }
        static void ReceiptText(List<int> inputQuantity, List<bool> size, int total)
        {
            /*A static void which prints all the text for the receipt, getting the information from 3 different list from accross the program*/
            List<Prices> prices = StructPrices();

            int borderStart = 0;
            int borderEnd = 30;
            Console.SetCursorPosition((borderEnd / 2) - ((("RECEIPT").Length) / 2), 1);
            Console.Write("RECEIPT");
            int o = 0;
            for (int i = 0; i < inputQuantity.Count; i++)
            {

                if (inputQuantity[i] != 0)
                {
                    Console.SetCursorPosition(borderStart + 1, o + 2);
                    Console.Write("{0} x{1}", prices[i].item.ToString().Replace('_', ' '), inputQuantity[i]);
                    int priceItem = prices[i].price * inputQuantity[i];


                    for (int u = 0; u < (29 - (((Convert.ToString(prices[i].item)).Length) + ((Convert.ToString(priceItem)).Length)) - 2); u++)
                    {
                        Console.Write(".");
                    }
                    Console.SetCursorPosition(borderEnd - ((Convert.ToString(priceItem)).Length), o + 2);

                    if ((i == 8 || i == 9 || i == 10) && size[i - 8] == true)
                    {
                        priceItem = 20;
                    }

                    Console.Write("{0}", priceItem);
                    o++;
                }


            }

            Console.SetCursorPosition(borderStart + 1, 26);
            Console.Write("TOTAL");
            for (int u = 0; u < (30 - (("TOTAL").Length + Convert.ToString(total).Length)); u++)
            {
                Console.Write(".");
            }
            Console.SetCursorPosition(borderEnd - ((Convert.ToString(total).Length)), 26);
            Console.Write("{0}", total);
        }
        static List<Prices> StructPrices()
        {
            /*A list of structs that contain the name and price of everything in the menu, it returns the list to anywhere that calls the method*/

            List<Prices> itemPrice = new List<Prices>();

            Prices Pizza;
            Pizza.item = MenuItems.Pizza;
            Pizza.price = 65;
            Pizza.qty = 0;
            itemPrice.Add(Pizza);

            Prices Burger;
            Burger.item = MenuItems.Burger;
            Burger.price = 35;
            Burger.qty = 0;
            itemPrice.Add(Burger);

            Prices Fries;
            Fries.item = MenuItems.Fries;
            Fries.price = 20;
            Fries.qty = 0;
            itemPrice.Add(Fries);

            Prices Mayonnaise;
            Mayonnaise.item = MenuItems.Mayonnaise;
            Mayonnaise.price = 10;
            Mayonnaise.qty = 0;
            itemPrice.Add(Mayonnaise);

            Prices Remoulade;
            Remoulade.item = MenuItems.Remoulade;
            Remoulade.price = 10;
            Remoulade.qty = 0;
            itemPrice.Add(Remoulade);

            Prices Ketchup;
            Ketchup.item = MenuItems.Ketchup;
            Ketchup.price = 10;
            Ketchup.qty = 0;
            itemPrice.Add(Ketchup);

            Prices Pint;
            Pint.item = MenuItems.Pint;
            Pint.price = 30;
            Pint.qty = 0;
            itemPrice.Add(Pint);

            Prices Luxury_Pint;
            Luxury_Pint.item = MenuItems.Luxury_Pint;
            Luxury_Pint.price = 40;
            Luxury_Pint.qty = 0;
            itemPrice.Add(Luxury_Pint);

            Prices Jolly_Cola;
            Jolly_Cola.item = MenuItems.Jolly_Cola;
            Jolly_Cola.price = 12;
            Jolly_Cola.qty = 0;
            itemPrice.Add(Jolly_Cola);

            Prices Tuborg_Squash;
            Tuborg_Squash.item = MenuItems.Tuborg_Squash;
            Tuborg_Squash.price = 12;
            Tuborg_Squash.qty = 0;
            itemPrice.Add(Tuborg_Squash);

            Prices Faxe_Kondi;
            Faxe_Kondi.item = MenuItems.Faxe_Kondi;
            Faxe_Kondi.price = 12;
            Faxe_Kondi.qty = 0;
            itemPrice.Add(Faxe_Kondi);

            return itemPrice;
        }
        static int GetPrice(List<int> inputQuantity, List<bool> size)
        {
            /*Calculates the total price of the order and returns it to main.*/
            List<Prices> itemPrice = StructPrices();

            int total = 0;
            for (int i = 0; i < itemPrice.Count; i++)
            {
                if (inputQuantity[i] != 0)
                {
                    if ((i == 8 || i == 9 || i == 10) && size[i - 8] == true)
                    {
                        total += 20 * inputQuantity[i];
                    }
                    else
                    {
                        total += itemPrice[i].price * inputQuantity[i];
                    }
                }

            }
            return total;

        }


    }
}


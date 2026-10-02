using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using L2O___D09;

namespace Lab2_EF
{
    public class CaseInsensitiveComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }
    }
    public class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return GetCanonicalString(x) == GetCanonicalString(y);
        }

        public int GetHashCode(string obj)
        {
            return GetCanonicalString(obj).GetHashCode();
        }

        private string GetCanonicalString(string word)
        {
            char[] wordChars = word.Trim().ToCharArray();
            Array.Sort(wordChars);
            return new string(wordChars);
        }
    }

    public static class Program
    {
        private static string[] Dictionary;

        public static void Main(string[] args)
        {
            Dictionary = File.ReadAllLines("dictionary_english.txt");

            RestrictionOperators();
            ElementOperators();
            SetOperators();
            AggregateOperators();
            OrderingOperators();
            PartitioningOperators();
            ProjectionOperators();
            Quantifiers();
            GroupingOperators();

            Console.WriteLine();
        }

        #region Q1
        private static void RestrictionOperators()
        {
            Console.WriteLine("Restriction Operators");

            // 1. Find all products that are out of stock.
            Console.WriteLine("1. Products out of stock");
            var outOfStock =
                from p in ListGenerators.ProductList
                where p.UnitsInStock == 0
                select p;
            foreach (var p in outOfStock)
                Console.WriteLine(p);

            // 2. Find all products that are in stock and cost more than 3.00 per unit.
            Console.WriteLine("2. In stock, price > 3.00");
            var inStockExpensive =
                from p in ListGenerators.ProductList
                where p.UnitsInStock > 0 && p.UnitPrice > 3.00M
                select p;
            foreach (var p in inStockExpensive)
                Console.WriteLine(p);

            // 3. Return digits whose name is shorter than their value.
            Console.WriteLine("3. Digits shorter than their value");
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortDigits =
                digits.Where((digit, index) => digit.Length < index);
            foreach (var d in shortDigits)
                Console.WriteLine(d);
        }
        #endregion

        #region Q2
        private static void ElementOperators()
        {
            Console.WriteLine("Element Operators");

            // 1. Get first Product out of Stock
            Console.WriteLine("1. First product out of stock");
            var firstOutOfStock =
                (from p in ListGenerators.ProductList
                 where p.UnitsInStock == 0
                 select p).First();
            Console.WriteLine(firstOutOfStock);

            // 2. First product whose Price > 1000, or null if there is no match.
            Console.WriteLine("2. First product with Price > 1000 / null");
            var firstExpensive =
                (from p in ListGenerators.ProductList
                 where p.UnitPrice > 1000
                 select p).FirstOrDefault();
            Console.WriteLine(firstExpensive == null ? "null" : firstExpensive.ToString());

            // 3. Retrieve the second number greater than 5.
            Console.WriteLine("3. Second number greater than 5");
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThan5 =
                (from n in arr
                 where n > 5
                 select n).ElementAt(1); //skip 
            Console.WriteLine(secondGreaterThan5);
        }
        #endregion

        #region Q3
        private static void SetOperators()
        {
            Console.WriteLine("Set Operators");

            // 1. Find the unique Category names from Product List
            Console.WriteLine("1. Unique category names");
            var categories =
                (from p in ListGenerators.ProductList
                 select p.Category).Distinct();
            foreach (var c in categories)
                Console.WriteLine(c);

            var productFirstLetters = ListGenerators.ProductList.Select(p => p.ProductName[0]);
            var customerFirstLetters = ListGenerators.CustomerList.Select(c => c.CompanyName[0]);

            // 2. Unique first letter from both product and customer names.
            Console.WriteLine("2. Unique first letters");
            var unionLetters = productFirstLetters.Union(customerFirstLetters);
            foreach (var letter in unionLetters)
                Console.WriteLine(letter);

            // 3. Common first letter from both product and customer names.
            Console.WriteLine("3. Common first letters");
            var commonLetters = productFirstLetters.Intersect(customerFirstLetters);
            foreach (var letter in commonLetters)
                Console.WriteLine(letter);

            // 4. First letters of product names not also first letters of customer names.
            Console.WriteLine("4. Product-only first letters");
            var productOnlyLetters = productFirstLetters.Except(customerFirstLetters);
            foreach (var letter in productOnlyLetters)
                Console.WriteLine(letter);

            // 5. Last three characters of every product and customer name (with duplicates).
            Console.WriteLine("5. Last three characters of names");
            var productLastThree = ListGenerators.ProductList.Select(p => p.ProductName.Substring(p.ProductName.Length - 3));
            var customerLastThree = ListGenerators.CustomerList.Select(c => c.CompanyName.Substring(c.CompanyName.Length - 3));
            var allLastThree = productLastThree.Concat(customerLastThree);
            foreach (var s in allLastThree)
                Console.WriteLine(s);
        }
        #endregion

        #region Q4
        private static void AggregateOperators()
        {
            Console.WriteLine("Aggregate Operators");

            // 1. Use Count to get the number of odd numbers in the array.
            Console.WriteLine("1. Count of odd numbers");
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddCount = numbers.Count(n => n % 2 != 0);
            Console.WriteLine(oddCount);

            // 2. List of customers and how many orders each has.
            Console.WriteLine("2. Orders per customer");
            var ordersPerCustomer =
                from c in ListGenerators.CustomerList
                select new { c.CompanyName, OrderCount = c.Orders.Length };
            foreach (var c in ordersPerCustomer)
                Console.WriteLine($"{c.CompanyName}: {c.OrderCount} orders");

            // 3. List of categories and how many products each has.
            Console.WriteLine("3. Products per category");
            var productsPerCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                select new { Category = g.Key, ProductCount = g.Count() };
            foreach (var c in productsPerCategory)
                Console.WriteLine($"{c.Category}: {c.ProductCount} products");

            // 4. Get the total of the numbers in an array.
            Console.WriteLine("4. Total of array numbers");
            Console.WriteLine(numbers.Sum());

            // 5. Total number of characters of all words in dictionary_english.txt.
            Console.WriteLine("5. Total characters in dictionary");
            var words = Dictionary;
            Console.WriteLine(words.Sum(w => w.Length));

            // 6. Total units in stock for each product category.
            Console.WriteLine("6. Total units in stock by category");
            var stockPerCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                select new { Category = g.Key, TotalUnitsInStock = g.Sum(p => p.UnitsInStock) };
            foreach (var c in stockPerCategory)
                Console.WriteLine($"{c.Category}: {c.TotalUnitsInStock} units");

            // 7. Length of the shortest word in dictionary_english.txt.
            Console.WriteLine("7. Shortest word length");
            Console.WriteLine(words.Min(w => w.Length));

            // 8. Cheapest price among each category's products.
            Console.WriteLine("8. Cheapest price by category");
            var cheapestByCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                select new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) };
            foreach (var c in cheapestByCategory)
                Console.WriteLine($"{c.Category}: {c.CheapestPrice:C}");

            // 9. Products with the cheapest price in each category (using let).
            Console.WriteLine("9. Cheapest products by category");
            var cheapestProductsByCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                let cheapestPrice = g.Min(p => p.UnitPrice)
                select new
                {
                    Category = g.Key,
                    CheapestProducts = g.Where(p => p.UnitPrice == cheapestPrice)
                };
            foreach (var c in cheapestProductsByCategory)
            {
                Console.WriteLine(c.Category + ":");
                foreach (var p in c.CheapestProducts)
                    Console.WriteLine("   " + p);
            }

            // 10. Length of the longest word in dictionary_english.txt.
            Console.WriteLine("10. Longest word length");
            Console.WriteLine(words.Max(w => w.Length));

            // 11. Most expensive price among each category's products.
            Console.WriteLine("11. Most expensive price by category");
            var mostExpensiveByCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                select new { Category = g.Key, MostExpensivePrice = g.Max(p => p.UnitPrice) };
            foreach (var c in mostExpensiveByCategory)
                Console.WriteLine($"{c.Category}: {c.MostExpensivePrice:C}");

            // 12. Products with the most expensive price in each category.
            Console.WriteLine("12. Most expensive products by category");
            var mostExpensiveProductsByCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                let maxPrice = g.Max(p => p.UnitPrice)
                select new
                {
                    Category = g.Key,
                    MostExpensiveProducts = g.Where(p => p.UnitPrice == maxPrice)
                };
            foreach (var c in mostExpensiveProductsByCategory)
            {
                Console.WriteLine(c.Category + ":");
                foreach (var p in c.MostExpensiveProducts)
                    Console.WriteLine("   " + p);
            }

            // 13. Average length of the words in dictionary_english.txt.
            Console.WriteLine("13. Average word length");
            Console.WriteLine(words.Average(w => w.Length));

            // 14. Average price of each category's products.
            Console.WriteLine("14. Average price by category");
            var averagePriceByCategory =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                select new { Category = g.Key, AveragePrice = g.Average(p => p.UnitPrice) };
            foreach (var c in averagePriceByCategory)
                Console.WriteLine($"{c.Category}: {c.AveragePrice:C}");
        }
        #endregion

        #region Q5
        private static void OrderingOperators()
        {
            Console.WriteLine("Ordering Operators");

            // 1. Sort a list of products by name.
            Console.WriteLine("1. Products sorted by name");
            var sortedByName =
                from p in ListGenerators.ProductList
                orderby p.ProductName
                select p;
            foreach (var p in sortedByName)
                Console.WriteLine(p);

            // 2. Custom comparer, case-insensitive sort of words.
            Console.WriteLine("2. Case-insensitive sort with custom comparer");
            string[] words1 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords1 = words1.OrderBy(w => w, new CaseInsensitiveComparer());
            foreach (var w in sortedWords1)
                Console.WriteLine(w);

            // 3. Sort products by units in stock, highest to lowest.
            Console.WriteLine("3. Products sorted by units in stock desc");
            var sortedByStock =
                from p in ListGenerators.ProductList
                orderby p.UnitsInStock descending
                select p;
            foreach (var p in sortedByStock)
                Console.WriteLine(p);

            // 4. Sort digits by length of name, then alphabetically.
            Console.WriteLine("4. Digits sorted by name length, then alphabetically");
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits =
                from d in digits
                orderby d.Length, d
                select d;
            foreach (var d in sortedDigits)
                Console.WriteLine(d);

            // 5. Sort by word length, then case-insensitive alphabetically.
            Console.WriteLine("5. Words sorted by length, then case-insensitive");
            string[] words2 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords2 = words2
                .OrderBy(w => w.Length)
                .ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in sortedWords2)
                Console.WriteLine(w);

            // 6. Sort products by category, then by unit price descending.
            Console.WriteLine("6. Products sorted by category, then price desc");
            var sortedByCategoryThenPrice =
                from p in ListGenerators.ProductList
                orderby p.Category, p.UnitPrice descending
                select p;
            foreach (var p in sortedByCategoryThenPrice)
                Console.WriteLine(p);

            // 7. Sort by word length, then case-insensitive descending.
            Console.WriteLine("7. Words sorted by length, then case-insensitive descending");
            string[] words3 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords3 = words3
                .OrderBy(w => w.Length)
                .ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in sortedWords3)
                Console.WriteLine(w);

            // 8. Digits whose second letter is 'i', reversed from the original order.
            Console.WriteLine("8. Digits with second letter 'i', reversed");
            string[] digits2 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var reversedIDigits =
                (from d in digits2
                 where d.Length > 1 && d[1] == 'i'
                 select d).Reverse();
            foreach (var d in reversedIDigits)
                Console.WriteLine(d);
        }
        #endregion

        #region Q6
        private static void PartitioningOperators()
        {
            Console.WriteLine("Partitioning Operators");

            var washingtonOrders =
                from c in ListGenerators.CustomerList
                where c.Region == "WA"
                from o in c.Orders
                select o;

            // 1. Get the first 3 orders from customers in Washington.
            Console.WriteLine("1. First 3 orders from customers in WA");
            foreach (var o in washingtonOrders.Take(3))
                Console.WriteLine(o);

            // 2. Get all but the first 2 orders from customers in Washington.
            Console.WriteLine("2. All but first 2 orders from customers in WA");
            foreach (var o in washingtonOrders.Skip(2))
                Console.WriteLine(o);

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 3. Elements from the start until a number less than its position is hit.
            Console.WriteLine("3. TakeWhile value >= position");
            var takenWhileInPlace = numbers.TakeWhile((n, index) => n >= index);
            foreach (var n in takenWhileInPlace)
                Console.WriteLine(n);

            // 4. Elements starting from the first element divisible by 3.
            Console.WriteLine("4. SkipWhile not divisible by 3");
            var skipUntilDivisibleBy3 = numbers.SkipWhile(n => n % 3 != 0);
            foreach (var n in skipUntilDivisibleBy3)
                Console.WriteLine(n);

            // 5. Elements starting from the first element less than its position.
            Console.WriteLine("5. SkipWhile value >= position");
            var skipWhileInPlace = numbers.SkipWhile((n, index) => n >= index);
            foreach (var n in skipWhileInPlace)
                Console.WriteLine(n);
        }
        #endregion

        #region Q7
        private static void ProjectionOperators()
        {
            Console.WriteLine("Projection Operators");

            // 1. Return a sequence of just the names of a list of products.
            Console.WriteLine("1. Product names");
            var productNames =
                from p in ListGenerators.ProductList
                select p.ProductName;
            foreach (var name in productNames)
                Console.WriteLine(name);

            // 2. Uppercase and lowercase versions of each word (anonymous types).
            Console.WriteLine("2. Upper/lower case versions of words");
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var upperLower =
                from w in words
                select new { Upper = w.ToUpper(), Lower = w.ToLower() };
            foreach (var w in upperLower)
                Console.WriteLine($"Uppercase: {w.Upper}, Lowercase: {w.Lower}");

            // 3. Some properties of Products, with UnitPrice renamed to Price.
            Console.WriteLine("3. Product projection with renamed Price");
            var productProjection =
                from p in ListGenerators.ProductList
                select new { p.ProductName, p.Category, Price = p.UnitPrice };
            foreach (var p in productProjection)
                Console.WriteLine($"{p.ProductName} ({p.Category}): {p.Price:C}");

            // 4. Determine if the value of ints in an array match their position.
            Console.WriteLine("4. In-place check");
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var inPlaceCheck =
                arr.Select((num, index) => new { Number = num, InPlace = (num == index) });
            Console.WriteLine("Number: In-place?");
            foreach (var item in inPlaceCheck)
                Console.WriteLine($"{item.Number}: {item.InPlace}");

            // 5. Pairs of numbers from both arrays such that numbersA < numbersB.
            Console.WriteLine("5. Pairs where a < b");
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs =
                from a in numbersA
                from b in numbersB
                where a < b
                select new { a, b };
            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs)
                Console.WriteLine($"{pair.a} is less than {pair.b}");

            // 6. All orders where the order total is less than 500.00.
            Console.WriteLine("6. Orders with total < 500.00");
            var smallOrders =
                from c in ListGenerators.CustomerList
                from o in c.Orders
                where o.Total < 500.00M
                select o;
            foreach (var o in smallOrders)
                Console.WriteLine(o);

            // 7. All orders where the order was made in 1998 or later.
            Console.WriteLine("7. Orders made in 1998 or later");
            var recentOrders =
                from c in ListGenerators.CustomerList
                from o in c.Orders
                where o.OrderDate.Year >= 1998
                select o;
            foreach (var o in recentOrders)
                Console.WriteLine(o);
        }
        #endregion

        #region Q8
        private static void Quantifiers()
        {
            Console.WriteLine("Quantifiers");

            // 1. Determine if any word in dictionary_english.txt contains "ei".
            Console.WriteLine("1. Any word contains 'ei'?");
            var words = Dictionary;
            bool anyContainsEi = words.Any(w => w.Contains("ei"));
            Console.WriteLine(anyContainsEi);

            // 2. Grouped products, only categories with at least one out-of-stock product.
            Console.WriteLine("2. Categories with at least one out-of-stock product");
            var categoriesWithOutOfStock =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                where g.Any(p => p.UnitsInStock == 0)
                select g;
            foreach (var g in categoriesWithOutOfStock)
            {
                Console.WriteLine(g.Key + ":");
                foreach (var p in g)
                    Console.WriteLine("   " + p);
            }

            // 3. Grouped products, only categories where all products are in stock.
            Console.WriteLine("3. Categories where all products are in stock");
            var categoriesAllInStock =
                from p in ListGenerators.ProductList
                group p by p.Category into g
                where g.All(p => p.UnitsInStock > 0)
                select g;
            foreach (var g in categoriesAllInStock)
            {
                Console.WriteLine(g.Key + ":");
                foreach (var p in g)
                    Console.WriteLine("   " + p);
            }
        }
        #endregion

        #region Q9
        private static void GroupingOperators()
        {
            Console.WriteLine("Grouping Operators");

            // 1. Partition a list of numbers by their remainder when divided by 5.
            Console.WriteLine("1. Numbers grouped by remainder mod 5");
            var numbers = Enumerable.Range(0, 15);
            var numberGroups =
                from n in numbers
                group n by n % 5 into g
                select g;
            foreach (var g in numberGroups)
            {
                Console.WriteLine($"Numbers with a remainder of {g.Key} when divided by 5:");
                foreach (var n in g)
                    Console.WriteLine(n);
            }

            // 2. Partition the words in dictionary_english.txt by their first letter.
            Console.WriteLine("2. Words grouped by first letter");
            var words = Dictionary;
            var wordGroups =
                from w in words
                group w by w[0] into g
                select g;
            foreach (var g in wordGroups)
            {
                Console.WriteLine($"Words starting with '{g.Key}':");
                foreach (var w in g)
                    Console.WriteLine(w);
            }

            // 3. Group by a custom comparer that matches words made of the same characters.
            Console.WriteLine("3. Words grouped by shared characters");
            string[] arr = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var anagramGroups = arr.GroupBy(w => w, new AnagramEqualityComparer());
            foreach (var g in anagramGroups)
            {
                foreach (var w in g)
                    Console.WriteLine(w);
                Console.WriteLine();
            }
        } 
        #endregion
    }
}

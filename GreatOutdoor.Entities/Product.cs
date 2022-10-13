using System;
using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.Entities
{
    /// <summary>
    /// Interface for Product Entity
    /// </summary>    
    public interface IProduct
    {
        Guid ProductID { get; set; }
        string ProductName { get; set; }
        Category category { get; set; }
        string ProductColor { get; set; }
        string ProductSize { get; set; }
        string ProductMaterial { get; set; }
        double ProductPrice { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }
    /// <summary>
    /// Represents Product
    /// </summary>

    public class Product : IProduct
    {
        /* Auto-Implemented Properties*/
        [Required("Product ID can't be blank!")]
        public Guid ProductID { get; set; }

        [Required("Product Name can't be blank!")]
        public string ProductName { get; set; }

        [Required("Product color can't be blank!")]
        public string ProductColor { get; set; }

        [Required("Product size can't be blank!")]
        public string ProductSize { get; set; }

        [Required("Product material can't be blank!")]
        public string ProductMaterial { get; set; }

        [Required("Product Category can't be blank!")]

        public Category category { get; set; }




        [Required("Product price can't be blank!")]
        public double ProductPrice { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }


        /* Constructor */
        public Product()
        {
            ProductID = default(Guid);
            ProductName = null;
            category = default(Category);
            ProductColor = null;
            ProductSize = null;
            ProductMaterial = null;
            ProductPrice = default(double);
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}


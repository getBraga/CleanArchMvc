﻿using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product 
    {
        public int Id { get; private set; }
        public  string Name { get; private set; } =  string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; } 
        public int Stock { get; private set; } 
        public string? Image { get; private set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        private Product() { }
           
        public Product(string name, string description, decimal price, int stock, string image)
        {
          
            ValidateDomain(name, description, price, stock, image);

            Name = name;
            Description =  description;
            Price = price;
            Image = image;
   
        
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
 
            Id = id;
           
            ValidateDomain(name, description, price, stock, image);
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        

        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;

        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid description. Description is required");

            DomainExceptionValidation.When(description.Length < 5,
                "Invalid description, too short, minimum 5 characters");

            DomainExceptionValidation.When(price < 0, "Invalid price value");

            DomainExceptionValidation.When(stock < 0, "Invalid stock value");

            DomainExceptionValidation.When(image != null && image.Length > 250,
                "Invalid image name, too long, maximum 250 characters");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;

           

        }

     
    }
}

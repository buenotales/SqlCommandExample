using SqlCommandExample.Entities;
using SqlCommandExample.Repositories;
using System;
using System.Linq;

namespace SqlCommandExample
{
    public class App : IApp
    {
        private readonly IProductRespository productRespository;

        public App(IProductRespository productRespository)
        {
            this.productRespository = productRespository;
        }

        public void Execute()
        {
            Console.WriteLine("### How to use Sql Command ###");
            Console.WriteLine("\n");


            Console.WriteLine($"Count table products: {productRespository.GetAll().Count()}\n");

            Console.WriteLine($"Inserting row ...\n");
            productRespository.Add(new ProductEntity() { Name = "Produto 1", Price = 10 });

            Console.WriteLine($"Count table products: {productRespository.GetAll().Count()}");
        }
    }
}

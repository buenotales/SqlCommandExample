using SqlCommandExample.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandExample.Repositories
{
    public class ProductRepository : IProductRespository
    {
        private readonly IDbConnection DbConnection;

        public ProductRepository()
        {
            this.DbConnection = new SqlConnection("Data Source=localhost;Initial Catalog=Learning;User ID=sa;Password=Senha@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            this.DbConnection.Open();
        }

        ~ProductRepository()
        {
            if (this.DbConnection != null)
                this.DbConnection.Close();
        }

        public ProductEntity Add(ProductEntity entity)
        {
            IDbCommand insert = DbConnection.CreateCommand();
            insert.CommandText = "INSERT INTO Products (name, price) VALUES (@name, @price);";

            IDbDataParameter paramName = new SqlParameter("name", entity.Name);
            IDbDataParameter paramPrice = new SqlParameter("price", entity.Price);

            insert.Parameters.Add(paramName);
            insert.Parameters.Add(paramPrice);

            var rowsAffected = insert.ExecuteNonQuery();

            if (rowsAffected > 0)
                return entity;

            return default;
        }

        public ProductEntity Get(int id)
        {
            var select = DbConnection.CreateCommand();
            select.CommandText = "SELECT id, name, price FROM products WHERE id = @id;";

            IDataParameter paramId = new SqlParameter("id", id);

            select.Parameters.Add(paramId);

            ProductEntity product = null;

            using (var reader = select.ExecuteReader())
                if (reader.Read())
                    product = new ProductEntity()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]),
                        Price = Convert.ToDecimal(reader["price"]),
                    };

            return product;
        }

        public IEnumerable<ProductEntity> GetAll()
        {
            ICollection<ProductEntity> list = new Collection<ProductEntity>();

            IDbCommand select = DbConnection.CreateCommand();
            select.CommandText = "SELECT id, name, price FROM products;";

            using (var reader = select.ExecuteReader())
                while (reader.Read())
                    list.Add(new ProductEntity()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]),
                        Price = Convert.ToDecimal(reader["price"]),
                    });

            return list;
        }

        public ProductEntity Update(ProductEntity entity)
        {
            IDbCommand update = DbConnection.CreateCommand();
            update.CommandText = "UPDATE Products SET name = @name, price = @price WHERE id = @id;";

            IDbDataParameter paramName = new SqlParameter("name", entity.Name);
            IDbDataParameter paramPrice = new SqlParameter("price", entity.Price);
            IDbDataParameter paramId = new SqlParameter("id", entity.Id);

            update.Parameters.Add(paramName);
            update.Parameters.Add(paramPrice);
            update.Parameters.Add(paramId);

            var rowsAffected = update.ExecuteNonQuery();

            if (rowsAffected > 0)
                return entity;

            return default;
        }

        public bool Remove(ProductEntity entity)
        {
            IDbCommand remove = DbConnection.CreateCommand();
            remove.CommandText = "DELETE FROM products WHERE id = @id;";
            IDbDataParameter paramId = new SqlParameter("id", entity.Id);

            remove.Parameters.Add(paramId);

            var rowsAffected = remove.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}

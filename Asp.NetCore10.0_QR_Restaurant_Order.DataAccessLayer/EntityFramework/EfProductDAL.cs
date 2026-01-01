using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Repositories;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.EntityFramework
{
    public class EfProductDAL : GenericRepository<Product>, IProductDAL
    {
        public EfProductDAL(SignalRContext context) : base(context)
        {
        }

        public List<Product> GetProductsWithCategories()
        {
            var context = new SignalRContext(); // DbContext'i oluştur
            var values= context.Products.Include(p => p.Category).ToList(); // Ürünleri kategorileriyle birlikte al
            return values;
        }
    }
}

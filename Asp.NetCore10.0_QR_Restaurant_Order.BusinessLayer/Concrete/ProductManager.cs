using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public void TAdd(Product t)
        {
            _productDAL.Add(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TDelete(Product t)
        {
            _productDAL.Delete(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public Product TGetByID(int id)
        {
            return _productDAL.GetByID(id); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }
    
        public List<Product> TGetListAll()
        {
           return _productDAL.GetListAll(); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.)
        }

        public void TUpdate(Product t)
        {
            _productDAL.Update(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }
    }
}

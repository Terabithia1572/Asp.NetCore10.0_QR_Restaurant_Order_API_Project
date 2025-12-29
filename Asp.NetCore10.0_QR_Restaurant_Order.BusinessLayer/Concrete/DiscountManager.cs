using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class DiscountManager : IDiscountService
    {
        private readonly IDiscountDAL _discountDAL;

        public DiscountManager(IDiscountDAL discountDAL)
        {
            _discountDAL = discountDAL;
        }

        public void TAdd(Discount t)
        {
            _discountDAL.Add(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TDelete(Discount t)
        {
            _discountDAL.Delete(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public Discount TGetByID(int id)
        {
            return _discountDAL.GetByID(id); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public List<Discount> TGetListAll()
        {
            return _discountDAL.GetListAll(); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TUpdate(Discount t)
        {
            _discountDAL.Update(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }
    }
}

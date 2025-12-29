using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDAL _aboutDAL;

        public AboutManager(IAboutDAL aboutDAL)
        {
            _aboutDAL = aboutDAL;
        }

        public void TAdd(About t)
        {
            _aboutDAL.Add(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 

        }

        public void TDelete(About t)
        {
            _aboutDAL.Delete(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public About TGetByID(int id)
        {
            return _aboutDAL.GetByID(id); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public List<About> TGetListAll()
        {
            return _aboutDAL.GetListAll(); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TUpdate(About t)
        {
            _aboutDAL.Update(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }
    }
}

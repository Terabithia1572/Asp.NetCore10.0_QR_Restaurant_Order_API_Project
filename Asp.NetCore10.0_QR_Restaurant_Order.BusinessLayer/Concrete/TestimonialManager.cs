using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class TestimonialManager : ITestimonialService
    {
        private readonly ITestimonialDAL _testimonialDAL;

        public TestimonialManager(ITestimonialDAL testimonialDAL)
        {
            _testimonialDAL = testimonialDAL;
        }

        public void TAdd(Testimonial t)
        {
            _testimonialDAL.Add(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TDelete(Testimonial t)
        {
            _testimonialDAL.Delete(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public Testimonial TGetByID(int id)
        {
            return _testimonialDAL.GetByID(id); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public List<Testimonial> TGetListAll()
        {
            return _testimonialDAL.GetListAll(); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TUpdate(Testimonial t)
        {
            _testimonialDAL.Update(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }
    }
}

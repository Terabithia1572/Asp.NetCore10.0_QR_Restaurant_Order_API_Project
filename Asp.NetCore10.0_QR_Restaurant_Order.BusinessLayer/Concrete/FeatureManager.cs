using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class FeatureManager : IFeatureService
    {
        private readonly IFeatureDAL _featureDAL;

        public FeatureManager(IFeatureDAL featureDAL)
        {
            _featureDAL = featureDAL;
        }

        public void TAdd(Feature t)
        {
            _featureDAL.Add(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TDelete(Feature t)
        {
            _featureDAL.Delete(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public Feature TGetByID(int id)
        {
            return _featureDAL.GetByID(id); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public List<Feature> TGetListAll()
        {
            return _featureDAL.GetListAll(); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }

        public void TUpdate(Feature t)
        {
            _featureDAL.Update(t); // İş katmanı mantığı burada uygulanabilir (örneğin, doğrulama, iş kuralları vb.) 
        }
    }
}

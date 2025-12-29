using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        private readonly SignalRContext _context;

        public GenericRepository(SignalRContext context)
        {
            _context = context;
        }

        public void TAdd(T t)
        {
            _context.Set<T>().Add(t); // Generic olarak gelen T tipindeki varlığı ekle
            _context.SaveChanges(); // Değişiklikleri veritabanına kaydet
        }

        public void TDelete(T t)
        {
            _context.Remove(t); // Generic olarak gelen T tipindeki varlığı sil
            _context.SaveChanges(); // Değişiklikleri veritabanına kaydet
        }

        public T TGetByID(int id)
        {
            return _context.Set<T>().Find(id); // Generic olarak gelen T tipindeki varlığı ID ile bul ve döndür
        }

        public List<T> TGetListAll()
        {
           return _context.Set<T>().ToList(); // Generic olarak gelen T tipindeki tüm varlıkları listele ve döndür
        }

        public void TUpdate(T t)
        {
            _context.Update(t); // Generic olarak gelen T tipindeki varlığı güncelle
            _context.SaveChanges(); // Değişiklikleri veritabanına kaydet
        }
    }
}

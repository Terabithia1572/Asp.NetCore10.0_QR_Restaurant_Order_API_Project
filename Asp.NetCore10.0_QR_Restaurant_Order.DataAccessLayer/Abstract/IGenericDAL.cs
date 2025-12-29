using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract
{
    public interface IGenericDAL<T> where T : class
    {
        void Add(T t); // T tipinde bir parametre alır ve geri dönüş tipi yoktur. Ekleme İşlemi Yapar
        void Delete(T t); // T tipinde bir parametre alır ve geri dönüş tipi yoktur. Silme İşlemi Yapar
        void Update(T t); // T tipinde bir parametre alır ve geri dönüş tipi yoktur. Güncelleme İşlemi Yapar
        List<T> GetListAll(); // T tipinde bir liste döner. Tüm verileri listeleme işlemi yapar.
        T GetByID(int id); // int tipinde bir id parametresi alır ve T tipinde bir nesne döner. ID'ye göre veri getirme işlemi yapar.
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract
{
    public interface IGenericDAL<T> where T : class
    {
        void TAdd(T t); // T tipinde bir parametre alır ve geri dönüş tipi yoktur. Ekleme İşlemi Yapar
        void TDelete(T t); // T tipinde bir parametre alır ve geri dönüş tipi yoktur. Silme İşlemi Yapar
        void TUpdate(T t); // T tipinde bir parametre alır ve geri dönüş tipi yoktur. Güncelleme İşlemi Yapar
        List<T> TGetListAll(); // T tipinde bir liste döner. Tüm verileri listeleme işlemi yapar.
        T TGetByID(int id); // int tipinde bir id parametresi alır ve T tipinde bir nesne döner. ID'ye göre veri getirme işlemi yapar.
    }
}

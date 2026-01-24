using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;

        public OrderManager(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public void TAdd(Order t)
        {
            _orderDAL.Add(t);
        }

        public void TDelete(Order t)
        {
            _orderDAL.Delete(t);
        }

        public Order TGetByID(int id)
        {
            return _orderDAL.GetByID(id);
        }

        public List<Order> TGetListAll()
        {
            return _orderDAL.GetListAll();
        }

        public void TUpdate(Order t)
        {
            _orderDAL.Update(t);
        }

        // Eğer async özel metotların varsa:
        // public Task<int> CreateOrderAsync(CreateOrderDTO dto) { ... }
    }
}

using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDAL _orderDetailDAL;

        public OrderDetailManager(IOrderDetailDAL orderDetailDAL)
        {
            _orderDetailDAL = orderDetailDAL;
        }

        public void TAdd(OrderDetail t)
        {
           _orderDetailDAL.Add(t);
        }

        public void TDelete(OrderDetail t)
        {
            _orderDetailDAL.Delete(t);
        }

        public OrderDetail TGetByID(int id)
        {
            return _orderDetailDAL.GetByID(id);
        }

        public List<OrderDetail> TGetListAll()
        {
            return _orderDetailDAL.GetListAll();
        }

        public void TUpdate(OrderDetail t)
        {
            _orderDetailDAL.Update(t);
        }
        
    }
}

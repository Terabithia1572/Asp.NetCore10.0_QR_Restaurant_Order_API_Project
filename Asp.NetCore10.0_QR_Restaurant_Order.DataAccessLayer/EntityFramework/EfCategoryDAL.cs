using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Repositories;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.EntityFramework
{
    public class EfCategoryDAL : GenericRepository<Category>, ICategoryDAL
    {
        public EfCategoryDAL(SignalRContext context) : base(context)
        {
        }
    }
}

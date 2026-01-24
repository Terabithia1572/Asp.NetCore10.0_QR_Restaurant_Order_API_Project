using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites
{
    public class Table
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public string QRCodeUrl { get; set; }
        public bool Status { get; set; } // true: Dolu, false: Müsait

        public ICollection<Order> Orders { get; set; }
    }
}

using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ContactDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.ContactMap
{
    public class ContactMapping:Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, ResultContactDTO>().ReverseMap(); //Contact ile ResultContactDTO arasında çift yönlü eşleme yapar
            CreateMap<Contact, CreateContactDTO>().ReverseMap(); //Contact ile CreateContactDTO arasında çift yönlü eşleme yapar
            CreateMap<Contact, UpdateContactDTO>().ReverseMap(); //Contact ile UpdateContactDTO arasında çift yönlü eşleme yapar
            CreateMap<Contact, GetContactByIDDTO>().ReverseMap(); //Contact ile GetContactByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}

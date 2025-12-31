using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.EntityFramework;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SignalRContext>(); // DbContext'i ekler
builder.Services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly()); // AutoMapper'ý ekler
builder.Services.AddScoped<IAboutService,AboutManager>(); // IAboutService için AboutManager'ý ekler
builder.Services.AddScoped<IAboutDAL, EfAboutDAL>(); // IAboutDAL için EfAboutDAL'ý ekler
builder.Services.AddScoped<IBookingService, BookingManager>(); // IBookingService için BookingManager'ý ekler
builder.Services.AddScoped<IBookingDAL, EfBookingDAL>(); // IBookingDAL için EfBookingDAL'ý ekler
builder.Services.AddScoped<ICategoryService, CategoryManager>(); // ICategoryService için CategoryManager'ý ekler
builder.Services.AddScoped<ICategoryDAL, EfCategoryDAL>(); // ICategoryDAL için EfCategoryDAL'ý ekler
builder.Services.AddScoped<IContactService,ContactManager>(); // IContactService için ContactManager'ý ekler
builder.Services.AddScoped<IContactDAL, EfContactDAL>(); // IContactDAL için EfContactDAL'ý ekler
builder.Services.AddScoped<IDiscountService, DiscountManager>(); // IDiscountService için DiscountManager'ý ekler
builder.Services.AddScoped<IDiscountDAL, EfDiscountDAL>(); // IDiscountDAL için EfDiscountDAL'ý ekler
builder.Services.AddScoped<IFeatureService, FeatureManager>(); // IFeatureService için FeatureManager'ý ekler
builder.Services.AddScoped<IFeatureDAL, EfFeatureDAL>(); // IFeatureDAL için EfFeatureDAL'ý ekler
builder.Services.AddScoped<IFooterService, FooterManager>(); // IFooterService için FooterManager'ý ekler
builder.Services.AddScoped<IFooterDAL, EfFooterDAL>(); // IFooterDAL için EfFooterDAL'ý ekler
builder.Services.AddScoped<IProductService, ProductManager>(); // IProductService için ProductManager'ý ekler
builder.Services.AddScoped<IProductDAL, EfProductDAL>(); // IProductDAL için EfProductDAL'ý ekler
builder.Services.AddScoped<ITestimonialService, TestimonialManager>(); // ITestimonialService için TestimonialManager'ý ekler
builder.Services.AddScoped<ITestimonialDAL, EfTestimonialDAL>(); // ITestimonialDAL için EfTestimonialDAL'ý ekler


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

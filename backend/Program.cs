using DinkToPdf.Contracts;
using DinkToPdf;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Authorization;
using ASPNET_API.Helpers;
using ASPNET_API.Models;
using ASPNET_API.Models.Momo;
using ASPNET_API.Services;
using ASPNET_API.Notification;
using Microsoft.AspNetCore.Mvc;
using ASPNET_API.Infrastructure;
using ASPNET_API.Application.Services;
using ASPNET_API.Application;
using ASPNET_API.Application.Services.Interfa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();

// =================== Infrastructure =======================

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("MyDB"));

// =================== Infrastructure =======================




builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllers().AddNewtonsoftJson();

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExportHTMLtoPDF, ExportHTMLtoPDF>();
builder.Services.AddScoped<IQuestionBankService, QuestionBankService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddScoped<PDFService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddAutoMapper(typeof(MappingProfile));


//service
builder.Services.AddScoped<ConsultationRequestService>();
builder.Services.AddScoped<CourseEnrollService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<ExamCandidateService>();
builder.Services.AddScoped<LessonService>();
//builder.Services.AddScoped<QuestionBankService>();
//builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<IAdminService, AdminService>();



//CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()  // Allow all origins
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

//custom InvalidModelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorMessage = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault(); // Lấy lỗi đầu tiên

        //return new BadRequestObjectResult(new { message = errorMessage });
        return new BadRequestObjectResult(errorMessage);
    };
});

//send mail
builder.Services.AddOptions();                                        // Kích hoạt Options
var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
builder.Services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject
builder.Services.AddTransient<IEmailSender, MailService>();        // Đăng ký dịch vụ Mail
//send mail


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();

// Ensure wwwroot/uploads directory exists
var uploadDir = Path.Combine(builder.Environment.WebRootPath, "uploads");
if (!Directory.Exists(uploadDir))
{
    Directory.CreateDirectory(uploadDir);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapHub<NotiHub>("/notiHub");
app.UseRouting();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();
app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    try
//    {
//        var context = services.GetRequiredService<DonationWebApp_v2Context>();
//        //SeedDatabase(context);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}

app.Run();

//void SeedDatabase(DonationWebApp_v2Context context)
//{
//    var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "seed-data.sql");

//    if (File.Exists(sqlFilePath))
//    {
//        ClearDatabase(context);
//        var sql = File.ReadAllText(sqlFilePath);
//        context.Database.ExecuteSqlRaw(sql); 
//    }
//    else
//    {
//        throw new FileNotFoundException("The seed-data.sql file was not found.", sqlFilePath);
//    }
//}

//void ClearDatabase(DonationWebApp_v2Context context)
//{
//    //context.Database.ExecuteSqlRaw("DELETE FROM Category");

//    //context.SaveChanges();
//}
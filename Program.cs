using FreelanceProjectControl.Data;
using FreelanceProjectControl.Models;
using FreelanceProjectControl.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProjectRepository>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Project API

app.MapGet("/Project/Get", async (IProjectService service) =>
{
    var result = await service.GetAllProjects();
    return result.Any() ? Results.Ok(result) : Results.NotFound("No records found");
})
.WithName("GetAllProjects");

app.MapGet("/Project/GetById", async (IProjectService service, string id) =>
{
    var result = await service.GetProjectById(id);
    return result is not null ? Results.Ok(result) : Results.NotFound($"No record found - id: {id}");
})
.WithName("GetProjectById");

app.MapPost("/Project/Create", async (IProjectService service, Project project) =>
{
    bool result = await service.CreateProject(project);
    return result ? Results.Ok() : Results.BadRequest("Error creating new Project Entity");
})
.WithName("CreateProject");

app.MapPut("/Project/Update", async (IProjectService service, Project project) =>
{
    bool result = await service.UpdateProject(project);
    return result ? Results.Ok() : Results.NotFound($"No record found - id: {project.Id}");
})
.WithName("UpdateProject");

app.MapDelete("/Project/Delete", async (IProjectService service, string id) =>
{
    bool result = await service.DeleteProject(id);
    return result ? Results.Ok() : Results.NotFound($"No record found - id: {id}");
})
.WithName("DeleteProject");

#endregion

app.Run();
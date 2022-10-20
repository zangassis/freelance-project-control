using Dapper;
using FreelanceProjectControl.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace FreelanceProjectControl.Data;
public class ProjectRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectRepository(IOptions<ConnectionString> connectionString)
    {
        _dbConnection = new MySqlConnection(connectionString.Value.ProjectConnection);
    }

    public async Task<List<Project>> GetAll()
    {
        try
        {
            _dbConnection?.Open();

            string query = @"select id, name, customer, workedHours, flatRateAmount, hourlyRateAmount, startDate, endDate, active from project";
            
            var projects = await _dbConnection.QueryAsync<Project>(query);
            return projects.ToList();
        }
        catch (Exception)
        {
            return new List<Project>();
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<Project?> GetById(string id)
    {
        try
        {
            _dbConnection?.Open();

            string query = $@"select id, name, customer, workedHours, flatRateAmount, hourlyRateAmount, startDate, endDate, active from project where id = '{id}'";
            
            var customer = await _dbConnection.QueryAsync<Project>(query, id);
            return customer.FirstOrDefault();
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Create(Project project)
    {
        try
        {
            _dbConnection?.Open();

            string query = @"insert into project(id, name, customer, workedHours, flatRateAmount, hourlyRateAmount, startDate, endDate, active) 
                             values(@Id, @Customer, @Name, @WorkedHours, @FlatRateAmount, @HourlyRateAmount, @StartDate, @EndDate, @Active)";

            await _dbConnection.ExecuteAsync(query, project);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Update(Project project)
    {
        try
        {
            _dbConnection?.Open();

            string selectQuery = $@"select * from project where id = '{project.Id}'";

            var entity = await _dbConnection.QueryAsync<Project>(selectQuery, project.Id);

            if (entity is null)
                return false;

            string updateQuery = @"update project set name = @Name, customer = @Customer, workedHours = @WorkedHours, flatRateAmount = @FlatRateAmount, 
                                   hourlyRateAmount = @HourlyRateAmount, startDate = @StartDate, endDate = @EndDate, active = @Active 
                                   where id = @Id";

            await _dbConnection.ExecuteAsync(updateQuery, project);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Delete(string id)
    {
        try
        {
            _dbConnection?.Open();

            string selectQuery = $@"select * from project where id = '{id}'";

            var entity = await _dbConnection.QueryAsync<Project>(selectQuery, id);

            if (entity is null)
                return false;

            string query = $@"delete from project where id = '{id}'";

            await _dbConnection.ExecuteAsync(query);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }
}


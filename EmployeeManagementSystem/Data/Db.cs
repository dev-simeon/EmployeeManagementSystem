using EmployeeManagementSystem.Data.Models;
using EmployeeManagementSystem.Models;
using MongoDB.Driver;

namespace EmployeeManagementSystem.Data;

public class Db
{
    private const string _connectionString = "mongodb://localhost:27017";
    private const string _databaseName = "employee_management";
    private const string _organisationCollection = "organizations";
    private const string _departmentCollection = "department";
    private const string _employeeCollection = "employees";
    private const string _bankAccountCollection = "bank-accounts";
    private const string _payrollCollection = "payroll";
    private const string _roleCollection = "role";
    private const string _qualificationCollection = "qualifications";
    private const string _allowanceCollection = "allowances";
    private const string _deductionCollection = "deductions";

    private readonly IMongoDatabase _db;

    public Db()
    {
        var client = new MongoClient(_connectionString);
        _db = client.GetDatabase(_databaseName);
    }

    public List<Organisation> FindAllOrganisation()
    {
        var organisationCollection = _db.GetCollection<Organisation>(_organisationCollection);
        var result = organisationCollection.Find(_ => true);
        return result.ToList();
    }

    public Organisation FindAnOrganisation(string organisationName)
    {
        var organisationCollection = _db.GetCollection<Organisation>(_organisationCollection);
        var result = organisationCollection.Find(O => O.OrganisationName == organisationName);
        return result.FirstAsync().Result;
    }

    public Organisation FindAnOrganisationWithId(string organisationId)
    {
        var organisationCollection = _db.GetCollection<Organisation>(_organisationCollection);
        var result = organisationCollection.Find(O => O.Id == organisationId);
        return result.FirstAsync().Result;
    }

    public BankAccount FindBankAccount(string employeeId)
    {
        var AccountCollection = _db.GetCollection<BankAccount>(_bankAccountCollection);
        var result = AccountCollection.Find(b => b.UserId == employeeId);
        return result.FirstAsync().Result;
    }

    public List<Employee> FindAllEmployees()
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        var result = employeeCollection.Find(_ => true);
        return result.ToList();
    }

    public Employee FindAnEmployee(string email)
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        var result = employeeCollection.Find(e => e.Email == email);
        return result.FirstAsync().Result;
    }

    public Employee FindEmployeeWithId(string id)
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        var result = employeeCollection.Find(e => e.Id == id);
        return result.FirstAsync().Result;
    }

    public List<Employee> FindAllEmployeesInAnOrganisation(string organisationId)
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        var result = employeeCollection.Find(e => e.OrganisationId == organisationId);
        return result.ToList();
    }

    public Task CreateOrganisation(Organisation organisation)
    {
        var organisationCollection = _db.GetCollection<Organisation>(_organisationCollection);
        return organisationCollection.InsertOneAsync(organisation);
    }

    public Task CreateDepartment(Department department)
    {
        var departmentCollection = _db.GetCollection<Department>(_departmentCollection);
        return departmentCollection.InsertOneAsync(department);
    }

    public Department FindADepartment(string departmentId)
    {
        var departmentCollection = _db.GetCollection<Department>(_departmentCollection);
        var result = departmentCollection.Find(d => d.Id == departmentId);
        return result.FirstAsync().Result;
    }

    public List<Department> FindAllDepartmentBelongingToAnOrganisation(string organisationId)
    {
        var departmentCollection = _db.GetCollection<Department>(_departmentCollection);
        var result = departmentCollection.Find(d => d.OrganisationId == organisationId);
        return result.ToList();
    }

    public Task DeleteDepartment(string departmentId)
    {
        var departmentCollection = _db.GetCollection<Department>(_departmentCollection);
        return departmentCollection.DeleteOneAsync(d => d.Id == departmentId);
    }

    public Task DeleteEmployee(string employeeId)
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        return employeeCollection.DeleteOneAsync(e => e.Id == employeeId);
    }

    public Task DeleteOrganisation(string organisationId)
    {
        var organisationCollection = _db.GetCollection<Organisation>(_organisationCollection);
        return organisationCollection.DeleteOneAsync(o => o.Id == organisationId);
    }

    public Task DeleteRole(string roleId)
    {
        var roleCollection = _db.GetCollection<Role>(_roleCollection);
        return roleCollection.DeleteOneAsync(r => r.Id == roleId);
    }

    public Task AddEmployee(Employee employee)
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        return employeeCollection.InsertOneAsync(employee);
    }

    public Task AddQualification(Qualification qualification)
    {
        var qualificationCollection = _db.GetCollection<Qualification>(_qualificationCollection);
        return qualificationCollection.InsertOneAsync(qualification);
    }

    public Task CreateRole(Role role)
    {
        var roleCollection = _db.GetCollection<Role>(_roleCollection);
        return roleCollection.InsertOneAsync(role);
    }

    public Task CreateBankAccount(BankAccount bankAccount)
    {
        var bankCollection = _db.GetCollection<BankAccount>(_bankAccountCollection);
        return bankCollection.InsertOneAsync(bankAccount);
    }

    public Role FindRole(string roleId)
    {
        var roleCollection = _db.GetCollection<Role>(_roleCollection);
        var result = roleCollection.Find(r => r.Id == roleId);
        return result.FirstAsync().Result;
    }

    public Task UpdateUserInfo(Employee employee)
    {
        var employeeCollection = _db.GetCollection<Employee>(_employeeCollection);
        var filter = Builders<Employee>.Filter.Eq(e => e.Id, employee.Id);
        return employeeCollection.ReplaceOneAsync(filter, employee);
    }

    public Task UpdateDepartmentInfo(Department department)
    {
        var departmentCollection = _db.GetCollection<Department>(_departmentCollection);
        var filter = Builders<Department>.Filter.Eq(d => d.Id, department.Id);
        return departmentCollection.ReplaceOneAsync(filter, department);
    }

    public Task UpdateRole(Role role)
    {
        var roleCollection = _db.GetCollection<Role>(_roleCollection);
        var filter = Builders<Role>.Filter.Eq(r => r.Id, role.Id);
        return roleCollection.ReplaceOneAsync(filter, role);
    }
}

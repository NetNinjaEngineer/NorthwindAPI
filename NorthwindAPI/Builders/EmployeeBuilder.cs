using NorthwindAPI.Entities;

namespace NorthwindAPI.Builders
{
    public class EmployeeBuilder
    {
        private Employee employee = new Employee();

        public EmployeeBuilder() { }

        public EmployeeBuilder(int employeeId) { employee.EmployeeId = employeeId; }

        public EmployeeBuilder SetEmployeeId(int employeeId)
        {
            employee.EmployeeId = employeeId;
            return this;
        }

        public EmployeeBuilder SetLastName(string lastName)
        {
            employee.LastName = lastName;
            return this;
        }

        public EmployeeBuilder SetFirstName(string firstName)
        {
            employee.FirstName = firstName;
            return this;
        }

        public EmployeeBuilder WithTitle(string? title)
        {
            employee.Title = title;
            return this;
        }

        public EmployeeBuilder WithTitleOfCourtesy(string? titleOfCourtesy)
        {
            employee.TitleOfCourtesy = titleOfCourtesy;
            return this;
        }

        public EmployeeBuilder WithBirthDate(DateTime? birthDate)
        {
            employee.BirthDate = birthDate;
            return this;
        }

        public EmployeeBuilder WithHireDate(DateTime? hireDate)
        {
            employee.HireDate = hireDate;
            return this;
        }

        public EmployeeBuilder SetAddress(string? address)
        {
            employee.Address = address;
            return this;
        }

        public EmployeeBuilder SetCity(string? city)
        {
            employee.City = city;
            return this;
        }

        public EmployeeBuilder SetRegion(string? region)
        {
            employee.Region = region;
            return this;
        }

        public EmployeeBuilder WithPostalCode(string? postalCode)
        {
            employee.PostalCode = postalCode;
            return this;
        }

        public EmployeeBuilder WithCountry(string? country)
        {
            employee.Country = country;
            return this;
        }

        public EmployeeBuilder WithHomePhone(string? homePhone)
        {
            employee.HomePhone = homePhone;
            return this;
        }

        public EmployeeBuilder WithExtension(string? extension)
        {
            employee.Extension = extension;
            return this;
        }

        public EmployeeBuilder WithPhoto(byte[]? photo)
        {
            employee.Photo = photo;
            return this;
        }

        public EmployeeBuilder WithNotes(string? notes)
        {
            employee.Notes = notes;
            return this;
        }

        public EmployeeBuilder ReportsTo(int? reportsTo)
        {
            employee.ReportsTo = reportsTo;
            return this;
        }

        public EmployeeBuilder WithPhotoPath(string? photoPath)
        {
            employee.PhotoPath = photoPath;
            return this;
        }

        public Employee Build()
        {
            return employee;
        }
    }
}

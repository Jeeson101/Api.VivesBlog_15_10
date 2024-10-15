using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Core;
using VivesBlog.Dtos.Requests;
using VivesBlog.Dtos.Results;
using VivesBlog.Model;
using VivesBlog.Services.Extensions;

namespace VivesBlog.Services
{
    public class PersonService
    {
        private readonly VivesBlogDbContext _dbContext;

        public PersonService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<PersonResult>> Find()
        {
            return await _dbContext.People
                .Project()
                .ToListAsync();
        }

        //Get (by id)
        public async Task<ServiceResult<PersonResult>> Get(int id)
        {
            var person = await _dbContext.People
                .Project()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return new ServiceResult<PersonResult>().NotFound(nameof(Person));
            }

            return new ServiceResult<PersonResult>(person);
        }

        //Create
        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var person = new Person()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        //Update
        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return new ServiceResult<PersonResult>().NotFound(nameof(Person));
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await _dbContext.SaveChangesAsync();

            return await Get(id);
        }

        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return new ServiceResult().NotFound(nameof(Person));
            }

            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();

            return new ServiceResult();
        }

    }
}

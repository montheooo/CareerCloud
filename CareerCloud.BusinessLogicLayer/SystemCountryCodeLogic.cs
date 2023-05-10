using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        private IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repo)
        {
            _repository = repo;
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        public SystemCountryCodePoco Get(string code)
        {
            return _repository.GetSingle(c => c.Code == code);
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, "Cannot be empty"));
                }

                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, "Cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
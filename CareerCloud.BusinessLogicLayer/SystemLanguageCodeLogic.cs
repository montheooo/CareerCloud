using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        private IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        public SystemLanguageCodePoco Get(string languageid)
        {
            return _repository.GetSingle(c => c.LanguageID == languageid);
        }

        public virtual List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, "Cannot be empty"));
                }

                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, "Cannot be empty"));
                }

                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, "Cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
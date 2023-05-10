using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository)
            : base(repository)
        {
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();
            var websiteExtensions = new string[] { ".ca", ".com", ".biz" };

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\""));
                }
                else if (!poco.CompanyWebsite.Contains(websiteExtensions[0]) &&
                        !poco.CompanyWebsite.Contains(websiteExtensions[1]) &&
                        !poco.CompanyWebsite.Contains(websiteExtensions[2]))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\""));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                }
                else
                {
                    var phone = poco.ContactPhone.Split('-');

                    if (phone.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                    else if (phone[0].Length != 3 || phone[1].Length != 3 || phone[2].Length != 4)
                    {
                        exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
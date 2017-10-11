using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using O2.Telephony.Dal.Imp;
using O2.Telephony.Logic;
using O2.Telephony.Models;
using O2.Telephony.Provider.Interfaces;

namespace O2.Telephony.Service.Areas.Api.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IProviderLogic _providerLogic;

        public AccountsController(IProviderLogic providerLogic)
        {
            _providerLogic = providerLogic;
        }

        // GET api/accounts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/accounts/5
        [NotImplExceptionFilter]
        public Account Get(Guid id)
        {
            var accountLogic = new AccountLogic(new AccountDal(), _providerLogic);

            AccountResult<Account> account = accountLogic.Get(id);

            if (account.HasError)
            {
                switch (account.ResultCode)
                {
                    case AccountResultCode.InvalidParameter:
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    case AccountResultCode.AccountNotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }

            return account.Value;
        }

        // POST api/accounts/{id}/create
        public Guid Create(Guid id)
        {
            var accountLogic = new AccountLogic(new AccountDal(), _providerLogic);

            AccountResult<Account> account = accountLogic.Create(id);

            if (account.HasError)
            {
                switch (account.ResultCode)
                {
                    case AccountResultCode.InvalidParameter:
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    case AccountResultCode.AccountNotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }

            return account.Value.Id;
        }

        // POST api/accounts
        public void Post([FromBody]string value)
        {
        }

        // PUT api/accounts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/accounts/5
        public void Delete(int id)
        {
        }
    }
}

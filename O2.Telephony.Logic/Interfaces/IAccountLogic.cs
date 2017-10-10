using System;
using O2.Telephony.Models;

namespace O2.Telephony.Logic.Interfaces
{
    public interface IAccountLogic
    {
        //create
        AccountResult<Account> Create(Guid? parentId);

        //read
        AccountResult<Account> Get(Guid accountId);

        //update
        //delete
        //process
    }
}

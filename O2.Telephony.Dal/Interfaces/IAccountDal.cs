using System;
using O2.Telephony.Models;

namespace O2.Telephony.Dal.Interfaces
{
    /// <summary>
    /// Interface for AccountData
    /// </summary>
    public interface IAccountDal
    {
        //create
        Account Create(Guid accountId, Guid parentId);

        //read
        Account Read(Guid id);

        //update
        //delete
        //process

    }
}

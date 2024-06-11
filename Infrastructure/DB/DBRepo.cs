using Services.B.Core.Interfaces;
using Services.B.Core.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.B.Infrastructure.DB
{
    public class DBRepo : DBRepoBase, IDBRepo
    {
        #region *** private


        #endregion
        #region *** ctor

        public DBRepo(IOptions<AppSettings> settings, ILoggerRepo logManager) : base(settings, logManager) { }

        #endregion
        #region *** public 

        #endregion
    }
}

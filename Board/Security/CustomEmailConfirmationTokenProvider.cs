using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyBoard.Security
{
  public class CustomEmailConfirmationTokenProvider<TUser> :
    DataProtectorTokenProvider<TUser> where TUser : class
  {
    
    public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
      IOptions<CustomEmailConfirmationTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger)
    : base(dataProtectionProvider, options,logger)
    {

    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApi2Book.Common.TypeMapping
{
    public interface IAutoMapperTypeConfigurator
    {
        void Configure();
    }
}

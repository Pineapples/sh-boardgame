using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.Repositories
{
    public interface IPolicyRepository
    {
        Policy Add(Policy policy);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly DataContext _context;

        public PolicyRepository(DataContext context)
        {
            _context = context;
        }

        public Policy Add(Policy policy)
        {
            var returnPolicy = _context.Add(policy);
            _context.SaveChanges();
            return returnPolicy.Entity;
        }
    }
}

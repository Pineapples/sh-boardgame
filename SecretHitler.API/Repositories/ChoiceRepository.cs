using System;
using Microsoft.EntityFrameworkCore;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly DataContext _context;

        public ChoiceRepository(DataContext context)
        {
            this._context = context;
        }

        public void Update(Choice choice) {
            this._context.Entry(choice).CurrentValues.SetValues(choice);
            this._context.SaveChanges();
        }

        public void Add(Choice choice) {
            this._context.Choices.Add(choice);
            this._context.SaveChanges();
        }
    }
}

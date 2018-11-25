using System;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Repositories
{
    public interface IChoiceRepository
    {
        void Update(Choice choice);
        void Add(Choice choice);
    }
}

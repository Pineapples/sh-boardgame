using System;
using SecretHitler.Models.Exceptions.Interface;

namespace SecretHitler.Models.Exceptions
{
    public class EntityNotFoundException<T> : Exception, IEntityNotFoundException
    {
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(int id) : base($"An entity of type {typeof(T)} with id: {id} was not found.")
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}

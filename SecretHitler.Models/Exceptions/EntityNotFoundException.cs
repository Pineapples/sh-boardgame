using System;
using System.Collections.Generic;
using System.Text;

namespace SecretHitler.Models.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(int id) : base($"An entity of type {typeof(T)} with id: {id} was not found.")
        {
        }
    }
}

﻿using System.Net;

namespace BarberBoss.Exceptions.ExceptionsBase
{
    public class NotFoundException : BarberBossException
    {
        public NotFoundException(string message) : base(message)
        {
        }
        public override int StatusCode => (int)HttpStatusCode.NotFound;
        public override List<string> GetErrors()
        {
            return [Message];
        }

    }
}

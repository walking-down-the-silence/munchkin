using Munchkin.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Munchkin.Infrastructure
{
    public interface IExpansionRegister
    {
        IReadOnlyCollection<ExpansionOption> GetAvaialableExpansions();

        IReadOnlyCollection<IExpansion> SearchExpansions(Func<IExpansion, bool> filter);
    }
}

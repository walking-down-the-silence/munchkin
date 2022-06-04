using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Extensions
{
    public static class EGenderExtensions
    {
        public static EGender OppositeSex(this EGender gender)
        {
            return gender switch
            {
                EGender.Male => EGender.Female,
                EGender.Female => EGender.Male,
                _ => throw new ArgumentOutOfRangeException(nameof(gender))
            };
        }
    }
}

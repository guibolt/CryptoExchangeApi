using FluentValidation.Results;
using System.Collections.Generic;

namespace CryptoExchange.Core.Command
{
    public abstract class CommandBase
    {
        protected ValidationResult Validation { get; set; }
        public abstract bool IsValid();
        public abstract IList<ValidationFailure> Errors();
    }
}

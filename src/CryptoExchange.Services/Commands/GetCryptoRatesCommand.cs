using CryptoExchange.Core.Command;
using CryptoExchange.Services.Commands.Validation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace CryptoExchange.Services.Commands
{
    public class GetCryptoRatesCommand : CommandBase
    {
        public string FirstCoin { get; private set; }
        public string SecondCoin { get; private set; }
        public GetCryptoRatesCommand(string fisrtCoin, string secondCoin)
        {
            FirstCoin = fisrtCoin;
            SecondCoin = secondCoin;
        }

        public override bool IsValid()
        {
            Validation = new GetCryptoRatesCommandValidation().Validate(this);
            return Validation.IsValid;
        }
        public override IList<ValidationFailure> Errors() => Validation.Errors;
    }
}

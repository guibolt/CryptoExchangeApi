using CryptoExchange.Core.Command;
using CryptoExchange.Services.Commands.Validation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace CryptoExchange.Services.Commands
{
    public class GetHistorialRatesCommand : CommandBase
    {
        public string CoinSymbol { get; private set; }
        public DateTime InitialDate { get; private set; }
        public GetHistorialRatesCommand(string coinSymbol, DateTime initialDate)
        {
            CoinSymbol = coinSymbol;
            InitialDate = initialDate;
        }

        public override bool IsValid()
        {
            Validation = new GetHistorialRatesCommandValidation().Validate(this);
            return Validation.IsValid;
        }
        public override IList<ValidationFailure> Errors() => Validation.Errors;
    }
}

using FluentValidation;

namespace CryptoExchange.Services.Commands.Validation
{
    public class GetHistorialRatesCommandValidation : AbstractValidator<GetHistorialRatesCommand>
    {
        public GetHistorialRatesCommandValidation()
        {
            RuleFor(c => c.CoinSymbol)
                .NotNull().
                NotEmpty().
                WithMessage("CoinSymbol is invalid");

            RuleFor(c => c.InitialDate)
                .NotNull()
                .WithMessage("InitialDate is invalid");
        }
    }
}

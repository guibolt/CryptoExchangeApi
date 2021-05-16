using FluentValidation;

namespace CryptoExchange.Services.Commands.Validation
{
    public class GetCryptoRatesCommandValidation : AbstractValidator<GetCryptoRatesCommand>
    {
        public GetCryptoRatesCommandValidation()
        {
            RuleFor(c => c.FirstCoin)
                .NotNull().
                NotEmpty().
                WithMessage("FirstCoin is invalid");

            RuleFor(c => c.SecondCoin)
                 .NotNull().
                NotEmpty()
                .WithMessage("SecondCoin is invalid");
        }
    }
}

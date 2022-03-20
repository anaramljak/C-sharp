using FluentValidation;
using System;
using System.Linq;

namespace Lab6.Validators
{
    public class SlipValidator : AbstractValidator<PaymentSlip>
    {
        public SlipValidator()
        {
            RuleFor(slip => slip.PayerName)
                .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(slip => slip.RecipientName)
                .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(slip => slip.Currency)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(3).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidCurrency).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.PayerIBAN)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(21).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidIBAN).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.PayerModel)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(4).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidModel).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.PayerNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(22).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidNumber).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.RecipientIBAN)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(21).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidIBAN).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.RecipientModel)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(4).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidModel).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.RecipientNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(22).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidNumber).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.PaymentDescription)
                .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(slip => slip.Date)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidDate).WithMessage("Invalid {PropertyName}");

            RuleFor(slip => slip.Total)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidTotal).WithMessage("Invalid {PropertyName}");
        }

        public bool BeAValidCurrency(string currency)
        {
            return currency.All(Char.IsLetter);
        }

        public bool BeAValidIBAN(string iban)
        {
            return (iban.Substring(0,2).All(Char.IsLetter) && iban.Substring(2,19).All(Char.IsDigit));
        }

        public bool BeAValidModel(string model)
        {
            return (model.StartsWith("HR") && model.Substring(2,2).All(Char.IsDigit));
        }

        public bool BeAValidNumber(string number)
        {
            string[] lines = number.Split('-');
            if (lines.Length <= 3)
                return true;
            return false;
        }

        public bool BeAValidDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int enteredYear = date.Year;

            if (enteredYear <= currentYear && enteredYear > (currentYear - 120))
                return true;

            return false;
        }

        public bool BeAValidTotal(string total)
        {
            decimal result;
            if (Decimal.TryParse(total, out result) && Int16.Parse(total) != 0)
                return true;
            return false;
        }
    }

}

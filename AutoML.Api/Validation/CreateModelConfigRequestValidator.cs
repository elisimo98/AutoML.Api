using AutoML.Api.Models;
using FluentValidation;

namespace AutoML.Api.Validation
{
    public class CreateModelConfigRequestValidator : AbstractValidator<CreateModelConfigRequest>
    {
        public CreateModelConfigRequestValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(255).WithMessage("FileName cannot exceed 255 characters.");

            RuleFor(x => x.TestSize)
                .InclusiveBetween(0.0, 1.0)
                .WithMessage("TestSize must be between 0.0 and 1.0.");

            RuleFor(x => x.RandomState)
                .GreaterThanOrEqualTo(0)
                .WithMessage("RandomState must be a non-negative integer.");

            RuleFor(x => x.Epochs)
                .GreaterThan(0)
                .WithMessage("Epochs must be greater than 0.")
                .LessThanOrEqualTo(1000)
                .WithMessage("Epochs cannot exceed 1000.");

            RuleFor(x => x.ModelType)
                .NotEmpty().WithMessage("ModelType is required.")
                .Must(BeAValidModelType)
                .WithMessage("ModelType must be one of: 'classification', 'regression', or 'clustering'.");

            RuleFor(x => x.TargetColumn)
                .NotEmpty().WithMessage("TargetColumn is required.");
        }

        private bool BeAValidModelType(string modelType)
        {
            var validTypes = new[] { "classification", "regression", "clustering" };
            return validTypes.Contains(modelType.ToLower());
        }
    }
}

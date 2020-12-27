using FluentValidation;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models.Validators
{
    public sealed class TrackQueryValidator : AbstractValidator<TrackQuery>
    {

        public TrackQueryValidator()
        {
            RuleFor(x => x.Name).Length(0, 10);
            RuleFor(x => x.PriceFrom).Matches(@"^(gt|gte|lt|lte|eq):[1-9]{1}\d*\.?\d*$").WithMessage("Value must be in the form of 'gt:10.5', gte:10.5, lt:10.5, lte:10.5, eq:10.5");
            RuleFor(x => x.PriceTo).Matches(@"^(gt|gte|lt|lte|eq):[1-9]{1}\d*\.?\d*$").WithMessage("Value must be in the form of 'gt:10.5', gte:10.5, lt:10.5, lte:10.5, eq:10.5");
        }
    }
}

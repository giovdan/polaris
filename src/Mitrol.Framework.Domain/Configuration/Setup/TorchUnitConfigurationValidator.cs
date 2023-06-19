﻿namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;


    public class TorchUnitConfigurationValidator : AbstractValidator<TorchUnitConfiguration>
    {
        public TorchUnitConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(torch => torch.Path)
                .GreaterThan((byte)0).WithMessage("Must be greater than zero for Fanuc cnc machines")
                .When((torch, context) => context.RootContextData.TryGetValue("Fanuc.IsPresent", out var value) && (bool)value);

            RuleFor(torch => torch.SlaveTorchCount)
                .GreaterThan((byte)0).WithMessage("Must be greater than zero for MSTSLV torches")
                .When(torch => torch.Type is Domain.Enums.TorchTypeEnum.MSTSLV);

            RuleFor(torch => torch.SlaveTorchCount)
                .GreaterThan((byte)0).WithMessage("Must be greater than zero for MSTSLV torches")
                .When(torch => torch.Type is Domain.Enums.TorchTypeEnum.MSTSLV);
        }
    }
}
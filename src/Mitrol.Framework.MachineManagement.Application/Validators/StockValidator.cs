namespace Mitrol.Framework.MachineManagement.Application.Validators
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class StockValidator : BaseEntityValidator<IMachineManagentDatabaseContext>
        , IEntityValidator<StockItemToAdd, IMachineManagentDatabaseContext>
    {
        private IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();

        internal class StockItemToAddValidator : AbstractValidator<StockItemToAdd>
        {
            public StockItemToAddValidator()
            {
                RuleFor(x => x.Attributes.Count).GreaterThan(0)
                    .WithErrorCode(ErrorCodesEnum.ERR_ATT003.ToString());
                RuleFor(x => (ProfileTypeEnum)x.ProfileTypeId).IsInEnum()
                    .WithErrorCode(ErrorCodesEnum.ERR_GEN015.ToString());
            }
        }

        public StockValidator(IServiceFactory serviceFactory): base(serviceFactory)
        {

        }
        private Result<long> ValidateStock(StockItemToAdd stockItemToAdd)
        {
            var errorsDetail = new List<ErrorDetail>();

            EntityRepository.Attach(UnitOfWork);
            //la quantity deve essere un numero e deve avere valore maggiore di zero
            if (stockItemToAdd.Attributes.TryGetValue(DatabaseDisplayNameEnum.Quantity, out var quantityValue))
            {
                if (!int.TryParse(quantityValue.ToString(), out int quantity) || quantity <= 0)
                    errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Quantity.ToString(), ErrorCodesEnum.ERR_STK004.ToString()));
            }
            else
                errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Quantity.ToString(), ErrorCodesEnum.ERR_STK005.ToString()));

            //il codice materiale deve esistere ed avere un codice valido
            if (stockItemToAdd.Attributes.TryGetValue(DatabaseDisplayNameEnum.MaterialCode, out var materialCode))
            {
                var entityType = ParentTypeEnum.Material.GetEntityType();
                var materialCodeValue = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(materialCode.ToString());
                //Recupero identificativo del materiale dal codice
                if (!EntityRepository.FindBy(m =>
                        m.EntityTypeId == entityType
                        && m.DisplayName == materialCodeValue.Value).Any())
                    errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.MaterialCode.ToString(), ErrorCodesEnum.ERR_STK002.ToString()));
            }
            else
                errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.MaterialCode.ToString(), ErrorCodesEnum.ERR_STK006.ToString()));

            //la length deve essere un numero e deve avere valore maggiore di zero
            if (stockItemToAdd.Attributes.TryGetValue(DatabaseDisplayNameEnum.Length, out var lengthValue))
            {
                //Recupero identificativo del materiale dal codice
                if (!decimal.TryParse(lengthValue.ToString(), out decimal length) || length <= 0)
                    errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Length.ToString(), ErrorCodesEnum.ERR_STK009.ToString()));
            }
            else
                errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Length.ToString(), ErrorCodesEnum.ERR_STK008.ToString()));

            // per profili P
            if (stockItemToAdd.ProfileTypeId != (long)ProfileTypeEnum.P)
            {
                //il codice profilo deve esistere ed avere un codice valido
                if (stockItemToAdd.Attributes.TryGetValue(DatabaseDisplayNameEnum.ProfileCode, out var profileCode))
                {
                    var entityType = ParentTypeEnum.Profile.GetEntityType();
                    var profileCodeValue = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(profileCode.ToString());
                    //Recupero identificativo del profilo dal codice
                    if (!EntityRepository.FindBy(p => p.EntityTypeId == entityType 
                             && p.DisplayName == profileCodeValue.Value).Any())
                        errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.ProfileCode.ToString(), ErrorCodesEnum.ERR_STK003.ToString()));
                }
                else
                    errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.ProfileCode.ToString(), ErrorCodesEnum.ERR_STK007.ToString()));
            }
            else /* per i profili non P */
            {
                //la width deve essere un numero e deve avere valore maggiore di zero
                if (stockItemToAdd.Attributes.TryGetValue(DatabaseDisplayNameEnum.Width, out var widthValue))
                {
                    //Recupero la larghezza
                    if (!decimal.TryParse(widthValue.ToString(), out decimal width) || width <= 0)
                        errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Width.ToString(), ErrorCodesEnum.ERR_STK013.ToString()));
                }
                else
                    errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Width.ToString(), ErrorCodesEnum.ERR_STK012.ToString()));

                //la thickness deve essere un numero e deve avere valore maggiore di zero
                if (stockItemToAdd.Attributes.TryGetValue(DatabaseDisplayNameEnum.Thickness, out var thicknessValue))
                {
                    //Recupero lo spessore
                    if (!decimal.TryParse(thicknessValue.ToString(), out decimal thickness) || thickness <= 0)
                        errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Thickness.ToString(), ErrorCodesEnum.ERR_STK011.ToString()));
                }
                else
                    errorsDetail.Add(new ErrorDetail(DatabaseDisplayNameEnum.Thickness.ToString(), ErrorCodesEnum.ERR_STK010.ToString()));
            }

            if (errorsDetail.Any())
                return Result.Fail<long>(errorsDetail);
            else
                return Result.Ok<long>(0);
        }

        public Result Validate(StockItemToAdd stockItem)
        {
            var validator = new StockItemToAddValidator();
            var validationResult = validator.Validate(stockItem);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.Errors.ToErrorDetails());
            }

            return ValidateStock(stockItem);
        }
    }
}

namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CachedPieceFactory
    {
        public const int MAX_LEVEL_ALLOWED = 3;

        public static CachedPiece CachedPiece { get; private set; }

        public static CachedPiece Create(long id, PieceItemIdentifier identifiers
                    , ProfileTypeEnum profileType
                    , MeasurementSystemEnum conversionSystem)
        {
            CachedPiece = new CachedPiece(id, identifiers, profileType, conversionSystem);
            return CachedPiece;
        }

        public static void ResetCache()
        {
            CachedPiece = null;
        }
    }

    public class CachedPiece
    {
        [JsonProperty("Identifiers")]
        public PieceItemIdentifier Identifiers { get; }

        public CachedPiece(long id, PieceItemIdentifier identifiers
                , ProfileTypeEnum profileType
                , MeasurementSystemEnum conversionSystem)
        {
            Identifiers = identifiers;
            Id = id;
            ProfileType = profileType;
            RemovedOperations = new HashSet<CachedPieceOperation>();
            Operations = new Dictionary<int, CachedPieceOperation>();
            Attributes = Array.Empty<AttributeDetailItem>();
            CurrentMeasurementSystem = conversionSystem;
        }

        [JsonProperty("SuggestedFormat")]
        public string SuggestedFormat => "${Identifiers.Contract}-${Identifiers.Drawing}-${Identifiers.Project}-${Identifiers.Assembly}-${Identifiers.Part}";
       

        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; }

        [JsonProperty("Id")]
        public long Id { get; internal set; }

        [JsonProperty("Attributes")]
        public AttributeDetailItem[] Attributes { get; set; }

        //HashSet contenente le operazioni aggiunti/rimosse
        [JsonIgnore()]
        public HashSet<CachedPieceOperation> RemovedOperations { get; internal set; }

        [JsonIgnore()]
        public Dictionary<int, CachedPieceOperation> Operations { get; internal set; }

        [JsonIgnore()]
        public MeasurementSystemEnum CurrentMeasurementSystem { get; internal set; }

        internal void AddOperation(CachedPieceOperation operation)
        {
            operation.Status = OperationRowStatusEnum.Added;
            Operations.Add(operation.LineNumber, operation);
        }

        internal void ReNumberingOperations(int startingLine, int offset = 1)
        {
            if (Operations.Any())
            {
                var orderedOps = GetOrderedOperations();
                //1. Trasformo le operazioni in una lista
                //2. Recupero le operazioni che non devono essere rinumerate
                var operations = orderedOps
                            .TakeWhile(op => op.LineNumber <= startingLine);

                var children = orderedOps
                    .ToLookup(op => op.ParentLineNumber, op => op);

                //3. Rinumero le rimanenti
                var operationsToRenum = orderedOps
                    .Where(op => op.LineNumber > startingLine)
                                .Select(op =>
                                {
                                    //Se ci sono figli appartenenti all'operazione che si sta rinumerando
                                    if (children[op.LineNumber].Any())
                                    {
                                        foreach (var child in children[op.LineNumber])
                                        {
                                            child.ParentLineNumber += offset;
                                        }
                                    }

                                    op.LineNumber += offset;
                                    if (op.Status == OperationRowStatusEnum.AttributeUpdated)
                                    {
                                        op.Status = OperationRowStatusEnum.FullUpdated;
                                    }
                                    else if (op.Status == OperationRowStatusEnum.UnChanged)
                                    {
                                        op.Status = OperationRowStatusEnum.Updated;
                                    }
                                    return op;
                                });
                
                Operations = operations.Concat(operationsToRenum)
                                .ToDictionary(op => op.LineNumber, op => op);
            }
        }

        internal void AbsoluteRenumbering(IEnumerable<CachedPieceOperation> orderedOps)
        {
            // Riordina le operazioni
            Operations = orderedOps
                               .Select((op, index) =>
                               {
                                   op.LineNumber = index;
                                   if (op.Status == OperationRowStatusEnum.AttributeUpdated)
                                   {
                                       op.Status = OperationRowStatusEnum.FullUpdated;
                                   }
                                   else if (op.Status == OperationRowStatusEnum.UnChanged)
                                   {
                                       op.Status = OperationRowStatusEnum.Updated;
                                   }
                                   return op;
                               }).ToDictionary(op => op.LineNumber, op => op);

            // Assegna il parentLineNumber
            Operations = Operations.Select(op =>
            {
                op.Value.ParentLineNumber = op.Value.GetParentLineNumber();
                return op;
            }).ToDictionary(op => op.Key, opt => opt.Value);
        }

        internal IEnumerable<CachedPieceOperation> GetOrderedOperations()
        {
            return Operations.OrderBy(op => op.Key)
                        .Select(op => op.Value);
        }

        internal int RemoveOperation(CachedPieceOperation operation)
        {
            //Effettuo lo skip delle linee che precedono quella da cancellare
            //Recupero se esistono figli e nipoti di operation
            var nodesToRemove = new List<CachedPieceOperation> { operation };

            nodesToRemove.AddRange(Operations
                .SkipWhile(node => node.Key <= operation.LineNumber)
                .TakeWhile(node => node.Key > operation.LineNumber
                            && node.Value.Level > operation.Level)
                            .Select(op => op.Value));

            foreach (var nodeToRemove in nodesToRemove)
            {
                //Elimino Fisicamente la riga se quest'ultima è stata aggiunta ma non è presente nel DB
                if (nodeToRemove.Status == OperationRowStatusEnum.Added)
                {
                    RemovedOperations.Remove(nodeToRemove);
                }
                else
                {
                    nodeToRemove.Status = OperationRowStatusEnum.Removed;
                    RemovedOperations.Add(nodeToRemove);
                }

                Operations.Remove(nodeToRemove.LineNumber);
            }

            return nodesToRemove.Count;
        }

        internal void SetIdentifiers(PieceItemIdentifier identifiers)
        {
            this.Identifiers.Assembly = identifiers.Assembly;
            this.Identifiers.Contract = identifiers.Contract;
            this.Identifiers.Drawing = identifiers.Drawing;
            this.Identifiers.Part = identifiers.Part;
            this.Identifiers.Drawing = identifiers.Drawing;
            this.Identifiers.Project = identifiers.Project;
        }
    }
}

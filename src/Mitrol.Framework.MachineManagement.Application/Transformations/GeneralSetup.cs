namespace Mitrol.Framework.MachineManagement.Application.Transformations
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Configuration.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Setup;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Newtonsoft.Json;

    // TO DO
    public class GeneralSetup : Observable
        //, IReadOnlyGeneralSetup
    {
        public GeneralSetup(IRootConfiguration configurationRoot)
        {
            // Valore di tolleranza rilevamento larghezza (SA)
            var tolWidth = configurationRoot.Setup?.General?.ToleranceProfileWidth ?? new ToleranceConfiguration();
            
            // Valore di tolleranza rilevamento lunghezza (Lb)
            var tolLength = configurationRoot.Setup?.General?.ToleranceProfileLength ?? new ToleranceConfiguration();
            
            // Valore di tolleranza rilevamento spessore (Ta)
            var tolThickness = configurationRoot.Setup?.General?.ToleranceProfileThickness ?? new ToleranceConfiguration();
            
            // Valore di tolleranza rilevamento Altezza (Sb)
            var tolHeight = configurationRoot.Setup?.General?.ToleranceProfileHeight ?? new ToleranceConfiguration();

            MaterialCodeObject = new SetupProperty<string>();
            ProfileTypeObject = new SetupProperty<ProfileTypeEnum>();
            ProfileCodeObject = new SetupProperty<string>();   
            MaterialTypeObject = new SetupProperty<MaterialTypeEnum>();

            SaObject = new SetupPropertyWithTolerance<float>(tolWidth );
            TaObject = new SetupPropertyWithTolerance<float>(tolThickness);
            TbaObject = new SetupPropertyWithTolerance<float>(tolThickness);
            SbaObject = new SetupPropertyWithTolerance<float>(tolHeight);
            TbbObject = new SetupPropertyWithTolerance<float>(tolThickness);
            SbbObject = new SetupPropertyWithTolerance<float>(tolHeight);
            SabObject = new SetupPropertyWithTolerance<float>(tolWidth);
            AngleObject = new SetupPropertyWithCustomComparer<float>();
            DsaObject = new SetupPropertyWithTolerance<float>(tolHeight);
            DsbObject = new SetupPropertyWithTolerance<float>(tolHeight);
            LbObject = new SetupPropertyWithCustomComparer<float>();
            CarriageTypeObject = new SetupProperty<CarriageTypeEnum>();
            PuObject = new SetupProperty<PuTypeEnum>();
            RiObject = new SetupPropertyWithCustomComparer<float>();
            RfObject = new SetupPropertyWithCustomComparer<float>();

            PosCaObject = new SetupPropertyWithCustomComparer<float>();
            PosCbObject = new SetupPropertyWithCustomComparer<float>();
            PosCcObject = new SetupPropertyWithCustomComparer<float>();
        }
        /// <summary>
        /// Stato di cambio setup generale (profilo, materiale, dimensioni, ..)
        /// </summary>
        [JsonProperty("Change")]
        public SetupActionEnum SetupAction
        {
            get => _SetupAction;
            set => SetProperty(ref _SetupAction, value);
        }
        private SetupActionEnum _SetupAction;

        /// <summary>
        /// Tipo di programma in esecuzione
        /// </summary>
        [JsonProperty("ProgramType")]
        public ProgramTypeEnum ProgramType
        {
            get => _programType;
            set => SetProperty(ref _programType, value);
        }
        private ProgramTypeEnum _programType;

        /// <summary>
        /// Oggetto Codice del materiale (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.MaterialCode,AttributeDefinitionGroupEnum.Generic,3)]
        [DirtyableChild]
        [JsonProperty("MaterialCodeOb")]
        public SetupProperty<string> MaterialCodeObject
        {
            get => _materialCodeObject;
            set => SetProperty(ref _materialCodeObject, value);
        }

        private SetupProperty<string> _materialCodeObject;

        /// <summary>
        /// Codice del materiale corrente (montato in macchina)
        /// Lasciato per  Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public string MaterialCode
        {
            get => _materialCodeObject.Value;
        }

        /// <summary>
        /// Oggetto Tipo di materiale (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [JsonProperty("MaterialTypeOb")]
        [DirtyableChild]
        public SetupProperty<MaterialTypeEnum> MaterialTypeObject
        {
            get => _materialTypeObject;
            set => SetProperty(ref _materialTypeObject, value);
        }
        private SetupProperty<MaterialTypeEnum> _materialTypeObject;

        /// <summary>
        /// Tipo di materiale corrente(montato in macchina)
        /// Lasciato per  Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public MaterialTypeEnum MaterialType
        {
            get => _materialTypeObject.Value;
        }

        /// <summary>
        /// Oggetto Codice del profilo (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.ProfileCode, AttributeDefinitionGroupEnum.Generic,2)]
        [DirtyableChild]
        [JsonProperty("ProfileCodeOb")]
        public SetupProperty<string> ProfileCodeObject
        {
            get => _profileCodeObject;
            set => SetProperty(ref _profileCodeObject, value);
        }
        private SetupProperty<string> _profileCodeObject;

        /// <summary>
        /// Codice del profilo corrente (montato in macchina)
        /// Lasciato per  Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public string ProfileCode
        {
            get => _profileCodeObject.Value;
        }

        /// <summary>
        /// Oggetto Tipo di profilo (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.ProfileType, AttributeDefinitionGroupEnum.Generic,1)]
        [DirtyableChild]
        [JsonProperty("ProfileTypeOb")]
        public SetupProperty<ProfileTypeEnum> ProfileTypeObject
        {
            get => _profileTypeObject;
            set => SetProperty(ref _profileTypeObject, value);
        }
        private SetupProperty<ProfileTypeEnum> _profileTypeObject;

        /// <summary>
        /// Tipo di profilo corrente (montato in macchina)
        /// Lasciato per  Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public ProfileTypeEnum ProfileType
        {
            get => _profileTypeObject.Value;
        }

        /// <summary>
        /// Oggetto Larghezza anima / larghezza ala destra angolare (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Sa, AttributeDefinitionGroupEnum.Dimensions,2)]
        [DirtyableChild]
        [JsonProperty("SAOb")]
        public SetupProperty<float> SaObject
        {
            get => _sa;
            set => SetProperty(ref _sa, value);
        }
        private SetupProperty<float> _sa;

        /// <summary>
        /// Larghezza anima / larghezza ala destra angolare corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Sa
        {
            get => _sa.Value;
        }

        /// <summary>
        /// Oggetto Spessore anima / spessore ala destra angolare (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Ta, AttributeDefinitionGroupEnum.Dimensions,3)]
        [DirtyableChild]
        [JsonProperty("TAOb")]
        public SetupProperty<float> TaObject
        {
            get => _ta;
            set => SetProperty(ref _ta, value);
        }
        private SetupProperty<float> _ta;

        /// <summary>
        /// Spessore anima / spessore ala destra angolare corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Ta
        {
            get => _ta.Value;
        }

        /// <summary>
        /// Oggetto Larghezza ala destra (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Sba, AttributeDefinitionGroupEnum.Dimensions,4)]
        [DirtyableChild]
        [JsonProperty("SBAOb")]
        public SetupProperty<float> SbaObject
        {
            get => _sba;
            set => SetProperty(ref _sba, value);
        }
        private SetupProperty<float> _sba;

        /// <summary>
        /// Larghezza ala destra corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Sba
        {
            get => _sba.Value;
        }

        /// <summary>
        /// Oggetto Spessore ala destra (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [JsonProperty("TBAOb")]
        [DirtyableChild]
        [GeneralSetupProperties(AttributeDefinitionEnum.Tba, AttributeDefinitionGroupEnum.Dimensions,5)]
        public SetupProperty<float> TbaObject
        {
            get => _tba;
            set => SetProperty(ref _tba, value);
        }
        private SetupProperty<float> _tba;

        /// <summary>
        /// Spessore ala destra corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Tba
        {
            get => _tba.Value;
        }

        /// <summary>
        /// Oggetto Larghezza ala sinistra (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Sbb, AttributeDefinitionGroupEnum.Dimensions,6)]
        [DirtyableChild]
        [JsonProperty("SBBOb")]
        public SetupProperty<float> SbbObject
        {
            get => _sbb;
            set => SetProperty(ref _sbb, value);
        }
        private SetupProperty<float> _sbb;

        /// <summary>
        /// Larghezza ala sinistra corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Sbb
        {
            get => _sbb.Value;
        }

        /// <summary>
        /// Oggetto Spessore ala sinistra (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Tbb, AttributeDefinitionGroupEnum.Dimensions,7)]
        [DirtyableChild]
        [JsonProperty("TBBOb")]
        public SetupProperty<float> TbbObject
        {
            get => _tbb;
            set => SetProperty(ref _tbb, value);
        }
        private SetupProperty<float> _tbb;

        /// <summary>
        /// Spessore ala sinistra corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Tbb
        {
            get => _tbb.Value;
        }

        /// <summary>
        /// Raggio di raccordo del profilo corrente
        /// </summary>       
        [JsonProperty("R")]
        public float Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value);
        }
        private float _radius;

        /// <summary>
        /// oggetto Angolo di curvatura profilo angolare V corrente (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.ProfileAngle, AttributeDefinitionGroupEnum.Dimensions,11)]
        [DirtyableChild]
        [JsonProperty("ANGOb")]
        public SetupProperty<float> AngleObject
        {
            get => _angle;
            set => SetProperty(ref _angle, value);
        }
        private SetupProperty<float> _angle;

        /// <summary>
        /// Angolo di curvatura profilo angolare V corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Angle
        {
            get => _angle.Value;
        }

        /// <summary>
        /// Oggetto Larghezza delle linguette superiori (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Sab, AttributeDefinitionGroupEnum.Dimensions,8)]
        [DirtyableChild]
        [JsonProperty("SABOb")]
        public SetupProperty<float> SabObject
        {
            get => _sab;
            set => SetProperty(ref _sab, value);
        }
        private SetupProperty<float> _sab;

        /// <summary>
        /// Larghezza delle linguette superiori corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Sab
        {
            get => _sab.Value;
        }

        /// <summary>
        /// Profili U di tipo 'Bended'
        /// </summary>
        [JsonProperty("BENDED")]
        public bool Bended
        {
            get => _bended;
            set => SetProperty(ref _bended, value);
        }
        private bool _bended;

        /// <summary>
        /// Disassamento ala destra (profili D)  (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Dsa, AttributeDefinitionGroupEnum.Dimensions,9)]
        [DirtyableChild]
        [JsonProperty("DSAOb")]
        public SetupProperty<float> DsaObject
        {
            get => _dsa;
            set => SetProperty(ref _dsa, value);
        }
        private SetupProperty<float> _dsa;

        /// <summary>
        /// Disassamento ala destra (profili D) corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Dsa
        {
            get => _dsa.Value;
        }

        /// <summary>
        /// Disassamento ala sinistra (profili D) (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Dsb, AttributeDefinitionGroupEnum.Dimensions, 10)]
        [DirtyableChild]
        [JsonProperty("DSBOb")]
        public SetupProperty<float> DsbObject
        {
            get => _dsb;
            set => SetProperty(ref _dsb, value);
        }
        private SetupProperty<float> _dsb;

        /// <summary>
        /// Disassamento ala sinistra (profili D) corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Dsb
        {
            get => _dsb.Value;
        }
      
        /// <summary>
        /// Peso lineare
        /// </summary>
        [JsonProperty("WL")]
        public float LinearWeight
        {
            get => _linearWeight;
            set => SetProperty(ref _linearWeight, value);
        }
        private float _linearWeight;

        /// <summary>
        /// Peso specifico (solo per piastre)
        /// </summary>
        [JsonProperty("SW")]
        public float SpecificWeight
        {
            get => _specificWeight;
            set => SetProperty(ref _specificWeight, value);
        }
        private float _specificWeight;

        /// <summary>
        /// Sezione
        /// </summary>
        [JsonProperty("SEZ")]
        public float Section
        {
            get => _section;
            set => SetProperty(ref _section, value);
        }
        private float _section;

        /// <summary>
        /// oggetto Lunghezza lastra/barra (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.Length, AttributeDefinitionGroupEnum.Dimensions,1)]
        [DirtyableChild]
        [JsonProperty("LBOb")]
        public SetupProperty<float> LbObject
        {
            get => _lb;
            set => SetProperty(ref _lb, value);
        }
        private SetupProperty<float> _lb;

        /// <summary>
        /// Lunghezza lastra/barra corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Lb
        {
            get => _lb.Value;
        }
      
        /// <summary>
        /// Lunghezza barra rilevata
        /// </summary>
        [JsonProperty("LBR")]
        public float Lbr
        {
            get => _relBarLength;
            set => SetProperty(ref _relBarLength, value);
        }
        private float _relBarLength;

        /// <summary>
        /// Lunghezza barra rilevata iniziale
        /// </summary>
        [JsonProperty("LBRI")]
        public float Lbri
        {
            get => _Lbri;
            set => SetProperty(ref _Lbri, value);
        }
        private float _Lbri;


        /// <summary>
        /// Oggetto Angolo di taglio iniziale anima (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary> 
        [GeneralSetupProperties(AttributeDefinitionEnum.WebInitialAngle, AttributeDefinitionGroupEnum.Dimensions,12)]
        [DirtyableChild]
        [JsonProperty("RIOb")]
        public SetupProperty<float> RiObject
        { 
            get => _riAngle;
            set => SetProperty(ref _riAngle, value);
        }
        private SetupProperty<float> _riAngle;

        /// <summary>
        /// Angolo di taglio iniziale anima corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float Ri
        {
            get => RiObject.Value;
        }

        /// <summary>
        /// Oggetto Angolo di taglio finale anima (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.WebFinalAngle, AttributeDefinitionGroupEnum.Dimensions,13)]
        [DirtyableChild]
        [JsonProperty("RFOb")]
        public SetupProperty<float> RfObject
        {
            get => _rfAngle;
            set => SetProperty(ref _rfAngle, value);
        }
        private SetupProperty<float> _rfAngle;

        /// <summary>
        /// Angolo di taglio finale anima corrente
        /// </summary>
        [JsonIgnore]
        public float Rf
        {
            get => _rfAngle.Value;
        }
        /// <summary>
         /// Angolo di taglio finale ala
         /// </summary>
        [JsonProperty("RBF")]
        public float Rbf
        {
            get => _rbfAngle;
            set => SetProperty(ref _rbfAngle, value);
        }
        private float _rbfAngle;

        /// <summary>
        /// Oggetto Tipo di motrice (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.CarriageType, AttributeDefinitionGroupEnum.Properties,1)]
        [DirtyableChild]
        [JsonProperty("CarrigeTypeOb")]
        public SetupProperty<CarriageTypeEnum> CarriageTypeObject
        {
            get => _carriageType;
            set => SetProperty(ref _carriageType, value);
        }
        private SetupProperty<CarriageTypeEnum> _carriageType;

        /// <summary>
        /// Tipo di motrice corrente (montato in macchina)
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public CarriageTypeEnum CarriageType
        {
            get => _carriageType.Value;
        }

        /// <summary>
        /// Oggetto Profilo U con ali sui rulli (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.PuFlangesPosition, AttributeDefinitionGroupEnum.Properties,2)]
        [JsonProperty("PUOb")]
        public SetupProperty<PuTypeEnum> PuObject
        {
            get => _pu;
            set => SetProperty(ref _pu, value);
        }
        private SetupProperty<PuTypeEnum> _pu;

        /// <summary>
        /// Profilo U con ali sui rulli (valore corrente montato in macchina)
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public bool Pu
        {
            get => _pu.Value==PuTypeEnum.FlangesDownward?true:false;
        }

        /// <summary>
        /// Oggetto Posizione pinza A (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.PincherAPosition, AttributeDefinitionGroupEnum.Dimensions, 14)]
        [DirtyableChild]
        [JsonProperty("PosCaOb")]
        public SetupProperty<float> PosCaObject
        {
            get => _posCaObject;
            set => SetProperty(ref _posCaObject, value);
        }
        private SetupProperty<float> _posCaObject;

        /// <summary>
        /// Posizione pinza A corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float PosCa
        {
            get => _posCaObject.Value;
        }

        /// <summary>
        /// Oggetto Posizione pinza B (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.PincherBPosition, AttributeDefinitionGroupEnum.Dimensions, 15)]
        [DirtyableChild]
        [JsonProperty("PosCbOb")]
        public SetupProperty<float> PosCbObject
        {
            get => _posCbObject;
            set => SetProperty(ref _posCbObject, value);
        }
        private SetupProperty<float> _posCbObject;

        /// <summary>
        /// Posizione pinza B corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float PosCb
        {
            get => _posCbObject.Value;
        }

        /// <summary>
        /// Oggetto Posizione pinza C (contiene sia il valore corrente che quello richiesto dal programma corrente)
        /// </summary>
        [GeneralSetupProperties(AttributeDefinitionEnum.PincherCPosition, AttributeDefinitionGroupEnum.Dimensions, 16)]
        [DirtyableChild]
        [JsonProperty("PosCcOb")]
        public SetupProperty<float> PosCcObject
        {
            get => _posCcObject;
            set => SetProperty(ref _posCcObject, value);
        }
        private SetupProperty<float> _posCcObject;

        /// <summary>
        /// Posizione pinza C corrente
        /// Lasciato per Processing interfaccia IReadOnlyGeneralSetup
        /// </summary>
        [JsonIgnore]
        public float PosCc
        {
            get => _posCcObject.Value;  
        }

        /// <summary>
        /// Posizione pinza A rilevata
        /// </summary>
        [JsonProperty("PZA")]
        public float PosPzA
        {
            get => _posPzA;
            set => SetProperty(ref _posPzA, value);
        }
        private float _posPzA;

        /// <summary>
        /// Posizione pinza B rilevata
        /// </summary>
        [JsonProperty("PZB")]
        public float PosPzB
        {
            get => _posPzB;
            set => SetProperty(ref _posPzB, value);
        }
        private float _posPzB;

        /// <summary>
        /// Posizione pinza C rilevata
        /// </summary>
        [JsonProperty("PZC")]
        public float PosPzC
        {
            get => _posPzC;
            set => SetProperty(ref _posPzC, value);
        }
        private float _posPzC;

        /// <summary>
        /// Offset pinza rilevato
        /// </summary>
        [JsonProperty("OFFPZ")]
        public float OffPz
        {
            get => _offPz;
            set => SetProperty(ref _offPz, value);
        }
        private float _offPz;

        /// <summary>
        /// Numero della stazione corrente (Gemini)
        /// </summary>
        [JsonProperty("Station")]
        public int Station
        {
            get => _station;
            set => SetProperty(ref _station, value);
        }
        private int _station;

        /// <summary>
        /// Piano corrente di lavoro del profilo 1=DA 2=DB 3=DC 4=DD 5=DB rotoT (TANG_V11)
        /// </summary>
        [JsonProperty("PL")]
        public float Pl
        {
            get => _pl;
            set => SetProperty(ref _pl, value);
        }
        private float _pl;

        /// <summary>
        /// Origine pezzo 0=iniziale, 1=finale, 2=mezzeria (TANG_V11)
        /// </summary>
        [JsonProperty("OP")]
        public float Op
        {
            get => _op;
            set => SetProperty(ref _op, value);
        }
        private float _op;

        /// <summary>
        /// Correttore origine X pezzo (TANG_V11)
        /// </summary>
        [JsonProperty("OC")]
        public float Oc
        {
            get => _oc;
            set => SetProperty(ref _oc, value);
        }
        private float _oc;

        /// <summary>
        /// Origine pezzo nel caso di OP=3 (TANG_V11)
        /// </summary>
        [JsonProperty("OCX")]
        public float Ocx
        {
            get => _ocx;
            set => SetProperty(ref _ocx, value);
        }
        private float _ocx;

        /// <summary>
        /// Invalida lo stato attuale dell'oggetto: le proprietà dell'oggetto non sono valide quindi non è possibile calcolare lo stato globale del General Setup   
        /// </summary>
        public void InvalidateStatus()
        {
            this.SetupAction = SetupActionEnum.ToValidate;
        }

        /// <summary>
        /// Valida lo stato attuale dell'oggetto: le proprietà dell'oggetto assumono valori validi per calcolare lo stato globale del General Setup   
        /// </summary>
        public void ValidateStatus()
        {
            if (this.SetupAction==SetupActionEnum.ToValidate)
                this.SetupAction = SetupActionEnum.NotUsed;
        }


        /// <summary>
        /// Funzione che assegna il valore Required delle proprietà ( dati del programma caricato) al valore corrente (Value)
        /// </summary>
        /// <returns></returns>
        public bool ConfirmGeneralSetup()
        {
            // Se lo stato è SetupActionEnum.ToValidate o SetupActionEnum.NotUsed
            // allora non devo aggiornare nulla
            if (this.SetupAction == SetupActionEnum.ToValidate ||
                this.SetupAction == SetupActionEnum.NotUsed)
                return false;

            if (MaterialTypeObject.Action == SetupActionEnum.RequiredConfirm)
                MaterialTypeObject.SetValueToRequired();
            if (MaterialCodeObject.Action == SetupActionEnum.RequiredConfirm)
                MaterialCodeObject.SetValueToRequired();

            if (ProfileTypeObject.Action == SetupActionEnum.RequiredConfirm)
                ProfileTypeObject.SetValueToRequired();
            if (ProfileCodeObject.Action == SetupActionEnum.RequiredConfirm)
                ProfileCodeObject.SetValueToRequired();

            if (SaObject.Action == SetupActionEnum.RequiredConfirm)
                SaObject.SetValueToRequired();
            if (TaObject.Action == SetupActionEnum.RequiredConfirm)
                TaObject.SetValueToRequired();
            if (SbaObject.Action == SetupActionEnum.RequiredConfirm)
                SbaObject.SetValueToRequired();
            if (TbaObject.Action == SetupActionEnum.RequiredConfirm)
                TbaObject.SetValueToRequired();
            if (SbbObject.Action == SetupActionEnum.RequiredConfirm)
                SbbObject.SetValueToRequired();
            if (TbbObject.Action == SetupActionEnum.RequiredConfirm)
                TbbObject.SetValueToRequired();
            if (SabObject.Action == SetupActionEnum.RequiredConfirm)
                SabObject.SetValueToRequired();
            if (DsaObject.Action == SetupActionEnum.RequiredConfirm)
                DsaObject.SetValueToRequired();
            if (DsbObject.Action == SetupActionEnum.RequiredConfirm)
                DsbObject.SetValueToRequired();
            if (AngleObject.Action == SetupActionEnum.RequiredConfirm)
                AngleObject.SetValueToRequired();

            if (CarriageTypeObject.Action == SetupActionEnum.RequiredConfirm)
                CarriageTypeObject.SetValueToRequired();
            if (PuObject.Action == SetupActionEnum.RequiredConfirm)
                PuObject.SetValueToRequired();

            if (PosCaObject.Action == SetupActionEnum.RequiredConfirm)
                PosCaObject.SetValueToRequired();
            if (PosCbObject.Action == SetupActionEnum.RequiredConfirm)
                PosCbObject.SetValueToRequired();
            if (PosCcObject.Action == SetupActionEnum.RequiredConfirm)
                PosCcObject.SetValueToRequired();

            return true;
        }

        /// <summary>
        /// Reset di tutte le proprietà Required del setup generale 
        /// </summary>
        public void ResetRequired()
        {
            MaterialCodeObject.ResetRequiredValue();
            ProfileTypeObject.ResetRequiredValue();
            ProfileCodeObject.ResetRequiredValue();
            MaterialTypeObject.ResetRequiredValue();
            SaObject.ResetRequiredValue();
            TaObject.ResetRequiredValue();
            SbaObject.ResetRequiredValue();
            TbaObject.ResetRequiredValue();
            SbbObject.ResetRequiredValue();
            TbbObject.ResetRequiredValue();
            SabObject.ResetRequiredValue();
            DsaObject.ResetRequiredValue();
            DsbObject.ResetRequiredValue();
            AngleObject.ResetRequiredValue();
            LbObject.ResetRequiredValue();
            PosCaObject.ResetRequiredValue();
            PosCbObject.ResetRequiredValue();
            PosCcObject.ResetRequiredValue();

            RiObject.ResetRequiredValue();
            RfObject.ResetRequiredValue();

            Rbf = 0;

            LinearWeight = 0;
            SpecificWeight = 0;
            Section = 0;
            PuObject.ResetRequiredValue();

            Bended = false;
            CarriageTypeObject.ResetRequiredValue();

            // Stato del setup generale
            SetupAction = SetupActionEnum.NotRequired;
        }

        /// <summary>
        /// Aggiornamento della action(stato) del setup generale(cumulativa) con quella della proprietà data
        /// </summary>
        private SetupActionEnum SetCumulativeSetupAction(SetupActionEnum cumulativeSetupAction, SetupActionEnum propertyAction)
        {
            switch (cumulativeSetupAction)
            {
                case SetupActionEnum.NotUsed:
                    // NotUsed e NotRequired non danno uno stato di allarme
                    if (propertyAction == SetupActionEnum.NotUsed || propertyAction == SetupActionEnum.NotRequired)
                        cumulativeSetupAction = SetupActionEnum.Ok;
                    else
                        cumulativeSetupAction = propertyAction;
                    break;
                case SetupActionEnum.Ok:
                    // L'unica cosa che può cambiare il mio stato è RequiredConfirm
                    if (propertyAction == SetupActionEnum.RequiredConfirm)
                        cumulativeSetupAction = propertyAction;
                    break;
            }
            return cumulativeSetupAction;
        }

        /// <summary>
        /// Aggiornamento dello stato del setup generale e degli stati delle singole proprietà(materiale, profilo, dimensioni ...)
        /// </summary>
        public void UpdateStatus()
        {
            if (ProgramType == ProgramTypeEnum.G_93)
                return;

            if (SetupAction != SetupActionEnum.ToValidate)
            {
                SetupActionEnum calcSetupAction = SetupActionEnum.NotUsed;

                // Tipo di profilo
                if (!ProfileTypeObject.IsRequiredValueDefault())
                    ProfileTypeObject.UpdateStatus();
                else
                    ProfileTypeObject.Action = SetupActionEnum.NotRequired;
                calcSetupAction = SetCumulativeSetupAction(calcSetupAction, ProfileTypeObject.Action);

                // Codice profilo
                if (!ProfileCodeObject.IsRequiredValueDefault())
                    ProfileCodeObject.UpdateStatus();
                else
                    ProfileCodeObject.Action = SetupActionEnum.NotRequired;
                calcSetupAction = SetCumulativeSetupAction(calcSetupAction, ProfileCodeObject.Action);

                // Le discrepanze di tipo di materiale non influiscono sullo stato del setup Generale
                if (MaterialTypeObject.IsRequiredValueDefault())
                    MaterialTypeObject.Action = SetupActionEnum.NotRequired;

                // Le discrepanze di codice materiale non influiscono sullo stato del setup Generale
                if (MaterialCodeObject.IsRequiredValueDefault())
                    MaterialCodeObject.Action = SetupActionEnum.NotRequired;

                // Guardo se è cambiato il tipo di motrice
                if (CarriageTypeObject.Action != SetupActionEnum.NotRequired)
                    CarriageTypeObject.UpdateStatus();
                calcSetupAction = SetCumulativeSetupAction(calcSetupAction, CarriageTypeObject.Action);

                // Guardo se è cambiato flag profilo U
                if (PuObject.Action != SetupActionEnum.NotRequired)
                    PuObject.UpdateStatus();
                calcSetupAction = SetCumulativeSetupAction(calcSetupAction, PuObject.Action);

                // TODO: da implementare (il codice sotto è quello che c'era in APGS)
                //// Controllo LB con tolleranza
                //if (chk_lb && mrin->esrmis)
                //{
                //    if (!CalcolaTlr(dp1->dp_tool.tlr_lb, CON_LB, SUP_LBR))
                //        return (UT_NOK);
                //}

                // Controllo dimensioni profilo (se richiesto)
                if (!ProfileTypeObject.IsRequiredValueDefault())
                {
                    if (SaObject.Action != SetupActionEnum.NotRequired)
                        SaObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, SabObject.Action);

                    if (TaObject.Action != SetupActionEnum.NotRequired)
                        TaObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, TaObject.Action);

                    if (SbaObject.Action != SetupActionEnum.NotRequired)
                        SbaObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, SbaObject.Action);

                    if (TbaObject.Action != SetupActionEnum.NotRequired)
                        TbaObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, TbaObject.Action);

                    if (SbbObject.Action != SetupActionEnum.NotRequired)
                        SbbObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, SbbObject.Action);

                    if (TbbObject.Action != SetupActionEnum.NotRequired)
                        TbbObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, TbbObject.Action);

                    if (DsaObject.Action != SetupActionEnum.NotRequired)
                        DsaObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, DsaObject.Action);

                    if (DsbObject.Action != SetupActionEnum.NotRequired)
                        DsbObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, DsbObject.Action);

                    if (SabObject.Action != SetupActionEnum.NotRequired)
                        SabObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, SabObject.Action);

                    if (AngleObject.Action != SetupActionEnum.NotRequired)
                        AngleObject.UpdateStatus();
                    calcSetupAction = SetCumulativeSetupAction(calcSetupAction, AngleObject.Action);
                }
                SetupAction = calcSetupAction;
            }
        }
    }
}

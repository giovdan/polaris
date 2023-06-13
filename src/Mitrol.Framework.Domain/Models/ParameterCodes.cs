namespace Mitrol.Framework.Domain.Models
{
    using System;

    public class ParameterCodeAttribute : Attribute
    {
        public ParameterCodeAttribute(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }

    public class LinearNestingParameterAttribute : Attribute
    {
        public LinearNestingParameterAttribute()
        {
           
        }
    }

    /// <summary>
    /// Definizione di tutti gli indici dei parametri utilizzati nella HMI (Execution e Processing)
    /// </summary>
    public class ParameterCodes
    {
        #region < Definizione parametri cnc Fanuc >

        // Distanza minima tra le due teste di taglio montate sull'asse V, con asse Y di foratura configurato
        public const string PAR_V606_1 = "PAR606-C";

        // Distanza minima tra le due teste di foratura o taglio montate sugli assi Y o V (esclusivi)
        public const string PAR_V608_1 = "PAR608-C";

        // Offset X torcia plasma C
        public const string PAR_V613_1 = "PAR613-C";
        // Offset X torcia plasma D
        public const string PAR_V613_2 = "PAR613-D";

        // Offset Y torcia plasma C
        public const string PAR_V614_1 = "PAR614-C";
        // Offset Y torcia plasma D
        public const string PAR_V614_2 = "PAR614-D";

        // Offset X palpatore torcia C
        public const string PAR_V615_1 = "PAR615-C";
        // Offset X palpatore torcia D
        public const string PAR_V615_2 = "PAR615-D";

        // Offset Y palpatore torcia C
        public const string PAR_V616_1 = "PAR616-C";
        // Offset Y palpatore torcia D
        public const string PAR_V616_2 = "PAR616-D";

        // Corsa asse U finestra pressore
        public const string PAR_V620_1 = "PAR620-C";
        // Corsa asse U finestra pressore
        public const string PAR_V620_2 = "PAR620-D";

        // Raggio di utilizzo deformazione piastra Plasma C
        public const string PAR_V621_1 = "PAR621-C";
        // Raggio di utilizzo deformazione piastra Plasma D
        public const string PAR_V621_2 = "PAR621-D";

        // Offset X torcia C ossitaglio
        public const string PAR_V640_1 = "PAR640-C";
        // Offset X torcia D ossitaglio
        public const string PAR_V640_2 = "PAR640-D";

        // Offset Y torcia C ossitaglio
        public const string PAR_V641_1 = "PAR641-C";
        // Offset Y torcia D ossitaglio
        public const string PAR_V641_2 = "PAR641-D";

        // Lunghezza utensile massima
        public const string PAR_V650_1 = "PAR650-C";

        // Distanza tra le palpature del percorso libero (LEAD-CUT) di fresatura testa C
        public const string PAR_V651_1 = "PAR651-C";
        // Distanza tra le palpature del percorso libero (LEAD-CUT) di fresatura testa D
        public const string PAR_V651_2 = "PAR651-D";

        // Minimo delta torcia C slave 2 ossitaglio
        public const string PAR_V672_1 = "PAR672-C";
        // Minimo delta torcia D slave 2 ossitaglio
        public const string PAR_V672_2 = "PAR672-D";

        // Minimo delta torcia C slave 3 ossitaglio
        public const string PAR_V673_1 = "PAR673-C";
        // Minimo delta torcia D slave 3 ossitaglio
        public const string PAR_V673_2 = "PAR673-D";

        // Minimo delta torcia C slave 4 ossitaglio
        public const string PAR_V679_1 = "PAR679-C";
        // Minimo delta torcia D slave 4 ossitaglio
        public const string PAR_V679_2 = "PAR679-D";

        // Offset X torcia C ossitaglio Bevel
        public const string PAR_V681_1 = "PAR681-C";
        // Offset X torcia D ossitaglio Bevel
        public const string PAR_V681_2 = "PAR681-D";

        // Offset Y torcia C ossitaglio Bevel
        public const string PAR_V682_1 = "PAR682-C";
        // Offset Y torcia D ossitaglio Bevel
        public const string PAR_V682_2 = "PAR682-D";

        // Offset torcia C ossitaglio Bevel Bottom rispetto alla torcia centrale diritta
        public const string PAR_V683_1 = "PAR683-C";
        // Offset torcia D ossitaglio Bevel Bottom rispetto alla torcia centrale diritta
        public const string PAR_V683_2 = "PAR683-D";

        // Offset torcia C ossitaglio Bevel Top    rispetto alla torcia centrale diritta
        public const string PAR_V684_1 = "PAR684-C";
        // Offset torcia D ossitaglio Bevel Top    rispetto alla torcia centrale diritta
        public const string PAR_V684_2 = "PAR684-D";

        // Offset X palpatore meccanico testa C di foratura (non utilizzato ! Mazza 10/12/2013)
        public const string PAR_V685_1 = "PAR685-C";
        // Offset X palpatore meccanico testa D di foratura (non utilizzato ! Mazza 10/12/2013)
        public const string PAR_V685_2 = "PAR685-D";

        // Offset Y palpatore meccanico testa C di foratura (non utilizzato ! Mazza 10/12/2013)
        public const string PAR_V686_1 = "PAR686-C";
        // Offset Y palpatore meccanico testa D di foratura (non utilizzato ! Mazza 10/12/2013)
        public const string PAR_V686_2 = "PAR686-D";

        // Raggio di utilizzo deformazione piastra foratura C
        public const string PAR_V689_1 = "PAR689-C";
        // Raggio di utilizzo deformazione piastra foratura D
        public const string PAR_V689_2 = "PAR689-D";

        // Minimo delta torcia C slave 5 ossitaglio
        public const string PAR_V772_1 = "PAR772-C";
        // Minimo delta torcia D slave 5 ossitaglio
        public const string PAR_V772_2 = "PAR772-D";

        // Ottimizzazione taglio: Distanza per interpolazione X V W
        public const string PAR_V923_1 = "PAR923-C";
        // Ottimizzazione taglio: Distanza per interpolazione X V W
        public const string PAR_V923_2 = "PAR923-D";

        // XPR: Riduzione velocità taglio durante i cerchi (HOLE) canale 1
        public const string PAR_V924_1 = "PAR924-C";
        // XPR: Riduzione velocità taglio durante i cerchi (HOLE) canale 2
        public const string PAR_V924_2 = "PAR924-D";

        // XPR: Valore % del raggio di uscita dal foro di plasma (HOLE) canale 1
        public const string PAR_V925_1 = "PAR925-C";
        // XPR: Valore % del raggio di uscita dal foro di plasma (HOLE) canale 2
        public const string PAR_V925_2 = "PAR925-D";

        // XPR: Valore max. del raggio di uscita dal foro di plasma (HOLE) canale 1
        public const string PAR_V926_1 = "PAR926-C";
        // XPR: Valore max. del raggio di uscita dal foro di plasma (HOLE) canale 2
        public const string PAR_V926_2 = "PAR926-D";

        // XPR: Valore max. diametro del foro di plasma per disabilitare stand-off (HOLE) canale 1
        public const string PAR_V927_1 = "PAR927-C";
        // XPR: Valore max. diametro del foro di plasma per disabilitare stand-off (HOLE) canale 2
        public const string PAR_V927_2 = "PAR927-D";

        // Raggio aspirazione trucioli
        public const string PAR_V938_1 = "PAR938-C";
        // Raggio aspirazione trucioli
        public const string PAR_V938_2 = "PAR938-D";

        // Offset X marcatrice a scrittura 38 caratteri (TipoG) canale 1
        public const string PAR_V946_1 = "PAR946-C";
        // Offset X marcatrice a scrittura 38 caratteri (TipoG) canale 2
        public const string PAR_V946_2 = "PAR946-D";
        
        // Offset Y marcatrice a scrittura 38 caratteri (TipoG) canale 1
        public const string PAR_V947_1 = "PAR947-C";
        // Offset Y marcatrice a scrittura 38 caratteri (TipoG) canale 2
        public const string PAR_V947_2 = "PAR947-D";
        
        // Altezza Y del carattere marcatrice a scrittura 38 caratteri (TipoG)
        public const string PAR_V948_1 = "PAR948-C";

        // Larghezza X del carattere marcatrice a scrittura 38 caratteri (TipoG)
        public const string PAR_V949_1 = "PAR949-C";

        // % della velocità (ridotta) durante il taglio (programmata da SR2158)
        public const string PAR_V953_1 = "PAR953-C";

        // Spessore oltre il quale si inverte la sequenza tagli bevel multipli
        public const string PAR_V955_1 = "PAR955-C";

        // Offset X pinzatura tubi tondi (TipoG)
        public const string PAR_V956_1 = "PAR956-C";

        // Offset Y pinzatura tubi tondi (TipoG)
        public const string PAR_V957_1 = "PAR957-C";

        // Offset X marcatrice a getto d'inchiostro canale 1
        public const string PAR_V970_1 = "PAR970-C";
        // Offset X marcatrice a getto d'inchiostro canale 2
        public const string PAR_V970_2 = "PAR970-D";
        
        // Offset Y marcatrice a getto d'inchiostro canale 1
        public const string PAR_V971_1 = "PAR971-C";
        // Offset Y marcatrice a getto d'inchiostro canale 2
        public const string PAR_V971_2 = "PAR971-D";

        // TIPO G: Quota Y per esclusione Z in zona pinze /pegaso se TipoG interrompe JUMP
        public const string PAR_V979_1 = "PAR979-C";
        // TIPO G: Quota Y per esclusione Z in zona pinze /pegaso se TipoG interrompe JUMP
        public const string PAR_V979_2 = "PAR979-D";
        
        // Distanza 1 tra pinze CA e CB - min (TipoG)
        public const string PAR_V980_1 = "PAR980-C";

        // Distanza 2 tra pinze CA e CB - max (TipoG)
        public const string PAR_V981_1 = "PAR981-C";

        // Offset spazzolatura (TipoG) canale 1
        public const string PAR_V982_1 = "PAR982-C";
        // Offset spazzolatura (TipoG) canale 2
        public const string PAR_V982_2 = "PAR982-D";

        // Quota Y bordo pinze per calcoli orgine NY (TipoG)
        public const string PAR_V987_1 = "PAR987-C";

        // Offset botola di scarico pezzi (TipoG) canale 1
        public const string PAR_V988_1 = "PAR988-C";
        // Offset botola di scarico pezzi (TipoG) canale 2
        public const string PAR_V988_2 = "PAR988-D";

        // Minimo delta torcia C slave 2 plasma
        public const string PAR_V691_1 = "PAR691-C";
        // Minimo delta torcia D slave 2 plasma
        public const string PAR_V691_2 = "PAR691-D";
        
        // Limite MAX corsa asse Y di foratura C
        public const string PAR_V765_1 = "PAR765-C";
        // Limite MAX corsa asse Y di foratura C
        public const string PAR_V765_2 = "PAR765-D";
        
        // Limite MIN corsa asse Y di foratura C
        public const string PAR_V766_1 = "PAR766-C";
        // Limite MIN corsa asse Y di foratura D
        public const string PAR_V766_2 = "PAR766-D";

        #endregion < Definizione parametri cnc Fanuc >

        #region < Definizione parametri cnc Mitrol >

        public const string G005_SUPPL_FREANG = "G0005";
        public const string G006_OVLAP_FREANG = "G0006";
        public const string I_OFF_MAND1 = "G0007";
        public const string G010_OFF_MARCA = "G0010";
        public const string G011_OFF_TAGLIO = "G0011";
        public const string G012_OFF_MK_SCR = "G0012";
        public const string G014_DELTA_SFRIDO = "G0014";
        public const string G022_MCGF_FORO_INCL = "G0022";
        public const string G023_MEL_C10R = "G0023";
        public const string G024_MEP_C10R = "G0024";
        public const string G029_TOL_PRF_TAN = "G0029";
        public const string G030_MINX_PZ_SCAR = "G0030";
        public const string G031_MINY_PZ_SCAR = "G0031";
        public const string G042_PSX_CAR = "G0042";
        public const string G051_MIN_ALA = "G0051";
        public const string G052_MIN_ANI = "G0052";
        public const string G053_CORR_OFFX_A = "G0053";
        public const string G054_CORR_OFFX_B = "G0054";
        public const string G055_CORR_OFFX_C = "G0055";
        public const string I_OFF_T0 = "G0057";
        public const string G059_OFF_MAND_U_1 = "G0059";
        public const string G084_R_SENS_PZ = "G0084";
        public const string G085_OFF_R_SENS_PZ = "G0085";
        public const string G086_OFF_MAND_U_2 = "G0086";
        public const string G087_Y_RIPO = "G0087";
        public const string G116_MIN_ANI_W = "G0116";
        public const string G119_OFF_FORAV = "G0119";
        public const string G124_OFF_MAND1_T = "G0124";
        public const string G125_LUCE_SEGA = "G0125";
        public const string G128_MIN_LONG = "G0128";
        public const string G138_OFF_CAL = "G0138";
        public const string G144_PSY_CAR = "G0144";
        public const string G145_LUCE_IMPIANTO = "G0145";
        public const string G147_MAX_DIM_STZ = "G0147";
        public const string G149_OFF_L_TRIA_AX2 = "G0149";
        public const string G150_OFF_T_TRIA_AX2 = "G0150";
        public const string G151_L_MAX_AX2 = "G0151";
        public const string G152_OFF_L_TRIA_AX3 = "G0152";
        public const string G153_OFF_T_TRIA_AX3 = "G0153";
        public const string G154_L_MAX_AX3 = "G0154";
        public const string G155_OFF_X_RIPO = "G0155";
        public const string G156_OFF_Y_RIPO = "G0156";
        public const string G157_L_C10R_NRH = "G0157";
        public const string G158_H1_C10R = "G0158";
        public const string G159_H2_C10R = "G0159";
        public const string G160_L_MAX_C10R_L = "G0160";
        public const string G161_L_B13R = "G0161";
        public const string G162_H1_B13R = "G0162";
        public const string G163_H2_B13R = "G0163";
        public const string I_GEN_SCA_SPE = "G0169";
        public const string G178_PIA_K1 = "G0178";
        public const string G179_PIA_K2 = "G0179";
        public const string G180_PIA_K3 = "G0180";
        public const string G181_PIA_K4 = "G0181";
        public const string G182_PIA_K5 = "G0182";
        public const string G188_MIN_VW = "G0188";
        public const string G194_LARG_BOTO = "G0194";
        public const string G195_MIN_LP_ACC = "G0195";
        public const string G198_LARG_PZ = "G0198";
        public const string G199_DIST_PZ_FF = "G0199";
        public const string G202_RANGEX_OPT_MILL = "G0202";
        public const string G284_LUNG_PROL = "G0284";
        public const string G285_LARG_PROL = "G0285";
        public const string G286_LARG_SEGA = "G0286";
        public const string G288_RIPO_AUT = "G0288";
        public const string G306_OFF_MAND3E_T = "G0306";
        public const string G307_OFF_MAND3F_T = "G0307";
        public const string G313_OFF_CONT_SPIA = "G0313";
        public const string G314_FASCIA_IN_PRESA = "G0314";
        public const string G322_OFF_X_MKJET = "G0322";
        public const string G341_OFF_L_ROMBO = "G0341";
        public const string G342_OFF_T_ROMBO = "G0342";
        public const string G346_CRTL_RULLO = "G0346";
        public const string G353_OFF_FASCIO = "G0353";
        public const string G356_OFF_SPAZZOLA = "G0356";
        public const string G367_L_C10R_NLH = "G0367";
        public const string G368_SBOS_RETT = "G0368";
        public const string I_GEN_CRS = "G0375";
        public const string G380_G_MAX_Y = "G0380";
        public const string G381_G_MIN_W = "G0381";
        public const string G392_OFF_MK_T = "G0392";
        public const string G393_OFF_NRH1_T = "G0393";
        public const string G396_OFF_NLL1_T = "G0396";
        public const string G403_OFF_L_TRIA = "G0403";
        public const string G404_OFF_T_TRIA = "G0404";
        public const string G405_OFF_L_RETT = "G0405";
        public const string G406_OFF_T_RETT = "G0406";
        public const string G407_OFF_L_TRIAD = "G0407";
        public const string G408_OFF_T_TRIAD = "G0408";
        public const string G409_OFF_NRH1 = "G0409";
        public const string G410_OFF_NRH2 = "G0410";
        public const string G411_OFF_NLH1_L1 = "G0411";
        public const string G412_OFF_NLH2_L2 = "G0412";
        public const string G413_D_TRIA_NUT = "G0413";
        public const string G414_D_RETT_NUT = "G0414";
        public const string G416_L_MAX_AX0 = "G0416";
        public const string G417_L_MAX_B10 = "G0417";
        public const string G418_L_MAX_C10 = "G0418";
        public const string G420_SBOS_TRIAD_F = "G0420";
        public const string I_GEN_MIN_LP = "G0435";
        public const string G458_OFF_TC1_X = "G0458";
        public const string G459_OFF_TC1_Y = "G0459";
        public const string G460_OFF_TC2_X = "G0460";
        public const string G461_OFF_TC2_Y = "G0461";
        public const string I_OFF_SCA_CUT = "G0463";
        public const string G464_OVER_CUT_OXY = "G0464";
        public const string I_PREC_CALC_CUT = "G0465";
        public const string G628_OFF_C10R_NRH1 = "G0628";
        public const string G629_OFF_C10R_NRH1_T = "G0629";
        public const string G630_OFF_B13R_NRH1 = "G0630";
        public const string G631_OFF_B13R_NRH1_T = "G0631";
        public const string G632_L_C10R_NLL = "G0632";
        public const string G633_TOLL_RIM = "G0633";
        public const string G641_PALP_I_F = "G0641";
        public const string G642_RANGEX_OPT = "G0642";
        public const string G671_OFF_MAND2_T = "G0671";
        public const string G672_OFF_MAND3_T = "G0672";
        public const string G674_RISC_ZERO_LUN = "G0674";
        public const string G675_RISC_ZERO_ALT = "G0675";
        public const string G676_RISC_ZERO_PAS = "G0676";
        public const string G677_RISC_ZERO_NUM = "G0677";
        public const string G745_OFF_SEGA = "G0745";
        public const string G746_OFF_TC2P_X = "G0746";
        public const string G747_OFF_TC2P_Y = "G0747";
        public const string G748_OFF_TC2O_X = "G0748";
        public const string G749_OFF_TC2O_Y = "G0749";
        public const string G770_OFF_L_TRIAD_B11 = "G0770";
        public const string G771_OFF_T_TRIAD_B11 = "G0771";
        public const string G772_OFF_L_TRIAD_B12 = "G0772";
        public const string G773_OFF_T_TRIAD_B12 = "G0773";
        public const string G774_L_MAX_B11 = "G0774";
        public const string G775_L_MAX_B12 = "G0775";
        public const string G798_G_AUX = "G0798";
        public const string G830_OFF_L_TRIA_1 = "G0830";
        public const string G831_OFF_T_TRIA_1 = "G0831";
        public const string G832_L_MAX_AX1 = "G0832";
        public const string G833_DELTA_AX1 = "G0833";
        public const string G838_TOLL_MAT_FOR = "G0838";
        public const string G839_OFF_PALP_X = "G0839";
        public const string G840_OFF_PALP_Y = "G0840";
        public const string G847_TOL_SPIGOLO = "G0847";
        public const string G848_PRI_INC = "G0848";
        public const string G849_SEC_INC = "G0849";
        public const string G910_OFF_PALP_2_X = "G0910";
        public const string G911_OFF_PALP_2_Y = "G0911";
        public const string G918_MAX_PALP_FRESA = "G0918";
        public const string G921_TH_DIA = "G0921";
        public const string G922_TH_FEED = "G0922";
        public const string G925_D_TSC = "G0925";
        public const string G926_TSC_V_POS = "G0926";
        public const string G927_TSC_V_NEG = "G0927";
        public const string G928_MG_COL_TSA = "G0928";
        public const string G930_TSC_X_AVA = "G0930";
        public const string G931_TSC_X_IND = "G0931";
        public const string G932_MDV_DIN = "G0932";
        public const string G933_MDV_LIN = "G0933";
        public const string G944_UTROT_X = "G0944";
        public const string G945_UTROT_Y = "G0945";
        public const string G992_EXTRA_RET_C10 = "G0992";
        public const string G993_OFF_SPINGITORE = "G0993";
        public const string G998_PERC_R_LEADOUT = "G0998";
        public const string G999_MAX_R_LEADOUT = "G0999";
        public const string G1311_MAX_DN_STDOFF = "G1311";
        public const string G1312_LIM_PRES_EXC = "G1312";
        public const string G1318_MATROT_X = "G1318";
        public const string G1319_MATROT_Y = "G1319";
        public const string G1324_MATROT_C = "G1324";
        public const string G1326_OFFX_VASSOIO = "G1326";
        public const string G1327_OFFY_VASSOIO = "G1327";
        public const string G1329_LUNG_VASSOIO = "G1329";
        public const string G1330_LARG_VASSOIO = "G1330";
        public const string G1331_DCL_AS_C10R_NRH1 = "G1331";
        public const string G1332_DCT_AS_C10R_NRH1 = "G1332";
        public const string G1333_DCL_AO_C10R_NRH1 = "G1333";
        public const string G1334_DCT_AO_C10R_NRH1 = "G1334";
        public const string G1335_DCL_AS_C10R_NLH1 = "G1335";
        public const string G1336_DCT_AS_C10R_NLH1 = "G1336";
        public const string G1337_DCL_AO_C10R_NLH1 = "G1337";
        public const string G1338_DCT_AO_C10R_NLH1 = "G1338";
        public const string G1339_DCL_AS_C10R_NLL1 = "G1339";
        public const string G1340_DCT_AS_C10R_NLL1 = "G1340";
        public const string G1341_DCL_AO_C10R_NLL1 = "G1341";
        public const string G1342_DCT_AO_C10R_NLL1 = "G1342";
        public const string G1343_OFF_L_C11 = "G1343";
        public const string G1344_OFF_T_C11 = "G1344";
        public const string G1345_L_MAX_C11 = "G1345";
        public const string G1346_SBOS_C11 = "G1346";
        public const string G1347_L_C11 = "G1347";
        public const string G1348_R_C11 = "G1348";
        
        [LinearNestingParameter]
        public const string G1349_R_MORSA_SEGA_S = "G1349";
        [LinearNestingParameter]
        public const string G1350_L_MORSA_SEGA_S = "G1350";

        public const string G1351_PERC_RID_VEL_CUT = "G1351";
        public const string G1352_L_MAX_C10R = "G1352";
        public const string G1353_TK_MAX_C10R = "G1353";
        public const string G1354_OFF_X_MKJET2 = "G1354";
        public const string G1355_OFF_L_C12 = "G1355";
        public const string G1356_OFF_T_C12 = "G1356";
        public const string G1357_L1_C12 = "G1357";
        public const string G1358_L2_C12 = "G1358";
        public const string G1360_H2_C12 = "G1360";
        public const string G1361_H3_C12 = "G1361";
        public const string G1362_OFF_L_B14 = "G1362";
        public const string G1363_OFF_T_B14 = "G1363";
        public const string G1364_L_B14 = "G1364";
        public const string G1365_H1_B14 = "G1365";
        public const string G1367_R_B14 = "G1367";
        public const string G1368_L_MAX_B14 = "G1368";
        public const string G1369_H1_C12 = "G1369";

        public const string AXX_MIN = "PAXX_MIN";
        public const string AXX_OB = "PAXX_OB";

        #endregion < Definizione parametri cnc Mitrol >


        #region < Definizione parametri ad uso esclusivo BE/FE (non vengono inviati al cnc) >

        // Larghezza impianto (Gemini, usato per disegnare overlay pressore)
        public const string PLR_0001 = "PLR0001";

        #endregion < Definizione parametri ad uso esclusivo BE/FE (non vengono inviati al cnc) >
    }

}

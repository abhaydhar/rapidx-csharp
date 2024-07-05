
      ******************************************************************
      *                                                                *
      *   SHORT TABLE OF VALID VALUES
      *                                                                *
      *   FIELD MNEMONIC: R-CLM-EXC-CD                                 *
      *           NUMBER: 1737                                         *
      *        GENERATED: 2011-10-20 18:30:25                          *
      *     REQUESTED BY: ACS0127  2011-10-20-18.21.00.546000          *
      *                                                                *
      ******************************************************************
       01  WV-R1737-VALID-VALUES.
         05  WV-R1737-CODE-VALUES.
DF1049     10  WV-R1737-C-MISSING-GROUP-ID                              PML
DF1049                     VALUE IS  '3001'                             PML
DF1049                                 PIC  X(00004).                   PML
           10  WV-R1737-C-MISS-MBR-NUM                                  PMLMISS
                           VALUE IS  '3002'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-REL-CD                                   PMLMISS
                           VALUE IS  '3004'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-OTHR-INSR-CD                             PMLMISS
                           VALUE IS  '3006'
                                       PIC  X(00004).
           10  WV-R1737-C-FUTURE-DOS
                           VALUE IS  '3010'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-DOS
                           VALUE IS  '3011'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-SMKR-IND                                 PMLMISS
                           VALUE IS  '3014'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
DF1171     10  WV-R1737-C-UNACCEPTED-TXN-3016                           PML
DF1171                     VALUE IS  '3016'                             PML
DF1171                                 PIC  X(00004).                   PML
           10  WV-R1737-C-MISS-PROC-NUM                                 PML1.11
                           VALUE IS  '3017'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISMATCH-TRAN-CNT                             COR3013
                           VALUE IS  '3018'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-TRAN-CNT-GRTR-ONE                             COR3013
                           VALUE IS  '3019'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INVAL-TRAN-CNT-D0                             COR3013
                           VALUE IS  '3020'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-CLAIM-SEG-D0                             COR3013
                           VALUE IS  '3021'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-CLIN-SEG-D0                              PMLMISS
                           VALUE IS  '3022'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-UNIT-MEAS-CD                             PMLMISS
                           VALUE IS  '3026'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-INV-PRESCR-DT                                 CR14158
                           VALUE IS  '3028'                             CR14158
                                       PIC  X(00004).                   CR14158
           10  WV-R1737-C-MISS-RFL-AUTH-NUM                             PMLMISS
                           VALUE IS  '3029'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-ALPHA-RFL-AUTH-NUM                            PMLV1.7
                           VALUE IS  '3030'                             PMLV1.7
                                       PIC  X(00004).                   PMLV1.7
           10  WV-R1737-C-MISS-MGAP-ID                                  PMLMISS
                           VALUE IS  '3031'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-MCAID-IND                                PMLMISS
                           VALUE IS  '3033'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-INV-PRV-ASGN-IND                              PMLMISS
                           VALUE IS  '3035'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-CMPD-MOD-CD                              PMLMISS
                           VALUE IS  '3041'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PRSC-FST-NAM                             PMLMISS
                           VALUE IS  '3042'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PRSC-STR-AD                              PMLMISS
                           VALUE IS  '3043'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PRSC-CITY-NAM                            PMLMISS
                           VALUE IS  '3044'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PRSC-ST-CD                               PMLMISS
                           VALUE IS  '3045'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PRSC-ZIP-CD                              PMLMISS
                           VALUE IS  '3046'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-LEVEL-SVC-CD                             PMLMISS
                           VALUE IS  '3048'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PCP-LAST-NAM                             PMLMISS
                           VALUE IS  '3058'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISMATCH-CMPD-NUM                             COR3013
                           VALUE IS  '3059'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-ADDC-SEG-D0                              PMLMISS
                           VALUE IS  '3060'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISSING-SVC-LOC-CD                            COR3013
                           VALUE IS  '3062'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-PAT-LOC-CD-00                             PMLMISS
                           VALUE IS  '3063'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PAT-LOC-CD                               PMLMISS
                           VALUE IS  '3064'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-COB-SEG                                  PMLV1.8
                           VALUE IS  '3078'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PAT-SEG-D0
                           VALUE IS  '3079'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PROV-SEG-D0
                           VALUE IS  '3080'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PRSC-SEG-D0
                           VALUE IS  '3081'
                                       PIC  X(00004).
           10  WV-R1737-C-MISSING-WKR-SEG                               PMLV1.8
                           VALUE IS  '3082'
                                       PIC  X(00004).
           10  WV-R1737-C-CMPD-NOT-SUPPORTED                            COR3013
                           VALUE IS  '3083'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-DUR-PPS-CNT                               COR3013
                           VALUE IS  '3093'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-COB-PAID-CNT                              COR3013
                           VALUE IS  '3098'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-INTD-QTY-AMT                              PMLV1.8
                           VALUE IS  '3100'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-INV-INTD-DAY-AMT                              PMLV1.8
                           VALUE IS  '3101'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-CMPD-SEG-D0
                           VALUE IS  '3111'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-CMPD-SEG                                  PMLV1.8
                           VALUE IS  '3112'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-PRIM-CMPD-ING
                           VALUE IS  '3113'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-RX-DY-OVD-CD-1                            COR3013
                           VALUE IS  '3121'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-RX-DY-OVD-CD-2                            COR3013
                           VALUE IS  '3122'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-RX-DY-OVD-CD-3                            COR3013
                           VALUE IS  '3123'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-DAW-DIF-AMT-SUB                           MDMLV1.12
                           VALUE IS '3130'                              MDMLV1.1
                                       PIC  X(00004).                   MDMLV1.1
           10  WV-R1737-C-INV-CMPD-ING-CNT
                           VALUE IS  '3131'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-DUR-PPS-CNT-D0
                           VALUE IS  '3138'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-UNIT-DOSE-CD                             PMLV1.8
                           VALUE IS  '3153'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-CMPD-PRD-ID-CD                           PMLV1.8
                           VALUE IS  '3154'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-RT-ADMIN-DAT                             PMLV1.8
                           VALUE IS  '3155'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-CMPD-TY-CD                               PMLMISS
                           VALUE IS  '3156'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-MCAID-ID                                 PMLMISS
                           VALUE IS  '3167'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-MCAID-AGCY                               PMLMISS
                           VALUE IS  '3168'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-INV-COB-PAT-CD                                COR3009A
                           VALUE IS  '3169'                             COR3009A
                                       PIC  X(00004).                   COR3009A
           10  WV-R1737-C-MISS-COB-PAT-AMT                              PMLV1.6
                           VALUE IS  '3170'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-COB-PAT-CNT                              PMLV1.6
                           VALUE IS  '3171'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-OTHPYR-CRDID                             PMLV1.6
                           VALUE IS  '3172'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-DLY-RSN-CD                               PMLV1.6
                           VALUE IS  '3173'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-RX-OVD-CNT-ERR                                PMLSIT
                           VALUE IS  '3175'                             PMLSIT
                                       PIC  X(00004).                   PMLSIT
           10  WV-R1737-C-INV-REVERSAL                                  PMLV1.6
                           VALUE IS  '3182'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-INV-COB-PD-REPS                               PMLV1.6
                           VALUE IS  '3194'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-BLLENT-TY-CD                             PMLV1.6
                           VALUE IS  '3204'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-PAY-TO-QL-CD                             PMLV1.8
                           VALUE IS  '3205'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PAY-TO-ID                                PMLV1.8
                           VALUE IS  '3206'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PAY-TO-NAM                               PMLV1.8
                           VALUE IS  '3207'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PAY-TO-STR                               PMLV1.8
                           VALUE IS  '3208'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PAY-TO-CITY                              PMLV1.8
                           VALUE IS  '3209'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PAY-TO-ST-CD                             PMLV1.8
                           VALUE IS  '3210'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PAY-TO-ZIP                               PMLV1.8
                           VALUE IS  '3211'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-PH-SVC-TY-CD                             PMLV1.8
                           VALUE IS  '3213'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-GNREQ-PROD-ID                            PMLV1.8
                           VALUE IS  '3214'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-GNREQ-TY-CD                              PMLV1.8
                           VALUE IS  '3221'                             PMLV1.8
                                       PIC  X(00004).                   PMLV1.8
           10  WV-R1737-C-MISS-CLIN-MSRMT-DT                            PMLV1.9
                           VALUE IS  '3227'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
DF1491     10  WV-R1737-C-INV-DLY-RSN-CD                                PML
DF1491                     VALUE IS  '3234'                             PML
DF1491                                 PIC  X(00004).                   PML
           10  WV-R1737-C-UNACCEPTED-TXN-3242                           COR3013
                           VALUE IS  '3242'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-UNACCEPTED-TXN-3243                           COR3013
                           VALUE IS  '3243'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-PRSC-PHON                                PMLV1.9
                           VALUE IS  '3248'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-LVL-EFRT-CD                              MMLV1.13
                           VALUE IS  '3251'                             MMLV1.13
                                       PIC  X(00004).                   MMLV1.13
           10  WV-R1737-C-INV-LVL-EFRT-CD                               COR6916
                           VALUE IS  '3252'                             COR6916
                                       PIC  X(00004).                   COR6916
MDM-I2     10  WV-R1737-C-INV-PPS-FEE
MDM-I2                     VALUE IS  '3253'
MDM-I2                                 PIC  X(00004).
           10  WV-R1737-C-MISS-INS-FST-NAM                              PMLV1.9
                           VALUE IS  '3255'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-INS-LAST-NAM                             PMLV1.9
                           VALUE IS  '3256'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-EMPLR-NAM                                PMLMISS
                           VALUE IS  '3258'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-EMPLR-STR-AD                             PMLMISS
                           VALUE IS  '3259'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-EMPLR-CITY-NAM                           PMLMISS
                           VALUE IS  '3260'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-EMPLR-ST-CD                              PMLMISS
                           VALUE IS  '3261'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-EMPLR-ZIP-CD                             PMLMISS
                           VALUE IS  '3263'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-EMPLR-PHON-NUM                           PMLMISS
                           VALUE IS  '3264'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-EMPLR-CNTCT-NM                           PMLMISS
                           VALUE IS  '3265'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-B-STR-AD                                 PMLMISS
                           VALUE IS  '3266'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-B-CITY-NAM                               PMLMISS
                           VALUE IS  '3267'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-B-ST-CD                                  PMLMISS
                           VALUE IS  '3268'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PAT-ZIP-CD                               PMLV1.9
                           VALUE IS  '3269'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-PAT-PHON-NUM                             PMLV1.9
                           VALUE IS  '3270'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-CARR-ID                                  PMLMISS
                           VALUE IS  '3271'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-CLM-ALT-ID                               PMLV1.9
                           VALUE IS  '3272'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-EMPLR-ID                                 PMLMISS
                           VALUE IS  '3273'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-INV-UC-CHRGE-AMT                              PML0104
                           VALUE IS  '3276'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PRSC-LST-NAM                             PML0104
                           VALUE IS  '3277'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-INV-GROSS-AMT                                 PML0104
                           VALUE IS  '3279'                             PML0104
                                       PIC  X(00004).
           10  WV-R1737-C-OTHR-PYR-NOT-NUM
                           VALUE IS  '3280'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-HDR-INJURY-DT                            PML0104
                           VALUE IS  '3284'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-INV-HDR-INJURY-DT                             PMLV1.9
                           VALUE IS  '3286'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-CLM-REF-ID                               PML0104
                           VALUE IS  '3287'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-ORIG-PROD-ID                             PMLV1.9
                           VALUE IS  '3288'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-ORIG-QTY-AMT                             PMLV1.9
                           VALUE IS  '3289'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-CNT-CMPD-NUM                             PMLV1.9
                           VALUE IS  '3290'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-INV-CMPD-DRUG-COST
                           VALUE IS  '3291'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-CMPD-DSG-CD                              PMLV1.9
                           VALUE IS  '3292'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
           10  WV-R1737-C-MISS-CMPD-DSP-IND                             PMLV1.6
                           VALUE IS  '3293'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-ORIG-PROD-CD                             PMLV1.6
                           VALUE IS  '3295'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-MISS-SCHED-RX-NUM                             PML0104
                           VALUE IS  '3296'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-RX-SVC-REF-CD                            PMLV1.10
                           VALUE IS  '3297'                             PMLV1.10
                                       PIC  X(00004).                   PMLV1.10
           10  WV-R1737-C-MISS-ASOC-RX-NUM                              PMLV1.9
                           VALUE IS  '3299'                             PMLV1.9
                                       PIC  X(00004).                   PMLV1.9
PML004     10  WV-R1737-C-INV-PRSC-QTY-AMT
PML004                     VALUE IS  '3305'
PML004                                 PIC  X(00004).
           10  WV-R1737-C-PRSC-QTY-NOT-NUM
                           VALUE IS  '3304'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-AUTH-CERT-ID                             PML0104
                           VALUE IS  '3306'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-PA-INTR-CD                               PML0104
                           VALUE IS  '3312'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-M-INTMDRY-AUTH-ID                             PMLMISS
                           VALUE IS  '3313'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-P-MISS-PHRM-ALT-ID-CD                           PMLV1.10
                           VALUE IS  '3314'                             PMLV1.10
                                       PIC  X(00004).                   PMLV1.10
           10  WV-R1737-C-MISS-INCNTV-AMT                               PML0104
                           VALUE IS  '3316'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-SUBM-PLAN-ID                             PMLMISS
                           VALUE IS  '3325'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PCT-RATE-AMT                             PMLMISS
                           VALUE IS  '3332'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-INV-TAX-RATE                                  PMLMISS
                           VALUE IS  '3333'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-PCT-BASIS-CD                             PMLMISS
                           VALUE IS  '3337'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-INV-TAX-CD                                    PMLMISS
                           VALUE IS  '3338'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-REQ-PAT-SEG
                           VALUE IS  '3341'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-CLNT-SEX-CD                               PMLMISS
                           VALUE IS  '3352'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
DF1171     10  WV-R1737-C-INV-GROUP-ON-SYSDT                            PML
DF1171                     VALUE IS  '3362'                             PML
DF1171                                 PIC  X(00004).                   PML
DF1171     10  WV-R1737-C-INV-SWITCH-VEND                               PML
DF1171                     VALUE IS  '3363'                             PML
DF1171                                 PIC  X(00004).                   PML
MDMFIX     10  WV-R1737-C-MISS-OTHER-PAYER                              MDM
MDMFIX                     VALUE IS  '3398'                             MDM
MDMFIX                                 PIC  X(00004).                   MDM
MDMFIX     10  WV-R1737-C-OTHER-PAYER-EXCEED                            COR3013
MDMFIX                     VALUE IS  '3399'                             COR3013
MDMFIX                                 PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-SEQ-COB-ID-CD                             COR3013
                           VALUE IS  '3403'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-COB-PAT-NUM                               PMLV1.7
                           VALUE IS  '3405'                             PMLV1.7
                                       PIC  X(00004).                   PMLV1.7
           10  WV-R1737-C-INV-SEQ-PAT-CNT
                           VALUE IS  '3406'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-COB-PAT-FLDS
                           VALUE IS  '3407'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-COB-PAT-AMT-CD
                           VALUE IS  '3409'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-COB-PAT-AMT                               COR3013
                           VALUE IS  '3410'                             COR3013
                                       PIC  X(00004).                   COR3013
PML001     10  WV-R1737-C-INV-SEQ-BENE-STG                              COR3013
PML001                     VALUE IS  '3421'                             COR3013
PML001                                 PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-SEQ-BENE-STG-CD                           DF3216
                           VALUE IS  '3422'                             DF3216
                                       PIC  X(00004).                   DF3216
PML001     10  WV-R1737-C-MISS-BENE-STG-GRP                             COR3013
PMLSIT                     VALUE IS  '3423'                             COR3013
PML001                                 PIC  X(00004).                   COR3013
PMLSIT     10  WV-R1737-C-MISS-BENE-STG-CNT
PMLSIT                     VALUE IS  '3424'                             
PMLSIT                                 PIC  X(00004).                   
PMLSIT     10  WV-R1737-C-MISS-BENE-STG-CD
PMLSIT                     VALUE IS  '3425'                             
PMLSIT                                 PIC  X(00004).                   
PMLSIT     10  WV-R1737-C-MISS-BENE-STG-AMT
PMLSIT                     VALUE IS  '3426'                             
PMLSIT                                 PIC  X(00004).                   
PMLSIT     10  WV-R1737-C-INV-BENE-STG-CNT
PMLSIT                     VALUE IS  '3427'                             
PMLSIT                                 PIC  X(00004).                   
PMLSIT     10  WV-R1737-C-INV2-BENE-STG-AMT
PMLSIT                     VALUE IS  '3428'                             
PMLSIT                                 PIC  X(00004).                   
PMLSIT     10  WV-R1737-C-INV3-BENE-STG-AMT
PMLSIT                     VALUE IS  '3429'                             
PMLSIT                                 PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-SEQ-COB-PAT-CD                            COR3013
                           VALUE IS  '3430'                             COR3013
                                       PIC  X(00004).                   COR3013
MDM-I2     10  WV-R1737-C-UNEX-PRSC-QTY                                 COR3013
MDM-I2                     VALUE IS  '3519'                             COR3013
MDM-I2                                 PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-RX-ORGN-CD                               COR3013
                           VALUE IS  '3520'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-PHARM-ALT-ID                             COR3013
                           VALUE IS  '3523'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-NEG-PAT-RESP-AMT                              COR3009A
                           VALUE IS  '3534'                             COR3009A
                                       PIC  X(00004).                   COR3009A
           10  WV-R1737-C-MISS-UC-CHARGE-AMT                            COR3013
                           VALUE IS  '3779'                             COR3013
                                       PIC  X(00004).                   COR3013
MDM-I2     10  WV-R1737-C-INV-SVC-REF-CD-SRV                            COR3015
MDM-I2                     VALUE IS  '3900'                             COR3015
MDM-I2                                 PIC  X(00004).                   COR3015
MDM-I2     10  WV-R1737-C-INV-SVC-REF-CD-RX                             COR3015
MDM-I2                     VALUE IS  '3907'                             COR3015
MDM-I2                                 PIC  X(00004).                   COR3015
MDM-I2     10  WV-R1737-C-INV-PMOD-CNT-REV                              COR3015
MDM-I2                     VALUE IS  '3910'                             COR3015
MDM-I2                                 PIC  X(00004).                   COR3015
MDM-I2     10  WV-R1737-C-INV-PMOD-CNT                                  COR3015
MDM-I2                     VALUE IS  '3912'                             COR3015
MDM-I2                                 PIC  X(00004).                   COR3015
           10  WV-R1737-C-INV-SVC-REF-NUM                               COR3015
                           VALUE IS  '3913'                             COR3015
                                       PIC  X(00004).                   COR3015
           10  WV-R1737-C-INV-INCNTV-AMT                                COR3015
                           VALUE IS  '3917'                             COR3015
                                       PIC  X(00004).                   COR3015
           10  WV-R1737-C-INV-SUBM-PPS-FEE                              COR3015
                           VALUE IS  '3918'                             COR3015
                                       PIC  X(00004).                   COR3015
           10  WV-R1737-C-INVALID-BIN
                           VALUE IS  '4001'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-VERSION
                           VALUE IS  '4002'
                                       PIC  X(00004).
           10  WV-R1737-C-UNACCEPTED-VERSION
                           VALUE IS  '4003'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-TRAN-CD
                           VALUE IS  '4005'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-PROC
                           VALUE IS  '4007'
                                       PIC  X(00004).
           10  WV-R1737-C-INV-MISS-CARDHLDR
                           VALUE IS  '4010'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-ELIG-OVRRD                               PML0104
                           VALUE IS  '4022'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-INV-DOS
                           VALUE IS  '4023'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-RX-NUM                                   PML0104
                           VALUE IS  '4025'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISSING-REFILL-NUM                            COR8463
                           VALUE IS  '4028'                             MDM0731
                                       PIC  X(00004).                   MDM0731
           10  WV-R1737-C-MISS-PRGNCY-IND                               PML0104
                           VALUE IS  '4031'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-REF-ID-CD                                PML0104
                           VALUE IS  '4032'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-PROD-SVC-ID                              COR3013
                           VALUE IS  '4034'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-DAW-CD                                   PMLV1.10
                           VALUE IS  '4037'                             PMLV1.10
                                       PIC  X(00004).                   PMLV1.10
           10  WV-R1737-C-INV-SUBM-INGR-COST
                           VALUE IS  '4038'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PRSC-ID                                  PML0104
                           VALUE IS  '4040'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-DRUG-PRESCR-DT                           PML0104
                           VALUE IS  '4043'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-INVALID-COB-CNT
                           VALUE IS  '4074'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-REJ-CNT                                  DF3071
                           VALUE IS  '4079'                             DF3071
                                       PIC  X(00004).                   DF3071
           10  WV-R1737-C-MISS-PROV-ID
                           VALUE IS  '4080'
                                       PIC  X(00004).
           10  WV-R1737-C-GROUP-NOT-FOUND
                           VALUE IS  '4082'
                                       PIC  X(00004).
           10  WV-R1737-C-NONM-GROUP-ID-FDOS
                           VALUE IS  '4083'
                                       PIC  X(00004).
           10  WV-R1737-C-CRDHLDR-NOT-FOUND
                           VALUE IS  '4086'
                                       PIC  X(00004).
           10  WV-R1737-C-PAT-NOT-COV
                           VALUE IS  '4097'
                                       PIC  X(00004).
           10  WV-R1737-C-PAT-NOT-COV-COE
                           VALUE IS  '4099'
                                       PIC  X(00004).
DF1428     10  WV-R1737-C-INV-LEVEL-SVC-CD                              PML
DF1428                     VALUE IS  '4100'                             PML
DF1428                                 PIC  X(00004).                   PML
DF2820     10  WV-R1737-C-MISS-BASIS-COST-CD
DF2820                     VALUE IS  '4109'
DF2820                                 PIC  X(00004).
           10  WV-R1737-C-INVALID-REFILL-NUM                            COR8463
                           VALUE IS  '4128'                             COR8463
                                       PIC  X(00004).                   COR8463
           10  WV-R1737-C-CLIN-CNT-OUT-OF-SQ
                           VALUE IS  '4110'
                                       PIC  X(00004).
           10  WV-R1737-C-NOT-COVERD-ON-PLAN
                           VALUE IS  '4114'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-DIAG-ICD9-CD                             PML0104
                           VALUE IS  '4127'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-EXACT-DUPLICATE
                           VALUE IS  '4185'
                                       PIC  X(00004).
           10  WV-R1737-C-SYSTEM-ERROR
                           VALUE IS  '4188'
                                       PIC  X(00004).
           10  WV-R1737-C-DUPLICATE-REVERSAL
                           VALUE IS  '4189'
                                       PIC  X(00004).
DF1171     10  WV-R1737-C-MISSING-SOFT-VEND                             PML
                           VALUE IS  '4211'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-SEG-ID
                           VALUE IS  '4212'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-TRAN-CNT
                           VALUE IS  '4213'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PPS-FEE
                           VALUE IS  '4214'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PROV-ID-CD                               PML0104
PML005                     VALUE IS  '4218'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-GROSS-AMT                                PML0104
                           VALUE IS  '4227'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-PAT-DAW-DIF                              DF2916
                           VALUE IS  '4233'                             DF2916
                                       PIC  X(00004).                   DF2916
           10  WV-R1737-C-INVALID-CMPD-CNT
                           VALUE IS  '4236'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-SUBM-ING-QTY                             COR3013
                           VALUE IS  '4237'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-PRSC-QTY                                 COR3013
                           VALUE IS  '4243'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-PRSC-ID-CD                               PML0104
                           VALUE IS  '4247'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-MISS-PRD-SVC-ID-CD                            COR3013
                           VALUE IS  '4248'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-MISS-SUB-QTY-AMT                              COR3013
                           VALUE IS  '4256'                             COR3013
                                       PIC  X(00004).                   COR3013
           10  WV-R1737-C-INV-PAYERID-DT                                COR3009A
                           VALUE IS  '4258'                             COR3009A
                                       PIC  X(00004).                   COR3009A
           10  WV-R1737-C-MISS-COB-PD-CT
                           VALUE IS  '4267'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-COB-PD-QL
                           VALUE IS  '4269'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-OTHR-AMT-CD                              PMLMISS
                           VALUE IS  '4285'                             PMLMISS
                                       PIC  X(00004).                   PMLMISS
           10  WV-R1737-C-MISS-DUR-COAGT-CD                             PML0104
                           VALUE IS  '4288'                             PML0104
                                       PIC  X(00004).                   PML0104
           10  WV-R1737-C-ONLY-1-TRAN-ALLW
                           VALUE IS  '4296'
                                       PIC  X(00004).
           10  WV-R1737-C-MISSING-CLM-SEG
                           VALUE IS  '4298'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-CLM-SEG
                           VALUE IS  '4299'
                                       PIC  X(00004).
           10  WV-R1737-C-ZERO-UC-CHRGE-AMT                             PMLV1.6
                           VALUE IS  '4300'                             PMLV1.6
                                       PIC  X(00004).                   PMLV1.6
           10  WV-R1737-C-INVALID-CLIN-SEG
                           VALUE IS  '4301'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-COB-SEG
                           VALUE IS  '4303'
                                       PIC  X(00004).
           10  WV-R1737-C-MISSING-CMPD-SEG
                           VALUE IS  '4304'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-CMPD-SEG
                           VALUE IS  '4305'
                                       PIC  X(00004).
           10  WV-R1737-C-MISSING-INSR-SEG
                           VALUE IS  '4309'
                                       PIC  X(00004).
           10  WV-R1737-C-MISSING-PRIC-SEG
                           VALUE IS  '4317'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-PRIC-SEG
                           VALUE IS  '4318'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-PA-SEG
                           VALUE IS  '4320'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-HDR-SEG
                           VALUE IS  '4321'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-DUR-CNT
                           VALUE IS  '4327'
                                       PIC  X(00004).
           10  WV-R1737-C-MISMATCH-CMPD-CNT
                           VALUE IS  '4328'
                                       PIC  X(00004).
           10  WV-R1737-C-MISMATCH-COB-CNT
                           VALUE IS  '4329'
                                       PIC  X(00004).
           10  WV-R1737-C-MISMATCH-DIAG-CNT
                           VALUE IS  '4331'
                                       PIC  X(00004).
           10  WV-R1737-C-DUR-CNT-OUT-OF-SEQ
                           VALUE IS  '4332'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-CMPD-PROD-CD                             PMLV1.10
                           VALUE IS  '4337'                             PMLV1.10
                                       PIC  X(00004).                   PMLV1.10
           10  WV-R1737-C-MISMATCH-OAMT-CNT
                           VALUE IS  '4348'
                                       PIC  X(00004).
           10  WV-R1737-C-MISMATCH-COB-RJ-CT
                           VALUE IS  '4349'
                                       PIC  X(00004).
           10  WV-R1737-C-MISMATCH-PMOD-CNT
                           VALUE IS  '4350'
                                       PIC  X(00004).
           10  WV-R1737-C-REPEAT-SEGMENT
                           VALUE IS  '4354'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-DIAG-CNT
                           VALUE IS  '4359'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-CLIN-CNT
                           VALUE IS  '4361'
                                       PIC  X(00004).
           10  WV-R1737-C-TBLS-SYSTEM-ERROR
                           VALUE IS  '4364'
                                       PIC  X(00004).
           10  WV-R1737-C-MEMBER-IS-LOCKED
                           VALUE IS  '4379'
                                       PIC  X(00004).
           10  WV-R1737-C-SYS-NOT-AVAILABLE
                           VALUE IS  '4410'
                                       PIC  X(00004).
           10  WV-R1737-C-NO-EXCEPTIONS-LOAD
                           VALUE IS  '4415'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-BATCH-TYPE
                           VALUE IS  '4437'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-LI-ITM-CNT
                           VALUE IS  '4438'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-PROD-SVC-ID-CM                           COR3013
                           VALUE IS  '4450'                             COR3013
                                       PIC  X(00004).                   COR3013
DF1171     10  WV-R1737-C-UNACCEPTED-TXN-4488                           PML
                           VALUE IS  '4488'                             PMLV1.11
                                       PIC  X(00004).
           10  WV-R1737-C-EF-NOT-SAME-DAY
                           VALUE IS  '4534'
                                       PIC  X(00004).
           10  WV-R1737-C-EF-INGREDS-NOT-COV
                           VALUE IS  '4538'
                                       PIC  X(00004).
           10  WV-R1737-C-COPAYMENT
                           VALUE IS  '4551'
                                       PIC  X(00004).
           10  WV-R1737-C-COPAY-WRAP-BRAND
                           VALUE IS  '4613'
                                       PIC  X(00004).
           10  WV-R1737-C-IGNORE-MEDICARE
                           VALUE IS  '4617'
                                       PIC  X(00004).
           10  WV-R1737-C-COPAY-WRAP-GENERIC
                           VALUE IS  '4621'
                                       PIC  X(00004).
           10  WV-R1737-C-NOT-ELIG-PHARMACY
                           VALUE IS  '4676'
                                       PIC  X(00004).
           10  WV-R1737-C-NO-COPAY-ANTIPSYCH
                           VALUE IS  '4679'
                                       PIC  X(00004).
           10  WV-R1737-C-INVALID-AUTH-NUM
                           VALUE IS  '4680'
                                       PIC  X(00004).
           10  WV-R1737-C-NO-PREV-CLAIM
                           VALUE IS  '4685'
                                       PIC  X(00004).
           10  WV-R1737-C-FRST-HSNO-NOT-COV
                           VALUE IS  '4686'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-CLNT-SEX-CD                              PML0104
                           VALUE IS  '4699'                             PML0104
                                       PIC  X(00004).                   PML0104
S600C1     10  WV-R1737-C-RHN-SUBMITTED
S600C1                     VALUE IS  '4713'
S600C1                                 PIC  X(00004).
           10  WV-R1737-C-NO-PRELATTB-HSN
                           VALUE IS  '4720'
                                       PIC  X(00004).
PMLSIT     10  WV-R1737-C-INVALID-HSN-AUTH-N
PMLSIT                     VALUE IS  '4721'
PMLSIT                                 PIC  X(00004).
           10  WV-R1737-C-NO-RELAT-PRELATTB
                           VALUE IS  '4729'
                                       PIC  X(00004).
S600C1     10  WV-R1737-C-INACTIVE-ALT-ID
S600C1                     VALUE IS  '4734'
S600C1                                 PIC  X(00004).
S600C1     10  WV-R1737-C-FULL-SUBST-SYSID
S600C1                     VALUE IS  '4735'
S600C1                                 PIC  X(00004).
S600C1     10  WV-R1737-C-PART-SUBST-SYSID
S600C1                     VALUE IS  '4736'
S600C1                                 PIC  X(00004).
           10  WV-R1737-C-DISASTER-RECOV-CD
                           VALUE IS  '4804'
                                       PIC  X(00004).
           10  WV-R1737-C-MORE-THAN-MAX-EXC
                           VALUE IS  '4899'
                                       PIC  X(00004).
           10  WV-R1737-C-MISS-FACI-SEG                                 PMLV1.10
                           VALUE IS  '9997'                             PMLV1.10
                                       PIC  X(00004).                   PMLV1.10
           10  WV-R1737-C-MISS-CMPD-MOD-NUM                             PMLV1.10
                           VALUE IS  '9999'                             PMLV1.10
                                       PIC  X(00004).                   PMLV1.10
         05  WV-R1737-SEARCH-TABLE  REDEFINES
               WV-R1737-CODE-VALUES.
           10  WV-R1737-TABLE-ENTRY
      *                    OCCURS 00274 TIMES                           COR8463
                           OCCURS 00275 TIMES                           CR14158
                           ASCENDING KEY IS
                 WV-R1737-R-CLM-EXC-CD
                           INDEXED BY WXV-R1737-TABLE-ENTRY.
             15  WV-R1737-R-CLM-EXC-CD
                                       PIC  X(00004).
         05  WV-R1737-MAX-ENTRIES      PIC S9(04)
                           USAGE IS COMP-3
      *                    VALUE IS +00274.                             COR8463
                           VALUE IS +00275.                             CR14158

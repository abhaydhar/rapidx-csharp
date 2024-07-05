
      *****************************************************************
      *                                                               *
      *    MEMBER:  W1C66791                                          *
      *      NAME:  C-NCPDP-CNSLDTD                                   *
      * SUBSYSTEM:  C Claims                                          *
      *   VERSION:  1                                                 *
      * GENERATED:  2011-09-16 10:30:07 REQUESTED BY ACS0127          *
      *                                                               *
      * NCPDP Consolidated Layout                                     *
      *                                                               *
      *****************************************************************
      *                                                               *
       01  W1C66791-C-NCPDP-CNSLDTD.                                    S6679
           05  W1C66791-C-NCP-CICS-TASK-ID                              F8849
                                       PIC  X(00004).
           05  W1C66791-C-NCP-NDC-MSG-CD                                F8865
                                       PIC  X(00003).
           05  W1C66791-C-NCP-NET-REF-NUM                               F8866
                                       PIC  X(00006).
           05  W1C66791-C-NCP-BIN-NUM  PIC S9(00006).                   F8846
           05  W1C66791-C-NCP-VERSION-NUM                               F8897
                                       PIC  X(00002).
           05  W1C66791-C-NCP-POS-TRAN-CD                               F8869
                                       PIC  X(00002).
           05  W1C66791-C-NCP-PRCS-NUM                                  F8870
                                       PIC  X(00010).
           05  W1C66791-C-NCP-TRAN-CNT-NUM                              F6464
                                       PIC  X(00001).
           05  W1C66791-C-NCP-PROV-ID-CD                                F6624
                                       PIC  X(00002).
           05  W1C66791-C-NCP-PROV-ID  PIC  X(00015).                   F8887
           05  W1C66791-C-NCP-GROUP-ID                                  F8862
                                       PIC  X(00015).
           05  W1C66791-C-NCP-HOME-PLN-TX                               F7238
                                       PIC  X(00003).
           05  W1C66791-C-NCP-SUBM-PLAN-ID                              F6179
                                       PIC  X(00008).
           05  W1C66791-C-NCP-CARDHOLDR-ID                              F8847
                                       PIC  X(00020).
           05  W1C66791-C-NCP-CLNT-MBR-NUM                              F8852
                                       PIC  X(00003).
           05  W1C66791-C-NCP-INS-FST-NAM                               F0208
                                       PIC  X(00012).
           05  W1C66791-C-NCP-INS-LAST-NAM                              F6785
                                       PIC  X(00015).
           05  W1C66791-C-NCP-CLNT-DOB-DT                               F8851
                                       PIC  X(00008).
           05  W1C66791-C-NCP-CLNT-SEX-CD                               F8853
                                       PIC  X(00001).
           05  W1C66791-C-NCP-REL-CD   PIC S9(00001).                   F8890
           05  W1C66791-C-NCP-REL-CD-X REDEFINES                        PMLMISS
               W1C66791-C-NCP-REL-CD   PIC  X(01).                      PMLMISS
           05  W1C66791-C-NCP-OTHPYR-BNUM                               F2575
                                       PIC S9(00006) COMP.
           05  W1C66791-C-NCP-OTHPYR-BNUM-X REDEFINES                   PMLMISS
               W1C66791-C-NCP-OTHPYR-BNUM                               PMLMISS
                                       PIC  X(04).                      PMLMISS
           05  W1C66791-C-NCP-OTHPYR-PNUM                               F2315
                                       PIC  X(00010).
           05  W1C66791-C-NCP-OTHPYR-CRDID                              F9244
                                       PIC  X(00020).
           05  W1C66791-C-NCP-OTHPYR-GRPID                              F1387
                                       PIC  X(00015).
           05  W1C66791-C-NCP-MGAP-ID  PIC  X(00020).                   F5904
           05  W1C66791-C-NCP-MCAID-IND                                 F4469
                                       PIC  X(00002).
           05  W1C66791-C-NCP-PRV-ASGN-IND                              F8781
                                       PIC  X(00001).
           05  W1C66791-C-NCP-CMS-FAC-IND                               F2316
                                       PIC  X(00001).
           05  W1C66791-C-NCP-MCAID-ID                                  F1605
                                       PIC  X(00020).
           05  W1C66791-C-NCP-MCAID-AGCY                                F2317
                                       PIC  X(00015).
           05  W1C66791-C-NCP-SVC-FST-DT                                F8896
                                       PIC S9(00008).
           05  W1C66791-C-NCP-SVC-FST-DT-X REDEFINES                    PMLMISS
               W1C66791-C-NCP-SVC-FST-DT                                PMLMISS
                                       PIC X(08).                       PMLMISS
           05  W1C66791-C-NCP-ELIG-OVRRD                                F8861
                                       PIC S9(00001).
           05  W1C66791-C-NCP-ELIG-OVRRD-X REDEFINES                    PMLMISS
               W1C66791-C-NCP-ELIG-OVRRD                                PMLMISS
                                       PIC  X(01).                      PMLMISS
           05  W1C66791-C-NCP-PAT-ID-CD                                 F4743
                                       PIC  X(00002).
           05  W1C66791-C-NCP-PAT-ID   PIC  X(00020).                   F8242
           05  W1C66791-C-NCP-B-FST-NAM                                 F8844
                                       PIC  X(00012).
           05  W1C66791-C-NCP-B-LAST-NAM                                F8845
                                       PIC  X(00015).
           05  W1C66791-C-NCP-B-STR-AD                                  F9149
                                       PIC  X(00030).
           05  W1C66791-C-NCP-B-CITY   PIC  X(00020).                   F1304
           05  W1C66791-C-NCP-B-ST-CD  PIC  X(00002).                   F2318
           05  W1C66791-C-NCP-PAT-ZIP-CD                                F7106
                                       PIC  X(00020).
           05  W1C66791-C-NCP-PAT-PHON-NUM                              F7194
                                       PIC  X(00010).
           05  W1C66791-C-SET-3403-IND PIC  X(01).                      COR3013
           05  W1C66791-C-SET-3406-IND PIC  X(01).                      COR3013
PML        05  W1C66791-C-SET-3421-IND PIC  X(01).                      COR3013
           05  W1C66791-C-SET-3430-IND PIC  X(01).                      COR3013
           05  W1C66791-C-SET-3422-IND PIC  X(01).                      DF3216
           05  W1C66791-C-NCP-PREG-IND                                  F6804
                                       PIC  X(00001).
           05  W1C66791-C-NCP-SOFT-VEND-ID                              F5357
                                       PIC  X(00010).
           05  W1C66791-C-NCP-SVC-LOC-CD                                F1064
                                       PIC S9(00002) COMP.
           05  W1C66791-C-NCP-SVC-LOC-CD-X REDEFINES                    PMLMISS
               W1C66791-C-NCP-SVC-LOC-CD                                PMLMISS
                                       PIC  X(02).                      PMLMISS
           05  W1C66791-C-NCP-EMPLR-ID                                  F6031
                                       PIC  X(00015).
           05  W1C66791-C-NCP-SMKR-IND                                  F1670
                                       PIC  X(00001).
           05  W1C66791-C-NCP-PAT-EMAIL-TX                              F1392
                                       PIC  X(00060).
           05  W1C66791-C-NCP-PAT-LOC-CD                                F1606
                                       PIC S9(00002) COMP.
           05  W1C66791-C-NCP-PAT-LOC-CD-X REDEFINES                    PMLMISS
               W1C66791-C-NCP-PAT-LOC-CD                                PMLMISS
                                       PIC  X(02).                      PMLMISS
           05  W1C66791-C-NCP-SEG-HDR-NUM                               F2721
                                       PIC 9(00002) COMP.
           05  W1C66791-C-NCP-SEG-HDR-NUM-X REDEFINES                   PMLMISS
               W1C66791-C-NCP-SEG-HDR-NUM                               PMLMISS
                                       PIC  X(02).                      PMLMISS
           05  W1C66791-C-NCP-SEG-PAT-NUM                               F3903
                                       PIC 9(00002) COMP.
           05  W1C66791-C-NCP-SEG-PAT-NUM-X REDEFINES                   PMLMISS
               W1C66791-C-NCP-SEG-PAT-NUM                               PMLMISS
                                       PIC  X(02).                      PMLMISS
           05  W1C66791-C-NCP-SEG-INSR-NUM                              F4466
                                       PIC 9(00002) COMP.
           05  W1C66791-C-NCP-SEG-INSR-NUM-X REDEFINES                  PMLMISS
               W1C66791-C-NCP-SEG-INSR-NUM                              PMLMISS
                                       PIC  X(02).                      PMLMISS
           05  W1C66791-C-NCP-CNT-RX-NUM                                F6551
                                       PIC S9(00001) COMP.
           05  W1C66791-C-NCP-CNT-RX-NUM-X REDEFINES                    PMLMISS
               W1C66791-C-NCP-CNT-RX-NUM                                PMLMISS
                                       PIC  X(02).                      PMLMISS
           05  W1C66791-C-NCPDP-CNSLDTD-RX                              S6680
                           OCCURS 0004 TIMES
                           INDEXED BY W1C66791-C-NCPDP-CNSLDTD-RX-X .
               10  W1C66791-C-NCP-RX-NUM-CD                             F4852
                                       PIC  X(00001).
               10  W1C66791-C-NCP-RX-NUM                                F8893
                                       PIC  X(00012).
               10  W1C66791-C-NCP-NEW-REFLL-CD                          F8867
                                       PIC S9(00002).
               10  W1C66791-C-NCP-NEW-REFLL-CD-X REDEFINES              PMLMISSF8867
                   W1C66791-C-NCP-NEW-REFLL-CD                          PMLMISSF8867
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-SUBM-QTY-NUM                          F8895
                                       PIC S9(00008).
               10  W1C66791-C-NCP-SUBM-QTY-NUM-X REDEFINES              F8895
                   W1C66791-C-NCP-SUBM-QTY-NUM                          F8895
                                       PIC  X(08).
               10  W1C66791-C-NCP-DYS-SPLY-NUM                          F8860
                                       PIC S9(00003).
               10  W1C66791-C-NCP-DYS-SPLY-NUM-X REDEFINES              PMLMISSF8860
                   W1C66791-C-NCP-DYS-SPLY-NUM                          PMLMISSF8860
                                       PIC  X(03).                      PMLMISS
               10  W1C66791-C-NCP-COMPOUND-CD                           F8854
                                       PIC  X(00001).
               10  W1C66791-C-NCP-PROD-ID-CD                            F3221
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PROD-ID                               F8864
                                       PIC  X(00019).
               10  W1C66791-C-NCP-DAW-IND                               F8855
                                       PIC  X(00001).
               10  W1C66791-C-NCP-SUBM-ING-AMT                          F1469
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-SUBM-ING-AMT-X REDEFINES              PMLMISSF1469
                   W1C66791-C-NCP-SUBM-ING-AMT                          PMLMISSF1469
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-PRSC-ID-CD                            F2798
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PRSC-ID                               F8886
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PCP-LOC-CD                            F1104
                                       PIC  X(00003).
               10  W1C66791-C-NCP-PCP-LAST-NAM                          F0655
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PRSC-LOC-CD                           F7707
                                       PIC  X(00003).
               10  W1C66791-C-NCP-PRSC-LST-NAM                          F7859
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PRSC-FST-NAM                          F1441
                                       PIC  X(00010).
               10  W1C66791-C-NCP-PRSC-STR-AD                           F2319
                                       PIC  X(00030).
               10  W1C66791-C-NCP-PRSC-CITY                             F9416
                                       PIC  X(00020).
               10  W1C66791-C-NCP-PRSC-ST-CD                            F0607
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PRSC-ZIP-CD                           F2921
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PRSC-PHON                             F1555
                                       PIC  X(00010).
               10  W1C66791-C-NCP-PRESCR-DT                             F8885
                                       PIC S9(00008).
               10  W1C66791-C-NCP-PRESCR-DT-X REDEFINES                 PMLMISSF8885
                   W1C66791-C-NCP-PRESCR-DT                             PMLMISSF8885
                                       PIC  X(08).                      PMLMISS
               10  W1C66791-C-NCP-RFL-AUTH-NUM                          F8891
                                       PIC  X(00002).
               10  W1C66791-C-NCP-RX-ORIGIN-CD                          F8894
                                       PIC  X(00001).
               10  W1C66791-C-NCP-SUB-OVD-CNT                           F2617
                                       PIC S9(00002) COMP.
               10  W1C66791-C-NCP-SUB-OVD-CNT-X REDEFINES               PMLMISSF2617
                   W1C66791-C-NCP-SUB-OVD-CNT                           PMLMISSF2617
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-SUB-OVD-GRP                           S0239
                           OCCURS 0003 TIMES
                           INDEXED BY W1C66791-C-NCP-SUB-OVD-GRP-X .
                   15  W1C66791-C-NCP-RX-DY-OVD-CD                      F8892
                                       PIC  X(00002).
               10  W1C66791-C-NCP-UNIT-MEAS-CD                          F1602
                                       PIC  X(00002).
               10  W1C66791-C-NCP-UAC-AMT                               F8841
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-UAC-AMT-X REDEFINES                   PMLMISSF8841
                   W1C66791-C-NCP-UAC-AMT                               PMLMISSF8841
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-OTHR-INSR-CD                          F8868
                                       PIC S9(00002) COMP-3.
               10  W1C66791-C-NCP-OTHR-INSR-CD-X REDEFINES              PMLMISSF8868
                   W1C66791-C-NCP-OTHR-INSR-CD                          PMLMISSF8868
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-SALES-TX-AMT                          F4683
                                       PIC S9(00004)V9(00002) COMP-3.
               10  W1C66791-C-NCP-SALES-TX-AMT-X REDEFINES              PMLMISSF4683
                   W1C66791-C-NCP-SALES-TX-AMT                          PMLMISSF4683
                                       PIC  X(04).                      PMLMISS
               10  W1C66791-C-NCP-DISP-FEE-AMT                          F7440
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-DISP-FEE-AMT-X REDEFINES              PMLMISSF7440
                   W1C66791-C-NCP-DISP-FEE-AMT                          PMLMISSF7440
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-AUTH-CERT-ID                          F9394
                                       PIC S9(00002).
               10  W1C66791-C-NCP-AUTH-CERT-ID-X REDEFINES              PMLMISSF9394
                   W1C66791-C-NCP-AUTH-CERT-ID                          PMLMISSF9394
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-AUTH-NUM                              F8843
                                       PIC 9(00011).
               10  W1C66791-C-NCP-AUTH-NUM-X REDEFINES                  PMLMISSF8843
                   W1C66791-C-NCP-AUTH-NUM                              PMLMISSF8843
                                       PIC  X(11).                      PMLMISS
               10  W1C66791-C-NCP-INJURY-DT                             F8863
                                       PIC  X(00008).
               10  W1C66791-C-NCP-EMPLR-NAM                             F0402
                                       PIC  X(00030).
               10  W1C66791-C-NCP-EMPLR-STR-AD                          F2799
                                       PIC  X(00030).
               10  W1C66791-C-NCP-EMPLR-CITY                            F2320
                                       PIC  X(00020).
               10  W1C66791-C-NCP-EMPLR-ST-CD                           F9637
                                       PIC  X(00002).
               10  W1C66791-C-NCP-EMPLR-ZIP-CD                          F3245
                                       PIC  X(00015).
               10  W1C66791-C-NCP-EMPLR-PHON                            F1388
                                       PIC  X(00010).
               10  W1C66791-C-NCP-EMPLR-CNTCT                           F5644
                                       PIC  X(00030).
               10  W1C66791-C-NCP-CARR-ID                               F2335
                                       PIC  X(00010).
               10  W1C66791-C-NCP-CLM-REF-ID                            F8850
                                       PIC  X(00030).
               10  W1C66791-C-NCP-BLLENT-TY-CD                          F2321
                                       PIC S9(00002) COMP.
               10  W1C66791-C-NCP-BLLENT-TY-CD-X REDEFINES              PMLMISSF2321
                   W1C66791-C-NCP-BLLENT-TY-CD                          PMLMISSF2321
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-PAY-TO-QL-CD                          F1095
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PAY-TO-ID                             F1750
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PAY-TO-NAM                            F8119
                                       PIC  X(00020).
               10  W1C66791-C-NCP-PAY-TO-STR                            F2322
                                       PIC  X(00030).
               10  W1C66791-C-NCP-PAY-TO-CITY                           F2332
                                       PIC  X(00020).
               10  W1C66791-C-NCP-PAY-TO-ST-CD                          F3619
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PAY-TO-ZIP                            F0731
                                       PIC  X(00015).
               10  W1C66791-C-NCP-GNREQ-TY-CD                           F0595
                                       PIC  X(00002).
               10  W1C66791-C-NCP-GNREQ-PRD-ID                          F8164
                                       PIC  X(00019).
               10  W1C66791-C-NCP-REF-ID-CD                             F1245
                                       PIC  X(00002).
               10  W1C66791-C-NCP-REF-PHYS-ID                           F8889
                                       PIC  X(00015).
               10  W1C66791-C-NCP-SUBM-QTY-AMT                          F3440
                                       PIC S9(00007)V9(00003) COMP-3.
               10  W1C66791-C-NCP-SUBM-QTY-AMT-X REDEFINES              PMLMISSF3440
                   W1C66791-C-NCP-SUBM-QTY-AMT                          PMLMISSF3440
                                       PIC  X(06).                      PMLMISS
               10  W1C66791-C-NCP-PROV-CST-CD                           F8899
                                       PIC  X(00002).
               10  W1C66791-C-NCP-UNIT-DOSE-CD                          F6535
                                       PIC  X(00001).
               10  W1C66791-C-NCP-GROSS-AMT                             F7497
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-GROSS-AMT-X REDEFINES                 PMLMISSF7497
                   W1C66791-C-NCP-GROSS-AMT                             PMLMISSF7497
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-MCAID-PD-AMT                          F8880
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-MCAID-PD-AMT-X REDEFINES              PMLMISSF8880
                   W1C66791-C-NCP-MCAID-PD-AMT                          PMLMISSF8880
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-LEVEL-SVC-CD                          F6387
                                       PIC  X(00002).
               10  W1C66791-C-PAT-DAW-DIF-AMT                           F1136
                                       PIC S9(00009)V9(00002) COMP-3.
               10  W1C66791-C-PAT-DAW-DIF-AMT-X REDEFINES               PMLMISSF1136
                   W1C66791-C-PAT-DAW-DIF-AMT                           PMLMISSF1136
                                       PIC  X(06).                      PMLMISS
               10  W1C66791-C-NCP-DISP-STAT-CD                          F7541
                                       PIC  X(00001).
               10  W1C66791-C-NCP-PHRM-ID-CD                            F4307
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PHRM-ID                               F2900
                                       PIC  X(00015).
               10  W1C66791-C-NCP-QTY-PRSC-AMT                          F7224
                                       PIC S9(00007)V9(00003) COMP-3.
               10  W1C66791-C-NCP-QTY-PRSC-AMT-X REDEFINES              PMLMISSF7224
                   W1C66791-C-NCP-QTY-PRSC-AMT                          PMLMISSF7224
                                       PIC  X(06).                      PMLMISS
               10  W1C66791-C-NCP-ORIG-PROD-CD                          F7181
                                       PIC  X(00002).
               10  W1C66791-C-NCP-ORIG-PROD-ID                          F5542
                                       PIC  X(00019).
               10  W1C66791-C-NCP-ORIG-QTY-AMT                          F0544
                                       PIC S9(00007)V9(00003) COMP-3.
               10  W1C66791-C-NCP-ORIG-QTY-AMT-X REDEFINES              PMLMISSF0544
                   W1C66791-C-NCP-ORIG-QTY-AMT                          PMLMISSF0544
                                       PIC  X(06).                      PMLMISS
               10  W1C66791-C-NCP-CLM-ALT-ID                            F5265
                                       PIC  X(00020).
               10  W1C66791-C-NCP-SCHED-RX-NUM                          F9547
                                       PIC  X(00012).
               10  W1C66791-C-NCP-INTD-QTY-AMT                          F6532
                                       PIC S9(00007)V9(00003) COMP-3.
               10  W1C66791-C-NCP-INTD-QTY-AMT-X REDEFINES              PMLMISSF6532
                   W1C66791-C-NCP-INTD-QTY-AMT                          PMLMISSF6532
                                       PIC  X(06).                      PMLMISS
               10  W1C66791-C-NCP-INTD-DAY-AMT                          F6136
                                       PIC S9(00003) COMP-3.
               10  W1C66791-C-NCP-INTD-DAY-AMT-X REDEFINES              PMLMISSF6136
                   W1C66791-C-NCP-INTD-DAY-AMT                          PMLMISSF6136
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-DLY-RSN-CD                            F1608
                                       PIC  X(00002).
               10  W1C66791-C-NCP-TRNS-REF-NUM                          F0659
                                       PIC  X(00010).
               10  W1C66791-C-NCP-PAT-ASGN-IND                          F0760
                                       PIC  X(00001).
               10  W1C66791-C-NCP-RT-ADMIN-DAT                          F0766
                                       PIC  X(00011).
               10  W1C66791-C-NCP-CMPD-TY-CD                            F6751
                                       PIC  X(00002).
               10  W1C66791-C-NCP-MCAID-CTL-ID                          F0045
                                       PIC  X(00020).
               10  W1C66791-C-NCP-PH-SVC-TY-CD                          F6626
                                       PIC  X(00002).
               10  W1C66791-C-NCP-ASOC-RX-NUM                           F6653
                                       PIC  X(00012).
               10  W1C66791-C-NCP-ASOC-RX-DT                            F5100
                                       PIC S9(00008) COMP-3.
               10  W1C66791-C-NCP-ASOC-RX-DT-X REDEFINES                PMLMISSF5100
                   W1C66791-C-NCP-ASOC-RX-DT                            PMLMISSF5100
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-CMPD-DSG-CD                           F3934
                                       PIC  X(00002).
               10  W1C66791-C-NCP-CMPD-DSP-IND                          F3535
                                       PIC S9(00001) COMP-3.
               10  W1C66791-C-NCP-CMPD-DSP-IND-X REDEFINES              PMLMISSF3535
                   W1C66791-C-NCP-CMPD-DSP-IND                          PMLMISSF3535
                                       PIC  X(01).                      PMLMISS
               10  W1C66791-C-NCP-CMPD-ADMN-CD                          F2712
                                       PIC S9(00002) COMP-3.
               10  W1C66791-C-NCP-CMPD-ADMN-CD-X REDEFINES              PMLMISSF2712
                   W1C66791-C-NCP-CMPD-ADMN-CD                          PMLMISSF2712
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-PA-INTR-CD                            F1698
                                       PIC S9(00002) COMP-3.
               10  W1C66791-C-NCP-PA-INTR-CD-X REDEFINES                PMLMISSF1698
                   W1C66791-C-NCP-PA-INTR-CD                            PMLMISSF1698
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-PA-INTR-ID                            F2881
                                       PIC  X(00011).
               10  W1C66791-C-NCP-PA-REQ-CD                             F9832
                                       PIC  X(00001).
               10  W1C66791-C-NCP-PA-RQ-BEG-DT                          F4161
                                       PIC S9(00008) COMP-3.
               10  W1C66791-C-NCP-PA-RQ-BEG-DT-X REDEFINES              PMLMISSF4161
                   W1C66791-C-NCP-PA-RQ-BEG-DT                          PMLMISSF4161
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-PA-RQ-END-DT                          F6859
                                       PIC S9(00008) COMP-3.
               10  W1C66791-C-NCP-PA-RQ-END-DT-X REDEFINES              PMLMISSF6859
                   W1C66791-C-NCP-PA-RQ-END-DT                          PMLMISSF6859
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-PA-BAS-RQ-CD                          F0479
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PA-REP-FNAME                          F2323
                                       PIC  X(00012).
               10  W1C66791-C-NCP-PA-REP-LNAME                          F1531
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PA-REP-STR                            F2324
                                       PIC  X(00030).
               10  W1C66791-C-NCP-PA-REP-CITY                           F0212
                                       PIC  X(00020).
               10  W1C66791-C-NCP-PA-REP-ST-CD                          F7494
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PA-REP-ZIP                            F2755
                                       PIC  X(00015).
               10  W1C66791-C-NCP-PA-ASGN-NUM                           F3523
                                       PIC S9(00011) COMP-3.
               10  W1C66791-C-NCP-PA-ASGN-NUM-X REDEFINES               PMLMISSF3523
                   W1C66791-C-NCP-PA-ASGN-NUM                           PMLMISSF3523
                                       PIC  X(06).                      PMLMISS
               10  W1C66791-C-NCP-PA-AUTH-NUM                           F5141
                                       PIC  X(00020).
               10  W1C66791-C-NCP-PA-SUPP-TX                            F2794
                                       PIC  X(00500).
               10  W1C66791-C-NCP-PPS-FEE-AMT                           F8668
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-PPS-FEE-AMT-X REDEFINES               PMLMISSF8668
                   W1C66791-C-NCP-PPS-FEE-AMT                           PMLMISSF8668
                                       PIC  X(05).                      V
               10  W1C66791-C-NCP-INCNTV-AMT                            F4389
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-INCNTV-AMT-X REDEFINES                PMLMISSF4389
                   W1C66791-C-NCP-INCNTV-AMT                            PMLMISSF4389
                                       PIC X(05).                       PMLMISS
               10  W1C66791-C-NCP-PCT-TX-AMT                            F8709
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C66791-C-NCP-PCT-TX-AMT-X REDEFINES                PMLMISSF8709
                   W1C66791-C-NCP-PCT-TX-AMT                            PMLMISSF8709
                                       PIC  X(05).                      PMLMISS
               10  W1C66791-C-NCP-PCT-RATE-AMT                          F1287
                                       PIC S9(00003)V9(00004) COMP-3.
               10  W1C66791-C-NCP-PCT-RATE-AMT-X REDEFINES              PMLMISS
                   W1C66791-C-NCP-PCT-RATE-AMT                          PMLMISS
                                       PIC X(04).                       PMLMISS
               10  W1C66791-C-NCP-PCT-BASIS-CD                          F6585
                                       PIC  X(00002).
               10  W1C66791-C-NCP-CNT-DUR-NUM                           F0272
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-DUR-NUM-X REDEFINES               PMLMISSF0272
                   W1C66791-C-NCP-CNT-DUR-NUM                           PMLMISSF0272
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-CLIN-NUM                          F5923
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-CLIN-NUM-X REDEFINES              PMLMISSF5923
                   W1C66791-C-NCP-CNT-CLIN-NUM                          PMLMISSF5923
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-DIAG-NUM                          F7851
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-DIAG-NUM-X REDEFINES              PMLMISSF7851
                   W1C66791-C-NCP-CNT-DIAG-NUM                          PMLMISSF7851
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-OAMT-NUM                          F6642
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-OAMT-NUM-X REDEFINES              PMLMISSF6642
                   W1C66791-C-NCP-CNT-OAMT-NUM                          PMLMISSF6642
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-COB-NUM                           F7422
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-COB-NUM-X REDEFINES               PMLMISSF7422
                   W1C66791-C-NCP-CNT-COB-NUM                           PMLMISSF7422
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-OVD-NUM                           F5461
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-OVD-NUM-X REDEFINES               PMLMISSF5461
                   W1C66791-C-NCP-CNT-OVD-NUM                           PMLMISSF5461
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-CMPD-NUM                          F5628
                                       PIC S9(00002) COMP.
               10  W1C66791-C-NCP-CNT-CMPD-NUM-X REDEFINES              PMLMISSF5628
                   W1C66791-C-NCP-CNT-CMPD-NUM                          PMLMISSF5628
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-CNT-MOD-NUM                           F1780
                                       PIC S9(00001) COMP.
               10  W1C66791-C-NCP-CNT-MOD-NUM-X REDEFINES               PMLMISSF1780
                   W1C66791-C-NCP-CNT-MOD-NUM                           PMLMISSF1780
                                       PIC  X(02).                      PMLMISS
               10  W1C66791-C-NCP-SEG-NUM-GRP.                          S4817
                   15  W1C66791-C-NCP-SEG-CLM-NUM                       F6489
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-CLM-NUM-X REDEFINES           PMLMISSF6489
                       W1C66791-C-NCP-SEG-CLM-NUM                       PMLMISSF6489
                                       PIC  X(02).                      PMLMISS
                   15  W1C66791-C-NCP-SEG-PHRM-NUM                      F6771
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-PHRM-NUM-X REDEFINES          PMLMISSF6771
                       W1C66791-C-NCP-SEG-PHRM-NUM                      PMLMISSF6771
                                       PIC  X(02).                      PMLMISS
                   15  W1C66791-C-NCP-SEG-PRSC-NUM                      F5405
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-PRSC-NUM-X REDEFINES          PMLMISSF5405
                       W1C66791-C-NCP-SEG-PRSC-NUM                      PMLMISSF5405
                                       PIC  X(02).                      PMLMISS
                   15  W1C66791-C-NCP-SEG-COB-NUM                       F0374
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-COB-NUM-X REDEFINES           PMLMISS
                       W1C66791-C-NCP-SEG-COB-NUM                       PMLMISS
                                       PIC  X(02).                      PMLMISS
                   15  W1C66791-C-NCP-SEG-WKR-NUM                       F3954
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-WKR-NUM-X REDEFINES           F3954
                       W1C66791-C-NCP-SEG-WKR-NUM                       F3954
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-DUR-NUM                       F6355
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-DUR-NUM-X REDEFINES           F6355
                       W1C66791-C-NCP-SEG-DUR-NUM                       F6355
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-PRIC-NUM                      F2338
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-PRIC-NUM-X REDEFINES          F2338
                       W1C66791-C-NCP-SEG-PRIC-NUM                      F2338
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-COUP-NUM                      F5143
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-COUP-NUM-X REDEFINES          F5143
                       W1C66791-C-NCP-SEG-COUP-NUM                      F5143
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-CMPD-NUM                      F7489
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-CMPD-NUM-X REDEFINES          F7489
                       W1C66791-C-NCP-SEG-CMPD-NUM                      F7489
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-PA-NUM                        F1415
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-PA-NUM-X REDEFINES            F1415
                       W1C66791-C-NCP-SEG-PA-NUM                        F1415
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-CLIN-NUM                      F9655
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-CLIN-NUM-X REDEFINES          F9655
                       W1C66791-C-NCP-SEG-CLIN-NUM                      F9655
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-FACI-NUM                      F2690
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-FACI-NUM-X REDEFINES          F2690
                       W1C66791-C-NCP-SEG-FACI-NUM                      F2690
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-ADDC-NUM                      F2326
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-ADDC-NUM-X REDEFINES          F2326
                       W1C66791-C-NCP-SEG-ADDC-NUM                      F2326
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-NOTE-NUM                      F8120
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-NOTE-NUM-X REDEFINES          F8120
                       W1C66791-C-NCP-SEG-NOTE-NUM                      F8120
                                       PIC  X(02).
                   15  W1C66791-C-NCP-SEG-INV-NUM                       F3745
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-SEG-INV-NUM-X REDEFINES           F3745
                       W1C66791-C-NCP-SEG-INV-NUM                       F3745
                                       PIC  X(02).
               10  W1C66791-C-NCP-FLD-CNT-NUM.                          S4064
                   15  W1C66791-C-NCP-PMOD-NUM                          F1034
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-PMOD-NUM-X REDEFINES              F1034
                       W1C66791-C-NCP-PMOD-NUM                          F1034
                                       PIC  X(02).
                   15  W1C66791-C-NCP-COB-ID-NUM                        F1232
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-COB-ID-NUM-X REDEFINES            PMLMISS
                       W1C66791-C-NCP-COB-ID-NUM                        PMLMISS
                                       PIC X(02).                       PMLMISS
                   15  W1C66791-C-NCP-OTH-AMT-NUM
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-OTH-AMT-NUM-X REDEFINES           PMLMISS
                       W1C66791-C-NCP-OTH-AMT-NUM                       PMLMISS
                                       PIC  X(02).                      PMLMISS
                   15  W1C66791-C-NCP-CMPD-NUM                          F4303
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-CMPD-NUM-X REDEFINES              F4303
                       W1C66791-C-NCP-CMPD-NUM                          F4303
                                       PIC  X(02).
                   15  W1C66791-C-NCP-DIAG-NUM                          F0570
                                       PIC 9(00002) COMP.
                   15  W1C66791-C-NCP-DIAG-NUM-X REDEFINES              F0570
                       W1C66791-C-NCP-DIAG-NUM                          F0570
                                       PIC  X(02).
               10  W1C66791-C-NCP-DUR-PPS-GRP                           S4808
                           OCCURS 0009 TIMES
                           INDEXED BY W1C66791-C-NCP-DUR-PPS-GRP-X .
                   15  W1C66791-C-NCP-DUR-PPS-NUM                       F4687
                                       PIC 9(00001) COMP-3.
                   15  W1C66791-C-NCP-DUR-PPS-NUM-X REDEFINES           F4687
                       W1C66791-C-NCP-DUR-PPS-NUM                       F4687
                                       PIC  X(01).
                   15  W1C66791-C-NCP-DRG-CNFLT-CD                      F8857
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-DUR-INTRV-CD                      F8858
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-DUR-OUTCM-CD                      F8859
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-LVL-EFRT-CD                       F1486
                                       PIC  X(02).
      *                                PIC S9(00002) COMP-3.            COR6916
      *            15  W1C66791-C-NCP-LVL-EFRT-CD-X REDEFINES           COR6916
      *                W1C66791-C-NCP-LVL-EFRT-CD                       COR6916
      *                                PIC  X(02).
                   15  W1C66791-C-NCP-DUR-COAGT-CD                      F5905
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-DUR-COAGT-ID                      F0061
                                       PIC  X(00019).
               10  W1C66791-C-NCP-CLIN-GRP                              S6583
                           OCCURS 0005 TIMES
                           INDEXED BY W1C66791-C-NCP-CLIN-GRP-X .
                   15  W1C66791-C-NCP-CLIN-INF-NUM                      F2724
                                       PIC 9(00001) COMP-3.
                   15  W1C66791-C-NCP-CLIN-INF-NUM-X REDEFINES          F2724
                       W1C66791-C-NCP-CLIN-INF-NUM                      F2724
                                       PIC  X(01).
                   15  W1C66791-C-NCP-CLIN-MSMT-DT                      F8346
                                       PIC S9(00008) COMP-3.
                   15  W1C66791-C-NCP-CLIN-MSMT-DT-X REDEFINES          F8346
                       W1C66791-C-NCP-CLIN-MSMT-DT                      F8346
                                       PIC  X(05).
                   15  W1C66791-C-NCP-CLIN-MSMT-TM                      F8113
                                       PIC S9(00004) COMP-3.
                   15  W1C66791-C-NCP-CLIN-MSMT-TM-X REDEFINES          F8113
                       W1C66791-C-NCP-CLIN-MSMT-TM                      F8113
                                       PIC  X(03).
                   15  W1C66791-C-NCP-CLIN-DIM-CD                       F8521
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-CLIN-UNIT-CD                      F0613
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-CLIN-VALU-TX                      F8206
                                       PIC  X(00015).
               10  W1C66791-C-NCP-DIAG-GRP                              S7260
                           OCCURS 0005 TIMES
                           INDEXED BY W1C66791-C-NCP-DIAG-GRP-X .
                   15  W1C66791-C-NCP-DIAG-QUAL-CD                      F9219
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-DIAG-ICD9-CD                      F8856
                                       PIC  X(00015).
               10  W1C66791-C-NCP-OTHR-AMT-GRP                          S8099
                           OCCURS 0003 TIMES
                           INDEXED BY W1C66791-C-NCP-OTHR-AMT-GRP-X .
                   15  W1C66791-C-NCP-OTHR-AMT-CD                       F6306
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-OTHR-AMT                          F4569
                                       PIC S9(00006)V9(00002) COMP-3.
                   15  W1C66791-C-NCP-OTHR-AMT-X REDEFINES              F4569
                       W1C66791-C-NCP-OTHR-AMT                          F4569
                                       PIC  X(05).
               10  W1C66791-C-NCP-COB-ID-GRP                            S3084
                           OCCURS 0009 TIMES
                           INDEXED BY W1C66791-C-NCP-COB-ID-GRP-X .
                   15  W1C66791-C-NCP-COB-COV-CD                        F7393
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-COB-ID-CD                         F7862
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-COB-ID                            F7586
                                       PIC  X(00010).
                   15  W1C66791-C-NCP-COB-DT                            F8888
                                       PIC S9(00008) COMP-3.
                   15  W1C66791-C-NCP-COB-DT-X REDEFINES                F8888
                       W1C66791-C-NCP-COB-DT                            F8888
                                       PIC  X(05).
                   15  W1C66791-C-NCP-CNTRL-NUM                         F2327
                                       PIC  X(00030).
                   15  W1C66791-C-NCP-CNT-COBA-NUM                      F8016
                                       PIC S9(00001) COMP.
                   15  W1C66791-C-NCP-CNT-COBA-NUM-X REDEFINES          F8016
                       W1C66791-C-NCP-CNT-COBA-NUM                      F8016
                                       PIC  X(02).
                   15  W1C66791-C-NCP-CNT-COBR-NUM                      F3291
                                       PIC S9(00001) COMP.
                   15  W1C66791-C-NCP-CNT-COBR-NUM-X REDEFINES          F3291
                       W1C66791-C-NCP-CNT-COBR-NUM                      F3291
                                       PIC  X(02).
                   15  W1C66791-C-NCP-CNT-COBP-NUM                      F1286
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-CNT-COBP-NUM-X REDEFINES          F1286
                       W1C66791-C-NCP-CNT-COBP-NUM                      F1286
                                       PIC  X(02).
                   15  W1C66791-C-NCP-CNT-COBB-NUM                      F2699
                                       PIC S9(00001) COMP.
                   15  W1C66791-C-NCP-CNT-COBB-NUM-X REDEFINES          F2699
                       W1C66791-C-NCP-CNT-COBB-NUM                      F2699
                                       PIC  X(02).
                   15  W1C66791-C-NCP-COB-PAID-NUM                      F3701
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-COB-PAID-NUM-X REDEFINES          F3701
                       W1C66791-C-NCP-COB-PAID-NUM                      F3701
                                       PIC  X(02).
                   15  W1C66791-C-NCP-COB-REJ-NUM                       F0698
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-COB-REJ-NUM-X REDEFINES           F0698
                       W1C66791-C-NCP-COB-REJ-NUM                       F0698
                                       PIC  X(02).
                   15  W1C66791-C-NCP-COB-PAT-NUM                       F1622
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-COB-PAT-NUM-X REDEFINES           F1622
                       W1C66791-C-NCP-COB-PAT-NUM                       F1622
                                       PIC  X(02).
                   15  W1C66791-C-NCP-COB-BENE-NUM                      F4991
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-COB-BENE-NUM-X REDEFINES          F4991
                       W1C66791-C-NCP-COB-BENE-NUM                      F4991
                                       PIC  X(02).
                   15  W1C66791-C-NCP-COB-AMT-GRP                       S7042
                           OCCURS 0009 TIMES
                           INDEXED BY W1C66791-C-NCP-COB-AMT-GRP-X .
                       20  W1C66791-C-NCP-COB-AMT-CD                    F4098
                                       PIC  X(00002).
                       20  W1C66791-C-NCP-COB-AMT                       F3287
                                       PIC S9(00006)V9(00002) COMP-3.
                       20  W1C66791-C-NCP-COB-AMT-X REDEFINES           F3287
                           W1C66791-C-NCP-COB-AMT                       F3287
                                       PIC  X(05).
                   15  W1C66791-C-NCP-COB-REJ-GRP                       S4699
                           OCCURS 0025 TIMES
                           INDEXED BY W1C66791-C-NCP-COB-REJ-GRP-X .
                       20  W1C66791-C-NCP-COB-REJ-CD                    F5712
                                       PIC  X(00003).
                   15  W1C66791-C-NCP-COB-PAT-GRP                       S1985
                           OCCURS 0025 TIMES
                           INDEXED BY W1C66791-C-NCP-COB-PAT-GRP-X .
                       20  W1C66791-C-NCP-COB-PAT-CD                    F1389
                                       PIC  X(00002).
                       20  W1C66791-C-NCP-COB-PAT-AMT                   F2691
                                       PIC S9(00008)V9(00002) COMP-3.
                       20  W1C66791-C-NCP-COB-PAT-AMT-X REDEFINES       F2691
                           W1C66791-C-NCP-COB-PAT-AMT                   F2691
                                       PIC  X(06).
                   15  W1C66791-C-NCP-COB-BENE-GRP                      S6430
                           OCCURS 0004 TIMES
                           INDEXED BY W1C66791-C-NCP-COB-BENE-GRP-X .
                       20  W1C66791-C-NCP-COB-BENE-CD                   F2694
                                       PIC  X(00002).
                       20  W1C66791-C-NCP-COB-BENE-AMT                  F3175
                                       PIC S9(00006)V9(00002) COMP-3.
                       20  W1C66791-C-NCP-COB-BENE-AMT-X REDEFINES      F3175
                           W1C66791-C-NCP-COB-BENE-AMT                  F3175
                                       PIC  X(05).
               10  W1C66791-C-NCP-CPN-GRP.                              S3420
                   15  W1C66791-C-NCP-CPN-TY-CD                         F5462
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-CPN-NUM                           F0894
                                       PIC  X(00015).
                   15  W1C66791-C-NCP-CPN-VAL-AMT                       F1096
                                       PIC S9(00006)V9(00002) COMP-3.
                   15  W1C66791-C-NCP-CPN-VAL-AMT-X REDEFINES           F1096
                       W1C66791-C-NCP-CPN-VAL-AMT                       F1096
                                       PIC  X(05).
               10  W1C66791-C-NCP-CMPD-GRP                              S3708
                           OCCURS 0040 TIMES
                           INDEXED BY W1C66791-C-NCP-CMPD-GRP-X .
                   15  W1C66791-C-NCP-CMPD-PROD-CD                      F3858
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-CMPD-PROD-ID                      F7398
                                       PIC  X(00019).
                   15  W1C66791-C-NCP-CMPD-QTY-AMT                      F8342
                                       PIC S9(00007)V9(00003) COMP-3.
                   15  W1C66791-C-NCP-CMPD-QTY-AMT-X REDEFINES          F8342
                       W1C66791-C-NCP-CMPD-QTY-AMT                      F8342
                                       PIC  X(06).
                   15  W1C66791-C-NCP-CMPD-CST-AMT                      F9521
                                       PIC S9(00006)V9(00002) COMP-3.
                   15  W1C66791-C-NCP-CMPD-CST-AMT-X REDEFINES          F9521
                       W1C66791-C-NCP-CMPD-CST-AMT                      F9521
                                       PIC  X(05).
                   15  W1C66791-C-NCP-CMPD-BAS-CD                       F6919
                                       PIC  X(00002).
                   15  W1C66791-C-NCP-CMPD-MOD-NUM                      F9387
                                       PIC S9(00002) COMP.
                   15  W1C66791-C-NCP-CMPD-MOD-NUM-X REDEFINES          F9387
                       W1C66791-C-NCP-CMPD-MOD-NUM                      F9387
                                       PIC  X(02).
                   15  W1C66791-C-NCP-CMPND-MOD-GRP                     S0233
                           OCCURS 0010 TIMES
                           INDEXED BY W1C66791-C-NCP-CMPND-MOD-GRP-X .
                       20  W1C66791-C-NCP-CMPND-MOD-CD                  F1390
                                       PIC  X(00002).
               10  W1C66791-C-NCP-PROC-MOD-GRP                          S5371
                           OCCURS 0010 TIMES
                           INDEXED BY W1C66791-C-NCP-PROC-MOD-GRP-X .
                   15  W1C66791-C-NCP-PROC-MOD-CD                       F5158
                                       PIC  X(00002).
               10  W1C66791-C-NCP-FACI-ID                               F2328
                                       PIC  X(00010).
               10  W1C66791-C-NCP-FACI-NAM                              F2329
                                       PIC  X(00030).
               10  W1C66791-C-NCP-FACI-STR-AD                           F0597
                                       PIC  X(00030).
               10  W1C66791-C-NCP-FACI-CITY                             F4978
                                       PIC  X(00020).
               10  W1C66791-C-NCP-FACI-ST-CD                            F0519
                                       PIC  X(00002).
               10  W1C66791-C-NCP-FACI-ZIP-CD                           F0634
                                       PIC  X(00015).
               10  W1C66791-C-NCP-ADDC-TY-CD                            F0895
                                       PIC  X(00003).
               10  W1C66791-C-NCP-ADDC-REQ-DT                           F2330
                                       PIC S9(00008) COMP.
               10  W1C66791-C-NCP-ADDC-REQ-DT-X REDEFINES               F2330
                   W1C66791-C-NCP-ADDC-REQ-DT                           F2330
                                       PIC  X(04).
               10  W1C66791-C-NCP-ADDC-REV-DT                           F1067
                                       PIC S9(00008) COMP.
               10  W1C66791-C-NCP-ADDC-REV-DT-X REDEFINES               F1067
                   W1C66791-C-NCP-ADDC-REV-DT                           F1067
                                       PIC  X(04).
               10  W1C66791-C-NCP-ADDC-ST-IND                           F0735
                                       PIC  X(00001).
               10  W1C66791-C-NCP-ADDC-LEN-CD                           F2331
                                       PIC S9(00002) COMP.
               10  W1C66791-C-NCP-ADDC-LEN-CD-X REDEFINES               F2331
                   W1C66791-C-NCP-ADDC-LEN-CD                           F2331
                                       PIC  X(02).
               10  W1C66791-C-NCP-ADDC-LEN-NUM                          F0488
                                       PIC S9(00003) COMP.
               10  W1C66791-C-NCP-ADDC-LEN-NUM-X REDEFINES              F0488
                   W1C66791-C-NCP-ADDC-LEN-NUM                          F0488
                                       PIC  X(02).
               10  W1C66791-C-NCP-ADDC-PRSC-DT                          F3427
                                       PIC S9(00008) COMP.
               10  W1C66791-C-NCP-ADDC-PRSC-DT-X REDEFINES              F3427
                   W1C66791-C-NCP-ADDC-PRSC-DT                          F3427
                                       PIC  X(04).
               10  W1C66791-C-NCP-ADDC-SUP-DOC                          F4609
                                       PIC  X(00065).
               10  W1C66791-C-NCP-ADDC-QST-CNT                          F1694
                                       PIC S9(00002) COMP.
               10  W1C66791-C-NCP-ADDC-QST-CNT-X REDEFINES              F1694
                   W1C66791-C-NCP-ADDC-QST-CNT                          F1694
                                       PIC  X(02).
               10  W1C66791-C-NCP-ADDC-QST-NUM                          F4401
                                       PIC  X(00003).
               10  W1C66791-C-NCP-ADDC-QST-PCT                          F3534
                                       PIC S9(00003)V9(00002) COMP-3.
               10  W1C66791-C-NCP-ADDC-QST-PCT-X REDEFINES              F3534
                   W1C66791-C-NCP-ADDC-QST-PCT                          F3534
                                       PIC  X(03).
               10  W1C66791-C-NCP-ADDC-QST-DT                           F2579
                                       PIC S9(00008) COMP.
               10  W1C66791-C-NCP-ADDC-QST-DT-X REDEFINES               F2579
                   W1C66791-C-NCP-ADDC-QST-DT                           F2579
                                       PIC  X(04).
               10  W1C66791-C-NCP-ADDC-QST-AMT                          F4152
                                       PIC S9(00009)V9(00002) COMP-3.
               10  W1C66791-C-NCP-ADDC-QST-AMT-X REDEFINES              F4152
                   W1C66791-C-NCP-ADDC-QST-AMT                          F4152
                                       PIC  X(06).
               10  W1C66791-C-NCP-ADDC-QST-RSP                          F3794
                                       PIC  X(00011).
               10  W1C66791-C-NCP-ADDC-QST-TX                           F9274
                                       PIC  X(00030).
               10  W1C66791-C-NCP-NARR-MSG-TX                           F0677
                                       PIC  X(00200).

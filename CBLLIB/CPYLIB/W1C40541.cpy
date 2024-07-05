
      *****************************************************************
      *                                                               *
      *    MEMBER:  W1C40541                                          *
      *      NAME:  C-CNTL-PHRM-IO-VW                                 *
      * SUBSYSTEM:  C Claims                                          *
      *   VERSION:  1                                                 *
      * GENERATED:  2012-12-07 08:32:11 REQUESTED BY ACS0127          *
      *                                                               *
      * Claim Control Pharmacy IO View                                *
      *                                                               *
      *****************************************************************
      *                                                               *
       01  W1C40541-C-CNTL-PHRM-IO-VW.                                  S4054
           05  W1C40541-C-CNTL-ONL-COMMON-VW.                           S1593
               10  W1C40541-C-CNTL-EXE-MODE-CD                          F5089
                                       PIC  X(00001).
               10  W1C40541-C-CNTL-EXC-ADR-NUM                          F6518
                                       PIC S9(00008) COMP.
               10  W1C40541-C-CNTL-PROC-CD                              F0146
                                       PIC  X(00001).
               10  W1C40541-C-CNTL-EVNT-SRC-CD                          F8286
                                       PIC  X(00008).
           05  W1C40541-C-PHRM-VW.                                      S4154
               10  W1C40541-C-HDR-COMMON-VW.                            S5777
                   15  W1C40541-C-SORT-DAT                              F6298
                                       PIC  X(00060).
                   15  W1C40541-C-COUNTERS-VW.                          S6118
                       20  W1C40541-C-CNT-EXC-NUM                       F3615
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-COE-NUM                       F5337
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-RLTD-HST-NUM                  F9838
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-DUR-PPS-NUM                   F6272
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-CLINICAL-NUM                  F3037
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-DIAG-NUM                      F9343
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-OTHR-AMT-NUM                  F4578
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-COB-ID-NUM                    F2841
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-LI-NUM                        F8510
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-PROC-MOD-NUM                  F5224
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-BSE-CHG-NUM                   F5345
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-TPL-BLNG-NUM                  F7569
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-OVRD-EOB-NUM                  F7026
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-OVRD-EXC-NUM                  F7715
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-RUL-NUM                       CR3837
                                       PIC S9(00004) COMP.              CR3837
                   15  W1C40541-C-HDR-TB.                               S3491
                       20  W1C40541-R-CUST-PART-NUM                     F4329
                                       PIC S9(00004) COMP.
                       20  W1C40541-B-SYS-ID                            F0694
                                       PIC S9(00009) COMP.
                       20  W1C40541-C-TCN-NUM                           F1024
                                       PIC S9(00017) COMP-3.
                       20  W1C40541-C-CNTL-ORGN-CD                      F0147
                                       PIC  X(00001).
                       20  W1C40541-C-BAT-MED-SRC-CD                    F0142
                                       PIC  X(00001).
                       20  W1C40541-C-BAT-JLN-DT-NUM                    F0727
                                       PIC  X(00005).
                       20  W1C40541-C-BAT-IMG-CNTL-NUM                  F0738
                                       PIC  X(00002).
                       20  W1C40541-C-BAT-NUM                           F0729
                                       PIC  X(00003).
                       20  W1C40541-C-BAT-TY-CD                         F0140
                                       PIC  X(00001).
                       20  W1C40541-B-FST-NAM                           F0637
                                       PIC  X(00015).
                       20  W1C40541-B-LAST-NAM                          F0639
                                       PIC  X(00021).
                       20  W1C40541-B-MI-NAM                            F0640
                                       PIC  X(00001).
                       20  W1C40541-B-RACE-CD                           F0230
                                       PIC  X(00006).
                       20  W1C40541-B-GENDER-CD                         F0229
                                       PIC  X(00001).
                       20  W1C40541-C-PAT-REL-CD                        F0871
                                       PIC S9(00001) COMP.
                       20  W1C40541-C-PAT-LOC-CD                        F5668
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-PRGNCY-IND                        F9269
                                       PIC  X(00001).
                       20  W1C40541-A-AUTH-NUM                          F2749
                                       PIC  X(00020).
                       20  W1C40541-C-PA-BASIS-REQ-CD                   F9032
                                       PIC  X(00002).
                       20  W1C40541-C-PA-REQ-BEG-DT                     F8048
                                       PIC  X(00010).
                       20  W1C40541-C-PA-REQ-END-DT                     F6066
                                       PIC  X(00010).
                       20  W1C40541-C-PA-SUP-DOC-TX                     F8950
                                       PIC  X(00070).
                       20  W1C40541-C-INTMDRY-AUTH-CD                   F8642
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-INTMDRY-AUTH-ID                   F3141
                                       PIC  X(00011).
                       20  W1C40541-C-CLM-REF-ID-NUM                    F7500
                                       PIC  X(00030).
                       20  W1C40541-C-BLNG-NTRPRS-ID                    F5424
                                       PIC  X(00015).
                       20  W1C40541-C-ATTACH-1ST-CD                     F6701
                                       PIC  X(00002).
                       20  W1C40541-C-ATTACH-2ND-CD                     F3737
                                       PIC  X(00002).
                       20  W1C40541-C-ATTACH-3RD-CD                     F1342
                                       PIC  X(00002).
                       20  W1C40541-C-HDR-RMK-IND                       F1019
                                       PIC  X(00001).
                       20  W1C40541-C-OTHR-INSR-IND                     F3078
                                       PIC 9(00002) COMP.
                       20  W1C40541-P-BORDER-CD                         F7067
                                       PIC  X(00001).
                       20  W1C40541-C-COST-AVOID-IND                    F0777
                                       PIC  X(00001).
                       20  W1C40541-C-HDR-RA-NUM                        F1042
                                       PIC S9(00009) COMP.
                       20  W1C40541-C-EXC-LOCN-CD                       F2822
                                       PIC  X(00003).
                       20  W1C40541-C-EXC-LOCN-ID                       F3969
                                       PIC  X(00008).
                       20  W1C40541-C-SUSP-LOCN-EXC-CD                  F1174
                                       PIC  X(00004).
                       20  W1C40541-C-OVRRD-EXC-LOC-CD                  F1126
                                       PIC  X(00003).
                       20  W1C40541-C-COST-CENTER-CD                    F7827
                                       PIC  X(00005).
                       20  W1C40541-C-CYCL-NUM                          F1014
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-OVRRD-SEC-EOB-ID                  F8871
                                       PIC  X(00007).
                       20  W1C40541-C-CALC-TOT-AMT                      F0776
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-TOT-TPL-AMT                       F1029
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-TOT-NET-CHRG-AMT                  F1027
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-PREV-REIMB-AMT                    F1154
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-PREV-REIMB-CD                     F1149
                                       PIC  X(00001).
                       20  W1C40541-P-PC-ALT-ID-CD                      F7364
                                       PIC  X(00002).
                       20  W1C40541-P-PC-ALT-ID                         F4355
                                       PIC  X(00015).
                       20  W1C40541-B-DOB-DT                            F0601
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-ENTRD-DT                      F0995
                                       PIC  X(00010).
                       20  W1C40541-C-BILLED-DT                         F0966
                                       PIC  X(00010).
                       20  W1C40541-C-INV-BILL-DT                       F2625
                                       PIC  X(00010).
                       20  W1C40541-C-EXC-LOCN-DT                       F4798
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-BATCH-DT                      F0965
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-LST-CYCL-DT                   F1010
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-ORIG-PD-DT                    F1015
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-SUSP-DT                       F1021
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-HX-DT                         F1005
                                       PIC  X(00010).
                       20  W1C40541-C-ADJUD-TM                          F3251
                                       PIC  X(00008).
                       20  W1C40541-C-HDR-INJURY-DT                     F1912
                                       PIC  X(00010).
                       20  W1C40541-C-SWTCH-VNDR-CD                     F8664
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-TXN-ELPSED-NUM                    F6397
                                       PIC 9(00004)V9(00006) COMP-3.
                       20  W1C40541-B-RPT-ALT-ID                        F5459
                                       PIC  X(00020).
                       20  W1C40541-B-PAT-ALT-ID                        F6224
                                       PIC  X(00020).
                       20  W1C40541-B-PAT-ALT-ID-CD                     F2696
                                       PIC  X(00002).
                       20  W1C40541-C-SUB-CAPITATED-CD                  F5720
                                       PIC  X(00002).
                       20  W1C40541-C-NCPDP-TXN-CD                      F2716
                                       PIC  X(00002).
                       20  W1C40541-G-AUD-USER-ID                       F0531
                                       PIC  X(00030).
                       20  W1C40541-G-AUD-TS                            F0530
                                       PIC  X(00026).
                       20  W1C40541-C-BIN-NUM                           F2292
                                       PIC S9(00006) COMP.
                       20  W1C40541-C-PRCS-NUM                          F6434
                                       PIC  X(00010).
                       20  W1C40541-C-TRAN-CNT-NUM                      F2568
                                       PIC S9(00001) COMP.
                       20  W1C40541-C-HOME-PLN-TX                       F5069
                                       PIC  X(00003).
                       20  W1C40541-C-SUBM-PLAN-ID                      F3738
                                       PIC  X(00008).
                       20  W1C40541-C-PH-SVC-TY-CD                      F2303
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-PH-SVC-TY-CD-X REDEFINES          DF3180
                           W1C40541-C-PH-SVC-TY-CD                      DF3180
                                       PIC  X(00002).                   DF3180
                       20  W1C40541-C-PCP-LOC-CD                        F6431
                                       PIC  X(00003).
                       20  W1C40541-C-PCP-LAST-NAM                      F1479
                                       PIC  X(00015).
                       20  W1C40541-C-POS-TRAN-CD                       F7011
                                       PIC  X(00002).
                       20  W1C40541-C-REF-ID-CD                         F8167
                                       PIC  X(00002).
                       20  W1C40541-C-SVC-LOC-CD                        F2700
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-SVC-LOC-CD-X REDEFINES            DF1066
                           W1C40541-C-SVC-LOC-CD                        DF1066
                                       PIC  X(00002).                   DF1066
                       20  W1C40541-C-REF-PHYS-ID                       F2339
                                       PIC  X(00015).
                       20  W1C40541-C-NCPDP-RTG-NUM                     F5193
                                       PIC  X(00015).
                       20  W1C40541-C-PLAN-STAT-CD                      F0771
                                       PIC  X(00002).
                       20  W1C40541-C-PLN-CUST-ID                       F1630
                                       PIC  X(00015).
                       20  W1C40541-C-PLAN-AUX-1ST-ID                   F9595
                                       PIC  X(00003).
                       20  W1C40541-C-PLAN-AUX-2ND-ID                   F1167
                                       PIC  X(00003).
                       20  W1C40541-C-PLAN-AUX-3RD-ID                   F8905
                                       PIC  X(00003).
                       20  W1C40541-C-BAT-FILE-ID                       F0635
                                       PIC  X(00007).
                       20  W1C40541-R-SNDR-ID                           F2592
                                       PIC  X(00024).
                       20  W1C40541-C-BAT-DT                            F1221
                                       PIC  X(00010).
                       20  W1C40541-C-BAT-TM                            F2360
                                       PIC  X(00008).
                   15  W1C40541-C-LI-DRUG-MAIN-VW.                      S6147
                       20  W1C40541-R-CUST-ID                           F7281
                                       PIC  X(00006).
                       20  W1C40541-R-GROUP-ID                          F4720
                                       PIC  X(00008).
                       20  W1C40541-R-PLAN-ID                           F0097
                                       PIC  X(00003).
                       20  W1C40541-C-HDR-SVC-FST-DT                    F1022
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-STAT-CD                       F1020
                                       PIC  X(00001).
                       20  W1C40541-C-HDR-TXN-TY-CD                     F1030
                                       PIC  X(00001).
                       20  W1C40541-C-BAT-DOC-TY-CD                     F0161
                                       PIC  X(00001).
                       20  W1C40541-B-MBR-NUM                           F5722
                                       PIC S9(00004) COMP.
                       20  W1C40541-B-CRDHLD-ALT-ID-CD                  F6262
                                       PIC  X(00002).
                       20  W1C40541-B-CRDHLDR-ALT-ID                    F7680
                                       PIC  X(00020).
                       20  W1C40541-C-HDR-TY-CD                         F1031
                                       PIC  X(00001).
                       20  W1C40541-C-TOT-CHRG-AMT                      F1025
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-RX-SVC-REF-CD                     F9049
                                       PIC  X(00001).
                       20  W1C40541-C-RX-SVC-REF-NUM                    F0990
                                       PIC S9(00012) COMP-3.
                       20  W1C40541-C-RX-REFILL-NUM                     F1159
                                       PIC S9(00002) COMP-3.
                       20  W1C40541-C-DRUG-RX-OVRRD-CD                  F0874
                                       PIC  X(00002).
                       20  W1C40541-C-DRUG-RX-OVRD-CNT                  F7191
                                       PIC S9(00001) COMP.
                       20  W1C40541-C-DRUG-RX-OVR2-CD                   F9475
                                       PIC  X(00002).
                       20  W1C40541-C-DRUG-RX-OVR3-CD                   F2299
                                       PIC  X(00002).
                       20  W1C40541-C-HDR-ALLOW-AMT                     F0964
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-OVER-MAX-AMT                      F0821
                                       PIC S9(00005)V9(00002) COMP-3.
                       20  W1C40541-C-TOT-COPAY-AMT                     F1026
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-HDR-ADJUD-DT                      F0963
                                       PIC  X(00010).
                       20  W1C40541-C-PD-DAYS-SPLY-AMT                  F1138
                                       PIC S9(00003) COMP-3.
                       20  W1C40541-C-HDR-PD-QTY-AMT                    F1265
                                       PIC S9(00008)V9(00003) COMP-3.
                       20  W1C40541-C-DISP-FEE-AMT                      F0817
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-DRUG-SLS-TAX-AMT                  F0875
                                       PIC S9(00005)V9(00002) COMP-3.
                       20  W1C40541-C-DRUG-SUB-QTY-AMT                  F0991
                                       PIC S9(00008)V9(00003) COMP-3.
                       20  W1C40541-C-INPUT-FRM-CD                      F3145
                                       PIC  X(00001).
                       20  W1C40541-C-PAT-LIAB-AMT                      F1137
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-BLNG-PROV-TY-CD                   F0733
                                       PIC  X(00003).
                       20  W1C40541-C-BLNG-PROV-ID                      F0403
                                       PIC 9(00009) COMP.
                       20  W1C40541-P-PAY-TO-PROV-NUM                   F6313
                                       PIC  X(00015).
                       20  W1C40541-B-HH-SYS-ID                         F0586
                                       PIC S9(00009) COMP.
                       20  W1C40541-C-BAT-PYMT-TY-CD                    F0070
                                       PIC  X(00001).
                       20  W1C40541-C-CREDIT-CD                         F0978
                                       PIC  X(00001).
                       20  W1C40541-C-HDR-CLNT-AGE                      F0971
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-TOT-REIMB-AMT                     F1028
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-C-HDR-PD-DT                         F1017
                                       PIC  X(00010).
                       20  W1C40541-C-HDR-ID-CD                         F1006
                                       PIC  X(00002).
                       20  W1C40541-C-DRUG-TEST-IND                     F0877
                                       PIC  X(00001).
                       20  W1C40541-C-PA-TYP-CD                         F0596
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-HDR-DRUG-DED-AMT                  F0987
                                       PIC S9(00009)V9(00002) COMP-3.
                       20  W1C40541-P-SVC-PH-ALT-ID-CD                  F4844
                                       PIC  X(00002).
                       20  W1C40541-P-SVC-PHARM-ALT-ID                  F5485
                                       PIC  X(00015).
                       20  W1C40541-C-HDR-DOC-NUM                       F0983
                                       PIC  X(00006).
                       20  W1C40541-P-PRSC-ALT-ID-CD                    F2693
                                       PIC  X(00002).
                       20  W1C40541-P-PRSC-ALT-ID                       F1140
                                       PIC  X(00015).
                       20  W1C40541-C-PRSC-PROV-ID                      F0410
                                       PIC S9(00008) COMP.
                       20  W1C40541-C-SUB-DAY-SPLY-AMT                  F1170
                                       PIC S9(00003) COMP-3.
                       20  W1C40541-C-DRUG-PRESCR-DT                    F0860
                                       PIC  X(00010).
                       20  W1C40541-C-DRUG-CMPND-CD                     F0824
                                       PIC  X(00001).
                       20  W1C40541-C-DISP-STATUS-CD                    F8723
                                       PIC  X(00001).
                       20  W1C40541-C-DAILY-RPT-IND                     F0806
                                       PIC  X(00001).
                       20  W1C40541-R-DUPCK-EXMT-IND                    F8419
                                       PIC  X(00001).
                       20  W1C40541-R-MO-GROUP-ID                       F2813
                                       PIC  X(00006).
                       20  W1C40541-C-REMAIN-ALLOW-AMT                  F3798
                                       PIC S9(00006)V9(00002) COMP-3.
                   15  W1C40541-C-LI-DRUG-DTL-GRP.                      S6750
                       20  W1C40541-C-LI-DRUG-DTL-VW                    S1441
                           OCCURS 0015 TIMES
                           INDEXED BY W1C40541-C-LI-DRUG-DTL-VW-X .
                           25  W1C40541-C-LI-NUM                        F1073
                                       PIC S9(00003) COMP.
                           25  W1C40541-C-DRUG-GEN-CD-NUM               F0841
                                       PIC  X(00005).
                           25  W1C40541-C-DRUG-DEA-CD                   F0858
                                       PIC  X(00001).
                           25  W1C40541-C-DRUG-ROUTE-CD                 F0872
                                       PIC  X(00001).
                           25  W1C40541-R-DRG-THR-CHAR3-CD              F1804
                                       PIC  X(00003).
                           25  W1C40541-R-DRUG-GCN-SEQ-NUM              F1838
                                       PIC S9(00006) COMP-3.
                           25  W1C40541-R-DRUG-GEN-PRD-CD               F1839
                                       PIC  X(00001).
                           25  W1C40541-R-DRUG-CD                       F1813
                                       PIC  X(00011).
                           25  W1C40541-C-BSE-AMT-SRC-CD                F0167
                                       PIC  X(00002).
                           25  W1C40541-C-DRUG-MAINT-IND                F0848
                                       PIC  X(00001).
                           25  W1C40541-C-PROD-SVC-ID-CD                F1567
                                       PIC  X(00002).
                           25  W1C40541-C-PROD-SVC-ID                   F2726
                                       PIC  X(00019).
                           25  W1C40541-C-LI-BSE-AMT                    F1072
                                       PIC S9(00007)V9(00002) COMP-3.
                           25  W1C40541-C-CALC-ALLOW-AMT                F0743
                                       PIC S9(00009)V9(00002) COMP-3.
                           25  W1C40541-C-LI-ALLOW-AMT                  F8909
                                       PIC S9(00009)V9(00002) COMP-3.
                           25  W1C40541-C-LI-ING-ALLOW-AMT              F4334
                                       PIC S9(00007)V9(00002) COMP-3.
                           25  W1C40541-C-SUBM-ING-QTY-AMT              F9796
                                       PIC S9(00008)V9(00003) COMP-3.
                           25  W1C40541-C-SUBM-ING-CST-AMT              F8161
                                       PIC S9(00007)V9(00002) COMP-3.
                           25  W1C40541-C-LI-PD-QTY-AMT                 F0989
                                       PIC S9(00008)V9(00003) COMP-3.
                           25  W1C40541-C-DISCT-RATE-AMT                F0832
                                       PIC S9(00007)V9(00002) COMP-3.
                           25  W1C40541-C-INGRED-DISCT-PCT              F1055
                                       PIC S9(00003)V9(00002) COMP-3.
                           25  W1C40541-C-BASIS-COST-CD                 F9136
                                       PIC  X(00002).
                           25  W1C40541-C-REIMB-STAT-CD                 F0162
                                       PIC  X(00001).
                           25  W1C40541-A-ID                            F0426
                                       PIC S9(00011) COMP-3.
                           25  W1C40541-A-LI-NUM                        F0429
                                       PIC S9(00002) COMP.
                           25  W1C40541-C-PRIOR-AUTH-IND                F1142
                                       PIC  X(00001).
                           25  W1C40541-C-LI-REIMB-AMT                  F1087
                                       PIC S9(00007)V9(00002) COMP-3.
                           25  W1C40541-C-DRUG-NAM                      F0849
                                       PIC  X(00030).
                           25  W1C40541-C-CTBCK-RSN-CD                  F1243
                                       PIC  X(00001).
                           25  W1C40541-C-CTBCK-CD                      F7510
                                       PIC  X(00001).
                           25  W1C40541-P-PRS-ALT-ID-TY-CD              F1488
                                       PIC  X(00006).
                           25  W1C40541-P-SPH-ALT-ID-TY-CD              F1638
                                       PIC  X(00006).
                           25  W1C40541-C-DER-OVRRD-CD                  F2619
                                       PIC  X(00002).
                           25  W1C40541-R-RX-LMT-EXMT-CD                F2368
                                       PIC  X(00001).
                           25  W1C40541-C-SPA-PRCS-CD                   F2585
                                       PIC  X(00001).
                           25  W1C40541-R-GENR-THERA-IND                F5609
                                       PIC  X(00001).
                           25  W1C40541-R-BRND-GENR-CD                  F7206
                                       PIC  X(00001).
                           25  W1C40541-A-PA-EXMPT-CD                   F3898
                                       PIC  X(00001).
                           25  W1C40541-C-CMPD-MOD-NUM                  F7963
                                       PIC S9(00002) COMP.
                           25  W1C40541-C-CMPD-MOD-1-CD                 F1601
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-2-CD                 F4692
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-3-CD                 F2314
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-4-CD                 F5021
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-5-CD                 F6798
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-6-CD                 F0997
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-7-CD                 F6512
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-8-CD                 F2615
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-9-CD                 F7501
                                       PIC  X(00002).
                           25  W1C40541-C-CMPD-MOD-10-CD                F2574
                                       PIC  X(00002).
                           25  W1C40541-C-RX-NUM-CD                     F5139
                                       PIC  X(00001).
                           25  W1C40541-R-PRC-PD-IND-CD                 F7646
                                       PIC  X(00003).
                           25  W1C40541-C-UNT-CHG-AMT                   F8297
                                       PIC S9(00009)V9(00005) COMP-3.
                           25  W1C40541-R-PRC-NUM                       F1623
                                       PIC  X(00002).
                   15  W1C40541-C-LI-EXC-GRP.                           S7922
                       20  W1C40541-C-LI-EXC-VW                         S2498
                           OCCURS 0050 TIMES
                           INDEXED BY W1C40541-C-LI-EXC-VW-X .
                           25  W1C40541-C-LI-NUM                        F1073
                                       PIC S9(00003) COMP.
                           25  W1C40541-R-CLM-EXC-CD                    F1737
                                       PIC  X(00004).
                           25  W1C40541-C-LI-EXC-CLRK-ID                F1077
                                       PIC  X(00008).
                           25  W1C40541-C-EXC-STAT-CD                   F4200
                                       PIC  X(00001).
                           25  W1C40541-R-EXC-FORCE-APP-CD              F1903
                                       PIC  X(00001).
                           25  W1C40541-R-FORCE-DENY-CD                 F1914
                                       PIC  X(00001).
                           25  W1C40541-R-EXC-EOB-ADJUD-CD              F1901
                                       PIC  X(00004).
                           25  W1C40541-R-CLM-ADJUD-REJ-CD              F4453
                                       PIC  X(00003).
                           25  W1C40541-C-POST-APPL-CD                  F0155
                                       PIC  X(00001).
                           25  W1C40541-C-EXC-OVRRD-IND                 F1165
                                       PIC  X(00001).
                           25  W1C40541-C-EXC-COMP-IND                  F4653
                                       PIC  X(00001).
                           25  W1C40541-C-EXC-DEP-IND                   F2355
                                       PIC  X(00001).
                   15  W1C40541-C-HDR-PARAM-ERR-VW.                     S9202
                       20  W1C40541-C-ERR-PARAM-CD                      F0999
                                       PIC  X(00004).
                       20  W1C40541-C-ERR-PROG-ID                       F1000
                                       PIC  X(00008).
                       20  W1C40541-C-ERR-MSG-DAT                       F0998
                                       PIC  X(00010).
                   15  W1C40541-C-HDR-ADJ-VD-VW.                        S3518
                       20  W1C40541-C-KEYED-REPLCD-NUM                  F1068
                                       PIC S9(00017) COMP-3.
                       20  W1C40541-C-HDR-ADJ-RSN-CD                    F0961
                                       PIC  X(00003).
                       20  W1C40541-C-REPLCD-TCN-NUM                    F0701
                                       PIC S9(00017) COMP-3.
                       20  W1C40541-C-HDR-ADJ-STAT-CD                   F0962
                                       PIC  X(00001).
                       20  W1C40541-C-REPLCMT-TCN-NUM                   F0702
                                       PIC S9(00017) COMP-3.
                       20  W1C40541-F-FCN-DT                            F5689
                                       PIC  X(00010).
                       20  W1C40541-F-FCN-MED-CD                        F7607
                                       PIC  X(00001).
                       20  W1C40541-F-FCN-NUM                           F6613
                                       PIC S9(00009) COMP.
                       20  W1C40541-C-ADJ-ORIG-TCN-NUM                  F0960
                                       PIC S9(00017) COMP-3.
                       20  W1C40541-C-ADJ-SEQ-NUM                       F0708
                                       PIC S9(00004) COMP.
                   15  W1C40541-C-HDR-COE-GRP.                          S1448
                       20  W1C40541-C-HDR-COE-VW                        S9079
                           OCCURS 0002 TIMES
                           INDEXED BY W1C40541-C-HDR-COE-VW-X .
                           25  W1C40541-C-SEQ-NUM                       F1161
                                       PIC S9(00004) COMP.
                           25  W1C40541-R-PLAN-ID                       F0097
                                       PIC  X(00003).
                           25  W1C40541-B-COE-COA-CD                    F2678
                                       PIC  X(00003).
                           25  W1C40541-B-FED-MATCH-CD                  F2671
                                       PIC  X(00001).
                           25  W1C40541-B-FED-CAT-CD                    F2672
                                       PIC  X(00001).
                           25  W1C40541-B-MONEY-CD                      F2673
                                       PIC  X(00001).
                           25  W1C40541-B-AID-CTGRY-CD                  F9887
                                       PIC  X(00005).
                           25  W1C40541-B-ELIG-BEG-ACTN-CD              F1376
                                       PIC  X(00002).
                           25  W1C40541-B-ELIG-END-ACTN-CD              F4270
                                       PIC  X(00002).
                           25  W1C40541-B-LCL-WLFR-OFC-NUM              F3009
                                       PIC S9(00004) COMP.
                           25  W1C40541-B-CASE-ID                       F8742
                                       PIC  X(00009).
                           25  W1C40541-B-FILLER-TX                     F1818
                                       PIC  X(00007).
               10  W1C40541-C-HDR-WARRANT-VW.                           S4552
                   15  W1C40541-C-HDR-RA-NUM                            F1042
                                       PIC S9(00009) COMP.
                   15  W1C40541-C-HDR-WARR-NUM                          F1041
                                       PIC  X(00011).
                   15  W1C40541-C-HDR-WARR-MED-CD                       F1040
                                       PIC  X(00001).
                   15  W1C40541-C-HDR-WARR-AMT                          F1038
                                       PIC S9(00013)V9(00002) COMP-3.
                   15  W1C40541-C-HDR-WARR-DT                           F1039
                                       PIC  X(00010).
               10  W1C40541-C-HDR-DRUG-VW.                              S4813
                   15  W1C40541-C-DAW-CD                                F0246
                                       PIC  X(00001).
                   15  W1C40541-C-SUBM-INCNTVE-AMT                      F3680
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-SUBM-PCT-RT-AMT                       F6661
                                       PIC S9(00005)V9(00002) COMP-3.
                   15  W1C40541-C-SUBM-PCT-TX-AMT                       F8967
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-SUBM-PCT-TX-CD                        F2890
                                       PIC  X(00002).
                   15  W1C40541-C-QTY-PRSC-AMT                          F2956
                                       PIC S9(00008)V9(00003) COMP-3.
                   15  W1C40541-C-ELIG-OVRRD-IND                        F0882
                                       PIC  X(00001).
                   15  W1C40541-C-MGMT-OVRRD-IND                        F1112
                                       PIC  X(00001).
                   15  W1C40541-C-UNIT-DOSE-CD                          F3575
                                       PIC S9(00001) COMP.
                   15  W1C40541-C-DSG-FM-DESC-CD                        F7054
                                       PIC  X(00002).
                   15  W1C40541-C-DISP-UNT-FM-CD                        F8811
                                       PIC  X(00001).
                   15  W1C40541-C-RT-OF-ADMIN-CD                        F9670
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-HDR-DRUG-XREF-CD                      F0992
                                       PIC  X(00015).
                   15  W1C40541-C-ASOC-RX-SVC-NUM                       F1211
                                       PIC S9(00012) COMP-3.
                   15  W1C40541-C-ASOC-RX-SVC-DT                        F1231
                                       PIC  X(00010).
                   15  W1C40541-C-INT-DISP-QTY-AMT                      F6059
                                       PIC S9(00008)V9(00003) COMP-3.
                   15  W1C40541-C-INT-DISP-DAY-NUM                      F2263
                                       PIC S9(00003) COMP.
                   15  W1C40541-C-ORG-PROD-SVC-CD                       F6580
                                       PIC  X(00002).
                   15  W1C40541-C-ORG-PROD-SVC-ID                       F5475
                                       PIC  X(00019).
                   15  W1C40541-C-ORG-QTY-PRSC-AMT                      F9273
                                       PIC S9(00008)V9(00003) COMP-3.
                   15  W1C40541-C-REFIL-AUTH-NUM                        F0851
                                       PIC S9(00002) COMP-3.
                   15  W1C40541-C-SUBM-INGRED-AMT                       F1171
                                       PIC S9(00009)V9(00002) COMP-3.
                   15  W1C40541-C-DRUG-VERSN-NUM                        F0880
                                       PIC  X(00002).
                   15  W1C40541-C-DRUG-ENTRD-TM                         F0878
                                       PIC  X(00008).
                   15  W1C40541-C-DRUG-PAYEE-CD                         F0857
                                       PIC  X(00001).
                   15  W1C40541-C-DRUG-PROV-ST-CD                       F0865
                                       PIC  X(00002).
                   15  W1C40541-C-DRUG-CNTY-CD                          F0863
                                       PIC  X(00003).
                   15  W1C40541-C-DRUG-ZIP-NUM                          F0866
                                       PIC S9(00009) COMP-3.
                   15  W1C40541-C-DRUG-NETWRK-CD                        F0850
                                       PIC  X(00006).
                   15  W1C40541-C-EMPL-LAST-NAM                         F0837
                                       PIC  X(00020).
                   15  W1C40541-C-EMPL-FST-NAM                          F0836
                                       PIC  X(00012).
                   15  W1C40541-C-EMPL-MI-NAM                           F0835
                                       PIC  X(00001).
                   15  W1C40541-C-DRUG-DEPT-CD                          F0830
                                       PIC  X(00030).
                   15  W1C40541-C-DRUG-CVRG-CD                          F0828
                                       PIC  X(00001).
                   15  W1C40541-C-RECIP-CNTY-CD                         F0869
                                       PIC  X(00003).
                   15  W1C40541-C-RECIP-ZIP-CD                          F0870
                                       PIC  X(00020).
                   15  W1C40541-C-DRUG-RPT-CLM-IND                      F0873
                                       PIC  X(00001).
                   15  W1C40541-P-PHARM-ALT-ID-CD                       F6011
                                       PIC  X(00002).
                   15  W1C40541-P-PHARM-ALT-ID                          F6862
                                       PIC  X(00015).
                   15  W1C40541-C-PRSC-LIC-NUM                          F1145
                                       PIC  X(00015).
                   15  W1C40541-C-DRUG-DSP-FEE-PCT                      F0833
                                       PIC S9(00003)V9(00002) COMP-3.
                   15  W1C40541-C-DRUG-COPAY-PCT                        F0827
                                       PIC S9(00003)V9(00002) COMP-3.
                   15  W1C40541-C-SUBM-TAX-AMT                          F0876
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-DRUG-IFACE-TY-CD                      F0847
                                       PIC  X(00001).
                   15  W1C40541-C-IFACE-CLM-ID                          F0846
                                       PIC  X(00030).
                   15  W1C40541-C-IFACE-CLM-CR-CD                       F0845
                                       PIC  X(00017).
                   15  W1C40541-C-FCN-NUM                               F0822
                                       PIC S9(00009) COMP-3.
                   15  W1C40541-C-PROV-ON-REVW-IND                      F0867
                                       PIC  X(00001).
                   15  W1C40541-C-DRUG-TPL-IND                          F0879
                                       PIC  X(00001).
                   15  W1C40541-C-DRUG-GROSS-AMT                        F0843
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-RX-ORGN-CD                            F3422
                                       PIC 9(00001) COMP.
                   15  W1C40541-C-LVL-OF-SVC-CD                         F4468
                                       PIC 9(00002) COMP.
                   15  W1C40541-C-LVL-OF-SVC-CD-X REDEFINES             F4468
                       W1C40541-C-LVL-OF-SVC-CD
                                       PIC X(02).
                   15  W1C40541-C-CLNC-ID                               F1566
                                       PIC  X(00006).
                   15  W1C40541-C-BAS-DAYS-SPLY-CD                      F0953
                                       PIC  X(00001).
                   15  W1C40541-C-SUBM-PRO-FEE-AMT                      F5489
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-SUBM-DSP-FEE-AMT                      F3206
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-PAT-DAW-DIF-AMT                       F1136
                                       PIC S9(00009)V9(00002) COMP-3.
                   15  W1C40541-C-UC-CHARGE-AMT                         F4761
                                       PIC S9(00007)V9(00002) COMP-3.
                   15  W1C40541-C-UNIT-OF-MSR-CD                        F0242
                                       PIC  X(00002).
                   15  W1C40541-P-SFWR-CERT-ID                          F8147
                                       PIC  X(00010).
                   15  W1C40541-C-INQ-STAT-CD                           F2113
                                       PIC  X(00001).
                   15  W1C40541-C-SUBM-A-ID                             F8611
                                       PIC S9(00011) COMP-3.
                   15  W1C40541-P-MEDICAID-NUM                          F0457
                                       PIC  X(00015).
                   15  W1C40541-P-NCPDP-NUM                             F5493
                                       PIC  X(00015).
                   15  W1C40541-C-REFLL-DUE-DT                          F1573
                                       PIC  X(00010).
                   15  W1C40541-C-CNSM-MED-QTY-AMT                      F2285
                                       PIC S9(00008)V9(00003) COMP-3.
                   15  W1C40541-C-NXT-ALLOW-FIL-DT                      F1385
                                       PIC  X(00010).
                   15  W1C40541-C-MCO-PROV-ID                           F2287
                                       PIC  X(00015).
                   15  W1C40541-C-PCC-PROV-ID                           F0176
                                       PIC  X(00015).
                   15  W1C40541-C-MBHP-PROV-ID                          F2288
                                       PIC  X(00015).
                   15  W1C40541-C-CSMP-PROV-ID                          F7237
                                       PIC  X(00015).
                   15  W1C40541-C-AFFL-USED-IND                         F0256
                                       PIC  X(00001).
               10  W1C40541-C-HDR-RLTD-HIST-GRP.                        S5403
                   15  W1C40541-C-HDR-RLTD-HIST-VW                      S5582
                           OCCURS 0100 TIMES                            CR3837
                           INDEXED BY W1C40541-C-HDR-RLTD-HIST-VW-X .
                       20  W1C40541-C-LI-NUM                            F1073
                                       PIC S9(00003) COMP.
                       20  W1C40541-C-RLTD-LI-TCN-NUM                   F0721
                                       PIC S9(00017) COMP-3.
                       20  W1C40541-C-RLTD-LI-NUM                       F0720
                                       PIC S9(00003) COMP.
                       20  W1C40542-R-CLM-EXC-CD                        F1737
                                       PIC  X(00004).
                       20  W1C40542-R-RSN-FOR-SVC-CD                    F8605
                                       PIC  X(00002).
                       20  W1C40542-C-HDR-PD-DT                         F1017
                                       PIC  X(00010).
                       20  W1C40542-C-EXC-STAT-CD                       F4200
                                       PIC  X(00001).
                       20  W1C40542-C-HDR-TY-CD                         F1031
                                       PIC  X(00001).
                       20  W1C40541-C-SYS-REPLCD-IND                    F6886
                                       PIC  X(00001).
                       20  W1C40542-R-CLM-ADJUD-REJ-CD                  F4453
                                       PIC  X(00003).
                       20  W1C40541-C-POST-APPL-CD                      F0155
                                       PIC  X(00001).
               10  W1C40541-C-HDR-DUR-PPS-GRP.                          S2972
                   15  W1C40541-C-HDR-DUR-PPS-VW                        S3073
                           OCCURS 0009 TIMES
                           INDEXED BY W1C40541-C-HDR-DUR-PPS-VW-X .
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-R-RSN-FOR-SVC-CD                    F8605
                                       PIC  X(00002).
                       20  W1C40541-C-DUR-PPS-CD                        F0834
                                       PIC  X(00002).
                       20  W1C40541-C-DUR-RSLT-SVC-CD                   F0881
                                       PIC  X(00002).
                       20  W1C40541-C-DUR-LVL-EFRT-CD                   F4634
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-DUR-LVL-EFRT-CD-X REDEFINES       F4634
                           W1C40541-C-DUR-LVL-EFRT-CD
                                       PIC  X(00002).
                       20  W1C40541-C-DUR-CO-AGT-ID-CD                  F4197
                                       PIC  X(00002).
                       20  W1C40541-C-DUR-CO-AGT-ID                     F5094
                                       PIC  X(00019).
               10  W1C40541-C-HDR-CLINICAL-GRP.                         S1040
                   15  W1C40541-C-HDR-CLINICAL-VW                       S0663
                           OCCURS 0005 TIMES
                           INDEXED BY W1C40541-C-HDR-CLINICAL-VW-X .
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CLIN-MSRMT-DT                     F8172
                                       PIC  X(00010).
                       20  W1C40541-C-CLIN-MSRMT-TM                     F4597
                                       PIC  X(00008).
                       20  W1C40541-C-CLIN-MSRMT-DM-CD                  F2649
                                       PIC  X(00002).
                       20  W1C40541-C-CLIN-MSRMT-UN-CD                  F5234
                                       PIC  X(00002).
                       20  W1C40541-C-CLIN-MSRMT-DESC                   F3011
                                       PIC  X(00015).
               10  W1C40541-C-HDR-DIAG-GRP.                             S4687
                   15  W1C40541-C-HDR-DIAG-VW                           S0860
                           OCCURS 0005 TIMES
                           INDEXED BY W1C40541-C-HDR-DIAG-VW-X .
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-DIAG-QL-CD                        F1284
                                       PIC  X(00002).
                       20  W1C40541-R-DIAG-CD                           F1756
                                       PIC  X(00015).
               10  W1C40541-C-HDR-OTHR-AMT-GRP.                         S8723
                   15  W1C40541-C-HDR-OTHR-AMT-VW                       S6752
                           OCCURS 0003 TIMES
                           INDEXED BY W1C40541-C-HDR-OTHR-AMT-VW-X .
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-SUBM-OTHR-AMT-CD                  F7805
                                       PIC  X(00002).
                       20  W1C40541-C-SUBM-OTHR-AMT                     F6786
                                       PIC S9(00007)V9(00002) COMP-3.
               10  W1C40541-C-HDR-COB-GRP.                              S5672
                   15  W1C40541-C-HDR-COB-VW                            S5353
                           OCCURS 0009 TIMES
                           INDEXED BY W1C40541-C-HDR-COB-VW-X .
                       20  W1C40541-C-COB-SEQ-NUM                       F7302
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-PYR-HIERARCHY-CD                  F7781
                                       PIC  X(00002).
                       20  W1C40541-C-PAYERID-CD                        F2337
                                       PIC  X(00002).
                       20  W1C40541-C-PAYER-ID                          F0245
                                       PIC  X(00010).
                       20  W1C40541-C-PAYERID-DT                        F8615
                                       PIC  X(00010).
                       20  W1C40541-C-CNTRL-NUM                         F4598
                                       PIC  X(00030).
                       20  W1C40541-C-COB-INSR-IND                      F6989
                                       PIC S9(00002) COMP.
                       20  W1C40541-C-CNT-COB-PAID-NUM                  F5749
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-COB-REJ-NUM                   F8574
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-COB-PAT-NUM                   F0532
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-CNT-COB-BENE-NUM                  F2369
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-HDR-COB-PAID-GRP.                 S4835
                           25  W1C40541-C-HDR-COB-PAID-VW               S8184
                           OCCURS 0009 TIMES
                           INDEXED BY W1C40541-C-HDR-COB-PAID-VW-X .
                               30  W1C40542-C-COB-SEQ-NUM               F7302
                                       PIC S9(00004) COMP.
                               30  W1C40541-C-COB-PAID-SEQ-NUM          F7096
                                       PIC S9(00004) COMP.
                               30  W1C40541-C-PAYERID-PD-CD             F8101
                                       PIC  X(00002).
                               30  W1C40541-C-PAYERID-PD-AMT            F1235
                                       PIC S9(00007)V9(00002) COMP-3.
                       20  W1C40541-C-HDR-COB-REJ-GRP.                  S1686
                           25  W1C40541-C-HDR-COB-REJ-VW                S2722
                           OCCURS 0025 TIMES
                           INDEXED BY W1C40541-C-HDR-COB-REJ-VW-X .
                               30  W1C40543-C-COB-SEQ-NUM               F7302
                                       PIC S9(00004) COMP.
                               30  W1C40541-C-COB-REJ-SEQ-NUM           F8086
                                       PIC S9(00004) COMP.
                               30  W1C40541-C-PAYERID-REJ-CD            F4717
                                       PIC  X(00003).
                       20  W1C40541-C-OTHR-PYR-PAT-GRP.                 S7919
                           25  W1C40541-C-COB-PAT-NUM                   F0893
                                       PIC S9(00002) COMP.
                           25  W1C40541-C-OTHR-PYR-PAT-VW               S9055
                           OCCURS 0025 TIMES
                           INDEXED BY W1C40541-C-OTHR-PYR-PAT-VW-X .
                               30  W1C40544-C-COB-SEQ-NUM               F7302
                                       PIC S9(00004) COMP.
                               30  W1C40541-C-OTHR-PAT-SEQ-NUM          F2340
                                       PIC S9(00002) COMP.
                               30  W1C40541-C-COB-PAT-CD                F2311
                                       PIC  X(00002).
                               30  W1C40541-C-COB-PAT-AMT               F0576
                                       PIC S9(00008)V9(00002) COMP-3.
                       20  W1C40541-C-BENE-STG-GRP.                     S9194
                           25  W1C40541-C-BENE-STG-VW                   S0302
                           OCCURS 0004 TIMES
                           INDEXED BY W1C40541-C-BENE-STG-VW-X .
                               30  W1C40545-C-COB-SEQ-NUM               F7302
                                       PIC S9(00004) COMP.
                               30  W1C40541-C-BENE-STG-SEQ-NUM          F1402
                                       PIC S9(00002) COMP.
                               30  W1C40541-C-BENE-CNT-NUM              F2603
                                       PIC S9(00001) COMP.
                               30  W1C40541-C-BENE-CD                   F5338
                                       PIC  X(00002).
                               30  W1C40541-C-BENE-AMT                  F6504
                                       PIC S9(00006)V9(00002) COMP-3.
               10  W1C40541-C-HDR-PROC-MOD-GRP.                         S9036
                   15  W1C40541-C-HDR-PROC-MOD-VW                       S4039
                           OCCURS 0010 TIMES
                           INDEXED BY W1C40541-C-HDR-PROC-MOD-VW-X .
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-R-PROC-MOD-CD                       F0139
                                       PIC  X(00002).
               10  W1C40541-C-LI-BSE-CHG-PHRM-GRP.                      S4124
                   15  W1C40541-C-LI-BSE-CHG-VW                         S9244
                           OCCURS 0008 TIMES
                           INDEXED BY W1C40541-C-LI-BSE-CHG-VW-X .
                       20  W1C40541-C-LI-NUM                            F1073
                                       PIC S9(00003) COMP.
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-BSE-CHNG-RSN-CD                   F0737
                                       PIC  X(00002).
                       20  W1C40541-C-BSE-AMT-CHG-AMT                   F0736
                                       PIC S9(00007)V9(00002) COMP-3.
               10  W1C40541-C-LI-TPL-BLNG-GRP.                          S6662
                   15  W1C40541-C-LI-TPL-BLNG-VW                        S3551
                           OCCURS 0006 TIMES
                           INDEXED BY W1C40541-C-LI-TPL-BLNG-VW-X .
                       20  W1C40541-C-LI-NUM                            F1073
                                       PIC S9(00003) COMP.
                       20  W1C40541-T-PLCY-NUM                          F2561
                                       PIC  X(00020).
                       20  W1C40541-T-PLCY-SEQ-NUM                      F2588
                                       PIC S9(00003) COMP.
                       20  W1C40541-T-PLCY-RESRC-CD                     F2587
                                       PIC  X(00002).
                       20  W1C40541-T-CARR-ID                           F2459
                                       PIC  X(00010).
                       20  W1C40541-T-CVRG-PLCY-CD                      F2558
                                       PIC  X(00002).
               10  W1C40541-C-HDR-OVRRD-EOB-P-GRP.                      S7918
                   15  W1C40541-C-HDR-OVRRD-EOB-VW                      S4955
                           OCCURS 0020 TIMES
                           INDEXED BY W1C40541-C-HDR-OVRRD-EOB-VW-X .
                       20  W1C40541-C-LI-NUM                            F1073
                                       PIC S9(00003) COMP.
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-OVRRD-EOB-CD                      F1128
                                       PIC  X(00004).
                       20  W1C40541-C-OVRRD-EOB-ID                      F1129
                                       PIC  X(00007).
               10  W1C40541-C-HDR-OVRRD-EXC-P-GRP.                      S7413
                   15  W1C40541-C-HDR-OVRRD-EXC-VW                      S1683
                           OCCURS 0020 TIMES
                           INDEXED BY W1C40541-C-HDR-OVRRD-EXC-VW-X .
                       20  W1C40541-C-LI-NUM                            F1073
                                       PIC S9(00003) COMP.
                       20  W1C40541-C-SEQ-NUM                           F1161
                                       PIC S9(00004) COMP.
                       20  W1C40541-C-OVRRD-EXC-CD                      F1130
                                       PIC  X(00004).
                       20  W1C40541-C-OVRRD-EXC-ID                      F1131
                                       PIC  X(00007).
               10  W1C40541-C-HDR-D0-MISC-FLDS-VW.                      S7050
                   15  W1C40542-C-RX-NUM-CD                             F5139
                                       PIC  X(00001).
                   15  W1C40541-C-PMOD-NUM                              F8902
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-COB-ID-NUM                            F0213
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-OTH-AMT-NUM                           F7313
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-CMPD-NUM                              F0521
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-DIAG-NUM                              F0248
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-DUR-PPS-NUM                           F0502
                                       PIC S9(00001) COMP.
                   15  W1C40541-C-CLIN-INF-NUM                          F1701
                                       PIC S9(00001) COMP.
                   15  W1C40541-C-COB-PAID-NUM                          F6067
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-COB-REJ-NUM                           F6219
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-B-STR-AD                              F6292
                                       PIC  X(00030).
                   15  W1C40541-C-B-CITY-NAM                            F1554
                                       PIC  X(00020).
                   15  W1C40541-C-B-ST-CD                               F2295
                                       PIC  X(00002).
                   15  W1C40541-C-PAT-PHON-NUM                          F6134
                                       PIC  X(00010).
                   15  W1C40541-C-PAT-EMAIL-TX                          F9668
                                       PIC  X(00080).
                   15  W1C40541-C-INS-FST-NAM                           F6861
                                       PIC  X(00012).
                   15  W1C40541-C-INS-LAST-NAM                          F0897
                                       PIC  X(00015).
                   15  W1C40541-C-EMPLR-ID                              F0891
                                       PIC  X(00015).
                   15  W1C40541-C-EMPLR-NAM                             F6511
                                       PIC  X(00030).
                   15  W1C40541-C-EMPLR-STR-AD                          F2309
                                       PIC  X(00030).
                   15  W1C40541-C-EMPLR-CITY-NAM                        F6700
                                       PIC  X(00020).
                   15  W1C40541-C-EMPLR-ST-CD                           F0199
                                       PIC  X(00002).
                   15  W1C40541-C-EMPLR-ZIP-CD                          F3174
                                       PIC  X(00015).
                   15  W1C40541-C-EMPLR-PHON-NUM                        F0187
                                       PIC  X(00010).
                   15  W1C40541-C-EMPLR-CNTCT-NAM                       F8163
                                       PIC  X(00030).
                   15  W1C40541-C-SMKR-IND                              F0977
                                       PIC  X(00001).
                   15  W1C40541-C-SUBM-GROUP-ID                         F8121
                                       PIC  X(00015).
                   15  W1C40541-C-MGAP-ID                               F5519
                                       PIC  X(00020).
                   15  W1C40541-C-MCAID-IND                             F8743
                                       PIC  X(00002).
                   15  W1C40541-C-PRV-ASGN-IND                          F9348
                                       PIC  X(00001).
                   15  W1C40541-C-CMS-FAC-IND                           F1386
                                       PIC  X(00001).
                   15  W1C40541-C-MCAID-ID                              F8438
                                       PIC  X(00020).
                   15  W1C40541-C-MCAID-AGCY-NUM                        F1098
                                       PIC  X(00015).
                   15  W1C40541-C-MCAID-CTL-ID                          F7608
                                       PIC  X(00020).
                   15  W1C40541-C-MCAID-PD-AMT                          F1109
                                       PIC S9(00006)V9(00002) COMP-3.
                   15  W1C40541-C-CLM-ALT-ID                            F2928
                                       PIC  X(00020).
                   15  W1C40541-C-SCHED-RX-NUM                          F0614
                                       PIC  X(00012).
                   15  W1C40541-C-DLY-RSN-CD                            F0892
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-TRNS-REF-NUM                          F2334
                                       PIC  X(00010).
                   15  W1C40541-C-PAT-ASGN-IND                          F3253
                                       PIC  X(00001).
                   15  W1C40541-C-RT-ADMIN-DAT                          F8901
                                       PIC  X(00011).
                   15  W1C40541-C-CMPD-TY-CD                            F1417
                                       PIC  X(00002).
                   15  W1C40541-C-PRSC-LOC-CD                           F0592
                                       PIC  X(00003).
                   15  W1C40541-C-PRSC-LST-NAM                          F1593
                                       PIC  X(00015).
                   15  W1C40541-C-PRSC-FST-NAM                          F2310
                                       PIC  X(00010).
                   15  W1C40541-C-PRSC-STR-AD                           F8665
                                       PIC  X(00030).
                   15  W1C40541-C-PRSC-CITY-NAM                         F1062
                                       PIC  X(00020).
                   15  W1C40541-C-PRSC-ST-CD                            F5097
                                       PIC  X(00002).
                   15  W1C40541-C-PRSC-ZIP-CD                           F0211
                                       PIC  X(00015).
                   15  W1C40541-C-PRSC-PHON-NUM                         F2702
                                       PIC  X(00010).
                   15  W1C40541-C-OTHPYR-BIN-NUM                        F7507
                                       PIC S9(00006) COMP.
                   15  W1C40541-C-OTHPYR-PCN-NUM                        F2333
                                       PIC  X(00010).
                   15  W1C40541-C-OTHPYR-CRD-ID                         F0896
                                       PIC  X(00020).
                   15  W1C40541-C-OTHPYR-GRP-ID                         F3188
                                       PIC  X(00015).
                   15  W1C40541-C-CARR-ID                               F8769
                                       PIC  X(00010).
                   15  W1C40541-C-BLLENT-TY-CD                          F2308
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-PAY-TO-QL-CD                          F5695
                                       PIC  X(00002).
                   15  W1C40541-C-PAY-TO-ID                             F2307
                                       PIC  X(00015).
                   15  W1C40541-C-PAY-TO-NAM                            F4060
                                       PIC  X(00020).
                   15  W1C40541-C-PAY-TO-STR-AD                         F2306
                                       PIC  X(00030).
                   15  W1C40541-C-PAY-TO-CITY-NAM                       F6226
                                       PIC  X(00020).
                   15  W1C40541-C-PAY-TO-ST-CD                          F2305
                                       PIC  X(00002).
                   15  W1C40541-C-PAY-TO-ZIP-CD                         F6189
                                       PIC  X(00015).
                   15  W1C40541-C-GNREQ-TY-CD                           F2304
                                       PIC  X(00002).
                   15  W1C40541-C-GNREQ-PRD-ID                          F2302
                                       PIC  X(00019).
                   15  W1C40541-C-CPN-TY-CD                             F5906
                                       PIC  X(00002).
                   15  W1C40541-C-CPN-NUM                               F3362
                                       PIC  X(00015).
                   15  W1C40541-C-CPN-VAL-AMT                           F0186
                                       PIC S9(00006)V9(00002) COMP-3.
                   15  W1C40541-C-PA-REQ-CD                             F0375
                                       PIC  X(00001).
                   15  W1C40541-C-PA-REP-FST-NAM                        F2400
                                       PIC  X(00012).
                   15  W1C40541-C-PA-REP-LST-NAM                        F5407
                                       PIC  X(00015).
                   15  W1C40541-C-PA-REP-STR-AD                         F5460
                                       PIC  X(00030).
                   15  W1C40541-C-PA-REP-CITY-NAM                       F8431
                                       PIC  X(00020).
                   15  W1C40541-C-PA-REP-ST-CD                          F7716
                                       PIC  X(00002).
                   15  W1C40541-C-PA-REP-ZIP-CD                         F2298
                                       PIC  X(00015).
                   15  W1C40541-C-ADDC-TY-CD                            F8879
                                       PIC  X(00003).
                   15  W1C40541-C-ADDC-REQ-DT                           F0355
                                       PIC  X(00010).
                   15  W1C40541-C-ADDC-REV-DT                           F2481
                                       PIC  X(00010).
                   15  W1C40541-C-ADDC-ST-IND                           F5903
                                       PIC  X(00001).
                   15  W1C40541-C-ADDC-LEN-CD                           F2688
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-ADDC-LEN-NUM                          F1061
                                       PIC S9(00003) COMP.
                   15  W1C40541-C-ADDC-PRSC-DT                          F2297
                                       PIC  X(00010).
                   15  W1C40541-C-ADDC-SUP-DOC-TX                       F7190
                                       PIC  X(00065).
                   15  W1C40541-C-ADDC-QST-CNT-NUM                      F2313
                                       PIC S9(00002) COMP.
                   15  W1C40541-C-ADDC-QST-NUM                          F1216
                                       PIC  X(00003).
                   15  W1C40541-C-ADDC-QST-PCT                          F2426
                                       PIC S9(00003)V9(00002) COMP-3.
                   15  W1C40541-C-ADDC-QST-DT                           F4512
                                       PIC  X(00010).
                   15  W1C40541-C-ADDC-QST-AMT                          F1591
                                       PIC S9(00009)V9(00002) COMP-3.
                   15  W1C40541-C-ADDC-QST-RSP-NUM                      F9254
                                       PIC 9(00011) COMP-3.
                   15  W1C40541-C-ADDC-QST-TX                           F9086
                                       PIC  X(00030).
                   15  W1C40541-C-FACI-ID                               F2296
                                       PIC  X(00010).
                   15  W1C40541-C-FACI-NAM                              F7498
                                       PIC  X(00030).
                   15  W1C40541-C-FACI-STR-AD                           F2686
                                       PIC  X(00030).
                   15  W1C40541-C-FACI-CITY-NAM                         F2572
                                       PIC  X(00020).
                   15  W1C40541-C-FACI-ST-CD                            F1215
                                       PIC  X(00002).
                   15  W1C40541-C-FACI-ZIP-CD                           F5351
                                       PIC  X(00015).
                   15  W1C40541-C-NARR-MSG-TX                           F0517
                                       PIC  X(00200).
                   15  W1C40541-C-APP-MSG-CD                            F5655
                                       PIC  X(00003).
                   15  W1C40541-C-LTC-IND                               F2348
                                       PIC  X(00001).
                   15  W1C40541-C-TOT-PAT-RESP-AMT                      F8903
                                       PIC S9(00008)V9(00002) COMP-3.
                   15  W1C40541-P-CERT-SPECL-CD                         F0021
                                       PIC  X(00003).
                   15  W1C40541-C-DRUG-FFP-CD                           F1614
                                       PIC  X(00002).
                   15  W1C40541-C-CMPD-PRD-SVC-CD                       F4341
                                       PIC  X(00002).
                   15  W1C40541-C-CMPD-PRD-SVC-ID                       F2370
                                       PIC  X(00019).
                   15  W1C40541-C-SUBM-GENDER-CD                        F9828
                                       PIC  X(00001).
                   15  W1C40541-C-SUBM-DOB-DT                           F2374
                                       PIC  X(00010).
                   15  W1C40541-C-PRSC-DEA-ID-CD                        F9344
                                       PIC  X(00002).
                   15  W1C40541-C-PRSC-DEA-ID                           F2620
                                       PIC  X(00015).
               10  W1C40541-C-HCPCS-FLDS-VW.                            S7627
                   15  W1C40541-C-HCPCS-LI-NUM                          F7662
                                       PIC S9(00003) COMP.
                   15  W1C40541-R-HCPCS-CD                              F1144
                                       PIC  X(00005).
                   15  W1C40541-R-HCPCS-SEQ-NUM                         F0901
                                       PIC S9(00005) COMP.
                   15  W1C40541-R-HCPCS-REDEF-TX                        F8904
                                       PIC  X(00005).
                   15  W1C40541-R-HCPCS-MOD-CD                          F9638
                                       PIC  X(00002).
                   15  W1C40541-R-HCPCS-REC-ID-CD                       F6428
                                       PIC  X(00001).
                   15  W1C40541-R-HCPCS-SHORT-DESC                      F2349
                                       PIC  X(00028).
                   15  W1C40541-R-HCPCS-CIM-1ST-ID                      F0582
                                       PIC  X(00006).
                   15  W1C40541-R-HCPCS-MCM-1ST-ID                      F1240
                                       PIC  X(00008).
                   15  W1C40541-R-HCPCS-LAB-1ST-CD                      F2902
                                       PIC  X(00003).
                   15  W1C40541-R-HCPCS-XREF-1-CD                       F4214
                                       PIC  X(00005).
                   15  W1C40541-R-HCPCS-CVRG-CD                         F2350
                                       PIC  X(00001).
                   15  W1C40541-R-HCPCS-MOG-TX                          F6863
                                       PIC  X(00003).
                   15  W1C40541-R-HCPCS-MOG-IND                         F1146
                                       PIC  X(00001).
                   15  W1C40541-R-HCPCS-MOG-DT                          F0660
                                       PIC  X(00010).
                   15  W1C40541-R-HCPCS-PROC-NUM                        F1399
                                       PIC  X(00004).
                   15  W1C40541-R-HCPCS-SVC-1ST-CD                      F0522
                                       PIC  X(00001).
                   15  W1C40541-R-HCPCS-CD-ADD-DT                       F1147
                                       PIC  X(00010).
                   15  W1C40541-R-HCPCS-ACTN-DT                         F1400
                                       PIC  X(00010).
                   15  W1C40541-R-HCPCS-TERM-DT                         F0214
                                       PIC  X(00010).
                   15  W1C40541-R-HCPCS-ACTN-CD                         F5922
                                       PIC  X(00001).
                                       
               10  W1C40541-C-LI-RUL-NUM-GRP.                           CR3837
                   15  W1C40541-C-LI-RUL-NUM-VW                         CR3837
                           OCCURS 0400 TIMES 				CR3837
                           INDEXED BY W1C40541-C-LI-RUL-NUM-VW-X. 	CR3837
                       20  W1C40541-C-LI-NUM                            CR3837
                                       PIC S9(00004) COMP.              CR3837
                       20  W1C40541-C-SPA-RUL-SEQ-NUM                   CR3837
                                       PIC S9(00004) COMP.		CR3837
                       20  W1C40541-C-SPA-RUL-NUM                       CR3837
                                       PIC  9(00006).			CR3837
      
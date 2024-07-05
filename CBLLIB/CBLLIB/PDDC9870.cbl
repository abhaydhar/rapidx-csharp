000010 IDENTIFICATION DIVISION.                                         00001000
000020 PROGRAM-ID.    PDDC9870.                                         00002000
000030 AUTHOR.        ACS GOVERMENT HEALTHCARE.                         00003000
000040 DATE-COMPILED.                                                   00004000
000050*REMARKS.                                                         00005000
000060***************************************************************** 00006000
000070*                                                               * 00007000
000080*@ PROGRAM-ID: PDDC9870                                         * 00008000
000090*@ TITLE: CONVERT  LAYOUT TO CLAIM  LAYOUT                      * 00009000
000100*                                                               * 00010000
000110***************************************************************** 00011000
000120*                                                               * 00012000
000130*?  I. PROGRAM ABSTRACT                                         * 00013000
000140*+                                                              * 00014000
000150*+  THIS IS THE ONLINE SAMPLE PROGRAM WHICH FORMATS AN CLAIM    * 00015000
000160*+  RECORD FROM THE NCPDP CONSOLIDATED LAYOUT.                  * 00016000
000170*+                                                              * 00017000

000300***************************************************************** 00030000
000310*+                                                              * 00031000
000320*+ II. INPUT/OUTPUT MATRIX                                      * 00032000
000330*+                                                              * 00033000
000340*+     I/O              TITLE                           ID      * 00034000
000350*+     ---   ------------------------------------  ----------   * 00035000
000360*+      I    G_LIST_DTL_TB                         GSLDTLTB     * 00036000
000370*+      I    R_CUST_GRP_XREF_TB                    RCGXRFTB     * 00037000
000380*+      I    R_CUST_DTL_TB                         RCUSTDTB     * 00038000
000390*+                                                              * 00039000
000400***************************************************************** 00040000
000410*+                                                              * 00041000
000420*+III. SECTION DEPENDENCY CHART.                                * 00042000
000430*+                                                              * 00043000
000440***************************************************************** 00044000
000450*                                                               * 00045000
000451*                                                               * 00046000
000460*+ IV. CSR CHANGE LOG.                                          * 00047000
000470*                                                               * 00048000
000480*           -----------  CSR CHANGES  -----------               * 00049000
000490*                                                               * 00050000
000500*  CSR    CHANGE      ANALYST      DESCRIPTION OF CHANGE        * 00051000
000510*  NBR     DATE        NAME                                     * 00052000
000520*                                                               * 00053000
000521*---------------------------------------------------------------* 00054000
      ** CR12914  03/23/22  NAGENDRA  SCHEDULE-II PARTIAL FILL CHANGES *CR12914
      ** MULTRN   05/03/23  MADHURI   UNCOMMNETED THE CODE TO ALLOW    *MULTRN
      *                               MULTIPLE TRANSACTIONS IN A SINGLE*MULTRN
      *                               TRANSMISSION.                    *MULTRN
000820***************************************************************** 00137000
000830                                                                  00138000
000840 ENVIRONMENT DIVISION.                                            00139000
000850 CONFIGURATION SECTION.                                           00140000
000880 DATA DIVISION.                                                   00141000
001897                                                                  00142000
000890 WORKING-STORAGE SECTION.                                         00143000
000900                                                                  00144000
000910 01  WK-002-PARTITION-NUM.                                        00145000
000920     05  WK-002-MASSHE           PIC S9(4)      VALUE 1 COMP.     00146000
           05  WK-EXIST-CHECK          PIC X(01)     VALUE SPACES.      COR0784
000930*    VALUES BELOW MUST BE DETERMINED WHEN IMPLEMENTED             00147000
000960                                                                  00148000
000970 01  WK-030-MISC-CONSTANTS.                                       00149000
000980     05  WK-030-PROGRAM-NAME     PIC X(08)      VALUE 'PDDC0426'. 00150000
000990     05  WK-030-ZERO             PIC 9(01)      VALUE 0.          00151000
001000     05  WK-030-ONE              PIC 9(01)      VALUE 1.          00152000
001010     05  WK-030-CHAR-ONE         PIC X(06)      VALUE '000001'.   00153000
001020     05  WK-030-TWO              PIC 9(01)      VALUE 2.          00154000
001030     05  WK-030-LOC-50           PIC 9(02)      VALUE 50.         00155000
001040     05  WK-030-NUMBER           PIC 9(04)      VALUE 0.          00156000
001050     05  WK-030-TYPE-BAE         PIC X(03)      VALUE 'BAE'.      00157000
001060     05  WK-030-PHARMACY-UNIT-DOSE                                00158000
001070                                 PIC 9(01)      VALUE 3.          00159000
001919     05  WK-030-PARM3201-START-DATE                               00160000
001920                                 PIC X(10)                        00161000
001921                                 VALUE          '0000-00-00'.     00162000
001919     05  WK-030-PARM3201-END-DATE                                 00163000
001920                                 PIC X(10)                        00164000
001921                                 VALUE          '0000-00-00'.     00165000
           05  WK-000-2017-03-31       PIC X(10)  VALUE '2017-03-31'.   COR6916
           05  WH-100-ACCUM-QTY-DISP-NUM   PIC S9(00010)V9(00003) COMP-3 
                                                         VALUE +0.       
           05  WH-100-ACCUM-HIST-COUNT     PIC 9(00010) VALUE  0.        
           05  WH-000-SCC-MATCH-COUNT      PIC 9(00010) VALUE  0.        
           05  WH-100-HISTORY-QTY-PRS-AMT  PIC 9(00008)V9(00003).     
001500 01  WS-000-LI-SUB           PIC S9(05) COMP-3 VALUE +1.		   
001100                                                                  00166000
001110 01  WS-000-SWITCHES.                                             00167000
001120     05  WS-000-COMPOUND-SW      PIC X(01)      VALUE LOW-VALUES. 00168000
001130         88  WS-000-COMPOUND-NO                 VALUE LOW-VALUES. 00169000
001140         88  WS-000-COMPOUND-YES                VALUE HIGH-VALUES.00170000
POSTFE     05  WS-000-EC-IN-PRCS-IS-FATAL-SW PIC X(01) VALUE SPACES.
POSTFE         88  WS-000-EC-IN-PRCS-IS-FATAL          VALUE 'Y'.
POSTFE         88  WS-000-EC-IN-PRCS-NOT-FATAL         VALUE 'N'.
           05  WS-000-3113-POSTED-SW         PIC X(01) VALUE SPACES.
               88  WS-000-3113-POSTED-NO               VALUE 'N'.
               88  WS-000-3113-POSTED-YES              VALUE 'Y'.
           05  WS-000-REASON-FR-SVC-SW       PIC X(01) VALUE SPACES.    COR6916
               88  WS-000-REASON-FR-SVC-SUB            VALUE 'N'.       COR6916
               88  WS-000-REASON-FR-SVC-NOT-SUB        VALUE 'Y'.       COR6916
           05  WS-000-PROF-SVC-SW            PIC X(01) VALUE SPACES.    COR6916
               88  WS-000-PROF-SVC-SUB                 VALUE 'N'.       COR6916
               88  WS-000-PROF-SVC-NOT-SUB             VALUE 'Y'.       COR6916
           05  WS-000-RESULT-OF-SVC-SW       PIC X(01) VALUE SPACES.    COR6916
               88  WS-000-RESULT-OF-SVC-SUB            VALUE 'N'.       COR6916
               88  WS-000-RESULT-OF-SVC-NOT-SUB        VALUE 'Y'.       COR6916
           05  WS-COMP-PREP-FEES-SW          PIC X(01) VALUE SPACES.    COR6916
               88  WS-COMP-PREP-FEES-NOT-FOUND         VALUE 'N'.       COR6916
               88  WS-COMP-PREP-FEES-FOUND             VALUE 'Y'.       COR6916
           05  WS-FIRST-TIME-CMPND-THRU-SW        PIC X(01) VALUE 'Y'.  CR14386
               88  WS-FIRST-TIME-CMPND                      VALUE 'Y'.  CR14386
               88  WS-NOT-FIRST-TIME-CMPND                  VALUE 'N'.  CR14386
009300     05  WS-000-BENEFIT-STAT-FOUND-SW       PIC X(01) VALUE SPACE.
009310         88  WS-000-BENEFIT-STAT-FOUND                VALUE 'Y'.  
009320         88  WS-000-BENEFIT-STAT-NOT-FOUND            VALUE 'N'. 
009220     05  WS-00-REPRICE-CLAIM-SW             PIC X(01) VALUE 'Y'.
009230         88  WS-00-REPRICE-CLAIM-YES                  VALUE 'Y'.
009240         88  WS-00-REPRICE-CLAIM-NO                   VALUE 'N'.  
		   
MDM        05  WS-000-POS-TRAN-CD            PIC X(02) VALUE SPACE.
007490     05  WS-PAYER-SW                     PIC X VALUE 'N'.
007500         88  WS-PAYER-ID-NOT-SENT
007510                     VALUE 'N'.
007520         88  WS-PAYER-ID-SENT
007530                     VALUE 'Y'.
           05  WS-PAYER-CNT-SW                 PIC X VALUE 'N'.         COR3009
               88  WS-PAYER-CNT-NOT-SENT                                COR3009
                           VALUE 'N'.                                   COR3009
               88  WS-PAYER-CNT-SENT                                    COR3009
                           VALUE 'Y'.                                   COR3009
          05   WS-PAYER-PAT-CNT-SW             PIC X VALUE 'N'.         COR3009
               88  WS-PAYER-PAT-CNT-NOT-SENT                            COR3009
                           VALUE 'N'.                                   COR3009
               88  WS-PAYER-PAT-CNT-SENT                                COR3009
                           VALUE 'Y'.                                   COR3009
           05  WS-PAYER03-SW             PIC X VALUE 'N'.               COR3009
               88  WS-PAYER03-ID-NOT-SENT                               COR3009
                           VALUE 'N'.                                   COR3009
               88  WS-PAYER03-ID-SENT                                   COR3009
                           VALUE 'Y'.                                   COR3009
007540     05  WS-COVERAGE-MATCH-SWITCH   PIC X VALUE 'N'.              COR0804
007550         88  WS-COVERAGE-MATCH-COB-NO                             COR0804
007560                     VALUE 'N'.                                   COR0804
007570         88  WS-COVERAGE-MATCH-COB-YES                            COR0804
007580                     VALUE 'Y'.                                   COR0804
           05  WS-COVERAGE-MEDICARE-B-SWITCH   PIC X VALUE 'N'.         COR3009
               88  WS-COVERAGE-MEDICARE-B-NO                            COR3009
                           VALUE 'N'.                                   COR3009
               88  WS-COVERAGE-MEDICARE-B-YES                           COR3009
                           VALUE 'Y'.                                   COR3009
           05  WS-COVERAGE-MEDICARE-C-SWITCH   PIC X VALUE 'N'.         COR3009
               88  WS-COVERAGE-MEDICARE-C-NO                            COR3009
                           VALUE 'N'.                                   COR3009
               88  WS-COVERAGE-MEDICARE-C-YES                           COR3009
                           VALUE 'Y'.                                   COR3009
           05  WS-COVERAGE-MEDICARE-D-SWITCH   PIC X VALUE 'N'.         COR3009
               88  WS-COVERAGE-MEDICARE-D-NO                            COR3009
                           VALUE 'N'.                                   COR3009
               88  WS-COVERAGE-MEDICARE-D-YES                           COR3009
                           VALUE 'Y'.                                   COR3009
007540     05  WS-COVERAGE-MATCH-SWITCH   PIC X VALUE 'N'.              COR0804
007550         88  WS-COVERAGE-MATCH-COB-NO                             COR0804
007560                     VALUE 'N'.                                   COR0804
007570         88  WS-COVERAGE-MATCH-COB-YES                            COR0804
007580                     VALUE 'Y'.                                   COR0804
001150                                                                  00171000
001160 01  WW-000-WORK-AREA.                                            00172000
           05  WW-000-C-CNT-EXE-NUM    PIC S9(4)      VALUE 0 COMP.     I2DSN
           05  SUB                     PIC S9(4)      VALUE 1 COMP.
           05  WW-000-SUB              PIC S9(3)      VALUE 0 COMP.     00173000
MDM-I2     05  WW-000-SUB-2            PIC S9(3)      VALUE 0 COMP.     00173000
MDM-I2     05  WW-000-SUB-3            PIC S9(3)      VALUE 0 COMP.     00173000
001180     05  WW-000-TCN              PIC 9(17).                       00175000
001190     05  WW-000-EXT-EXC-CD       PIC X(03).                       00176000
001200     05  WW-000-INT-EXC-CD       PIC X(04).                       00177000
001210     05  WW-000-RETURN-CODE      PIC 9(03).                       00178000
MAPOPS*    05  WW-000-EXISTS           PIC X(01).                       00179000
001230     05  WW-000-LIST             PIC X(15).                       00180000
001240     05  WW-000-PRESCRIBER.                                       00181000
001250         10  WW-000-PRESC-CH-1-2 PIC X(02).                       00182000
001260         10  FILLER              PIC X(13).                       00183000
001270     05  WW-000-TXN-HDR-CHECK.                                    00184000
001280         10  WW-000-CHECK-BIN    PIC X          VALUE SPACE.      00185000
001290         10  WW-000-CHECK-VERS   PIC X          VALUE SPACE.      00186000
001300         10  WW-000-CHECK-TXN    PIC X          VALUE SPACE.      00187000
001310         10  WW-000-CHECK-PROC   PIC X          VALUE SPACE.      00188000
001320         10  WW-000-CHECK-COUNT  PIC X          VALUE SPACE.      00189000
001330     05  WW-000-SET-EDIT         PIC X          VALUE SPACE.      00190000
001340     05  WW-000-2CHAR-FIELD      PIC X(02).                       DF1066
001350     05  WW-000-2NUM-FIELD       REDEFINES                        DF1066
001360         WW-000-2CHAR-FIELD      PIC 9(02).                       DF1066
001370     05  WW-010-DATE             PIC 9(08).                       DF1066
001380     05  FILLER REDEFINES WW-010-DATE.                            00195000
001390         10  WW-010-CCYY         PIC X(04).                       00196000
001400         10  WW-010-MM           PIC X(02).                       00197000
001410         10  WW-010-DD           PIC X(02).                       00198000
001420     05  WW-010-DATE-FORMATTED.                                   00199000
001430         10  WW-010-CCYY-F       PIC X(04).                       00200000
001440         10  FILLER              PIC X(01)      VALUE '-'.        00201000
001450         10  WW-010-MM-F         PIC X(02).                       00202000
001460         10  FILLER              PIC X(01)      VALUE '-'.        00203000
001470         10  WW-010-DD-F         PIC X(02).                       00204000
001480     05  WW-010-TIME             PIC 9(04).                       00205000
001490     05  FILLER REDEFINES WW-010-TIME.                            00206000
001500         10  WW-010-HH           PIC X(02).                       00207000
001510         10  WW-010-MI           PIC X(02).                       00208000
001520     05  WW-010-TIME-FORMATTED.                                   00209000
001530         10  WW-010-HH-F         PIC X(02).                       00210000
001540         10  FILLER              PIC X(01)      VALUE '.'.        00211000
001550         10  WW-010-MI-F         PIC X(02).                       00212000
001560         10  FILLER              PIC X(01)      VALUE '.'.        00213000
001570         10  WW-010-SS-F         PIC X(02)      VALUE '00'.       00214000
001580     05  WW-010-CURR-DATE.                                        00215000
001590         10  WW-010-CENTURY-YEAR.                                 00216000
001600             15  WW-010-CENTURY  PIC X(02).                       00217000
001610             15  WW-010-YEAR     PIC X(02).                       00218000
001620         10  WW-010-MONTH        PIC X(02).                       00219000
001630         10  WW-010-DAY          PIC X(02).                       00220000
001640     05  WW-010-CURR-DATE-9 REDEFINES                             00221000
001650         WW-010-CURR-DATE        PIC 9(08).                       00222000
DF1171     05  WW-010-CURR-DATE-FORMATTED.                              PML
DF1171         10  WW-010-CURR-CCYY-F  PIC X(04).                       PML
DF1171         10  FILLER              PIC X(01)      VALUE '-'.        PML
DF1171         10  WW-010-CURR-MM-F    PIC X(02).                       PML
DF1171         10  FILLER              PIC X(01)      VALUE '-'.        PML
DF1171         10  WW-010-CURR-DD-F    PIC X(02).                       PML
001660     05  WW-010-CURR-TIME.                                        00223000
001670         10  WW-010-CURRTIME-HH  PIC X(02).                       00224000
001680         10  WW-010-CURRTIME-MI  PIC X(02).                       00225000
001690         10  WW-010-CURRTIME-SS  PIC X(02).                       0x0226000
001700         10  WW-010-CURRTIME-MS  PIC X(02).                       00227000
001710     05  WW-010-NUM-1            PIC 9(01).                       00228000
001720     05  WW-010-TOT-TPL          PIC 9(09)V99   VALUE ZERO.
           05  WW-010-TOT-PAT-PAID     PIC S9(09)V99  VALUE ZERO.
           05  WW-010-DAW-DIF-AMT      PIC S9(09)V99  VALUE ZERO.
001730     05  WW-020-NDC.                                              00230000
001740         10  WW-020-NDC-1-11     PIC 9(11).                       00231000
001750         10  FILLER              PIC X(08).                       00232000
001760     05  WW-020-NCPDP-PROV-NUMBER.                                00233000
001770         10  WW-020-PROV-NUMBER  PIC X(07).                       00234000
001780         10  FILLER              PIC X(08).                       00235000
001790     05  WW-020-PROV-NUMBER-9    PIC 9(09).                       00236000
001800     05  WW-030-PROC-CNTL-NUM.                                    00237000
001810         10  FILLER              PIC X(08).                       00238000
001820             88  WW-030-PROC-MASS               VALUE             00239000
001830                 'MASSTEST' 'X2TEST  ' 'X2SYST  '                 00240000
PL0223                 'X2ACCP  ' 'X2TRNG  ' 'X2MAPROD' 'DRMATEST'
PL0223                 'DRMASYST' .
001850         10  FILLER              PIC X(02).
001800     05  WW-030-TEST-PROC-CNTL-NUM.                               MDM-4/9/13
001810         10  FILLER              PIC X(08).                       MDM-4/9/13
001820             88  WW-030-PROD-PROC-CNTL          VALUE             MDM-8/6/13
PL0223                            'DRMADRDR'.                           MDM-8/6/13
001820             88  WW-030-TEST-PROC-CNTL          VALUE             MDM-4/9/13
PL0223                 'DRMASYST'   .                                   MDM-4/9/13
001850         10  FILLER              PIC X(02).                       MDM-4/9/13
001860     05  WW-030-MASS-GROUP-ID    PIC X(15)      VALUE             00243000
001870             'MASSHEALTH     '.                                   00244000
001880     05  WW-040-NCPDP-RECIP-CARDHOLDER.                           00245000
001890         10  WW-040-RECIP-CARDHOLDER-ID                           00246000
001900                                 PIC X(18).                       00247000
001910     05  WW-050-RECIP-MBR-NUM-X                                   00248000
001920                                 PIC X(03).                       00249000
001930     05  WW-050-RECIP-MBR-NUM-FMT-1 REDEFINES                     00250000
001940         WW-050-RECIP-MBR-NUM-X.                                  00251000
001950         10  WW-050-RECIP-MBR-NUM-1-2                             00252000
001960                                 PIC 9(02).                       00253000
001970         10  FILLER              PIC X(01).                       00254000
001980     05  WW-050-RECIP-MBR-NUM-FMT-2 REDEFINES                     00255000
001990         WW-050-RECIP-MBR-NUM-X.                                  00256000
002000         10  FILLER              PIC X(01).                       00257000
002010         10  WW-050-RECIP-MBR-NUM-2-3                             00258000
002020                                 PIC 9(02).                       00259000
001120     05  WW-000-FATAL-ERROR-IND  PIC X(01)      VALUE ' '.        00260000
001130         88  WW-000-NO-FATAL-ERROR              VALUE 'N'.        00261000
001140         88  WW-000-FATAL-ERROR                 VALUE 'Y'.        00262000

           05  WS-000-HIST-SCHDL-II-CURS     PIC X VALUE 'N'.           CR12914
               88  WS-000-END-O-SCHDL-II-DATA                           CR12914
                           VALUE 'Y'.                                   CR12914
               88  WS-000-END-O-SCHDL-II-DATA-NOT                       CR12914
                           VALUE 'N'.                                   CR12914
01150                                                                   00263000
MAPOPS       EXEC SQL BEGIN DECLARE SECTION END-EXEC.                   00264000
MAPOPS 01  WW-000-WORK-AREA.                                            PMLS220
MAPOPS       05  WW-000-SYSLIST-EXIST             PIC X(01).            PMLS220
                 88  WW-000-SYSLIST-EXISTS        VALUE 'Y'.            PMLS220
             05  WW-000-SYSLIST-STRT-VALUE        PIC X(15).
             05  WW-000-SYSLIST-STRT-NUM       REDEFINES
                 WW-000-SYSLIST-STRT-VALUE        PIC 9(15).
             05  WW-000-SYSLIST-END-VALUE         PIC X(15).
             05  WW-000-SYSLIST-END-NUM        REDEFINES
                 WW-000-SYSLIST-END-VALUE         PIC 9(15).
             05  WW-000-SYSLIST-STRT-2-VALUE      PIC X(15).            COR3009A
             05  WW-000-SYSLIST-END-2-VALUE       PIC X(15).            COR3009A
             05  WW-000-SYSLIST-STRT-3-VALUE      PIC X(15).            COR3009A
             05  WW-000-SYSLIST-END-3-VALUE       PIC X(15).            COR3009A
             05  WH-000-ALLOW-SHDL-II-COUNT      PIC 9(10) VALUE ZEROES.CR12914
MAPOPS       EXEC SQL END DECLARE SECTION END-EXEC.                     00267000
MAPOPS                                                                  00268000
002030 ++INCLUDE WWG00050.                                              00269000
002040                                                                  00270000
002050* WD-999-SYSTEM-ABORT                                             00271000
002060                     COPY WD999950.                               00272000
002070* MAX OCCURENCES OF INDIVIDUAL CLAIM VIEWS                        00273000
002080                     COPY WKC80350.                               00274000
002090* WK-805-SQL-CODE-VALUES                                          00275000
002100                     COPY WKC80550.                               00276000
002110                                                                  00277000
002130                     COPY WVC0070C.                               00279000
002140                     COPY WVC0161C.                               00280000
002160                     COPY WVC0824C.                               00282000
002190                     COPY WVC1020C.                               00285000
002210                     COPY WVC1031C.                               00287000
002220                     COPY WVC1567C.                               00288000
002250                     COPY WVC4200C.                               00291000
002260                     COPY WVC4398C.                               00292000
002270                     COPY WVC8869C.                               00293000
002280                     COPY WVC8897C.                               00294000
002310                     COPY WVG0003C.                               00297000
002320                     COPY WVG0145C.                               00298000
002340                     COPY WVR1737C.                               00300000
002350                     COPY WVR4453C.                               00301000
                                                                        PMLMISS
      ** COPYBOOKS ADDED FOR USE BY S600C1                              PMLMISS
           COPY WKC80650.                                               PMLMISS
           COPY WVR0156C.                                               PMLMISS
           COPY WVR1903C.                                               PMLMISS
           COPY WVR1914C.                                               PMLMISS
           COPY WVG5880C.                                               PMLMISS
      ** END OF COPYBOOKS ADDED FOR USE BY S600C1                       PMLMISS
002360                                                                  00302000
002370     EXEC SQL                                                     00303000
002380          INCLUDE SQLCA                                           00304000
002390     END-EXEC.                                                    00305000
002400     EXEC SQL                                                     00306000
002410          INCLUDE GSLDTLTB                                        00307000
002420     END-EXEC.                                                    00308000
002430     EXEC SQL                                                     00309000
002440          INCLUDE RCGXRFTB                                        00310000
002450     END-EXEC.                                                    00311000
002430     EXEC SQL                                                     00309000
002440          INCLUDE RGROUPTB                                        00310000
002450     END-EXEC.                                                    00311000
002460     EXEC SQL                                                     00312000
002470          INCLUDE RCUSTDTB                                        00313000
002480     END-EXEC.                                                    00314000

      ** DCLGENS NEEDED BY S600C1                                       PMLMISS
           EXEC SQL                                                     PMLMISS
                INCLUDE RCEDS2TB                                        PMLMISS
           END-EXEC.                                                    PMLMISS
           EXEC SQL                                                     PMLMISS
                INCLUDE RCEEXCTB                                        PMLMISS
           END-EXEC.                                                    PMLMISS
      ** END OF DCLGENS NEEDED BY S600C1                                PMLMISS
                                                                        PMLMISS
           EXEC SQL
                INCLUDE WWC60050                                        PMLMISS
           END-EXEC.
002400     EXEC SQL                                                     00306000
002410          INCLUDE BBENEFTB                                        00307000
002420     END-EXEC.                                                    00308000
002490                                                                  00318000
002400     EXEC SQL                                                     00306000
002410          INCLUDE CLEXCXTB                                        00307000
002420     END-EXEC.                                                    00308000
002400     EXEC SQL                                                     00306000
002410          INCLUDE CLDRUGTB                                        00307000
002420     END-EXEC.                                                    00308000

           EXEC SQL                                                     CR12914
             DECLARE PHARM_SCHEDULE_II_MEM CURSOR FOR                   CR12914
               SELECT  C_QTY_PRSC_AMT                                   CR12914
                      ,C_DRUG_SUB_QTY_AMT                               CR12914
                      ,C_DRUG_RX_OVRRD_CD                               CR12914
                      ,C_DRUG_RX_OVR2_CD                                CR12914
                      ,C_DRUG_RX_OVR3_CD                                CR12914
                 FROM  C_LI_DRUG_TB A                                   CR12914
                      ,C_HDR_DRUG_TB B                                  CR12914
                WHERE A.R_CUST_PART_NUM  = :CLEXCXTB-R-CUST-PART-NUM    CR12914
                  AND A.B_SYS_ID         = :CLEXCXTB-B-SYS-ID           CR12914
                  AND A.R_CUST_PART_NUM  = B.R_CUST_PART_NUM            CR12914
                  AND A.B_SYS_ID         = B.B_SYS_ID                   CR12914
                  AND A.C_TCN_NUM        = B.C_TCN_NUM                  CR12914
                  AND A.C_HDR_STAT_CD    = :WV-C1020-C-PAID             CR12914
                  AND A.C_BLNG_PROV_ID   = :CLDRUGTB-C-BLNG-PROV-ID     CR12914
                  AND A.C_RX_SVC_REF_NUM = :CLDRUGTB-C-RX-SVC-REF-NUM   CR12914
                  AND A.C_HDR_TXN_TY_CD IN (:WV-C1030-C-ORIG-CLAIM      CR12914
                                           ,:WV-C1030-C-DEBOFADJ)       CR12914
                  AND NVL(A.C_CREDIT_CD,' ') <>                         CR12914
                                                :WV-C0978-C-COMPLETE    CR12914
                  AND A.R_DRUG_GCN_SEQ_NUM                              CR12914
                                         = :CLDRUGTB-R-DRUG-GCN-SEQ-NUM CR12914
                  ORDER BY A.C_HDR_SVC_FST_DT ASC                       CR12914
           END-EXEC.                                                    CR12914

002500 LINKAGE SECTION.                                                 00319000
002510 01  DFHCOMMAREA                 PIC X(01).                       00320000
002520                                                                  00321000
002530***  THIS IS THE CONSOLIDATED CLAIM VIEW                          00322000
002540                     COPY W1C66791.                               00323000
002550                                                                  00324000
002560***  THIS IS THE PHARMACY CLAIM CONTROL IO VIEW                   00325000
002570                     COPY W1C40541.                               00326000
002580                                                                  00327000
002590***  THIS IS THE ADJUSTMENT CLAIM CONTROL IO VIEW                 00328000
002600                     COPY W1C40611 REPLACING                      00329000
002610     == W1C40611-C-CNTL-ADJ-IO-VW == BY                           00330000
002620     == W1C40611-C-CNTL-ADJ-IO-VW REDEFINES                       00331000
002630        W1C40541-C-CNTL-PHRM-IO-VW ==.                            00332000
002640                                                                  00333000
002650***  THIS IS THE CLAIMS MODULE IO VIEW                            00334000
002660                     COPY W1C56941.                               00335000
002670                                                                  00336000
002680 01  WL-000-RX-NUM               PIC 9(1).
002680 01  WL-000-QTY-PRSC-AMT-OMT     PIC X(01).                       COR11078

                                                                        00338000
002690                                                                  00339000
002700 PROCEDURE DIVISION USING DFHEIBLK                                00340000
002710                          DFHCOMMAREA                             00341000
002720                          W1C66791-C-NCPDP-CNSLDTD                00342000
002730                          W1C40541-C-CNTL-PHRM-IO-VW              00343000
002740                          W1C56941-C-MDUL-OUT-WS                  00344000
002750                          WL-000-RX-NUM                           COR11078
                                WL-000-QTY-PRSC-AMT-OMT.                COR11078
002760*                                                                 00346000
002770 S010-000-MAIN-LOGIC SECTION.                                     00347000
002780*                                                                 00348000
002790***************************************************************** 00349000
002800*                                                               * 00350000
002810*?  S010-000-MAIN-LOGIC SECTION                                 * 00351000
002820*+                                                              * 00352000
002830*+  THIS SECTION CONTROLS THE OVERALL FLOW OF THE PROGRAM.      * 00353000
002840*+  FIRST IT PERFORMS THE INITIALIZATION SECTION.  NEXT, THE    * 00354000
002850*+  CLAIM RECORD IS INITIALIZED.  NEXT, EDITS ARE PERFORMED ON  * 00355000
002860*+  THE NCPDP TRANSACTION.  IF ANY TRANSACTION ERRORS EXIST,    * 00356000
002870*+  THE CLAIM IS NOT FORMATTED, AND THE PROGRAM TERMINATES.  IF * 00357000
002880*+  NO ERRORS ARE PRESENT, THE CLAIM RECORD IS FORMATTED, AND   * 00358000
002890*+  THE TERMINATION SECTION IS PERFORMED.                       * 00359000
002900*+                                                              * 00360000
002910***************************************************************** 00361000
002920 S010-010-START.                                                  00362000
002930*                                                                 00363000
002940     PERFORM S100-000-INITIALIZATION.                             00364000
002950                                                                  00365000
002960     PERFORM S200-000-EDIT-TRANSACTION.                           00366000
002970                                                                  00367000
002980*    NO EXCEPTIONS POSTED                                         00368000
002990**   IF W1C40541-C-CNT-EXC-NUM       = 0                          PMLMISS
002990     IF NOT WW-000-FATAL-ERROR                                    PMLMISS
003000         PERFORM S300-000-FORMAT-CLAIM                            00370000
003010     ELSE                                                         00371000
POSTFE         MOVE WV-C1020-C-DENIED  TO W1C40541-C-HDR-STAT-CD
003020         PERFORM S341-000-FORMAT-HDR                              00372000
003030     END-IF.                                                      00373000
003040                                                                  00374000
003050     PERFORM S800-000-TERMINATION.                                00375000
003060                                                                  00376000
003070     PERFORM S810-000-RETURN-TO-CALLER.                           00377000
003080*                                                                 00378000
003090 S010-999-EXIT.                                                   00379000
003100     EXIT.                                                        00380000
003110/                                                                 00381000
003120 S100-000-INITIALIZATION SECTION.                                 00382000
003130*                                                                 00383000
003140***************************************************************** 00384000
003150*                                                               * 00385000
003160*?  S100-000-INITIALIZATION SECTION                             * 00386000
003170*+                                                              * 00387000
003180*+  THIS SECTION CONTROLS ALL PROGRAM INITIALIZATION FUNCTIONS. * 00388000
003190*                                                               * 00389000
003200***************************************************************** 00390000
003210 S100-010-START.                                                  00391000
003220*                                                                 00392000
003230     EXEC SQL                                                     00393000
MAPOPS        SELECT  CURRENT_TIMESTAMP INTO :WW-000-TIMESTAMP
MAPOPS        FROM DUAL
003250     END-EXEC.                                                    00396000
003260                                                                  00397000
003270     IF SQLCODE OF SQLCA           = 0                            00398000
003280         CONTINUE                                                 00399000
003290     ELSE                                                         00400000
003300         MOVE 'S100-010-START'    TO W1C56941-G-PROG-SECTION-TX   00401000
003310         MOVE SPACES              TO W1C56941-G-SQL-TABLE-NAM     00402000
003320         MOVE 'SET TIME  '        TO W1C56941-G-SQL-FUNCTION-TX   00403000
003330         PERFORM S910-000-SQL-ERROR                               00404000
003340         PERFORM S999-000-BAD-RETURN                              00405000
003350     END-IF.                                                      00406000
003360                                                                  00407000
003370     MOVE WW-000-TIMESTAMP        TO WW-000-TIMESTAMP-SPLIT.      00408000
003390     MOVE WW-000-TIMESTAMP-YYYY   TO WW-010-CENTURY-YEAR.         00409000
003400     MOVE WW-000-TIMESTAMP-MM     TO WW-010-MONTH.                00410000
003410     MOVE WW-000-TIMESTAMP-MD     TO WW-010-DAY.                  00411000
003420     MOVE WW-000-TIMESTAMP-HH     TO WW-010-CURRTIME-HH           00412000
003430     MOVE WW-000-TIMESTAMP-MI     TO WW-010-CURRTIME-MI           00413000
003440     MOVE WW-000-TIMESTAMP-SS     TO WW-010-CURRTIME-SS           00414000
003450     MOVE WW-000-TIMESTAMP-MS     TO WW-010-CURRTIME-MS           00415000
DF1171                                                                  PML
DF1171     MOVE WW-010-CENTURY-YEAR     TO WW-010-CURR-CCYY-F.          PML
DF1171     MOVE WW-010-MONTH            TO WW-010-CURR-MM-F.            PML
DF1171     MOVE WW-010-DAY              TO WW-010-CURR-DD-F.            PML
003460                                                                  00416000
003470     SET W1C66791-C-NCPDP-CNSLDTD-RX-X                            00417000
003480                                  TO WL-000-RX-NUM.               00418000
PL1        MOVE WK-030-PROGRAM-NAME     TO WD-999-MSG (1).
MDM
MDM        MOVE W1C66791-C-NCP-POS-TRAN-CD TO WS-000-POS-TRAN-CD.
003490*                                                                 00419000
003500 S100-999-EXIT.                                                   00420000
003510     EXIT.                                                        00421000
003520*                                                                 00422000
003530 S200-000-EDIT-TRANSACTION SECTION.                               00423000
003540*                                                                 00424000
003550***************************************************************** 00425000
003560*                                                                 00426000
003570*?  S200-000-EDIT-TRANSACTION SECTION                           * 00427000
003580*+                                                              * 00428000
003590*+  THIS SECTION EDITS THE FOLLOWING NCPDP TRANSACTION FIELDS:  * 00429000
003600*+  BIN NUMBER, VERSION NUMBER, TRANSACTION CODE, AND PROCESSOR * 00430000
003610*+  NUMBER.  IF ANY ARE IN ERROR, THE APPROPRIATE REJECT CODE   * 00431000
003620*+  IS POSTED TO THE CLAIM, AND THE CLAIM IS NOT FORMATTED.     * 00432000
003630*                                                               * 00433000
003640***************************************************************** 00434000
003650 S200-000-START.                                                  00435000
003660*                                                                 00436000
           SET WS-FIRST-TIME-CMPND     TO TRUE.                         CR14386
001130     SET WW-000-NO-FATAL-ERROR      TO TRUE.                      00437000
      ****                                                              I2DSN
      *    MOVE WV-R1737-C-DISASTER-RECOV-CD TO WW-600-CLM-EXC-CD.      I2DSN
      *    MOVE ZEROES                       TO WW-600-CLM-EXC-LI.      I2DSN
      *    MOVE W1C40541-C-CNT-EXE-NUM       TO WW-000-C-CNT-EXE-NUM.   I2DSN
      *    PERFORM S600C1-000-POST-EXCEPTION.                           I2DSN
      *     IF W1C40541-C-CNT-EXE-NUM > WW-000-C-CNT-EXE-NUM            I2DSN
      *        AND W1C40541-C-CNT-EXE-CD OF W1C40541-C-LI-EXC-VM        I2DSN
      *                                      (W1C40541-C-CNT-EXE-NUM)   I2DSN
      *                      = WV-R1737-C-DISASTER-RECOV-CD             I2DSN
      *        AND (W1C40541-C-EXC-STAT-CD OF                           I2DSN
      *                 W1C40541-C-LI-EXC-VM (W1C40541-C-CNT-EXE-NUM)   I2DSN
      *                      = WV-C4200-C-DENY                          I2DSN
      *                        OR WV-C4200-C-DENY-RPT                   I2DSN
      *                        OR WV-C4200-C-FORCE-DENY)                I2DSN
      *            SET WW-000-FATAL-ERROR TO TRUE                       I2DSN
      *            GO TO S200-999-EXIT                                  I2DSN
      ***   END-IF.                                                     I2DSN

           MOVE W1C66791-C-NCP-PRCS-NUM TO WW-030-TEST-PROC-CNTL-NUM.   MDM-4/9/13
           IF WW-030-TEST-PROC-CNTL                                     MDM-4/9/13
              PERFORM S500C1-000-TEST-CLM-FORMAT                        MDM-4/9/13
           END-IF.
003680                                                                  00438000
003670     PERFORM S210-000-VALIDATE-GROUP.                             00439000
003680                                                                  00440000
      ** IF AN INVALID GROUP WAS FOUND BYPASS ALL OTHER EDITS SINCE     PML
      ** WE CAN NOT DERIVE A CUSTOMER PARTITION NUMBER.                 PML
                                                                        PML
           IF W1C40541-C-CNT-EXC-NUM       = 0                          PML
               CONTINUE                                                 PML
           ELSE                                                         PML
               SET WW-000-FATAL-ERROR     TO TRUE                       PML
               GO TO S200-999-EXIT                                      PML
           END-IF.                                                      PML

      ** Removed IF WW-030-PROD-PROC-CNTL so that                       DF5745
      ** S550C1-000-PROD-CLM-FORMAT will be perform in all regions.     DF5745

      *    IF WW-030-PROD-PROC-CNTL                                     DF5745
              PERFORM S550C1-000-PROD-CLM-FORMAT.                       DF5745
      *    END-IF.                                                      DF5745
003770*                                                                 00450000
003780 S200-010-EDIT-BIN.                                               00451000
003790*                                                                 00452000
003800     IF W1C66791-C-NCP-BIN-NUM = HIGH-VALUES                      PMLS220
             MOVE ZEROES TO W1C66791-C-NCP-BIN-NUM                      PMLS220
           END-IF.                                                      PMLS220
003830         MOVE '7575'                 TO GSLDTLTB-G-LIST-NUM       PMLS220
003840         MOVE W1C66791-C-NCP-BIN-NUM TO GSLDTLTB-G-LIST-STRT-LMT  00457000
003850                                        GSLDTLTB-G-LIST-END-LMT   PMLS220
003860         PERFORM S220-000-SEARCH-LIST                             00459000
003870                                                                  00460000
003880         IF NOT WW-000-SYSLIST-EXISTS                             PMLS220
003900             MOVE 'X'                TO WW-000-CHECK-BIN          00463000
      **           FIELD     EDIT    EXCP      **********************   PMLV1.11
003910             MOVE WV-R1737-C-INVALID-BIN
                                               TO WW-600-CLM-EXC-CD     PMLV1.11
                   MOVE ZEROES                 TO WW-600-CLM-EXC-LI     PMLV1.11
POSTFE             SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
                   PERFORM S600C1-000-POST-EXCEPTION                    PMLV1.11
POSTFE             SET WW-000-FATAL-ERROR  TO TRUE                      PML
POSTFE             SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
               END-IF.                                                  PMLS220
004040*                                                                 00478000
004050 S200-020-EDIT-VERSION.                                           00479000
004060*                                                                 00480000
           IF W1C66791-C-NCP-VERSION-NUM = HIGH-VALUES                  COR3013
              MOVE 'X'                TO WW-000-CHECK-VERS              COR3013
      **      FIELD     EDIT    EXCP 4002 **********************        COR3013
              MOVE WV-R1737-C-MISS-VERSION TO WW-600-CLM-EXC-CD         COR3013
              MOVE ZEROES                  TO WW-600-CLM-EXC-LI         COR3013
      **  HARD CODING VERSION NUMBER HERE SO THAT WE CAN GET A
      **  REJECT CODE IN THE RESPONSE.
              MOVE 'D0'                    TO W1C40541-C-DRUG-VERSN-NUM
              SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE                    COR3013
              PERFORM S600C1-000-POST-EXCEPTION                         COR3013
              SET WW-000-FATAL-ERROR  TO TRUE                           COR3013
              SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE                   COR3013
           END-IF.

004250     MOVE '7002'                     TO GSLDTLTB-G-LIST-NUM.      PMLS220
004260     MOVE W1C66791-C-NCP-VERSION-NUM TO GSLDTLTB-G-LIST-STRT-LMT  PMLS220
004270                                        GSLDTLTB-G-LIST-END-LMT.  PMLS220
004280     PERFORM S220-000-SEARCH-LIST.                                00504000
004290                                                                  00505000
004300     IF WW-000-SYSLIST-EXISTS                                     COR3013
                 MOVE 'X'                TO WW-000-CHECK-VERS
      **         FIELD     EDIT    EXCP 4003 **********************     PMLV1.11
004320           MOVE WV-R1737-C-UNACCEPTED-VERSION                     00508000
                                         TO WW-600-CLM-EXC-CD           PMLV1.11
                 MOVE ZEROES             TO WW-600-CLM-EXC-LI           PMLV1.11
      **  HARD CODING VERSION NUMBER HERE SO THAT WE CAN GET A
      **  REJECT CODE IN THE RESPONSE.
                 MOVE 'D0'               TO W1C40541-C-DRUG-VERSN-NUM
                 SET WS-000-EC-IN-PRCS-IS-FATAL  TO TRUE                COR3013
                 PERFORM S600C1-000-POST-EXCEPTION                      PMLV1.11
                 SET WW-000-FATAL-ERROR          TO TRUE                COR3013
                 SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE                COR3013
004370     END-IF.                                                      PMLS220
004380*

           IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-RX-REVERSAL-51-D0 COR3013
             IF W1C66791-C-NCP-TRAN-CNT-NUM NUMERIC AND                 COR3013
                W1C66791-C-NCP-TRAN-CNT-NUM > 1                         COR3013
      **     EDIT 3182 FOR B2 WITH TRANSACTION COUNT > 1 ***************COR3013
                 MOVE WV-R1737-C-INV-REVERSAL TO WW-600-CLM-EXC-CD      COR3013
                 MOVE ZEROES                  TO WW-600-CLM-EXC-LI      COR3013
                 PERFORM S600C1-000-POST-EXCEPTION                      COR3013
             END-IF                                                     COR3013
           END-IF.                                                      COR3013
005070*                                                                 00607000
       S200-040-999-EXIT.                                               POPSIII
           EXIT.                                                        POPSIII
005260*                                                                 00627000
005270 S200-050-CHECK-DOWNTIME.                                         00628000
005280*                                                                 00629000
005310     MOVE '7998'                     TO GSLDTLTB-G-LIST-NUM.      00632000
005320     MOVE 'ON'                       TO GSLDTLTB-G-LIST-STRT-LMT  PMLS220
005330                                        GSLDTLTB-G-LIST-END-LMT.  PMLS220
005340     PERFORM S220-000-SEARCH-LIST.                                00635000
005350                                                                  00636000
005160     IF WW-000-SYSLIST-EXISTS                                     MDM0920
      **       FIELD     EDIT    EXCP 3017                              PMLV1.11
005380         MOVE WV-R1737-C-SYS-NOT-AVAILABLE                        00639000
                                               TO WW-600-CLM-EXC-CD     PMLV1.11
               MOVE ZEROES                     TO WW-600-CLM-EXC-LI     PMLV1.11
POSTFE         SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
               PERFORM S600C1-000-POST-EXCEPTION                        PMLV1.11
POSTFE         SET WW-000-FATAL-ERROR  TO TRUE                          PML
POSTFE         SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
005430     END-IF.                                                      PMLS220
005440*                                                                 00646000
007290 S200-060-CHECK-TRANS-COUNT.                                      00647000
                                                                        PMLMISS
005460**   NCP-TRAN-CNT-NUM IS POPULATED FROM NCPDP TXN HEADER          PMLMISS
                                                                        PMLMISS
007310     IF W1C66791-C-NCP-TRAN-CNT-NUM = HIGH-VALUES                 COR3013
007360        MOVE 'X'                    TO WW-000-CHECK-COUNT         COR3013
      **      FIELD     EDIT    EXCP 4213                               COR3013
007370        MOVE WV-R1737-C-MISS-TRAN-CNT   TO WW-600-CLM-EXC-CD      COR3013
              MOVE ZEROES                     TO WW-600-CLM-EXC-LI      COR3013
POSTFE        SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE                    COR3013
              PERFORM S600C1-000-POST-EXCEPTION                         COR3013
POSTFE        SET WW-000-FATAL-ERROR  TO TRUE                           COR3013
POSTFE        SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE                   COR3013
007420     END-IF.                                                      COR3013

      * UNCOMMENTED THE BELOW CODE TO ALLOW MULTIPLE TRANSACTIONS       MULTRN
           IF W1C66791-C-NCP-TRAN-CNT-NUM NUMERIC AND                   00649000
007320        W1C66791-C-NCP-TRAN-CNT-NUM > 0     AND                   00650000
007330        W1C66791-C-NCP-TRAN-CNT-NUM < 5                           00651000
           THEN                                                         00652000
007340         MOVE W1C66791-C-NCP-TRAN-CNT-NUM                         00653000
                                           TO W1C40541-C-TRAN-CNT-NUM   00654000
007350     ELSE                                                         00655000
007360         MOVE 'X'                    TO WW-000-CHECK-COUNT        00656000
      **       FIELD     EDIT    EXCP                                   PMLV1.11
MULTRN         MOVE WV-R1737-C-TRAN-CNT-GRTR-ONE                        MULTRN
007370*        MOVE WV-R1737-C-INVALID-TRAN-CNT                         00657000
                                               TO WW-600-CLM-EXC-CD     PMLV1.11
               MOVE ZEROES                     TO WW-600-CLM-EXC-LI     PMLV1.11
POSTFE         SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
               PERFORM S600C1-000-POST-EXCEPTION                        PMLV1.11
POSTFE         SET WW-000-FATAL-ERROR  TO TRUE                          PML
POSTFE         SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
007420     END-IF.                                                      00663000
      * UNCOMMENTED THE ABOVE CODE TO ALLOW MULTIPLE TRANSACTIONS       MULTRN

           IF W1C66791-C-NCP-TRAN-CNT-NUM NUMERIC                       COR3013
              IF W1C66791-C-NCP-TRAN-CNT-NUM = W1C66791-C-NCP-CNT-RX-NUMCOR3013
                 CONTINUE                                               COR3013
              ELSE                                                      COR3013
      **         FIELD     EDIT    EXCP 3018                            COR3013
                 MOVE 'X'                          TO WW-000-CHECK-COUNTCOR3013
                 MOVE WV-R1737-C-MISMATCH-TRAN-CNT TO WW-600-CLM-EXC-CD COR3013
                 MOVE ZEROES                       TO WW-600-CLM-EXC-LI COR3013
                 SET WS-000-EC-IN-PRCS-IS-FATAL    TO TRUE              COR3013
                 PERFORM S600C1-000-POST-EXCEPTION                      COR3013
                 SET WW-000-FATAL-ERROR  TO TRUE                        COR3013
                 SET WS-000-EC-IN-PRCS-NOT-FATAL   TO TRUE              COR3013
              END-IF                                                    COR3013
           END-IF.                                                      COR3013

           IF W1C66791-C-NCP-TRAN-CNT-NUM NUMERIC AND                   COR3013
              W1C66791-C-NCP-TRAN-CNT-NUM > 1                           COR3013
      **      FIELD     EDIT    EXCP 3019                               COR3013
              MOVE 'X'                          TO WW-000-CHECK-COUNT   COR3013
              MOVE WV-R1737-C-TRAN-CNT-GRTR-ONE TO WW-600-CLM-EXC-CD    COR3013
              MOVE ZEROES                       TO WW-600-CLM-EXC-LI    COR3013
              SET WS-000-EC-IN-PRCS-IS-FATAL    TO TRUE                 COR3013
              PERFORM S600C1-000-POST-EXCEPTION                         COR3013
              SET WW-000-FATAL-ERROR  TO TRUE                           COR3013
              SET WS-000-EC-IN-PRCS-NOT-FATAL   TO TRUE                 COR3013
           END-IF.                                                      COR3013

           IF W1C66791-C-NCP-TRAN-CNT-NUM NUMERIC AND                   COR3013
              W1C66791-C-NCP-TRAN-CNT-NUM > 1                           COR3013
             IF (W1C66791-C-NCP-COMPOUND-CD                             COR3013
                (W1C66791-C-NCPDP-CNSLDTD-RX-X) = 2)                    COR3013
      **      FIELD     EDIT    EXCP 3020                               COR3013
                MOVE 'X'                          TO WW-000-CHECK-COUNT COR3013
                MOVE WV-R1737-C-INVAL-TRAN-CNT-D0 TO WW-600-CLM-EXC-CD  COR3013
                MOVE ZEROES                       TO WW-600-CLM-EXC-LI  COR3013
                SET WS-000-EC-IN-PRCS-IS-FATAL    TO TRUE               COR3013
                PERFORM S600C1-000-POST-EXCEPTION                       COR3013
                SET WW-000-FATAL-ERROR  TO TRUE                         COR3013
                SET WS-000-EC-IN-PRCS-NOT-FATAL   TO TRUE               COR3013
             END-IF
           END-IF.
005440*                                                                 00664000
005450 S200-070-CHECK-VERSION.                                          00665000
005460*                                                                 00666000
005470     IF W1C66791-C-NCP-VERSION-NUM = WV-C8897-C-VERSION-D0        00667000
                                        OR WV-C8897-C-VERSION-51
           THEN
005480         PERFORM S250-000-EDIT-51-D0-TXN                          00669000
005490     END-IF.                                                      00670000
005500*                                                                 00671000
005510 S200-999-EXIT.                                                   00672000
005520     EXIT.                                                        00673000
005530/                                                                 00674000
005540 S210-000-VALIDATE-GROUP SECTION.                                 00675000
005550*                                                                 00676000
005560***************************************************************** 00677000
005570*                                                               * 00678000
005580*?  S210-000-VALIDATE-GROUP SECTION                             * 00679000
005590*+                                                              * 00680000
005600*+  THIS SECTION USES THE SUBMITTED GROUP ID TO OBTAIN THE      * 00681000
005610*+  CUSTOMER GROUP ID AND MATCHING PARTITION NUMBER.  IT THEN   * 00682000
005620*+  USES THE PARTITION NUMBER TO OBTAIN THE CUSTOMER ID.        * 00683000
005630*                                                               * 00684000
005640***************************************************************** 00685000
005650 S210-010-START.                                                  00686000
005660*                                                                 00687000
005670**   SINCE REVERSALS DO NOT HAVE GROUP IDS WE NEED TO BASE   **   00688000
005680**   PARTITION NUMBER OFF THE PROC CNTL NUMBER.              **   00689000
005690*                                                                 00690000
005700     IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-RX-1-REVERSAL     00691000
005710         MOVE W1C66791-C-NCP-PRCS-NUM                             00692000
005720                                     TO WW-030-PROC-CNTL-NUM      00693000
005730         EVALUATE TRUE                                            00694000
005740           WHEN WW-030-PROC-MASS                                  00695000
005750             MOVE WW-030-MASS-GROUP-ID                            00696000
005760                                     TO RCGXRFTB-R-NCPDP-GRP-ID   00697000
POSTFE                                        W1C66791-C-NCP-GROUP-ID
005770           WHEN OTHER                                             00698000
005780             MOVE SPACES             TO RCGXRFTB-R-NCPDP-GRP-ID   00699000
POSTFE                                        W1C66791-C-NCP-GROUP-ID
005790         END-EVALUATE                                             00700000
PL0223**   ELSE                                                         00701000
PL0223**       MOVE W1C66791-C-NCP-GROUP-ID                             00702000
PL0223**                                  TO RCGXRFTB-R-NCPDP-GRP-ID    00703000
005830     END-IF.                                                      00704000
005840
                                                                        00705000
005850     IF W1C66791-C-NCP-GROUP-ID       = HIGH-VALUES               PMLMISS
005890         MOVE WV-R1737-C-MISSING-GROUP-ID                         PMLMISS
                                           TO WW-600-CLM-EXC-CD         PML0212
               MOVE ZEROES                 TO WW-600-CLM-EXC-LI         PML0212
POSTFE         SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
               PERFORM S600C1-000-POST-EXCEPTION                        PML0212
POSTFE         SET WW-000-FATAL-ERROR  TO TRUE                          PML
POSTFE         SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
               GO TO S210-999-EXIT                                      PML
005940     END-IF.                                                      00715000

           MOVE W1C66791-C-NCP-GROUP-ID    TO RCGXRFTB-R-NCPDP-GRP-ID.
005950                                                                  00716000
005960     EXEC SQL                                                     00717000
005970        SELECT  R_GROUP_ID,                                       00718000
005980                R_CUST_PART_NUM                                   00719000
005990         INTO  :RCGXRFTB-R-GROUP-ID,                              00720000
006000               :RCGXRFTB-R-CUST-PART-NUM                          00721000
006010         FROM   R_CUST_GRP_XREF_TB                                00722000
DF1225        WHERE   R_NCPDP_GRP_ID  = :RCGXRFTB-R-NCPDP-GRP-ID
006040     END-EXEC.                                                    00725000
006050                                                                  00726000
006060     EVALUATE SQLCODE                OF SQLCA                     00727000
006070       WHEN +0                                                    00728000
006080         MOVE RCGXRFTB-R-GROUP-ID    TO W1C40541-R-GROUP-ID       00729000
006090         MOVE RCGXRFTB-R-CUST-PART-NUM                            00730000
006100                                     TO W1C40541-R-CUST-PART-NUM  00731000
006120       WHEN +100                                                  00733000
006130         MOVE SPACES                 TO W1C40541-R-GROUP-ID       00734000
006140         MOVE ZERO                   TO W1C40541-R-CUST-PART-NUM  00735000
006160         MOVE WV-R1737-C-GROUP-NOT-FOUND                          00737000
006170                                     TO WW-000-INT-EXC-CD         00738000
006180         MOVE WV-R4453-C-NONM-GROUP-ID                            00739000
006190                                     TO WW-000-EXT-EXC-CD         00740000
006200         PERFORM S600-000-INSERT-EXC-CODE                         00741000
006210       WHEN OTHER                                                 00742000
006220         MOVE 'S210-000-VALIDATE-GROUP'                           00743000
006230                                     TO W1C56941-G-PROG-SECTION-TX00744000
006240         MOVE 'R_CUST_GRP_XREF_TB'   TO W1C56941-G-SQL-TABLE-NAM  00745000
006250         MOVE 'SELECT'               TO W1C56941-G-SQL-FUNCTION-TX00746000
006260         MOVE RCGXRFTB-R-NCPDP-GRP-ID                             00747000
006270                                     TO W1C56941-G-KEY-TX         00748000
006280         PERFORM S910-000-SQL-ERROR                               00749000
006290         PERFORM S999-000-BAD-RETURN                              00750000
006300     END-EVALUATE.                                                00751000
006310                                                                  00752000
006320     MOVE W1C40541-R-CUST-PART-NUM   TO RCUSTDTB-R-CUST-PART-NUM. PML0212
006330                                                                  00754000
006340     EXEC SQL                                                     00755000
006350        SELECT  R_CUST_ID                                         00756000
DF1171               ,R_CUST_BEG_DT                                     PML
DF1171               ,R_CUST_END_DT                                     PML
006360         INTO  :RCUSTDTB-R-CUST-ID                                00757000
DF1171              ,:RCUSTDTB-R-CUST-BEG-DT
DF1171              ,:RCUSTDTB-R-CUST-END-DT
006370         FROM   R_CUST_DTL_TB                                     00758000
006380        WHERE   R_CUST_PART_NUM       = :RCUSTDTB-R-CUST-PART-NUM 00759000
006400     END-EXEC.                                                    00761000
006410                                                                  00762000
006420     EVALUATE SQLCODE                OF SQLCA                     00763000
006430       WHEN +0                                                    00764000
006440         MOVE RCUSTDTB-R-CUST-ID     TO W1C40541-R-CUST-ID        00765000
006450       WHEN +100                                                  00766000
006460         MOVE WV-R1737-C-GROUP-NOT-FOUND                          00767000
006470                                     TO WW-000-INT-EXC-CD         00768000
006480         MOVE WV-R4453-C-NONM-GROUP-ID                            00769000
006490                                     TO WW-000-EXT-EXC-CD         00770000
006500         PERFORM S600-000-INSERT-EXC-CODE                         00771000
DF1171         GO TO S210-999-EXIT                                      PML
006510       WHEN OTHER                                                 00772000
006520         MOVE 'S210-000-VALIDATE-GROUP'                           00773000
006530                                     TO W1C56941-G-PROG-SECTION-TX00774000
006540         MOVE 'R_CUST_DTL_TB'        TO W1C56941-G-SQL-TABLE-NAM  00775000
006550         MOVE 'SELECT'               TO W1C56941-G-SQL-FUNCTION-TX00776000
006560         MOVE RCUSTDTB-R-CUST-PART-NUM                            00777000
006570                                     TO WK-030-NUMBER             00778000
006580         MOVE WK-030-NUMBER          TO W1C56941-G-KEY-TX         00779000
006590         PERFORM S910-000-SQL-ERROR                               00780000
006600         PERFORM S999-000-BAD-RETURN                              00781000
006610     END-EVALUATE.                                                00782000

      ***  HAVE TO CHECK RGROUPTB FOR END DATES - M.MELTON
           MOVE W1C40541-R-CUST-PART-NUM TO RGROUPTB-R-CUST-PART-NUM.
           MOVE W1C40541-R-GROUP-ID      TO RGROUPTB-R-GROUP-ID

005960     EXEC SQL                                                     00717000
005970        SELECT R_GRP_BEG_DT                                       00718000
005980              ,R_GRP_END_DT                                       00719000
005990         INTO  :RGROUPTB-R-GRP-BEG-DT                             00720000
006000              ,:RGROUPTB-R-GRP-END-DT                             00721000
006010         FROM   R_GROUP_DTL_TB                                    00722000
DF1225        WHERE   R_CUST_PART_NUM  = :RGROUPTB-R-CUST-PART-NUM
DF1225          AND   R_GROUP_ID       = :RGROUPTB-R-GROUP-ID
006040     END-EXEC.                                                    00725000

006420     EVALUATE SQLCODE                OF SQLCA                     00763000
006430       WHEN +0                                                    00764000
006440         CONTINUE                                                 00765000
006450       WHEN +100                                                  00766000
POSTFE         MOVE '0001-01-01' TO RGROUPTB-R-GRP-BEG-DT
                                    RGROUPTB-R-GRP-END-DT
006510       WHEN OTHER                                                 00772000
006520         MOVE 'S210-000-VALIDATE-GROUP'                           00773000
006530                                     TO W1C56941-G-PROG-SECTION-TX00774000
006540         MOVE 'R_GROUP_DTL_TB'       TO W1C56941-G-SQL-TABLE-NAM  00775000
006550         MOVE 'SELECT'               TO W1C56941-G-SQL-FUNCTION-TX00776000
006560         MOVE RGROUPTB-R-CUST-PART-NUM                            00777000
006570                                     TO WK-030-NUMBER             00778000
006580         MOVE WK-030-NUMBER          TO W1C56941-G-KEY-TX         00779000
006590         PERFORM S910-000-SQL-ERROR                               00780000
006600         PERFORM S999-000-BAD-RETURN                              00781000
006610     END-EVALUATE.                                                00782000

DF1171     IF RGROUPTB-R-GRP-BEG-DT    <= WW-010-CURR-DATE-FORMATTED    PML
DF1171     AND RGROUPTB-R-GRP-END-DT   >= WW-010-CURR-DATE-FORMATTED    PML
DF1171       CONTINUE                                                   PML
DF1171     ELSE                                                         PML
DF1171***  EXCEPTION 3362 FOR INVALID DATES ON RGROUPTB
DF1171       MOVE WV-R1737-C-INV-GROUP-ON-SYSDT TO WW-600-CLM-EXC-CD    PML
DF1171       MOVE ZEROES                        TO WW-600-CLM-EXC-LI    PML
POSTFE       SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
DF1171       PERFORM S600C1-000-POST-EXCEPTION                          PML
POSTFE       SET WW-000-FATAL-ERROR  TO TRUE                            PML
POSTFE       SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
DF1171     END-IF.                                                      PML
DF1171                                                                  PML
DF1319     MOVE '0301'                     TO GSLDTLTB-G-LIST-NUM.      PML
DF1319     MOVE W1C66791-C-NCP-GROUP-ID    TO GSLDTLTB-G-LIST-STRT-LMT  PML
DF1319                                        GSLDTLTB-G-LIST-END-LMT.  PML
DF1319     PERFORM S220A-000-SEARCH-LIST.                               PML
DF1319                                                                  PML
DF1319     IF NOT WW-000-SYSLIST-EXISTS                                 PML
DF1319**      FIELD     EDIT    EXCP                                    PML
DF1319         MOVE WV-R1737-C-GROUP-NOT-FOUND                          PML
DF1319                                         TO WW-600-CLM-EXC-CD     PML
DF1319         MOVE ZEROES                     TO WW-600-CLM-EXC-LI     PML
POSTFE         SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
DF1319         PERFORM S600C1-000-POST-EXCEPTION                        PML
POSTFE         SET WW-000-FATAL-ERROR  TO TRUE                          PML
POSTFE         SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
DF1319     ELSE
DF1319**     FIELD     EDIT    EXCP 4082                                PML
DF1319       IF GSLDTLTB-G-LIST-EFF-STRT-DT                             PML
DF1319                                   <= WW-010-CURR-DATE-FORMATTED  PML
DF1319       AND GSLDTLTB-G-LIST-EFF-END-DT                             PML
DF1319                                    >= WW-010-CURR-DATE-FORMATTED PML
DF1319         CONTINUE                                                 PML
DF1319       ELSE                                                       PML
DF1319         MOVE WV-R1737-C-GROUP-NOT-FOUND                          PML
DF1319                                       TO WW-600-CLM-EXC-CD       PML
DF1319         MOVE ZEROES                   TO WW-600-CLM-EXC-LI       PML
POSTFE         SET WS-000-EC-IN-PRCS-IS-FATAL TO TRUE
DF1319         PERFORM S600C1-000-POST-EXCEPTION                        PML
POSTFE         SET WW-000-FATAL-ERROR  TO TRUE                          PML
POSTFE         SET WS-000-EC-IN-PRCS-NOT-FATAL TO TRUE
DF1319       END-IF                                                     PML
DF1319     END-IF.                                                      PML
DF1319                                                                  PML
006630 S210-999-EXIT.                                                   00784000
006640     EXIT.                                                        00785000
006650/                                                                 00786000
006660 S220-000-SEARCH-LIST SECTION.                                    00787000
006670*                                                                 00788000
006680***************************************************************** 00789000
006690*                                                               * 00790000
006700*?  S220-000-SEARCH-LIST         SECTION                        * 00791000
006710*+                                                              * 00792000
006720*+  THIS SECTION SEARCHES THE LIST DETAIL TABLE BASED UPON THE  * 00793000
006730*+  FIELDS PASSED TO IT.                                        * 00794000
006740*+                                                              * 00795000
006750***************************************************************** 00796000
006760*                                                                 00797000
006770 S220-010-START.                                                  00798000
006780*                                                                 00799000
HAKJOB*    IF GSLDTLTB-G-LIST-STRT-LMT = SPACES                         PMLS220
HAKJOB*       MOVE 'Y'   TO WW-000-SYSLIST-EXIST                        PMLS220
HAKJOB*       GO TO S220-999-EXIT                                       PMLS220
HAKJOB*    END-IF.                                                      PMLS220
                                                                        PMLS220
           MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID.       PMLS220
           MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD.   PMLS220
           MOVE W1C40541-C-DRUG-VERSN-NUM TO GSLDTLTB-R-NCPDP-VRSN-NUM. PMLS220
           MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM.  PMLS220
           MOVE WV-G0003-C-CLAIMS         TO GSLDTLTB-G-LIST-SUBSYS-CD. PMLS220
                                                                        PMLS220
      **   CURRENT_DATE MUST BE USED UNLESS DOS OR OTHER DATE FROM      PMLS220
      **   CLAIM IS VALIDATED FOR DATE FORMAT.  REMEMBER...DATES ARE    PMLS220
      **   NOT VALIDATED FOR DATE FORMAT UNTIL PDDC8120 EXECUTES.       PMLS220
006790     EXEC SQL                                                     PMLS220
006800        SELECT 'Y'                                                PMLS220
006810         INTO :WW-000-SYSLIST-EXIST                               PMLS220
006820         FROM G_LIST_DTL_TB                                       PMLS220
006830        WHERE (R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM     PMLS220
                     OR R_CUST_PART_NUM = +0)                           PMLS220
                AND G_LIST_SUBSYS_CD   =  :GSLDTLTB-G-LIST-SUBSYS-CD    PMLS220
                AND G_LIST_NUM         =  :GSLDTLTB-G-LIST-NUM          PMLS220
                AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)         PMLS220
                  OR (R_GROUP_ID       =  'ALL     '))                  PMLS220
                AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)     PMLS220
                  OR (C_NCPDP_TXN_CD   =  'AL'))                        PMLS220
                AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM)   PMLS220
                  OR (R_NCPDP_VRSN_NUM =  'AL'))                        PMLS220
MAPOPS          AND G_LIST_EFF_STRT_DT <= TRUNC(CURRENT_DATE)           PMLS220
                AND G_LIST_STRT_LMT   <= :GSLDTLTB-G-LIST-STRT-LMT      PMLS220
                AND G_LIST_END_LMT    >= :GSLDTLTB-G-LIST-END-LMT       PMLS220
MAPOPS          AND G_LIST_EFF_END_DT  >= TRUNC(CURRENT_DATE)           PMLS220
MAPOPS          AND ROWNUM = 1                                          PMLS220
006910     END-EXEC                                                     00812000
006920                                                                  00813000
006930     EVALUATE SQLCODE            OF SQLCA                         00814000
006940       WHEN WK-805-SUCCESSFUL-CALL                                PMLS220
006950         CONTINUE                                                 00816000
006960       WHEN WK-805-DATA-NOT-FOUND                                 PMLS220
006970         MOVE 'N'                TO WW-000-SYSLIST-EXIST          PMLS220
006980       WHEN OTHER                                                 00819000
006990         MOVE 'S220-000-SEARCH-LIST '                             00820000
007000                                 TO W1C56941-G-PROG-SECTION-TX    00821000
007010         MOVE 'G_LIST_DTL_TB'    TO W1C56941-G-SQL-TABLE-NAM      00822000
007020         MOVE 'SELECT'           TO W1C56941-G-SQL-FUNCTION-TX    00823000
007030         MOVE W1C40541-R-CUST-PART-NUM                            PML0212
007040                                 TO WK-030-NUMBER                 00825000
007050         STRING WK-030-NUMBER ' '                                 00826000
007060                GSLDTLTB-G-LIST-STRT-LMT ' '                      00827000
007070                GSLDTLTB-G-LIST-NUM ' '                           00828000
007080           DELIMITED BY SIZE   INTO W1C56941-G-KEY-TX             00829000
007090         PERFORM S910-000-SQL-ERROR                               00830000
007100         PERFORM S999-000-BAD-RETURN                              00831000
007110     END-EVALUATE.                                                00832000
007120                                                                  00833000
007130 S220-999-EXIT.                                                   00834000
007140     EXIT.                                                        00835000
007150*                                                                 00836000
DF1319 S220A-000-SEARCH-LIST            SECTION.                        PML
DF1319***************************************************************** PML
DF1319*                                                               * PML
DF1319*?  S220A-000-SEARCH-LIST         SECTION                       * PML
DF1319*+                                                              * PML
DF1319*+  SAME AS S220 EXCEPT EFFECTIVE DATES ARE BROUGHT BACK BUT NOT* PML
DF1319*+  USED TO QUALIFY QUERY.                                      * PML
DF1319*+                                                              * PML
DF1319***************************************************************** PML
DF1319*                                                                 PML
DF1319 S220A-010-START.                                                 PML
DF1319*                                                                 PML
DF1319     MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID.       PML
DF1319     MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD.   PML
DF1319     MOVE W1C40541-C-DRUG-VERSN-NUM TO GSLDTLTB-R-NCPDP-VRSN-NUM. PML
DF1319     MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM.  PML
DF1319     MOVE WV-G0003-C-CLAIMS         TO GSLDTLTB-G-LIST-SUBSYS-CD. PML
DF1319                                                                  PML
DF1319**   CURRENT_DATE MUST BE USED UNLESS DOS OR OTHER DATE FROM      PML
DF1319**   CLAIM IS VALIDATED FOR DATE FORMAT.  REMEMBER...DATES ARE    PML
DF1319**   NOT VALIDATED FOR DATE FORMAT UNTIL PDDC8120 EXECUTES.       PML
DF1319     EXEC SQL                                                     PML
DF1319        SELECT 'Y'                                                PML
DF1319               ,G_LIST_EFF_STRT_DT                                PML
DF1319               ,G_LIST_EFF_END_DT                                 PML
DF1319         INTO :WW-000-SYSLIST-EXIST                               PML
DF1319              ,:GSLDTLTB-G-LIST-EFF-STRT-DT                       PML
DF1319              ,:GSLDTLTB-G-LIST-EFF-END-DT                        PML
DF1319         FROM G_LIST_DTL_TB                                       PML
DF1319        WHERE (R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM     PML
DF1319               OR R_CUST_PART_NUM = +0)                           PML
DF1319          AND G_LIST_SUBSYS_CD   =  :GSLDTLTB-G-LIST-SUBSYS-CD    PMS
DF1319          AND G_LIST_NUM         =  :GSLDTLTB-G-LIST-NUM          PML
DF1319          AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)         PML
DF1319            OR (R_GROUP_ID       =  'ALL     '))                  PML
DF1319          AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)     PML
DF1319            OR (C_NCPDP_TXN_CD   =  'AL'))                        PML
DF1319          AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM)   PML
DF1319            OR (R_NCPDP_VRSN_NUM =  'AL'))                        PML
DF1319          AND G_LIST_STRT_LMT   <= :GSLDTLTB-G-LIST-STRT-LMT      PML
DF1319          AND G_LIST_END_LMT    >= :GSLDTLTB-G-LIST-END-LMT       PML
DF1319          AND ROWNUM = 1                                          PML
DF1319     END-EXEC                                                     PML
DF1319                                                                  PML
DF1319     EVALUATE SQLCODE            OF SQLCA                         PML
DF1319       WHEN WK-805-SUCCESSFUL-CALL                                PML
DF1319         CONTINUE                                                 PML
DF1319       WHEN WK-805-DATA-NOT-FOUND                                 PML
DF1319         MOVE 'N'                TO WW-000-SYSLIST-EXIST          PML
DF1319       WHEN OTHER                                                 PML
DF1319         MOVE 'S220A-000-SEARCH-LIST '                            PML
DF1319                                 TO W1C56941-G-PROG-SECTION-TX    PML
DF1319         MOVE 'G_LIST_DTL_TB'    TO W1C56941-G-SQL-TABLE-NAM      PML
DF1319         MOVE 'SELECT'           TO W1C56941-G-SQL-FUNCTION-TX    PML
DF1319         MOVE W1C40541-R-CUST-PART-NUM                            PML
DF1319                                 TO WK-030-NUMBER                 PML
DF1319         STRING WK-030-NUMBER ' '                                 PML
DF1319                GSLDTLTB-G-LIST-STRT-LMT ' '                      PML
DF1319                GSLDTLTB-G-LIST-NUM ' '                           PML
DF1319           DELIMITED BY SIZE   INTO W1C56941-G-KEY-TX             PML
DF1319         PERFORM S910-000-SQL-ERROR                               PML
DF1319         PERFORM S999-000-BAD-RETURN                              PML
DF1319     END-EVALUATE.                                                PML
DF1319                                                                  PML
DF1319 S220A-999-EXIT.                                                  PML
DF1319     EXIT.                                                        PML
007160/
006660 S221-000-SEARCH-LIST SECTION.                                    MDM-I1
006670*                                                                 MDM-I1
006680***************************************************************** MDM-I1
006690*                                                               * MDM-I1
006700*?  S221-000-SEARCH-LIST         SECTION                        * MDM-I1
006710*+                                                              * MDM-I1
006720*+  THIS SECTION SEARCHES THE LIST DETAIL TABLE BASED UPON THE  * MDM-I1
006730*+  FIELDS PASSED TO IT, RETURNING THE 2ND LABEL BEGIN AND      * MDM-I1
      *+  END RANGES.                                                  *MDM-I1
006740*+                                                              * MDM-I1
006750***************************************************************** MDM-I1
006760*                                                                 MDM-I1
006770 S221-010-START.                                                  MDM-I1
006780*                                                                 MDM-I1
           MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID.       MDM-I1
           MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD.   MDM-I1
           MOVE W1C40541-C-DRUG-VERSN-NUM TO GSLDTLTB-R-NCPDP-VRSN-NUM. MDM-I1
           MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM.  MDM-I1
           MOVE WV-G0003-C-CLAIMS         TO GSLDTLTB-G-LIST-SUBSYS-CD. MDM-I1
                                                                        MDM-I1
      **   CURRENT_DATE MUST BE USED UNLESS DOS OR OTHER DATE FROM      MDM-I1
      **   CLAIM IS VALIDATED FOR DATE FORMAT.  REMEMBER...DATES ARE    MDM-I1
      **   NOT VALIDATED FOR DATE FORMAT UNTIL PDDC8120 EXECUTES.       MDM-I1
006790     EXEC SQL                                                     MDM-I1
006800        SELECT 'Y'                                                MDM-I1
006810         INTO :WW-000-SYSLIST-EXIST                               MDM-I1
006820         FROM G_LIST_DTL_TB                                       MDM-I1
006830        WHERE (R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM     MDM-I1
                     OR R_CUST_PART_NUM = +0)                           MDM-I1
                AND G_LIST_SUBSYS_CD   =  :GSLDTLTB-G-LIST-SUBSYS-CD    MDM-I1
                AND G_LIST_NUM         =  :GSLDTLTB-G-LIST-NUM          MDM-I1
                AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)         MDM-I1
                  OR (R_GROUP_ID       =  'ALL     '))                  MDM-I1
                AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)     MDM-I1
                  OR (C_NCPDP_TXN_CD   =  'AL'))                        MDM-I1
                AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM)   MDM-I1
                  OR (R_NCPDP_VRSN_NUM =  'AL'))                        MDM-I1
MAPOPS          AND G_LIST_EFF_STRT_DT <= TRUNC(CURRENT_DATE)           MDM-I1
                AND G_LIST_STRT_LMT   <= :GSLDTLTB-G-LIST-STRT-LMT      MDM-I1
                AND G_LIST_END_LMT    >= :GSLDTLTB-G-LIST-END-LMT       MDM-I1
MAPOPS          AND G_LIST_EFF_END_DT  >= TRUNC(CURRENT_DATE)           MDM-I1
MAPOPS          AND G_LIST_STRT_2_LMT <=  :GSLDTLTB-G-LIST-STRT-2-LMT   MDM-I1
                AND G_LIST_END_2_LMT  >=  :GSLDTLTB-G-LIST-END-2-LMT    MDM-I1
                AND ROWNUM = 1                                          MDM-I1
006910     END-EXEC                                                     MDM-I1
006920                                                                  MDM-I1
006930     EVALUATE SQLCODE            OF SQLCA                         MDM-I1
006940       WHEN WK-805-SUCCESSFUL-CALL                                MDM-I1
006950         CONTINUE                                                 MDM-I1
006960       WHEN WK-805-DATA-NOT-FOUND                                 MDM-I1
006970         MOVE 'N'                TO WW-000-SYSLIST-EXIST          MDM-I1
006980       WHEN OTHER                                                 MDM-I1
006990         MOVE 'S221-000-SEARCH-LIST '                             MDM-I1
007000                                 TO W1C56941-G-PROG-SECTION-TX    MDM-I1
007010         MOVE 'G_LIST_DTL_TB'    TO W1C56941-G-SQL-TABLE-NAM      MDM-I1
007020         MOVE 'SELECT'           TO W1C56941-G-SQL-FUNCTION-TX    MDM-I1
007030         MOVE W1C40541-R-CUST-PART-NUM                            MDM-I1
007040                                 TO WK-030-NUMBER                 MDM-I1
007050         STRING WK-030-NUMBER ' '                                 MDM-I1
007060                GSLDTLTB-G-LIST-STRT-LMT ' '                      MDM-I1
007070                GSLDTLTB-G-LIST-NUM ' '                           MDM-I1
007080           DELIMITED BY SIZE   INTO W1C56941-G-KEY-TX             MDM-I1
007090         PERFORM S910-000-SQL-ERROR                               MDM-I1
007100         PERFORM S999-000-BAD-RETURN                              MDM-I1
007110     END-EVALUATE.                                                MDM-I1
007120                                                                  MDM-I1
007130 S221-999-EXIT.                                                   MDM-I1
007140     EXIT.                                                        MDM-I1
007150*                                                                 00836000
006660 S222-000-SEARCH-LIST SECTION.                                    MDM-I1
006670*                                                                 MDM-I1
006680***************************************************************** MDM-I1
006690*                                                               * MDM-I1
006700*?  S222-000-SEARCH-LIST         SECTION                        * MDM-I1
006710*+                                                              * MDM-I1
006720*+  THIS SECTION RETURNS A VALUE FROM SPECIFIC CONFIG LIST      * MDM-I1
006730*+  0447.  USES VALUE FOR POSTING EC 3131.                      * MDM-I1
006740*+                                                              * MDM-I1
006750***************************************************************** MDM-I1
006760*                                                                 MDM-I1
006770 S222-010-START.                                                  MDM-I1
006780*                                                                 MDM-I1
           MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID.       MDM-I1
           MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD.   MDM-I1
           MOVE W1C40541-C-DRUG-VERSN-NUM TO GSLDTLTB-R-NCPDP-VRSN-NUM. MDM-I1
           MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM.  MDM-I1
           MOVE WV-G0003-C-CLAIMS         TO GSLDTLTB-G-LIST-SUBSYS-CD. MDM-I1
                                                                        MDM-I1
      **   CURRENT_DATE MUST BE USED UNLESS DOS OR OTHER DATE FROM      MDM-I1
      **   CLAIM IS VALIDATED FOR DATE FORMAT.  REMEMBER...DATES ARE    MDM-I1
      **   NOT VALIDATED FOR DATE FORMAT UNTIL PDDC8120 EXECUTES.       MDM-I1
006790     EXEC SQL                                                     MDM-I1
006800        SELECT G_LIST_STRT_LMT                                    MDM-I1
                    ,G_LIST_END_LMT                                     MDM-I1
006810         INTO :WW-000-SYSLIST-STRT-VALUE                          MDM-I1
                   ,:WW-000-SYSLIST-END-VALUE                           MDM-I1
006820         FROM G_LIST_DTL_TB                                       MDM-I1
006830        WHERE (R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM     MDM-I1
                     OR R_CUST_PART_NUM = +0)                           MDM-I1
                AND G_LIST_SUBSYS_CD   =  :GSLDTLTB-G-LIST-SUBSYS-CD    MDM-I1
                AND G_LIST_NUM         =  :GSLDTLTB-G-LIST-NUM          MDM-I1
                AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)         MDM-I1
                  OR (R_GROUP_ID       =  'ALL     '))                  MDM-I1
                AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)     MDM-I1
                  OR (C_NCPDP_TXN_CD   =  'AL'))                        MDM-I1
                AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM)   MDM-I1
                  OR (R_NCPDP_VRSN_NUM =  'AL'))                        MDM-I1
MAPOPS          AND G_LIST_EFF_STRT_DT <= TRUNC(CURRENT_DATE)           MDM-I1
MAPOPS          AND G_LIST_EFF_END_DT  >= TRUNC(CURRENT_DATE)           MDM-I1
                AND ROWNUM = 1                                          MDM-I1
006910     END-EXEC                                                     MDM-I1
006920                                                                  MDM-I1
006930     EVALUATE SQLCODE            OF SQLCA                         MDM-I1
006940       WHEN WK-805-SUCCESSFUL-CALL                                MDM-I1
               CONTINUE
006960       WHEN WK-805-DATA-NOT-FOUND                                 MDM-I1
006970         MOVE '999999999999999'   TO WW-000-SYSLIST-STRT-VALUE    MDM-I1
                                           WW-000-SYSLIST-END-VALUE
006980       WHEN OTHER                                                 MDM-I1
006990         MOVE 'S222-000-SEARCH-LIST '                             MDM-I1
007000                                 TO W1C56941-G-PROG-SECTION-TX    MDM-I1
007010         MOVE 'G_LIST_DTL_TB'    TO W1C56941-G-SQL-TABLE-NAM      MDM-I1
007020         MOVE 'SELECT'           TO W1C56941-G-SQL-FUNCTION-TX    MDM-I1
007030         MOVE W1C40541-R-CUST-PART-NUM                            MDM-I1
007040                                 TO WK-030-NUMBER                 MDM-I1
007050         STRING WK-030-NUMBER ' '                                 MDM-I1
007060                GSLDTLTB-G-LIST-STRT-LMT ' '                      MDM-I1
007070                GSLDTLTB-G-LIST-NUM ' '                           MDM-I1
007080           DELIMITED BY SIZE   INTO W1C56941-G-KEY-TX             MDM-I1
007090         PERFORM S910-000-SQL-ERROR                               MDM-I1
007100         PERFORM S999-000-BAD-RETURN                              MDM-I1
007110     END-EVALUATE.                                                MDM-I1
007120                                                                  MDM-I1
007130 S222-999-EXIT.                                                   MDM-I1
007140     EXIT.                                                        MDM-I1
007150*                                                                 00836000
006660 S223-000-SEARCH-LIST SECTION.                                    COR3009A
006670*                                                                 COR3009A
006680***************************************************************** COR3009A
006690*                                                               * COR3009A
006700*?  S223-000-SEARCH-LIST         SECTION                        * COR3009A
006710*+                                                              * COR3009A
006720*+  THIS SECTION RETURNS A VALUE FROM SPECIFIC CONFIG LIST      * COR3009A
006730*+  3510.  USES VALUE FOR EVALUATING OTHER PAYER PATIEN         * COR3009A
      *+  RESPONSIBILITY AMOUNT QUALIFIER.                            * COR3009A
006740*+                                                              * COR3009A
006750***************************************************************** COR3009A
006760*                                                                 COR3009A
006770 S223-010-START.                                                  COR3009A
006780*                                                                 COR3009A
           MOVE 'N'                      TO WW-000-SYSLIST-EXIST        COR3009A
           MOVE SPACES                   TO WW-000-SYSLIST-STRT-2-VALUE COR3009A
                                            WW-000-SYSLIST-END-2-VALUE  COR3009A
                                            WW-000-SYSLIST-STRT-3-VALUE COR3009A
                                            WW-000-SYSLIST-END-3-VALUE. COR3009A
           MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID.       COR3009A
           MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD.   COR3009A
           MOVE W1C40541-C-DRUG-VERSN-NUM TO GSLDTLTB-R-NCPDP-VRSN-NUM. COR3009A
           MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM.  COR3009A
           MOVE WV-G0003-C-CLAIMS         TO GSLDTLTB-G-LIST-SUBSYS-CD. COR3009A
                                                                        COR3009A
      **   CURRENT_DATE MUST BE USED UNLESS DOS OR OTHER DATE FROM      COR3009A
      **   CLAIM IS VALIDATED FOR DATE FORMAT.  REMEMBER...DATES ARE    COR3009A
      **   NOT VALIDATED FOR DATE FORMAT UNTIL PDDC8120 EXECUTES.       COR3009A
006790     EXEC SQL                                                     COR3009A
006800        SELECT NVL (G_LIST_STRT_2_LMT, ' ')                       COR3009A
                    ,NVL (G_LIST_END_2_LMT, ' ')                        COR3009A
                    ,NVL (G_LIST_STRT_3_LMT, ' ')                       COR3009A
                    ,NVL (G_LIST_END_3_LMT, ' ')                        COR3009A
006810         INTO :WW-000-SYSLIST-STRT-2-VALUE                        COR3009A
                   ,:WW-000-SYSLIST-END-2-VALUE                         COR3009A
                   ,:WW-000-SYSLIST-STRT-3-VALUE                        COR3009A
                   ,:WW-000-SYSLIST-END-3-VALUE                         COR3009A
006820         FROM G_LIST_DTL_TB                                       COR3009A
006830        WHERE (R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM     COR3009A
                     OR R_CUST_PART_NUM = +0)                           COR3009A
                AND G_LIST_SUBSYS_CD   =  :GSLDTLTB-G-LIST-SUBSYS-CD    COR3009A
                AND G_LIST_NUM         =  :GSLDTLTB-G-LIST-NUM          COR3009A
                AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)         COR3009A
                  OR (R_GROUP_ID       =  'ALL     '))                  COR3009A
                AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)     COR3009A
                  OR (C_NCPDP_TXN_CD   =  'AL'))                        COR3009A
                AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM)   COR3009A
                  OR (R_NCPDP_VRSN_NUM =  'AL'))                        COR3009A
                AND G_LIST_STRT_LMT   <= :GSLDTLTB-G-LIST-STRT-LMT      COR3009A
                AND G_LIST_END_LMT    >= :GSLDTLTB-G-LIST-END-LMT       COR3009A
MAPOPS          AND G_LIST_EFF_STRT_DT <= TRUNC(CURRENT_DATE)           COR3009A
MAPOPS          AND G_LIST_EFF_END_DT  >= TRUNC(CURRENT_DATE)           COR3009A
                AND ROWNUM = 1                                          COR3009A
006910     END-EXEC                                                     COR3009A
006920                                                                  COR3009A
006930     EVALUATE SQLCODE            OF SQLCA                         COR3009A
006940       WHEN WK-805-SUCCESSFUL-CALL                                COR3009A
               MOVE 'Y'   TO WW-000-SYSLIST-EXIST                       COR3009A
006960       WHEN WK-805-DATA-NOT-FOUND                                 COR3009A
               MOVE 'N'   TO WW-000-SYSLIST-EXIST                       COR3009A
006970         MOVE 'NO'                TO WW-000-SYSLIST-STRT-2-VALUE  COR3009A
                                           WW-000-SYSLIST-END-2-VALUE   COR3009A
006980       WHEN OTHER                                                 COR3009A
006990         MOVE 'S223-000-SEARCH-LIST '                             COR3009A
007000                                 TO W1C56941-G-PROG-SECTION-TX    COR3009A
007010         MOVE 'G_LIST_DTL_TB'    TO W1C56941-G-SQL-TABLE-NAM      COR3009A
007020         MOVE 'SELECT'           TO W1C56941-G-SQL-FUNCTION-TX    COR3009A
007030         MOVE W1C40541-R-CUST-PART-NUM                            COR3009A
007040                                 TO WK-030-NUMBER                 COR3009A
007050         STRING WK-030-NUMBER ' '                                 COR3009A
007060                GSLDTLTB-G-LIST-STRT-LMT ' '                      COR3009A
007070                GSLDTLTB-G-LIST-NUM ' '                           COR3009A
007080           DELIMITED BY SIZE   INTO W1C56941-G-KEY-TX             COR3009A
007090         PERFORM S910-000-SQL-ERROR                               COR3009A
007100         PERFORM S999-000-BAD-RETURN                              COR3009A
007110     END-EVALUATE.                                                COR3009A
007120                                                                  COR3009A
007130 S223-999-EXIT.                                                   COR3009A
007140     EXIT.                                                        COR3009A
007150*                                                                 COR3009A
007170 S250-000-EDIT-51-D0-TXN SECTION.                                 00838000
007180*                                                                 00839000
007190***************************************************************** 00840000
007200*                                                               * 00841000
007210*?  S250-000-EDIT-51-D0-TXN         SECTION                     * 00842000
007220*+                                                              * 00843000
007230*+  THIS SECTION EDITS FIELDS COMMON TO NCPDP 5.1 AND D.0.      * 00844000
007240*+  IF ANY ARE IN ERROR, THE APPROPRIATE REJECT CODE IS POSTED  * 00845000
007250*+  AND THE CLAIM IS NOT FORMATTED.                             * 00846000
007260*+                                                              * 00847000
007270***************************************************************** 00848000
007280*                                                                 00849000
007440 S250-020-MI-PA-REQ-TYPE.                                         00850000
007450                                                                  00851000
007460*************** EDIT MOVED TO PDDC8300                            00852000
007470                                                                  00853000
007480 S250-030-MI-COB-COUNT.                                           00854000
                                                                        00855000
           MOVE W1C66791-C-NCP-GROUP-ID    TO W1C40541-C-SUBM-GROUP-ID. 00856000
                                                                        00857000
           IF   W1C66791-C-NCP-SUBM-PLAN-ID = HIGH-VALUES               PMLMISS
      **   FIELD FO NOT ENTERED / 3325                                  PMLMISS
             MOVE WV-R1737-C-MISS-SUBM-PLAN-ID TO WW-600-CLM-EXC-CD     PMLMISS
             MOVE ZEROES                 TO WW-600-CLM-EXC-LI           PMLMISS
             PERFORM S600C1-000-POST-EXCEPTION                          PMLMISS
             MOVE SPACES                 TO W1C40541-C-SUBM-PLAN-ID     PMLMISS
           ELSE                                                         PMLMISS
           MOVE W1C66791-C-NCP-SUBM-PLAN-ID                             00858000
                                           TO W1C40541-C-SUBM-PLAN-ID.  00859000

           IF   W1C66791-C-NCP-HOME-PLN-TX      = HIGH-VALUES           PMLMISS
      **   FIELD CE NOT ENTERED / EDIT NOT IN BRD                       PMLMISS
      **     MOVE WV-R1737-C-            TO WW-600-CLM-EXC-CD           PMLMISS
      **     MOVE ZEROES                 TO WW-600-CLM-EXC-LI           PMLMISS
      **     PERFORM S600C1-000-POST-EXCEPTION                          PMLMISS
             MOVE SPACES                 TO W1C40541-C-HOME-PLN-TX      PMLMISS
           ELSE                                                         PMLMISS
           MOVE W1C66791-C-NCP-HOME-PLN-TX                              00860000
                                           TO W1C40541-C-HOME-PLN-TX.   00861000
013560 S250-999-EXIT.
013570     EXIT.
013580/
013590 S300-000-FORMAT-CLAIM    SECTION.
013600*                                                                 01717000
013610***************************************************************** 01718000
013620*                                                               * 01719000
013630*?  S300-000-FORMAT-CLAIM        SECTION                        * 01720000
013640*+                                                              * 01721000
013650*+  THIS SECTION CALLS SECTIONS TO FORMAT THE DRUG CLAIM AND    * 01722000
013660*+  THE CREDIT REQUEST CLAIM, DEPENDING ON TRANSACTION CODE.    * 01723000
013670*+                                                              * 01724000
013680***************************************************************** 01725000
013690*                                                                 01726000
013700 S300-010-START.                                                  01727000
013710*                                                                 01728000
           IF W1C66791-C-NCP-POS-TRAN-CD   =                            01729000
013890                         WV-C8869-C-RX-BILLING-51-D0   OR         01730000
013900                         WV-C8869-C-RX-RE-BILL-51-D0   OR         01731000
MDM-I2                         WV-C8869-C-SERVICE-BILLING-D0 OR
MDM-I2                         WV-C8869-C-SERVICE-REBILL-D0  OR
013910                         WV-C8869-C-CS-RPT-51-D0       OR         01732000
013920                         WV-C8869-C-CS-REVR-51-D0      OR         01733000
013930                         WV-C8869-C-CS-RB-51-D0        OR         01734000
013940                         WV-C8869-C-INFO-RPT-51-D0     OR         01735000
013950                         WV-C8869-C-INFO-REVR-51-D0    OR         01736000
013960                         WV-C8869-C-INFO-RB-51-D0      OR         01737000
013970                         WV-C8869-C-PA-REQ-BILL-51-D0  OR         01738000
013980                         WV-C8869-C-PA-REVR-51-D0      OR         01739000
013990                         WV-C8869-C-PA-INQ-51-D0       OR         01740000
014000                         WV-C8869-C-PA-REQ-51-D0       OR         01741000
013730                         WV-C8869-C-RX-1-BILLING       OR         01742000
013740                         WV-C8869-C-RX-2-BILLING       OR         01743000
013750                         WV-C8869-C-RX-3-BILLING       OR         01744000
013760                         WV-C8869-C-RX-4-BILLING       OR         01745000
013770                         WV-C8869-C-RX-1-DOWN-BILLING  OR         01746000
013780                         WV-C8869-C-RX-2-DOWN-BILLING  OR         01747000
013790                         WV-C8869-C-RX-3-DOWN-BILLING  OR         01748000
013800                         WV-C8869-C-RX-4-DOWN-BILLING  OR         01749000
013810                         WV-C8869-C-RX-1-RE-BILLING    OR         01750000
013820                         WV-C8869-C-RX-2-RE-BILLING    OR         01751000
013830                         WV-C8869-C-RX-3-RE-BILLING    OR         01752000
013840                         WV-C8869-C-RX-4-RE-BILLING    OR         01753000
013850                         WV-C8869-C-RX-1-RE-BILL-HO               01754000
013860*                        WV-C8869-C-RX-2-RE-BILL-HO   OR          01755000
013870*                        WV-C8869-C-RX-3-RE-BILL-HO   OR          01756000
013880*                        WV-C8869-C-RX-4-RE-BILL-HO               01757000
           THEN                                                         01758000
014010         PERFORM S310-000-FORMAT-DRUG-CLAIM                       01759000
014020     ELSE                                                         01760000
014030         IF W1C66791-C-NCP-POS-TRAN-CD =                          01761000
014050                        WV-C8869-C-RX-REVERSAL-51-D0   OR         01762000
014040                        WV-C8869-C-RX-1-REVERSAL       OR         01763000
                              WV-C8869-C-SERVICE-REVERS-D0
               THEN                                                     01764000
014060             PERFORM S320-000-FORMAT-CREDIT-REQ                   01765000
014070         ELSE                                                     01766000
014080             PERFORM S330-000-FORMAT-ELIG-VERIF                   01767000
014090         END-IF                                                   01768000
014100     END-IF.                                                      01769000
014110*                                                                 01770000
014120 S300-999-EXIT.                                                   01771000
014130     EXIT.                                                        01772000
014140/                                                                 01773000
014150 S310-000-FORMAT-DRUG-CLAIM SECTION.                              01774000
014160*                                                                 01775000
014170******************************************************************01776000
014180*                                                                *01777000
014190*?  S310-000-FORMAT-DRUG-CLAIM   SECTION                         *01778000
014200*+                                                               *01779000
014210*+  THIS SECTION FORMATS THE PHARMACY CLAIM FROM THE CONSOLIDATED*01780000
014220*+  LAYOUT OF THE INPUT TRANSACTION.                             *01781000
014230*+                                                               *01782000
014240******************************************************************01783000
014250*                                                                 01784000
014260 S310-010-START.                                                  01785000
014270*                                                                 01786000
014280     PERFORM S340-000-FORMAT-HDR-COMMON.                          01787000
014290                                                                  01788000
014300     PERFORM S360-000-FMT-DRUG-VAR.                               01789000
014310                                                                  01790000
014320****  M O V E   I N   L I N E   I T E M S  ****                   01791000
014330                                                                  01792000
           IF W1C66791-C-NCP-CNT-CMPD-NUM-X
                (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES
              MOVE ZEROES 
                TO W1C66791-C-NCP-CNT-CMPD-NUM
                     (W1C66791-C-NCPDP-CNSLDTD-RX-X)
           END-IF.

014340     IF W1C66791-C-NCP-CNT-CMPD-NUM                               01793000
014350                      (W1C66791-C-NCPDP-CNSLDTD-RX-X) = 0         01794000
           THEN                                                         01795000
014360         MOVE LOW-VALUES           TO WS-000-COMPOUND-SW          01796000
014370         SET W1C40541-C-LI-DRUG-DTL-VW-X                          01797000
014380                                   TO +1                          01798000
014390         PERFORM S365-000-FMT-DRUG-LI                             01799000
014400     ELSE                                                         01800000
014410         MOVE HIGH-VALUES          TO WS-000-COMPOUND-SW          01801000
014420         PERFORM S365-000-FMT-DRUG-LI                             01802000
014430           VARYING W1C40541-C-LI-DRUG-DTL-VW-X                    01803000
                    FROM +1 BY +1                                       01804000
014440           UNTIL W1C40541-C-LI-DRUG-DTL-VW-X >                    01805000
014450                           W1C66791-C-NCP-CNT-CMPD-NUM            01806000
014460                             (W1C66791-C-NCPDP-CNSLDTD-RX-X)      01807000
                    OR > 15                                             01808000
014470     END-IF.                                                      01809000
014480                                                                  01810000
015220*
015230 S310-999-EXIT.
015240     EXIT.
015250/
015260 S320-000-FORMAT-CREDIT-REQ SECTION.                              01914000
015270*                                                                 01915000
015280***************************************************************** 01916000
015290*                                                               * 01917000
015300*?  S320-000-FORMAT-CREDIT-REQUEST SECTION                      * 01918000
015310*+                                                              * 01919000
015320*+  THIS SECTION FORMATS THE PHARMACY REQUEST CLAIM FROM THE    * 01920000
015330*+  CONSOLIDATED INPUT REVERSAL TRANSACTION.                    * 01921000
015340*+                                                              * 01922000
015350***************************************************************** 01923000
015360*                                                                 01924000
015370 S320-010-START.                                                  01925000
015380*                                                                 01926000
015390     PERFORM S340-000-FORMAT-HDR-COMMON.                          01927000
015400                                                                  01928000
015410     PERFORM S350-000-FMT-CREDIT-VAR.                             01929000
015420*                                                                 01930000
015430 S320-999-EXIT.                                                   01931000
015440     EXIT.                                                        01932000
015450/                                                                 01933000
015460 S330-000-FORMAT-ELIG-VERIF SECTION.                              01934000
015470*                                                                 01935000
015480***************************************************************** 01936000
015490*                                                               * 01937000
015500*?  S330-000-FORMAT-ELIGIBILITY-VERIFICATION SECTION            * 01938000
015510*+                                                              * 01939000
015520*+  THIS SECTION FORMATS THE PHARMACY CLAIM TO USE FOR          * 01940000
015530*+  ELIGIBILITY VERIFICATION FROM THE CONSOLIDATED ELIGIBILITY  * 01941000
015540*+  VERIFICATION INPUT TRANSACTION.                             * 01942000
015550*+                                                              * 01943000
015560***************************************************************** 01944000
015570*                                                                 01945000
015580 S330-010-START.                                                  01946000
015590*                                                                 01947000
015600     PERFORM S340-000-FORMAT-HDR-COMMON.                          01948000
015610
015620     IF W1C66791-C-NCP-OTHR-INSR-CD
015630       (W1C66791-C-NCPDP-CNSLDTD-RX-X) NUMERIC
015640         MOVE W1C66791-C-NCP-OTHR-INSR-CD
015650             (W1C66791-C-NCPDP-CNSLDTD-RX-X)
015660                                     TO W1C40541-C-OTHR-INSR-IND
015670     END-IF.
015680                                                                  01949000
015690 S330-999-EXIT.                                                   01957000
015700     EXIT.                                                        01958000
015710/                                                                 01959000
015720 S340-000-FORMAT-HDR-COMMON SECTION.                              01960000
015730*                                                                 01961000
015740***************************************************************** 01962000
015750*                                                               * 01963000
015760*?  S340-000-FORMAT-HEADER-COMMON SECTION                       * 01964000
015770*+                                                              * 01965000
015780*+  THIS SECTION FORMATS THE COMMON HEADER PORTION OF THE DRUG  * 01966000
015790*+  CLAIM FOR PRESCRIPTIONS AND ELIGIBILITY VERIFICATIONS.      * 01967000
015800*+                                                              * 01968000
015810***************************************************************** 01969000
015820*                                                                 01970000
015830 S340-010-START.                                                  01971000
015840*                                                                 01972000
015850*THIS LOGIC MOVES IN DEFAULT CLAIM TYPES BASED ON INPUT DATA      01973000
015860*WHICH THEN CAN BE OVERRIDDEN IN PDDC8130.                        01974000
015870                                                                  01975000
           IF W1C66791-C-NCP-VERSION-NUM = WV-C8897-C-VERSION-D0        01976000
MDM-I2        IF W1C66791-C-NCP-POS-TRAN-CD =                           01977000
MDM-I2                                    WV-C8869-C-RX-BILLING-51-D0
                 MOVE WV-C1031-C-PHARMACY-D0 TO W1C40541-C-HDR-TY-CD
MDM-I2        ELSE
MDM-I2           IF W1C66791-C-NCP-POS-TRAN-CD =                        01977000
MDM-I2                                    WV-C8869-C-SERVICE-BILLING-D0
MDM-I2              MOVE WV-C1031-C-SERVICE-D0 TO W1C40541-C-HDR-TY-CD
MDM-I2           END-IF
MDM-I2        END-IF
           ELSE                                                         01978000
015880        IF W1C66791-C-NCP-VERSION-NUM = WV-C8897-C-VERSION-51     01979000
015890           MOVE WV-C1031-C-PHARMACY-51 TO W1C40541-C-HDR-TY-CD    01980000
015900        ELSE                                                      01981000
015910           MOVE WV-C1031-C-PHARMACY-32 TO W1C40541-C-HDR-TY-CD    01982000
015920        END-IF                                                    01983000
           END-IF.                                                      01984000
015930                                                                  01985000
015940     EVALUATE W1C66791-C-NCP-POS-TRAN-CD                          01986000
015950       WHEN WV-C8869-C-RX-REVERSAL-51-D0                          01987000
015960       WHEN WV-C8869-C-RX-1-REVERSAL                              01988000
             WHEN WV-C8869-C-SERVICE-REVERS-D0
015970         MOVE WV-G0145-C-CREDADJREC  TO W1C40541-C-HDR-ID-CD      01989000
015980         MOVE WV-C1031-C-CRED-REQ    TO W1C40541-C-HDR-TY-CD      01990000
015990         MOVE WV-C0161-C-FFS         TO W1C40611-C-BAT-DOC-TY-CD  01991000
016000         MOVE WV-C0070-C-CRE-CLM-CR  TO W1C40541-C-BAT-PYMT-TY-CD 01992000
016010       WHEN WV-C8869-C-ELIG-VF-51-D0                              01993000
016020       WHEN WV-C8869-C-ELIGIBILITY-VERIF                          01994000
016030         MOVE WV-C1031-C-ELIG-VERF-REQ                            01995000
016040                                     TO W1C40541-C-HDR-TY-CD      01996000
016050         MOVE WV-C0161-C-FFS         TO W1C40541-C-BAT-DOC-TY-CD  01997000
016060         MOVE WV-C0070-C-NORM-PAY    TO W1C40541-C-BAT-PYMT-TY-CD 01998000
016070       WHEN WV-C8869-C-RX-BILLING-51-D0                           01999000
MDM-I2       WHEN WV-C8869-C-SERVICE-BILLING-D0
016080       WHEN WV-C8869-C-RX-1-BILLING                               02000000
016090       WHEN WV-C8869-C-RX-2-BILLING                               02001000
016100       WHEN WV-C8869-C-RX-3-BILLING                               02002000
016110       WHEN WV-C8869-C-RX-4-BILLING                               02003000
016120       WHEN WV-C8869-C-RX-1-DOWN-BILLING                          02004000
016130       WHEN WV-C8869-C-RX-2-DOWN-BILLING                          02005000
016140       WHEN WV-C8869-C-RX-3-DOWN-BILLING                          02006000
016150       WHEN WV-C8869-C-RX-4-DOWN-BILLING                          02007000
016160         MOVE WV-G0145-C-PHRM-CLAIM  TO W1C40541-C-HDR-ID-CD      02008000
016170         MOVE WV-C0161-C-FFS         TO W1C40541-C-BAT-DOC-TY-CD  02009000
016180         MOVE WV-C0070-C-NORM-PAY    TO W1C40541-C-BAT-PYMT-TY-CD 02010000
016190       WHEN WV-C8869-C-RX-RE-BILL-51-D0                           02011000
MDM-I2       WHEN WV-C8869-C-SERVICE-REBILL-D0
016200       WHEN WV-C8869-C-RX-1-RE-BILLING                            02012000
016210       WHEN WV-C8869-C-RX-2-RE-BILLING                            02013000
016220       WHEN WV-C8869-C-RX-3-RE-BILLING                            02014000
016230       WHEN WV-C8869-C-RX-4-RE-BILLING                            02015000
016240         MOVE WV-C1031-C-REPL-REQ    TO W1C40541-C-HDR-TY-CD      02016000
016250         MOVE WV-G0145-C-CREDADJREC  TO W1C40541-C-HDR-ID-CD      02017000
016260         MOVE WV-C0161-C-ADJUSTMENT  TO W1C40541-C-BAT-DOC-TY-CD  02018000
016270         MOVE WV-C0070-C-ADJ-CLM-AD  TO W1C40541-C-BAT-PYMT-TY-CD 02019000
016280       WHEN WV-C8869-C-RX-1-RE-BILL-HO                            02020000
016290*      WHEN WV-C8869-C-RX-2-RE-BILL-HO                            02021000
016300*      WHEN WV-C8869-C-RX-3-RE-BILL-HO                            02022000
016310*      WHEN WV-C8869-C-RX-4-RE-BILL-HO                            02023000
016320         MOVE WV-C1031-C-REPL-REQ    TO W1C40541-C-HDR-TY-CD      02024000
016330         MOVE WV-G0145-C-CREDADJREC  TO W1C40541-C-HDR-ID-CD      02025000
016340         MOVE WV-C0161-C-ADJUSTMENT  TO W1C40541-C-BAT-DOC-TY-CD  02026000
016350         MOVE WV-C0070-C-HIS-NO-PAY  TO W1C40541-C-BAT-PYMT-TY-CD 02027000
016360       WHEN WV-C8869-C-PA-REQ-BILL-51-D0                          02028000
016370       WHEN WV-C8869-C-PA-INQ-51-D0                               02029000
016380       WHEN WV-C8869-C-PA-REQ-51-D0                               02030000
016390         MOVE WV-C1031-C-PA-REQ      TO W1C40541-C-HDR-TY-CD      02031000
016400         MOVE WV-C0161-C-FFS         TO W1C40541-C-BAT-DOC-TY-CD  02032000
016410       WHEN WV-C8869-C-PA-REVR-51-D0                              02033000
016420         MOVE WV-C1031-C-PA-REQ      TO W1C40541-C-HDR-TY-CD      02034000
016430         MOVE WV-C0161-C-ADJUSTMENT  TO W1C40541-C-BAT-DOC-TY-CD  02035000
016440     END-EVALUATE.                                                02036000
016450**                                                                02037000
           IF W1C66791-C-NCP-CARDHOLDR-ID = HIGH-VALUES                 PMLMISS
             MOVE SPACES TO W1C66791-C-NCP-CARDHOLDR-ID                 PMLMISS
           END-IF.                                                      PMLMISS
                                                                        PMLMISS
           IF W1C66791-C-NCP-PAT-ID = HIGH-VALUES                       PMLMISS
             MOVE SPACES TO W1C66791-C-NCP-PAT-ID                       PMLMISS
           END-IF.                                                      PMLMISS
                                                                        PMLMISS
           IF W1C66791-C-NCP-SVC-FST-DT-X = HIGH-VALUES                 PMLV1.6
      **     FIELD 401 EDIT 15 EXCP 4023 ****************************   PMLV1.6
             MOVE WV-R1737-C-MISS-INV-DOS TO WW-600-CLM-EXC-CD          PMLV1.6
             MOVE ZEROES                 TO WW-600-CLM-EXC-LI           PMLV1.6
             PERFORM S600C1-000-POST-EXCEPTION                          PMLV1.6
             MOVE ZEROES TO W1C66791-C-NCP-SVC-FST-DT                   PMLV1.6
           END-IF.                                                      PMLV1.6
0
017310     IF W1C66791-C-NCP-POS-TRAN-CD =                              02109000
017320        WV-C8869-C-ELIGIBILITY-VERIF OR                           02110000
017330        WV-C8869-C-ELIG-VF-51-D0                                  02111000
           THEN
017340         GO TO S340-999-EXIT                                      02112000
017350     END-IF.                                                      02113000
017360                                                                  02114000

017420                                                                  02120000
017430                                                                  PML
017530     MOVE W1C66791-C-NCP-CLNT-SEX-CD TO W1C40541-C-SUBM-GENDER-CD MDMI2
                                              W1C40541-B-GENDER-CD.     PML
                                                                        PML
017790     IF W1C66791-C-NCP-CLNT-DOB-DT   NUMERIC                      02169000
017810         MOVE W1C66791-C-NCP-CLNT-DOB-DT                          02170000
017820                                     TO WW-010-DATE               02171000
017830         MOVE WW-010-CCYY            TO WW-010-CCYY-F             02172000
017840         MOVE WW-010-MM              TO WW-010-MM-F               02173000
017850         MOVE WW-010-DD              TO WW-010-DD-F               02174000
017860         MOVE WW-010-DATE-FORMATTED  TO W1C40541-B-DOB-DT         02175000
                                              W1C40541-C-SUBM-DOB-DT    MDMI2
           ELSE                                                         02176000
               MOVE W1C66791-C-NCP-CLNT-DOB-DT TO W1C40541-B-DOB-DT     02177000
017870                                           W1C40541-C-SUBM-DOB-DT MDMI2
           END-IF.                                                      02178000
018190 S340-999-EXIT.                                                   02220000
018200     EXIT.                                                        02221000
018210/                                                                 02222000
018220 S341-000-FORMAT-HDR SECTION.                                     02223000
018230*                                                                 02224000
018240***************************************************************** 02225000
018250*                                                               * 02226000
018260*?  S341-000-FORMAT-HEADER SECTION                              * 02227000
018270*+                                                              * 02228000
018280*+  THIS SECTION FORMATS THE COMMON HEADER RESPONSE FOR CLAIMS  * 02229000
018290*+  REJECTED AT THE HEADER LEVEL.                               * 02230000
018300*+                                                              * 02231000
018310***************************************************************** 02232000
018320*                                                                 02233000
018330 S341-010-START.                                                  02234000
018340*                                                                 02235000
018370     IF   W1C66791-C-NCP-PROV-ID-CD = HIGH-VALUES                 PMLMISS
018380       MOVE SPACES TO W1C40541-P-SVC-PH-ALT-ID-CD                 PMLMISS
           ELSE                                                         PMLMISS
018370     MOVE W1C66791-C-NCP-PROV-ID-CD                               02236000
018380       TO W1C40541-P-SVC-PH-ALT-ID-CD.                            02237000
018490*                                                                 02238000
018500     IF   W1C66791-C-NCP-PROV-ID = HIGH-VALUES                    02239000
018510       MOVE SPACES TO W1C40541-P-SVC-PHARM-ALT-ID                 02240000
           ELSE
018500     MOVE W1C66791-C-NCP-PROV-ID                                  02239000
018510       TO W1C40541-P-SVC-PHARM-ALT-ID.                            02240000
018520*                                                                 02241000
018530     IF W1C66791-C-NCP-SVC-FST-DT-X = HIGH-VALUES                 02242000
018530       MOVE ZEROES TO  W1C66791-C-NCP-SVC-FST-DT                  02242000
           END-IF.

018530     IF W1C66791-C-NCP-SVC-FST-DT NUMERIC                         02242000
           AND                                                          02243000
018540        W1C66791-C-NCP-SVC-FST-DT > 0                             02244000
           THEN                                                         02245000
018550         MOVE W1C66791-C-NCP-SVC-FST-DT                           02246000
018560           TO WW-010-DATE                                         02247000
018570         MOVE WW-010-CCYY                                         02248000
018570           TO WW-010-CCYY-F                                       02249000
018580         MOVE WW-010-MM                                           02250000
018580           TO WW-010-MM-F                                         02251000
018590         MOVE WW-010-DD                                           02252000
018590           TO WW-010-DD-F                                         02253000
018600         MOVE WW-010-DATE-FORMATTED                               02254000
018600           TO W1C40541-C-HDR-SVC-FST-DT                           02255000
018610     END-IF.                                                      02256000

           IF W1C66791-C-NCP-VERSION-NUM = WV-C8897-C-VERSION-D0        01976000
MDM-I2        IF W1C66791-C-NCP-POS-TRAN-CD =                           01977000
MDM-I2                                    WV-C8869-C-RX-BILLING-51-D0
                 MOVE WV-C1031-C-PHARMACY-D0 TO W1C40541-C-HDR-TY-CD
MDM-I2        ELSE
MDM-I2           IF W1C66791-C-NCP-POS-TRAN-CD =                        01977000
MDM-I2                                    WV-C8869-C-SERVICE-BILLING-D0
MDM-I2              MOVE WV-C1031-C-SERVICE-D0 TO W1C40541-C-HDR-TY-CD
MDM-I2           END-IF
MDM-I2        END-IF
           ELSE                                                         01978000
015880        IF W1C66791-C-NCP-VERSION-NUM = WV-C8897-C-VERSION-51     01979000
015890           MOVE WV-C1031-C-PHARMACY-51 TO W1C40541-C-HDR-TY-CD    01980000
015900        ELSE                                                      01981000
015910           MOVE WV-C1031-C-PHARMACY-32 TO W1C40541-C-HDR-TY-CD    01982000
015920        END-IF                                                    01983000
           END-IF.                                                      01984000

018620*                                                                 02257000
018630 S341-999-EXIT.                                                   02258000
018640     EXIT.                                                        02259000
018650/                                                                 02260000
018660 S350-000-FMT-CREDIT-VAR SECTION.                                 02261000
018670*                                                                 02262000
018680***************************************************************** 02263000
018690*                                                               * 02264000
018700*?  S350-000-FMT-CREDIT-VAR SECTION                             * 02265000
018710*+                                                              * 02266000
018720*+  THIS SECTION FORMATS THE VARIABLE PORTION OF THE CREDIT     * 02267000
018730*+  REQUEST.                                                    * 02268000
018740*+                                                              * 02269000
018750***************************************************************** 02270000
018760*                                                                 02271000
018770 S350-010-START.                                                  02272000
018780*                                                                 02273000
018790     IF W1C66791-C-NCP-RX-NUM                                     02274000
018800       (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES              02275000
      **     FIELD 402 EDIT 16 EXCP 4025 ****************************   PMLMISS
             MOVE WV-R1737-C-MISS-RX-NUM TO WW-600-CLM-EXC-CD           PMLMISS
             MOVE ZEROES                 TO WW-600-CLM-EXC-LI           PMLMISS
             PERFORM S600C1-000-POST-EXCEPTION                          PMLMISS
             MOVE SPACES TO  W1C66791-C-NCP-RX-NUM
                            (W1C66791-C-NCPDP-CNSLDTD-RX-X)
           END-IF.

018790     IF W1C66791-C-NCP-RX-NUM                                     02274000
018800       (W1C66791-C-NCPDP-CNSLDTD-RX-X) NUMERIC                    02275000
018810         MOVE W1C66791-C-NCP-RX-NUM                               02276000
018820             (W1C66791-C-NCPDP-CNSLDTD-RX-X)                      02277000
018830                                     TO W1C40611-C-RX-SVC-REF-NUM 02278000
018840     END-IF.                                                      02279000

018850     IF W1C66791-C-NCP-RX-NUM-CD                                  02280000
018860       (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES              02281000
      **     FIELD 455 EDIT EM EXCP 3297 ****************************   PMLMISS
             MOVE WV-R1737-C-MISS-RX-SVC-REF-CD TO WW-600-CLM-EXC-CD    PMLMISS
             MOVE ZEROES                 TO WW-600-CLM-EXC-LI           PMLMISS
             PERFORM S600C1-000-POST-EXCEPTION                          PMLMISS
             MOVE SPACES TO  W1C66791-C-NCP-RX-NUM-CD
                            (W1C66791-C-NCPDP-CNSLDTD-RX-X)
           END-IF.

PML001**   NO NO NO.  WE DO NOT SET C-RX-SVC-REF-CD TO RX-BILLING WHEN  COR3013
PML001**   NCP-RX-NUM-CD IS MISSING / NOT SPECIFIED.                    COR3013
PML001*    IF W1C66791-C-NCP-RX-NUM-CD                                  COR3013
PML001*      (W1C66791-C-NCPDP-CNSLDTD-RX-X) = SPACES                   COR3013
PML001*        MOVE WV-C9049-C-RX-BILLING  TO W1C40611-C-RX-SVC-REF-CD  COR3013
PML001*    ELSE                                                         COR3013
               MOVE W1C66791-C-NCP-RX-NUM-CD
PML001             (W1C66791-C-NCPDP-CNSLDTD-RX-X)
                                           TO W1C40611-C-RX-SVC-REF-CD. COR3013
MDM-I2     IF W1C66791-C-NCP-PMOD-NUM                                   COR3015
MDM-I2           (W1C66791-C-NCPDP-CNSLDTD-RX-X) > 0                    COR3015
MDM-I2**     FIELD 458 EDIT    EXCP 3910 *************************      COR3015
MDM-I2       MOVE WV-R1737-C-INV-PMOD-CNT-REV TO WW-600-CLM-EXC-CD      COR3015
MDM-I2       MOVE ZEROES                      TO WW-600-CLM-EXC-LI      COR3015
MDM-I2       PERFORM S600C1-000-POST-EXCEPTION                          COR3015
MDM-I2     END-IF.                                                      COR3015
MDM-I2                                                                  COR3015
MDM-I2     IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-RX-REVERSAL-51-D0 COR3015
MDM-I2       IF W1C66791-C-NCP-PPS-FEE-AMT-X                            COR3015
MDM-I2         (W1C66791-C-NCPDP-CNSLDTD-RX-X) < HIGH-VALUES            COR3015
MDM-I2**     FIELD 477 EDIT    EXCP 3918 *************************      COR3015
MDM-I2         MOVE WV-R1737-C-INV-SUBM-PPS-FEE TO WW-600-CLM-EXC-CD    COR3015
MDM-I2         MOVE ZEROES                      TO WW-600-CLM-EXC-LI    COR3015
MDM-I2         PERFORM S600C1-000-POST-EXCEPTION                        COR3015
MDM-I2       END-IF                                                     COR3015
MDM-I2     END-IF.                                                      COR3015
MDM-I2                                                                  COR3015
MDM-I2     IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-SERVICE-REVERS-D0 COR3015
MDM-I2       IF W1C66791-C-NCP-PPS-FEE-AMT-X                            COR3015
MDM-I2         (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES            COR3015
MDM-I2**     FIELD 477 EDIT    EXCP 4214 *************************      COR3015
MDM-I2         MOVE WV-R1737-C-MISS-PPS-FEE TO WW-600-CLM-EXC-CD        COR3015
MDM-I2         MOVE ZEROES                  TO WW-600-CLM-EXC-LI        COR3015
MDM-I2         PERFORM S600C1-000-POST-EXCEPTION                        COR3015
MDM-I2       END-IF                                                     COR3015
MDM-I2     END-IF.                                                      COR3015
MDM-I2                                                                  COR3015
MDM-I2     IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-SERVICE-REVERS-D0 COR3015
MDM-I2       IF W1C66791-C-NCP-PPS-FEE-AMT                              COR3015
MDM-I2         (W1C66791-C-NCPDP-CNSLDTD-RX-X) < 0                      COR3015
MDM-I2**     FIELD 477 EDIT    EXCP 3253 *************************      COR3015
MDM-I2         MOVE WV-R1737-C-INV-PPS-FEE TO WW-600-CLM-EXC-CD         COR3015
MDM-I2         MOVE ZEROES                 TO WW-600-CLM-EXC-LI         COR3015
MDM-I2         PERFORM S600C1-000-POST-EXCEPTION                        COR3015
MDM-I2       END-IF                                                     COR3015
MDM-I2     END-IF.                                                      COR3015
MDM-I2                                                                  COR3015
MDM-I2     IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-SERVICE-REVERS-D0 COR3015
MDM-I2       IF W1C66791-C-NCP-INCNTV-AMT-X                             COR3015
MDM-I2         (W1C66791-C-NCPDP-CNSLDTD-RX-X) < HIGH-VALUES            COR3015
MDM-I2**     FIELD 438 EDIT    EXCP 3917 *************************      COR3015
MDM-I2         MOVE WV-R1737-C-INV-INCNTV-AMT TO WW-600-CLM-EXC-CD      COR3015
MDM-I2         MOVE ZEROES                    TO WW-600-CLM-EXC-LI      COR3015
MDM-I2         PERFORM S600C1-000-POST-EXCEPTION                        COR3015
MDM-I2       END-IF                                                     COR3015
MDM-I2     END-IF.                                                      COR3015
018930*                                                                 02288000
018940 S350-999-EXIT.                                                   02289000
018950     EXIT.                                                        02290000
018960/                                                                 02291000
018970 S360-000-FMT-DRUG-VAR SECTION.                                   02292000
018980*                                                                 02293000
018990***************************************************************** 02294000
019000*                                                               * 02295000
019010*?  S360-000-FMT-DRUG-VAR SECTION                               * 02296000
019020*+                                                              * 02297000
019030*+  THIS SECTION FORMATS THE VARIABLE PORTION OF THE PHARMACY   * 02298000
019040*+  REQUEST.                                                    * 02299000
019050*+                                                              * 02300000
019060***************************************************************** 02301000
019070*                                                                 02302000
019080 S360-010-START.                                                  02303000
019090*                                                                 02304000

           IF W1C66791-C-NCP-RX-NUM                                     02305000
             (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES              02306000
             MOVE SPACES TO  W1C66791-C-NCP-RX-NUM
                            (W1C66791-C-NCPDP-CNSLDTD-RX-X)
           END-IF.

FIXSHT*    IF W1C66791-C-NCP-RX-NUM                                     02305000
FIXSHT*      (W1C66791-C-NCPDP-CNSLDTD-RX-X) NUMERIC                    02306000
FIXSHT         MOVE FUNCTION NUMVAL(W1C66791-C-NCP-RX-NUM               02307000
FIXSHT                              (W1C66791-C-NCPDP-CNSLDTD-RX-X))    02308000
FIXSHT           TO W1C40611-C-RX-SVC-REF-NUM                           02309000
FIXSHT*    END-IF.                                                      02310000

           IF W1C66791-C-NCP-RX-NUM-CD
             (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES
             MOVE SPACES TO  W1C66791-C-NCP-RX-NUM-CD
                            (W1C66791-C-NCPDP-CNSLDTD-RX-X)
           END-IF.


           MOVE W1C66791-C-NCP-PRSC-ID-CD                               02367000
019700         (W1C66791-C-NCPDP-CNSLDTD-RX-X)                          02368000
                                       TO W1C40541-P-PRSC-ALT-ID-CD.    02369000
019910                                                                  02370000
019920     IF   W1C66791-C-NCP-PRSC-ID                                  02371000
019930         (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES            02372000
      **     FIELD 411 EDIT    EXCP 4040 ****************************   COR3013
             MOVE WV-R1737-C-MISS-PRSC-ID TO WW-600-CLM-EXC-CD          COR3013
             MOVE ZEROES                  TO WW-600-CLM-EXC-LI          COR3013
             PERFORM S600C1-000-POST-EXCEPTION                          COR3013
             MOVE SPACES TO  W1C66791-C-NCP-PRSC-ID
                            (W1C66791-C-NCPDP-CNSLDTD-RX-X)
           END-IF.


           PERFORM S495-000-POST-4642. 

PMLSIT     EVALUATE TRUE
PMLSIT       WHEN W1C66791-C-NCP-SUB-OVD-CNT-X                          02486000
PMLSIT             (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES        02487000
PMLSIT         MOVE ZEROES TO W1C66791-C-NCP-SUB-OVD-CNT
PMLSIT                       (W1C66791-C-NCPDP-CNSLDTD-RX-X)
PML003       WHEN W1C66791-C-NCP-SUB-OVD-CNT                            02486000
PMLSIT             (W1C66791-C-NCPDP-CNSLDTD-RX-X) NOT NUMERIC          02487000
PMLSIT         MOVE ZEROES TO W1C66791-C-NCP-SUB-OVD-CNT
PMLSIT                       (W1C66791-C-NCPDP-CNSLDTD-RX-X)
PMLSIT       WHEN W1C66791-C-NCP-SUB-OVD-CNT                            486000
PMLSIT             (W1C66791-C-NCPDP-CNSLDTD-RX-X) NUMERIC              02487000
PMLSIT         IF W1C66791-C-NCP-SUB-OVD-CNT
PMLSIT           (W1C66791-C-NCPDP-CNSLDTD-RX-X) < 0
PMLSIT           MOVE WV-R1737-C-RX-OVD-CNT-ERR TO WW-600-CLM-EXC-CD
PMLSIT           MOVE ZEROES                    TO WW-600-CLM-EXC-LI       MDM-I2
PMLSIT           PERFORM S600C1-000-POST-EXCEPTION                          MDM-I2
PMLSIT           COMPUTE   W1C66791-C-NCP-SUB-OVD-CNT
PMLSIT                    (W1C66791-C-NCPDP-CNSLDTD-RX-X)
PMLSIT                   = W1C66791-C-NCP-SUB-OVD-CNT
PMLSIT                    (W1C66791-C-NCPDP-CNSLDTD-RX-X) * -1
PMLSIT         END-IF
PMLSIT     END-EVALUATE.

           MOVE W1C66791-C-NCP-SUB-OVD-CNT                              02486000
                   (W1C66791-C-NCPDP-CNSLDTD-RX-X)                      02487000
                                         TO W1C40541-C-DRUG-RX-OVRD-CNT.02488000
      **  RX-OVRRD IS IN 5.1 & D.0. HAS A 'NOT ENTERED' EDIT AND        DF3046
      **  IS USED IN MULTIPLE EDITS IN COMBINATION WITH OTHER FIELDS.   DF3046
      **  MUST COMMUNICATE DOWNSTREAM WHETHER ENTERED OR NOT.           DF3046
      **  LEAVE AS HIGH-VALUES WHEN NOT ENTERED.  PDDC8000 WILL         DF3046
      **  DETECT THIS, INITIALIZE FIELD TO SPACES, AND THROW SWITCH.    DF3046
           IF W1C66791-C-NCP-RX-DY-OVD-CD                               COR3013
                 (W1C66791-C-NCPDP-CNSLDTD-RX-X 1) = HIGH-VALUES        COR3013
             MOVE WV-R1737-C-INV-RX-DY-OVD-CD-1 TO WW-600-CLM-EXC-CD    COR3013
             MOVE ZEROES                        TO WW-600-CLM-EXC-LI    COR3013
             PERFORM S600C1-000-POST-EXCEPTION                          COR3013
           END-IF.                                                      COR3013


020720     IF W1C66791-C-NCP-GROSS-AMT                                  02525000
020730       (W1C66791-C-NCPDP-CNSLDTD-RX-X) NUMERIC                    02526000
           THEN                                                         02527000
020720       IF W1C66791-C-NCP-GROSS-AMT                                02525000
020730         (W1C66791-C-NCPDP-CNSLDTD-RX-X) < +0.00                  COR3013
      **       FIELD 430 EDIT DU EXCP 3279 ***********************      PMLMISS
               MOVE WV-R1737-C-INV-GROSS-AMT  TO WW-600-CLM-EXC-CD      PMLMISS
               MOVE ZEROES                    TO WW-600-CLM-EXC-LI      PMLMISS
               PERFORM S600C1-000-POST-EXCEPTION                        PMLMISS
020740       ELSE
020720         IF W1C66791-C-NCP-GROSS-AMT                              02525000
020730           (W1C66791-C-NCPDP-CNSLDTD-RX-X) = +0.00                PMLMISS
                 MOVE W1C66791-C-NCP-GROSS-AMT                          02528000
020750               (W1C66791-C-NCPDP-CNSLDTD-RX-X)                    02529000
020760                                    TO W1C40541-C-TOT-CHRG-AMT    02530000
020860                                       W1C40541-C-DRUG-GROSS-AMT  PML0104
               END-IF
             END-IF
020790     END-IF.                                                      02533000
020800                                                                  02534000
020810     IF W1C66791-C-NCP-UAC-AMT                                    02535000
020820       (W1C66791-C-NCPDP-CNSLDTD-RX-X) NUMERIC                    02536000
           THEN                                                         02537000
020830         MOVE W1C66791-C-NCP-UAC-AMT                              02538000
020840             (W1C66791-C-NCPDP-CNSLDTD-RX-X)                      02539000
020850                                   TO W1C40541-C-UC-CHARGE-AMT    02540000
           ELSE
      **        FIELD 426 EDIT DQ EXCP 3276 ***********************
                MOVE WV-R1737-C-INV-UC-CHRGE-AMT TO WW-600-CLM-EXC-CD   PMLV1.6
                MOVE ZEROES                    TO WW-600-CLM-EXC-LI     PMLMISS
                PERFORM S600C1-000-POST-EXCEPTION                       PMLMISS
                MOVE ZEROES              TO W1C40541-C-UC-CHARGE-AMT
020880     END-IF.                                                      02543000
020890                                                                  02544000


           IF W1C40541-C-DRUG-CMPND-CD = WV-C0824-C-COMPOUND            CR14386
             IF WS-FIRST-TIME-CMPND                                     CR14386
              PERFORM S1100-000-CMPND-CONFIG-7506                       CR14386
              SET WS-NOT-FIRST-TIME-CMPND   TO TRUE                     CR14386
              IF WH-000-SYSLIST-EXIST  = 'Y'                            CR14386
                 MOVE FUNCTION NUMVAL (GSLDTLTB-G-LIST-STRT-2-LMT)      CR14386
                                       TO WW-100-CLM-AGE-BEG-RNG        CR14386
                 MOVE FUNCTION NUMVAL (GSLDTLTB-G-LIST-END-2-LMT)       CR14386
                                       TO WW-100-CLM-AGE-END-RNG        CR14386
                 MOVE FUNCTION NUMVAL (GSLDTLTB-G-LIST-STRT-3-LMT)      CR14386
                                       TO WW-100-CLM-MH-ALLOW-THR       CR14386
                 MOVE FUNCTION NUMVAL (GSLDTLTB-G-LIST-STRT-4-LMT)      CR14386
                                       TO WW-100-CLM-LMT-EXC-CD         CR14386
                 IF ((W1C40541-C-HDR-CLNT-AGE  >=                       CR14386
                                           WW-100-CLM-AGE-BEG-RNG)      CR14386
                                      AND                               CR14386
                     (W1C40541-C-HDR-CLNT-AGE  <=                       CR14386
                                           WW-100-CLM-AGE-END-RNG))     CR14386
                   IF WS-150-APPRV-LINE-ITEM-AMT  >=                    CR14386
                                          WW-100-CLM-MH-ALLOW-THR       CR14386
                    IF W1C52991-R-CLM-EXC-DISP-CD  OF                   CR14386
                       W1C52991-C-CNTL-EXC-LI-VW(WW-100-CLM-LMT-EXC-CD, CR14386
                       W1C52991-C-CNTL-EXC-LI-VW-X) = SPACE             CR14386
                           CONTINUE                                     CR14386
                    ELSE                                                CR14386
                       MOVE WW-100-CLM-LMT-EXC-CD TO WW-600-CLM-EXC-IDX CR14386
                       MOVE ZEROS  TO  WW-600-CLM-EXC-LI                CR14386
                       PERFORM S600C1-000-POST-EXCEPTION                CR14386
                       SET WS-00-REPRICE-CLAIM-YES TO TRUE              CR14386
                    END-IF                                              CR14386
                   END-IF                                               CR14386
                 END-IF                                                 CR14386
              END-IF                                                    CR14386
             END-IF                                                     CR14386
           END-IF.                                                      CR14386

DF2198     IF W1C40541-C-TOT-PAT-RESP-AMT  > WL-807-R-MAX-COPAY-WRAP
PML001        PERFORM S416-POST-4609-3604-IDX                           COR3013
           ELSE                                                         COR0975
              CONTINUE                                                  COR0975
           END-IF.                                                      COR0981

              PERFORM S820-000-GET-BENEFIT-STAT                         DF2453
			  
              IF WS-000-BENEFIT-STAT-FOUND                              DF2453
035110        AND (BBENEFTB-B-BENE-CAP-STAT-CD =                        DF2453
035120           WV-B2372-C-BENEFIT-MET OR WV-B2372-C-BENEFIT-MET-MAN)  DF2453  
035130            MOVE ZEROS  TO W1C40541-C-TOT-REIMB-AMT               DF2453
035150            IF W1C52991-R-CLM-EXC-DISP-CD OF                      DF2453
035160               W1C52991-C-CNTL-EXC-HDR-VW                         DF2453
035170              (WK-806-3353-IDX) = SPACE                           DF2453
035180            OR WT-00-3353-POSTED-YES                              DF2453
035190               CONTINUE                                           DF2453
035200            ELSE                                                  DF2453
035210               MOVE WK-806-3353-IDX  TO  WW-600-CLM-EXC-IDX       DF2453
035220               MOVE +0  TO  WW-600-CLM-EXC-LI                     DF2453
035230               SET WT-00-3353-POSTED-YES TO TRUE                  DF2453
035240               SET WS-00-REPRICE-CLAIM-YES TO TRUE                DF2453
035250               PERFORM S600-INSERT-EXCEPTION                      DF2453
035260           END-IF                                                 DF2453
DF3736           IF WL-807-B-BENEFIT-TY-CD = '2'                        DF3736 
DF3736                 IF W1C52991-R-CLM-EXC-DISP-CD OF                 DF3736
DF3736                    W1C52991-C-CNTL-EXC-HDR-VW                    DF3736
DF3736                   (WK-806-3053-IDX) = SPACE                      DF3736
DF3736                    CONTINUE                                      DF3736
DF3736                 ELSE                                             DF3736
DF3736                    MOVE WK-806-3053-IDX  TO  WW-600-CLM-EXC-IDX  DF3736
DF3736                    MOVE +0  TO  WW-600-CLM-EXC-LI                DF3736
DF3736                    SET WS-00-REPRICE-CLAIM-YES TO TRUE           DF3736
DF3736                    PERFORM S600-INSERT-EXCEPTION                 DF3736
DF3736                    CONTINUE                                      DF3736
DF3736                 END-IF                                           DF3736
DF3736           ELSE IF WL-807-B-BENEFIT-TY-CD = '3'                   DF3736
DF3736                 IF W1C52991-R-CLM-EXC-DISP-CD OF                 DF3736
DF3736                    W1C52991-C-CNTL-EXC-HDR-VW                    DF3736
DF3736                   (WK-806-3061-IDX) = SPACE                      DF3736
DF3736                    CONTINUE                                      DF3736
DF3736                 ELSE                                             DF3736
DF3736                    MOVE WK-806-3061-IDX  TO  WW-600-CLM-EXC-IDX  DF3736
DF3736                    MOVE +0  TO  WW-600-CLM-EXC-LI                DF3736
DF3736                    SET WS-00-REPRICE-CLAIM-YES TO TRUE           DF3736
DF3736                    PERFORM S600-INSERT-EXCEPTION                 DF3736
DF3736                 END-IF                                           DF3736
DF3736           END-IF                                                 DF3736
035280        END-IF.                                                   DF2453

           SET  W1C40541-C-LI-DRUG-DTL-VW-X TO  WS-000-LI-SUB.          CR11078
                                                                        CR11078
           IF   W1C40541-C-DRUG-DEA-CD (W1C40541-C-LI-DRUG-DTL-VW-X)    CR11078
                                      = WV-R0314-C-SCHED2-MOST-ABUSED   CR11078
              THEN                                                      CR11078
                PERFORM S925-000-SCC-VALID                              CR12914
           END-IF.                                                      CR12914
      *                                                                 CR12914


023490 S360-999-EXIT.                                                   02945000
023500     EXIT.                                                        02946000
                                                                        PML-A
023520 S365-000-FMT-DRUG-LI SECTION.                                    02948000
023530*                                                                 02949000
023540***************************************************************** 02950000
023550*                                                               * 02951000
023560*?  S365-000-FMT-DRUG-LI SECTION                                * 02952000
023570*+                                                              * 02953000
023580*+  THIS SECTION FORMATS THE LINE ITEM OF THE PHARMACY REQUEST. * 02954000
023590*+                                                              * 02955000
023600***************************************************************** 02956000
023610*                                                                 02957000
023620 S365-010-START.                                                  02958000
023630*                                                                 02959000
023640     SET W1C40541-C-LI-NUM        OF W1C40541-C-LI-DRUG-DTL-VW    02960000
023650                                    (W1C40541-C-LI-DRUG-DTL-VW-X) 02961000
023660                                  TO W1C40541-C-LI-DRUG-DTL-VW-X. 02962000
023670     SET W1C66791-C-NCP-CMPD-GRP-X                                02963000
023680                                  TO W1C40541-C-LI-DRUG-DTL-VW-X. 02964000
023690     MOVE W1C40541-C-LI-NUM       OF W1C40541-C-LI-DRUG-DTL-VW    02965000
023700                                    (W1C40541-C-LI-DRUG-DTL-VW-X) 02966000
023710                                  TO W1C40541-C-CNT-LI-NUM.       02967000
023720                                                                  02968000
023730*    ACCEPTING UPC, HRI, AND NDC AND MOVING TO R-DRUG-CD          02969000

           IF W1C66791-C-NCP-CMPD-NUM
             (W1C66791-C-NCPDP-CNSLDTD-RX-X) > 0
             IF W1C66791-C-NCP-CMPD-PROD-CD
               (W1C66791-C-NCPDP-CNSLDTD-RX-X,
                W1C66791-C-NCP-CMPD-GRP-X) = HIGH-VALUES
      **      FIELD 488 EDIT    EXCP 3154 *********************
               MOVE WV-R1737-C-MISS-CMPD-PRD-ID-CD TO WW-600-CLM-EXC-CD
               MOVE ZEROES                         TO WW-600-CLM-EXC-LI
023740         PERFORM S600C1-000-POST-EXCEPTION                        02970000
             END-IF
           END-IF.

023750     IF WS-000-COMPOUND-NO                                        02971000
023760       IF W1C66791-C-NCP-PROD-ID-CD
023770         (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES
      **       FIELD 488 EDIT    EXCP 4248 *********************        COR3013
               MOVE WV-R1737-C-MISS-PRD-SVC-ID-CD TO WW-600-CLM-EXC-CD  COR3013
               MOVE ZEROES                        TO WW-600-CLM-EXC-LI  COR3013
               PERFORM S600C1-000-POST-EXCEPTION                        COR3013
               MOVE SPACES TO  W1C66791-C-NCP-PROD-ID-CD
                              (W1C66791-C-NCPDP-CNSLDTD-RX-X)
             END-IF
023760       IF W1C66791-C-NCP-PROD-ID-CD                               02972000
023770         (W1C66791-C-NCPDP-CNSLDTD-RX-X) =                        02973000
023780                                   WV-C1567-C-UNIVER-PROD-CD      02974000
023790                                OR WV-C1567-C-HLTH-RELATED-ITEM   02975000
023800                                OR WV-C1567-C-NATIONAL-DRUG-CODE  02976000
023810                                OR WV-C1567-C-NOT-SPEC-BLANK      02977000
MDM-I2                                OR WV-C1567-C-HCFA-COMM-PROC
             THEN                                                       02978000
023820           IF   W1C66791-C-NCP-PROD-ID
023830               (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES
                   MOVE SPACES TO  W1C66791-C-NCP-PROD-ID
                                  (W1C66791-C-NCPDP-CNSLDTD-RX-X)
                 END-IF
023820           MOVE W1C66791-C-NCP-PROD-ID                            02979000
023830               (W1C66791-C-NCPDP-CNSLDTD-RX-X)                    02980000
023840                                  TO WW-020-NDC                   02981000
023850           IF WW-020-NDC-1-11 NUMERIC                             02982000
023860               MOVE WW-020-NDC-1-11                               02983000
023870                                  TO W1C40541-R-DRUG-CD           02984000
023880                                    (W1C40541-C-LI-DRUG-DTL-VW-X) 02985000
023890           END-IF                                                 02986000
023900       END-IF                                                     02987000
023910     ELSE                                                         02988000
023920       IF W1C66791-C-NCP-CMPD-PROD-CD                             02989000
023930         (W1C66791-C-NCPDP-CNSLDTD-RX-X,                          02990000
023940          W1C66791-C-NCP-CMPD-GRP-X) = HIGH-VALUES                02991000
      **       FIELD 488 EDIT 43 EXCP 4337 *********************        PMLV1.10
               MOVE WV-R1737-C-MISS-CMPD-PROD-CD TO WW-600-CLM-EXC-CD   PMLV1.10
               MOVE ZEROES                       TO WW-600-CLM-EXC-LI   PMLV1.10
               PERFORM S600C1-000-POST-EXCEPTION                        PMLV1.10
      **       FIELD 488 EDIT    EXCP 4248 *********************        COR3013
               MOVE WV-R1737-C-MISS-PRD-SVC-ID-CD TO WW-600-CLM-EXC-CD  COR3013
               MOVE ZEROES                        TO WW-600-CLM-EXC-LI  COR3013
               PERFORM S600C1-000-POST-EXCEPTION                        COR3013
               MOVE SPACES TO  W1C66791-C-NCP-CMPD-PROD-CD
                               (W1C66791-C-NCPDP-CNSLDTD-RX-X,
                                W1C66791-C-NCP-CMPD-GRP-X)
             END-IF
023920       IF W1C66791-C-NCP-CMPD-PROD-CD                             02989000
023930         (W1C66791-C-NCPDP-CNSLDTD-RX-X,                          02990000
023940          W1C66791-C-NCP-CMPD-GRP-X) =                            02991000
023950                                    WV-C1567-C-UNIVER-PROD-CD     02992000
023960                                 OR WV-C1567-C-HLTH-RELATED-ITEM  02993000
023970                                 OR WV-C1567-C-NATIONAL-DRUG-CODE 02994000
023980                                 OR WV-C1567-C-NOT-SPEC-BLANK     02995000
             THEN                                                       02996000
023990           IF   W1C66791-C-NCP-CMPD-PROD-ID                       02997000
024000               (W1C66791-C-NCPDP-CNSLDTD-RX-X,                    02998000
024010                W1C66791-C-NCP-CMPD-GRP-X) = HIGH-VALUES          02999000
                   MOVE SPACES TO  W1C66791-C-NCP-CMPD-PROD-ID
023990                            (W1C66791-C-NCPDP-CNSLDTD-RX-X,       02997000
024000                             W1C66791-C-NCP-CMPD-GRP-X)           02998000
024010           END-IF                                                 02999000
023990           MOVE W1C66791-C-NCP-CMPD-PROD-ID                       02997000
024000               (W1C66791-C-NCPDP-CNSLDTD-RX-X,                    02998000
024010                W1C66791-C-NCP-CMPD-GRP-X)                        02999000
024020                                  TO WW-020-NDC                   03000000
024030           IF WW-020-NDC-1-11 NUMERIC                             03001000
024040               MOVE WW-020-NDC-1-11                               03002000
024050                                  TO W1C40541-R-DRUG-CD           03003000
024060                                    (W1C40541-C-LI-DRUG-DTL-VW-X) 03004000
024070           END-IF                                                 03005000
024080       END-IF                                                     03006000
024090     END-IF.                                                      03007000
                                                                        03008000
025460*                                                                 03240000
025470 S365-999-EXIT.                                                   03241000
025480     EXIT.                                                        03242000


PML001 S416-POST-4609-3604-IDX   SECTION.                               COR3013
      *****************************************************************
      *                                                               *
      *****************************************************************
       S416-000-START.                                                  COR0828

           IF WL-807-MCARE-DRUG-EXC-YES                                 CR6791
              GO TO S416-999-EXIT                                       CR6791
           END-IF.                                                      CR6791

           IF W1C52991-R-CLM-EXC-DISP-CD  OF                            COR0763
042680        W1C52991-C-CNTL-EXC-HDR-VW (WK-806-4609-IDX) = SPACE      COR0763
           OR WT-00-4609-POSTED-YES                                     COR0843
042700         CONTINUE                                                 COR0763
           ELSE                                                         COR0763
              IF (W1C40541-C-DRUG-RX-OVRRD-CD = WV-C0874-C-OTHER        COR0981
               OR W1C40541-C-DRUG-RX-OVR2-CD  = WV-C9475-C-OTHER
               OR W1C40541-C-DRUG-RX-OVR3-CD  = WV-C2299-C-OTHER)
                 MOVE WV-C4200-C-IGNORE                                 COR0981
                   TO W1C52991-R-CLM-EXC-DISP-CD OF                     COR0981
                      W1C52991-C-CNTL-EXC-HDR-VW                        COR0981
                      (WK-806-4609-IDX)                                 COR0981
              ELSE                                                      COR0981
                 SET WS-00-REPRICE-CLAIM-YES TO TRUE                    COR0975
              END-IF                                                    COR0981
              SET WT-00-4609-POSTED-YES TO TRUE                         COR0975
              MOVE WK-806-4609-IDX  TO  WW-600-CLM-EXC-IDX              COR0828
              MOVE +0  TO  WW-600-CLM-EXC-LI                            COR0828
              PERFORM S600-INSERT-EXCEPTION                             COR0828
           END-IF.                                                      COR0763

           IF W1C52991-R-CLM-EXC-DISP-CD  OF                            COR0763
042680        W1C52991-C-CNTL-EXC-HDR-VW (WK-806-3604-IDX) = SPACE      COR0763
           OR WT-00-3604-POSTED-YES                                     COR0843
042700         CONTINUE                                                 COR0763
           ELSE                                                         COR0763
              IF (W1C40541-C-DRUG-RX-OVRRD-CD = WV-C0874-C-OTHER        COR0981
               OR W1C40541-C-DRUG-RX-OVR2-CD  = WV-C9475-C-OTHER
               OR W1C40541-C-DRUG-RX-OVR3-CD  = WV-C2299-C-OTHER)
                 MOVE WV-C4200-C-IGNORE                                 DF2744
                   TO W1C52991-R-CLM-EXC-DISP-CD OF                     DF2744
                      W1C52991-C-CNTL-EXC-HDR-VW                        DF2744
                      (WK-806-3604-IDX)                                 DF2744
              ELSE                                                      DF2744
                 SET WS-00-REPRICE-CLAIM-YES TO TRUE                    COR0975
              END-IF                                                    COR0981
              SET WT-00-3604-POSTED-YES TO TRUE                         COR0975
PML001        MOVE WK-806-3604-IDX  TO  WW-600-CLM-EXC-IDX              COR3013
PML001        MOVE +0               TO  WW-600-CLM-EXC-LI               COR3013
PML001        PERFORM S600-INSERT-EXCEPTION                             COR3013
           END-IF.                                                      COR0763

037030 S416-999-EXIT.                                                   COR0828
037040     EXIT.                                                        COR0828

054150 S495-000-POST-4642 SECTION.                                      COR3009
054200 S495-010-START.                                                  COR3009
                                                                        COR3009
           SET WS-010-MEDB-OVERRIDE-NOTFND TO TRUE.                     COR3009
                                                                        COR3009
012540     IF W1C52991-R-CLM-EXC-DISP-CD  OF                            COR3009
012550         W1C52991-C-CNTL-EXC-LI-VW (WK-806-4642-IDX,              COR3009
012560         W1C52991-C-CNTL-EXC-LI-VW-X) = SPACES                    COR3009
012570         GO TO S495-999-EXIT                                      COR3009
012580     END-IF.                                                      COR3009
                                                                        COR3009
R10003     IF (W1C40541-C-OTHR-INSR-IND = WV-C3078-C-PAYMENT-COLLECTED  COR1097
                                      OR  WV-C3078-C-PMT-NOT-COLLECTED) COR1097
           AND WS-PAYER-ID-SENT                                         COR1097
           THEN                                                         COR1097
              IF WS-COVERAGE-MEDICARE-B-YES                             COR1097
              OR WS-COVERAGE-MEDICARE-C-YES                             DF3505
              OR WS-COVERAGE-MEDICARE-D-YES                             DF3505
              THEN                                                      COR1097
                 SET WS-010-MEDB-OVERRIDE-FND TO TRUE                   COR1097
              END-IF                                                    COR1097
           END-IF.                                                      COR1097
                                                                        CR14172
           IF (W1C40541-C-OTHR-INSR-IND = WV-C3078-C-PAYMENT-COLLECTED  CR14172
                                      OR  WV-C3078-C-PMT-NOT-COLLECTED) CR14172
              PERFORM VARYING WA-COB-SUB FROM +1 BY +1                  CR14172
                UNTIL WA-COB-SUB > W1C40541-C-CNT-COB-ID-NUM            CR14172
                 IF WS-PAYER03-ID-SENT                                  CR14172
                    MOVE '8909' TO GSLDTLTB-G-LIST-NUM                  CR14172
                    MOVE W1C40541-C-PAYER-ID(WA-COB-SUB)                CR14172
                      TO GSLDTLTB-G-LIST-STRT-LMT                       CR14172
                         GSLDTLTB-G-LIST-END-LMT                        CR14172
                    PERFORM S545-000-GET-SYSLIST-OCC0204                CR14172
                    IF WH-000-OCC24-SYSLIST-EXIST = 'Y'                 CR14172
                    THEN                                                CR14172
                       SET WS-010-MEDB-OVERRIDE-FND TO TRUE             CR14172
                    END-IF                                              CR14172
                 END-IF                                                 CR14172
              END-PERFORM                                               CR14172
           END-IF.                                                      CR14172
                                                                        CR14172
                                                                        COR1097
           IF WS-010-MEDB-OVERRIDE-FND                                  COR1097
              MOVE WV-C4200-C-IGNORE                                    COR1097
                TO W1C52991-R-CLM-EXC-DISP-CD                           COR1097
                OF W1C52991-C-CNTL-EXC-LI-VW (WK-806-4642-IDX,          COR1097
                   W1C52991-C-CNTL-EXC-LI-VW-X)                         COR1097
           END-IF.                                                      COR1097
                                                                        COR1097
012590           MOVE WK-806-4642-IDX TO WW-600-CLM-EXC-IDX             COR1097
012610           SET  WW-600-CLM-EXC-LI TO W1C52991-C-CNTL-EXC-LI-VW-X  COR1097
012630           PERFORM S600-000-INSERT-EXCEPTION                      COR1097
                 MOVE WV-T2558-C-MEDICARE-B                             DF3471
                   TO WH-060-TPL-CVRG-CD                                DF3471
                 MOVE WK-806-4642-IDX                                   COR1097
                   TO WH-060-CLM-EXC-CD-NUM                             COR1097
                 PERFORM S550-000-FETCH-RESP-CARR-ID                    COR1097
                 SET WS-4642-POSTED TO TRUE                             DF3053
                                                                        COR1097
054310                                                                  COR3009
054320 S495-999-EXIT.                                                   COR3009
054330     EXIT.                                                        COR3009

025490                                                                  03243000
025500                                                                  03244000
026080/                                                                 03366000
029400 S600-000-INSERT-EXC-CODE SECTION.                                03912000

000630 S600-010-START.
000640
000650     IF W1C40541-R-CUST-PART-NUM > ZERO
000660       MOVE W1C40541-R-CUST-PART-NUM
000670                           TO RCEDS2TB-R-CUST-PART-NUM
000680     ELSE
000690       MOVE 0
000700                           TO RCEDS2TB-R-CUST-PART-NUM
000710     END-IF
000720
000730     MOVE WW-000-INT-EXC-CD
000740                           TO RCEDS2TB-R-CLM-EXC-CD
                                    WW-600-CLM-EXC-CD
000750
000760     IF W1C40541-R-GROUP-ID > SPACES
000770       MOVE W1C40541-R-GROUP-ID
000780                           TO RCEDS2TB-R-GROUP-ID
000790     ELSE
000800       MOVE 'ALL     '
000810                           TO RCEDS2TB-R-GROUP-ID
000820     END-IF
000830
000840     MOVE W1C40541-R-PLAN-ID OF W1C40541-C-LI-DRUG-MAIN-VW
000850                           TO RCEDS2TB-R-PLAN-ID
000860
000870     MOVE 'C'
000880                           TO RCEDS2TB-C-BAT-DOC-TY-CD
000890
000900     IF W1C40541-C-BAT-MED-SRC-CD > SPACES
000910       MOVE W1C40541-C-BAT-MED-SRC-CD
000920                           TO RCEDS2TB-C-BAT-MED-SRC-CD
000930     ELSE
000940       MOVE WW-600-DEFAULT-BAT-MED-SRC-CD
000950                           TO RCEDS2TB-C-BAT-MED-SRC-CD
000960     END-IF
000970
           IF W1C66791-C-NCP-VERSION-NUM = WV-C8897-C-VERSION-D0
MDM-I2        IF W1C66791-C-NCP-POS-TRAN-CD =                           01977000
MDM-I2                                    WV-C8869-C-RX-BILLING-51-D0
                 MOVE WV-C1031-C-PHARMACY-D0
                   TO W1C40541-C-HDR-TY-CD
MDM-I2        ELSE
MDM-I2           IF W1C66791-C-NCP-POS-TRAN-CD =                        01977000
MDM-I2                                    WV-C8869-C-SERVICE-BILLING-D0
MDM-I2              MOVE WV-C1031-C-SERVICE-D0
MDM-I2                TO W1C40541-C-HDR-TY-CD
MDM-I2           END-IF
MDM-I2        END-IF
           ELSE
              MOVE WV-C1031-C-PHARMACY-51
                TO RCEDS2TB-C-HDR-TY-CD
           END-IF

           IF W1C66791-C-NCP-POS-TRAN-CD = WV-C8869-C-RX-REVERSAL-51-D0
MDM-I2                                  OR WV-C8869-C-SERVICE-REVERS-D0
              MOVE WV-C1031-C-CRED-REQ
                TO RCEDS2TB-C-HDR-TY-CD
           ELSE
              IF W1C66791-C-NCP-POS-TRAN-CD = 
                                           WV-C8869-C-RX-RE-BILL-51-D0
MDM-I2                                  OR WV-C8869-C-SERVICE-REBILL-D0
                 MOVE WV-C1031-C-REPL-REQ
                   TO RCEDS2TB-C-HDR-TY-CD
              END-IF
           END-IF
               

           IF WW-000-INT-EXC-CD IS NUMERIC
              MOVE FUNCTION NUMVAL(WW-000-INT-EXC-CD) 
                TO WW-600-CLM-EXC-IDX
           ELSE
              MOVE ZEROES
                TO WW-600-CLM-EXC-IDX
           END-IF

           EXEC SQL
               SELECT NVL(D.R_CLM_EXC_DISP_CD, C.R_CLM_EXC_DISP_CD)
                       AS R_CLM_EXC_DISP_CD
                       , NVL(D.R_EXC_FORCE_APP_CD, C.R_EXC_FORCE_APP_CD)
                       AS R_EXC_FORCE_APP_CD
                       , NVL(D.R_FORCE_DENY_CD, C.R_FORCE_DENY_CD)
                       AS R_FORCE_DENY_CD
MDM                    , NVL(D.R_CLM_ADJUD_REJ_CD, C.R_CLM_ADJUD_REJ_CD)
MDM                    AS R_CLM_ADJUD_REJ_CD
MDM                    , NVL(D.R_TXT_EOB_CD, C.R_TXT_EOB_CD)
MDM                    AS R_TXT_EOB_CD
                   INTO :RCEDS2TB-R-CLM-EXC-DISP-CD
                       ,:RCEDS2TB-R-EXC-FORCE-APP-CD
                       ,:RCEDS2TB-R-FORCE-DENY-CD
MDM                    ,:RCEDS2TB-R-CLM-ADJUD-REJ-CD
MDM                    ,:RCEDS2TB-R-TXT-EOB-CD
               FROM
               (SELECT NVL(B.R_CLM_EXC_CD, A.R_CLM_EXC_CD)
                       AS R_CLM_EXC_CD
                       , NVL(B.R_CLM_EXC_DISP_CD, A.R_CLM_EXC_DISP_CD)
                       AS R_CLM_EXC_DISP_CD
                       , NVL(B.R_EXC_FORCE_APP_CD, A.R_EXC_FORCE_APP_CD)
                       AS R_EXC_FORCE_APP_CD
                       , NVL(B.R_FORCE_DENY_CD, A.R_FORCE_DENY_CD)
                       AS R_FORCE_DENY_CD
MDM                    , NVL(B.R_CLM_ADJUD_REJ_CD, A.R_CLM_ADJUD_REJ_CD)
MDM                    AS R_CLM_ADJUD_REJ_CD
MDM                    , NVL(B.R_TXT_EOB_CD, A.R_TXT_EOB_CD)
MDM                    AS R_TXT_EOB_CD
               FROM
               (SELECT R_CUST_PART_NUM
                       ,R_CLM_EXC_CD
                       ,R_CLM_EXC_DISP_CD
                       ,R_EXC_FORCE_APP_CD
                       ,R_FORCE_DENY_CD
MDM                    ,R_CLM_ADJUD_REJ_CD
MDM                    ,R_TXT_EOB_CD
               FROM R_CLM_EXC_DISP_TB
003080          WHERE R_CUST_PART_NUM    = 0
002230            AND R_CLM_EXC_CD       = :RCEDS2TB-R-CLM-EXC-CD
003090            AND R_PLAN_ID          = '999'
003110            AND R_GROUP_ID         = 'ALL     '
003120            AND C_BAT_DOC_TY_CD    = :RCEDS2TB-C-BAT-DOC-TY-CD
003130            AND C_BAT_MED_SRC_CD   = :RCEDS2TB-C-BAT-MED-SRC-CD
003140            AND C_HDR_TY_CD        = :RCEDS2TB-C-HDR-TY-CD
X2OS              AND R_CLM_EXC_BEG_DT  <= CURRENT_DATE
X2OS              AND R_CLM_EXC_END_DT  >= CURRENT_DATE) A
003170          LEFT OUTER JOIN
003180          (SELECT R_CUST_PART_NUM
003190                 ,R_CLM_EXC_CD
003200                 ,R_CLM_EXC_IDX_NUM
003210                 ,R_CLM_EXC_DISP_CD
003220                 ,R_EXC_FORCE_APP_CD
003230                 ,R_FORCE_DENY_CD
MDM                    ,R_CLM_ADJUD_REJ_CD
MDM                    ,R_TXT_EOB_CD
003240          FROM R_CLM_EXC_DISP_TB
003250          WHERE R_CUST_PART_NUM    = :RCEDS2TB-R-CUST-PART-NUM
002230            AND R_CLM_EXC_CD       = :RCEDS2TB-R-CLM-EXC-CD
003260            AND R_PLAN_ID          = '999'
009799            AND R_GROUP_ID         = 'ALL     '
003280            AND C_BAT_DOC_TY_CD    = :RCEDS2TB-C-BAT-DOC-TY-CD
003290            AND C_BAT_MED_SRC_CD   = :RCEDS2TB-C-BAT-MED-SRC-CD
003300            AND C_HDR_TY_CD        = :RCEDS2TB-C-HDR-TY-CD
X2OS              AND R_CLM_EXC_BEG_DT  <= CURRENT_DATE
X2OS              AND R_CLM_EXC_END_DT  >= CURRENT_DATE) B
003330          ON A.R_CLM_EXC_CD       = B.R_CLM_EXC_CD
003340
X2OS          ) C
003360        LEFT OUTER JOIN
003370          (SELECT R_CUST_PART_NUM
003380                 ,R_CLM_EXC_CD
003390                 ,R_CLM_EXC_IDX_NUM
003400                 ,R_CLM_EXC_DISP_CD
003410                 ,R_EXC_FORCE_APP_CD
003420                 ,R_FORCE_DENY_CD
MDM                    ,R_CLM_ADJUD_REJ_CD
MDM                    ,R_TXT_EOB_CD
003430          FROM R_CLM_EXC_DISP_TB
003440          WHERE R_CUST_PART_NUM    = :RCEDS2TB-R-CUST-PART-NUM
002230            AND R_CLM_EXC_CD       = :RCEDS2TB-R-CLM-EXC-CD
009818            AND R_PLAN_ID          = '999'
009820            AND R_GROUP_ID         = :RCEDS2TB-R-GROUP-ID
009821            AND C_BAT_DOC_TY_CD    = :RCEDS2TB-C-BAT-DOC-TY-CD
009822            AND C_BAT_MED_SRC_CD   = :RCEDS2TB-C-BAT-MED-SRC-CD
009823            AND C_HDR_TY_CD        = :RCEDS2TB-C-HDR-TY-CD
X2OS              AND R_CLM_EXC_BEG_DT  <= CURRENT_DATE
X2OS              AND R_CLM_EXC_END_DT  >= CURRENT_DATE) D
009826          ON C.R_CLM_EXC_CD       = D.R_CLM_EXC_CD
           END-EXEC


002410     EVALUATE TRUE
002420
002430       WHEN SQLCODE =  WK-805-SUCCESSFUL-CALL
POSTFE         IF WS-000-EC-IN-PRCS-IS-FATAL
POSTFE           MOVE '3' TO RCEDS2TB-R-CLM-EXC-DISP-CD
POSTFE         ELSE
POSTFE           IF RCEDS2TB-R-CLM-EXC-DISP-CD = SPACES OR 'Z'
POSTFE              GO TO S600-999-EXIT
POSTFE           END-IF
POSTFE         END-IF
002470
002480       WHEN SQLCODE =  WK-805-DATA-NOT-FOUND
POSTFE         IF WS-000-EC-IN-PRCS-IS-FATAL
POSTFE           MOVE '3' TO RCEDS2TB-R-CLM-EXC-DISP-CD
POSTFE         ELSE
POSTFE           GO TO S600-999-EXIT
POSTFE         END-IF

002560       WHEN OTHER
002570*        SQLCODE IS UNACCEPTABLE
002580         MOVE  'S600-000-START             ' TO  WD-999-MSG(2)
002590         MOVE  'R_CLM_EXC-DISP_TB          ' TO  WD-999-MSG(3)
002600         MOVE  'SELECT'                      TO  WD-999-MSG(4)
002610         MOVE RCEDS2TB-R-CUST-PART-NUM
002620               TO WW-600-R-CUST-PART-NUM
002630         STRING  WW-600-R-CUST-PART-NUM
002640                 ' '
002650                 RCEDS2TB-R-CLM-EXC-CD
002660                 ' '
002670                 RCEDS2TB-R-PLAN-ID
002680                 ' '
002690                 RCEDS2TB-R-GROUP-ID
002700                 ' '
002710                 RCEDS2TB-C-BAT-DOC-TY-CD
002720                 ' '
002730                 RCEDS2TB-C-BAT-MED-SRC-CD
002740                 ' '
002750                 RCEDS2TB-C-HDR-TY-CD        DELIMITED BY SIZE
002760         INTO                                    WD-999-MSG(5)
002770         MOVE  WV-G5880-C-APPLIC-ERR         TO  WD-999-MSG(6)
002780         PERFORM S900-000-DUAL-MDUL-DB2-ERR
002790
002800     END-EVALUATE
002810
002840     EVALUATE TRUE
002850
002860       WHEN W1C40541-C-CNT-EXC-NUM LESS THAN WW-600-MAX-EXC
002870         PERFORM S605-000-INSERT-EXC
002880
002890       WHEN W1C40541-C-CNT-EXC-NUM EQUAL WW-600-MAX-EXC
002900*-----------------------------------------------------------*
002910***      ** DO A COUNT TO SEE IF EXC ALREADY POSTED       ***
002920*-----------------------------------------------------------*
002930         PERFORM VARYING WW-600-COUNT FROM +1 BY +1
002940           UNTIL WW-600-COUNT GREATER THAN WW-600-MAX-EXC
002950              OR W1C40541-R-CLM-EXC-CD OF
002960                 W1C40541-C-LI-EXC-VW (WW-600-COUNT)
002970                 EQUAL WW-600-4899-MORE-THAN-MAX-EXC
002980         END-PERFORM
002990*-----------------------------------------------------------*
003000***          ** IF NOT ALREADY POSTED                     ***
003010*-----------------------------------------------------------*
003020         IF  WW-600-COUNT GREATER THAN WW-600-MAX-EXC
003030         THEN
003040             IF WW-600-CLM-EXC-CD EQUAL
003050                WW-600-4445-1ST-CYCLE-MASS-ADJ
003060*-----------------------------------------------------------*
003070***         ** GO AHEAD AND POST 4894 IN SECOND TO LAST EXC**
003080*-----------------------------------------------------------*
003090             THEN
003100                 SUBTRACT 2 FROM WW-600-MAX-EXC
003110                   GIVING W1C40541-C-CNT-EXC-NUM
003120                 PERFORM S605-000-INSERT-EXC
003130             END-IF
003140             MOVE WW-600-MAX-EXC
003150               TO W1C40541-C-CNT-EXC-NUM
003160             IF W1C40541-R-CLM-EXC-CD (WW-600-MAX-EXC) EQUAL
003170                WW-600-4445-1ST-CYCLE-MASS-ADJ
003180             THEN
003190                 MOVE W1C40541-C-LI-EXC-VW
003200                       (WW-600-MAX-EXC)
003210                   TO W1C40541-C-LI-EXC-VW
003220                       (WW-600-MAX-EXC - 1)
003230                 MOVE SPACES
003240                   TO W1C40541-C-LI-EXC-VW (WW-600-MAX-EXC)
003250             END-IF
003260*-----------------------------------------------------------*
003270***                ** POST EXC 4899 IN LAST SLOT **       ***
003280*-----------------------------------------------------------*
003290             MOVE WW-600-4899-MORE-THAN-MAX-EXC
003300               TO W1C40541-R-CLM-EXC-CD OF
003310                  W1C40541-C-LI-EXC-VW
003320                 (W1C40541-C-CNT-EXC-NUM)
003330             MOVE SPACES
003340               TO W1C40541-C-LI-EXC-CLRK-ID OF
003350                  W1C40541-C-LI-EXC-VW
003360                 (W1C40541-C-CNT-EXC-NUM)
003370*            MOVE WW-600-MAX-LINE-NUM                             SR444
003380*            MOVE WW-600-HDR-LINE-NUM                             SR444
                   MOVE 0
003390               TO W1C40541-C-LI-NUM  OF
003400                  W1C40541-C-LI-EXC-VW
003410                   (W1C40541-C-CNT-EXC-NUM)
003420*-----------------------------------------------------------*
003430***  ** ASSUME 4899 SHOULD NEVER BE "IGNORED", AND WILL     *
003440***  ** ALWAYS FORCE A CLAIM TO DENY/REJECT.                *
003450*-----------------------------------------------------------*
003460
003470              MOVE WV-R0156-C-SUSPEND
003480                TO W1C40541-C-EXC-STAT-CD      OF
003490                   W1C40541-C-LI-EXC-VW
003500                   (W1C40541-C-CNT-EXC-NUM)
003510              MOVE WV-R1903-C-CANT-FORCE
003520                TO W1C40541-R-EXC-FORCE-APP-CD OF
003530                   W1C40541-C-LI-EXC-VW
003540                   (W1C40541-C-CNT-EXC-NUM)
003550              MOVE WV-R1914-C-CAN-DENY
003560                TO W1C40541-R-FORCE-DENY-CD    OF
003570                   W1C40541-C-LI-EXC-VW
003580                   (W1C40541-C-CNT-EXC-NUM)
003590         ELSE
003600             IF WW-600-CLM-EXC-CD EQUAL
003610                WW-600-4445-1ST-CYCLE-MASS-ADJ
003620*-----------------------------------------------------------*
003630***            ** GO AHEAD AND POST 4894 IN LAST EXC **   ***
003640*-----------------------------------------------------------*
003650             THEN
003660                 SUBTRACT +1 FROM WW-600-MAX-EXC
003670                   GIVING W1C40541-C-CNT-EXC-NUM
003680                 PERFORM S605-000-INSERT-EXC
003690             END-IF
003700         END-IF
003710         ADD +2 TO WW-600-MAX-EXC
003720           GIVING W1C40541-C-CNT-EXC-NUM
003730         MOVE WW-600-MAX-EXC
003740           TO W1C40541-C-CNT-EXC-NUM
003750
003760       WHEN OTHER
003770         IF WW-600-CLM-EXC-CD EQUAL
003780            WW-600-4445-1ST-CYCLE-MASS-ADJ
003790         THEN
003800             IF W1C40541-R-CLM-EXC-CD (WW-600-MAX-EXC) EQUAL
003810                WW-600-4899-MORE-THAN-MAX-EXC
003820*-----------------------------------------------------------*
003830***         ** GO AHEAD AND POST 854 IN SECOND TO LAST EXC***
003840*-----------------------------------------------------------*
003850             THEN
003860                 SUBTRACT +2 FROM WW-600-MAX-EXC
003870                   GIVING W1C40541-C-CNT-EXC-NUM
003880                 PERFORM S605-000-INSERT-EXC
003890             ELSE
003900*-----------------------------------------------------------*
003910***                ** POST 854 IN LAST EXC SLOT **        ***
003920*-----------------------------------------------------------*
003930                 SUBTRACT +1 FROM WW-600-MAX-EXC
003940                   GIVING W1C40541-C-CNT-EXC-NUM
003950                 PERFORM S605-000-INSERT-EXC
003960             END-IF
003970             ADD +2 TO WW-600-MAX-EXC
003980               GIVING W1C40541-C-CNT-EXC-NUM
003990             MOVE WW-600-MAX-EXC
004000               TO W1C40541-C-CNT-EXC-NUM
004010         END-IF
004020     END-EVALUATE.
004030
004040 S600-999-EXIT.
004050     EXIT.
005200/
005210 S605-000-INSERT-EXC SECTION.
005220****************************************************************
005230*
005240*                          S605 - INSERT-EXC
005250*
005260*?  POST THE CURRENT EXCEPTION
005270*+
005280*
005290****************************************************************
005300 S605-010-START.
005310*--------------------------------------------------------------
005320*    EXCEPTION CODE SHOULD NEVER BE BLANK.  THIS INDICATES
005330*    THAT THE EXCEPTION BEING POSTED IS SET TO -IGNORE- BUT
005340*    SOME PROGRAM IS NOT CHECKING DISPOSITION STATUS BEFORE
005350*    ATTEMPTING TO POST EXCEPTION (HERE).  INSTEAD OF
005360*    POSTING BLANKS AS THE EXCEPTION, S630 ROUTINE WILL
005370*    FIND THE EXCEPTION CODE IN THE EXCEPTION TABLE USING
005380*    THE EXCEPTION INDEX.  THEN IT WILL POST THE EXCEPTION
005390*    CODE FOUND ALONG WITH A STATUS OF SUPER-SUSPEND.
005400*-------------------------------------------------------------
005410     IF WW-600-CLM-EXC-CD EQUAL SPACES
005420     THEN
005430         PERFORM S630-000-FIND-BLANK-EXC
005440     END-IF.
005450*-----------------------------------------------------------*
005460***            ** DO A COUNT TO SEE IF EXC ALREADY POSTED ***
005470*-----------------------------------------------------------*
005480     PERFORM VARYING WW-600-COUNT FROM +1 BY +1
005490       UNTIL WW-600-COUNT > W1C40541-C-CNT-EXC-NUM
005500          OR (W1C40541-R-CLM-EXC-CD  OF
005510              W1C40541-C-LI-EXC-VW (WW-600-COUNT)
005520             EQUAL WW-600-CLM-EXC-CD
005530          AND W1C40541-C-LI-NUM  OF
005540              W1C40541-C-LI-EXC-VW (WW-600-COUNT)
005550             EQUAL WW-600-CLM-EXC-LI)
005560     END-PERFORM
005570*-----------------------------------------------------------*
005580***            ** IF NOT ALREADY POSTED                   ***
005590*-----------------------------------------------------------*
005600     IF WW-600-COUNT GREATER THAN
005610        W1C40541-C-CNT-EXC-NUM
005620     THEN
005630         ADD +1 TO W1C40541-C-CNT-EXC-NUM
005640         MOVE WW-600-CLM-EXC-CD
005650           TO W1C40541-R-CLM-EXC-CD  OF
005660              W1C40541-C-LI-EXC-VW
005670             (W1C40541-C-CNT-EXC-NUM)
005680         MOVE RCEDS2TB-R-CLM-EXC-DISP-CD
005690           TO W1C40541-C-EXC-STAT-CD OF
005700              W1C40541-C-LI-EXC-VW
005710              (W1C40541-C-CNT-EXC-NUM)
005720         MOVE SPACES
005730           TO W1C40541-C-LI-EXC-CLRK-ID
005740              (W1C40541-C-CNT-EXC-NUM)
005741         IF WW-600-CLM-EXC-LI > W1C40541-C-CNT-LI-NUM             SR5421
005750            MOVE ZEROES                                           SR5421
005760              TO W1C40541-C-LI-NUM   OF                           SR5421
005770                 W1C40541-C-LI-EXC-VW                             SR5421
005780                 (W1C40541-C-CNT-EXC-NUM)                         SR5421
005781         ELSE                                                     SR5421
005782            MOVE WW-600-CLM-EXC-LI
005783              TO W1C40541-C-LI-NUM   OF
005784                 W1C40541-C-LI-EXC-VW
005785                 (W1C40541-C-CNT-EXC-NUM)
005786         END-IF                                                   SR5421
005790         MOVE RCEDS2TB-R-EXC-FORCE-APP-CD
005800           TO W1C40541-R-EXC-FORCE-APP-CD OF
005810              W1C40541-C-LI-EXC-VW
005820              (W1C40541-C-CNT-EXC-NUM)
005830         MOVE RCEDS2TB-R-FORCE-DENY-CD
005840           TO W1C40541-R-FORCE-DENY-CD OF
005850              W1C40541-C-LI-EXC-VW
005860              (W1C40541-C-CNT-EXC-NUM)
               MOVE RCEDS2TB-R-TXT-EOB-CD
005900           TO W1C40541-R-EXC-EOB-ADJUD-CD OF
005910              W1C40541-C-LI-EXC-VW
005920              (W1C40541-C-CNT-EXC-NUM)
               MOVE RCEDS2TB-R-CLM-ADJUD-REJ-CD
005940           TO W1C40541-R-CLM-ADJUD-REJ-CD OF
005950              W1C40541-C-LI-EXC-VW
005960              (W1C40541-C-CNT-EXC-NUM)
006140     END-IF.
006150
006160 S605-999-EXIT.
006170     EXIT.
006180/
007720 S630-000-FIND-BLANK-EXC SECTION.
007730******************************************************************
007740*                                                                *
007750*        SECTION 630     FIND BLANK EXC                          *
007760*                                                                *
007770*?   THIS SECTION SELECTS THE CLAIM EXCEPTION CODE FOR           *
007780*+   EXCEPTIONS THAT HAVE DISPOSITION SETTINGS OF IGNORE.        *
007790*+                                                               *
007800*                                                                *
007810******************************************************************
007820 S630-010-START.
007830
007840     IF W1C40541-R-CUST-PART-NUM > ZERO
007850       MOVE W1C40541-R-CUST-PART-NUM
PL1                            TO RCEDS2TB-R-CUST-PART-NUM
007870     ELSE
007880       MOVE WW-600-DEFAULT-CUST-PART-NUM
PL1                            TO RCEDS2TB-R-CUST-PART-NUM
007900     END-IF
007910
007920     EXEC SQL
007930         SELECT R_CLM_EXC_CD
007940           INTO  :WW-600-CLM-EXC-CD
PL1              FROM  R_CLM_EXC_DISP_TB
PL1             WHERE  R_CUST_PART_NUM = :RCEDS2TB-R-CUST-PART-NUM
007970            AND  R_CLM_EXC_IDX_NUM  =  :WW-600-CLM-EXC-IDX
008000     END-EXEC.
008010
008020     EVALUATE SQLCODE OF SQLCA
008030
008040         WHEN WK-805-SUCCESSFUL-CALL
008050             CONTINUE
008060
008070         WHEN OTHER
008080             MOVE W1C56941-C-MDUL-NAM TO WD-999-MSG (1)
008090             MOVE 'S630-000-FIND-BLANK-EXC'
008100                                      TO WD-999-MSG (2)
PL1                MOVE 'R_CLM_EXC_DISP_TB' TO WD-999-MSG (3)
008120             MOVE 'SELECT'            TO WD-999-MSG (4)
008130             MOVE W1C40541-C-TCN-NUM  TO WW-600-TCN
008140             MOVE WW-600-CLM-EXC-IDX  TO WW-600-DISP-CLM-EXC-IDX
PL1                MOVE RCEDS2TB-R-CUST-PART-NUM
008160               TO WW-600-R-CUST-PART-NUM
008170             STRING WW-600-TCN
008180                ' ' WW-600-R-CUST-PART-NUM
008190                ' ' WW-600-DISP-CLM-EXC-IDX
008200                 DELIMITED BY SIZE
008210             INTO WD-999-MSG (5)
008220             MOVE '2'                 TO WD-999-MSG (6)
008230             MOVE WV-C4398-C-FAILURE
008240               TO W1C56941-C-MDUL-RTRN-CD
008250               GO TO S900-000-DUAL-MDUL-DB2-ERR
008260     END-EVALUATE.
008270
008280 S630-999-EXIT.
008290     EXIT.
008300/
029870 S500C1-000-TEST-CLM-FORMAT   SECTION.                            MDM-4/9/13
029880      COPY S500C1.                                                MDM-4/9/13

029870 S550C1-000-PROD-CLM-FORMAT   SECTION.                            DF3407
029880      COPY S550C1.                                                DF3407

029870 S600C1-000-POST-EXCEPTION   SECTION.                             03958000
029880      COPY S600C1.                                                03959000

029870 S800-000-TERMINATION SECTION.                                    03958000
029880*                                                                 03959000
029890***************************************************************** 03960000
029900*                                                               * 03961000
029910*?  S800-000-TERMINATION SECTION                                * 03962000
029920*+                                                              * 03963000
029930*+  THIS SECTION PERFORMS ALL TERMINATION FUNCTIONS INCLUDING   * 03964000
029940*+  PASSING CONTROL TO THE POS SENDER PROGRAM WHICH COMPRESSES, * 03965000
029950*+  LOGS, AND SENDS THE NCPDP RESPONSE.                         * 03966000
029960*+                                                              * 03967000
029970***************************************************************** 03968000
029980*                                                                 03969000
029990 S800-010-START.                                                  03970000
030000*                                                                 03971000
           PERFORM VARYING SUB FROM 1 BY 1
              UNTIL SUB > 15
                MOVE ZEROES             TO W1C40541-A-ID(SUB)
           END-PERFORM.

           MOVE WV-C4398-C-SUCCESS TO W1C56941-C-MDUL-RTRN-CD.
030000*                                                                 03980000
030010 S800-999-EXIT.                                                   03981000
030020     EXIT.                                                        03982000
030030/                                                                 03983000
030040 S810-000-RETURN-TO-CALLER SECTION.                               03984000
030050*                                                                 03985000
030060***************************************************************** 03986000
030070*                                                               * 03987000
030080*?  S810-000-RETURN-TO-CALLER SECTION                           * 03988000
030090*+                                                              * 03989000
030100*+  THIS SECTION RETURNS TO THE CALLING PROGRAM                   03990000
030110*+                                                              * 03991000
030120***************************************************************** 03992000
030130*                                                                 03993000
030140 S810-010-START.                                                  03994000
030150*                                                                 03995000
030160     GOBACK.                                                      03996000
030170*                                                                 03997000
030180 S810-999-EXIT.                                                   03998000
030190     EXIT.                                                        03999000

073630 S820-000-GET-BENEFIT-STAT   SECTION.                             DF2453
      ***************************************************************** DF2453
      *                                                               * DF2453
      ***************************************************************** DF2453
       S820-010-START.                                                  DF2453
073620                                                                  DF2453
073640*    IF WL-807-TY-OF-TM-PER = '1'                                 DF3736
073640     IF WL-807-TY-OF-TM-PER = WV-G0117-C-CAL-YEAR                 DF3736
073660        MOVE W1C40541-C-HDR-SVC-FST-DT (1:4)                      DF2453
073670          TO WH-000-B-BENEFIT-YR-ID                               DF2453
           END-IF.                                                      DF2453
                                                                        DF2453
     *     IF WL-807-TY-OF-TM-PER = '2'                                 DF3736
           IF WL-807-TY-OF-TM-PER = WV-G0117-C-FISCAL-YR                DF3736
              MOVE W1C40541-C-HDR-SVC-FST-DT (6:2)                      DF2453
                TO WH-000-B-BENEFIT-MO-ID                               DF2453
              IF WH-000-B-BENEFIT-MO-ID < WL-807-R-FY-BEG-MO-NUM        DF2453
                 MOVE W1C40541-C-HDR-SVC-FST-DT (1:4)                   DF2453
                   TO WH-000-B-BENEFIT-YR-ID                            DF2453
              ELSE                                                      DF2453
                 MOVE W1C40541-C-HDR-SVC-FST-DT (1:4)                   DF2453
                   TO WH-000-B-BENEFIT-YR-ID                            DF2453
                 ADD +1 TO WH-000-B-BENEFIT-YR-ID                       DF2453
              END-IF                                                    DF2453
           END-IF.                                                      DF2453
                                                                        DF2453
           MOVE W1C40541-B-SYS-ID                                       DF2453
073650       TO BBENEFTB-B-SYS-ID.                                      DF2453
073680     MOVE WH-000-B-BENEFIT-YR-ID                                  DF2453
073690       TO BBENEFTB-B-YR-ID.                                       DF2453
FIXIT      MOVE W1C40541-R-GROUP-ID                                     DF2453
FIXIT        TO BBENEFTB-R-GROUP-ID.                                    DF2453
DF3736     MOVE WL-807-B-BENEFIT-TY-CD                                  DF3736
DF3736       TO BBENEFTB-B-BENEFIT-TY-CD.                               DF3736
073700                                                                  DF2453
073710     EXEC SQL                                                     DF2453
073720         SELECT B_SYS_ID                                          DF2453
073730               ,B_YR_ID                                           DF2453
073740               ,B_BENE_CAP_STAT_CD                                DF2453
073750               ,COALESCE(B_BENEFIT_YTD_AMT,0)                     DF2453
073760               ,B_CAP_MET_DT                                      DF2453
073770               ,B_CAP_MET_FILL_DT                                 DF2453
073780               ,B_CAP_NOTFY_DT                                    DF2453
073790               ,B_CAP_NOTFY_SRC_CD                                DF2453
073800               ,COALESCE(B_PDCS_YTD_AMT,0)                        DF2453
073810               ,B_LST_FILL_DT                                     DF2453
073820               ,B_LST_ADJUD_DT                                    DF2453
073830               ,B_BENEFIT_UPD_IND                                 DF2453
073840               ,G_AUD_USER_ID                                     DF2453
073850               ,G_AUD_TS                                          DF2453
755910               ,COALESCE(C_TCN_NUM,0)                             DF2453
073860          INTO  :BBENEFTB-B-SYS-ID                                DF2453
073870               ,:BBENEFTB-B-YR-ID                                 DF2453
073880               ,:BBENEFTB-B-BENE-CAP-STAT-CD                      DF2453
073890               ,:BBENEFTB-B-BENEFIT-YTD-AMT                       DF2453
073900               ,:BBENEFTB-B-CAP-MET-DT                            DF2453
073910               ,:BBENEFTB-B-CAP-MET-FILL-DT                       DF2453
073920               ,:BBENEFTB-B-CAP-NOTFY-DT                          DF2453
073930               ,:BBENEFTB-B-CAP-NOTFY-SRC-CD                      DF2453
073940               ,:BBENEFTB-B-PDCS-YTD-AMT                          DF2453
073950               ,:BBENEFTB-B-LST-FILL-DT                           DF2453
073960               ,:BBENEFTB-B-LST-ADJUD-DT                          DF2453
073970               ,:BBENEFTB-B-BENEFIT-UPD-IND                       DF2453
073980               ,:BBENEFTB-G-AUD-USER-ID                           DF2453
073990               ,:BBENEFTB-G-AUD-TS                                DF2453
757310               ,:BBENEFTB-C-TCN-NUM                               DF2453
074000           FROM B_BENEFIT_TB                                      DF2453
074010          WHERE B_SYS_ID        = :BBENEFTB-B-SYS-ID              DF2453
074020            AND B_YR_ID         = :BBENEFTB-B-YR-ID               DF2453
DF1751            AND RTRIM(R_GROUP_ID) =                               DF2453
DF1751                                  RTRIM(:BBENEFTB-R-GROUP-ID)     DF2453
DF3736            AND B_BENEFIT_TY_CD   = :BBENEFTB-B-BENEFIT-TY-CD     DF3736     
074030     END-EXEC.                                                    DF2453
074040                                                                  DF2453
074050     EVALUATE SQLCODE                                             DF2453
074060         WHEN                           WK-805-SUCCESSFUL-CALL    DF2453
074070             SET WS-000-BENEFIT-STAT-FOUND TO TRUE                DF2453
074080         WHEN                            WK-805-DATA-NOT-FOUND    DF2453
               WHEN                            -1405                    DF2453
074090             SET WS-000-BENEFIT-STAT-NOT-FOUND TO TRUE            DF2453
074100         WHEN OTHER                                               DF2453
074110             MOVE WK-000-PROG-NAM             TO WD-999-MSG (1)   DF2453
074120             MOVE 'S820-000-GET-BENEFIT-STAT' TO WD-999-MSG (2)   DF2453
074130             MOVE 'BBENEFTB'                  TO WD-999-MSG (3)   DF2453
074140             MOVE 'SELECT'                    TO WD-999-MSG (4)   DF2453
074150             MOVE BBENEFTB-B-SYS-ID           TO WD-999-MSG (5)   DF2453
074160             GO TO S900-000-DUAL-MDUL-DB2-ERR                     DF2453
074170     END-EVALUATE.                                                DF2453
074180                                                                  DF2453
074190 S820-999-EXIT.                                                   DF2453
074200     EXIT.                                                        DF2453
074210                                                                  DF2453

                                                                        PMLMISS
      ** S900C ADDED FOR USE BY S600C1                                  PMLMISS
            COPY S900C.                                                 PMLMISS

       S925-000-SCC-VALID SECTION.                                      CR12914
      *----------------------------------------------------------------*CR12914
      *     S925-000-SCC-VALID                                         *CR12914
      *----------------------------------------------------------------*CR12914
       S925-010-START.                                                  CR12914
           INITIALIZE GSLDTLTB-G-LIST-STRT-LMT                          CR12914
                      GSLDTLTB-G-LIST-END-LMT                           CR12914
                      GSLDTLTB-G-LIST-STRT-2-LMT                        CR12914
                      GSLDTLTB-G-LIST-END-2-LMT.                        CR12914
      *                                                                 CR12914
      ***  VALIDATING FIRST OCCURANCE OF SUBMISSION CLARIFICATION CODE  CR12914
      *                                                                 CR12914
              IF W1C40541-C-DRUG-RX-OVRRD-CD > SPACES                   CR12914
                 MOVE W1C40541-C-DRUG-RX-OVRRD-CD TO                    CR12914
                                 GSLDTLTB-G-LIST-STRT-LMT               CR12914
                                 GSLDTLTB-G-LIST-END-LMT                CR12914
                 PERFORM S931-000-7700-CONFIG-CHECK                     CR12914
                 MOVE 'Y' TO GSLDTLTB-G-LIST-STRT-2-LMT                 CR12914
                             GSLDTLTB-G-LIST-END-2-LMT                  CR12914
                 MOVE SPACES TO   WK-EXIST-CHECK                        CR12914
                 PERFORM S926-000-SCC-CONFIG-CHECK                      CR12914
      *                                                                 CR12914
                 IF WK-EXIST-CHECK = '1'                                CR12914
                   IF (W1C52991-R-CLM-EXC-DISP-CD                       CR12914
                      OF W1C52991-C-CNTL-EXC-HDR-VW                     CR12914
                      OF W1C52991-C-CNTL-EXC-VW                         CR12914
                         (WK-806-3163-IDX) EQUAL SPACES)                CR12914
                      CONTINUE                                          CR12914
                   ELSE                                                 CR12914
                      MOVE WK-806-3163-IDX     TO WW-600-CLM-EXC-IDX    CR12914
                      MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI     CR12914
                      PERFORM S600-000-INSERT-EXCEPTION                 CR12914
                      PERFORM S927-000-HISTORY-CHECK                    CR12914
                   END-IF                                               CR12914
                   GO TO S925-999-EXIT                                  CR12914
                 END-IF                                                 CR12914
              END-IF.                                                   CR12914
      *                                                                 CR12914
      ***  VALIDATING SECOND OCCURANCE OF SUBMISSION CLARIFICATION CODE CR12914
      *                                                                 CR12914
              IF W1C40541-C-DRUG-RX-OVR2-CD > SPACES                    CR12914
                 MOVE W1C40541-C-DRUG-RX-OVR2-CD TO                     CR12914
                                 GSLDTLTB-G-LIST-STRT-LMT               CR12914
                                 GSLDTLTB-G-LIST-END-LMT                CR12914
                 PERFORM S931-000-7700-CONFIG-CHECK                     CR12914
                 MOVE 'Y' TO GSLDTLTB-G-LIST-STRT-2-LMT                 CR12914
                             GSLDTLTB-G-LIST-END-2-LMT                  CR12914
                 MOVE SPACES TO   WK-EXIST-CHECK                        CR12914
                 PERFORM S926-000-SCC-CONFIG-CHECK                      CR12914
      *                                                                 CR12914
                 IF WK-EXIST-CHECK = '1'                                CR12914
                   IF (W1C52991-R-CLM-EXC-DISP-CD                       CR12914
                      OF W1C52991-C-CNTL-EXC-HDR-VW                     CR12914
                      OF W1C52991-C-CNTL-EXC-VW                         CR12914
                         (WK-806-3163-IDX) EQUAL SPACES)                CR12914
                      CONTINUE                                          CR12914
                   ELSE                                                 CR12914
                      MOVE WK-806-3163-IDX     TO WW-600-CLM-EXC-IDX    CR12914
                      MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI     CR12914
                      PERFORM S600-000-INSERT-EXCEPTION                 CR12914
                      PERFORM S927-000-HISTORY-CHECK                    CR12914
                   END-IF                                               CR12914
                   GO TO S925-999-EXIT                                  CR12914
                 END-IF                                                 CR12914
              END-IF.                                                   CR12914
      *                                                                 CR12914
      ***  VALIDATING THIRD OCCURANCE OF SUBMISSION CLARIFICATION CODE  CR12914
      *                                                                 CR12914
              IF W1C40541-C-DRUG-RX-OVR3-CD > SPACES                    CR12914
                 MOVE W1C40541-C-DRUG-RX-OVR3-CD TO                     CR12914
                                 GSLDTLTB-G-LIST-STRT-LMT               CR12914
                                 GSLDTLTB-G-LIST-END-LMT                CR12914
                 PERFORM S931-000-7700-CONFIG-CHECK                     CR12914
                 MOVE 'Y' TO GSLDTLTB-G-LIST-STRT-2-LMT                 CR12914
                             GSLDTLTB-G-LIST-END-2-LMT                  CR12914
                 MOVE SPACES TO   WK-EXIST-CHECK                        CR12914
                 PERFORM S926-000-SCC-CONFIG-CHECK                      CR12914
      *                                                                 CR12914
                 IF WK-EXIST-CHECK = '1'                                CR12914
                   IF (W1C52991-R-CLM-EXC-DISP-CD                       CR12914
                      OF W1C52991-C-CNTL-EXC-HDR-VW                     CR12914
                      OF W1C52991-C-CNTL-EXC-VW                         CR12914
                         (WK-806-3163-IDX) EQUAL SPACES)                CR12914
                      CONTINUE                                          CR12914
                   ELSE                                                 CR12914
                      MOVE WK-806-3163-IDX     TO WW-600-CLM-EXC-IDX    CR12914
                      MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI     CR12914
                      PERFORM S600-000-INSERT-EXCEPTION                 CR12914
                      PERFORM S927-000-HISTORY-CHECK                    CR12914
                   END-IF                                               CR12914
                   GO TO S925-999-EXIT                                  CR12914
                 END-IF                                                 CR12914
              END-IF.                                                   CR12914
      *                                                                 CR12914
       S925-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
       S926-000-SCC-CONFIG-CHECK SECTION.                               CR12914
      *----------------------------------------------------------------*CR12914
      *     S926-000-SCC-CONFIG-CHECK                                  *CR12914
      *----------------------------------------------------------------*CR12914
       S926-010-START.                                                  CR12914
      *                                                                 CR12914
             MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM CR12914
             MOVE 'C' TO GSLDTLTB-G-LIST-SUBSYS-CD                      CR12914
             MOVE W1C40541-C-HDR-SVC-FST-DT                             CR12914
                                     TO GSLDTLTB-G-LIST-EFF-STRT-DT     CR12914
                                       ,GSLDTLTB-G-LIST-EFF-END-DT      CR12914
             MOVE '0420' TO GSLDTLTB-G-LIST-NUM                         CR12914
             MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID      CR13997
             MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD  CR13997
             MOVE W1C40541-C-DRUG-VERSN-NUM TO                          CR13997
                                        GSLDTLTB-R-NCPDP-VRSN-NUM       CR13997
             EXEC SQL                                                   CR12914
                SELECT '1'                                              CR12914
                INTO :WK-EXIST-CHECK                                    CR12914
                  FROM G_LIST_DTL_TB                                    CR12914
                 WHERE R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM   CR12914
                  AND G_LIST_SUBSYS_CD   = :GSLDTLTB-G-LIST-SUBSYS-CD   CR12914
                  AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)       CR13997
                   OR (R_GROUP_ID        =  'ALL     '))                CR13997
                  AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)   CR13997
                   OR (C_NCPDP_TXN_CD    =  'AL'))                      CR13997
                  AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM) CR13997
                   OR (R_NCPDP_VRSN_NUM  =  'AL'))                      CR13997
                  AND G_LIST_EFF_STRT_DT <=                             CR12914
                            :GSLDTLTB-G-LIST-EFF-STRT-DT                CR12914
                  AND G_LIST_STRT_LMT    = :GSLDTLTB-G-LIST-STRT-LMT    CR12914
                  AND G_LIST_END_LMT     = :GSLDTLTB-G-LIST-END-LMT     CR12914
                  AND G_LIST_EFF_END_DT >= :GSLDTLTB-G-LIST-EFF-END-DT  CR12914
                  AND G_LIST_NUM         = :GSLDTLTB-G-LIST-NUM         CR12914
                  AND G_LIST_STRT_2_LMT  = :GSLDTLTB-G-LIST-STRT-2-LMT  CR12914
                  AND G_LIST_END_2_LMT   = :GSLDTLTB-G-LIST-END-2-LMT   CR12914
                  AND ROWNUM = 1                                        CR12914
             END-EXEC                                                   CR12914
             MOVE SQLCODE                TO WW-001-SQLCODE              CR12914
                                         WD-999-SQLCODE                 CR12914
             EVALUATE TRUE                                              CR12914
                                                                        CR12914
                WHEN  WW-001-SUCCESSFUL-CALL                            CR12914
                    CONTINUE                                            CR12914
                WHEN WW-001-DATA-NOT-FOUND                              CR12914
                    MOVE SPACES TO WK-EXIST-CHECK                       CR12914
                WHEN OTHER                                              CR12914
                  MOVE WK-001-PROGNAME                                  CR12914
                    TO WD-999-MSG (1)                                   CR12914
                  MOVE 'S926-000-SCC-CONFIG-CHECK'                      CR12914
                    TO WD-999-MSG (2)                                   CR12914
                  MOVE 'G_LIST_DTL_TB'                                  CR12914
                    TO WD-999-MSG (3)                                   CR12914
                  MOVE 'SELECT'                                         CR12914
                    TO WD-999-MSG (4)                                   CR12914
                  MOVE SPACES                                           CR12914
                    TO WD-999-MSG (5)                                   CR12914
                  GO TO S900-000-DUAL-MDUL-DB2-ERR                      CR12914
              END-EVALUATE.                                             CR12914
      *                                                                 CR12914
       S926-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
      *                                                                 CR12914
       S927-000-HISTORY-CHECK SECTION.                                  CR12914
      *----------------------------------------------------------------*CR12914
      *     S930-000-HISTORY-CHECK                                     *CR12914
      *   THIS SECTION WILL GET CHECK HISTORY OF PAID CLAIMS FOR       *CR12914
      *   SCHEDULE-II DRUGS FOR THAT MEMBER WITH SAME RX-NUM           *CR12914
      *   TO CHECK IF THIS IS SUBSEQUENT FILL TO REDUCED QUANTITY      *CR12914
      *     OF PREVIOUS PAID CLAIM                                     *CR12914
      *----------------------------------------------------------------*CR12914
       S927-010-START.                                                  CR12914
                                                                        CR12914
                                                                        CR12914
           MOVE W1C40541-R-CUST-PART-NUM                                CR12914
             TO CLEXCXTB-R-CUST-PART-NUM                                CR12914
                                                                        CR12914
           MOVE W1C40541-B-SYS-ID                                       CR12914
             TO CLEXCXTB-B-SYS-ID                                       CR12914
                                                                        CR12914
           MOVE W1C40541-R-DRUG-GCN-SEQ-NUM (WS-000-LI-SUB)             CR12914
             TO CLDRUGTB-R-DRUG-GCN-SEQ-NUM                             CR12914
                                                                        CR12914
           MOVE W1C40541-C-BLNG-PROV-ID    TO                           CR12914
                                      CLDRUGTB-C-BLNG-PROV-ID.          CR12914
                                                                        CR12914
           MOVE W1C40541-C-RX-SVC-REF-NUM                               CR12914
                                    TO CLDRUGTB-C-RX-SVC-REF-NUM.       CR12914
           MOVE ZEROES              TO WH-100-ACCUM-QTY-DISP-NUM.       CR12914
      *                                                                 CR12914
           SET WS-000-END-O-SCHDL-II-DATA-NOT TO TRUE.                  CR12914
           INITIALIZE WH-100-ACCUM-HIST-COUNT.                          CR12914
           INITIALIZE WH-000-SCC-MATCH-COUNT                            CR12914
                      WH-100-HISTORY-QTY-PRS-AMT.                       CR12914
      *                                                                 CR12914
           PERFORM S928-000-OPEN-SCHDL-II-CURS.                         CR12914
      *                                                                 CR12914
           PERFORM S929-000-FETCH-SCHDL-II-CURS UNTIL                   CR12914
                     WS-000-END-O-SCHDL-II-DATA.                        CR12914
      *                                                                 CR12914
           PERFORM S930-000-CLOSE-SCHDL-II-CURS.                        CR12914
      *                                                                 CR12914
           IF  WH-100-ACCUM-HIST-COUNT > 0                              CR12914
                CONTINUE                                                CR12914
           ELSE                                                         CR12914
                IF (W1C52991-R-CLM-EXC-DISP-CD                          CR12914
                  OF W1C52991-C-CNTL-EXC-HDR-VW                         CR12914
                  OF W1C52991-C-CNTL-EXC-VW                             CR12914
                     (WK-806-3164-IDX) EQUAL SPACES)                    CR12914
                  CONTINUE                                              CR12914
                ELSE                                                    CR12914
                  MOVE WK-806-3164-IDX     TO WW-600-CLM-EXC-IDX        CR12914
                  MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI         CR12914
                  PERFORM S600-000-INSERT-EXCEPTION                     CR12914
                END-IF                                                  CR12914
																		                                    CR12914
                IF W1C40541-C-DRUG-SUB-QTY-AMT >                        CR12914
                                         W1C40541-C-QTY-PRSC-AMT        CR12914
                   IF (W1C52991-R-CLM-EXC-DISP-CD                       CR12914
                     OF W1C52991-C-CNTL-EXC-HDR-VW                      CR12914
                     OF W1C52991-C-CNTL-EXC-VW                          CR12914
                        (WK-806-3166-IDX) EQUAL SPACES)                 CR12914
                     CONTINUE                                           CR12914
                   ELSE                                                 CR12914
                     MOVE WK-806-3166-IDX     TO WW-600-CLM-EXC-IDX     CR12914
                     MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI      CR12914
                     PERFORM S600-000-INSERT-EXCEPTION                  CR12914
                   END-IF                                               CR12914
                END-IF                                                  CR12914

                GO TO S927-999-EXIT                                     CR12914
           END-IF.                                                      CR12914
      *                                                                 CR12914
      *  WE ARE ADDING +1 TO SCC-MATCH-COUNT TO CONSIDER THE INPROCESS  CR12914
      *  CLAIM + COUNT OF ALL HISTORY CLAIMS ACCUMULATED                CR12914
      *                                                                 CR12914
      *    COMPUTE WH-000-SCC-MATCH-COUNT = WH-000-SCC-MATCH-COUNT + 1  CR12914
           IF WH-000-SCC-MATCH-COUNT > WH-000-ALLOW-SHDL-II-COUNT       CR12914
                IF (W1C52991-R-CLM-EXC-DISP-CD                          CR12914
                  OF W1C52991-C-CNTL-EXC-HDR-VW                         CR12914
                  OF W1C52991-C-CNTL-EXC-VW                             CR12914
                     (WK-806-3616-IDX) EQUAL SPACES)                    CR12914
                  CONTINUE                                              CR12914
                ELSE                                                    CR12914
                  MOVE WK-806-3616-IDX     TO WW-600-CLM-EXC-IDX        CR12914
                  MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI         CR12914
                  PERFORM S600-000-INSERT-EXCEPTION                     CR12914
                END-IF                                                  CR12914
           ELSE                                                         CR12914
               COMPUTE WH-100-ACCUM-QTY-DISP-NUM  =                     CR12914
                               WH-100-ACCUM-QTY-DISP-NUM  +             CR12914
                               W1C40541-C-DRUG-SUB-QTY-AMT              CR12914
               IF WH-100-ACCUM-QTY-DISP-NUM >                           CR12914
                                             WH-100-HISTORY-QTY-PRS-AMT CR12914
                   IF (W1C52991-R-CLM-EXC-DISP-CD                       CR12914
                     OF W1C52991-C-CNTL-EXC-HDR-VW                      CR12914
                     OF W1C52991-C-CNTL-EXC-VW                          CR12914
                        (WK-806-3166-IDX) EQUAL SPACES)                 CR12914
                     CONTINUE                                           CR12914
                   ELSE                                                 CR12914
                     MOVE WK-806-3166-IDX     TO WW-600-CLM-EXC-IDX     CR12914
                     MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI      CR12914
                     PERFORM S600-000-INSERT-EXCEPTION                  CR12914
                   END-IF                                               CR12914
                END-IF                                                  CR12914
           END-IF.                                                      CR12914
      *                                                                 CR12914
       S927-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
       S928-000-OPEN-SCHDL-II-CURS SECTION.                             CR12914
      *----------------------------------------------------------------*CR12914
      *     S928-000-OPEN-SCHEDUL-CURSOR                               *CR12914
      *----------------------------------------------------------------*CR12914
       S928-010-START.                                                  CR12914
      *                                                                 CR12914
           EXEC SQL                                                     CR12914
                   OPEN PHARM_SCHEDULE_II_MEM                           CR12914
           END-EXEC.                                                    CR12914
      *                                                                 CR12914
           MOVE SQLCODE OF SQLCA TO WW-001-SQLCODE.                     CR12914
      *                                                                 CR12914
           EVALUATE TRUE                                                CR12914
           WHEN WW-001-SUCCESSFUL-CALL                                  CR12914
               CONTINUE                                                 CR12914
           WHEN OTHER                                                   CR12914
               MOVE WK-001-PROGNAME                                     CR12914
                 TO WD-999-MSG (1)                                      CR12914
               MOVE 'S928-000-OPEN-SCHDL-II-CURS'                       CR12914
                 TO WD-999-MSG (2)                                      CR12914
               MOVE '******************'                                CR12914
                 TO WD-999-MSG (3)                                      CR12914
               MOVE 'OPEN'                                              CR12914
                 TO WD-999-MSG (4)                                      CR12914
               MOVE SPACES                                              CR12914
                 TO WD-999-MSG (5)                                      CR12914
               GO TO S900-000-DUAL-MDUL-DB2-ERR                         CR12914
           END-EVALUATE.                                                CR12914
      *                                                                 CR12914
       S928-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
       S929-000-FETCH-SCHDL-II-CURS SECTION.                            CR12914
      *----------------------------------------------------------------*CR12914
      *     S929-000-FETCH-SCHDL-II-CURS                               *CR12914
      *----------------------------------------------------------------*CR12914
       S929-010-START.                                                  CR12914
      *                                                                 CR12914
             EXEC SQL                                                   CR12914
                   FETCH PHARM_SCHEDULE_II_MEM                          CR12914
                   INTO   :CHDRUGTB-C-QTY-PRSC-AMT                      CR12914
                         ,:CLDRUGTB-C-DRUG-SUB-QTY-AMT                  CR12914
                         ,:WH-000-CLM-HOLD-SCC-01                       CR12914
                         ,:WH-000-CLM-HOLD-SCC-02                       CR12914
                         ,:WH-000-CLM-HOLD-SCC-03                       CR12914
             END-EXEC.                                                  CR12914
      *                                                                 CR12914
           MOVE SQLCODE OF SQLCA TO WW-001-SQLCODE.                     CR12914
      *                                                                 CR12914
           EVALUATE TRUE                                                CR12914
           WHEN WW-001-SUCCESSFUL-CALL                                  CR12914
               SET WS-000-END-O-SCHDL-II-DATA-NOT TO TRUE               CR12914
               ADD +1 TO WH-100-ACCUM-HIST-COUNT                        CR12914
               IF  WH-100-ACCUM-HIST-COUNT = +1                         CR12914
                  MOVE CHDRUGTB-C-QTY-PRSC-AMT TO                       CR12914
                            WH-100-HISTORY-QTY-PRS-AMT                  CR12914
               END-IF                                                   CR12914
               IF W1C40541-C-QTY-PRSC-AMT = WH-100-HISTORY-QTY-PRS-AMT  CR12914
                     CONTINUE                                           CR12914
               ELSE                                                     CR12914
                     IF (W1C52991-R-CLM-EXC-DISP-CD                     CR12914
                       OF W1C52991-C-CNTL-EXC-HDR-VW                    CR12914
                       OF W1C52991-C-CNTL-EXC-VW                        CR12914
                          (WK-806-3165-IDX) EQUAL SPACES)               CR12914
                       CONTINUE                                         CR12914
                     ELSE                                               CR12914
                       MOVE WK-806-3165-IDX     TO WW-600-CLM-EXC-IDX   CR12914
                       MOVE WS-000-LI-SUB       TO WW-600-CLM-EXC-LI    CR12914
                       PERFORM S600-000-INSERT-EXCEPTION                CR12914
                  END-IF                                                CR12914
               END-IF                                                   CR12914
      *                                                                 CR12914
               COMPUTE  WH-100-ACCUM-QTY-DISP-NUM =                     CR12914
                              WH-100-ACCUM-QTY-DISP-NUM  +              CR12914
                            CLDRUGTB-C-DRUG-SUB-QTY-AMT                 CR12914
      *                                                                 CR12914
               IF  (WH-000-CLM-HOLD-SCC-01 > SPACES                     CR12914
                              OR                                        CR12914
                   WH-000-CLM-HOLD-SCC-02 > SPACES                      CR12914
                              OR                                        CR12914
                   WH-000-CLM-HOLD-SCC-03 > SPACES)                     CR12914
                IF (W1C40541-C-DRUG-RX-OVRRD-CD > SPACES AND            CR12914
                   (W1C40541-C-DRUG-RX-OVRRD-CD                         CR12914
                                         = WH-000-CLM-HOLD-SCC-01       CR12914
                                   OR                                   CR12914
                    W1C40541-C-DRUG-RX-OVRRD-CD                         CR12914
                                         = WH-000-CLM-HOLD-SCC-02       CR12914
                                   OR                                   CR12914
                    W1C40541-C-DRUG-RX-OVRRD-CD                         CR12914
                                         = WH-000-CLM-HOLD-SCC-03))     CR12914
                     ADD +1 TO WH-000-SCC-MATCH-COUNT                   CR12914
                ELSE                                                    CR12914
                   IF (W1C40541-C-DRUG-RX-OVR2-CD > SPACES AND          CR12914
                      (W1C40541-C-DRUG-RX-OVR2-CD                       CR12914
                                         = WH-000-CLM-HOLD-SCC-01       CR12914
                                   OR                                   CR12914
                       W1C40541-C-DRUG-RX-OVR2-CD                       CR12914
                                         = WH-000-CLM-HOLD-SCC-02       CR12914
                                   OR                                   CR12914
                       W1C40541-C-DRUG-RX-OVR2-CD                       CR12914
                                         = WH-000-CLM-HOLD-SCC-03))     CR12914
                      ADD +1 TO WH-000-SCC-MATCH-COUNT                  CR12914
                    ELSE                                                CR12914
                      IF (W1C40541-C-DRUG-RX-OVR3-CD > SPACES AND       CR12914
                         (W1C40541-C-DRUG-RX-OVR3-CD                    CR12914
                                         = WH-000-CLM-HOLD-SCC-01       CR12914
                                   OR                                   CR12914
                         W1C40541-C-DRUG-RX-OVR3-CD                     CR12914
                                         = WH-000-CLM-HOLD-SCC-02       CR12914
                                   OR                                   CR12914
                         W1C40541-C-DRUG-RX-OVR3-CD                     CR12914
                                         = WH-000-CLM-HOLD-SCC-03))     CR12914
                         ADD +1 TO WH-000-SCC-MATCH-COUNT               CR12914
                      END-IF                                            CR12914
                    END-IF                                              CR12914
                END-IF                                                  CR12914
               END-IF                                                   CR12914
           WHEN WW-001-DATA-NOT-FOUND                                   CR12914
               SET WS-000-END-O-SCHDL-II-DATA TO TRUE                   CR12914
           WHEN OTHER                                                   CR12914
               MOVE WK-001-PROGNAME                                     CR12914
                 TO WD-999-MSG (1)                                      CR12914
               MOVE 'S929-000-FETCH-SCHDL-II-CURS'                      CR12914
                 TO WD-999-MSG (2)                                      CR12914
               MOVE '******************'                                CR12914
                 TO WD-999-MSG (3)                                      CR12914
               MOVE 'OPEN'                                              CR12914
                 TO WD-999-MSG (4)                                      CR12914
               MOVE SPACES                                              CR12914
                 TO WD-999-MSG (5)                                      CR12914
               GO TO S900-000-DUAL-MDUL-DB2-ERR                         CR12914
           END-EVALUATE.                                                CR12914
      *                                                                 CR12914
       S929-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
       S930-000-CLOSE-SCHDL-II-CURS SECTION.                            CR12914
      *----------------------------------------------------------------*CR12914
      *     S928-000-OPEN-SCHEDUL-CURSOR                               *CR12914
      *----------------------------------------------------------------*CR12914
       S930-010-START.                                                  CR12914
      *                                                                 CR12914
           EXEC SQL                                                     CR12914
                   CLOSE PHARM_SCHEDULE_II_MEM                          CR12914
           END-EXEC.                                                    CR12914
      *                                                                 CR12914
           MOVE SQLCODE OF SQLCA TO WW-001-SQLCODE.                     CR12914
      *                                                                 CR12914
           EVALUATE TRUE                                                CR12914
           WHEN WW-001-SUCCESSFUL-CALL                                  CR12914
               CONTINUE                                                 CR12914
           WHEN OTHER                                                   CR12914
               MOVE WK-001-PROGNAME                                     CR12914
                 TO WD-999-MSG (1)                                      CR12914
               MOVE 'S930-000-CLOSE-SCHDL-II-CURS'                      CR12914
                 TO WD-999-MSG (2)                                      CR12914
               MOVE '******************'                                CR12914
                 TO WD-999-MSG (3)                                      CR12914
               MOVE 'OPEN'                                              CR12914
                 TO WD-999-MSG (4)                                      CR12914
               MOVE SPACES                                              CR12914
                 TO WD-999-MSG (5)                                      CR12914
               GO TO S900-000-DUAL-MDUL-DB2-ERR                         CR12914
           END-EVALUATE.                                                CR12914
      *                                                                 CR12914
       S930-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
      *                                                                 CR12914
       S931-000-7700-CONFIG-CHECK SECTION.                              CR12914
      *----------------------------------------------------------------*CR12914
      *     S931-000-7700-CONFIG-CHECK                                 *CR12914
      *----------------------------------------------------------------*CR12914
       S931-010-START.                                                  CR12914
             INITIALIZE WH-000-ALLOW-SHDL-II-COUNT.                     CR12914
             MOVE W1C40541-R-CUST-PART-NUM  TO GSLDTLTB-R-CUST-PART-NUM CR12914
             MOVE 'C' TO GSLDTLTB-G-LIST-SUBSYS-CD                      CR12914
             MOVE W1C40541-C-HDR-SVC-FST-DT                             CR12914
                                     TO GSLDTLTB-G-LIST-EFF-STRT-DT     CR12914
                                       ,GSLDTLTB-G-LIST-EFF-END-DT      CR12914
             MOVE W1C40541-R-GROUP-ID       TO GSLDTLTB-R-GROUP-ID      CR13997
             MOVE W1C40541-C-NCPDP-TXN-CD   TO GSLDTLTB-C-NCPDP-TXN-CD  CR13997
             MOVE W1C40541-C-DRUG-VERSN-NUM TO                          CR13997
                                        GSLDTLTB-R-NCPDP-VRSN-NUM       CR13997
             MOVE '7700' TO GSLDTLTB-G-LIST-NUM                         CR12914
             EXEC SQL                                                   CR12914
                SELECT G_LIST_STRT_2_LMT                                CR12914
                INTO :GSLDTLTB-G-LIST-STRT-2-LMT                        CR12914
                  FROM G_LIST_DTL_TB                                    CR12914
                 WHERE R_CUST_PART_NUM    = :GSLDTLTB-R-CUST-PART-NUM   CR12914
                  AND G_LIST_SUBSYS_CD   = :GSLDTLTB-G-LIST-SUBSYS-CD   CR12914
                  AND ((R_GROUP_ID       =  :GSLDTLTB-R-GROUP-ID)       CR13997
                   OR (R_GROUP_ID        =  'ALL     '))                CR13997
                  AND ((C_NCPDP_TXN_CD   =  :GSLDTLTB-C-NCPDP-TXN-CD)   CR13997
                   OR (C_NCPDP_TXN_CD    =  'AL'))                      CR13997
                  AND ((R_NCPDP_VRSN_NUM =  :GSLDTLTB-R-NCPDP-VRSN-NUM) CR13997
                   OR (R_NCPDP_VRSN_NUM  =  'AL'))                      CR13997
                  AND G_LIST_EFF_STRT_DT <=                             CR12914
                            :GSLDTLTB-G-LIST-EFF-STRT-DT                CR12914
                  AND G_LIST_STRT_LMT    = :GSLDTLTB-G-LIST-STRT-LMT    CR12914
                  AND G_LIST_END_LMT     = :GSLDTLTB-G-LIST-END-LMT     CR12914
                  AND G_LIST_EFF_END_DT >= :GSLDTLTB-G-LIST-EFF-END-DT  CR12914
                  AND G_LIST_NUM         = :GSLDTLTB-G-LIST-NUM         CR12914
                  AND ROWNUM = 1                                        CR12914
             END-EXEC                                                   CR12914
             MOVE SQLCODE                TO WW-001-SQLCODE              CR12914
                                         WD-999-SQLCODE                 CR12914
             EVALUATE TRUE                                              CR12914
                                                                        CR12914
                WHEN  WW-001-SUCCESSFUL-CALL                            CR12914
                     MOVE FUNCTION NUMVAL(GSLDTLTB-G-LIST-STRT-2-LMT)   CR12914
                               TO WH-000-ALLOW-SHDL-II-COUNT            CR12914
                WHEN WW-001-DATA-NOT-FOUND                              CR12914
                      CONTINUE                                          CR12914
                WHEN OTHER                                              CR12914
                  MOVE WK-001-PROGNAME                                  CR12914
                    TO WD-999-MSG (1)                                   CR12914
                  MOVE 'S931-000-7700-CONFIG-CHECK'                     CR12914
                    TO WD-999-MSG (2)                                   CR12914
                  MOVE 'G_LIST_DTL_TB'                                  CR12914
                    TO WD-999-MSG (3)                                   CR12914
                  MOVE 'SELECT'                                         CR12914
                    TO WD-999-MSG (4)                                   CR12914
                  MOVE SPACES                                           CR12914
                    TO WD-999-MSG (5)                                   CR12914
                  GO TO S900-000-DUAL-MDUL-DB2-ERR                      CR12914
              END-EVALUATE.                                             CR12914
      *                                                                 CR12914
       S931-999-EXIT.  EXIT.                                            CR12914
      *                                                                 CR12914
	  
       S1100-000-CMPND-CONFIG-7506   SECTION.                           CR14386
      ***************************************************************** CR14386
      *                                                               * CR14386
      *+  S1100-000-CMPND-CONFIG-7506  SECTION                        * CR14386
      *+                                                              * CR14386
      *+  THIS SECTION VERIFIES ALLOWED ROUTES OF ADMIN FOR COMPOUND  * CR14386
      *+  CLAIMS                                                      * CR14386
      *+                                                              * CR14386
      ***************************************************************** CR14386
       S1100-010-START.                                                 CR14386
                                                                        CR14386
           MOVE 'N'                      TO WH-000-SYSLIST-EXIST.       CR14386
           MOVE W1C40541-R-CUST-PART-NUM TO GSLDTLTB-R-CUST-PART-NUM.   CR14386
           MOVE WV-G0003-C-CLAIMS        TO GSLDTLTB-G-LIST-SUBSYS-CD.  CR14386
           MOVE '7506'                   TO GSLDTLTB-G-LIST-NUM.        CR14386
           MOVE W1C40541-C-RT-ADMIN-DAT  TO GSLDTLTB-G-LIST-STRT-LMT    CR14386
                                            GSLDTLTB-G-LIST-END-LMT.    CR14386
           MOVE W1C40541-C-HDR-SVC-FST-DT TO GSLDTLTB-G-LIST-EFF-STRT-DTCR14386
                                             GSLDTLTB-G-LIST-EFF-END-DT.CR14386
           MOVE W1C40541-R-GROUP-ID      TO GSLDTLTB-R-GROUP-ID.        CR14386
           MOVE W1C40541-C-NCPDP-TXN-CD  TO GSLDTLTB-C-NCPDP-TXN-CD.    CR14386
           MOVE W1C40541-C-DRUG-VERSN-NUM TO                            CR14386
                                            GSLDTLTB-R-NCPDP-VRSN-NUM.  CR14386
                                                                        CR14386
           EXEC SQL                                                     CR14386
               SELECT 'Y'                                               CR14386
                     ,G_LIST_STRT_2_LMT                                 CR14386
                     ,G_LIST_END_2_LMT                                  CR14386
                     ,G_LIST_STRT_3_LMT                                 CR14386
                     ,G_LIST_STRT_4_LMT                                 CR14386
                 INTO :WH-000-SYSLIST-EXIST                             CR14386
                     ,:GSLDTLTB-G-LIST-STRT-2-LMT                       CR14386
                     ,:GSLDTLTB-G-LIST-END-2-LMT                        CR14386
                     ,:GSLDTLTB-G-LIST-STRT-3-LMT                       CR14386
                     ,:GSLDTLTB-G-LIST-STRT-4-LMT                       CR14386
                 FROM G_LIST_DTL_TB                                     CR14386
              WHERE R_CUST_PART_NUM     = :GSLDTLTB-R-CUST-PART-NUM     CR14386
                AND G_LIST_SUBSYS_CD    = :GSLDTLTB-G-LIST-SUBSYS-CD    CR14386
                AND G_LIST_NUM          = :GSLDTLTB-G-LIST-NUM          CR14386
                AND ((R_GROUP_ID        = :GSLDTLTB-R-GROUP-ID)         CR14386
                 OR  (R_GROUP_ID        = 'ALL     '))                  CR14386
                AND ((C_NCPDP_TXN_CD    = :GSLDTLTB-C-NCPDP-TXN-CD)     CR14386
                 OR  (C_NCPDP_TXN_CD    = 'AL'))                        CR14386
                AND ((R_NCPDP_VRSN_NUM  = :GSLDTLTB-R-NCPDP-VRSN-NUM)   CR14386
                 OR  (R_NCPDP_VRSN_NUM  = 'AL'))                        CR14386
                AND RTRIM(G_LIST_STRT_LMT)    <=                        CR14386
                             RTRIM(:GSLDTLTB-G-LIST-STRT-LMT)           CR14386
                AND RTRIM(G_LIST_END_LMT)     >=                        CR14386
                             RTRIM(:GSLDTLTB-G-LIST-END-LMT)            CR14386
                AND G_LIST_EFF_STRT_DT <= :GSLDTLTB-G-LIST-EFF-STRT-DT  CR14386
                AND G_LIST_EFF_END_DT  >= :GSLDTLTB-G-LIST-EFF-END-DT   CR14386
                AND ROWNUM = 1                                          CR14386
           END-EXEC                                                     CR14386
                                                                        CR14386
           EVALUATE SQLCODE OF SQLCA                                    CR14386
               WHEN                            WK-805-SUCCESSFUL-CALL   CR14386
                   CONTINUE                                             CR14386
               WHEN                            WK-805-DATA-NOT-FOUND    CR14386
                   MOVE 'N' TO WH-000-SYSLIST-EXIST                     CR14386
               WHEN OTHER                                               CR14386
                   MOVE WK-000-PROG-NAM               TO WD-999-MSG (1) CR14386
                   MOVE 'S1100-000-CMPND-CONFIG-7506' TO WD-999-MSG (2) CR14386
                   MOVE 'GSLDTLTB'                  TO WD-999-MSG (3)   CR14386
                   MOVE 'SELECT'                    TO WD-999-MSG (4)   CR14386
                   STRING GSLDTLTB-G-LIST-SUBSYS-CD                     CR14386
                     ' '  GSLDTLTB-G-LIST-NUM                           CR14386
                     ' '  GSLDTLTB-G-LIST-EFF-STRT-DT                   CR14386
                     ' '  GSLDTLTB-G-LIST-STRT-LMT                      CR14386
                        DELIMITED BY SIZE                               CR14386
                        INTO                           WD-999-MSG (5)   CR14386
                   GO TO S900-000-DUAL-MDUL-DB2-ERR                     CR14386
           END-EVALUATE.                                                CR14386
                                                                        CR14386
                                                                        CR14386
       S1100-999-EXIT.                                                  CR14386
           EXIT.                                                        CR14386
                                                                        CR14901


030200                                                                  04000000
030210 S910-000-SQL-ERROR SECTION.                                      04001000
030220*                                                                 04002000
030230***************************************************************** 04003000
030240*                                                               * 04004000
030250*?  S910-000-SQL-ERROR SECTION                                  * 04005000
030260*+                                                              * 04006000
030270*+  THIS SECTION FORMATS THE SQL ERROR RESPONSE.                * 04007000
030280*+                                                              * 04008000
030290***************************************************************** 04009000
030300*                                                                 04010000
030310 S910-010-START.                                                  04011000
030320*                                                                 04012000
030330     MOVE WV-C4398-C-SQL-SOFT-ERROR TO W1C56941-C-MDUL-RTRN-CD.   04013000
030340     MOVE 'POS CONSOLIDATED TO INTERNAL CLAIM'                    04014000
030350                                    TO W1C56941-C-MDUL-NAM.       04015000
030360     MOVE WK-030-PROGRAM-NAME       TO W1C56941-G-PROG-NAM.       04016000
030370     MOVE SQLCODE                   TO W1C56941-G-SQL-CODE-NUM.   04017000
030380     MOVE W1C40541-G-AUD-USER-ID    TO W1C56941-G-AUD-USER-ID.    04018000
030390     MOVE SQLCAID                   TO W1C56941-G-SQL-AID-TX.     04019000
030400     MOVE SQLCABC                   TO W1C56941-G-SQL-ABC-TX.     04020000
030410     MOVE SQLERRM                   TO W1C56941-G-SQL-ERROR-TX.   04021000
030420     MOVE SQLERRP                   TO W1C56941-G-SQL-ERRORP-TX.  04022000
030430     MOVE SQLERRD(1)                TO W1C56941-G-SQL-ERRORD1-TX. 04023000
030440     MOVE SQLERRD(2)                TO W1C56941-G-SQL-ERRORD2-TX. 04024000
030450     MOVE SQLERRD(3)                TO W1C56941-G-SQL-ERRORD3-TX. 04025000
030460     MOVE SQLERRD(4)                TO W1C56941-G-SQL-ERRORD4-TX. 04026000
030470     MOVE SQLERRD(5)                TO W1C56941-G-SQL-ERRORD5-TX. 04027000
030480     MOVE SQLERRD(6)                TO W1C56941-G-SQL-ERRORD6-TX. 04028000
030490     MOVE SQLWARN0                  TO W1C56941-G-SQL-WARNING1-TX.04029000
030500     MOVE SQLWARN1                  TO W1C56941-G-SQL-WARNING2-TX.04030000
030510     MOVE SQLWARN2                  TO W1C56941-G-SQL-WARNING3-TX.04031000
030520     MOVE SQLWARN3                  TO W1C56941-G-SQL-WARNING4-TX.04032000
030530     MOVE SQLWARN4                  TO W1C56941-G-SQL-WARNING5-TX.04033000
030540     MOVE SQLWARN5                  TO W1C56941-G-SQL-WARNING6-TX.04034000
030550     MOVE SQLWARN6                  TO W1C56941-G-SQL-WARNING7-TX.04035000
030560     MOVE SQLWARN7                  TO W1C56941-G-SQL-WARNING8-TX.04036000
030570     MOVE SQLWARN8                  TO W1C56941-G-SQL-WARNING9-TX.04037000
030580     MOVE SQLSTATE                  TO W1C56941-G-SQL-STATE-TX.   04038000
030590*                                                                 04039000
030600 S910-999-EXIT.                                                   04040000
030610     EXIT.                                                        04041000
036117                                                                  04042000
036118                                                                  04043000
030630 S999-000-BAD-RETURN SECTION.                                     04044000
030640*                                                                 04045000
030650***************************************************************** 04046000
030660*                                                               * 04047000
030670*?  S999-000-BAD-RETURN        SECTION                          * 04048000
030680*+                                                              * 04049000
030690*+  WHEN A REJECT CODE IS NOT FOUND IN THE REJECT CONTROL       * 04050000
030700*+  TABLE, REJECT CODE 85 (SYSTEM ERROR) IS POSTED              * 04051000
030710*+  WITH A DENIED STATUS, AND THE TWA ERROR MESSAGE IS          * 04052000
030720*+  FORMATTED.  NOTE:  THE WD-999-MESSAGE ABORT MESSAGE         * 04053000
030730*+  INFORMATION WAS FORMATTED IN S600-INSERT-EXC-CODE.          * 04054000
030740*+                                                              * 04055000
030750***************************************************************** 04056000
030760*                                                                 04057000
030770 S999-010-START.                                                  04058000
030780*                                                                 04059000
030790     MOVE WV-R1737-C-SYSTEM-ERROR    TO WW-000-INT-EXC-CD.        04060000
030800     MOVE WV-R4453-C-CLM-NOT-PROCESSED                            04061000
030810                                     TO WW-000-EXT-EXC-CD.        04062000
030820                                                                  04063000
030830     PERFORM S600-000-INSERT-EXC-CODE.                            04064000
030840                                                                  04065000
030850     PERFORM S810-000-RETURN-TO-CALLER.                           04066000
030860*                                                                 04067000
030870 S999-999-EXIT.                                                   04068000
030880     EXIT.                                                        04069000
030890******************************************************************04070000
030900****************  E N D   O F   P D D C 0 4 2 6  *****************04071000
030910******************************************************************04072000
030920                                                                  04073000

000010*---------------------------------------------------------------*
000020* MEMBER NAME:   S600C1                                         *
000030* AUTHOR:        ACS STATE HEALTHCARE.                          *
000040* PROJECT:       PDCSX2.                                        *
000050* REMARKS:       USED BY:      PDDC0426,                        *
000090*                                                               *
000100*---------------------------------------------------------------*
000110*                                                               *
000120******************************************************************
000130*                                                                *
000140*        SECTION 600     INSERT CLAIM EXCEPTION CODE             *
000150*                                                                *
000160*?     THIS SECTION INSERTS THE EXCEPTION OF INDEX               *
000170*+     WW-600-CLM-EXC-IDX INTO THE NEXT AVALIABLE EXCEPTION.     *
000180*+     IF THE MAXIMUM NUMBER OF EXCEPTIONS HAVE BEEN REACHED,    *
000190*+     THIS ROUTINE WILL NOT POST THE EXCEPTION, RATHER          *
000200*+     EXCEPTION 4899 IS POSTED TO INDICATE THAT AN EXCEPTION    *
000210*+     OVERFLOW CONDITION EXISTS.                                *
000220*                                                                *
000230*      DCLGENS NEEDED:     RCEDEPTB (R_CLM_EXC_DEP_TB)           *
000240*                          RCEEXCTB (R_CLM_EXC_TB)               *
000250*                          RECEEBTB (R_CLM_EXC_EOB_TB)           *
000260*                                                                *
000270*      COPYBOOKS NEEDED:   WKC80650: EXCEPTION INDEX             *
000280*                          WWC60050: WORKING-STORAGE FOR S600C   *
000290*                          WVC4200C: CLAIM EXCEPTION STATUS      *
000300*                          WVR0156C: EXCEPTION DISPOSITION       *
000310*                          WVR1903C: EXCEPTION FORCE APPLY       *
000320*                          WVR1914C: EXCEPTION FORCE DENY        *
000330*                          WVG5800C:                             *
000340*      PROCEDURE COPY:     S900C (DB2 EXCEPTION POSTING ROUTINE) *
000350*                                                                *
000360******************************************************************
000370******************************************************************
000380*                                                                *
000390*+ CHANGE LOG:                                                   *
000400*+ CSR #    PROGRAMMER   DATE      DESCRIPTION                   *
000410*+ ------   ---------- ----------  ------------------------------*
000601*         P LYNN       12/29/2011  INITIAL VERSION FOR MA        *
000602* S600C1    PATTY LYNN 08/02/2012  CHANGES TO ALLOW S600C1 TO BE *
000602*                                  USED BY PDDC8130.             *
000610******************************************************************
000620/
000630 S600C1-010-START.
000640
000650     IF W1C40541-R-CUST-PART-NUM > ZERO
000660       MOVE W1C40541-R-CUST-PART-NUM
000670                           TO RCEDS2TB-R-CUST-PART-NUM
MDM                                 RCEEXCTB-R-CUST-PART-NUM
000680     ELSE
000690       MOVE WW-600-DEFAULT-CUST-PART-NUM
000700                           TO RCEDS2TB-R-CUST-PART-NUM
MDM                                 RCEEXCTB-R-CUST-PART-NUM
000710     END-IF.
000720
000730     MOVE WW-600-CLM-EXC-CD
000740                           TO RCEDS2TB-R-CLM-EXC-CD
MDM                                 RCEEXCTB-R-CLM-EXC-CD.
000750
000760     IF W1C40541-R-GROUP-ID > SPACES
000770       MOVE W1C40541-R-GROUP-ID
000780                           TO RCEDS2TB-R-GROUP-ID
MDM                                 RCEEXCTB-R-GROUP-ID
000790     ELSE
000800       MOVE WW-600-DEFAULT-GROUP-ID
000810                           TO RCEDS2TB-R-GROUP-ID
MDM                                 RCEEXCTB-R-GROUP-ID
000820     END-IF.

00830      IF W1C40541-R-PLAN-ID OF W1C40541-C-LI-DRUG-MAIN-VW          PMLMISS
                                 > SPACES                               PMLMISS
000840     MOVE W1C40541-R-PLAN-ID OF W1C40541-C-LI-DRUG-MAIN-VW            SM27
000850                           TO RCEDS2TB-R-PLAN-ID
           ELSE                                                         PMLMISS
             MOVE WW-600-DEFAULT-PLAN-ID                                PMLMISS
                                 TO RCEDS2TB-R-PLAN-ID                  PMLMISS
000710     END-IF.                                                      PMLMISS
000860
000870     IF   W1C40541-C-BAT-DOC-TY-CD > SPACES
000870       MOVE W1C40541-C-BAT-DOC-TY-CD
000880                           TO RCEDS2TB-C-BAT-DOC-TY-CD
000890     ELSE
             MOVE WW-600-DEFAULT-BAT-DOC-TY-CD
000880                           TO RCEDS2TB-C-BAT-DOC-TY-CD
           END-IF.

000900     IF W1C40541-C-BAT-MED-SRC-CD > SPACES
000910       MOVE W1C40541-C-BAT-MED-SRC-CD
000920                           TO RCEDS2TB-C-BAT-MED-SRC-CD
000930     ELSE
000940       MOVE WW-600-DEFAULT-BAT-MED-SRC-CD
000950                           TO RCEDS2TB-C-BAT-MED-SRC-CD
000960     END-IF.
000970
           IF W1C40541-C-HDR-TY-CD > SPACES                             PMLMISS
00980      MOVE W1C40541-C-HDR-TY-CD
000990                           TO RCEDS2TB-C-HDR-TY-CD
           ELSE                                                         PMLMISS
             MOVE WW-600-DEFAULT-HDR-TY-CD                              PMLMISS
                                 TO RCEDS2TB-C-HDR-TY-CD                PMLMISS
000710     END-IF.                                                      PMLMISS

S600C1     IF W1C40541-C-NCPDP-TXN-CD > SPACES
S600C1        IF W1C40541-C-NCPDP-TXN-CD = WV-C8869-C-RX-REVERSAL-51-D0
S600C1           MOVE WV-C1031-C-CRED-REQ
S600C1             TO RCEDS2TB-C-HDR-TY-CD
S600C1        END-IF
S600C1     ELSE
S600C1        IF WS-000-POS-TRAN-CD = WV-C8869-C-RX-REVERSAL-51-D0
S600C1           MOVE WV-C1031-C-CRED-REQ
S600C1             TO RCEDS2TB-C-HDR-TY-CD
S600C1        END-IF
S600C1     END-IF.
S600C1     IF W1C40541-C-NCPDP-TXN-CD > SPACES
S600C1        IF W1C40541-C-NCPDP-TXN-CD = WV-C8869-C-RX-RE-BILL-51-D0
S600C1           MOVE WV-C1031-C-REPL-REQ
S600C1             TO RCEDS2TB-C-HDR-TY-CD
              END-IF                                                    RFENDIF
S600C1     ELSE
S600C1        IF WS-000-POS-TRAN-CD = WV-C8869-C-RX-RE-BILL-51-D0
S600C1           MOVE WV-C1031-C-REPL-REQ
S600C1             TO RCEDS2TB-C-HDR-TY-CD
S600C1        END-IF
S600C1     END-IF.

001040     EXEC SQL
002150         SELECT R_CLM_EXC_DISP_CD                                 6146
002160               ,R_EXC_FORCE_APP_CD                                6146
002170               ,R_FORCE_DENY_CD                                   6146
                     ,R_CUST_PART_NUM
                     ,R_PLAN_ID
                     ,R_GROUP_ID
PL1                  ,R_CLM_ADJUD_REJ_CD
PL1                  ,R_TXT_EOB_CD
002180         INTO  :RCEDS2TB-R-CLM-EXC-DISP-CD                        6146
002190              ,:RCEDS2TB-R-EXC-FORCE-APP-CD                       6146
002200              ,:RCEDS2TB-R-FORCE-DENY-CD                          6146
002200              ,:RCEDS2TB-R-CUST-PART-NUM                          6146
002200              ,:RCEDS2TB-R-PLAN-ID                                6146
002200              ,:RCEDS2TB-R-GROUP-ID                               6146
PL1                 ,:RCEDS2TB-R-CLM-ADJUD-REJ-CD                       PML
PL1                 ,:RCEDS2TB-R-TXT-EOB-CD                             PML
002210           FROM   R_CLM_EXC_DISP_TB                               6146
002220           WHERE  R_CUST_PART_NUM     = :RCEDS2TB-R-CUST-PART-NUM PML
002230                  AND R_CLM_EXC_CD    = :RCEDS2TB-R-CLM-EXC-CD    6146
002240                  AND C_HDR_TY_CD     = :RCEDS2TB-C-HDR-TY-CD     6146
002250                  AND C_BAT_DOC_TY_CD = :RCEDS2TB-C-BAT-DOC-TY-CD 6146
002260                  AND C_BAT_MED_SRC_CD                            6146
002270                                     = :RCEDS2TB-C-BAT-MED-SRC-CD 6146
002280                   AND (R_PLAN_ID    = :RCEDS2TB-R-PLAN-ID        PML
002280                        OR R_PLAN_ID = '999')                     PML
002290                   AND (R_GROUP_ID   = :RCEDS2TB-R-GROUP-ID       PML
002290                        OR R_GROUP_ID  = 'ALL     ')              PML
                         AND ROWNUM = 1                                 PML
002390     END-EXEC.                                                    SG43
002400
002410     EVALUATE TRUE

002430       WHEN SQLCODE =  WK-805-SUCCESSFUL-CALL
POSTFE         IF WS-000-EC-IN-PRCS-IS-FATAL
POSTFE           MOVE WV-C4200-C-DENY TO RCEDS2TB-R-CLM-EXC-DISP-CD
POSTFE         ELSE
POSTFE           IF RCEDS2TB-R-CLM-EXC-DISP-CD
POSTFE              = (SPACES OR WV-C4200-C-IGNORE)
POSTFE              GO TO S600C1-999-EXIT
POSTFE           END-IF
POSTFE         END-IF
002470
002480       WHEN SQLCODE =  WK-805-DATA-NOT-FOUND
POSTFE         IF WS-000-EC-IN-PRCS-IS-FATAL
POSTFE           MOVE WV-C4200-C-DENY TO RCEDS2TB-R-CLM-EXC-DISP-CD
POSTFE         ELSE
POSTFE           GO TO S600C1-999-EXIT
POSTFE         END-IF

002560       WHEN OTHER
002570
002580         MOVE  'S600C1-000-START           ' TO  WD-999-MSG(2)
002590         MOVE  'R_CLM_EXC_DISP_TB          ' TO  WD-999-MSG(3)    G(3)
PL1            MOVE  'SELECT                     ' TO  WD-999-MSG(4)
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
002800     END-EVALUATE.
002810
MDM        EXEC SQL
MDM            SELECT C_POST_APPL_CD
MDM            INTO  :RCEEXCTB-C-POST-APPL-CD
MDM            FROM   R_CLM_EXC_TB
MDM            WHERE  R_CUST_PART_NUM     = :RCEEXCTB-R-CUST-PART-NUM
MDM                   AND R_CLM_EXC_CD    = :RCEEXCTB-R-CLM-EXC-CD
MDM                   AND (R_GROUP_ID     = :RCEEXCTB-R-GROUP-ID
MDM                     OR R_GROUP_ID     = 'ALL     ')
MDM                   AND ROWNUM = 1
MDM        END-EXEC.
MDM
MDM        EVALUATE TRUE
MDM
MDM          WHEN SQLCODE =  WK-805-SUCCESSFUL-CALL
MDM             CONTINUE
MDM          WHEN SQLCODE =  WK-805-DATA-NOT-FOUND
MDM             MOVE SPACES TO RCEEXCTB-C-POST-APPL-CD
MDM
MDM          WHEN OTHER
MDM            MOVE  'S600C1-000-START           ' TO  WD-999-MSG(2)
MDM            MOVE  'R_CLM_EXC_TB               ' TO  WD-999-MSG(3)
MDM            MOVE  'SELECT                     ' TO  WD-999-MSG(4)
MDM            MOVE RCEEXCTB-R-CUST-PART-NUM
MDM                  TO WW-600-R-CUST-PART-NUM
MDM            STRING  WW-600-R-CUST-PART-NUM
MDM                    ' '
MDM                    RCEEXCTB-R-CLM-EXC-CD
MDM                    ' '
MDM                    RCEEXCTB-R-GROUP-ID         DELIMITED BY SIZE
MDM            INTO                                    WD-999-MSG(5)
MDM            MOVE  SPACES                        TO  WD-999-MSG(6)
MDM            PERFORM S900-000-DUAL-MDUL-DB2-ERR
MDM
MDM        END-EVALUATE.
MDM
002840     EVALUATE TRUE
002850
002860       WHEN W1C40541-C-CNT-EXC-NUM LESS THAN WW-600-MAX-EXC
002870         PERFORM S605C1-000-INSERT-EXC
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
003140             MOVE WW-600-MAX-EXC
003150               TO W1C40541-C-CNT-EXC-NUM
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
003380             MOVE +0
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
003700         END-IF
003710         ADD +2 TO WW-600-MAX-EXC
003720           GIVING W1C40541-C-CNT-EXC-NUM
003730         MOVE WW-600-MAX-EXC
003740           TO W1C40541-C-CNT-EXC-NUM
003750
003760       WHEN OTHER
               CONTINUE

004020     END-EVALUATE.
004030
004040 S600C1-999-EXIT.
004050     EXIT.

005210 S605C1-000-INSERT-EXC SECTION.
005220****************************************************************
005230*
005240*                          S605C1 - INSERT-EXC
005250*
005260*?  POST THE CURRENT EXCEPTION
005270*+
005280*
005290****************************************************************
005300 S605C1-010-START.
005310*--------------------------------------------------------------
005320*    EXCEPTION CODE SHOULD NEVER BE BLANK.  THIS INDICATES
005330*    THAT THE EXCEPTION BEING POSTED IS SET TO -IGNORE- BUT
005340*    SOME PROGRAM IS NOT CHECKING DISPOSITION STATUS BEFORE
005350*    ATTEMPTING TO POST EXCEPTION (HERE).  INSTEAD OF
005360*    POSTING BLANKS AS THE EXCEPTION, S630C1 ROUTINE WILL
005370*    FIND THE EXCEPTION CODE IN THE EXCEPTION TABLE USING
005380*    THE EXCEPTION INDEX.  THEN IT WILL POST THE EXCEPTION
005390*    CODE FOUND ALONG WITH A STATUS OF SUPER-SUSPEND.
005400*-------------------------------------------------------------
005410     IF WW-600-CLM-EXC-CD EQUAL SPACES
005420     THEN
005430         PERFORM S630C1-000-FIND-BLANK-EXC
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
005640         MOVE RCEDS2TB-R-CLM-EXC-CD
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
DF1443         MOVE RCEDS2TB-R-TXT-EOB-CD
005900           TO W1C40541-R-EXC-EOB-ADJUD-CD OF
005910              W1C40541-C-LI-EXC-VW
005920              (W1C40541-C-CNT-EXC-NUM)
DF1443         MOVE RCEDS2TB-R-CLM-ADJUD-REJ-CD
005940           TO W1C40541-R-CLM-ADJUD-REJ-CD OF
005950              W1C40541-C-LI-EXC-VW
005960              (W1C40541-C-CNT-EXC-NUM)
MDM            MOVE RCEEXCTB-C-POST-APPL-CD
MDM              TO W1C40541-C-POST-APPL-CD OF
MDM                 W1C40541-C-LI-EXC-VW
MDM                (W1C40541-C-CNT-EXC-NUM)
006140     END-IF.
006150
006160 S605C1-999-EXIT.
006170     EXIT.
006180/
007720 S630C1-000-FIND-BLANK-EXC SECTION.
007810******************************************************************
007820 S630C1-010-START.
007830
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
008090             MOVE 'S630C1-000-FIND-BLANK-EXC'
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
008280 S630C1-999-EXIT.
008290     EXIT.

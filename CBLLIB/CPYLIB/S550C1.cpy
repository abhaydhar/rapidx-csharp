      *---------------------------------------------------------------*
      * MEMBER NAME:   S550C1                                         *
      * AUTHOR:        XEROX STATE HEALTHCARE.                        *
      * PROJECT:       PDCSX2.                                        *
      * REMARKS:       USED BY:      PDDC0426,                        *
      *                                                               *
      *---------------------------------------------------------------*
      *                                                               *
      ******************************************************************
      *                                                                *
      *        SECTION 550     PROD CLAIM DEFUALT FIELD SETTING        *
      *                                                                *
      *?     THIS SECTION WILL REPLACE CERTAIN FIELDS DEFAULT ENTRIES  *
      *+     SO THAT PROD CLAIMS WON'T DENY UNNECESARIY FOR MISSING    *
      *+     SITUATIONAL FIELDS.                                       *
      *+                                                               *
      *                                                                *
      *      PROCEDURE COPY:     S550C (DB2 EXCEPTION POSTING ROUTINE) *
      *                                                                *
      ******************************************************************
      ******************************************************************
      *                                                                *
      *+ CHANGE LOG:                                                   *
      *+ CSR #    PROGRAMMER   DATE      DESCRIPTION                   *
      *+ ------   ---------- ----------  ------------------------------*
      *  DF3407   M. MELTON  08/06/2013  ADD PROD PCN FOR DEFAULT      *
      *                                  VALUES AND EXCEPTION POSTING  *
      *                                  TO IGNORE.                    *
      ******************************************************************
      /
       S550C1-010-START.

             IF W1C66791-C-NCP-DAW-IND
               (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES
                MOVE ZEROES TO W1C66791-C-NCP-DAW-IND
                              (W1C66791-C-NCPDP-CNSLDTD-RX-X)
      **  EC 4037
                MOVE WV-R1737-C-MISS-DAW-CD TO WW-600-CLM-EXC-CD
                MOVE ZEROES                 TO WW-600-CLM-EXC-LI
                PERFORM S600C1-000-POST-EXCEPTION
                MOVE WV-C4200-C-IGNORE
                  TO W1C40541-C-EXC-STAT-CD OF
                     W1C40541-C-LI-EXC-VW
                     (W1C40541-C-CNT-EXC-NUM)
             END-IF.

      **     IF W1C66791-C-NCP-NEW-REFLL-CD-X                           COR8463
      **       (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES            COR8463
      **        MOVE ZEROES TO W1C66791-C-NCP-NEW-REFLL-CD              COR8463
      **                      (W1C66791-C-NCPDP-CNSLDTD-RX-X)           COR8463
      **  EC 4028                                                       COR8463
      **        MOVE WV-R1737-C-INVALID-REFILL-NUM TO WW-600-CLM-EXC-CD COR8463
      **        MOVE ZEROES                        TO WW-600-CLM-EXC-LI COR8463
      **        PERFORM S600C1-000-POST-EXCEPTION                       COR8463
      **        MOVE WV-C4200-C-IGNORE                                  COR8463
      **          TO W1C40541-C-EXC-STAT-CD OF                          COR8463
      **             W1C40541-C-LI-EXC-VW                               COR8463
      **             (W1C40541-C-CNT-EXC-NUM)                           COR8463
      **     END-IF.                                                    COR8463
                                                                        COR8463
      **     IF W1C66791-C-NCP-RFL-AUTH-NUM                             COR8463
      **       (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES            COR8463
      **        MOVE W1C66791-C-NCP-NEW-REFLL-CD                        COR8463
      **            (W1C66791-C-NCPDP-CNSLDTD-RX-X)                     COR8463
      **          TO W1C66791-C-NCP-RFL-AUTH-NUM                        COR8463
      **            (W1C66791-C-NCPDP-CNSLDTD-RX-X)                     COR8463
      **  EC 3029                                                       COR8463
      **        MOVE WV-R1737-C-MISS-RFL-AUTH-NUM TO WW-600-CLM-EXC-CD  COR8463
      **        MOVE ZEROES                       TO WW-600-CLM-EXC-LI  COR8463
      **        PERFORM S600C1-000-POST-EXCEPTION                       COR8463
      **        MOVE WV-C4200-C-IGNORE                                  COR8463
      **          TO W1C40541-C-EXC-STAT-CD OF                          COR8463
      **             W1C40541-C-LI-EXC-VW                               COR8463
      **             (W1C40541-C-CNT-EXC-NUM)                           COR8463
      **     END-IF.                                                    COR8463

             IF W1C66791-C-NCP-OTHR-INSR-CD-X
               (W1C66791-C-NCPDP-CNSLDTD-RX-X) = HIGH-VALUES
                MOVE ZEROES TO W1C66791-C-NCP-OTHR-INSR-CD
                              (W1C66791-C-NCPDP-CNSLDTD-RX-X)
      **  EC 3006
                MOVE WV-R1737-C-MISS-OTHR-INSR-CD TO WW-600-CLM-EXC-CD
                MOVE ZEROES                       TO WW-600-CLM-EXC-LI
                PERFORM S600C1-000-POST-EXCEPTION
                MOVE WV-C4200-C-IGNORE
                  TO W1C40541-C-EXC-STAT-CD OF
                     W1C40541-C-LI-EXC-VW
                     (W1C40541-C-CNT-EXC-NUM)
             END-IF.

       S550C1-999-EXIT.
           EXIT.


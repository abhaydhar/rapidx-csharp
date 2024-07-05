
      ******************************************************************
      *                                                                *
      *   SHORT TABLE OF VALID VALUES
      *                                                                *
      *   FIELD MNEMONIC: B-BENE-CAP-STAT-CD                           *
      *           NUMBER: 2372                                         *
      *        GENERATED: 2012-12-27 12:33:43                          *
      *     REQUESTED BY: ACS0127  2012-12-27-11.53.22.000000          *
      *                                                                *
      ******************************************************************
       01  WV-B2372-VALID-VALUES.
         05  WV-B2372-CODE-VALUES.
           10  WV-B2372-C-BENEFIT-MET
                           VALUE IS  'A'
                                       PIC  X(00001).
           10  WV-B2372-C-BENEFIT-MET-MAN
                           VALUE IS  'M'
                                       PIC  X(00001).
           10  WV-B2372-C-BENEFIT-NOT-MET
                           VALUE IS  'N'
                                       PIC  X(00001).
         05  WV-B2372-SEARCH-TABLE  REDEFINES
               WV-B2372-CODE-VALUES.
           10  WV-B2372-TABLE-ENTRY
                           OCCURS 00003 TIMES
                           ASCENDING KEY IS
                 WV-B2372-B-BENE-CAP-STAT-CD
                           INDEXED BY WXV-B2372-TABLE-ENTRY.
             15  WV-B2372-B-BENE-CAP-STAT-CD
                                       PIC  X(00001).
         05  WV-B2372-MAX-ENTRIES      PIC S9(04)
                           USAGE IS COMP-3
                           VALUE IS +00003.

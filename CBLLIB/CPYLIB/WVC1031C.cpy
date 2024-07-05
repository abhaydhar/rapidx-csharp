
      ******************************************************************
      *                                                                *
      *   SHORT TABLE OF VALID VALUES
      *                                                                *
      *   FIELD MNEMONIC: C-HDR-TY-CD                                  *
      *           NUMBER: 1031                                         *
      *        GENERATED: 2012-05-07 17:30:59                          *
      *     REQUESTED BY: ACS0127  2012-05-07-16.58.07.765000          *
      *                                                                *
      ******************************************************************
       01  WV-C1031-VALID-VALUES.
         05  WV-C1031-CODE-VALUES.
           10  WV-C1031-C-PA-REQ
                           VALUE IS  'A'
                                       PIC  X(00001).
           10  WV-C1031-C-ELIG-VERF-REQ
                           VALUE IS  'E'
                                       PIC  X(00001).
           10  WV-C1031-C-FIN-TRANS
                           VALUE IS  'F'
                                       PIC  X(00001).
           10  WV-C1031-C-SERVICE-D0
                           VALUE IS  'M'
                                       PIC  X(00001).
           10  WV-C1031-C-PHARMACY-32
                           VALUE IS  'R'
                                       PIC  X(00001).
           10  WV-C1031-C-PHARMACY-51
                           VALUE IS  'S'
                                       PIC  X(00001).
           10  WV-C1031-C-PHARMACY-D0
                           VALUE IS  'T'
                                       PIC  X(00001).
           10  WV-C1031-C-REPL-REQ
                           VALUE IS  'Y'
                                       PIC  X(00001).
           10  WV-C1031-C-CRED-REQ
                           VALUE IS  'Z'
                                       PIC  X(00001).
           10  WV-C1031-C-EDI-837-I
                           VALUE IS  '3'
                                       PIC  X(00001).
           10  WV-C1031-C-EDI-837-P
                           VALUE IS  '4'
                                       PIC  X(00001).
         05  WV-C1031-SEARCH-TABLE  REDEFINES
               WV-C1031-CODE-VALUES.
           10  WV-C1031-TABLE-ENTRY
                           OCCURS 00011 TIMES
                           ASCENDING KEY IS
                 WV-C1031-C-HDR-TY-CD
                           INDEXED BY WXV-C1031-TABLE-ENTRY.
             15  WV-C1031-C-HDR-TY-CD
                                       PIC  X(00001).
         05  WV-C1031-MAX-ENTRIES      PIC S9(04)
                           USAGE IS COMP-3
                           VALUE IS +00011.

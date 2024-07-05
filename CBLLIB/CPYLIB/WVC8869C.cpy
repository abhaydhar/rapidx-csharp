
      ******************************************************************
      *                                                                *
      *   SHORT TABLE OF VALID VALUES
      *                                                                *
      *   FIELD MNEMONIC: C-NCP-POS-TRAN-CD                            *
      *           NUMBER: 8869                                         *
      *        GENERATED: 2011-06-03 12:30:21                          *
      *     REQUESTED BY: ACS0127  2011-06-03-12.15.20.953000          *
      *                                                                *
      ******************************************************************
       01  WV-C8869-VALID-VALUES.
         05  WV-C8869-CODE-VALUES.
           10  WV-C8869-C-RX-BILLING-51-D0
                           VALUE IS  'B1'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-REVERSAL-51-D0
                           VALUE IS  'B2'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-RE-BILL-51-D0
                           VALUE IS  'B3'
                                       PIC  X(00002).
           10  WV-C8869-C-CS-RPT-51-D0
                           VALUE IS  'C1'
                                       PIC  X(00002).
           10  WV-C8869-C-CS-REVR-51-D0
                           VALUE IS  'C2'
                                       PIC  X(00002).
           10  WV-C8869-C-CS-RB-51-D0
                           VALUE IS  'C3'
                                       PIC  X(00002).
           10  WV-C8869-C-PRE-DET-OF-BENE-D0
                           VALUE IS  'D1'
                                       PIC  X(00002).
           10  WV-C8869-C-ELIG-VF-51-D0
                           VALUE IS  'E1'
                                       PIC  X(00002).
           10  WV-C8869-C-INFO-RPT-51-D0
                           VALUE IS  'N1'
                                       PIC  X(00002).
           10  WV-C8869-C-INFO-REVR-51-D0
                           VALUE IS  'N2'
                                       PIC  X(00002).
           10  WV-C8869-C-INFO-RB-51-D0
                           VALUE IS  'N3'
                                       PIC  X(00002).
           10  WV-C8869-C-PA-REQ-BILL-51-D0
                           VALUE IS  'P1'
                                       PIC  X(00002).
           10  WV-C8869-C-PA-REVR-51-D0
                           VALUE IS  'P2'
                                       PIC  X(00002).
           10  WV-C8869-C-PA-INQ-51-D0
                           VALUE IS  'P3'
                                       PIC  X(00002).
           10  WV-C8869-C-PA-REQ-51-D0
                           VALUE IS  'P4'
                                       PIC  X(00002).
           10  WV-C8869-C-SERVICE-BILLING-D0
                           VALUE IS  'S1'
                                       PIC  X(00002).
           10  WV-C8869-C-SERVICE-REVERS-D0
                           VALUE IS  'S2'
                                       PIC  X(00002).
           10  WV-C8869-C-SERVICE-REBILL-D0
                           VALUE IS  'S3'
                                       PIC  X(00002).
           10  WV-C8869-C-ELIGIBILITY-VERIF
                           VALUE IS  '00'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-1-BILLING
                           VALUE IS  '01'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-2-BILLING
                           VALUE IS  '02'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-3-BILLING
                           VALUE IS  '03'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-4-BILLING
                           VALUE IS  '04'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-1-REVERSAL
                           VALUE IS  '11'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-1-DOWN-BILLING
                           VALUE IS  '21'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-2-DOWN-BILLING
                           VALUE IS  '22'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-3-DOWN-BILLING
                           VALUE IS  '23'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-4-DOWN-BILLING
                           VALUE IS  '24'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-1-RE-BILLING
                           VALUE IS  '31'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-2-RE-BILLING
                           VALUE IS  '32'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-3-RE-BILLING
                           VALUE IS  '33'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-4-RE-BILLING
                           VALUE IS  '34'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-1-RE-BILL-HO
                           VALUE IS  '41'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-2-RE-BILL-HO
                           VALUE IS  '42'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-3-RE-BILL-HO
                           VALUE IS  '43'
                                       PIC  X(00002).
           10  WV-C8869-C-RX-4-RE-BILL-HO
                           VALUE IS  '44'
                                       PIC  X(00002).
         05  WV-C8869-SEARCH-TABLE  REDEFINES
               WV-C8869-CODE-VALUES.
           10  WV-C8869-TABLE-ENTRY
                           OCCURS 00036 TIMES
                           ASCENDING KEY IS
                 WV-C8869-C-NCP-POS-TRAN-CD
                           INDEXED BY WXV-C8869-TABLE-ENTRY.
             15  WV-C8869-C-NCP-POS-TRAN-CD
                                       PIC  X(00002).
         05  WV-C8869-MAX-ENTRIES      PIC S9(04)
                           USAGE IS COMP-3
                           VALUE IS +00036.

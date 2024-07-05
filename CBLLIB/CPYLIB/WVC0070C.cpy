000010******************************************************************
000020*                                                                *
000030*   SHORT TABLE OF VALID VALUES
000040*                                                                *
000050*   FIELD MNEMONIC: C-BAT-PYMT-TY-CD                             *
000060*           NUMBER: 0070                                         *
000070*        GENERATED: 2002-08-15 10:00:25                          *
000080*                                                                *
000090******************************************************************
000100 01  WV-C0070-VALID-VALUES.
000110   05  WV-C0070-CODE-VALUES.
000120     10  WV-C0070-C-CRE-CLM-AD
000130                     VALUE IS  'A'
000140                                 PIC  X(00001).
000150     10  WV-C0070-C-CRE-CLM-CR
000160                     VALUE IS  'B'
000170                                 PIC  X(00001).
000180     10  WV-C0070-C-CRE-MAS-AD
000190                     VALUE IS  'C'
000200                                 PIC  X(00001).
000210     10  WV-C0070-C-CRE-MAS-CR
000220                     VALUE IS  'D'
000230                                 PIC  X(00001).
000240     10  WV-C0070-C-ADJ-CLM-AD
000250                     VALUE IS  'E'
000260                                 PIC  X(00001).
000270     10  WV-C0070-C-ADJ-MAS-AD
000280                     VALUE IS  'F'
000290                                 PIC  X(00001).
000300     10  WV-C0070-C-HIS-CR-ADJ
000310                     VALUE IS  'G'
000320                                 PIC  X(00001).
000330     10  WV-C0070-C-HIS-CR-CRE
000340                     VALUE IS  'H'
000350                                 PIC  X(00001).
000360     10  WV-C0070-C-HIS-CR-MA
000370                     VALUE IS  'I'
000380                                 PIC  X(00001).
000390     10  WV-C0070-C-HIS-CR-MC
000400                     VALUE IS  'J'
000410                                 PIC  X(00001).
000420     10  WV-C0070-C-HIS-ADJ-CA
000430                     VALUE IS  'K'
000440                                 PIC  X(00001).
000450     10  WV-C0070-C-HIS-ADJ-MA
000460                     VALUE IS  'L'
000470                                 PIC  X(00001).
000480     10  WV-C0070-C-NORM-PAY
000490                     VALUE IS  '0'
000500                                 PIC  X(00001).
000510     10  WV-C0070-C-HIS-NO-PAY
000520                     VALUE IS  '1'
000530                                 PIC  X(00001).
000540     10  WV-C0070-C-GA-DEBIT
000550                     VALUE IS  '2'
000560                                 PIC  X(00001).
000570     10  WV-C0070-C-GA-CREDIT
000580                     VALUE IS  '3'
000590                                 PIC  X(00001).
000600     10  WV-C0070-C-GA-HIS-DEB
000610                     VALUE IS  '6'
000620                                 PIC  X(00001).
000630     10  WV-C0070-C-GA-HIST-CR
000640                     VALUE IS  '7'
000650                                 PIC  X(00001).
000660   05  WV-C0070-SEARCH-TABLE  REDEFINES
000670         WV-C0070-CODE-VALUES.
000680     10  WV-C0070-TABLE-ENTRY
000690                     OCCURS 00018 TIMES
000700                     ASCENDING KEY IS
000710           WV-C0070-C-BAT-PYMT-TY-CD
000720                     INDEXED BY WXV-C0070-TABLE-ENTRY.
000730       15  WV-C0070-C-BAT-PYMT-TY-CD
000740                                 PIC  X(00001).
000750   05  WV-C0070-MAX-ENTRIES      PIC S9(04)
000760                     USAGE IS COMP-3
000770                     VALUE IS +00018.

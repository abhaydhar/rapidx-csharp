000010******************************************************************
000020*                                                                *
000030*   SHORT TABLE OF VALID VALUES
000040*                                                                *
000050*   FIELD MNEMONIC: R-CLM-EXC-DISP-CD                            *
000060*           NUMBER: 0156                                         *
000070*                                                                *
000080******************************************************************
000090 01  WV-R0156-VALID-VALUES.
000100   05  WV-R0156-CODE-VALUES.
000110     10  WV-R0156-C-BYPASS
000120                     VALUE IS  SPACE
000130                                 PIC  X(00001).
000140     10  WV-R0156-C-REJECT
000150                     VALUE IS  'R'
000160                                 PIC  X(00001).
000170     10  WV-R0156-C-IGNORE
000180                     VALUE IS  'Z'
000190                                 PIC  X(00001).
000200     10  WV-R0156-C-SUPER-SUSP
000210                     VALUE IS  '1'
000220                                 PIC  X(00001).
000230     10  WV-R0156-C-DENY-AND-REPORT
000240                     VALUE IS  '2'
000250                                 PIC  X(00001).
000260     10  WV-R0156-C-DENY
000270                     VALUE IS  '3'
000280                                 PIC  X(00001).
000290     10  WV-R0156-C-SUSPEND
000300                     VALUE IS  '4'
000310                                 PIC  X(00001).
000320     10  WV-R0156-C-PAY-AND-REPORT
000330                     VALUE IS  '5'
000340                                 PIC  X(00001).
000350     10  WV-R0156-C-PAY
000360                     VALUE IS  '6'
000370                                 PIC  X(00001).
000380   05  WV-R0156-SEARCH-TABLE  REDEFINES
000390         WV-R0156-CODE-VALUES.
000400     10  WV-R0156-TABLE-ENTRY
000410                     OCCURS 00009 TIMES
000420                     ASCENDING KEY IS
000430           WV-R0156-R-CLM-EXC-DISP-CD
000440                     INDEXED BY WXV-R0156-TABLE-ENTRY.
000450       15  WV-R0156-R-CLM-EXC-DISP-CD
000460                                 PIC  X(00001).
000470   05  WV-R0156-MAX-ENTRIES      PIC S9(04)
000480                     USAGE IS COMP-3
000490                     VALUE IS +00009.

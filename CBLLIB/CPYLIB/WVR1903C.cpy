000010******************************************************************
000020*                                                                *
000030*   SHORT TABLE OF VALID VALUES
000040*                                                                *
000050*   FIELD MNEMONIC: R-EXC-FORCE-APP-CD                           *
000060*           NUMBER: 1903                                         *
000070*                                                                *
000080******************************************************************
000090 01  WV-R1903-VALID-VALUES.
000100   05  WV-R1903-CODE-VALUES.
000110     10  WV-R1903-C-CAN-FORCE
000120                     VALUE IS  '0'
000130                                 PIC  X(00001).
000140     10  WV-R1903-C-CANT-FORCE
000150                     VALUE IS  '1'
000160                                 PIC  X(00001).
000170     10  WV-R1903-C-NEVER-FORC
000180                     VALUE IS  '2'
000190                                 PIC  X(00001).
000200   05  WV-R1903-SEARCH-TABLE  REDEFINES
000210         WV-R1903-CODE-VALUES.
000220     10  WV-R1903-TABLE-ENTRY
000230                     OCCURS 00003 TIMES
000240                     ASCENDING KEY IS
000250           WV-R1903-R-EXC-FORCE-APP-CD
000260                     INDEXED BY WXV-R1903-TABLE-ENTRY.
000270       15  WV-R1903-R-EXC-FORCE-APP-CD
000280                                 PIC  X(00001).
000290   05  WV-R1903-MAX-ENTRIES      PIC S9(04)
000300                     USAGE IS COMP-3
000310                     VALUE IS +00003.

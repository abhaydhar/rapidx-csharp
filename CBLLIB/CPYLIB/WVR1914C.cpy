000010******************************************************************
000020*                                                                *
000030*   SHORT TABLE OF VALID VALUES
000040*                                                                *
000050*   FIELD MNEMONIC: R-FORCE-DENY-CD                              *
000060*           NUMBER: 1914                                         *
000070*                                                                *
000080******************************************************************
000090 01  WV-R1914-VALID-VALUES.
000100   05  WV-R1914-CODE-VALUES.
000110     10  WV-R1914-C-CAN-DENY
000120                     VALUE IS  '0'
000130                                 PIC  X(00001).
000140     10  WV-R1914-C-CANT-DENY
000150                     VALUE IS  '1'
000160                                 PIC  X(00001).
000170     10  WV-R1914-C-NEVER-DENY
000180                     VALUE IS  '2'
000190                                 PIC  X(00001).
000200   05  WV-R1914-SEARCH-TABLE  REDEFINES
000210         WV-R1914-CODE-VALUES.
000220     10  WV-R1914-TABLE-ENTRY
000230                     OCCURS 00003 TIMES
000240                     ASCENDING KEY IS
000250           WV-R1914-R-FORCE-DENY-CD
000260                     INDEXED BY WXV-R1914-TABLE-ENTRY.
000270       15  WV-R1914-R-FORCE-DENY-CD
000280                                 PIC  X(00001).
000290   05  WV-R1914-MAX-ENTRIES      PIC S9(04)
000300                     USAGE IS COMP-3
000310                     VALUE IS +00003.

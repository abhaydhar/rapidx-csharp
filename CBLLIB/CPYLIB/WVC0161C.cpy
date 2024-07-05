000010******************************************************************
000020*                                                                *
000030*   SHORT TABLE OF VALID VALUES
000040*                                                                *
000050*   FIELD MNEMONIC: C-BAT-DOC-TY-CD                              *
000060*           NUMBER: 0161                                         *
000070*                                                                *
000080******************************************************************
000090 01  WV-C0161-VALID-VALUES.
000100   05  WV-C0161-CODE-VALUES.
000110     10  WV-C0161-C-ADJUSTMENT
000120                     VALUE IS  'A'
000130                                 PIC  X(00001).
000140     10  WV-C0161-C-FFS
000150                     VALUE IS  'C'
000160                                 PIC  X(00001).
000170     10  WV-C0161-C-ENCOUNTER
000180                     VALUE IS  'E'
000190                                 PIC  X(00001).
000200     10  WV-C0161-C-FINTXN
000210                     VALUE IS  'V'
000220                                 PIC  X(00001).
000230   05  WV-C0161-SEARCH-TABLE  REDEFINES
000240         WV-C0161-CODE-VALUES.
000250     10  WV-C0161-TABLE-ENTRY
000260                     OCCURS 00004 TIMES
000270                     ASCENDING KEY IS
000280           WV-C0161-C-BAT-DOC-TY-CD
000290                     INDEXED BY WXV-C0161-TABLE-ENTRY.
000300       15  WV-C0161-C-BAT-DOC-TY-CD
000310                                 PIC  X(00001).
000320   05  WV-C0161-MAX-ENTRIES      PIC S9(04)
000330                     USAGE IS COMP-3
000340                     VALUE IS +00004.


      *****************************************************************
      *                                                               *
      *    MEMBER:  W1C56941                                          *
      *      NAME:  C-MDUL-OUT-WS                                     *
      * SUBSYSTEM:  C Claims                                          *
      *   VERSION:  1                                                 *
      * GENERATED:  2011-08-21 08:00:19 REQUESTED BY ACS0658          *
      *                                                               *
      * Claims Module Output                                          *
      *                                                               *
      *****************************************************************
      *                                                               *
       01  W1C56941-C-MDUL-OUT-WS.                                      S5694
           05  W1C56941-C-MDUL-RTRN-INFO-WS.                            S4468
               10  W1C56941-C-MDUL-NAM                                  F4097
                                       PIC  X(00008).
               10  W1C56941-C-MDUL-RTRN-CD                              F4398
                                       PIC S9(00004) COMP.
           05  W1C56941-G-ERROR-LOG-TB.                                 S0122
               10  W1C56941-G-AUD-TS   PIC  X(00026).                   F0530
               10  W1C56941-G-PROG-NAM                                  F1321
                                       PIC  X(00030).
               10  W1C56941-G-PROG-SECTION-TX                           F1322
                                       PIC  X(00030).
               10  W1C56941-G-AUD-USER-ID                               F0531
                                       PIC  X(00030).
               10  W1C56941-G-KEY-TX   PIC  X(00060).                   F1313
               10  W1C56941-G-SQL-AID-TX                                F3494
                                       PIC  X(00008).
               10  W1C56941-G-SQL-ABC-TX                                F4691
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-CODE-NUM                              F1349
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-ERROR-TX                              F1350
                                       PIC  X(00070).
               10  W1C56941-G-SQL-TABLE-NAM                             F1368
                                       PIC  X(00030).
               10  W1C56941-G-SQL-FUNCTION-TX                           F1351
                                       PIC  X(00008).
               10  W1C56941-G-SQL-ERRORP-TX                             F4876
                                       PIC  X(00008).
               10  W1C56941-G-SQL-ERRORD1-TX                            F5005
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-ERRORD2-TX                            F5678
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-ERRORD3-TX                            F4576
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-ERRORD4-TX                            F4590
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-ERRORD5-TX                            F5608
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-ERRORD6-TX                            F9125
                                       PIC S9(00009) COMP.
               10  W1C56941-G-SQL-WARNING1-TX                           F4977
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING2-TX                           F5017
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING3-TX                           F8165
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING4-TX                           F9031
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING5-TX                           F9222
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING6-TX                           F3223
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING7-TX                           F8931
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING8-TX                           F6015
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING9-TX                           F4619
                                       PIC  X(00001).
               10  W1C56941-G-SQL-WARNING10-TX                          F4220
                                       PIC  X(00001).
               10  W1C56941-G-SQL-STATE-TX                              F5594
                                       PIC  X(00005).
               10  W1C56941-G-ERR-SVRTY-CD                              F5880
                                       PIC  X(00001).
               10  W1C56941-P-ID       PIC S9(00009) COMP.              F1563

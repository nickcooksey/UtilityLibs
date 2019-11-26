%SPLINE-CHANNEL-CONTOURPATH
#COMMENT BEGIN
MACHINE:OPTICS
DATE:10-07-15
TIME:15:16
Program notes:
#COMMENT END
#MCS OFF
#CS OFF
#RTCP OFF
#HSC OFF
#KIN ID[1]
G94
G70 G80 G90
G17 G40
G53
;CCOMP 0.06
D1
#RTCP ON
G70 G90 G17
G54
N100 G0   Z3.307  
N102 G0 X-3.8765 Y.05 Z3.307 B0 C0 
N104 G0   Z1.407  
N106 G01 X-3.8765 Y.05 Z1.307 F40.00
(JETON)
M61
G04 K1.0
M63
G04 K1.0
;START PATH 1
#HSC ON [FAST]
N108 G01 X-3.5658 Y.051 Z1.3572 F10.00
N110 G01 X-3.3597 Y.05 Z1.3869 F10.00
N112 G01 X-3.236 Y.05 Z1.4021 F10.00
N114 G01 X-3.134 Y.05 Z1.4128 F10.00
N116 G01 X-3.0326 Y.05 Z1.4214 F10.00
N118 G01 X-2.9385 Y.05 Z1.4274 F10.00
N120 G01 X-2.8502 Y.05 Z1.4309 F10.00
N122 G01 X-2.6836 Y.05 Z1.4313 F10.00
N124 G01 X-2.5324 Y.05 Z1.4234 F10.00
N126 G01 X-2.3931 Y.05 Z1.4079 F10.00
N128 G01 X-2.2633 Y.05 Z1.3852 F10.00
N130 G01 X-2.1413 Y.05 Z1.3556 F10.00
N132 G01 X-2.1079 Y.05 Z1.3459 F10.00
N134 G01 X-1.9942 Y.05 Z1.3079 F10.00
N136 G01 X-1.8721 Y.05 Z1.259 F10.00
N138 G01 X-1.737 Y.05 Z1.1968 F10.00
N140 G01 X-1.5786 Y.05 Z1.1158 F10.00
N142 G01 X-1.3639 Y.05 Z.999 F10.00
N144 G01 X-1.1847 Y.05 Z.9019 F10.00
N146 G01 X-1.094 Y.05 Z.8547 F10.00
N148 G01 X-.9844 Y.05 Z.8009 F10.00
N150 G01 X-.8262 Y.05 Z.7304 F10.00
N152 G01 X-.666 Y.05 Z.6679 F10.00
N154 G01 X-.5048 Y.05 Z.6136 F10.00
N156 G01 X-.3433 Y.05 Z.5677 F10.00
N158 G01 X-.1824 Y.05 Z.5303 F10.00
N160 G01 X-.0228 Y.05 Z.5014 F10.00
N162 G01 X.1351 Y.05 Z.4811 F10.00
N164 G01 X.2906 Y.05 Z.4691 F10.00
N166 G01 X.4433 Y.05 Z.4655 F10.00
N168 G01 X.593 Y.05 Z.4699 F10.00
N170 G01 X.7008 Y.05 Z.4783 F10.00
N172 G01 X.8445 Y.05 Z.4962 F10.00
N174 G01 X.9986 Y.05 Z.5232 F10.00
N176 G01 X1.168 Y.05 Z.5605 F10.00
N178 G01 X1.3622 Y.05 Z.6108 F10.00
N180 G01 X1.6064 Y.05 Z.6812 F10.00
N182 G01 X1.8959 Y.05 Z.7668 F10.00
N184 G01 X2.0895 Y.05 Z.8207 F10.00
N186 G01 X2.1858 Y.05 Z.8454 F10.00
N188 G01 X2.3873 Y.05 Z.8912 F10.00
N190 G01 X2.5989 Y.05 Z.9312 F10.00
N192 G01 X2.8224 Y.05 Z.9656 F10.00
N194 G01 X3.0609 Y.05 Z.9945 F10.00
N196 G01 X3.3184 Y.05 Z1.0179 F10.00
N198 G01 X3.6019 Y.05 Z1.036 F10.00
N200 G01 X3.924 Y.05 Z1.0491 F10.00
N202 G01 X4.3152 Y.05 Z1.058 F10.00
N204 G01 X4.6218 Y.05 Z1.0623 F10.00
#HSC OFF
;END PATH 1
(JETOFF)
M64
G04 K1.0
M62
G04 K1.0
N206 G0   Z3.307  
M5
#HSC OFF
#RTCP OFF
#KIN ID[1]
#MCS ON
F400
 M30

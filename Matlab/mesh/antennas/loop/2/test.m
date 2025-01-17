P1 = [0;1]; P2 = [0;2]; P3 = [2;2]; P4 = -P1; P5 = -P2; P6 = [2;-2];
C = [0;0]; C1 = [1.3;1]; C2 = [1.3;-1]; h = 0.15; r = 0.4;
S1 = seg(P3,P2,1);
S2 = Seg(P2,P1,1);
A1 = Arc(C,1, -%pi/2, %pi/2, -1, 2);
S3 = Seg(P4,P5,1);
S4 = Seg(P5,P6,1);
T = Ufunc(-%pi/2,%pi/2, "x=2+0.5*cos(t)", "y=2*sin(t)", 3);
Ce = MakeContour(S1,S2,A1,S3,S4,T);
Ci1 = MakeContour(Circle(C1,r,-1,4));
Ci2 = MakeContour(Circle(C2,r,-1,4));
// internals constrained curves:
Ti = Ufunc(-%pi/2,%pi/2, "x=1.8+0.5*cos(t)", "y=1.9*sin(t)", -1);
P7 = [1.2;0.4]; P8 = [1.9;0.4];
P10 = [1.2;-0.4]; P9 = [1.9;-0.4];
S5 = Seg(P7,P8,-2); S6 = Seg(P8,P9,-2);
S7 = Seg(P9,P10,-2); S8 = Seg(P10,P7,-2);
[PtB, EdB, MarkEdB, ne] = BoundaryDiscretisation(h,Ce,Ci1,Ci2,Ti,S5,S6,S7,S8);
Region = [1.1 1.3 ;... // x coordinate of a point in each region
0 0 ;... // y coordinate of a point in each region
0 1 ;... // region markers identifiers
%nan %nan]; // wanted max size of the triangles in each region
[P, T, markT, E, T2T] = triangulate(PtB, EdB, ne, Region);

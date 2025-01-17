
// Display mode
mode(0);

// Display warning for floating point exception
ieee(1);



clear
tic

// Need to sort the path when using code. This will only work if the File
//  is pointed to the Scilab directory.
// !! L.8: Unknown function loadmatfile not converted, original calling sequence used.
loadmatfile("mesh/loop4/loop4Mesh6.mat");
// ! L.9: mtlb(p) can be replaced by p() or p whether p is an M-file or not.
[s1,s2] = size(mtlb_double(mtlb(p)));
if s1==2 then
  p(3,1:length(0)) = 0;
  //to convert 2D to 3D
end;

//Eliminate unnecessary triangles
// ! L.15: mtlb(t) can be replaced by t() or t whether t is an M-file or not.
// !! L.15: Unknown function t not converted, original calling sequence used.
Remove = mtlb_find(bool2s(mtlb_logic(mtlb_double(t(4,":")),">",1)));
t((1:length([]),Remove)) = [];
TrianglesTotal = max(size(t));

//Find areas of separate triangles
for m = 1:TrianglesTotal
  N = t(1:3,m);
  Vec1 = p(:,N(1))-p(:,N(2));
  Vec2 = p(:,N(3))-p(:,N(2));
  // !! L.24: Matlab function cross not yet converted, original calling sequence used.
  Area(1,m) = norm(mtlb_double(Vec1,Vec2))/2;
  // another file for functions
  Center(1:length((1/3)*sum(p(:,N),2)),m) = (1/3)*sum(p(:,N),2);
  // another file for functions
end;

//Find all edge elements ""Edge_"" with at least two adjacent triangles
Edge_ = [];
n = 0;
for m = 1:TrianglesTotal
  N = t(1:3,m);
  for k = m+1:TrianglesTotal
    M = t(1:3,k);
    a = 1-and([N-M(1),N-M(2),N-M(3)],1);
    if sum(a)==2 then //triangles m and k have common edge
     n = n+1;
     Edge_ = [Edge_,M(find(a))];
     TrianglePlus(1,n) = m;
     TriangleMinus(1,n) = k;
    end;
  end;
end;
EdgesTotal = max(size(Edge_));

//This block is only meaningful for T junctions
//It leaves only two edge elements at a junction
Edge__ = [Edge_(2,:);Edge_(1,:)];
Remove = [];
for m = 1:EdgesTotal
  %v0 = [1,EdgesTotal];
  Edge_m = ones(%v0(1),%v0(2)) .*. Edge_(:,m);
  Ind1 = mtlb_any(mtlb_s(Edge_,Edge_m));
  Ind2 = mtlb_any(mtlb_s(Edge__,Edge_m));
  A = mtlb_find(bool2s(mtlb_logic(bool2s(Ind1) .*Ind2,"==",0)));
  if max(size(A))==3 then //three elements formally exist at a junction
   Out = find(bool2s(t(4,mtlb_e(TrianglePlus,A))==t(4,mtlb_e(TriangleMinus,A))));
   Remove = [Remove,A(Out)];
  end;
end;
Edge_(:,Remove) = [];
TrianglePlus = mtlb_i(TrianglePlus,Remove,[]);
TriangleMinus = mtlb_i(TriangleMinus,Remove,[]);
EdgesTotal = max(size(Edge_))


//All structures of this chapter have EdgeIndicator=2
EdgeIndicator = t(4,TrianglePlus)+t(4,TriangleMinus);

//Find edge length
for m = 1:EdgesTotal
  EdgeLength(1,m) = norm(p(:,Edge_(1,m))-p(:,Edge_(2,m)));
end;

toc
//Save result










// !! L.86: Unknown function savematfile not converted, original calling sequence used.
savematfile("mesh1","p","t","Edge_","TrianglesTotal","EdgesTotal","TrianglePlus","TriangleMinus","EdgeLength","EdgeIndicator","Area","Center")

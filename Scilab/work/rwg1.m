

clear all
tic

// Need to sort the path when using code. This will only work if the File 
//  is pointed to the Scilab directory.
loadmatfile('mesh/loop4/loop4Mesh6.mat');
[s1 s2]=size(p);
if(s1==2)
    p(3,:)=0;   //to convert 2D to 3D
end

//Eliminate unnecessary triangles
Remove=find(t(4,:)>1);   
t(:,Remove)=[];           
TrianglesTotal=length(t);

//Find areas of separate triangles
for m=1:TrianglesTotal
   N=t(1:3,m);
   Vec1=p(:,N(1))-p(:,N(2));
   Vec2=p(:,N(3))-p(:,N(2));
   Area(m)=norm(Vec1,Vec2)/2; 
   Center(:,m)=1/3*sum(p(:,N),2);     // another file for functions
end

//Find all edge elements "Edge_" with at least two adjacent triangles
Edge_=[];
n=0;
for m=1:TrianglesTotal
    N=t(1:3,m);
    for k=m+1:TrianglesTotal
        M=t(1:3,k);      
        a=1-all([N-M(1) N-M(2) N-M(3)]);
        if(sum(a)==2) %triangles m and k have common edge
            n=n+1;
            Edge_=[Edge_ M(find(a))]; 
            TrianglePlus(n)=m;
            TriangleMinus(n)=k; 
        end; 
    end
end
EdgesTotal=length(Edge_);

//This block is only meaningful for T junctions
//It leaves only two edge elements at a junction 
Edge__=[Edge_(2,:); Edge_(1,:)];
Remove=[];
for m=1:EdgesTotal
    Edge_m=repmat(Edge_(:,m),[1 EdgesTotal]);
    Ind1=any(Edge_  -Edge_m);
    Ind2=any(Edge__ -Edge_m);
    A=find(Ind1.*Ind2==0);
    if(length(A)==3)    //three elements formally exist at a junction 
        Out=find(t(4,TrianglePlus(A))==t(4,TriangleMinus(A)));
        Remove=[Remove A(Out)];
    end
end
Edge_(:,Remove)         =[];
TrianglePlus(Remove)    =[];
TriangleMinus(Remove)   =[];
EdgesTotal=length(Edge_)


//All structures of this chapter have EdgeIndicator=2
EdgeIndicator=t(4,TrianglePlus)+t(4,TriangleMinus);

//Find edge length
for m=1:EdgesTotal
   EdgeLength(m)=norm(p(:,Edge_(1,m))-p(:,Edge_(2,m)));
end

toc
//Save result
savematfile mesh1  p ...
            t ...
            Edge_ ...
            TrianglesTotal ...
            EdgesTotal ...
            TrianglePlus ...
            TriangleMinus ...
            EdgeLength ...
            EdgeIndicator ...
            Area ...
            Center

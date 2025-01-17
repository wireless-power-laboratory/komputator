% 17 June 2009
% Archemedian Spiral with a value of s as a function of wire-thickness
% for the primary winding:
tp = 16950:00.1:17610; % 660 degrees, n=1.75. 
xp = -tp.*sin(pi/180*tp);
yp = -tp.*cos(pi/180*tp);
% with a gap of 750
% for the secondary winding:
ts = 1440:00.1:16200; % 14580 degrees, n=40.
xs = -ts.*sin(-pi/180*ts);
ys = -ts.*cos(-pi/180*ts);
plot(xp,yp,'k+',xs,ys,'k') % now with thickness.
title('FIG. 2')
% playground
%subplot(211), plot(t,x,'c:')
%ylabel('X')
%title('A Spiral?')
%
%subplot(212), plot(t,y,'Color',[1 0.5 0],'linestyle','-.')
%ylabel('Y')
%title('It Doesn''t Look Like a Spiral...')
%xlabel('Angle, Degrees')
%	Or we could look at multiple plots on a single set of axes:
%clf
%plot(t,x,'go',t,y,'r--')
%title('Still Doesn''t Look Like a Spiral...')
%xlabel('Angle, Degrees')
%
%END OF FILE

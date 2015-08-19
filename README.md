# switcher
Environment Switcher

I seem to end up at work with projects needing different versions of Java/other settings and in the past conflicting Delphi components for different projects.  So to make my life easier I developed a 
a simple Windows Application to switch Environmental Variables, Java Versions and "Subst" Drive (points directly to project folder) according to project being working on.  Having needed to make changes/fix issues decided to rewrite it in C#  

It might be useful for you if so feel free to copy and expand.

You need to compile the project (written in VS 2015) and install the service (so it can start services/change env vars without needing admin prompt) using VS command promt - InstallUtil.exe

Setup things from settings menu - then create projects
using -switch commandline parameter will switch to last project (useful on PC startup)

To extend you can add switchers (follow one existing switcher)



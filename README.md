# SwitchTo
Provides the ability to bring a currently running application window to the foreground and 
optionally send keys to it.

<hr>

Many times when you are running a program such as a 'VMWare vSphere Client', and you have 
a corresponding open window such as a console window which you have minimized to the task 
bar, it cannot easily be restored.

This MS Window's mini-application provides you with an easy, very 'fast' way to solve this 
problem:

Create a shortcut to the following application:
 
`%ProgramFiles(x86)%\SokoolTools\SwitchTo\SwitchTo.exe`

Modify the shortcut, providing as the first argument, the ‘title’ of the window you want 
to “Switch To”, i.e., the title of the window to bring to the front of all other windows 
on the desktop.

Actually all you need provide is the minimum number of characters the title of the window 
begins with which can uniquely identify it from all other windows (for example: "EMN015U").

Now whenever you double-click that particular ‘SwitchTo’ shortcut, the window with the 
aforementioned title, will immediately be displayed on top of all other windows!

### Options

Optionally you can specify as a second argument, a key combination to be sent to the 
window subsequent to it being successfully switched to.

For example: specify "%(wf)" as the key combination to automatically enter full-screen 
mode in a VMware window.

The special keys combination characters are as follows: 
  + = = shift; 
  + % = alt; 
  + ^ = ctrl;

NOTE: If you double-click the application directly (i.e., without providing arguments), 
you will get an informational message indicating the need to provide at minimum one 
'argument'. 

If you provide a window ‘title’ as an argument which does not exist, you will receive a 
warning message without a window being brought forward.

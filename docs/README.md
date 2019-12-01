# SwitchTo
Provides the ability to bring a currently running application window to the foreground and 
optionally send keys to it.

<hr>

Many times when you are running a program such as a 'VMWare vSphere Client', and you have 
a corresponding open window such as a console window which you have minimized to the task 
bar, it cannot be easily restored.

This Microsoft Window's mini-application provides you with an easy, very 'fast' way to solve this 
problem:

Create a shortcut to the following application:
 
`%ProgramFiles(x86)%\SokoolTools\SwitchTo\SwitchTo.exe`
 
Modify the shortcut, providing as the first argument, the ‘title’ of the window you want 
to “Switch To”, i.e., the title of the window to bring to the front of all other windows 
on the desktop.

Actually all you need provide is the minimum number of characters the title of the window 
begins with which can uniquely identify it from all other window titles of current processes (an example be something like: "EMN015U").

Now whenever you double-click that particular ‘SwitchTo’ shortcut, the window with the 
aforementioned title, will immediately be displayed on top of all other windows!

### Options

Optionally you can specify as a second argument, a key combination to be sent to the 
window subsequent to it being successfully switched to.

For example in VMWare, you could specify "%(wf)" as the key combination to automatically enter full-screen 
mode.

The special keys combination characters are as follows: 
  + = = shift; 
  + % = alt; 
  + ^ = ctrl;

NOTE: If you double-click the application directly (i.e., without providing any arguments), 
you will get an informational message indicating the need to provide at a minimum one 
'argument'. 

If you run SwitchTo with a particular window ‘title’ as an argument and a title starting with that does not exist in the current list of running processes, you will receive a warning message - and no window will be brought forward.

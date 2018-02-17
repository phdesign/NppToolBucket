
NppToolBucket [![Build status](https://ci.appveyor.com/api/projects/status/bamdrbfcssox353p?svg=true)](https://ci.appveyor.com/project/phdesign/npptoolbucket)
=============

A plugin for Notepad++ written in C# .NET Framework 3.5.


Features
--------

ToolBucket contains the following features:

* Multi-line search and replace dialog
* Change indentation dialog
* Generate GUID
* Generate Lorem Ipsum
* Compute MD5 Hash
* Compute SHA1 Hash
* Base 64 encode
* Base 64 decode


Download
--------

Release versions can be downloaded from [phdesign.com.au](http://www.phdesign.com.au/npptoolbucket)


Installation
------------

Copy the NppToolBucket.dll file to your Notepad++\plugins directory.


Dependencies
------------

Requires .NET Framework 3.5 or higher to be installed on the system.


Version history
---------------

### v1.10

* Add menu item "Clear find all" to remove all bookmarks
* Add support for Find All / Count to work with "All Open Documents"

### v1.9

* Don't insert newline after last GUID
* Save GUID generation options to config file
* Add option to not prompt for GUID generation options each time (just use config settings)

### v1.8

* Update project template to support creation of x64 version

### v1.7
* Persisting 'Search In' option, window size and window location for the Find and Replace dialog

### v1.6
* Added support for searching and replacing Unicode characters

### v1.5
* Keyboard shortcut Alt+Shift+G to generate GUIDs
* An interface for options to generate GUIDs
* A bugfix for the indentation settings dialog where settings are applied even if cancelled

### v1.4
* Implemented Ctrl-A in textboxes to select all text
* Fixed bug when closing search and replace that active window doesn't return to notepad++
* Increase search / replace history length in dropdown to 60 characters and removed line breaks

### v1.3
* Implemented replace all in all open documents

### v1.2
* Initial release

# VRD-DownladUtility
It's a console application for downloading file from given url or ftp.
URL and Ftp Configuration can be done in TaskJanitor.xml file which is located in VRD-DownladUtility\VRD.ConsoleTasks\bin\Debug\App_Data\ folder.
One can edit the TaskJanitor.xml in notedpad++ and can use shortcut(Ctrl+Shift+Alt+B) to format the file.
It's possible to create multiple node's for ftp or url download configuration. It will be run all the nodes in single run.
The utility is created keeping exapansion in mind. Later, if one's need to add more task he can add new class which contains the logic for task without modifying the existing classes.

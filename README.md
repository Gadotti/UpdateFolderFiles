# UpdateFolderFiles
Console application to quickly update files from one folder to another

## How to use
Simply call de '.exe' file with two arguments. First de folder path with de files to be copied, second the folder path destination

> Example: 	C:\UpdateFolderFiles.exe "C:\System1\bin" "C:\System2\bin"

## How it will work
1. The program will copy and compare only the '.dll' and '.pdb' files, be free to add what you will need.
2. Files that don't exist in the destination path will be created there.
3. Files that has write time more recent in comparsion with the same file in the destination path, will be replaced.
4. Files that has minor write time, will be ignored.

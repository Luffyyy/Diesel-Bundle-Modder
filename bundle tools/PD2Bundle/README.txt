Place exe, hash64.dll, extensions.txt, and paths.txt in the games asset folder. Run executable 
through command line or by clicking on it. Wait a long time. Files should be extracted into the 
extract folder.
If you want to extract textures, sounds, and movies you must pass the "-extract_all" command line 
option to the program.

Other command line options include:
<bundle id> - use only a single bundle for extracting or listing files. For example "pd2bundle.exe all_0" 
	will extract only the contents of all_0.
-list - List the path hashes and file names in bundles. Output is redirected to the standard output. 
	To save you must redirect it to a file. For example "pd2bundle.exe -list > list.txt" will 
	produce a list of all files in all bundles.
-update - Will update extensions and paths to work with current patch of Payday 2.
	This works with multiple commands. For example "pd2bundle.exe -update -extract_all" will
	update extensions and paths and then it will extract all files with textures, sounds, and movies
	using updated extensions and paths.
-update_only - Will only update extensions and paths to work with current patch of Payday 2
	and then it will quit.

To use with PAYDAY: The Heist simply extract the tool into the assets folder of the game and rename 
paths_paydayth_wolfpack.txt to paths.txt after deleting the original paths.txt. Use tool are usual.
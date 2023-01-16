**THIS PROJECT IS CURRENTLY IN DEVELOPEMENT!**
<br><br>

# AIPAKFileDifferentiation
AIPAKFileDifferentiation is a program to display all the differences between two given PAK files for Alien Isolation. This includes creating, modifing and deleting items such as composites, entities, parameter and links.<br>
The goal of this project is to see the differences in a modded PAK file and the vanilla one. For example if a savestation has been added in a certain level it will be displayed as "CREATED" and the composite it is in.

### Features
* Show differences between 2 PAK files
* filters to show/hide different types and difference types
* different views: listview and treeview (as shown in the images below)

### Usage
Select two PAK files via the first two buttons. Click "Show differences". Loading all the differences might take a while depending on the each level and the amount differences both PAKs have.
Filters can be used to hide certain types.

### Download
The program can be downloaded on the [release page](https://github.com/Oliver2Goetz/AIPAKFileDifferentiation/releases). Be sure to download the newest release version

### Interface preview
![Menu](https://github.com/Oliver2Goetz/AIPAKFileDifferentiation/blob/master/images/window.png)

![Menu2](https://github.com/Oliver2Goetz/AIPAKFileDifferentiation/blob/master/images/window2.png)

### Libraries
This program uses the [CathodeLIB](https://github.com/OpenCAGE/CathodeLib) provided by [MattFiler](https://github.com/MattFiler) to read the content of PAK files.

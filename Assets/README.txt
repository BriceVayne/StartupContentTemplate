//TITLE

This is a simple plug-in to setup a new project.

The plug-in can be find in "Tool/Startup Content Template" tab.
All directories parse is displayed under "Directories" section.
Depthness is represented by indentation.
You cannot uncheck an existing folder (ex : Scenes).
The git option add an empty file to push all your empty directories.
Generate button create all directories that are not already created.

You can edit the SCT_JsonSettings to your usage but take care of the followings things :
- Json is extremely sensitive case. An empty settings looks like this : 

{
  "Directories": []
}

- All folders depthness is separated by a '/' character.
- Order in the list doesn't matter.
- No special character.
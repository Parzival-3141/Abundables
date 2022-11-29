# Abundables!

## An easy to use alternative to AssetBundles and Addressables in Unity.

![Editor Window](../repo-media/Editor_Window.png?raw=true)

Abundables is an editor tool to manage AssetBundles. It aims to strike a balance between Unity's bare AssetBundles
and overcomplicated Addressables. You have adequate control
over your Bundles with just a few clicks.

## How does it work?

Click the `Abundables/Open Editor` menu item to open the editor.
![Menu Item](../repo-media/Editor_Menubar.png)
*(You can also import existing AssetBundles here!)*

From there you can create/delete Bundles, add/remove Assets from Bundles, set Runtime Addresses, and
build your Bundles. (See [Diagram](#usage-diagram))

If for whatever reason you need access to the underlying data, it's located in a `ScriptableObject` at
`Assets/Abundables/Data/AbundableData`.

## Usage Diagram
![UI Diagram](../repo-media/Abundables_UI_1.png)
*(Note: Bundling folders is not implemented yet)*

## Installation

### Cloning the repo *(Recommended)*
You'll have to clone the repo in an existing Unity project for Abundables to function properly, which
you can do like so:

```
cd path/to/YourProject/Assets
git clone https://github.com/Parzival-3141/Abundables.git
```

Then let the scripts compile and you're done! 

### Installing as a UnityPackage
UnityPackages are a little finnicky, so TBD for now :/

## Contributing
Feel free to fork and make pull requests. You'll have to clone the repo in an existing Unity project for 
Abundables to function properly.
(See [Installation](#cloning-the-repo-recommended))

Making nice UI's with Unity's GUI API is a [**huge** pain](Editor/AbundablesWindow.cs), so any help cleaning up the Editor is super appreciated!

Avoid referencing anything that may be project specific. If porting to an earlier version of Unity, 
try to maintain compatibility with later versions.

## Tested in Unity 2021.3
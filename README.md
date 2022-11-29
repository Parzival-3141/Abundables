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

### Addresses
Addresses are the bread and butter of this project. They let you define an alias for assets which you can
use at access them at runtime, no libraries needed!

For example, lets say you have lots of audio files in your Bundle, but they're all in different places in your project.
Traditionally you'd have to add them one by one to the Bundle, then load them one by one at runtime.

```cs
var bundle = AssetBundle.LoadFromFile("../mybundle");

var audio1 = bundle.LoadAsset("path/to/audio1")
var audio2 = bundle.LoadAsset("different/path/to/audio2")
var audio3 = bundle.LoadAsset("another/different/path/to/audio3")
// etc
```

But by organizing their Addresses, you can avoid this problem alltogether!

```cs
var bundle = AssetBundle.LoadFromFile("../mybundle");

var audio1 = bundle.LoadAsset("audio/1")
var audio2 = bundle.LoadAsset("audio/2")
var audio3 = bundle.LoadAsset("audio/3")
// etc
```

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
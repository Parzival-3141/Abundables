// Written by joshcamas (https://gist.github.com/joshcamas/7443938f14bc7866e40c52e72a71bc47)
// Adapted by Parzival

using UnityEngine;
using UnityEditor;

namespace Abundables
{
    public static class FullEditorStyles
    {
        public static GUIStyle CenterText(this GUIStyle style)
        {
            return new GUIStyle(style) { alignment = TextAnchor.MiddleCenter };
        }

        public static readonly GUIStyle CNBox = new GUIStyle("CN Box");
        public static readonly GUIStyle CNEntryInfo = new GUIStyle("CN EntryInfo");
        public static readonly GUIStyle CNEntryWarn = new GUIStyle("CN EntryWarn");
        public static readonly GUIStyle CNEntryError = new GUIStyle("CN EntryError");
        public static readonly GUIStyle CNEntryBackEven = new GUIStyle("CN EntryBackEven");
        public static readonly GUIStyle CNEntryBackodd = new GUIStyle("CN EntryBackodd");
        public static readonly GUIStyle CNMessage = new GUIStyle("CN Message");
        public static readonly GUIStyle CNStatusError = new GUIStyle("CN StatusError");
        public static readonly GUIStyle CNStatusWarn = new GUIStyle("CN StatusWarn");
        public static readonly GUIStyle CNStatusInfo = new GUIStyle("CN StatusInfo");
        public static readonly GUIStyle CNCountBadge = new GUIStyle("CN CountBadge");
        public static readonly GUIStyle Box = new GUIStyle("Box");
        //public static readonly GUIStyle LogStyle = new GUIStyle("LogStyle");
        //public static readonly GUIStyle WarningStyle = new GUIStyle("WarningStyle");
        //public static readonly GUIStyle ErrorStyle = new GUIStyle("ErrorStyle");
        //public static readonly GUIStyle EvenBackground = new GUIStyle("EvenBackground");
        //public static readonly GUIStyle OddBackground = new GUIStyle("OddBackground");
        //public static readonly GUIStyle MessageStyle = new GUIStyle("MessageStyle");
        //public static readonly GUIStyle StatusError = new GUIStyle("StatusError");
        //public static readonly GUIStyle StatusWarn = new GUIStyle("StatusWarn");
        //public static readonly GUIStyle StatusLog = new GUIStyle("StatusLog");
        //public static readonly GUIStyle CountBadge = new GUIStyle("CountBadge");
        public static readonly GUIStyle InBigTitle = new GUIStyle("In BigTitle");
        public static readonly GUIStyle MiniLabel = new GUIStyle("miniLabel");
        public static readonly GUIStyle LargeLabel = new GUIStyle("LargeLabel");
        public static readonly GUIStyle BoldLabel = new GUIStyle("BoldLabel");
        public static readonly GUIStyle MiniBoldLabel = new GUIStyle("MiniBoldLabel");
        public static readonly GUIStyle WordWrappedLabel = new GUIStyle("WordWrappedLabel");
        public static readonly GUIStyle WordWrappedMiniLabel = new GUIStyle("WordWrappedMiniLabel");
        public static readonly GUIStyle WhiteLabel = new GUIStyle("WhiteLabel");
        public static readonly GUIStyle WhiteMiniLabel = new GUIStyle("WhiteMiniLabel");
        public static readonly GUIStyle WhiteLargeLabel = new GUIStyle("WhiteLargeLabel");
        public static readonly GUIStyle WhiteBoldLabel = new GUIStyle("WhiteBoldLabel");
        public static readonly GUIStyle MiniTextField = new GUIStyle("MiniTextField");
        public static readonly GUIStyle Radio = new GUIStyle("Radio");
        public static readonly GUIStyle MiniButton = new GUIStyle("miniButton");
        public static readonly GUIStyle MiniButtonLeft = new GUIStyle("miniButtonLeft");
        public static readonly GUIStyle MiniButtonMid = new GUIStyle("miniButtonMid");
        public static readonly GUIStyle MiniButtonRight = new GUIStyle("miniButtonRight");
        public static readonly GUIStyle Toolbar = new GUIStyle("toolbar");
        public static readonly GUIStyle Toolbarbutton = new GUIStyle("toolbarbutton");
        public static readonly GUIStyle ToolbarPopup = new GUIStyle("toolbarPopup");
        public static readonly GUIStyle ToolbarDropDown = new GUIStyle("toolbarDropDown");
        public static readonly GUIStyle ToolbarTextField = new GUIStyle("toolbarTextField");
        public static readonly GUIStyle ToolbarSeachTextField = new GUIStyle("ToolbarSeachTextField");
        public static readonly GUIStyle ToolbarSeachTextFieldPopup = new GUIStyle("ToolbarSeachTextFieldPopup");
        public static readonly GUIStyle ToolbarSeachCancelButton = new GUIStyle("ToolbarSeachCancelButton");
        public static readonly GUIStyle ToolbarSeachCancelButtonEmpty = new GUIStyle("ToolbarSeachCancelButtonEmpty");
        public static readonly GUIStyle SearchTextField = new GUIStyle("SearchTextField");
        public static readonly GUIStyle SearchCancelButton = new GUIStyle("SearchCancelButton");
        public static readonly GUIStyle SearchCancelButtonEmpty = new GUIStyle("SearchCancelButtonEmpty");
        public static readonly GUIStyle HelpBox = new GUIStyle("HelpBox");
        public static readonly GUIStyle AssetLabel = new GUIStyle("AssetLabel");
        public static readonly GUIStyle AssetLabelPartial = new GUIStyle("AssetLabel Partial");
        public static readonly GUIStyle AssetLabelIcon = new GUIStyle("AssetLabel Icon");
        public static readonly GUIStyle SelectionRect = new GUIStyle("selectionRect");
        public static readonly GUIStyle MinMaxHorizontalSliderThumb = new GUIStyle("MinMaxHorizontalSliderThumb");
        public static readonly GUIStyle DropDownButton = new GUIStyle("DropDownButton");
        public static readonly GUIStyle Label = new GUIStyle("Label");
        public static readonly GUIStyle ProgressBarBack = new GUIStyle("ProgressBarBack");
        public static readonly GUIStyle ProgressBarBar = new GUIStyle("ProgressBarBar");
        public static readonly GUIStyle ProgressBarText = new GUIStyle("ProgressBarText");
        public static readonly GUIStyle FoldoutPreDrop = new GUIStyle("FoldoutPreDrop");
        public static readonly GUIStyle INTitle = new GUIStyle("IN Title");
        public static readonly GUIStyle INTitleText = new GUIStyle("IN TitleText");
        public static readonly GUIStyle BoldToggle = new GUIStyle("BoldToggle");
        public static readonly GUIStyle Tooltip = new GUIStyle("Tooltip");
        public static readonly GUIStyle NotificationText = new GUIStyle("NotificationText");
        public static readonly GUIStyle NotificationBackground = new GUIStyle("NotificationBackground");
        public static readonly GUIStyle MiniPopup = new GUIStyle("MiniPopup");
        public static readonly GUIStyle TextField = new GUIStyle("textField");
        public static readonly GUIStyle ControlLabel = new GUIStyle("ControlLabel");
        public static readonly GUIStyle ObjectField = new GUIStyle("ObjectField");
        public static readonly GUIStyle ObjectFieldThumb = new GUIStyle("ObjectFieldThumb");
        public static readonly GUIStyle ObjectFieldMiniThumb = new GUIStyle("ObjectFieldMiniThumb");
        public static readonly GUIStyle Toggle = new GUIStyle("Toggle");
        public static readonly GUIStyle ToggleMixed = new GUIStyle("ToggleMixed");
        public static readonly GUIStyle ColorField = new GUIStyle("ColorField");
        public static readonly GUIStyle Foldout = new GUIStyle("Foldout");
        public static readonly GUIStyle TextFieldDropDown = new GUIStyle("TextFieldDropDown");
        public static readonly GUIStyle TextFieldDropDownText = new GUIStyle("TextFieldDropDownText");
        public static readonly GUIStyle PRLabel = new GUIStyle("PR Label");
        public static readonly GUIStyle ProjectBrowserGridLabel = new GUIStyle("ProjectBrowserGridLabel");
        public static readonly GUIStyle ObjectPickerResultsGrid = new GUIStyle("ObjectPickerResultsGrid");
        public static readonly GUIStyle ObjectPickerBackground = new GUIStyle("ObjectPickerBackground");
        public static readonly GUIStyle ObjectPickerPreviewBackground = new GUIStyle("ObjectPickerPreviewBackground");
        public static readonly GUIStyle ProjectBrowserHeaderBgMiddle = new GUIStyle("ProjectBrowserHeaderBgMiddle");
        public static readonly GUIStyle ProjectBrowserHeaderBgTop = new GUIStyle("ProjectBrowserHeaderBgTop");
        public static readonly GUIStyle ObjectPickerToolbar = new GUIStyle("ObjectPickerToolbar");
        public static readonly GUIStyle PRTextField = new GUIStyle("PR TextField");
        public static readonly GUIStyle PRPing = new GUIStyle("PR Ping");
        public static readonly GUIStyle ProjectBrowserIconDropShadow = new GUIStyle("ProjectBrowserIconDropShadow");
        public static readonly GUIStyle ProjectBrowserTextureIconDropShadow = new GUIStyle("ProjectBrowserTextureIconDropShadow");
        public static readonly GUIStyle ProjectBrowserIconAreaBg = new GUIStyle("ProjectBrowserIconAreaBg");
        public static readonly GUIStyle ProjectBrowserPreviewBg = new GUIStyle("ProjectBrowserPreviewBg");
        public static readonly GUIStyle ProjectBrowserSubAssetBg = new GUIStyle("ProjectBrowserSubAssetBg");
        public static readonly GUIStyle ProjectBrowserSubAssetBgOpenEnded = new GUIStyle("ProjectBrowserSubAssetBgOpenEnded");
        public static readonly GUIStyle ProjectBrowserSubAssetBgCloseEnded = new GUIStyle("ProjectBrowserSubAssetBgCloseEnded");
        public static readonly GUIStyle ProjectBrowserSubAssetBgMiddle = new GUIStyle("ProjectBrowserSubAssetBgMiddle");
        public static readonly GUIStyle ProjectBrowserSubAssetBgDivider = new GUIStyle("ProjectBrowserSubAssetBgDivider");
        public static readonly GUIStyle ProjectBrowserSubAssetExpandBtn = new GUIStyle("ProjectBrowserSubAssetExpandBtn");
    }
}
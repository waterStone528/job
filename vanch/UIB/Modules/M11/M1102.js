// JavaScript source code
var M1102 = {
    FocusModule: null,
    count: 3,
    Init: function () {
        RightToolBar.Disable();
        this.ClearMToolBar();
    },
    ClearMToolBar: function () {
        document.querySelector(".MToolBar>.Buttons").innerHTML = "";
        document.querySelector(".MToolBar>.UserID").setAttribute("data-Login", 0);
        LoadMod("M1103", "", "M11");
    }
};
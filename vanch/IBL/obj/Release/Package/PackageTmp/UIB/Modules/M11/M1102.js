// JavaScript source code
var M1102 = {
    FocusModule: null,
    count: 3,
    Init: function () {
        //RightToolBar.Disable();
        //this.ClearMToolBar();

        var busCode = "opeAccount";
        var opeType = "logout";
        var data = "busCode=" + busCode + "&opeType=" + opeType;

        var ajaxObj = new AJAXC();
        ajaxObj.data = data;
        ajaxObj.success = function () {
            RightToolBar.Disable();
            M1102.ClearMToolBar();
        }
        ajaxObj.start();
    },
    ClearMToolBar: function () {
        document.querySelector(".MToolBar>.Buttons").innerHTML = "";
        document.querySelector(".MToolBar>.UserID").setAttribute("data-Login", 0);
        LoadMod("M1103", "", "M11");
    }
};
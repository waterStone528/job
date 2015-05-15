// JavaScript source code
var MovingBoxCTL = {
    StatusCode: 0,
    MBWidth: 500,
    OffsetValue:30,
    RulePos:2,
    Prepare: function (Title, ContentElement) {
        document.styleSheets[0].rules[3].style.display = "";  //显示遮罩层
        document.querySelector("#MovingBox>.Content").innerHTML = "";
        document.querySelector("#MovingBox>.Title>label").innerText = Title;
        try { var CEW = parseFloat(ContentElement.currentStyle.width.replace("px", "")); } catch (err) { var CEW = parseFloat(window.getComputedStyle(ContentElement, null).width.replace("px", "")); }
        this.CalcMovingBoxWidth(CEW);
        var NewContent = ContentElement.cloneNode(true);
        NewContent.className = "";
        document.querySelector("#MovingBox>.Content").appendChild(NewContent);
        
    },
    Open: function (Mk) {
        if (Mk == 1) { };
        
        document.styleSheets[0].rules[this.RulePos].style.right = "0px"; 
    },
    Close: function (StatusCode) {
        document.styleSheets[0].rules[3].style.display = "none";  //隐藏遮罩层
        document.styleSheets[0].rules[this.RulePos].style.right = "-" + (this.MBWidth + this.OffsetValue + 20) + "px";
    },
    CalcMovingBoxWidth: function (ContentWidth) {
        var Left = parseFloat(document.styleSheets[0].rules[this.RulePos + 1].style.left.replace("px", ""));
        var Right = parseFloat(document.styleSheets[0].rules[this.RulePos + 1].style.right.replace("px", ""));
        if (isNaN(Left)) { Left = 0; }
        if (isNaN(Right)) { Right = 0; }
        this.OffsetValue = Left + Right;
        this.MBWidth = ContentWidth
        this.AdjMovingBoxWidth();
    },
    AdjMovingBoxWidth: function () {
        document.styleSheets[0].rules[this.RulePos].style.width = (this.MBWidth + this.OffsetValue) + "px";
    }
};
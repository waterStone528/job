// JavaScript source code
//the base data for Rules checking can use ajax to read from server.
//below code only for reference, need add code for real ENV.
var IRC = {
    DateMinMax: { Min: null, Max: null },
    ErrClassName: String = "InputErr",
    Pass: Boolean = true,
    InputErrBorder: function (ErrBorderObj, Err) {
        if (Err) {
            if (ErrBorderObj.className.indexOf(this.ErrClassName) > 0) { } else { ErrBorderObj.className += ' ' + this.ErrClassName; }
        } else {
            ErrBorderObj.className = ErrBorderObj.className.replace(" " + this.ErrClassName, "");
        }
    },
    ErrText: function (InpObj, PmpTxt) {
        if (InpObj == null) { return; }
        this.InputErrBorder(InpObj.parentNode, true);
        var ErrBorderObj = InpObj.parentNode;
        if (ErrBorderObj.Data_ErrDivID) { if (document.querySelector("#" + ErrBorderObj.Data_ErrDivID)) { ErrBorderObj.removeChild(document.querySelector("#" + ErrBorderObj.Data_ErrDivID)) }; } else { ErrBorderObj.Data_ErrDivID = "Err4" + InpObj.id; }
        InpObj.style.color = "transparent";

        var ErrDiv = document.createElement("div");
        ErrDiv.id = ErrBorderObj.Data_ErrDivID;
        ErrDiv.style.cssText = "position:relative;width:0px;height:0px;";
        var ErrBorderObjPadding = parseInt(ErrBorderObj.currentStyle.padding.replace("px", ""));
        ErrDiv.innerHTML = "<div class=\"PmpTxt\" style=\"position:absolute;top:" + 0 + "px;left:" + -ErrBorderObjPadding + "px;width:" + (ErrBorderObj.clientWidth + 0) + "px;height:" + (ErrBorderObj.clientHeight - ErrBorderObjPadding) + "px;line-height:" + (ErrBorderObj.clientHeight - ErrBorderObjPadding) + "px" + "\" "
        + "onmousedown=\"this.parentElement.parentElement.querySelector(\'#" + InpObj.id + "\').style.color = \'\';IRC.InputErrBorder(this.parentNode.parentNode, false); this.parentElement.parentElement.querySelector(\'#" + InpObj.id + "\').focus(); this.parentNode.parentNode.removeChild(this.parentNode);\"" + ">" + PmpTxt + "</div>";
        ErrBorderObj.insertBefore(ErrDiv, InpObj);//ErrBorderObj.appendChild(ErrDiv);
    },
    BorrowApDateCk: function (InpObj, InpVlaue) {
        var APdateArr = new Array();
        APdateArr = InpVlaue.split('-')
        var APdateF = new Date(APdateArr[0], parseInt(APdateArr[1] - 1), APdateArr[2]);
        if (APdateF > new Date()) {
            this.InputErrBorder(InpObj.parentNode, false);
            Pass = true;
        } else {
            this.ErrText(InpObj, "格式不正确");
            Pass = false;
        }
        return Pass;
    },
    BorrowAmtCk: function (InpObj, InpValue) {
        if (parseInt(InpValue) == InpValue) {
            this.InputErrBorder(InpObj.parentNode, false);
            if (parseInt(InpValue) >= 100000) {
                this.InputErrBorder(InpObj.parentNode, false);
                Pass = true;
            } else {
                this.ErrText(InpObj, ">=100000的数字");

                Pass = false;
            }
        } else {
            this.ErrText(InpObj, "请输入纯数字的整数");
            Pass = false;
        }
        return Pass;
    },
    BorrowMinAmt: function (InpObj, InpValue) {
        if (parseInt(InpValue) > 5) {
            this.InputErrBorder(InpObj.parentNode, false);
            Pass = true;
        } else {
            this.ErrText(InpObj, ">5的数字");
            Pass = false;
        }
        return Pass;
    },
    DayIncomeRate: function (InpObj, InpValue) {
        if (parseFloat(InpValue) > 0.01) {
            this.InputErrBorder(InpObj.parentNode, false);
            Pass = true;
        } else {
            this.ErrText(InpObj, "日利率必须大于0.01%");
            Pass = false;
        }
        return Pass;
    },
    LoginNameCk: function (InpObj, InpValue) {
        if (InpValue.length == 11) {
            this.InputErrBorder(InpObj.parentNode, false);
            Pass = true;
        } else {
            this.ErrText(InpObj, "必须输入11位手机号");
            Pass = false;
        }
        return Pass;
    },
    LoginPwdCk: function (InpObj, InpValue) {
        if (InpValue.length > 5) {
            this.InputErrBorder(InpObj.parentNode, false);
            Pass = true;
        } else {
            this.ErrText(InpObj, "密码不能为空且大于等于六位字符");
            Pass = false;
        }
        return Pass;
    },
    RegCard: function (InpObj, InpValue) {
        if (InpValue.length == 18) {
            this.InputErrBorder(InpObj.parentNode, false);
            Pass = true;
        }
        else {
            this.ErrText(InpObj, "身份证号必须是18位");
            Pass = false;
        }
        return Pass;
    },

    Init: function (InpObj, ValueMinMax, ErrTxt) {
        ///<summary>输入信息提示</summary>
        ///<param name="InpObj">控件Obj</param><param name="ValueMinMax">当前控件最大值与最小值（对象）</param><param name="ErrTxt">文本信息</param>
        var BorderObj = InpObj.parentNode;      //父框
        var PHDiv = document.createElement("div");
        var BorderObjPadding = parseInt(BorderObj.currentStyle.padding.replace("px", ""));
        PHDiv.id = "IRC" + InpObj.uniqueID;
        InpObj.setAttribute("data-IRC", ErrTxt);      //设置data-IRC属性，方便提交表单取值
        PHDiv.style.cssText = "display: inline;position:relative;width:0px;height:0px;";
        PHDiv.innerHTML = "<div style=\"left:-" + BorderObjPadding + "px;top:0px;width:" + InpObj.clientWidth + "px; height:" + (InpObj.clientHeight - BorderObjPadding) + "px;line-height:" + (InpObj.clientHeight - BorderObjPadding) + "px;color:#6f6f6f;position:absolute; background-color: #222226;opacity:.70; filter: alpha(opacity=70);background-color:rgba(0, 0, 0, 0.0);text-align:center;\" onmousedown=\" " + InpObj.uniqueID + ".focus();this.parentNode.parentNode.removeChild(this.parentNode);\">" + InpObj.getAttribute("data-IRC") + "</div>";
        BorderObj.insertBefore(PHDiv, InpObj);
        InpObj.SDMM = ValueMinMax;                       //设置控件规格判断
    },
    ErrTip: function (InpObj, ErrTxt) {
        this.InputErrBorder(InpObj.parentNode, true);
        var ErrBorderObj = InpObj.parentNode;//td
        if (ErrBorderObj.Data_ErrDivID) { if (document.querySelector("#" + ErrBorderObj.Data_ErrDivID)) { ErrBorderObj.removeChild(document.querySelector("#" + ErrBorderObj.Data_ErrDivID)) }; } else { ErrBorderObj.Data_ErrDivID = "Err4" + InpObj.uniqueID; }
        InpObj.style.color = "transparent";
        var ErrDiv = document.createElement("div");
        ErrDiv.id = ErrBorderObj.Data_ErrDivID;
        ErrDiv.style.cssText = "display:inline; white-space: nowrap; -ms-word-break: keep-all;position:relative;width:0px;height:0px;";
        var ErrBorderObjPadding = parseInt(ErrBorderObj.currentStyle.padding.replace("px", ""));
        ErrDiv.innerHTML = "<div class=\"PmpTxt\" style=\"background-color:#000; position:absolute;top:0px;left:" + -ErrBorderObjPadding + "px;width:" + (InpObj.clientWidth + 0) + "px;height:" + (InpObj.clientHeight) + "px;line-height:" + (InpObj.clientHeight - ErrBorderObjPadding) + "px" + "\">"+ErrTxt+"</div>";
        ErrDiv.setAttribute("onmouseup", InpObj.uniqueID + ".style.color='';IRC.InputErrBorder(this.parentNode, false); " + InpObj.uniqueID + ".focus();if(document.querySelectorAll('#IRC" + InpObj.uniqueID + "').length>0){this.parentNode.removeChild(document.querySelector('#IRC" + InpObj.uniqueID + "'));}; this.parentNode.removeChild(this);");
        ErrBorderObj.insertBefore(ErrDiv, InpObj);//ErrBorderObj.appendChild(ErrDiv);
    },
    IntervalNumber: function (InpObj, InpObjValue, ValueMinMax) {
        ///<summary>输入框错误信息提示</summary>
        ///<param name="InpObj">控件Obj</param><param name="InpObjValue">输入的值</param><param name="ValueMinMax">当前控件最大值与最小值（对象）</param>
        if (ValueMinMax.Min == null || ValueMinMax.Max == null) {
            if (parseFloat(InpObjValue) == InpObjValue) {
                InpObj.className = InpObj.className.replace(" " + this.ErrClassName, ""); Pass = true;
                if (parseFloat(InpObjValue) < parseFloat(ValueMinMax.Min)) { this.ErrTip(InpObj, ">=" + ValueMinMax.Min);Pass = false;}
            }
            else { this.ErrTip(InpObj, "请输入数字"); Pass = false; }
        } else {
            if (parseFloat(InpObjValue) == InpObjValue && parseFloat(InpObjValue) >= parseFloat(ValueMinMax.Min) && parseFloat(InpObjValue) <= parseFloat(ValueMinMax.Max)) {
                InpObj.className = InpObj.className.replace(" " + this.ErrClassName, "");Pass = true;
            }else{
                this.ErrTip(InpObj, ValueMinMax.Min + "~" + ValueMinMax.Max); Pass = false;
            }
        }
        return Pass;
    },

    DateInit: function (InpObj, ErrTxt) {
        ///<summary>输入日期提示</summary>
        ///<param name="DMin">最小日期</param><param name="DMin">最大日期</param><param name="ErrTxt">提示信息</param>
        var BorderObj = InpObj.parentNode;      //父框
        var PHDiv = document.createElement("div");
        var BorderObjPadding = parseInt(BorderObj.currentStyle.padding.replace("px", ""));
        PHDiv.id = "IRC" + InpObj.uniqueID;
        InpObj.setAttribute("data-IRC", ErrTxt);      //设置data-IRC属性，方便提交表单取值
        PHDiv.style.cssText = "display: inline;position:relative;width:0px;height:0px;";
        PHDiv.innerHTML = "<div style=\"left:-" + BorderObjPadding + "px;top:0px;width:" + InpObj.clientWidth + "px; height:" + (InpObj.clientHeight - BorderObjPadding) + "px;line-height:" + (InpObj.clientHeight - BorderObjPadding) + "px;color:#6f6f6f;position:absolute;\" onmousedown=\" " + InpObj.uniqueID + ".focus();this.parentNode.parentNode.removeChild(this.parentNode);\">" + InpObj.getAttribute("data-IRC") + "</div>";
        BorderObj.insertBefore(PHDiv, InpObj);
    },
    IntervalDate: function (InpObj, InpObjValue) {
        ///<summary>日期错误信息提示</summary>
        ///<param name="InpObj">控件Obj</param><param name="InpObjValue">输入的值</param>
        
        if (this.DateMinMax.Min == null || this.DateMinMax.Max == null) {
            InpObj.className = InpObj.className.replace(" " + this.ErrClassName, "");
            Pass = true;
        } else {
            if (new Date(InpObjValue) >= new Date(this.DateMinMax.Min) && new Date(InpObjValue) <= new Date(this.DateMinMax.Max)) {
                InpObj.className = InpObj.className.replace(" " + this.ErrClassName, "");
                Pass = true;
            } else {
                this.InputErrBorder(InpObj.parentNode, true);
                var ErrBorderObj = InpObj.parentNode;//td
                if (ErrBorderObj.Data_ErrDivID) { if (document.querySelector("#" + ErrBorderObj.Data_ErrDivID)) { ErrBorderObj.removeChild(document.querySelector("#" + ErrBorderObj.Data_ErrDivID)) }; } else { ErrBorderObj.Data_ErrDivID = "Err4" + InpObj.uniqueID; }
                InpObj.style.color = "transparent";
                var ErrDiv = document.createElement("div");
                ErrDiv.id = ErrBorderObj.Data_ErrDivID;
                ErrDiv.style.cssText = "display: inline;position:relative;width:0px;height:0px;";
                var ErrBorderObjPadding = parseInt(ErrBorderObj.currentStyle.padding.replace("px", ""));
                ErrDiv.innerHTML = "<div class=\"PmpTxt\" style=\"background-color:#000; position:absolute;top:" + 0 + "px;left:" + -ErrBorderObjPadding + "px;width:" + (InpObj.clientWidth + 0) + "px;height:" + (InpObj.clientHeight - ErrBorderObjPadding - 1) + "px;line-height:" + (InpObj.clientHeight - ErrBorderObjPadding) + "px" + "\">" + InpObj.getAttribute("data-IRC") + "</div>";
                ErrDiv.setAttribute("onmouseup", InpObj.uniqueID + ".style.color='white';CControl.ItemInfoDsp(this.parentNode.querySelector('div'), 0);IRC.InputErrBorder(this.parentNode, false); if(document.querySelectorAll('#IRC" + InpObj.uniqueID + "').length>0){this.parentNode.removeChild(document.querySelector('#IRC" + InpObj.uniqueID + "'));}; this.parentNode.removeChild(this);");
                ErrBorderObj.insertBefore(ErrDiv, InpObj);//ErrBorderObj.appendChild(ErrDiv);
                Pass = false;
            }
        }
        return Pass;
    },
};

// JavaScript source code
//var TransingStatus = {
//    StatusCode: 0,
//    DelayClose: function () {
//        window.setTimeout("TransingBoxi.style.display = 'none';TransingBoxi.querySelector('.LoseReason').style.display = 'none';", 5000);
//    },
//    SetStatus: function (StatusCode,Str) {
//        this.StatusCode = StatusCode;
//        switch (StatusCode) {
//            case 2:
//                TransingBox.querySelector("label").innerText = "交易执行失败";
//                TransingBox.querySelectorAll('div')[0].style.display = 'block';
//                TransingBox.querySelectorAll('div')[0].className = 'icon-Error';
//                //TransingBox.querySelector("img").src = "Images/TransactionWrong.png";
//                TransingBox.querySelector("img").style.display = "none";
//                TransingBox.querySelector(".LoseReason").style.display = "";
//                if (Str != null) { TransingBox.querySelector(".LoseReason>label").innerText = Str; }
//                this.DelayClose();
//                TransingBox.removeAttribute("onmouseup");
//                break;
//            case 1:
//                TransingBox.querySelector("label").innerText = "交易执行中.....";
//                TransingBox.querySelectorAll('div')[0].style.display = 'none';
//                TransingBox.querySelector("img").style.display = "block";
//                TransingBox.querySelector("img").src = "Images/loading.gif";
//                TransingBox.querySelector(".LoseReason").style.display = "none";
//                TransingBoxi.style.display = "";
//                TransingBox.removeAttribute("onmouseup");
//                break;
//            case 3:
//                TransingBox.querySelector("label").innerText = "交易执行成功";
//                TransingBox.querySelectorAll('div')[0].style.display = 'block';
//                TransingBox.querySelector("img").style.display = "none";
//                //TransingBox.querySelector("img").src = "Images/TransactionRight.png";
//                TransingBox.querySelector(".LoseReason").style.display = "none";
//                window.setTimeout("TransingBox.setAttribute(\"onmouseup\", \"TransingBoxi.style.display = 'none'\");", 500);
//                this.DelayClose();
//                break;
//            default:
//                break;
//        }
//    }
//};


var ExecutionBOX = document.querySelector('.ExecutionBox');
var FaiTextBox = document.querySelector('.FaiTextback')
var loadingBOXDisappearIns = null;
var TransingStatus = {
    StatusCode: 0,
    DelayClose: function () {
        window.setTimeout("TransingBoxi.style.display = 'none';TransingBoxi.querySelector('.LoseReason').style.display = 'none';", 5000);
    },
    SetStatus: function (StatusCode, Str) {
        clearTimeout(loadingBOXDisappearIns);
        TransingBoxi.style.display = ''
        if (StatusCode == 1) {//执行中... 
            ExecutionBOX.setAttribute('data-status', '1');
            //document.querySelector('.FaiExecution').style.display = "";
        }
        if (StatusCode == 2) {//执行失败
            FaiTextBox.removeAttribute('data-text');
            ExecutionBOX.setAttribute('data-status', '3');
            ExecutionBOX.querySelector('.subwrap').innerText = '尊敬的刘德华先生！您的账户V币不足，请尽快充值！';
            ExecutionBOX.focus();
            ExecutionBOX.setAttribute('data-status', '3a');
        }
        if (StatusCode == 3) {//执行成功
            ExecutionBOX.setAttribute('data-status', '2')
            loadingBOXDisappearIns = setTimeout("ExecutionBOX.setAttribute('data-status', '2a');", 2000);
        }
    }

}
function FaiTextEM() {
    if (FaiTextBox.getAttribute('data-text') == 'a')
    { FaiTextBox.setAttribute('data-text', 'b'); setTimeout("document.querySelector('.FaiTextback').style.display = 'none'", 500); }
    else { FaiTextBox.setAttribute('data-text', 'a'); FaiTextBox.style.display = 'block'; }
}

    


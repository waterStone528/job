// JavaScript source code

var TransingStatus = {
    ExecutionBOX:function(){return document.querySelector(".ExecutionBox")},
    FaiTextBox: function () { return document.querySelector(".FaiTextback") },
    loadingBOXDisappearIns: null,

    SetStatus: function (Act, msg) {
        clearTimeout(this.loadingBOXDisappearIns);
        if (Act == 1) {//执行中...
            this.ExecutionBOX().setAttribute('data-status', '1');
            document.querySelector('.FaiExecution').style.display = "";
        }
        if (Act == 3) {//执行成功
            this.ExecutionBOX().setAttribute('data-status', '2')
            this.loadingBOXDisappearIns = setTimeout("TransingStatus.ExecutionBOX().setAttribute('data-status', '2a');", 2000);
        }
        if (Act == 2) {//执行失败
            this.FaiTextBox().removeAttribute('data-text');
            this.ExecutionBOX().setAttribute('data-status', '3');
            this.ExecutionBOX().querySelector('.subwrap').innerText = '尊敬的刘德华先生！您的账户V币不足，请尽快充值！';
            this.ExecutionBOX().focus();
            this.ExecutionBOX().setAttribute('data-status', '3a');
        }
    },

    FaiTextEM: function () {
        if (this.FaiTextBox().getAttribute('data-text') == 'a') {
            this.FaiTextBox().setAttribute('data-text', 'b');
            setTimeout("document.querySelector('.FaiTextback').style.display = 'none'", 500);
        }else {
            this.FaiTextBox().setAttribute('data-text', 'a'); this.FaiTextBox().style.display = 'block';
        }
    }
};

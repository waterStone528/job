// JavaScript source code
//
var IB = {
    FocusItem: Object,
    MBElement: null,
    MBTitle: null,

    Del: function () {
        if (ItemContainer.contains(this.FocusItem)) { ItemContainer.removeChild(this.FocusItem); }
        ReCalcItemSize();
    },
    MovingBox: function (BtObj, Element) {
        var Title = BtObj.parentElement.parentElement.querySelectorAll(".ItemText")[0].innerText;
        MovingBoxCTL.Prepare(Title + " - 融资方：" + BtObj.innerText, document.querySelector("." + Element));
        MovingBoxCTL.Open();
    },
    BtAction: function (BtObj, Act) {
        /// <summary>寻找债权中按钮功能</summary>
        ///<param name="BtObj">焦点元素</param>
        ///<param name="Act">0:修改点击后的样式; 1:隐藏Item; 2:抵押物信息; 3:融资方信息; 4:预约确认; 5:企业信息;</param>
        switch (Act) {
            case 1:
                BtStatusClass(BtObj, 1);
                setTimeout("TransingStatus.SetStatus(3);MovingBoxCTL.Close();Item1.Del()", 1000);
                TransingStatus.SetStatus(1);
                //TransInvest.Trans(1, Item1.FocusItem);
                break;
            case 2:
                BtStatusClass(BtObj, 1);
                var ItemNo = BtObj.parentElement.parentElement.querySelectorAll(".ItemText")[0].innerText;
                this.Securities(ItemNo, BtObj.innerText);
                break
            case 3:
                BtStatusClass(BtObj, 1);
                this.MovingBox();
                break;
            case 4:
                BtStatusClass(BtObj, 1);
                this.FocusItem = BtObj.parentElement;
                var Title = BtObj.parentElement.querySelectorAll(".ItemText")[0].innerText;
                MovingBoxCTL.Prepare(Title + " - 预约", document.querySelector(".UExpenditure"));
                MovingBoxCTL.Open();
                break;
            case 5:
                BtStatusClass(BtObj, 1);
                var Title = BtObj.parentElement.parentElement.parentElement.querySelectorAll(".ItemText")[0].innerText;
                MovingBoxCTL.Prepare(Title + " - 企业信息：" + BtObj.parentElement.querySelectorAll(".BBtInTd")[0].innerText, document.querySelector(".UCompanyInfo"));
                MovingBoxCTL.Open();
                break;
            default:
                BtStatusClass(BtObj, 2);
                break;
        }

    }
};



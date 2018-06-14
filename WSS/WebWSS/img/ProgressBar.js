/**---------------------------------------
*refrence:MicrosoftAjax.js,jQuery.js
*function: 滚动条
-------------------------------------------*/

function ProgressBar(aliveSeconds,width,height,tickWidth,tickCount) {
    this.Timer = null;
    this.Width = typeof (width) == 'undefined' ? 360 : width;
    this.Height = typeof (height) == 'undefined' ? 60 : height;
    this.AliveSeconds = typeof (aliveSeconds) == 'undefined' ? 10 : aliveSeconds;
    this.TickWidth = typeof (tickWidth) == 'undefined' ? 2 : tickWidth;
    this.TickCount = typeof (tickCount) == 'undefined' ? 100 : tickCount;
    this.StepManually = false;//是否手动设置前进
    this.CompleteEvent = null;
    this.TipMessage = "数据正在加载中......";
    this._outer = null;
    this._inner=null;
    this._progrss = null;
    this._innerDown = null;
    this._mark = null;
    this._tickWidth = 0;
}

ProgressBar.prototype = {
    initialize: function() {

    },
    _createProgressBar: function() {
        var outer = document.createElement("DIV");
        var inner = document.createElement("DIV");
        var innerDown = document.createElement("DIV");
        var prs = document.createElement("DIV");
        var mask = document.createElement("DIV");

        prs.style.backgroundColor = "#467ef0";
        prs.style.width = "10px";
        prs.style.padding = "0px 0px 0px 0px";
        prs.style.margin = "0px 0px 0px 0px";

        outer.style.width = this.Width + "px";
        outer.style.height = this.Height + "px";
        outer.style.backgroundColor = "#E7FDFE";
        outer.style.border = "solid #022B2D 1px";
        outer.style.position = "absolute";
        outer.style.zIndex = "1000";
        outer.style.padding = "0px 0px 0px 0px";
        outer.style.margin = "0px 0px 0px 0px";
        outer.style.left = (document.documentElement.offsetWidth - this.Width) / 2 + "px";
        outer.style.top = (document.documentElement.offsetHeight - this.Height) / 2 + "px";
        outer.style.filter = "Alpha(opacity=95)";

        inner.style.width = (this.Width - 20) + "px";
        inner.style.height = "23px";
        inner.style.padding = "0px 0px 0px 0px";
        inner.style.margin = "10px 10px 0px 10px";
        inner.style.backgroundColor = "#ffffff";
        inner.style.border = "solid #022B2D 1px";

        innerDown.style.width = inner.style.width;
        innerDown.style.height = "23px";
        innerDown.style.padding = "0px 0px 0px 0px";
        innerDown.style.margin = "3px auto";
        innerDown.style.textAlign = "center";
        innerDown.style.fontSize = "14px";
        innerDown.style.fontWeight = "bold";
        innerDown.style.color = "#710425";
        prs.style.height = inner.style.height;

        mask.style.width = document.documentElement.offsetWidth + "px";
        mask.style.height = document.documentElement.offsetHeight + "px";
        mask.style.backgroundColor = "#888888";
        mask.style.position = "absolute";
        mask.style.zIndex = "500";
        mask.style.padding = "0px 0px 0px 0px";
        mask.style.margin = "0px 0px 0px 0px";
        mask.style.left = "0px";
        mask.style.top = "0px";
        mask.style.filter = "Alpha(opacity=65)";
        mask.style.display = "none";

        inner.appendChild(prs);
        outer.appendChild(inner);
        outer.appendChild(innerDown);
        document.body.appendChild(outer);
        document.body.appendChild(mask);

        this._outer = outer;
        this._inner = inner;
        this._progrss = prs;
        this._innerDown = innerDown;
        this._mark = mask;
        window.Progress = this;

        if (this.StepManually) {
            this._tickWidth = this._getTickWidth();
        }
        else {
            var tick = this._getIntervalTick();
            this.Timer = setInterval(this._graduallyChanging, tick);
        }
    },

    _getIntervalTick: function() {
        var totalWidth = this._inner.style.pixelWidth;
        var currentWidth = this._progrss.style.pixelWidth;
        var tick = this._round(this.AliveSeconds * 1000 * this.TickWidth / (totalWidth - currentWidth), 0);
        return tick;
    },

    _graduallyChanging: function() {
        var totalWidth = window.Progress._inner.style.pixelWidth;
        var currentWidth = window.Progress._progrss.style.pixelWidth;
        var percent = window.Progress._round(currentWidth * 100 / totalWidth, 0);
        if (currentWidth < totalWidth) {
            window.Progress._progrss.style.width = currentWidth + window.Progress.TickWidth + "px";
            window.Progress._innerDown.innerText = window.Progress.TipMessage + percent + "%";
            window.Progress._mark.style.display = "block";
        }
        else {
            if (window.Progress.CompleteEvent != null) {
                if (typeof window.Progress.CompleteEvent == 'function')
                    window.Progress.CompleteEvent.call();
                else
                    eval(window.Progress.CompleteEvent);
            }
        }
    },

    step: function(currentStep) {
        var currentWidth = window.Progress._progrss.style.pixelWidth;
        if (currentStep < window.Progress.TickCount) {
            window.Progress._progrss.style.width = currentWidth + window.Progress._tickWidth + "px";
            window.Progress._innerDown.innerText = window.Progress.TipMessage;
            window.Progress._mark.style.display = "block";
        }
        else {
            if (window.Progress.CompleteEvent != null) {
                if (typeof window.Progress.CompleteEvent == 'function')
                    window.Progress.CompleteEvent.call();
                else
                    eval(window.Progress.CompleteEvent);
            }
        }
    },

    _getTickWidth: function() {
        var totalWidth = this._inner.style.pixelWidth;
        var currentWidth = this._progrss.style.pixelWidth;
        var tickWidth = this._round((totalWidth - currentWidth) / this.TickCount, 0);
        return tickWidth;
    },

    _round: function(number, pos) {
        var n = new Number(number);
        var s = Math.pow(10, pos) * n;
        var t = Math.round(s);
        return t / Math.pow(10, pos);
    },

    show: function() {
        if (window.Progress != null) {
            window.Progress.hide();
        }
        this._createProgressBar();
    },

    hide: function() {
        if (this._outer != null) {
            if (this._outer.parentNode != null) {
                this._outer.parentNode.removeChild(this._outer);
            }
        }
        if (this._mark != null) {
            if (this._mark.parentNode != null) {
                this._mark.parentNode.removeChild(this._mark);
            }
        }
        this.dispose();
    },
    dispose: function() {
        clearInterval(this.Timer);
        this.Timer = null;
        this._outer = null;
        this._inner = null;
        window.Progress = null;
    }
};
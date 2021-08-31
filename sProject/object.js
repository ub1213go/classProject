    var gameRolling = false;
var deleteOK = true;

// 物件 & 視窗
var gamePic = {
    width: $(".gamePic").width(),
    height: $(".gamePic").height(),
    halfWidth: $(".gamePic").width() / 2,
    halfHeight: $(".gamePic").height() / 2
}
// 物件 & 按鍵
var KeyObject = {
    Up: 0,
    Down: 0,
    Left: 0,
    Right: 0,
    KeyKeepDown: function () { return (this.Up + this.Down + this.Left + this.Right) },
    reset: function(){
        this.Up = 0;
        this.Down = 0;
        this.Left = 0;
        this.Right = 0;
    }
}
// 物件 & 速度
var manSpeed = {
    speed: 0,
    // 左右轉速度
    turnLeftSpeed: 3,
    turnRightSpeed: 3,
    // 速度
    v: 0,
    // 加速度
    a: 0.3,
    count: 0,
    // 移動(起始速度, 最高速度)
    go: function (min, max) {
        this.v += this.a;
        this.v = limit(this.v, 0, 1);
        this.speed = this.speed + this.v;
        this.speed = limit(this.speed, min, max);
    },
    // 停止前進
    stop: function (min, max) {
        this.v -= this.a;
        this.v = limit(this.v, -0.1, 0);
        this.speed = this.speed + this.v;
        this.speed = limit(this.speed, min, max);
    },
    // 剎車
    brakes: function (min, max) {
        this.v -= (this.a + 0.045);
        this.v = limit(this.v, -4, 0);
        this.speed = this.speed + this.v;
        this.speed = limit(this.speed, min, max);
    },
    // 重設屬性
    reset: function () {
        this.v = 0;
        this.a = 0.005;
        this.speed = 0;
        this.audioPlay = false;
    }
}
// 物件 & 位置
var gamePlace1 = {
    // 場地圖片 
    backgroundURL: "url('./picBackground.png')",
    // 同向, 左車道
    sameRoadLeft: {
        left: "385px",
        top: "-100px"
    },
    // 同向, 右車道
    sameRoadRight: {
        left: "475px",
        top: "-100px"
    },
    // 同向, 右車道
    sameRoadMiddle: {
        left: "430px",
        top: "-100px"
    },
    // 逆向, 左車道
    reverseRoadLeft: {
        left: "170px",
        top: "-100px"
    },
    // 逆向, 右車道
    reverseRoadRight: {
        left: "270px",
        top: "-100px"
    },
    // 逆向, 中間車道
    reverseRoadMiddle: {
        left: "220px",
        top: "-100px"
    },
}
// 陣列 & npc
var npcCarArr = [];
var directionArr = ["same", "reverse"];
var gamePlaceArr = [
    gamePlace1.sameRoadLeft,
    gamePlace1.sameRoadRight,
    gamePlace1.sameRoadMiddle,
    gamePlace1.reverseRoadLeft,
    gamePlace1.reverseRoadRight,
    gamePlace1.reverseRoadMiddle
];

// 函式物件
function npcCar1(pos, speed, direction) {
    /* 屬性: 速度, 加速度, 圖片
       方法: 切左車道, 切右車道 */
    if (direction == "same") {
        this.derection = 1;
        this.backgroundURL = "url('./car2_up.png')"
    }
    else if (direction == "reverse") {
        this.derection = -1;
        this.backgroundURL = "url('./car_down.png')"
    }
    this.speed = speed;
    this.a = 0.3;
    this.v = 0;
    this.turnLeftSpeed = 3;
    this.turnRightSpeed = 3;
    this.switchTurn = 0;
    this.tempCount = 0;
    this.position = pos;
    this.timer;
    this.selfJQ = $(".gamePic").append("<div class='npcCar'></div>").children("div[class='npcCar']:last").css({
        "width": "50px",
        "height": "100px",
        "position": "absolute",
        "left": parseInt(this.position.left) + "px",
        "top": parseInt(this.position.top) + "px",
        "background-image": this.backgroundURL,
        "z-index": "1"
    })
    // 汽車陰影
    this.selfShadow = $(".gamePic").append("<div class='shadowCar'></div>").find("div[class='shadowCar']:last").css({
        "width": "100px",
        "height": "90px",
        "position": "absolute",
        "background-color": "hsl(0,0%,0%,0.3)",
        "-webkit-clip-path": "ellipse(25% 50% at 50% 50%)",
        "z-index": "0",
        "visibility": "hidden"
    })
    // 移動(起始速度, 最高速度)
    this.go = function (min, max) {
        this.v += this.a;
        this.v = limit(this.v, 0, 1);
        this.speed = this.speed + this.v;
        this.speed = limit(this.speed, min, max);
    },
        // 停止前進
        this.stop = function (min, max) {
            this.v -= this.a;
            this.v = limit(this.v, -0.1, 0);
            this.speed = this.speed + this.v;
            this.speed = limit(this.speed, min, max);
        },
        // 剎車
        this.brakes = function (min, max) {
            this.v -= (this.a + 0.045);
            this.v = limit(this.v, -4, 0);
            this.speed = this.speed + this.v;
            this.speed = limit(this.speed, min, max);
        },
        // 重設屬性
        this.reset = function () {
            this.v = 0;
            this.a = 0.005;
            this.speed = 0;
        }

    // 往左切
    this.turnLeft = function () {
        if (!this.switchTurn) {
            this.switchTurn = 1;
            this.tempCount = 0;
            this.timer = window.setInterval(() => {
                $(this.selfJQ).css("left", parseInt($(this.selfJQ).css("left")) - 1 + "px");
                this.tempCount++;
                if (this.tempCount == 30 || gameRolling == false) {
                    clearInterval(this.timer);
                    this.switchTurn = 0;
                }
            }, 200);
        }

    }
    // 切右切
    this.turnRight = function () {
        if (!this.switchTurn) {
            this.switchTurn = 1;
            this.tempCount = 0;
            this.timer = window.setInterval(() => {
                $(this.selfJQ).css("left", parseInt($(this.selfJQ).css("left")) + 1 + "px");
                this.tempCount++;
                if (this.tempCount == 30 || gameRolling == false) {
                    clearInterval(this.timer);
                    this.switchTurn = 0;
                }
            }, 200);
        }
    }
}



// 物件 & npc製造機
var timerNpcCreator = {
    timerCreateNpcCar: null,
    // x : 限制製造的數量
    // x : 5,

    createRandom: function(x, y) {
        return (Math.round(Math.random() * (y - x)) + x);
    },

    createTimer: function () {
        this.timerCreateNpcCar = window.setInterval(this.createNpcCar
            , 1000)
    },

    createNpcCar: function () {
        if (deleteOK && npcCarArr.length <= 5) {
            npcCarArr.push(new npcCar1(gamePlaceArr[timerNpcCreator.createRandom(0, 2)], timerNpcCreator.createRandom(10, 15), "same"));
            npcCarArr.push(new npcCar1(gamePlaceArr[timerNpcCreator.createRandom(3, 5)], timerNpcCreator.createRandom(10, 15), "reverse"));
        }
    },

    clearTimer: function () { 
        clearInterval(this.timerCreateNpcCar) 

    }
}
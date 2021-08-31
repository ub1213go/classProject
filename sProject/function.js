
var timerGame = null;
const FPS = 60;


// 函式 & 限制大小
function limit(x, min, max) {
    if (x > max) x = max;
    if (x < min) x = min;
    return x;
}

// 函式 & 遊戲結束
function gameOver() {
    StopGameTimer();
    timerNpcCreator.clearTimer();
    KeyObject.reset();
    manSpeed.reset();
    window.onkeydown = function (event) {
        if (event.keyCode == 32) {
            clearNpcCarArr(npcCarArr);
            $(".gameOver").remove();
            $(".reStart").remove();
            gameStart(FPS);
        }
    };
    window.onkeyup = null;
    $(".gamePic").append("<div class='gameOver'>GameOver</div>").find(":last").css({
        "height": "100%",
        "width": "100%",
        "position": "absolute",
        "background-image": "linear-gradient(rgba(0,0,0,0.4),rgba(0,0,0,0.4)), url('./human_die.png')",
        "background-position": "center",
        "background-repeat": "no-repeat",
        "color": "white",
        "font-weight": "blod",
        "font-family": "Verdana",
        "font-size": "100px",
        "display": "flex",
        "justify-content": "center",
        "align-items": "center",
        "z-index": "2",
        "user-select": "none",
        "opacity": "0",
    }).animate({ "opacity": "0.8" }).append("<div class='reStart'>重新開始</div>").find(":last").css({
        "font-size": "32px",
        "display": "flex",
        "position": "absolute",
        "top": "400px",
    }).hover(function () {
        $(this).css({
            "font-size": "48px",
            "cursor": "pointer",
            "border-radius": "15px",
            "padding": "10px",
            "background-color": "hsl(0, 0%, 0%, 1)"
        })
    }, function () {
        $(this).css({
            "font-size": "32px",
            "background-color": "",
        })
    }).one("click", function () {
        clearNpcCarArr(npcCarArr);
        $(".gameOver").remove();
        $(".reStart").remove();
        gameStart(FPS);
    })
}
// 函式 & 陰影綁定物件(物件, 陰影, x位移, y位移)
function clipShadow(ob1, ob2, paraX, paraY) {
    $(ob2).css("left", parseInt($(ob1).css("left")) + paraX + "px");
    $(ob2).css("top", parseInt($(ob1).css("top")) + paraY + "px");
    $(ob2).css("visibility", "visible");
}
// 函式 & 每個物件根據主角移動
function npcMove(ob1) {
    ob1.forEach(element => {
        let tempSpeed = (element.derection * element.speed) - manSpeed.speed;
        let tempTop = parseInt($(element.selfJQ).css("top"));
        $(element.selfJQ).css("top", tempTop - tempSpeed + "px");
    });
}

/* 物件 & 刪除範圍太遠的物件 */
var OverRangeManager = {
    timer: null,
    
    createTimer: function(arr){
        deleteOK = false;
        /* 每1秒刪除一次物件 */
        OverRangeManager.timer = setInterval(() => {
            for (i = 0; i < arr.length; i++) {
                if (parseInt($(arr[i].selfJQ).css("top")) > 700 || parseInt($(arr[i].selfJQ).css("top")) < -700) {
    
                    let garbage = arr.splice(i, 1);
                    garbage[0].selfJQ.remove();
                    garbage[0].selfShadow.remove();
                    delete garbage;
                    --i;
                }
            }
    
        }, 1000);
        deleteOK = true;
    },

    clearTimer: function(){
        clearInterval(OverRangeManager.timer)
    }

}
/* 函式 & 清空npcArr & 刪除jQ物件*/
function clearNpcCarArr(npcCarArr) {
    for (i = (npcCarArr.length - 1) ; i >= 0; i--) {
        $(npcCarArr[i].selfJQ).remove();
        $(npcCarArr[i].selfShadow).remove();
        npcCarArr.pop();
    }
}
// 函式 & 碰撞判定(自創物件類別,內縮值=> u, d, l, r)
function impact(ob1, up, down, left, right) {

    let tempHumanLeft = parseInt($(human).css("left"));
    let tempHumanTop = parseInt($(human).css("top")) + 5;
    let tempHumanWidth = parseInt($(human).width());
    let tempHumanHeight = parseInt($(human).height()) - 5;
    let tempCarLeft = parseInt($(ob1.selfJQ).css("left")) + left;
    let tempCarTop = parseInt($(ob1.selfJQ).css("top")) + up;
    let tempCarWidth = parseInt($(ob1.selfJQ).width()) - (right + left);
    let tempCarHeigth = parseInt($(ob1.selfJQ).height()) - (down + up);

    if(!(((tempHumanLeft + tempHumanWidth) < tempCarLeft) || (tempHumanLeft > (tempCarLeft + tempCarWidth))) && !((tempHumanTop > (tempCarTop + tempCarHeigth)) || ((tempHumanTop + tempHumanHeight) < tempCarTop))){
        return true;
    }else return false;
}
// 函式 & 進入指定範圍
function overRange() {
    let aa = parseInt($(human).css("left"));
    let bb = parseInt($(human).css("left")) + human.width();
    // 左邊界Left
    let dd1 = 145;
    // 右邊界Left
    let dd2 = 540;
    // 中間雙黃線Left邊界
    let dd3 = 337;
    let dd4 = 370;
    /* ------------------- */
    let cc1 = aa < dd1;
    let cc2 = bb > dd2;
    let cc3 = aa < dd4;
    let cc33 = bb > dd4;
    let cc4 = bb > dd3;
    let cc44 = aa < dd3;
    let cc5 = aa < dd3;
    let cc6 = bb > dd4;
    // 如果進入指定範圍
    if (cc1 || cc2 || ((cc3 & cc33) || (cc4 & cc44) || (cc5 && cc6)))
        return true;
    else return false;
}
// 函式 & 按鍵事件 & 按下
function keydown(event) {
    key = event.keyCode;
    switch (key) {
        case 38:
            KeyObject.Up = 1;
            break;
        case 40:
            if (KeyObject.Up == 0) {
                KeyObject.Down = 1;
                $(human).css("background-image", "url('./human_slow.png')")
            }
            if (KeyObject.Right == 1 && KeyObject.Down == 1) $(human).css("background-image", "url('./human_slow_right.png')");
            if (KeyObject.Left == 1 && KeyObject.Down == 1) $(human).css("background-image", "url('./human_slow_left.png')");
            break;
        case 37:
            KeyObject.Left = 1;
            $(human).css("background-image", "url('./human_left.png')");
            if (KeyObject.Right == 1) $(human).css("background-image", "url('./human.png')");
            break;
        case 39:
            KeyObject.Right = 1;
            $(human).css("background-image", "url('./human_right.png')")
            if (KeyObject.Left == 1) $(human).css("background-image", "url('./human.png')");
            break;

    }
}
// 函式 & 按鍵事件 & 放開
function keyup(event) {
    key = event.keyCode;
    switch (key) {
        case 38:
            KeyObject.Up = 0;
            break;
        case 40:
            KeyObject.Down = 0;
            if (KeyObject.KeyKeepDown() == 0 || KeyObject.Up == 1) $(human).css("background-image", "url('./human.png')");
            if (KeyObject.Right == 1) $(human).css("background-image", "url('./human_right.png')");
            if (KeyObject.Left == 1) $(human).css("background-image", "url('./human_left.png')");
            break;
        case 37:
            KeyObject.Left = 0;
            if (KeyObject.KeyKeepDown() == 0 || KeyObject.Up == 1) $(human).css("background-image", "url('./human.png')");
            if (KeyObject.Right == 1) $(human).css("background-image", "url('./human_right.png')");
            break;
        case 39:
            KeyObject.Right = 0;
            if (KeyObject.KeyKeepDown() == 0 || KeyObject.Up == 1) $(human).css("background-image", "url('./human.png')");
            if (KeyObject.Left == 1) $(human).css("background-image", "url('./human_left.png')");
            break;
        case 27:
            gameStop();
            event.stopPropagation();
            break;
    }
}

// 函式 & 創造介面: 暫停, 分數
function beginTopFace() {
    $(".gamePic").append("<div id='buttonStop'></div>" +
        "<div id='Point'></div>").find("#buttonStop, #Point").css({
            "width": "30px",
            "height": "30px",
            "position": "absolute",
            "background-position": "center",
            "background-repeat": "no-repeat",
            "border": "none",
            "user-select": "none",

        })
        .each(function (n, o) {
            let tempLeft = parseInt($(o).css("left"));
            let tempTop = parseInt($(o).css("Top"));
            $(o).css("top", tempTop + 10 + "px")
            $(o).css("left", tempLeft + n * 50 + 10 + "px");
        })
        .not("#Point")
        .on("mouseenter", function () { $(this).css("cursor", "pointer") })
        .on("click", function () {
            if (this.id.includes("Stop")) {
                gameStop();
            }
        })
    $("#buttonStop").css("background-image", "linear-gradient(rgba(0,0,0,0.2),rgba(0,0,0,0.2)), url('./stop.png')");
    $("#Point").css({
        "background-image": "linear-gradient(rgba(0,0,0,0.2),rgba(0,0,0,0.2))",
        "width": "300px",
        "height": "70px",
        "color": "white",
        "font-weight": "blod",
        "font-family": "Verdana",
        "font-size": "12px"
    });
}
/* 勝利條件達成 */
function win() {
    StopGameTimer();
    window.onkeyup = null;
    window.onkeydown = null;
    timerNpcCreator.clearTimer();
    KeyObject.reset();
    manSpeed.reset();
    // 創建暫停屏幕
    $(".gamePic").append("<div id='Win'></div>")
        .find("#Win")
        .css({
            "background-image": "linear-gradient(rgba(0,0,0,0.2),rgba(0,0,0,0.2)), url('./win.png')",
            "background-color": "hsla(0, 0%, 0%, 0.5)",
            "z-index": "2",
            "color": "white",
            "user-select": "none",
            "display": "flex",
            "align-items": "center",
            "justify-content": "center",
        }).animate({
            "width": "100%",
            "height": "100%"

        }, 200)
}
// 開始遊戲
var distance = 0;
var init = true;

function gameStart(FPS) {
    // 初始化 & 剛開始
    distance = 0;
    if(init){
        beginTopFace();
        Point.innerHTML = `<h1>距離:11km / ${distance}km</h1>`;
        init = false;
    }
    // 初始位置 & 背景
    $(".gamePic").css({
        "background-position-y" : "0px",
        "background-image" : "url('./picBackground.png')",
    });
    // 初始位置 & 人物
    human = $(".human");
    $(human).css({
        "background-image" : "url('./human.png')",
        "left" : gamePic.halfWidth + 130 + "px" ,
        "top" : gamePic.height - $(human).height() - 20 + "px",
    });
    // 初始位置 & 人物陰影
    $(".gamePic").append("<div id='shadowMan'></div>").find("#shadowMan").css({
        "width": "40px",
        "height": "40px",
        "position": "absolute",
        "background-color": "hsl(0,0%,0%,0.3)",
        "-webkit-clip-path": "ellipse(25% 50% at 50% 50%)",
        "z-index": "0"
    })

    /*  
    // location.assign("https://www.w3schools.com");

    // 測試用 - 背景圖的路徑
    if (!$(".gamePic").css("background-image").match(/picBackground/i)) {
        $(".gamePic").css("background-image", gamePlace1.backgroundURL);
        // url("./picBackground.png")
    }
    // 測試用 - 回傳視窗地址
    // console.log(location.href);

    */
   // 測試用 - 檢查位置 
    document.getElementsByClassName("gamePic")[0].onmousedown = function (event) {
        console.log(`event.offsetX : ${event.offsetX}, event.offsetY : ${event.offsetY}`);
    }
    window.onkeydown = keydown;
    window.onkeyup = keyup;


    // 執行 & timer & 圖片移動
    StartGameTimer(FPS);
    // 執行 & timer & 回收 
    OverRangeManager.createTimer(npcCarArr);
    // 執行 & timer & 製造npc 
    timerNpcCreator.createTimer();
    
}

// 遊戲捲動
function gameMain() {

    if (KeyObject.KeyKeepDown() > 0) {
        if (KeyObject.Up == 1) {
            manSpeed.go(3, 40);
        }
        if (KeyObject.Down == 1 && KeyObject.Up == 0) {
            manSpeed.brakes(0, 40);
        }
        if (KeyObject.Left == 1) {
            tempLeft = parseInt($(human).css("left"));
            if (tempLeft >= 0) {
                $(human).css("left", tempLeft - manSpeed.turnLeftSpeed + "px");
            }
        }
        if (KeyObject.Right == 1) {
            tempRight = parseInt($(human).css("left"));
            if (tempRight <= (700 - $(human).width())) {
                $(human).css("left", tempRight + manSpeed.turnRightSpeed + "px");
            }
        }
        tempTop = parseInt($(".gamePic").css("background-position-y"));
        if (tempTop >= 10000) {
            $(".gamePic").css("background-position-y", "0px");
            tempTop = parseInt($(".gamePic").css("background-position-y"));
        }
        $(".gamePic").css("background-position-y", tempTop + manSpeed.speed + "px");
        
    } else if (manSpeed.speed > 0) {
        manSpeed.stop(0, 40);
        tempTop = parseInt($(".gamePic").css("background-position-y"));
        $(".gamePic").css("background-position-y", tempTop + manSpeed.speed + "px");
    } else if (manSpeed.speed <= 0) {
        manSpeed.reset();
    }
    
    // 顯示距離 & 超出範圍懲罰 & 勝利達成
    if(overRange()){
        $("#Point").css("background-image", "linear-gradient(rgba(255,0,0,0.2),rgba(255,0,0,0.2))")
    }else{
        $("#Point").css("background-image", "linear-gradient(rgba(0,0,0,0.2),rgba(0,0,0,0.2))")
        distance += manSpeed.speed / 50;
    }
    tempValue = Math.floor(distance)/100;
    Point.innerHTML = `<h1>距離:11km / ${tempValue}km</h1>`
    if (tempValue >= 11) {
        win();
    }
    
    // 執行 & 相對位置
    npcMove(npcCarArr);
    // 執行 & 陰影綁定
    clipShadow($(human), $("#shadowMan"), -5, 9);
    // 執行 & 對每個車子做控制 & npcCar陰影綁定 & 碰撞時處理
    npcCarArr.forEach(function (element) {
        clipShadow($(element.selfJQ), $(element.selfShadow), -30, 5);
        /*
        每台車輛檢測是否重疊，如果重疊兩台車的圖片改成車禍
        
        每輛車互相檢測是否重疊=>{
            使用兩個陣列，一個是目前所有NPC的陣列，另一個是從NPC的陣列拿出一個來放入比對其他NPC的陣列

            第一個陣列(NPC的陣列)=>{
                暫時陣列，用完必須刪除
                copy目前NPC陣列=> =創造新陣列

                
            }
            第二個陣列(比對用){
                暫時陣列，用完也必須刪除
            }
        }

        車禍=>{
            把速度改為0
            把圖片改為車禍的圖片
        }

        如果沒有重疊請降速或者轉換車道

        降速=>{

        }

        轉換車道=>{

        }
        */
        /* -------------------------------------- */
        /* 放入程式碼 */
        /* -------------------------------------- */
        // npcCar 撞到人
        if (impact(element, 10, 10, 10, 10)) {
            $(human).css("background-image", "url('./human_dead.png')");
            gameOver();
        }
        let a = timerNpcCreator.createRandom(0, 20);
        if (a == 10) {
            element.turnLeft();
        } else if (a == 20) {
            element.turnRight();
        }
    })
}
// 函式 & 開始圖片滾動
function StartGameTimer(FPS){
    gameRolling = true;
    timerGame = window.setInterval(gameMain, 1000 / FPS);
}
// 函式 & 結束圖片滾動
function StopGameTimer(){
    gameRolling = false;
    clearInterval(timerGame);
}
// 函式 & 遊戲暫停
function gameStop() {
    StopGameTimer();
    let tempKeyup = window.onkeyup;
    let tempKeydown = window.onkeydown;
    window.onkeydown = null;
    // 創建暫停屏幕
    $(".gamePic").append("<div id='stopPic'>ESC 取消暫停</div>")
        .find("#stopPic")
        .css({
            "background-color": "hsla(0, 0%, 0%, 0.7)",
            "z-index": "1",
            "height": "0px",
            "color": "white",
            "user-select": "none",
            "display": "flex",
            "align-items": "center",
            "justify-content": "center",
            "font-size": "32px",
            "font-weight": "blod",
            "font-family": "Verdana"
        }).animate({
            "width": "100%",
            "height": "100%"

        }, 200)
    // 按下ESC, 取消暫停
    window.onkeyup = function (event) {
        if (event.keyCode == 27) {
            $("#stopPic").remove();
            StartGameTimer(FPS);
            window.onkeydown = tempKeydown;
            window.onkeyup = tempKeyup;
        }
    };
}

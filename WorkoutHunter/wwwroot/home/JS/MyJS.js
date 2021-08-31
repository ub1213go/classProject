
function CreateTitle() {
    function myFunction(e) {
        var title = e.parentNode.previousElementSibling;
        title.style.visibility = "visible";
        e.placeholder = "";
    }
    function myFunction2(e) {
        var title = e.parentNode.previousElementSibling;
        e.placeholder = title.innerText;
        title.style.visibility = "hidden";
    }
    var titles = document.querySelectorAll(".MyInputTitle");
    var inputBox = document.querySelectorAll(".MyCssInputBox");
    for (let i = 0; i < titles.length; i++) {
        titles[i].style.visibility = "hidden";
        titles[i].className += " MyCssInputTitle";
    }
    for (let i = 0; i < inputBox.length; i++) {
        inputBox[i].onfocus = function () {
            myFunction(this);
        }
        inputBox[i].onblur = function () {
            myFunction2(this);
        }
    }
}

function CheckRePassword(e) {
    var MyCssInputBoxs = document.querySelectorAll(".MyCssInputBox");
    if (MyCssInputBoxs.length == 2 && MyCssInputBoxs[0].value != MyCssInputBoxs[1].value) {
        e.type = "button";
        document.querySelector(".MyCssHint").innerText = "密碼不一致，請重新輸入";
    }
    else {
        e.type = "submit";
    }
}

var MyCheckRePasswordButton = document.querySelector("#MyCheckRePasswordButton");
if (MyCheckRePasswordButton != null) {
    MyCheckRePasswordButton.addEventListener("click", function () {
        CheckRePassword(MyCheckRePasswordButton);
    })
}

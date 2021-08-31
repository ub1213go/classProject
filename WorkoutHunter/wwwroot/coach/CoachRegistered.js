// 點擊左方學員按鈕，出現學員名稱和照片
function student_display(evt, studentNumber, studentName) {
	var i, studentContent, studentImg, tablinks;
	studentContent = document.getElementsByClassName("studentContent"); // 學員姓名div
	for (i = 0; i < studentContent.length; i++) {
		studentContent[i].style.display = "none";
	}
	studentImg = document.getElementsByClassName("studentImg"); // 學員照片
	for (i = 0; i < studentImg.length; i++) {
		studentImg[i].style.display = "none";
	}
	tablinks = document.getElementsByClassName("tablinks"); // 學員按鈕  tablinks=>按鈕的class
	for (i = 0; i < tablinks.length; i++) {
		tablinks[i].className = tablinks[i].className.replace(" active", "");
		tablinks[i].className = tablinks[i].className.replace(" MySelectBtn", "");

	}
	console.log(studentNumber);
  evt.currentTarget.className += " active";
  evt.currentTarget.className += " MySelectBtn";

  document.getElementById("SName").value = studentName;
  document.getElementById("SID").value = studentNumber;
}
//Get the element with id="defaultOpen" and click on it
// document.getElementById("defaultOpen").click();

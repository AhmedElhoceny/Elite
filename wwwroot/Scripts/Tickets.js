

var bars = document.querySelectorAll('.meter > span');

setInterval(function () {
	bars.forEach(function (bar) {
		var getWidth = parseFloat(bar.dataset.progress);

		for (var i = 0; i < getWidth; i++) {
			bar.style.width = i + '%';
		}
	});
}, 500);

var flag = 1;

function showFilter(x) {
	if (flag == 1) {
		flag = 0;
		x.parentElement.parentElement.querySelector(".filter").style.height = "200px"
		x.parentElement.parentElement.querySelector(".filter").style.transform = "scale(1, 1)"
	} else {
		flag = 1;
		x.parentElement.parentElement.querySelector(".filter").style.height = "0"
		x.parentElement.parentElement.querySelector(".filter").style.transform = "scale(0, 0)"
	}

}
var FlagRequests = 0;
let FadeOutText = () => {
	$("#s-holder").fadeOut("slow")
	$(".dashboard").fadeOut("slow")
	$(".request").fadeOut("slow")
	$(".ticket-create").fadeIn("slow")

}

$(".request").slideUp(100);
let ShowRequests = () => {
	(FlagRequests == 0)
	FlagRequests = 1;
	$("#s-holder").fadeOut("slow")
	$(".dashboard").fadeOut("slow")
	$(".request").fadeOut("slow")
	$(".ticket-create").fadeOut("slow")
	$(".request").slideDown(900);
	$(".request-inputs").fadeIn("slow")
}

let ShowDashBoard = () => {
	$("#s-holder").fadeOut("slow")
	$(".dashboard").slideDown("slow")
	$(".request").fadeOut("slow")
	$(".ticket-create").fadeOut("slow")
	$(".request").slideUp(900);
}





//profile//
function ShowProfile() {

	$(".profile-option").slideToggle("fast")
}

//profile//

function changePassword() {

	$(".set-password").slideToggle("fast")
}
function showSearch() {
	$(".medium").toggleClass("show-search")
	$(".search-holder").fadeToggle("slow")
}
function specifiedUser() {
	$(".specified-user").slideToggle("fast")
}
function togggleUser(x) {
	x.querySelector("span").classList.toggle("toguser")
	x.querySelector("span").classList.toggle("tog")
	x.querySelector("div").classList.toggle("tog-icon")
}
function overFlow(x) {
	x.parentElement.style.overflowY = "scroll";
	x.style.opacity = "0";
	x.style.transition = ".3s ease";
}
$("document").ready(function () {
	$('.left').animate({ left: "0" }, 600, function () { $('.header').fadeIn("slow", function () { $('.medium').fadeIn("slow", function () { $('.dashboard').fadeIn("slow") }) }); });
})
let ShowPendingRequests = () => {
	var UserId = document.getElementById("UserLoggedId").value;
	window.location.href = "/Home/ShowSentRequests?UserId=" + UserId;
}
let ShowApprovedRequests = ()=>{
	var UserId = document.getElementById("UserLoggedId").value;
	window.location.href = "/Home/ShowSentApprovedRequests?UserId=" + UserId;
}
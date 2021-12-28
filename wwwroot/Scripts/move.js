var searchBtn = document.getElementById("search-btn"),
    views = document.getElementById("views");

   searchBtn.onclick = function() {
    views.classList.toggle("views-toggle");
    views.style.transition = "all .3s ease-in";
}




var signIn = document.getElementById("sign-in"),
    signUp = document.getElementById("sign-up"),
    signDiv = document.getElementById("sign-up-div"),
    logDiv = document.getElementById("log-in-div"),
    logImg = document.getElementById("log-img");
signUp.onclick = function () {
    "use strict";
   
    signDiv.style.height = "100%";
}
signIn.onclick = function() {
    "use strict";
   logDiv.style.height = "100%";
   logImg.style.height = "100%";
}


    document.onkeydown = function(evt) {
    evt = evt || window.event;
    if(evt.keyCode == 27){
     signDiv.style.height = "0";
    logDiv.style.height = "0";
    logImg.style.height = "0";
    }
}


    
    
    
    
    
var thView = document.getElementById("twise-view"),
    fourthView = document.getElementById("main-view"),
    detail = document.getElementsByClassName("details"),
    imgs = document.getElementsByClassName("img"),
    quickV = document.getElementsByClassName("quick-v"),
    quickTxt = document.getElementsByClassName("quick"),
    showTog = document.getElementById("show-tog"),
    detailsImgs = document.getElementsByClassName("details-imgs"),
    myProduct = document.getElementsByClassName("product");

thView.onclick = function () {
    "use strict";
    var p;
for (p = 0; p < myProduct.length; p++) {
        myProduct[p].style.width = "1110px";
        myProduct[p].style.height = "500px";
        myProduct[p].style.transition = "all .5s ease"; 
    }
    var e;
    for (e = 0; e < detail.length; e++) {
        detail[e].style.display = "inline-block";
    }
    var i;
    for (i = 0; i < imgs.length; i++) {
        imgs[i].style.width = "50%";
    }
var q;
    for (q = 0; q < quickV.length; q++) {
        quickV[q].style.display = "none";
    }
    var d;
    for (d = 0; d < detailsImgs.length; d++) {
        detailsImgs[d].style.display = "block";
    }

}
   fourthView.onclick = function () {
    "use strict";
       var p;
for (p = 0; p < myProduct.length; p++) {
        myProduct[p].style.width = "250px";
        myProduct[p].style.height = "350px";
    }
    var e;
    for (e = 0; e < detail.length; e++) {
        detail[e].style.display = "none"
    }
    var i;
    for (i = 0; i < imgs.length; i++) {
        imgs[i].style.width = "100%";
    }
    var q;
    for (q = 0; q < quickV.length; q++) {
        quickV[q].style.display = "block";
    }
    var d;
    for (d = 0; d < detailsImgs.length; d++) {
        detailsImgs[d].style.display = "none";
    }
}

    var t;
    for(t = 0; t< quickTxt.length; t++) {
        quickTxt[t].onclick = function () {
            var p;
for (p = 0; p < myProduct.length; p++) {
        myProduct[p].style.width = "1200px";
        myProduct[p].style.height = "500px";
        myProduct[p].style.transition = "all .5s ease"; 
    }
    var e;
    for (e = 0; e < detail.length; e++) {
        detail[e].style.display = "inline-block";
    }
    var i;
    for (i = 0; i < imgs.length; i++) {
        imgs[i].style.width = "50%";
    }
var q;
    for (q = 0; q < quickV.length; q++) {
        quickV[q].style.display = "none";
    }
        }
    }
window.onscroll = function() {
    if(window.pageYOffset >= 30){
        views.style.height = "0";
        views.style.transition = "all .2s ease";
        showTog.style.display = "block";
    }else {
        views.style.height = "580px";
        showTog.style.display = "none";
    }
}




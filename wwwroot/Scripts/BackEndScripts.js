let RemoveUser = (element) => {
    let UserName = element.parentElement.parentElement.querySelector(".UserName").innerText;
    window.location.href = "/Admin/RemoveUser?UserName=" + UserName;
}
let ShowSignUpForm = (element) => {
    let UserName = element.parentElement.parentElement.querySelector(".UserName").innerText;
    document.getElementById("layout").style.height = "auto";
    document.getElementById("UserNameINputField").value = UserName;
    document.getElementById("UserNameINputField").disabled = true
}
let HideForm = () => {
    document.getElementById("layout").style.height = "0px";
}

let SubmitEdirOrCreateNew = (x) => {
    var Team = "";
    var Position = "";
    document.querySelectorAll(".team").forEach(ele => {
        if (ele.checked == true) {
            Team = ele.value;
        }
    })
    document.querySelectorAll(".title").forEach(ele => {
        if (ele.checked == true) {
            Position = ele.value;
        }
    })

    window.location.href = "/Home/CreateOrEdit?UserName=" + document.getElementById("UserNameINputField").value + "&Team=" + Team + "&Position=" + Position + "&Email=" + document.getElementById("EmailInput").value + "&Password=" + document.getElementById("PasswordInput").value
}
let AddUser = () => {
    document.getElementById("layout").style.height = "auto";
}
let ChangeProcessState = (State , ele) => {
    if (State == 'InProgress') {
        window.location.href = "/Home/ChangeProcessState?State=InProgress&UserName=" + document.getElementById("ClientUserName").value + "&PassWord=" + document.getElementById("ClienPassWord").value + "&TaskTitle=" + ele.parentElement.querySelector("input").value
    }
    else if (State == 'Closed') {
        window.location.href = "/Home/ChangeProcessState?State=Closed&UserName=" + document.getElementById("ClientUserName").value + "&PassWord=" + document.getElementById("ClienPassWord").value + "&TaskTitle=" + ele.parentElement.querySelector("input").value
    } 
}


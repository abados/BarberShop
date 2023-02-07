function change(day) {
    let myDay = document.getElementById(day);
    if (myDay.style.display == "none") {
        myDay.style.display = "";
    } else {
        myDay.style.display = "none";
    }
}
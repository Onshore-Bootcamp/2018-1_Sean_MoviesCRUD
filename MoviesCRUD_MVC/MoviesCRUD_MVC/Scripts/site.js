
var addMovieBtn = document.getElementById("AddMovieBTN");
var addActorBtn = document.getElementById("AddActorBTN");

if (addMovieBtn != undefined) {
    addMovieBtn.addEventListener("click", function () { document.location = "AddMovie" });
}
if (addActorBtn != undefined) {
    addActorBtn.addEventListener("click", function () { document.location = "AddActor" });
}


function startTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('txt').innerHTML =
        h + ":" + m + ":" + s;
    var t = setTimeout(startTime, 500);
}
function checkTime(i) {
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}
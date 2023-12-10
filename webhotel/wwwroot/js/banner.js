
if (document.getElementById('prev-bn') != null && document.getElementById('next-bn') != null) {

document.getElementById('next-bn').onclick = function () {
    let list = document.querySelectorAll('.bn-item');
    document.getElementById('bn-slide').appendChild(list[0]);
}
document.getElementById('prev-bn').onclick = function(){
    let list = document.querySelectorAll('.bn-item');
    document.getElementById('bn-slide').prepend(list[list.length - 1]);
}
}

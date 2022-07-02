$('input').change(function () {
    console.log("te");
    console.log(this.files[0].mozFullPath);
});
function removeEvents(tag){
    $(tag).click(function (event) {
        event.preventDefault();
    });
}


function closeModal(id) {
    mdb.Modal.getInstance(document.getElementById(id)).hide();
}
function openModal(id) {
    try {

        mdb.Modal.getInstance(document.getElementById(id)).show();
    } catch {
        const myModalEl = document.getElementById(id);
        const modal = new mdb.Modal(myModalEl);
        modal.show();
    }
}

function focusElement(id) {
    const element = document.getElementById(id);
    element.focus();
}
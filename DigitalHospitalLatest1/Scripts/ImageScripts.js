function ShowImagePreview(imageuploader, previewimage) {
    if (imageuploader.files && imageuploader.files[0]) {
        var reader = new fileReader();
        reader.onload = function (e) {
            $(previewimage).attr("src", e.target.result);
        }
        reader.readAsDataURL(imageuploader.files[0]);
    }
}
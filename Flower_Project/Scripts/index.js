var myWidget = cloudinary.createUploadWidget({
        cloudName: 'dks1ymxvy', //name
        uploadPreset: 'hxdwlopr' //presetCode
    },
    (error, result) => {
        if (!error && result && result.event === "success") {
            const imgLink = result.info.secure_url;
            document.querySelector("input[name='Avatar']").value = imgLink;
            const imgPreview = document.getElementById("preview");
            imgPreview.src = imgLink;
            imgPreview.classList.remove("hidden");
        }
    }
);

document.getElementById("upload_widget").addEventListener("click",
    function () {
        myWidget.open();
    },
    false);
$(function () {
    const avatarPreviewWidth = $(".upload-image-btn").outerWidth();
    $(".upload-image-btn").css({ 'height': (avatarPreviewWidth - 10) + 'px' });
    $("#preview").css({ 'height': (avatarPreviewWidth - 10) + 'px' });
});
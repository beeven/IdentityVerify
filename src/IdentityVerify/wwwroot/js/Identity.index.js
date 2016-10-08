"use strict";

function fileChanged(evt) {
    console.log("file changed");
}

var fitImageOn = function (canvas, imageObj) {
    var imageAspectRatio = imageObj.width / imageObj.height;
    var canvasAspectRatio = canvas.width / canvas.height;
    var renderableHeight, renderableWidth, xStart, yStart;

    // If image's aspect ratio is less than canvas's we fit on height
    // and place the image centrally along width
    if (imageAspectRatio < canvasAspectRatio) {
        renderableHeight = canvas.height;
        renderableWidth = imageObj.width * (renderableHeight / imageObj.height);
        xStart = (canvas.width - renderableWidth) / 2;
        yStart = 0;
    }

    // If image's aspect ratio is greater than canvas's we fit on width
    // and place the image centrally along height
    else if (imageAspectRatio > canvasAspectRatio) {
        renderableWidth = canvas.width
        renderableHeight = imageObj.height * (renderableWidth / imageObj.width);
        xStart = 0;
        yStart = (canvas.height - renderableHeight) / 2;
    }

    // Happy path - keep aspect ratio
    else {
        renderableHeight = canvas.height;
        renderableWidth = canvas.width;
        xStart = 0;
        yStart = 0;
    }
    console.log(imageObj.width, imageObj.height);
    var context = canvas.getContext('2d');
    context.drawImage(imageObj, xStart, yStart, renderableWidth, renderableHeight);
};

var fitVideoOn = function (canvas, videoObj) {
    var videoAspectRatio = videoObj.videoWidth / videoObj.videoHeight;
    var canvasAspectRatio = canvas.width / canvas.height;
    var renderableHeight, renderableWidth, xStart, yStart;

    // If image's aspect ratio is less than canvas's we fit on height
    // and place the image centrally along width
    if (videoAspectRatio < canvasAspectRatio) {
        renderableHeight = canvas.height;
        renderableWidth = videoObj.videoWidth * (renderableHeight / videoObj.videoHeight);
        xStart = (canvas.width - renderableWidth) / 2;
        yStart = 0;
    }

    // If image's aspect ratio is greater than canvas's we fit on width
    // and place the image centrally along height
    else if (videoAspectRatio > canvasAspectRatio) {
        renderableWidth = canvas.width
        renderableHeight = videoObj.videoHeight * (renderableWidth / videoObj.videoWidth);
        xStart = 0;
        yStart = (canvas.height - renderableHeight) / 2;
    }

    // Happy path - keep aspect ratio
    else {
        renderableHeight = canvas.height;
        renderableWidth = canvas.width;
        xStart = 0;
        yStart = 0;
    }
    var context = canvas.getContext('2d');
    context.drawImage(videoObj, xStart, yStart, renderableWidth, renderableHeight);

};


function IdentityVerifyView() {
    var that = this;

    var video = document.getElementById('video');
    this.HasCamera = ko.observable(true);
    this.ImageType = ko.observable("upload");

    function errBack(err) {
        that.HasCamera(false);
        console.log(err);
    }

    // Get access to the camera!
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // Not adding `{ audio: true }` since we only want video now
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            video.src = window.URL.createObjectURL(stream);
            video.play();
            that.ImageType("snap");
        }, errBack);
    }
    else if (navigator.getUserMedia) { // Standard
        navigator.getUserMedia({ video: true }, function (stream) {
            video.src = stream;
            video.play();
            that.ImageType("snap");
        }, errBack);
    } else if (navigator.webkitGetUserMedia) { // WebKit-prefixed
        navigator.webkitGetUserMedia({ video: true }, function (stream) {
            video.src = window.webkitURL.createObjectURL(stream);
            video.play();
            that.ImageType("snap");
        }, errBack);
    } else if (navigator.mozGetUserMedia) { // Mozilla-prefixed
        navigator.mozGetUserMedia({ video: true }, function (stream) {
            video.src = window.URL.createObjectURL(stream);
            video.play();
            that.ImageType("snap");
        }, errBack);
    } else {
        that.HasCamera(false);
    }


    // Elements for taking the snapshot
    var canvas = document.getElementById('canvas');
    var portrait = document.getElementById('portrait');
    var portraitFileElem = document.getElementById('portraitFileElem');




    this.ID = ko.observable();
    this.FullName = ko.observable();

    
    
    this.Portrait = ko.observable();
    this.submitting = ko.observable(false);
    this.submitBtnText = ko.observable("提交");
    this.hasPortrait = false;

    this.submitForm = function (event) {
        if (that.hasPortrait) {
            portrait.value = canvas.toDataURL("image/jpeg", 0.85);
        }

        return true;
    }
    this.onFormSubmit = function (event) {
        that.submitting(!that.submitting());
        that.submitBtnText("正在审核，大概需要20秒");
        return true;
    }
    this.uploadPortrait = function (event) {
        portraitFileElem.click();
    }
    this.snap = function (event) {
        fitVideoOn(canvas, video);
        //context.drawImage(video, 0, 0, 320, 240);
        that.hasPortrait = true;
        return false;
    }
    this.fileUploaded = function (data, event) {
        var file = event.target.files[0];
        console.log(file);
        if (!/^image\//.test(file.type)) { return; }
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.createElement("img");
            img.src = e.target.result;
            console.log(img);
            img.onload = function(){
                fitImageOn(canvas, img);
            }
            //context.drawImage(img, 0, 0, 320, 240);
        }
        reader.readAsDataURL(file);
        if (reader.result) {
            var img = document.createElement("img");
            img.src = e.target.result;
            fitImageOn(canvas, img);
        }
        that.hasPortrait = true;
    }
}
ko.applyBindings(new IdentityVerifyView());
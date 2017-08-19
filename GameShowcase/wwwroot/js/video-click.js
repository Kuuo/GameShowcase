var demoVideo = document.getElementById('demoVideo');

if (demoVideo != null) {
    demoVideo.onclick = function () {
        if (demoVideo.paused)
            demoVideo.play();
        else
            demoVideo.pause();
    }
}

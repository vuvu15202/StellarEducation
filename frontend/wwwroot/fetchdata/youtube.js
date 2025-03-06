
var player;

// This function creates an <iframe> (and YouTube player)
// after the API code downloads.
function onYouTubeIframeAPIReady() {
player = new YT.Player('player', {
    height: '390',
    width: '640',
    videoId: 'Az9GyMlb8S0', // Thay thế VIDEO_ID bằng ID của video YouTube
    events: {
    'onReady': onPlayerReady,
    'onStateChange': onPlayerStateChange
    }
});
}

// The API will call this function when the video player is ready.
function onPlayerReady(event) {
// Bắt đầu phát video (tùy chọn)
// event.target.playVideo();
}

function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.PLAYING) {
        setInterval(function () {
            var currentTime = player.getCurrentTime();
            document.getElementById('current-time').innerText = "Current Time: " + currentTime;
        }, 1000);
    }
}

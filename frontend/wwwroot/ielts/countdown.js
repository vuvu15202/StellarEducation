$(document).ready(function () {
    let countdownTime = 90; // Tổng thời gian (giây)

    function formatTime(seconds) {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
    }

    function updateCountdown() {
        const countdownElement = document.getElementById("countdown");

        if (countdownTime >= 0) {
            countdownElement.textContent = formatTime(countdownTime); // Cập nhật giao diện

            if (countdownTime <= 60) {
                countdownElement.classList.add("text-danger"); // Thêm class để đổi màu chữ
                countdownElement.classList.add("zoom-text"); // Thêm class để đổi màu chữ
            }

            countdownTime--;
        } else {
            clearInterval(timer); // Dừng bộ đếm
            countdownElement.textContent = "Time's up!";
            document.getElementById("lockScreen").classList.add("active"); // Kích hoạt khóa màn hình
        }
    }

    function onCountdownFinish() {
        alert("Countdown finished!");
    }

    // Cập nhật mỗi giây
    const timer = setInterval(updateCountdown, 1000);
});